using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using NUnit.Framework;
using System.IO;

namespace NativeJsonTest
{
	[TestFixture]
	public class WWWTests : MonoBehaviour
	{
		[Test]
		public void GetIPAddr ()
		{
			string url = "http://ip.jsontest.com/";

			WWW www = new WWW(url);
			//yield return www;
			while(!www.isDone) {
				Thread.Sleep (100);
			}

			Json json = new Json();
            json.ParseDocument(www.text);
			Assert.That(json["ip"].isString);
		}
	}
}
