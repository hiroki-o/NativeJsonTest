using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class ImplicitValueTests
	{
		[Test]
		public void InplicitStringValue ()
		{
			string expectedValue = "hello world";
			JsonValue v = expectedValue;
			
			Assert.AreEqual(expectedValue, v.stringValue);
			Assert.AreEqual(expectedValue, (string)v);

			Assert.That( v.isString );
			Assert.That( !v.isBool );
			Assert.That( !v.isNull );
			Assert.That( !v.isInt );
			Assert.That( !v.isDouble );
			Assert.That( !v.isObject );
			Assert.That( !v.isArray );

			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.boolValue; } );
//			Assert.Catch(typeof(JsonValueException), delegate { string s = v.stringValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int i = v.intValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { double d = v.doubleValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int c = v.array.Count; } );
			Assert.Catch(typeof(JsonValueException), delegate { v.array.PushBack(1); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetObject(); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetArray(); } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v["hoge"].isBool; } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.IsBool("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.AddMember("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.HasMember("hoge"); } );
		}

		[Test]
		public void InplicitBoolValue ()
		{
			bool expectedValue = true;
			JsonValue v = expectedValue;
			
			Assert.AreEqual(expectedValue, v.boolValue);
			Assert.AreEqual(expectedValue, (bool)v);

			Assert.That( !v.isString );
			Assert.That( v.isBool );
			Assert.That( !v.isNull );
			Assert.That( !v.isInt );
			Assert.That( !v.isDouble );
			Assert.That( !v.isObject );
			Assert.That( !v.isArray );
			
//			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.boolValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { string s = v.stringValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int i = v.intValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { double d = v.doubleValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int c = v.array.Count; } );
			Assert.Catch(typeof(JsonValueException), delegate { v.array.PushBack(1); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetObject(); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetArray(); } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v["hoge"].isBool; } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.IsBool("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.AddMember("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.HasMember("hoge"); } );
		}

		[Test]
		public void InplicitIntValue ()
		{
			int expectedValue = 38485;
			JsonValue v = expectedValue;

			Assert.AreEqual(expectedValue, v.intValue);
			Assert.AreEqual(expectedValue, (int)v);
			
			Assert.That( !v.isString );
			Assert.That( !v.isBool );
			Assert.That( v.isInt );
			Assert.That( !v.isNull );
			Assert.That( !v.isDouble );
			Assert.That( !v.isObject );
			Assert.That( !v.isArray );
			
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.boolValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { string s = v.stringValue; } );
//			Assert.Catch(typeof(JsonValueException), delegate { int i = v.intValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { double d = v.doubleValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int c = v.array.Count; } );
			Assert.Catch(typeof(JsonValueException), delegate { v.array.PushBack(1); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetObject(); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetArray(); } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v["hoge"].isBool; } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.IsBool("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.AddMember("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.HasMember("hoge"); } );
		}

		[Test]
		public void InplicitNullValue ()
		{
			string expectedValue = null;
			JsonValue v = expectedValue;
			
//			Assert.AreEqual(expectedValue, v.stringValue);
//			Assert.AreEqual(expectedValue, (string)v);
//			
			Assert.That( !v.isString );
			Assert.That( !v.isBool );
			Assert.That( !v.isInt );
			Assert.That( v.isNull );
			Assert.That( !v.isDouble );
			Assert.That( !v.isObject );
			Assert.That( !v.isArray );
			
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.boolValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { string s = v.stringValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int i = v.intValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { double d = v.doubleValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int c = v.array.Count; } );
			Assert.Catch(typeof(JsonValueException), delegate { v.array.PushBack(1); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetObject(); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetArray(); } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v["hoge"].isBool; } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.IsBool("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.AddMember("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.HasMember("hoge"); } );
		}

		[Test]
		public void InplicitDoubleValue ()
		{
			double expectedValue = 344156.777883;
			JsonValue v = expectedValue;
			
			Assert.AreEqual(expectedValue, v.doubleValue);
			Assert.AreEqual(expectedValue, (double)v);
						
			Assert.That( !v.isString );
			Assert.That( !v.isBool );
			Assert.That( !v.isInt );
			Assert.That( !v.isNull );
			Assert.That( v.isDouble );
			Assert.That( !v.isObject );
			Assert.That( !v.isArray );
			
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.boolValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { string s = v.stringValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int i = v.intValue; } );
//			Assert.Catch(typeof(JsonValueException), delegate { double d = v.doubleValue; } );
			Assert.Catch(typeof(JsonValueException), delegate { int c = v.array.Count; } );
			Assert.Catch(typeof(JsonValueException), delegate { v.array.PushBack(1); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetObject(); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.SetArray(); } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v["hoge"].isBool; } );
			Assert.Catch(typeof(JsonValueException), delegate { bool b = v.IsBool("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.AddMember("hoge"); } );
			Assert.Catch(typeof(JsonValueException), delegate { v.HasMember("hoge"); } );
		}
	}
}
