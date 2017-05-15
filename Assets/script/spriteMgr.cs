using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class spriteMgr : MonoBehaviour 
{
	/**料理方式共6種*/
	private static readonly int COOK_WAY_COUNT = 7;
	private static readonly int USE_FOOD_COUNT = 5;
	private static readonly int USE_STAPLE_COUNT = 8;
	private static readonly int MENU_COUNT = 21;

	public class KeyWord
	{
		// 料理方式子分類圖
		public static readonly string INDEX_COOK = "INDEX_COOK";
		public static readonly string INDEX_COOK_L = "INDEX_COOK_L";

		// 食材子分類圖
		public static readonly string INDEX_FOOD = "INDEX_FOOD";
		public static readonly string INDEX_FOOD_L = "INDEX_FOOD_L";

		// 主食子分類圖
		public static readonly string INDEX_STAPLE = "INDEX_STAPLE";
		public static readonly string INDEX_STAPLE_L = "INDEX_STAPLE_L";

		// 過濾器 - 料理方式
		public static readonly string FILTER_COOK = "FILTER_COOK";
		public static readonly string FILTER_COOK_L = "FILTER_COOK_L";

		// 餐點確認
		public static readonly string WANT_ORDER = "WANT_ORDER";
		public static readonly string WANT_STAPLE = "WANT_STAPLE";
		public static readonly string ORDER_STAPLE_L = "ORDER_STAPLE_L";
		// public static readonly string ORDER_FLOOR = "ORDER_FLOOR";

	}

	private Dictionary<string,Dictionary<int,Sprite>> spriteMap;
	private static spriteMgr _instance = null;
	public static spriteMgr Instance
	{
		get{return _instance;}
	}
	// Use this for initialization
	private void Awake ()
	{
		_instance = this;
	}

	void Start()
	{
		spriteMap = new Dictionary<string,Dictionary<int,Sprite>> ();
		// 料理方式子分類圖
		StartCoroutine( loadResourceSprite(COOK_WAY_COUNT, "index/cook/cook{0}", KeyWord.INDEX_COOK) );
		StartCoroutine( loadResourceSprite(COOK_WAY_COUNT, "index/cook/cook_L{0}", KeyWord.INDEX_COOK_L) );

		// 食材子分類圖
		StartCoroutine( loadResourceSprite(USE_FOOD_COUNT, "index/food/food{0}", KeyWord.INDEX_FOOD) );
		StartCoroutine( loadResourceSprite(USE_FOOD_COUNT, "index/food/food_L{0}", KeyWord.INDEX_FOOD_L) );

		// 主食子分類圖
		StartCoroutine( loadResourceSprite(USE_STAPLE_COUNT, "index/staple/staple{0}", KeyWord.INDEX_STAPLE) );
		StartCoroutine( loadResourceSprite(USE_STAPLE_COUNT, "index/staple/staple_L{0}", KeyWord.INDEX_STAPLE_L) );


		// 過濾器 - 料理方式子分類圖
		StartCoroutine( loadResourceSprite(COOK_WAY_COUNT, "filter/cook/cook{0}", KeyWord.FILTER_COOK) );
		StartCoroutine( loadResourceSprite(COOK_WAY_COUNT, "filter/cook/cook_L{0}", KeyWord.FILTER_COOK_L) );


		// 餐點確認 - 餐點圖(右)
		StartCoroutine( loadResourceSprite(MENU_COUNT, "want/orderImg/{0}", KeyWord.WANT_ORDER) );

		// 餐點確認 - 主食縮圖(左)
		StartCoroutine( loadResourceSprite(USE_STAPLE_COUNT, "want/staple/{0}", KeyWord.WANT_STAPLE) );

		// 餐點確認 - 確認大圖
		StartCoroutine( loadResourceSprite(USE_STAPLE_COUNT, "check/L{0}", KeyWord.ORDER_STAPLE_L) );

		Resources.UnloadUnusedAssets ();
		pageMgr.Instance.OnSpriteMgrReady ();

	}
	// Update is called once per frame
	void Update () {
		
	}

	public Sprite getSprite( string keyWord, int spriteIndex )
	{
		if (!spriteMap.ContainsKey (keyWord)) { return null; }

		if (!spriteMap[keyWord].ContainsKey (spriteIndex)) { return null; }

		return spriteMap [keyWord] [spriteIndex];
	}

	public Sprite getSprite( FilterType type, bool isLarge, int spriteIndex )
	{
		string keyWord = getFilterSpriteKeyWord (type, isLarge);
		return getSprite (keyWord, spriteIndex);
	}

	public Dictionary<int, Sprite> getIndexSpriteMap( FilterType type, bool isLarge )
	{
		string keyWord = getIndexSpriteKeyWord( type, isLarge );
		if (string.IsNullOrEmpty (keyWord)) { return new Dictionary<int, Sprite> ();}

		return spriteMap [keyWord];
	}

	public Dictionary<int,Sprite> getSpriteMap( string keyWord )
	{
		return spriteMap [keyWord];
	}

	public string getFilterSpriteKeyWord (FilterType type, bool isLarge)
	{
		switch (type) 
		{
		case FilterType.COOK:
			return (isLarge) ? spriteMgr.KeyWord.FILTER_COOK_L:spriteMgr.KeyWord.FILTER_COOK;
		}

		return null;
	}

	public string getIndexSpriteKeyWord( FilterType type, bool isLarge )
	{
		switch (type) 
		{
		case FilterType.COOK:
			return (isLarge) ? spriteMgr.KeyWord.INDEX_COOK_L:spriteMgr.KeyWord.INDEX_COOK;

		case FilterType.FOOD:
			return (isLarge) ? spriteMgr.KeyWord.INDEX_FOOD_L:spriteMgr.KeyWord.INDEX_FOOD;

		case FilterType.STAPLE:
			return (isLarge) ? spriteMgr.KeyWord.INDEX_STAPLE_L:spriteMgr.KeyWord.INDEX_STAPLE;
		}

		return null;
	}

	private IEnumerator loadResourceSprite(int maxCounter, string path, string keyWord)
	{
		Debug.logger.Log (string.Format("loadResourceSprite({0},{1},{2})",maxCounter, path, keyWord));

		spriteMap.Add( keyWord, new Dictionary<int,Sprite>() );

		Sprite sprite = null;
		for(int counter = 1;counter <= maxCounter;counter++)
		{
			
			sprite = Resources.Load<Sprite>(string.Format(path,counter));

			if (sprite != null) 
			{
				spriteMap [keyWord].Add (counter, sprite);
			}
			else 
			{
				// Debug.logger.LogError ("[Yu-Ning]", string.Format("找不到圖片 : Resources/{0}",string.Format (path, counter) ));
			}
		}
		yield return new WaitForSeconds (1.0f);
	}
}
