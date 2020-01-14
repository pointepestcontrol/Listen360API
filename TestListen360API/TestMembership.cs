using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    class TestMembership
    {
        TestUtil.TestContext _ctx;

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
        }

        [Test]
        public void Construct()
        {
            Membership membership = new Membership(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeMembership1));
            Assert.AreEqual(20, membership.Id);
            Assert.AreEqual(2, membership.OrganizationId);
            Assert.AreEqual("Joe", membership.FirstName);
            Assert.AreEqual("Franchisee", membership.LastName);
            Assert.AreEqual("joe.franchisee@example.com", membership.Email);
            Assert.AreEqual("Active", membership.Status);
            Assert.AreEqual("Owner", membership.Role);
            Assert.AreEqual("555-444-3333", membership.MobilePhoneNumber);
            Assert.AreEqual("US", membership.Country);
            Assert.AreEqual(new DateTime(2008, 12, 31, 11, 56, 47), membership.AcceptedAt.Value.ToUniversalTime());
        }

        [Test]
        public void CollectionPathIncludesOrganizationId()
        {
            Membership tech = new Membership(_ctx.Listen360, 1000);
            Assert.AreEqual("organizations/1000/memberships", tech.CollectionPath);
        }

        [Test]
        public void OrganizationIdAttributeNeverSerialized()
        {
            Membership tech = new Membership(_ctx.Listen360, 1000);
            Assert.IsFalse(tech.Changes.ContainsKey("organization-id"));
        }
    }
}
