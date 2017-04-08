using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class textToSpeech : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// StartCoroutine (translate (@"Hi I'm Bliss Chen.", @"en-gb"));
		StartCoroutine (translate (@"我是又寧 哈哈哈", @"zh-tw"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// google 檔案類型mpeg
	// RRS  檔案類型ogg
	IEnumerator translate(string text, string language)
	{
		string RRS_APIKey = "d0dd2c9bf5d94993bb96111e01e649ee";
		string RRS_URL = "http://api.voicerss.org/?key={0}&hl={1}&src={2}&r=-4&c=OGG";

		string GOOGLE_URL = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q={0}&tl={1}";
		// Remove the "spaces" in excess
		Regex rgx = new Regex ("\\s+");

		string result = rgx.Replace (text, "%20");

		// string url = string.Format (GOOGLE_URL, result, language);
		string url = string.Format (RRS_URL, RRS_APIKey, language, result);

		WWW www = new WWW (url);
		yield return www;
		AudioSource audio = this.GetComponent<AudioSource> ();
		// https://docs.unity3d.com/355/Documentation/ScriptReference/WWW.GetAudioClip.html
		audio.clip = www.GetAudioClip (false, false, AudioType.OGGVORBIS);
		audio.Play ();
	}

}
