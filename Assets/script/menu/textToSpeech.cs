using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using HoloToolkit.Unity;

public class textToSpeech : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// StartCoroutine (translate ("我是泰儒"));
		ttsTest();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator translate(string text)
	{
		// Remove the "spaces" in excess
		Regex rgx = new Regex ("\\s+");
		// Replace the "spaces" with "% 20" for the link Can be interpreted
		string result = rgx.Replace (text, "%20");
		string url = "http://translate.google.com/translate_tts?tl=en&q=" + result;
		WWW www = new WWW (url);
		yield return www;
		AudioSource audio = this.GetComponent<AudioSource> ();
		audio.clip = www.GetAudioClip (false, false, AudioType.MPEG);
		audio.Play ();
	}

	private void ttsTest()
	{
		TextToSpeechManager tts = new TextToSpeechManager ();
		tts.SpeakText("我是泰儒");
	}
}
