#region Header
/**
 * JsonTest.cs
 *   Tests for the Json class.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using LitJson;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NativeJsonTest
{
    [TestFixture]
    public class JsonTest
    {
        [Test]
        public void AsArrayTest ()
        {
            JsonObject data = new JsonObject ();

            JsonValueArray array = data.array;

            array.PushBack(1);
            array.PushBack(2);
            array.PushBack(3);
            array.PushBack("Launch!");

            //Assert.IsTrue(array.IsArray, "A1");
            Assert.AreEqual ("[1,2,3,\"Launch!\"]", data.ToString(), "A2");
        }

        [Test]
        public void AsBooleanTest ()
        {
            JsonValue data;

            data = true;
            Assert.IsTrue (data.isBool, "A1");
            Assert.AreEqual (true.ToString(), data.ToString (), "A3");

            data = false;
            bool f = false;

            Assert.AreEqual (f, (bool) data, "A4");
        }

        [Test]
        public void AsDoubleTest ()
        {
            JsonValue data;

            data = 3e6;
            Assert.IsTrue (data.isDouble, "A1");
            Assert.AreEqual (3e6, (double) data, "A2");
			Assert.AreEqual (((double)3000000.0).ToString(), data.ToString (), "A3");

            data = 3.14;
            Assert.IsTrue (data.isDouble, "A4");
            Assert.AreEqual (3.14, (double) data, "A5");
            Assert.AreEqual(((double)3.14).ToString(), data.ToString(), "A6");

            data = 0.123;
            double n = 0.123;

            Assert.AreEqual (n, (double) data, "A7");
        }

        [Test]
        public void AsIntTest ()
        {
            JsonValue data;

            data = 13;
            Assert.IsTrue (data.isInt, "A1");
            Assert.AreEqual ((int) data, 13, "A2");
            Assert.AreEqual(data.ToString(), "13", "A3");

            data = -00500;

            Assert.IsTrue (data.isInt, "A4");
            Assert.AreEqual ((int) data, -500, "A5");
            Assert.AreEqual(data.ToString(), "-500", "A6");

            data = 1024;
            int n = 1024;

            Assert.AreEqual ((int) data, n, "A7");
        }

        [Test]
        public void AsObjectTest ()
        {
            JsonObject data = new JsonObject ();

            data.AddMember("alignment", "left");
            //data["alignment"] = "left";
            data.AddMember("font", new JsonObject());
            data["font"].AddMember("name", "Arial");
            data["font"].AddMember("style", "italic");
            data["font"].AddMember("size", 10);
            data["font"].AddMember("color", "#fff");


            Assert.IsTrue (data.isObject, "A1");

            string json = "{\"alignment\":\"left\",\"font\":{" +
                "\"name\":\"Arial\",\"style\":\"italic\",\"size\":10," +
                "\"color\":\"#fff\"}}";

            Assert.AreEqual (json, data.ToString(), "A2");
        }

        [Test]
        public void AsStringTest ()
        {
            JsonValue data;

            data = "All you need is love";
            Assert.IsTrue (data.isString, "A1");
            Assert.AreEqual ("All you need is love", (string) data, "A2");
            Assert.AreEqual ("All you need is love", data.ToString (),
                             "A3");
        }

        [Test]
        public void EqualsTest ()
        {
            JsonValue a;
            JsonValue b;

            // Compare ints
            a = 7;
            b = 7;
            Assert.IsTrue (a.Equals (b), "A1");

            Assert.IsFalse (a.Equals (null), "A2");

            b = 8;
            Assert.IsFalse (a.Equals (b), "A3");

            // Compare longs
            a = 10L;
            b = 10L;
            Assert.IsTrue (a.Equals (b), "A4");

            b = 10;
            Assert.IsFalse (a.Equals (b), "A5");
            b = 11L;
            Assert.IsFalse (a.Equals (b), "A6");

            // Compare doubles
            a = 78.9;
            b = 78.9;
            Assert.IsTrue (a.Equals (b), "A7");

            b = 78.899999;
            Assert.IsFalse (a.Equals (b), "A8");

            // Compare booleans
            a = true;
            b = true;
            Assert.IsTrue (a.Equals (b), "A9");

            b = false;
            Assert.IsFalse (a.Equals (b), "A10");

            // Compare strings
            a = "walrus";
            b = "walrus";
            Assert.IsTrue (a.Equals (b), "A11");

            b = "Walrus";
            Assert.IsFalse (a.Equals (b), "A12");
        }

        [Test]
        public void GetKeysTest ()
        {
            JsonObject data = new JsonObject ();

            data.AddMember("first", "one");
            data.AddMember("second", "two");
            data.AddMember("third", "three");
            data.AddMember("fourth", "four");

            //Assert.AreEqual (4, data.Count, "A1");

            //foreach (string k in data.Keys)
            //    Assert.IsNotNull (data[k], "A2");
        }

        [Test]
        [ExpectedException (typeof (InvalidCastException))]
        public void InvalidCastTest ()
        {
            JsonValue data = 35;

            string str = (string) data;

            if (str != (string) data)
                str = (string) data;
        }

        [Test]
        public void NullValue ()
        {
            string json = "{\"test\":null}";

            JsonObject data = new JsonObject ();
            data.AddMember("test", (string)null);

            Assert.AreEqual (json, data.ToString ());
        }

        [Test]
        public void PropertiesOrderTest ()
        {
            JsonObject data = new JsonObject ();

            string json = "{\"first\":\"one\",\"second\":\"two\"," +
                "\"third\":\"three\",\"fourth\":\"four\"}";


                data.AddMember("first", "one");
                data.AddMember("second", "two");
                data.AddMember("third", "three");
                data.AddMember("fourth", "four");
                Assert.AreEqual (json, data.ToString ());
            
        }
    }
}
