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

	}
}
