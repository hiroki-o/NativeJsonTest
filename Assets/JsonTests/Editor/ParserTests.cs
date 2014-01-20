using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;

namespace NativeJsonTest
{
	[TestFixture]
	public class ParserTests : MonoBehaviour
	{
//		[Datapoint]
//		public double zero = 0;
		[SerializeField]
		public TextAsset testInput1;

		[Datapoint]
		public string value1 = "Hello Json.";
		[Datapoint]
		public string valueKey1 = "The quick brown fox jumps over the lazy dog";
		[Datapoint]
		public int valueKey2 = 456789;
		[Datapoint]
		public double value2 = 123.45;
		[Datapoint]
		public string[] value3 = {"A", "B", "C"};

		[Test]
//		[Ignore ("Ignored test")]
//		[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
		public void Parse ()
		{
			Json json = new Json();
			json.ParseDocument(testInput1.text);
		}

		[Test]
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

		[Test]
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
}
