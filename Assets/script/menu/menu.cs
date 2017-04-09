using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu 
{
	private static readonly string mainSpritePath = "menu/{0}/{1}";
	private static readonly int mainSpriteNum = 8;

	private static readonly string assistSpritePath = "menu/{0}/assist{1}/{2}";
	private static readonly int[] assistSpriteNum = new int[]{2,9,5,0};

	private static readonly string assistWordPath = "menu/{0}/assist3/w{1}";
	private static readonly int assistWordNum = 5;

	/**菜單編號*/
	private int menuID;
	/**菜單名稱*/
	private string menuName;
	/**價格*/
	private int price;
	/**料理方式*/
	private DataMgr.Cook cookWay;
	/**使用食材*/
	private DataMgr.Food useFood;
	/**主食*/
	private DataMgr.Staple useStaple;

	/**菜單主頁圖片*/
	private Sprite[] mainSpriteList;
	/**4個輔助頁的圖片*/
	private Sprite[][] assist1SpriteList; 
	/**輔助頁3的半透明文字*/
	private Sprite[] assist3wordList;
	/**輔助頁2要說出來的文字*/
	private Dictionary<string, string> assistSpeechText;

	public menu( int ID, string name, int price, DataMgr.Cook cook, DataMgr.Food food, DataMgr.Staple staple )
	{
		menuID = ID;
		menuName = name;
		this.price = price;
		cookWay = cook;
		useFood = food;
		useStaple = staple;

		loadMenuSprite ();
		loadSpeechText ();
	}

	public int getMenuID(){return menuID;}
	public string getMenuName(){return menuName;}
	public int getPrice(){return price;}
	public DataMgr.Cook getCookWay(){return cookWay;}
	public DataMgr.Food getUseFood(){return useFood;}
	public DataMgr.Staple getUseStaple(){return useStaple;}
	public Sprite[] getMainSpriteList(){return mainSpriteList;}
	public Sprite[] getWordSpriteList(){return assist3wordList;}
	public Dictionary<string, string> getAssistSpeechText(){return assistSpeechText;}

	private void loadMenuSprite()
	{
		mainSpriteList = new Sprite[mainSpriteNum];
		for( int index = 0;index < mainSpriteNum;index++ )
		{
			downloadMgr.Instance.downloadSprite (string.Format(mainSpritePath,menuID,(index+1)),loadMainSprite,index);
		}

		int assistNum = assistSpriteNum.Length;
		assist1SpriteList = new Sprite[assistNum][];
		for( int index = 0;index < assistNum;index++ )
		{
			int loadSpriteNum = assistSpriteNum [index];
			assist1SpriteList[index] = new Sprite[ loadSpriteNum ];
			for( int jIndex = 0;jIndex < loadSpriteNum;jIndex++ )
			{
				// Debug.logger.Log (string.Format("down path : {0}",string.Format(assistSpritePath,menuID,(index+1),(jIndex+1))));
				downloadMgr.Instance.downloadSprite ( string.Format(assistSpritePath,menuID,(index+1),(jIndex+1)), loadAssistSprite, new int[]{index, jIndex});
			}
		}

		assist3wordList = new Sprite[assistWordNum];
		for( int index = 0;index < assistWordNum;index++ )
		{
			// Debug.logger.Log (string.Format("down path : {0}",string.Format (assistWordPath, menuID, (index + 1))));
			downloadMgr.Instance.downloadSprite (string.Format(assistWordPath,menuID,(index+1)),loadWordSpriteList,index);
		}
	}

	private void loadSpeechText()
	{
		assistSpeechText = new Dictionary<string, string> ();
		// TEST
		assistSpeechText.Add("1", "宜蘭三星蔥");
		assistSpeechText.Add("5", "醬獨門");
		assistSpeechText.Add("6", "溫泉蛋");
		assistSpeechText.Add("8", "豬里肌肉彈口");
	}

	public void loadMainSprite(Sprite sprite, object userData)
	{
		// Debug.logger.Log (string.Format("loadMainSprite({0},{1})",sprite, userData));
		int index = (int)userData;
		mainSpriteList [index] = sprite;
	}

	public void loadAssistSprite(Sprite sprite, object userData)
	{
		int[] idx = (int[])userData;
		// Debug.logger.Log (string.Format("loadAssistSprite({0},( {1}, {2} ))",sprite, idx [0], idx [1]));

		assist1SpriteList [idx [0]] [idx [1]] = sprite;
	}


	public Sprite[] getAssistSpriteList( int assistIdx )
	{
		if ((assistIdx < 0) || (assistIdx >= assist1SpriteList.Length)) {return null;}

		return assist1SpriteList[assistIdx];
	}

	public void loadWordSpriteList(Sprite sprite, object userData)
	{
		// Debug.logger.Log (string.Format("loadMainSprite({0},{1})",sprite, userData));
		int index = (int)userData;
		assist3wordList [index] = sprite;
	}

}
