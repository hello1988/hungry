using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class spriteMgr : MonoBehaviour 
{
	/**料理方式共6種*/
	private static readonly int COOK_WAY_COUNT = 6;
	private static readonly int USE_FOOD_COUNT = 6;
	private static readonly int USE_STAPLE_COUNT = 9;
	private static readonly int MENU_COUNT = 37;

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

		// 過濾器 - 食材子分類圖
		// public static readonly string FILTER_FOOD = "FILTER_FOOD";
		// public static readonly string FILTER_FOOD_L = "FILTER_FOOD_L";

		// 過濾器 - 主食子分類圖
		// public static readonly string FILTER_STAPLE = "FILTER_STAPLE";
		// public static readonly string FILTER_STAPLE_L = "FILTER_STAPLE_L";

		// 餐點確認
		public static readonly string WANT_ORDER = "WANT_ORDER";
		public static readonly string WANT_STAPLE = "WANT_STAPLE";
		public static readonly string ORDER_STAPLE_L = "ORDER_STAPLE_L";
		public static readonly string ORDER_FLOOR = "ORDER_FLOOR";

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
		int counter = 1;
		string path = "index/cook/cook{0}";
		spriteMap.Add( KeyWord.INDEX_COOK, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.INDEX_COOK, counter, COOK_WAY_COUNT});

		counter = 1;
		path = "index/cook/cook_L{0}";
		spriteMap.Add( KeyWord.INDEX_COOK_L, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.INDEX_COOK_L, counter, COOK_WAY_COUNT});

		// 食材子分類圖
		counter = 1;
		path = "index/food/food{0}";
		spriteMap.Add( KeyWord.INDEX_FOOD, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.INDEX_FOOD, counter, USE_FOOD_COUNT});

		counter = 1;
		path = "index/food/food_L{0}";
		spriteMap.Add( KeyWord.INDEX_FOOD_L, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.INDEX_FOOD_L, counter, USE_FOOD_COUNT});

		// 主食子分類圖
		counter = 1;
		path = "index/staple/staple{0}";
		spriteMap.Add( KeyWord.INDEX_STAPLE, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.INDEX_STAPLE, counter, USE_STAPLE_COUNT});

		counter = 1;
		path = "index/staple/staple_L{0}";
		spriteMap.Add( KeyWord.INDEX_STAPLE_L, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.INDEX_STAPLE_L, counter, USE_STAPLE_COUNT});


		// 過濾器 - 料理方式子分類圖
		counter = 1;
		path = "filter/cook/cook{0}";
		spriteMap.Add( KeyWord.FILTER_COOK, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.FILTER_COOK, counter, COOK_WAY_COUNT});

		counter = 1;
		path = "filter/cook/cook_L{0}";
		spriteMap.Add( KeyWord.FILTER_COOK_L, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.FILTER_COOK_L, counter, COOK_WAY_COUNT});

		// 過濾器 - 食材子分類圖
		// counter = 1;
		// path = "filter/food/food{0}";
		// spriteMap.Add( KeyWord.FILTER_FOOD, new Dictionary<int,Sprite>() );
		// downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.FILTER_FOOD, counter, USE_FOOD_COUNT});

		// counter = 1;
		// path = "filter/food/food_L{0}";
		// spriteMap.Add( KeyWord.FILTER_FOOD_L, new Dictionary<int,Sprite>() );
		// downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.FILTER_FOOD_L, counter, USE_FOOD_COUNT});

		// 過濾器 - 主食子分類圖
		// counter = 1;
		// path = "filter/staple/staple{0}";
		// spriteMap.Add( KeyWord.FILTER_STAPLE, new Dictionary<int,Sprite>() );
		// downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.FILTER_STAPLE, counter, USE_STAPLE_COUNT});

		// counter = 1;
		// path = "filter/staple/staple_L{0}";
		// spriteMap.Add( KeyWord.FILTER_STAPLE_L, new Dictionary<int,Sprite>() );
		// downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.FILTER_STAPLE_L, counter, USE_STAPLE_COUNT});

		// 餐點確認 - 餐點圖(右)
		counter = 1;
		path = "want/orderImg/{0}";
		spriteMap.Add( KeyWord.WANT_ORDER, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.WANT_ORDER, counter, MENU_COUNT});

		// 餐點確認 - 主食縮圖(左)
		counter = 1;
		path = "want/staple/{0}";
		spriteMap.Add( KeyWord.WANT_STAPLE, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.WANT_STAPLE, counter, USE_STAPLE_COUNT});

		// 餐點確認 - 確認大圖
		counter = 1;
		path = "check/L{0}";
		spriteMap.Add( KeyWord.ORDER_STAPLE_L, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.ORDER_STAPLE_L, counter, USE_STAPLE_COUNT});

		// 餐點確認 - 主食縮圖
		counter = 1;
		path = "check/{0}";
		spriteMap.Add( KeyWord.ORDER_FLOOR, new Dictionary<int,Sprite>() );
		downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, KeyWord.ORDER_FLOOR, counter, MENU_COUNT});

	}
	// Update is called once per frame
	void Update () {
		
	}

	private void downloadCallBack(Sprite sprite, object userData)
	{
		object[] args = (object[])userData;
		string path = (string) args[0];
		string keyWord = (string) args[1];
		int counter = (int)args [2];
		// int minCounter = (args.Length >= 4) ? (int)args [3]:20;
		int minCounter = (int)args [3];

		if (sprite != null) 
		{
			spriteMap [keyWord].Add (counter, sprite);
		}
		else 
		{
			Debug.logger.LogError ("[Yu-Ning]", string.Format("找不到圖片 : Resources/{0}",string.Format (path, counter) ));
		}

		counter++;
		if (counter <= minCounter) 
		{
			downloadMgr.Instance.downloadSprite (string.Format (path, counter), downloadCallBack, new object[]{path, keyWord, counter, minCounter});
		}


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
		// case FilterType.FOOD:
			// return (isLarge) ? spriteMgr.KeyWord.FILTER_FOOD_L:spriteMgr.KeyWord.FILTER_FOOD;
		// case FilterType.STAPLE:
			// return (isLarge) ? spriteMgr.KeyWord.FILTER_STAPLE_L:spriteMgr.KeyWord.FILTER_STAPLE;
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
}
