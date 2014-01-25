using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class ConsistencyTests
	{
		string input1 = 
			@"{""member3"":true,
		""member4"":false,
		""member5"":""hello world again"",
		""member2"":456.789,
		""member1"":123}";

		string input2 = 
			@"{""member3"":true,
		""member2"":456.789,
		""member4"":false,
		""member5"":""hello world again"",
		""member1"":123}";

		[Test]
		public void MemberOrderConsistencyParseVsCode ()
		{
			JsonObject json  = new JsonObject();
			JsonObject json2 = new JsonObject();

			json.ParseDocument(input1);

			json2["member3"] = true;
			json2["member2"] = 456.789;
			json2["member4"] = false;
			json2["member1"] = 123;
			json2["member5"] = "hello world again";
			
			Assert.AreEqual(json.ToPrettyString(), json2.ToPrettyString());
		}

		[Test]
		public void MemberOrderConsistencyFromParse ()
		{
			JsonObject json  = new JsonObject(input1);
			JsonObject json2 = new JsonObject(input2);
			
			Assert.AreEqual(json.ToPrettyString(), json2.ToPrettyString());
		}

		[Test]
		public void MemberOrderConsistencyFromCode ()
		{
			JsonObject json  = new JsonObject();
			JsonObject json2 = new JsonObject();

			json["member1"] = 123;
			json["member2"] = 456.789;
			json["member3"] = true;
			json["member4"] = false;
			json["member5"] = "hello world again";			

			json2["member3"] = true;
			json2["member2"] = 456.789;
			json2["member4"] = false;
			json2["member1"] = 123;
			json2["member5"] = "hello world again";

			Assert.AreEqual(json.ToPrettyString(), json2.ToPrettyString());
		}
	}
}
