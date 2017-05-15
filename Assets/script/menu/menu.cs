using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class menuComparer:IComparer<menu>
{
	private FilterType filterType;

	public menuComparer( FilterType type )
	{
		filterType = type;
	}

	public int Compare (menu x, menu y)
	{
		return x.getSubIndexByType(filterType) - y.getSubIndexByType(filterType);
	}
}

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
	private Cook cookWay;
	/**使用食材*/
	private Food useFood;
	/**主食*/
	private Staple useStaple;

	/**菜單主頁圖片*/
	private Sprite[] mainSpriteList;
	/**4個輔助頁的圖片*/
	private Sprite[][] assist1SpriteList; 
	/**輔助頁3的半透明文字*/
	private Sprite[] assist3wordList;
	/**輔助頁2要說出來的文字*/
	private Dictionary<string, string> assistSpeechText;

	public menu( int ID, string name, int price, Cook cook, Food food, Staple staple, Dictionary<string, string> speechText )
	{
		menuID = ID;
		menuName = name;
		this.price = price;
		cookWay = cook;
		useFood = food;
		useStaple = staple;
		assistSpeechText = speechText;

		int assistNum = assistSpriteNum.Length;
		assist1SpriteList = new Sprite[assistNum][];
	}

	public int getMenuID(){return menuID;}
	public string getMenuName(){return menuName;}
	public int getPrice(){return price;}
	public Cook getCookWay(){return cookWay;}
	public Food getUseFood(){return useFood;}
	public Staple getUseStaple(){return useStaple;}
	public Dictionary<string, string> getAssistSpeechText(){return assistSpeechText;}

	public Sprite[] getMainSpriteList()
	{
		if (mainSpriteList == null) 
		{
			mainSpriteList = new Sprite[mainSpriteNum];
			string path = "";
			for( int index = 0;index < mainSpriteNum;index++ )
			{
				path = string.Format (mainSpritePath, menuID, (index + 1));
				mainSpriteList[index] = Resources.Load<Sprite>(path);
			}
		}
		return mainSpriteList;
	}

	public Sprite[] getAssistSpriteList( int assistIdx )
	{
		if ((assistIdx < 0) || (assistIdx >= assist1SpriteList.Length)) {return null;}

		if (assist1SpriteList [assistIdx] == null) 
		{
			int loadSpriteNum = assistSpriteNum [assistIdx];
			assist1SpriteList[assistIdx] = new Sprite[ loadSpriteNum ];
			string path = "";
			for( int index = 0;index < loadSpriteNum;index++ )
			{
				path = string.Format (assistSpritePath, menuID, (assistIdx + 1), (index + 1));
				assist1SpriteList[assistIdx][index] = Resources.Load<Sprite>(path);
			}
		}

		return assist1SpriteList[assistIdx];
	}

	public Sprite[] getWordSpriteList()
	{
		if (assist3wordList == null) 
		{
			string path = "";
			assist3wordList = new Sprite[assistWordNum];
			for( int index = 0;index < assistWordNum;index++ )
			{
				path = string.Format (assistWordPath, menuID, (index + 1));
				assist3wordList[index] = Resources.Load<Sprite>(path);
			}
		}
		return assist3wordList;
	}

	public int getSubIndexByType( FilterType type )
	{
		switch (type) 
		{
		case FilterType.COOK:
			return (int)getCookWay();
		case FilterType.FOOD:
			return (int)getUseFood ();
		case FilterType.STAPLE:
			return (int)getUseStaple ();
		}

		return -1;
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

		menu m = new menu ( menuID, menuName, price, (Cook)cookWay, (Food)useFood, (Staple)useStaple, speechText );
		return m;
	}

}
#pragma warning restore 0649