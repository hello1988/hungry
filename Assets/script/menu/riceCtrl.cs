using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riceCtrl : MonoBehaviour 
{
	[SerializeField]
	private GameObject pointer;
	void Awake () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void assistInit()
	{
		pointer.GetComponent<riceDrag> ().setPositionToAnchor (1);
	}

	public void setAppetite( GameObject appetite )
	{
		int riceGrammer = appetite.GetComponent<appetite> ().getRiceGrammer ();
		Debug.Log (string.Format("riceGrammer : {0}",riceGrammer));
	}
}
