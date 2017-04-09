using System;
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

	public menu( int ID, string name, int price, DataMgr.Cook cook, DataMgr.Food food, DataMgr.Staple staple, Dictionary<string, string> speechText )
	{
		menuID = ID;
		menuName = name;
		this.price = price;
		cookWay = cook;
		useFood = food;
		useStaple = staple;
		assistSpeechText = speechText;

		loadMenuSprite ();
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

#pragma warning disable 0649
[Serializable]
class menuInfo
{
	public menuStruct[] data;
}

[Serializable]
class menuStruct
{
	public int menuID;
	public string menuName;
	public int price;
	public int cookWay;
	public int useFood;
	public int useStaple;
	public string Speech1;
	public string Speech5;
	public string Speech6;
	public string Speech8;

	public menu createMenu()
	{
		Dictionary<string, string> speechText = new Dictionary<string, string> ();
		// TEST
		speechText.Add("1", Speech1);
		speechText.Add("5", Speech5);
		speechText.Add("6", Speech6);
		speechText.Add("8", Speech8);

		menu m = new menu ( menuID, menuName, price, (DataMgr.Cook)cookWay, (DataMgr.Food)useFood, (DataMgr.Staple)useStaple, speechText );
		return m;
	}

}
#pragma warning restore 0649