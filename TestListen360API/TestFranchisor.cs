using System;
using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    [TestFixture]
    public class TestFranchisor
    {
        TestUtil.TestContext _ctx;
        Franchisor _franchisor;

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
            _franchisor = new Franchisor(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchisor1));
        }

        [Test]
        public void Construct()
        {
            Assert.AreEqual(1, _franchisor.Id);
            Assert.IsFalse(_franchisor.ParentId.HasValue);
            Assert.AreEqual(1, _franchisor.RootId);
            Assert.AreEqual("Proserv Inc", _franchisor.Name);
            Assert.AreEqual("Proserv", _franchisor.ExternalName);
            Assert.AreEqual("joe.franchisor@example.com", _franchisor.Email);
            Assert.AreEqual("Active", _franchisor.Status);
            Assert.AreEqual("1 Mansell Road", _franchisor.StreetAddress);
            Assert.AreEqual("Alpharetta", _franchisor.City);
            Assert.AreEqual("GA", _franchisor.Region);
            Assert.AreEqual("30022", _franchisor.PostalCode);
            Assert.AreEqual("US", _franchisor.Country);
            Assert.AreEqual("Eastern Time (US & Canada)", _franchisor.TimeZone);
            Assert.AreEqual("678-444-2121", _franchisor.PhoneNumber);
            Assert.AreEqual("http://www.proserv.com", _franchisor.Website);
            Assert.AreEqual(new DateTime(2008, 01, 01, 17, 59, 50), _franchisor.CreatedAt.Value.ToUniversalTime());
        }

        [Test]
        public void GetCustomAttributeDefinitions()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<custom-attribute-definitions type=\"array\">{0}</custom-attribute-definitions>", Properties.Resources.FakeCustomAttributeDefinition1));
            ICustomAttributeDefinition[] attrDefs = _franchisor.GetCustomAttributeDefinitions();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/custom_attribute_definitions", _ctx.Url, _franchisor.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, attrDefs.Length);
            Assert.AreEqual(205, attrDefs[0].Id);
        }

        [Test]
        public void CustomAttributesNotSupported()
        {
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(this.ReadCustomAttribute));
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(this.WriteCustomAttribute));
        }

        public void ReadCustomAttribute()
        {
            string attr = _franchisor[1];
        }

        public void WriteCustomAttribute()
        {
            _franchisor[1] = "foo";
        }
    }
}
