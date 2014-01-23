using UnityEngine;
using System;
using System.Threading;

public class JsonExample : MonoBehaviour
{
	internal class Assert {
		public static void That(bool b) {
			Debug.Log ("Result:"+b);
		}
	}

	[SerializeField]
	public TextAsset testInput1;
	
	public string value1 = "Hello Json.";
	public string valueKey1 = "The quick brown fox jumps over the lazy dog";
	public int valueKey2 = 456789;
	public double value2 = 123.45;
	public string[] value3 = {"A", "B", "C"};

	void Start() {
		Debug.Log ("Parse");
		Parse();
		Debug.Log ("ParseAndToString");
		ParseAndToString();
		Debug.Log ("ParseModifyAndToString");
		ParseModifyAndToString();
		Debug.Log ("GetString");
		GetString();
		Debug.Log ("IsNotOtherThanString");
		IsNotOtherThanString();
		Debug.Log ("BuildFromCode");
		BuildFromCode();
	}

	public void Parse ()
	{
		JsonObject json = new JsonObject();
		json.ParseDocument(testInput1);
	}

	public void ParseAndToString ()
	{
		JsonObject json = new JsonObject();
		json.ParseDocument(testInput1);

		Debug.Log (json.ToPrettyString ());
	}

	public void ParseModifyAndToString ()
	{
		JsonObject json = new JsonObject();
		json.ParseDocument(testInput1);

		json.AddMember("category", "INIT");
		json.AddMember("data_value","foobar");

		JsonValue v = json["name1"];

		json["name1"].SetString ("adsjlfajsdlfajsdflajs dsjfjdfajd.");
		json["name2"].SetDouble(345.678);

		Debug.Log (json.ToPrettyString ());
	}

	public void BuildFromCode ()
	{
		JsonObject json = new JsonObject();

		json.AddMember("member1", "INIT");
		json.AddMember("member2","foobar");
		json.AddMember("member3");

		JsonValue jv = json.GetValue ("member3");
		jv.SetArray();
		jv.array.PushBack(1);
		jv.array.PushBack(2);
		jv.array.PushBack(3);
		jv.array.PushBack(4);

		JsonObject jsonObj = new JsonObject();
		jsonObj.AddMember("intv", 123);
		jsonObj.AddMember("srtv", "hoge");
		jsonObj.AddMember("doublev", 123.456);

		json.AddMember("member4", jsonObj);

		Debug.Log (json.ToPrettyString ());
	}


	public void GetString ()
	{
		JsonObject json = new JsonObject();
		json.ParseDocument(testInput1.text);

		// test direct query
		Assert.That ( json.IsString ("name1") );
		Assert.That ( json.GetString ("name1").Equals(value1) );

		JsonValue jv = json.GetValue("name1");
		Assert.That ( jv.isString );
		Assert.That ( jv.stringValue.Equals(value1) );
	}

	public void IsNotOtherThanString ()
	{
		JsonObject json = new JsonObject();
		json.ParseDocument(testInput1);
		
		// test direct query
		Assert.That ( !json.IsArray ("name1") );
		Assert.That ( !json.IsBool ("name1") );
		Assert.That ( !json.IsDouble ("name1") );
		Assert.That ( !json.IsInt ("name1") );
		Assert.That ( !json.IsNull ("name1") );
		Assert.That ( !json.IsNumber ("name1") );
		Assert.That ( !json.IsObject ("name1") );

		JsonValue jv = json.GetValue("name1");
		Assert.That ( !jv.isArray );
		Assert.That ( !jv.isBool );
		Assert.That ( !jv.isDouble );
		Assert.That ( !jv.isInt );
		Assert.That ( !jv.isNull );
		Assert.That ( !jv.isNumber );
		Assert.That ( !jv.isObject );
	}
}
