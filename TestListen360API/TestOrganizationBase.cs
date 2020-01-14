using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    [TestFixture]
    class TestOrganizationBase
    {
        TestUtil.TestContext _ctx;
        OrganizationBase _organization;

        class BookClub : OrganizationBase
        {
            public BookClub(IListen360 listen360)
                : base(listen360)
            { }

            public BookClub(IListen360 listen360, XmlNode node)
                : base(listen360, node)
            { }
        }

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
            _organization = new BookClub(_ctx.Listen360, TestUtil.GetXmlDocument("<book-club><id type=\"integer\">1</id></book-club>"));
        }

        [Test]
        public void FindChildByReference()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<children type=\"array\">{0}</children>", Properties.Resources.FakeFranchise1));
            IOrganization child = _organization.FindChildByReference("ABC123");

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/children?reference=ABC123", _ctx.Url, _organization.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(child);
        }

        [Test]
        public void FindChildByReferenceNotFound()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, "<children type=\"array\"></children>");
            IOrganization child = _organization.FindChildByReference("ABC123");
            Assert.Null(child);
        }

        [Test]
        public void GetChildren()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<children type=\"array\">{0}{1}</children>", Properties.Resources.FakeFranchise1, Properties.Resources.FakeFranchise2));
            IOrganization[] subsidiaries = _organization.GetChildren();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/children?page=1", _ctx.Url, _organization.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(2, subsidiaries.Length);
            Assert.AreEqual(2, subsidiaries[0].Id);
            Assert.AreEqual(3, subsidiaries[1].Id);
        }

        [Test]
        public void GetChildrenPaginated()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest,
                string.Format("<children type=\"array\">{0}{1}</children>",
                    Properties.Resources.FakeFranchise1, Properties.Resources.FakeFranchise2));

            IOrganization[] subsidiaries = _organization.GetChildren(2);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/children?page=2", _ctx.Url, _organization.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(2, subsidiaries.Length);
            Assert.AreEqual(2, subsidiaries[0].Id);
            Assert.AreEqual(3, subsidiaries[1].Id);
        }

        [Test]
        public void FindDescendentByReference()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<descendents type=\"array\">{0}</descendents>", Properties.Resources.FakeFranchise1));
            IOrganization child = _organization.FindDescendentByReference("ABC123");

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/descendents?reference=ABC123", _ctx.Url, _organization.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(child);
        }

        [Test]
        public void FindDescendentByReferenceNotFound()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, "<descendents type=\"array\"></descendents>");
            IOrganization child = _organization.FindChildByReference("ABC123");
            Assert.Null(child);
        }

        [Test]
        public void GetDescendents()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<descendents type=\"array\">{0}{1}</descendents>", Properties.Resources.FakeFranchise1, Properties.Resources.FakeFranchise2));
            IOrganization[] subsidiaries = _organization.GetDescendents();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/descendents?page=1", _ctx.Url, _organization.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(2, subsidiaries.Length);
            Assert.AreEqual(2, subsidiaries[0].Id);
            Assert.AreEqual(3, subsidiaries[1].Id);
        }

        [Test]
        public void GetDescendentsPaginated()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest,
                string.Format("<descendents type=\"array\">{0}{1}</descendents>",
                    Properties.Resources.FakeFranchise1, Properties.Resources.FakeFranchise2));

            IOrganization[] subsidiaries = _organization.GetDescendents(2);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/organizations/{1}/descendents?page=2", _ctx.Url, _organization.Id), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(2, subsidiaries.Length);
            Assert.AreEqual(2, subsidiaries[0].Id);
            Assert.AreEqual(3, subsidiaries[1].Id);
        }

        [Test]
        public void FindMembershipByEmail()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<memberships type=\"array\">{0}</memberships>", Properties.Resources.FakeMembership1));

            IMembership membership = _organization.FindMembershipByEmail("bob@example.com");

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/memberships?email=bob@example.com", _ctx.Url, _organization.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(membership);
        }

        [Test]
        public void FindMembershipByEmailNotFound()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, "<memberships type=\"array\"></memberships>");
            IMembership membership = _organization.FindMembershipByEmail("bob@example.com");
            Assert.Null(membership);
        }

        [Test]
        public void GetMemberships()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<memberships type=\"array\">{0}</memberships>", Properties.Resources.FakeMembership1));
            IMembership[] memberships = _organization.GetMemberships();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/memberships?page=1", _ctx.Url, _organization.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, memberships.Length);
            Assert.AreEqual(20, memberships[0].Id);
        }

        [Test]
        public void GetMembershipsPaginated()
        {
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<memberships type=\"array\">{0}</memberships>", Properties.Resources.FakeMembership1));
            _organization.GetMemberships(2);
            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/memberships?page=2", _ctx.Url, _organization.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
        }
    }
}
