using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assist2Ctrl : MonoBehaviour {

	private Dictionary<string, string> speechTextMap;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void assistInit()
	{
		custom curCustom = DataMgr.Instance.getOrderingCustom ();
		menu m = curCustom.getMenu (0);
		speechTextMap = m.getAssistSpeechText ();
	}

	public void playTextSpeech(string speechKey)
	{
		if (!speechTextMap.ContainsKey ( speechKey )) {return;}

		menuMgr.Instance.menuToSpeech (speechKey, speechTextMap[speechKey]);
	}
}
