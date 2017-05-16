using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;


public class textToSpeech : MonoBehaviour 
{
	[SerializeField]
	private int speed = -4;
	[SerializeField]
	private AudioClip[] audioList;

	private static readonly string RSS_APIKey = "d0dd2c9bf5d94993bb96111e01e649ee";
	private static readonly string RSS_URL = "https://api.voicerss.org/?key={0}&hl={1}&src={2}&r={3}&c=WAV";
	// private static readonly string GOOGLE_URL = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q={0}&tl={1}";

	Dictionary<string, AudioClip> clipMap;

	public static string getAudioKey( int menuID, string speechKey )
	{
		return string.Format ("menuSpeech_{0}_{1}",menuID, speechKey);
	}

	void Awake()
	{
		string[] speechKeyList = new string[]{"1", "5", "6", "8"};
		clipMap = new Dictionary<string, AudioClip>();

		int clipCounter = 0;
		int keyLength = speechKeyList.Length;
		for( int menuID = 1; menuID <= 21;menuID++ )
		{
			for( int index = 0;index < keyLength;index++ )
			{
				string audioKey = getAudioKey(menuID, speechKeyList[index]);
				clipMap.Add (audioKey, audioList[clipCounter++]);
			}
		}
	}

	public void onMenuReady(List<menu> menuList)
	{
		foreach(menu m in menuList)
		{
			Debug.Log (string.Format("{0} : {1}",m.getMenuID(), m.getMenuName()));
			Dictionary<string, string> speechTextMap = m.getAssistSpeechText ();
			foreach (string key in speechTextMap.Keys) 
			{
				// string audioKey = string.Format ("menuSpeech_{0}_{1}",m.getMenuID(), key);
				string audioKey = getAudioKey(m.getMenuID(), key);
				StartCoroutine (downloadAudio(audioKey, speechTextMap[key]));
			}

			StartCoroutine (delay());
		}
	}

	private IEnumerator downloadAudio( string audioKey, string text )
	{
		AudioClip clip = null;
		// Debug.Log (string.Format("{0} load from www",audioKey));
		// Remove the "spaces" in excess
		Regex rgx = new Regex ("\\s+");
		string result = rgx.Replace (text, "%20");

		// string url = string.Format (GOOGLE_URL, result, language);
		string url = string.Format (RSS_URL, RSS_APIKey, "zh-tw", result,speed);

		WWW www = new WWW (url);
		yield return www;

		// https://docs.unity3d.com/355/Documentation/ScriptReference/WWW.GetAudioClip.html
		Debug.logger.Log(string.Format("load audio complete : {0}",audioKey));
		clip = www.GetAudioClip (false, false, AudioType.WAV);
		SavWav.Save(audioKey, clip);

		www.Dispose ();
		yield return new WaitForSeconds (3);
	} 	

	private IEnumerator delay()
	{
		yield return new WaitForSeconds (5);
	}

	// google 檔案類型mpeg
	// RSS  檔案類型ogg
	public void speech(string audioKey)
	{
		if (!clipMap.ContainsKey (audioKey)) {return;}

		AudioClip clip = clipMap [audioKey];
		
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = clip;
		audio.Play ();

	}

}
