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
	private AudioClip testAudio;

	private static readonly string RSS_APIKey = "d0dd2c9bf5d94993bb96111e01e649ee";
	private static readonly string RSS_URL = "https://api.voicerss.org/?key={0}&hl={1}&src={2}&r={3}&c=WAV";
	// private static readonly string GOOGLE_URL = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q={0}&tl={1}";

	Dictionary<string, AudioClip> clipMap = new Dictionary<string, AudioClip>();

	// Use this for initialization
	void Start () {
		StartCoroutine (playAudioClipLocal("menuSpeech_1_1"));
		// playAudioClipLocal2("menuSpeech_1_1");
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
			string url = string.Format (RSS_URL, RSS_APIKey, language, result,speed);

			WWW www = new WWW (url);
			yield return www;

			// https://docs.unity3d.com/355/Documentation/ScriptReference/WWW.GetAudioClip.html
			Debug.logger.Log("load audio complete");
			clip = www.GetAudioClip (false, false, AudioType.WAV);
			if (!clipMap.ContainsKey (audioKey)) 
			{
				clipMap.Add (audioKey, clip);
				SavWav.Save(audioKey, clip);
			}

			www.Dispose ();
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

	IEnumerator playAudioClipLocal(string audioKey)
	{
		string url = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			// url = "file:/" + System.IO.Path.Combine (Application.streamingAssetsPath, audioKey + ".wav");
			url = "file:/" + System.IO.Path.Combine (Application.dataPath, string.Format("/Resources/audio/{0}.wav",audioKey));
		}
		else 
		{
			// url = "file://" + Application.dataPath + "/StreamingAssets/" + audioKey + ".wav";
			url = "file://" + Application.dataPath + string.Format("/Resources/audio/{0}.wav",audioKey);
		}

		WWW www = new WWW (url);
		Debug.Log(string.Format(">>>>>> before yield : {0}",url));
		yield return www;
		Debug.Log(">>>>>> after yield");

		AudioSource audio = GetComponent<AudioSource> ();
		AudioClip clip = www.GetAudioClip (false, false, AudioType.WAV);
		Debug.Log(string.Format(">>>>>> before get clip : {0}",(clip==null)));
		audio.clip = clip;
		Debug.Log(">>>>>> after get clip");
		audio.Play ();
	}

	private void playAudioClipLocal2(string audioKey)
	{
		string path = string.Format ("{0}",audioKey);
		// Debug.logger.Log (Application.dataPath);	// /Users/blisschen/Downloads/hungry/Assets
		AudioClip clip = Resources.Load<AudioClip>(path);

		Debug.Log(string.Format(">>>>>> get clip : {0}",(clip==null)));
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = clip;
		Debug.Log(">>>>>> play clip");
		audio.Play ();
	}
}
