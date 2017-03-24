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


	private int menuID;
	private string menuName;
	private int price;
	private DataMgr.Cook cookWay;
	private DataMgr.Food useFood;
	private DataMgr.Staple useStaple;

	private Sprite[] mainSpriteList;
	private Sprite[][] assist1SpriteList; 
	private Sprite[] assistwordList;

	public menu( int ID, string name, int price, DataMgr.Cook cook, DataMgr.Food food, DataMgr.Staple staple )
	{
		menuID = ID;
		menuName = name;
		this.price = price;
		cookWay = cook;
		useFood = food;
		useStaple = staple;

		loadMenuSprite ();
	}

	public int getMenuID(){return menuID;}
	public string getMenuName(){return menuName;}
	public int getPrice(){return price;}
	public DataMgr.Cook getCookWay(){return cookWay;}
	public DataMgr.Food getUseFood(){return useFood;}
	public DataMgr.Staple getUseStaple(){return useStaple;}
	public Sprite[] getMainSpriteList(){return mainSpriteList;}
	public Sprite[] getWordSpriteList(){return assistwordList;}

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

		assistwordList = new Sprite[assistWordNum];
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
		assistwordList [index] = sprite;
	}

}
