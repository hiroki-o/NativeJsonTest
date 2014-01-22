using UnityEngine;
using System.Collections;

public class WWWTests : MonoBehaviour {

	public string serverAddr = "http://localhost:8888";
	public bool getIpAddrTestSuccess;

	IEnumerator GetIPAddr ()
	{
		string url = serverAddr + "/jsontest/ip.json";
		
		WWW www = new WWW(url);
		yield return www;

		Json json = new Json();
		json.ParseDocument(www.text);

		getIpAddrTestSuccess = json["ip"].isString;
	}
	
	// Use this for initialization
	void Start () {
		StartCoroutine(GetIPAddr ());
	}
}
