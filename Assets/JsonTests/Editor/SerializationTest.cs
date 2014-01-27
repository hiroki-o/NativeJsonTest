using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class SerializationTests
	{
		public class TestClass {
			
			public enum Mode {
				Save,
				Go,
				Not,
				Dowit
			}

			[JsonIgnore]
			private int myProp;
			
			public int value1;
			public bool value2;
			public long value3;
			public ulong value4;
			public string value5;
			public uint value6;
			public byte value7;
			public float value8;
			public double value9;
			public Mode enumMode1;
			public Mode enumMode2;
			
			public TestClass object1;
			public TestClass object2;

			public int[] array1;
			
			public int MyProp {
				get {
					return myProp;
				}
				set {
					myProp = value;
				}
			}
			
			public TestClass() {}
		}
		
		public class TestClass2 {
			
			public enum Mode {
				Save,
				Go,
				Not,
				Dowit
			}
			
			private int myProp;
			
			public int value1;
			public bool value2;
			private long value3;
			public ulong value4;
			
			[JsonName("value5")]
			public string valueString;
			
			public uint value6;
			public byte value7;
			public float value8;
			public double value9;
			public Mode enumMode1;
			public Mode enumMode2;
			
			public TestClass2 object1;
			public TestClass object2;
			
			public long GetValue3() { return value3; }
			
			[JsonIgnore]
			public int[] array1;
			
			public int MyProp {
				get {
					return myProp;
				}
				set {
					myProp = value;
				}
			}
		}


		[Datapoint]
		string input = @"
{
""MyProp"":54321,
""value1"":12345,
""value2"":true,
""value3"":-1234567812345678,
""value4"":1234567812345678,
""value5"":""hello, deserializer!"",
""value6"":314748364,
""value7"":32,
""value8"":3.1415,
""value9"":11113.141592653,

""enumMode1"":""Dowit"",
""enumMode2"":""Go"",
""object1"": {
	""MyProp"":987654,
	""value1"":67890,
	""value2"":false,
	""value3"":-8765432123456,
	""value4"":8765432123456,
	""value5"":""this is object within object, yay!""
	},
""object2"": null,
""array1"":[10, 20, 30, 44, 555, 666, 7777, -99999]
}";

		TestClass CreateTestInstance() {
			TestClass t = new TestClass();
			
			t.MyProp = 54321;
			t.value1 = 12345;
			t.value2 = true;
			t.value3 = -1234567812345678;
			t.value4 = 1234567812345678;
			t.value5 = "hello, deserializer!";
			t.value6 = 314748364;
			t.value7 = 32;
			t.value8 = 3.1415f;
			t.value9 = 11113.141592653;
			
			t.enumMode1 = TestClass.Mode.Dowit;
			t.enumMode2 = TestClass.Mode.Go;
			
			TestClass obj = new TestClass();
			
			obj.MyProp = 987654;
			obj.value1 = 67890;
			obj.value2 = false;
			obj.value3 = -8765432123456;
			obj.value4 = 8765432123456;
			obj.value5 = "this is object within object, yay!";

			t.object1 = obj;
			t.object2 = null;
			
			t.array1 = new int[] {10, 20, 30, 44, 555, 666, 7777, -99999};

			return t;
		}

		[Test]
		public void TestSerialize ()
		{
			TestClass t = CreateTestInstance();

			JsonValue json = JsonObject.SerializeToJsonObject(t);

			Assert.That (json != null);

			Assert.AreEqual (t.MyProp, json["MyProp"].intValue, 	"MyProp");
			Assert.AreEqual (t.value1, json["value1"].intValue, 	"value1");
			Assert.AreEqual (t.value2, json["value2"].boolValue, 	"value2");
			Assert.AreEqual (t.value3, json["value3"].longValue, 	"value3");
			Assert.AreEqual (t.value4, json["value4"].ulongValue, 	"value4");
			Assert.AreEqual (t.value5, json["value5"].stringValue, 	"value5");
			Assert.AreEqual (t.value6, json["value6"].uintValue, 	"value6");
			Assert.AreEqual (t.value7, json["value7"].uintValue, 	"value7");
			Assert.AreEqual (t.value8, json["value8"].doubleValue, 	"value8");
			Assert.AreEqual (t.value9, json["value9"].doubleValue, 	"value9");

			Assert.AreEqual (t.enumMode1.ToString(), json["enumMode1"].stringValue, 	"enumMode1");
			Assert.AreEqual (t.enumMode2.ToString(), json["enumMode2"].stringValue, 	"enumMode2");


			Assert.AreEqual (t.object1.MyProp, json["object1"]["MyProp"].intValue, 		"object1.MyProp");
			Assert.AreEqual (t.object1.value1, json["object1"]["value1"].intValue, 		"object1.value1");
			Assert.AreEqual (t.object1.value2, json["object1"]["value2"].boolValue, 	"object1.value2");
			Assert.AreEqual (t.object1.value3, json["object1"]["value3"].longValue, 	"object1.value3");
			Assert.AreEqual (t.object1.value4, json["object1"]["value4"].ulongValue, 	"object1.value4");
			Assert.AreEqual (t.object1.value5, json["object1"]["value5"].stringValue, 	"object1.value5");
			Assert.AreEqual (t.object1.value6, json["object1"]["value6"].uintValue, 	"object1.value6");
			Assert.AreEqual (t.object1.value7, json["object1"]["value7"].uintValue, 	"object1.value7");
			Assert.AreEqual (t.object1.value8, json["object1"]["value8"].doubleValue, 	"object1.value8");
			Assert.AreEqual (t.object1.value9, json["object1"]["value9"].doubleValue, 	"object1.value9");
			Assert.AreEqual (t.object1.enumMode1.ToString(), json["object1"]["enumMode1"].stringValue, 	"object1.enumMode1");
			Assert.AreEqual (t.object1.enumMode2.ToString(), json["object1"]["enumMode2"].stringValue, 	"object1.enumMode2");

			Assert.That ( (t.object2 == null) && (json["object2"].isNull), "object2");

			JsonValueArray a = json["array1"].array;

			Assert.AreEqual (a.Count, t.array1.Length, "array1.Count");

			for(int i = 0; i < t.array1.Length; ++i) {
				Assert.AreEqual( t.array1[i], a[i].intValue, "array1["+i+"]");
			}
		}

		[Test]
		public void TestSerializeEnumToInt ()
		{
			TestClass t = CreateTestInstance();
			
			JsonValue json = JsonObject.SerializeToJsonObject(t, false);
			
			Assert.That (json != null);
			
			Assert.AreEqual (t.MyProp, json["MyProp"].intValue, 	"MyProp");
			Assert.AreEqual (t.value1, json["value1"].intValue, 	"value1");
			Assert.AreEqual (t.value2, json["value2"].boolValue, 	"value2");
			Assert.AreEqual (t.value3, json["value3"].longValue, 	"value3");
			Assert.AreEqual (t.value4, json["value4"].ulongValue, 	"value4");
			Assert.AreEqual (t.value5, json["value5"].stringValue, 	"value5");
			Assert.AreEqual (t.value6, json["value6"].uintValue, 	"value6");
			Assert.AreEqual (t.value7, json["value7"].uintValue, 	"value7");
			Assert.AreEqual (t.value8, json["value8"].doubleValue, 	"value8");
			Assert.AreEqual (t.value9, json["value9"].doubleValue, 	"value9");
			
			Assert.AreEqual ((int)t.enumMode1, json["enumMode1"].intValue, 	"enumMode1");
			Assert.AreEqual ((int)t.enumMode2, json["enumMode2"].intValue, 	"enumMode2");
			
			
			Assert.AreEqual (t.object1.MyProp, json["object1"]["MyProp"].intValue, 		"object1.MyProp");
			Assert.AreEqual (t.object1.value1, json["object1"]["value1"].intValue, 		"object1.value1");
			Assert.AreEqual (t.object1.value2, json["object1"]["value2"].boolValue, 	"object1.value2");
			Assert.AreEqual (t.object1.value3, json["object1"]["value3"].longValue, 	"object1.value3");
			Assert.AreEqual (t.object1.value4, json["object1"]["value4"].ulongValue, 	"object1.value4");
			Assert.AreEqual (t.object1.value5, json["object1"]["value5"].stringValue, 	"object1.value5");
			Assert.AreEqual (t.object1.value6, json["object1"]["value6"].uintValue, 	"object1.value6");
			Assert.AreEqual (t.object1.value7, json["object1"]["value7"].uintValue, 	"object1.value7");
			Assert.AreEqual (t.object1.value8, json["object1"]["value8"].doubleValue, 	"object1.value8");
			Assert.AreEqual (t.object1.value9, json["object1"]["value9"].doubleValue, 	"object1.value9");
			Assert.AreEqual ((int)t.object1.enumMode1, json["object1"]["enumMode1"].intValue, 	"object1.enumMode1");
			Assert.AreEqual ((int)t.object1.enumMode2, json["object1"]["enumMode2"].intValue, 	"object1.enumMode2");
			
			Assert.That ( (t.object2 == null) && (json["object2"].isNull), "object2");
			
			JsonValueArray a = json["array1"].array;
			
			Assert.AreEqual (a.Count, t.array1.Length, "array1.Count");
			
			for(int i = 0; i < t.array1.Length; ++i) {
				Assert.AreEqual( t.array1[i], a[i].intValue, "array1["+i+"]");
			}
		}


		[Test]
		public void TestSerializeRoundTrip ()
		{
			TestClass t  = CreateTestInstance();
			TestClass t2 = JsonObject.Deserialize<TestClass>(input);
			JsonValue json = JsonObject.SerializeToJsonObject(t2);

			Assert.AreEqual (t.MyProp, json["MyProp"].intValue, 	"MyProp");
			Assert.AreEqual (t.value1, json["value1"].intValue, 	"value1");
			Assert.AreEqual (t.value2, json["value2"].boolValue, 	"value2");
			Assert.AreEqual (t.value3, json["value3"].longValue, 	"value3");
			Assert.AreEqual (t.value4, json["value4"].ulongValue, 	"value4");
			Assert.AreEqual (t.value5, json["value5"].stringValue, 	"value5");
			Assert.AreEqual (t.value6, json["value6"].uintValue, 	"value6");
			Assert.AreEqual (t.value7, json["value7"].uintValue, 	"value7");
			Assert.AreEqual (t.value8, json["value8"].doubleValue, 	"value8");
			Assert.AreEqual (t.value9, json["value9"].doubleValue, 	"value9");
			
			Assert.AreEqual (t.enumMode1.ToString(), json["enumMode1"].stringValue, 	"enumMode1");
			Assert.AreEqual (t.enumMode2.ToString(), json["enumMode2"].stringValue, 	"enumMode2");

			Assert.AreEqual (t.object1.MyProp, json["object1"]["MyProp"].intValue, 		"object1.MyProp");
			Assert.AreEqual (t.object1.value1, json["object1"]["value1"].intValue, 		"object1.value1");
			Assert.AreEqual (t.object1.value2, json["object1"]["value2"].boolValue, 	"object1.value2");
			Assert.AreEqual (t.object1.value3, json["object1"]["value3"].longValue, 	"object1.value3");
			Assert.AreEqual (t.object1.value4, json["object1"]["value4"].ulongValue, 	"object1.value4");
			Assert.AreEqual (t.object1.value5, json["object1"]["value5"].stringValue, 	"object1.value5");
			Assert.AreEqual (t.object1.value6, json["object1"]["value6"].uintValue, 	"object1.value6");
			Assert.AreEqual (t.object1.value7, json["object1"]["value7"].uintValue, 	"object1.value7");
			Assert.AreEqual (t.object1.value8, json["object1"]["value8"].doubleValue, 	"object1.value8");
			Assert.AreEqual (t.object1.value9, json["object1"]["value9"].doubleValue, 	"object1.value9");
			Assert.AreEqual (t.object1.enumMode1.ToString(), json["object1"]["enumMode1"].stringValue, 	"object1.enumMode1");
			Assert.AreEqual (t.object1.enumMode2.ToString(), json["object1"]["enumMode2"].stringValue, 	"object1.enumMode2");

			Assert.That ( (t.object2 == null) && (json["object2"].isNull), "object2");

			Assert.AreEqual (null, t.object2, "object2");
			
			JsonValueArray a = json["array1"].array;
			
			Assert.AreEqual (a.Count, t.array1.Length, "array1.Count");
			
			int i = 0;
			foreach(JsonValue v in json["array1"]) {
				Assert.AreEqual (t.array1[i++], v.intValue, "array1["+i+"]");
			}

			TestClass t3 = json.Deserialize<TestClass>();
			JsonValue j2 = JsonObject.SerializeToJsonObject(t3);

			Assert.AreEqual (json.ToPrettyString(), j2.ToPrettyString(), "ToPrettyString");
		}	

		[Test]
		[ExpectedException(typeof(JsonSerializationException))]
		public void TestSerializeCircularReference ()
		{
			TestClass t  = CreateTestInstance();

			// make circular reference
			t.object2 = t;

			// Serializing object with circular reference should throw Exception
			JsonValue json = JsonObject.SerializeToJsonObject(t);
		}
	}
}
