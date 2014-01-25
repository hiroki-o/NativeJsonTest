using UnityEngine;
using System;
using System.Collections;
using JsonFx.Json;

public class StressTest : MonoBehaviour {

	public TextAsset txtjson;

	float litJsonTime;
	float jsonFxTime;
	float jsonOrgTime;
	float nativeTime;

	float lastTime;

	string errorLitJson;
	string errorJsonFx;
	string errorJsonOrg;
	string errorNative;

	bool onDuty;

	// Use this for initialization
	void Start () {
	}

	void OnGUI() {

		GUILayout.BeginVertical();
		GUILayout.Space(100);

		GUILayout.BeginHorizontal();
		GUILayout.Label("LitJson parse time:");
		if( errorLitJson != null ) {
			GUILayout.Label(errorLitJson);
		} else {
			GUILayout.Label(litJsonTime + " (sec)");
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("JsonFx parse time:");
		if( errorJsonFx != null ) {
			GUILayout.Label(errorJsonFx);
		} else {
			GUILayout.Label(jsonFxTime + " (sec)");
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("JsonOrg parse time:");
		if( errorJsonOrg != null ) {
			GUILayout.Label(errorJsonOrg);
		} else {
			GUILayout.Label(jsonOrgTime + " (sec)");
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("NATIVE parse time:");
		if( errorJsonFx != null ) {
			GUILayout.Label(errorNative);
		} else {
			GUILayout.Label(nativeTime + " (sec)");
		}
		GUILayout.EndHorizontal();
	
		GUILayout.Space(100);

		GUILayout.BeginHorizontal();
		if(!onDuty) {
			if(GUILayout.Button("LitJson")) {
				RunTest(delegate { TestLitJson(out errorLitJson, ref litJsonTime); });
			}
			if(GUILayout.Button("JsonFx")) {
				RunTest(delegate { TestJsonFx(out errorJsonFx, ref jsonFxTime); } );
			}
			if(GUILayout.Button("JsonOrg")) {
				RunTest(delegate { TestJsonOrg(out errorJsonOrg, ref jsonOrgTime); });
			}
			if(GUILayout.Button("NATIVE")) {
				RunTest(delegate { TestNative(out errorNative, ref nativeTime); });
			}
		} else {
			GUILayout.Label("Parsing....");
		}
		GUILayout.EndHorizontal();

		GUILayout.EndVertical ();
	}

	private delegate void RunnerDelegate();

	void RunTest(RunnerDelegate d) {
		onDuty = true;
		StartCoroutine(_Run (d));
	}

	IEnumerator _Run(RunnerDelegate d) {
		d();
		onDuty = false;
		yield return new WaitForEndOfFrame();
	}

	void TestNative(out string error, ref float v) {
		error = null;
		try {
			float tStart = Time.realtimeSinceStartup;
			JsonObject json = JsonObject.Parse(txtjson);
			float tParseEnd = Time.realtimeSinceStartup;
			v = (tParseEnd - tStart);
		} catch(Exception e) {
			error = e.Message;
		}
	}

	void TestJsonOrg(out string error, ref float v) {
		error = null;
		try {
			float tStart = Time.realtimeSinceStartup;
			JsonOrg.JsonObject jo = new JsonOrg.JsonObject(txtjson.text);
			float tParseEnd = Time.realtimeSinceStartup;
			v = (tParseEnd - tStart);
		} catch( Exception e) {
			error = e.Message;
		}
	}

	void TestLitJson(out string error, ref float v) {
		error = null;
		try {
			float tStart = Time.realtimeSinceStartup;
			LitJson.JsonData data = LitJson.JsonMapper.ToObject(txtjson.text);
			float tParseEnd = Time.realtimeSinceStartup;
			v = (tParseEnd - tStart);
		} catch( Exception e) {
			error = e.Message;
		}
	}

	void TestJsonFx(out string error, ref float v) {
		error = null;
		try {
			float tStart = Time.realtimeSinceStartup;
			JsonReaderSettings readerSettings = new JsonReaderSettings();
			readerSettings.AllowNullValueTypes = true;
			readerSettings.AllowUnquotedObjectKeys = true;
			JsonReader jsonReader = new JsonReader(txtjson.text, readerSettings);
			object obj = jsonReader.Deserialize();
			float tParseEnd = Time.realtimeSinceStartup;
			v = (tParseEnd - tStart);
		} catch( Exception e) {
			error = e.Message;
		}
	}
}
