using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	public class TestClass {

		public enum Mode {
			Save,
			Go,
			Not,
			Dowit
		}

		private int myProp;

		public int value1;
		public bool value2;
		public long value3;
		public ulong value4;
		public string value5;
		public uint value6;
		public byte value7;
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

	[TestFixture]
	public class DeserializationTests
	{
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

""enumMode1"":""Dowit"",
""enumMode2"":1,
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

		[Test]
		public void TestDeserialize ()
		{

			JsonObject json = new JsonObject();
			json.ParseDocument(input);

			TestClass t = json.Deserialize<TestClass>();

			Assert.AreEqual (54321,    t.MyProp, "MyProp");

			Assert.AreEqual (12345,    				t.value1, "value1");
			Assert.AreEqual (true,     				t.value2, "value2");
			Assert.AreEqual (-1234567812345678L,   	t.value3, "value3");
			Assert.AreEqual (1234567812345678UL,  	t.value4, "value4");
			Assert.AreEqual ("hello, deserializer!",t.value5, "value5");
			Assert.AreEqual (314748364,   			t.value6, "value6");
			Assert.AreEqual (32,    				t.value7, "value7");

			Assert.AreEqual (TestClass.Mode.Dowit,       t.enumMode1, "enumMode1");
			Assert.AreEqual (TestClass.Mode.Go,          t.enumMode2, "enumMode2");

			Assert.AreEqual (987654,    					t.object1.MyProp, "object1.MyProp");
			Assert.AreEqual (67890,     					t.object1.value1, "object1.value1");
			Assert.AreEqual (false,     					t.object1.value2, "object1.value2");
			Assert.AreEqual (-8765432123456,     			t.object1.value3, "object1.value3");
			Assert.AreEqual (8765432123456,     			t.object1.value4, "object1.value4");
			Assert.AreEqual ("this is object within object, yay!",  t.object1.value5, "object1.value5");

			Assert.AreEqual (null, t.object2, "object2");

			JsonValueArray a = json["array1"].array;

			Assert.AreEqual (a.Count, t.array1.Length, "array1.Count");

			int i = 0;
			foreach(JsonValue v in json["array1"]) {
				Assert.AreEqual (v.intValue, t.array1[i++], "array1["+i+"]");
			}
		}

		[Test]
		public void TestDeserializeWorkWithAttribute ()
		{
			
			TestClass2 t = JsonObject.Deserialize<TestClass2>(input);
			
			Assert.AreEqual (54321,    t.MyProp, "MyProp");
			
			Assert.AreEqual (12345,    				t.value1, "value1");
			Assert.AreEqual (true,     				t.value2, "value2");
			Assert.AreEqual (-1234567812345678L,   	t.GetValue3(), "value3");
			Assert.AreEqual (1234567812345678UL,  	t.value4, "value4");
			Assert.AreEqual ("hello, deserializer!",t.valueString, "value5");
			Assert.AreEqual (314748364,   			t.value6, "value6");
			Assert.AreEqual (32,    				t.value7, "value7");
			
			Assert.AreEqual (TestClass2.Mode.Dowit,       t.enumMode1, "enumMode1");
			Assert.AreEqual (TestClass2.Mode.Go,          t.enumMode2, "enumMode2");
			
			Assert.AreEqual (987654,    					t.object1.MyProp, "object1.MyProp");
			Assert.AreEqual (67890,     					t.object1.value1, "object1.value1");
			Assert.AreEqual (false,     					t.object1.value2, "object1.value2");
			Assert.AreEqual (-8765432123456,     			t.object1.GetValue3(), "object1.value3");
			Assert.AreEqual (8765432123456,     			t.object1.value4, "object1.value4");
			Assert.AreEqual ("this is object within object, yay!",  t.object1.valueString, "object1.value5");
			
			Assert.AreEqual (null, t.object2, "object2");

			// should be ignored
			Assert.AreEqual (null, t.array1, "array1.Count");
		}	
	}
}
