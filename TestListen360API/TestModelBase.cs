using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    [TestFixture]
    class TestModelBase
    {
        TestUtil.TestContext _ctx;

        const string _postXml = "<post><id type=\"integer\">1</id><title>First Post</title><body>Ha!</body><summary nil=\"true\"></summary><created-at type=\"datetime\">2008-01-01T12:59:50-05:00</created-at><custom-choice-1-label>Article</custom-choice-1-label></post>";
        const string _errorsXml = "<errors><error>Title cannot be blank</error><error>Body cannot be blank</error></errors>";

        class Post : Listen360API.ModelBase
        {
            public Post(IListen360 listen360) : base(listen360) {}
            public Post(IListen360 listen360, XmlNode node) : base(listen360, node) {}

            public override string CollectionPath
            {
                get { return "posts"; }
            }

            public string Title
            {
                get { return (string)ReadAttribute("title"); }
                set { WriteAttribute("title", value); }
            }

            public string Body
            {
                get { return (string)ReadAttribute("body"); }
                set { WriteAttribute("body", value); }
            }

            public string Summary
            {
                get { return (string)ReadAttribute("summary"); }
                set { WriteAttribute("summary", value); }
            }
        }

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
        }

        [Test]
        public void ConstructNewRecord()
        {
            Post post = new Post(_ctx.Listen360);
            Assert.IsFalse(post.HasChanged);
            Assert.IsFalse(post.Id.HasValue);
            Assert.IsNull(post.Title);
            Assert.IsNull(post.Body);
            Assert.IsNull(post.CreatedAt);
            Assert.IsNull(post.UpdatedAt);
            Assert.IsTrue(post.IsNewRecord);
        }

        [Test]
        public void ConstructDeserialize()
        {
            XmlNode node = TestUtil.GetElementForXml(_postXml);
            Post post = new Post(_ctx.Listen360, node);

            Assert.AreEqual(1, post.Id);
            Assert.AreEqual("First Post", post.Title);
            Assert.AreEqual("Ha!", post.Body);
            Assert.IsNull(post.Summary);
            Assert.AreEqual(new DateTime(2008, 01, 01, 17, 59, 50), post.CreatedAt.Value.ToUniversalTime());

            Assert.IsFalse(post.HasChanged);
            Assert.IsFalse(post.IsNewRecord);
        }

        [Test]
        public void DeserializeSingleInstance()
        {
            XmlNode node = TestUtil.GetElementForXml(TestListen360API.Properties.Resources.FakeFranchise1);
            object result = ModelBase.Deserialize(_ctx.Listen360, node);
            Assert.NotNull(result);
            Assert.IsAssignableFrom(typeof(Franchise), result);
        }

        [Test]
        public void DeserializeArray()
        {
            XmlNode node = TestUtil.GetElementForXml(string.Format("<records type=\"array\">{0}{1}</records>", TestListen360API.Properties.Resources.FakeFranchisor1, TestListen360API.Properties.Resources.FakeFranchise1));
            object result = ModelBase.Deserialize(_ctx.Listen360, node);

            Assert.NotNull(result);
            Assert.IsAssignableFrom(typeof(ArrayList), result);

            ArrayList list = (ArrayList)result;
            Assert.AreEqual(2, list.Count);
            Assert.IsAssignableFrom(typeof(Franchisor), list[0]);
            Assert.IsAssignableFrom(typeof(Franchise), list[1]);
        }

        [Test]
        public void DeserializeEmptyArray()
        {
            XmlNode node = TestUtil.GetElementForXml("<records type=\"array\"></records>");
            object result = ModelBase.Deserialize(_ctx.Listen360, node);

            Assert.NotNull(result);
            Assert.IsAssignableFrom(typeof(ArrayList), result);

            ArrayList list = (ArrayList)result;
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Create()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, _postXml);

            Post post = new Post(_ctx.Listen360);
            post.Title = "First Post";
            post.Body = "Ha!";
            post.Summary = null;

            Assert.IsTrue(post.Save());

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/posts", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Post, _ctx.WebRequest.MethodSetLastValue);
            Assert.IsTrue(_ctx.WebRequest.RequestSetCalled);
            Assert.AreEqual("<post><body>Ha!</body><summary nil=\"true\" /><title>First Post</title></post>", _ctx.WebRequest.RequestSetLastValue);

            Assert.AreEqual(1, post.Id);
            Assert.AreEqual(new DateTime(2008, 01, 01, 17, 59, 50), post.CreatedAt.Value.ToUniversalTime());
            Assert.IsFalse(post.HasChanged);
            Assert.IsFalse(post.IsNewRecord);
        }

        [Test]
        public void CreateInvalid()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, _errorsXml);

            Post post = new Post(_ctx.Listen360);
            post.Title = "My post";
            post.Save();

            Assert.IsFalse(post.Save());

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/posts", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Post, _ctx.WebRequest.MethodSetLastValue);
            Assert.IsTrue(_ctx.WebRequest.RequestSetCalled);
            Assert.AreEqual("<post><title>My post</title></post>", _ctx.WebRequest.RequestSetLastValue);

            Assert.IsTrue(post.HasChanged);
            Assert.IsTrue(post.IsNewRecord);
            Assert.IsFalse(post.IsValid);

            Assert.AreEqual(2, post.Errors.Length);
            Assert.AreEqual("Title cannot be blank", post.Errors[0]);
            Assert.AreEqual("Body cannot be blank", post.Errors[1]);
        }

        [Test]
        public void Update()
        {
            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            post.Body = "Welcome to my blog";

            Assert.IsTrue(post.HasChanged);
            Assert.IsTrue(post.Save());

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/posts/1", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Put, _ctx.WebRequest.MethodSetLastValue);
            Assert.IsTrue(_ctx.WebRequest.RequestSetCalled);
            Assert.AreEqual("<post><id>1</id><body>Welcome to my blog</body></post>", _ctx.WebRequest.RequestSetLastValue);

            Assert.IsFalse(post.HasChanged);
        }

        [Test]
        public void UpdateInvalid()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, _errorsXml);

            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            post.Body = "";

            Assert.IsFalse(post.Save());

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/posts/1", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Put, _ctx.WebRequest.MethodSetLastValue);
            Assert.IsTrue(_ctx.WebRequest.RequestSetCalled);
            Assert.AreEqual("<post><id>1</id><body></body></post>", _ctx.WebRequest.RequestSetLastValue);

            Assert.IsTrue(post.HasChanged);
            Assert.IsFalse(post.IsValid);

            Assert.AreEqual(2, post.Errors.Length);
            Assert.AreEqual("Title cannot be blank", post.Errors[0]);
            Assert.AreEqual("Body cannot be blank", post.Errors[1]);
        }

        [Test]
        public void Delete()
        {
            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            post.Delete();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/posts/1", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Delete, _ctx.WebRequest.MethodSetLastValue);
        }

        [Test]
        public void ReadCustomAttributeChoiceLabel()
        {
            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            Assert.NotNull(post[1]);
            Assert.AreEqual("Article", post[1]);
        }

        [Test]
        public void WriteCustomAttributeChoiceLabel()
        {
            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            post[1] = "News";
            Assert.AreEqual("News", post.Changes["custom-choice-1-label"]);
        }

        public class FakeCustomAttributeDefinition : ICustomAttributeDefinition
        {
            #region ICustomAttributeDefinition Members

            public long OrganizationId
            {
                get { throw new NotImplementedException(); }
            }

            public string Name
            {
                get { throw new NotImplementedException(); }
            }

            public int Ordinal
            {
                get { return 1; }
            }

            public string AttachedTo
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsMandatory
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsRestrictedToPredefinedChoices
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region IModel Members

            public long? Id
            {
                get { throw new NotImplementedException(); }
            }

            public DateTime? CreatedAt
            {
                get { throw new NotImplementedException(); }
            }

            public DateTime? UpdatedAt
            {
                get { throw new NotImplementedException(); }
            }

            public string this[int ordinal]
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string this[ICustomAttributeDefinition attributeDefinition]
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public bool IsNewRecord
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsValid
            {
                get { throw new NotImplementedException(); }
            }

            public string[] Errors
            {
                get { throw new NotImplementedException(); }
            }

            public Hashtable Changes
            {
                get { throw new NotImplementedException(); }
            }

            public bool HasChanged
            {
                get { throw new NotImplementedException(); }
            }

            public string SerializeChanges()
            {
                throw new NotImplementedException();
            }

            public void SerializeChanges(XmlDocument doc, XmlNode root)
            {
                throw new NotImplementedException();
            }

            public bool Save()
            {
                throw new NotImplementedException();
            }

            public void Reload()
            {
                throw new NotImplementedException();
            }

            public void Delete()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        [Test]
        public void ReadCustomAttributeChoiceLabelUsingAttributeDefinition()
        {
            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            ICustomAttributeDefinition attrDef = new FakeCustomAttributeDefinition();
            Assert.AreEqual(1, attrDef.Ordinal);
            Assert.NotNull(post[attrDef]);
            Assert.AreEqual("Article", post[attrDef]);
        }

        [Test]
        public void WriteCustomAttributeChoiceLabelUsingAttributeDefinition()
        {
            Post post = new Post(_ctx.Listen360, TestUtil.GetElementForXml(_postXml));
            ICustomAttributeDefinition attrDef = new FakeCustomAttributeDefinition();
            post[attrDef] = "News";
            Assert.AreEqual("News", post[attrDef]);
        }

        [Test]
        public void RestrictToTenCustomAttributes()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), new TestDelegate(AccessCustomAttributeAtIndexZero));
            Assert.Throws(typeof(ArgumentOutOfRangeException), new TestDelegate(AccessCustomAttributeAtIndexEleven));
        }

        public void AccessCustomAttributeAtIndexZero()
        {
            Post post = new Post(_ctx.Listen360);
            string attr = post[0];
        }

        public void AccessCustomAttributeAtIndexEleven()
        {
            Post post = new Post(_ctx.Listen360);
            string attr = post[11];
        }
    }
}
