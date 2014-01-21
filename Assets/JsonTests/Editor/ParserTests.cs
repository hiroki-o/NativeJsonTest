using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class ParserTests : MonoBehaviour
	{
//		[Datapoint]
//		public double zero = 0;
		[SerializeField]
		public string testInput1 = "1.txt";

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

        string jsonData = @"{
  ""$id"": ""1"",
  ""FolderId"": ""a4e8ba80-eb24-4591-bb1c-62d3ad83701e"",
  ""Name"": ""Root folder"",
  ""Description"": ""Description!"",
  ""CreatedDate"": ""2000-12-10T10:50:00Z"",
  ""Files"": [],
  ""ChildFolders"": [
    {
      ""$id"": ""2"",
      ""FolderId"": ""484936e2-7cbb-4592-93ff-b2103e5705e4"",
      ""Name"": ""Child folder"",
      ""Description"": ""Description!"",
      ""CreatedDate"": ""2001-11-20T10:50:00Z"",
      ""Files"": [
        {
          ""$id"": ""3"",
          ""FileId"": ""cc76d734-49f1-4616-bb38-41514228ac6c"",
          ""Name"": ""File 1"",
          ""Description"": ""Description!"",
          ""CreatedDate"": ""2002-10-30T10:50:00Z"",
          ""Folder"": {
            ""$ref"": ""2""
          },
          ""EntityKey"": {
            ""$id"": ""4"",
            ""EntitySetName"": ""File"",
            ""EntityContainerName"": ""DataServicesTestDatabaseEntities"",
            ""EntityKeyValues"": [
              {
                ""Key"": ""FileId"",
                ""Type"": ""System.Guid"",
                ""Value"": ""cc76d734-49f1-4616-bb38-41514228ac6c""
              }
            ]
          }
        }
      ],
      ""ChildFolders"": [],
      ""ParentFolder"": {
        ""$ref"": ""1""
      },
      ""EntityKey"": {
        ""$id"": ""5"",
        ""EntitySetName"": ""Folder"",
        ""EntityContainerName"": ""DataServicesTestDatabaseEntities"",
        ""EntityKeyValues"": [
          {
            ""Key"": ""FolderId"",
            ""Type"": ""System.Guid"",
            ""Value"": ""484936e2-7cbb-4592-93ff-b2103e5705e4""
          }
        ]
      }
    }
  ],
  ""ParentFolder"": null,
  ""EntityKey"": {
    ""$id"": ""6"",
    ""EntitySetName"": ""Folder"",
    ""EntityContainerName"": ""DataServicesTestDatabaseEntities"",
    ""EntityKeyValues"": [
      {
        ""Key"": ""FolderId"",
        ""Type"": ""System.Guid"",
        ""Value"": ""a4e8ba80-eb24-4591-bb1c-62d3ad83701e""
      }
    ]
  }
}";


        public string readFile()
        {
            TextReader tr = File.OpenText(@"C:\Users\zhonglei\Documents\GitHub\NativeJsonTest\Assets\TestData\1.txt");
            var temp = tr.ReadToEnd();
            tr.Close();
            return temp;

        }


		[Test]
		public void Parse ()
		{
			Json json = new Json();
            json.ParseDocument(jsonData);
            Assert.That(json.GetValue("FolderId").isString);
		}


        [Test]
        public void ParseEmptyString()
        {
            Json json = new Json();
            json.ParseDocument("");
        }

        [Test]
        public void ParseEmptyObject()
        {
            Json json = new Json();
            json.ParseDocument("{}");
        }

        [Test]
        public void ParseEmptyArray()
        {
            Json json = new Json();
            json.ParseDocument("{[]}");
        }
                [Test]
        public void ParseObjectEmptyArray()
        {
            Json json = new Json();
            json.ParseDocument("{\"Assets\" : []}");
        }

 
        [Test]
        public void ParseTwoObject()
        {
            Json json = new Json();
            json.ParseDocument(jsonData);
            json.ParseDocument("{}");
        }

        [Test]
        //[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseErrorString()
        {
            Json json = new Json();
            json.ParseDocument("{qqs");
        }


	}
}
