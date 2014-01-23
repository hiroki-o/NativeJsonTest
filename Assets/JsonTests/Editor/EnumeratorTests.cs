using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class EnumeratorTests
	{
		[Datapoint]
		string input = @"{""myArrays"":[1,2,333,546,589,-3236,27,844,91,12220]}";

		[Datapoint]
		int[] values = {1,2,333,546,589,-3236,27,844,91,12220};

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
		[ExpectedException(typeof(InvalidOperationException))]
		public void ArrayModifyDuringEnumeration ()
		{
			JsonObject json = new JsonObject();
			json.ParseDocument(input);
			
			int i = 0;
			JsonValue v = json["myArrays"];
			
			i = 0;
			foreach(JsonValue item in v) {
				Assert.That ( item.intValue == values[i++] );
				if(i == 5) {
					// modification during enumeration should
					// raise InvalidOperationException
					v.array.PushBack(99999);
				}
			}
		}

	}
}
