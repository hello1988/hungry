using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assist2Ctrl : MonoBehaviour 
{

	private menu curMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void assistInit()
	{
		custom curCustom = DataMgr.Instance.getOrderingCustom ();
		curMenu = curCustom.getMenu (0);
	}

	public void playTextSpeech(string speechKey)
	{
		if (curMenu == null) {return;}

		Dictionary<string, string> speechTextMap = curMenu.getAssistSpeechText ();
		if (!speechTextMap.ContainsKey ( speechKey )) {return;}

		string audioKey = textToSpeech.getAudioKey (curMenu.getMenuID(), speechKey);
		menuMgr.Instance.menuToSpeech (audioKey, speechTextMap[speechKey]);
	}
}
