﻿using UnityEngine;
using System;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class ParserTests
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
		[Test]
		public void Parse ()
		{
			JsonObject json = new JsonObject();
            json.ParseDocument(jsonData);
            Assert.That(json["FolderId"].isString);
		}


        [Test]
		[ExpectedException(typeof(JsonParseException))]
        public void ParseEmptyString()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument("");
        }

        [Test]
        public void ParseEmptyObject()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument("{}");
        }

        [Test]
		[ExpectedException(typeof(JsonParseException))]
		public void ParseEmptyArray()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument("{[]}");
        }
                [Test]
        public void ParseObjectEmptyArray()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument("{\"Assets\" : []}");
        }

 
        [Test]
        public void ParseTwoObject()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument(jsonData);
            json.ParseDocument("{}");
        }

        [Test]
		[ExpectedException(typeof(JsonParseException))]
		//[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseErrorString()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument("{qqs");
        }

                [Test]
        //[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseRandom()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument(@"[
              null,
              [
                null
              ],
              [
                []
              ]
            ]");

        }

        [Test]
        //[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseLongString()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument(@"{""ParentFolder"": ""00-01-00ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss-00-00-FF-FF-FF-FF-01-00-00-00-00-00-00-00-0C-02-00-00-00-4C-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2C-20-56-65-72-73-69-6F-6E-3D-33-2E-35-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-6E-75-6C-6C-05-01-00-00-00-1F-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-54-65-73-74-43-6C-61-73-73-07-00-00-00-05-5F-4E-61-6D-65-04-5F-4E-6F-77-0A-5F-42-69-67-4E-75-6D-62-65-72-09-5F-41-64-64-72-65-73-73-31-0A-5F-41-64-64-72-65-73-73-65-73-07-73-74-72-69-6E-67-73-0A-64-69-63-74-69-6F-6E-61-72-79-01-00-00-04-03-03-03-0D-05-1D-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-41-64-64-72-65-73-73-02-00-00-00-90-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4C-69-73-74-60-31-5B-5B-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-41-64-64-72-65-73-73-2C-20-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2C-20-56-65-72-73-69-6F-6E-3D-33-2E-35-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-6E-75-6C-6C-5D-5D-7F-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4C-69-73-74-60-31-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-E1-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-44-69-63-74-69-6F-6E-61-72-79-60-32-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-2C-5B-53-79-73-74-65-6D-2E-49-6E-74-33-32-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-02-00-00-00-06-03-00-00-00-04-52-69-63-6B-B6-25-3A-D1-C5-59-CC-88-0F-33-34-31-32-33-31-32-33-31-32-33-2E-31-32-31-09-04-00-00-00-09-05-00-00-00-09-06-00-00-00-09-07-00-00-00-05-04-00-00-00-1D-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-41-64-64-72-65-73-73-03-00-00-00-07-5F-73-74-72-65-65-74-06-5F-50-68-6F-6E-65-08-5F-45-6E-74-65-72-65-64-01-01-00-0D-02-00-00-00-06-08-00-00-00-0A-66-66-66-20-53-74-72-65-65-74-06-09-00-00-00-0E-28-35-30-33-29-20-38-31-34-2D-36-33-33-35-B6-BD-B8-BF-74-69-CC-88-04-05-00-00-00-90-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4C-69-73-74-60-31-5B-5B-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-41-64-64-72-65-73-73-2C-20-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2C-20-56-65-72-73-69-6F-6E-3D-33-2E-35-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-6E-75-6C-6C-5D-5D-03-00-00-00-06-5F-69-74-65-6D-73-05-5F-73-69-7A-65-08-5F-76-65-72-73-69-6F-6E-04-00-00-1F-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-41-64-64-72-65-73-73-5B-5D-02-00-00-00-08-08-09-0A-00-00-00-02-00-00-00-02-00-00-00-04-06-00-00-00-7F-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4C-69-73-74-60-31-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-03-00-00-00-06-5F-69-74-65-6D-73-05-5F-73-69-7A-65-08-5F-76-65-72-73-69-6F-6E-06-00-00-08-08-09-0B-00-00-00-03-00-00-00-03-00-00-00-04-07-00-00-00-E1-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-44-69-63-74-69-6F-6E-61-72-79-60-32-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-2C-5B-53-79-73-74-65-6D-2E-49-6E-74-33-32-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-04-00-00-00-07-56-65-72-73-69-6F-6E-08-43-6F-6D-70-61-72-65-72-08-48-61-73-68-53-69-7A-65-0D-4B-65-79-56-61-6C-75-65-50-61-69-72-73-00-03-00-03-08-92-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-47-65-6E-65-72-69-63-45-71-75-61-6C-69-74-79-43-6F-6D-70-61-72-65-72-60-31-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-08-E5-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4B-65-79-56-61-6C-75-65-50-61-69-72-60-32-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-2C-5B-53-79-73-74-65-6D-2E-49-6E-74-33-32-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-5B-5D-03-00-00-00-09-0C-00-00-00-03-00-00-00-09-0D-00-00-00-07-0A-00-00-00-00-01-00-00-00-04-00-00-00-04-1D-4E-65-77-74-6F-6E-73-6F-66-74-2E-4A-73-6F-6E-2E-54-65-73-74-73-2E-41-64-64-72-65-73-73-02-00-00-00-09-0E-00-00-00-09-0F-00-00-00-0D-02-11-0B-00-00-00-04-00-00-00-0A-06-10-00-00-00-18-4D-61-72-6B-75-73-20-65-67-67-65-72-20-5D-3E-3C-5B-2C-20-28-32-6E-64-29-0D-02-04-0C-00-00-00-92-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-47-65-6E-65-72-69-63-45-71-75-61-6C-69-74-79-43-6F-6D-70-61-72-65-72-60-31-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-00-00-00-00-07-0D-00-00-00-00-01-00-00-00-03-00-00-00-03-E3-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4B-65-79-56-61-6C-75-65-50-61-69-72-60-32-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-2C-5B-53-79-73-74-65-6D-2E-49-6E-74-33-32-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-04-EF-FF-FF-FF-E3-01-53-79-73-74-65-6D-2E-43-6F-6C-6C-65-63-74-69-6F-6E-73-2E-47-65-6E-65-72-69-63-2E-4B-65-79-56-61-6C-75-65-50-61-69-72-60-32-5B-5B-53-79-73-74-65-6D-2E-53-74-72-69-6E-67-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-2C-5B-53-79-73-74-65-6D-2E-49-6E-74-33-32-2C-20-6D-73-63-6F-72-6C-69-62-2C-20-56-65-72-73-69-6F-6E-3D-32-2E-30-2E-30-2E-30-2C-20-43-75-6C-74-75-72-65-3D-6E-65-75-74-72-61-6C-2C-20-50-75-62-6C-69-63-4B-65-79-54-6F-6B-65-6E-3D-62-37-37-61-35-63-35-36-31-39-33-34-65-30-38-39-5D-5D-02-00-00-00-03-6B-65-79-05-76-61-6C-75-65-01-00-08-06-12-00-00-00-0A-56-61-6C-20-26-20-61-73-64-31-01-00-00-00-01-ED-FF-FF-FF-EF-FF-FF-FF-06-14-00-00-00-0B-56-61-6C-32-20-26-20-61-73-64-31-03-00-00-00-01-EB-FF-FF-FF-EF-FF-FF-FF-06-16-00-00-00-0B-56-61-6C-33-20-26-20-61-73-64-31-04-00-00-00-01-0E-00-00-00-04-00-00-00-06-17-00-00-00-0E-1F-61-72-72-61-79-3C-61-64-64-72-65-73-73-09-09-00-00-00-B6-FD-0B-45-F4-58-CC-88-01-0F-00-00-00-04-00-00-00-06-19-00-00-00-0F-61-72-72-61-79-20-32-20-61-64-64-72-65-73-73-09-09-00-00-00-B6-3D-A2-1A-2B-58-CC-88-0B""}");

        }

        [Test]
        //[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseLongString2()
        {
            JsonObject json = new JsonObject();
            json.ParseDocument(@"{""ParentFolder"": ""000100sssssssssssssssssssssssssssssssssssssssssssssssssssssssassssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss1312321423432!@#$$%^^&*())))))))))))))))))""}");

			Assert.That ( json["ParentFolder"].stringValue == "000100sssssssssssssssssssssssssssssssssssssssssssssssssssssssassssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss1312321423432!@#$$%^^&*())))))))))))))))))" );
        }

        [Test]
        //[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseLongInt()
        {

			JsonObject json = new JsonObject();
            json.ParseDocument(@"{""ParentFolder"": -1111111111111111111111111111111111111}");


        }

        [Test]
        //[Ignore ("Ignored test")]
        //[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
        public void ParseLongDouble()
        {
			JsonObject json = new JsonObject();
            json.ParseDocument(@"{""ParentFolder"": 23.111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111}");
        }

    }
}
