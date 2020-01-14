using System;
using System.Xml;
using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    [TestFixture]
    public class TestListen360
    {
        TestUtil.TestContext _ctx;

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
        }

        [Test]
        public void GetEntryPoints()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<organizations type=\"array\">{0}</organizations>", Properties.Resources.FakeFranchisor1));
            IOrganization[] entryPoints = _ctx.Listen360.GetEntryPoints();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations?page=1", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            AssertGetWebRequestResponse();
            Assert.AreEqual(1, entryPoints.Length);
        }

        [Test]
        public void GetEntryPointsPaginated()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<organizations type=\"array\">{0}</organizations>", Properties.Resources.FakeFranchisor1));
            IOrganization[] entryPoints = _ctx.Listen360.GetEntryPoints(2);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations?page=2", _ctx.Url), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            AssertGetWebRequestResponse();
            Assert.AreEqual(1, entryPoints.Length);
        }

        [Test]
        public void GetOrganizationByIdFranchisor()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, Properties.Resources.FakeFranchisor1);
            const int id = 1;
            IOrganization org = _ctx.Listen360.GetOrganizationById(id);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}", _ctx.Url, id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            AssertGetWebRequestResponse();
            Assert.IsNotNull(org);
            Assert.AreEqual(id, org.Id);
            Assert.IsInstanceOf(typeof(Franchisor), org);
        }

        [Test]
        public void GetOrganizationByIdFranchise()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, Properties.Resources.FakeFranchise1);
            const long id = 2;
            IOrganization org = _ctx.Listen360.GetOrganizationById(id);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}", _ctx.Url, id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            AssertGetWebRequestResponse();
            Assert.IsNotNull(org);
            Assert.AreEqual(id, org.Id);
            Assert.IsInstanceOf(typeof(Franchise), org);
        }

        private void AssertGetWebRequestResponse()
        {
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Get, _ctx.WebRequest.MethodSetLastValue);
            Assert.IsTrue(_ctx.WebRequest.BasicAuthorizationSetCalled);
            Assert.AreEqual(_ctx.Authentication, _ctx.WebRequest.BasicAuthorizationSetLastValue);
            Assert.IsTrue(_ctx.WebRequest.ResponseGetCalled);
        }
    }
}
