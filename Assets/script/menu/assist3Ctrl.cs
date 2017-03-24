using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class assist3Ctrl : MonoBehaviour 
{
	[SerializeField]
	private Sprite[] spriteList; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void assistInit()
	{
		
	}

	public void setSpriteList( Sprite[] tipSpriteList )
	{
		spriteList = tipSpriteList;
	}

	public Sprite getTipSprite( int index )
	{
		if ((index < 0) || (index >= spriteList.Length)) {return null;}

		return spriteList [index];
	}

}
