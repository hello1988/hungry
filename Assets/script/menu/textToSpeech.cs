using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;


public class textToSpeech : MonoBehaviour 
{
	[SerializeField]
	private int speed = -4;

	private static readonly string RRS_APIKey = "d0dd2c9bf5d94993bb96111e01e649ee";
	private static readonly string RRS_URL = "http://api.voicerss.org/?key={0}&hl={1}&src={2}&r={3}&c=WAV";
	// private static readonly string GOOGLE_URL = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q={0}&tl={1}";

	Dictionary<string, AudioClip> clipMap = new Dictionary<string, AudioClip>();

	// Use this for initialization
	void Start () {
		// StartCoroutine (speech (@"Hi I'm Bliss Chen.", @"en-gb"));
		// StartCoroutine (speech (@"我是又寧 哈哈哈", @"zh-tw"));
		// StartCoroutine (playAudioClipLocal());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// google 檔案類型mpeg
	// RSS  檔案類型ogg
	public IEnumerator speech(string audioKey, string text, string language)
	{
		AudioClip clip = null;
		if (!clipMap.ContainsKey (audioKey)) 
		{
			// Debug.Log (string.Format("{0} load from www",audioKey));
			// Remove the "spaces" in excess
			Regex rgx = new Regex ("\\s+");
			string result = rgx.Replace (text, "%20");

			// string url = string.Format (GOOGLE_URL, result, language);
			string url = string.Format (RRS_URL, RRS_APIKey, language, result,speed);

			WWW www = new WWW (url);
			yield return www;

			// https://docs.unity3d.com/355/Documentation/ScriptReference/WWW.GetAudioClip.html
			clip = www.GetAudioClip (false, false, AudioType.WAV);
			if (!clipMap.ContainsKey (audioKey)) 
			{
				clipMap.Add (audioKey, clip);
			}
		} 
		else 
		{
			// Debug.Log (string.Format("{0} load from local",audioKey));
			clip = clipMap [audioKey];
		}

		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = clip;
		audio.Play ();

	}
	/*
	IEnumerator playAudioClipLocal()
	{
		var filepath = Path.Combine(Application.streamingAssetsPath, "test");
		string url = string.Format ("file://{0}",filepath);

		WWW www = new WWW (url);
		Debug.Log("before yield");
		yield return www;
		Debug.Log("after yield");

		AudioSource audio = GetComponent<AudioSource> ();
		Debug.Log("before get clip");
		audio.clip = www.GetAudioClip (false, false, AudioType.WAV);;
		Debug.Log("after get clip");
		audio.Play ();
	}
	*/
}
