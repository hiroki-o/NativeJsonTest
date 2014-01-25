using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class ArrayTests
	{
		[Datapoint]
		string input = @"{""myArrays"":[1,2,333,546,589,-3236,27,844,91,12220]}";

		[Datapoint]
		string input2 = @"{""myArrays"":""actually, this is not an array.""}";

		[Datapoint]
		int[] values = {1,2,333,546,589,-3236,27,844,91,12220};

		[Datapoint]
		int[] values2 = {1,2,333,546,33333,-3236,27,844,91,12220};

		[Test]
		public void ArrayIndexAccess ()
		{
			JsonObject json = new JsonObject();
			json.ParseDocument(input);

			int i = 0;
			JsonValue v = json["myArrays"];

			Assert.That ( v.array.Count == values.Length );

			for(i=0; i< values.Length; ++i) {
				Assert.That ( v[i].intValue == values[i] );
			}

			i = 0;
			foreach(JsonValue item in v) {
				Assert.That ( item.intValue == values[i++] );
			}
		}

		[Test]
		public void ArrayEnumeration ()
		{
			JsonObject json = new JsonObject();
			json.ParseDocument(input);
			
			int i = 0;
			JsonValue v = json["myArrays"];
			
			Assert.That ( v.array.Count == values.Length );
			
			i = 0;
			foreach(JsonValue item in v) {
				Assert.That ( item.intValue == values[i++] );
			}

			i = 0;
			foreach(JsonValue item in v.array) {
				Assert.That ( item.intValue == values[i++] );
			}
		}

		[Test]
		public void ArrayAccessToNotArray ()
		{
			JsonObject json = new JsonObject();
			json.ParseDocument(input2);
			
			int i = 0;
			JsonValue v = json["myArrays"];
			
			Assert.Catch (typeof(JsonValueException), delegate { i = v[1].intValue; } );
			Assert.Catch (typeof(JsonValueException), delegate { i = v.array[1].intValue; } );

		}

		[Test]
		public void ArrayIndexWrite ()
		{
			JsonObject json = new JsonObject();
			json.ParseDocument(input);
			
			JsonValue v = json["myArrays"];

			v[4] = 33333;
			
			for(int i=0; i< values2.Length; ++i) {
				Assert.That ( v[i].intValue == values2[i] );
			}
		}
	}
}
