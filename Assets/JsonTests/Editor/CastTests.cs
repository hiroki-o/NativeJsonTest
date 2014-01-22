using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class CastTests
	{
		[Datapoint]
		string input = @"{""myArrays"":[1,2,333,546,589,-3236,27,844,91,12220]}";

		[Datapoint]
		string input2 = @"{""myArrays"":""actually, this is not an array.""}";

		[Datapoint]
		int[] values = {1,2,333,546,589,-3236,27,844,91,12220};

//		[Test]
//		public void CastToInt ()
//		{
//			Json json = new Json();
//			json.ParseDocument(input);
//
//			int i = 0;
//			JsonValue v = json["myArrays"];
//
//			Assert.That ( v.array.Count == values.Length );
//
//			for(i=0; i< values.Length; ++i) {
//				Assert.That ( values[i] == (Int32)v[i] );
//			}
//		}
//
		[Test]
		public void CastToString ()
		{
			Json json = new Json();

			json.AddMember("member1");
			json["member1"].stringValue = "hello world";

			Assert.That ( "hello world" == (string)json["member1"] );
		}

		[Test]
		public void CastToWringType ()
		{
			Json json = new Json();

			json.AddMember("member1");

			json["member1"].stringValue = "hello world";

			Assert.Catch ( typeof(InvalidCastException), delegate { int v = (int)json["member1"]; } );
		}

		[Test]
		public void CastToJsonValueFromBool ()
		{
			Json json = new Json();
			
			json["member3"] = true;

			Assert.AreEqual(true, 			(bool)json["member3"]);
		}

		[Test]
		public void CastToJsonValueFromPrimitives ()
		{
			JsonValue v = 1;
			JsonValue v2 = 1.23;
			JsonValue v3 = true;
			JsonValue v4 = "hello world";

			Json json = new Json();

			json["member1"] = 123;
			json["member2"] = 456.789;
			json["member3"] = true;
			json["member4"] = false;
			json["member5"] = "hello world again";

			json["member6"] = v;
			json["member7"] = v2;
			json["member8"] = v3;
			json["member9"] = v4;

			Assert.AreEqual(123, 			(int)json["member1"]);
			Assert.AreEqual(456.789, 		(double)json["member2"]);
			Assert.AreEqual(true, 			(bool)json["member3"]);
			Assert.AreEqual(false, 			(bool)json["member4"]);
			Assert.AreEqual("hello world again", 	(string)json["member5"]);

			Assert.AreEqual(1, 				(int)json["member6"]);
			Assert.AreEqual(1.23, 			(double)json["member7"]);
			Assert.AreEqual(true, 			(bool)json["member8"]);
			Assert.AreEqual("hello world", 	(string)json["member9"]);
		}
	}
}
