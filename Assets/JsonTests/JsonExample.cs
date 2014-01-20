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
		Debug.Log ("GetString");
		GetString();
		Debug.Log ("IsNotOtherThanString");
		IsNotOtherThanString();
	}

	public void Parse ()
	{
		Json json = new Json();
		json.ParseDocument(testInput1.text);
	}

	public void GetString ()
	{
		Json json = new Json();
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
		Json json = new Json();
		json.ParseDocument(testInput1.text);
		
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
