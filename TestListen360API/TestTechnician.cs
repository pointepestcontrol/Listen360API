using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    class TestTechnician
    {
        TestUtil.TestContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new TestUtil.TestContext();
        }

        [Test]
        public void Construct()
        {
            Technician tech = new Technician(_context.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeTechnician1));
            Assert.AreEqual(15, tech.Id);
            Assert.AreEqual(1, tech.OrganizationId);
            Assert.AreEqual("Albert Smith", tech.Name);
            Assert.AreEqual("albert.smith@example.com", tech.Email);
            Assert.AreEqual("TECH100", tech.Reference);
            Assert.AreEqual("Active", tech.Status);
            Assert.AreEqual("333-444-5555", tech.MobilePhoneNumber);
            Assert.AreEqual("US", tech.Country);
        }

        [Test]
        public void CollectionPathIncludesOrganizationId()
        {
            Technician tech = new Technician(_context.Listen360, 1000);
            Assert.AreEqual("organizations/1000/technicians", tech.CollectionPath);
        }

        [Test]
        public void OrganizationIdAttributeNeverSerialized()
        {
            Technician tech = new Technician(_context.Listen360, 1000);
            Assert.IsFalse(tech.Changes.ContainsKey("organization-id"));
        }

        [Test]
        public void CustomAttributesNotSupported()
        {
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(this.ReadCustomAttribute));
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(this.WriteCustomAttribute));
        }

        public void ReadCustomAttribute()
        {
            Technician tech = new Technician(_context.Listen360, 1000);
            string attr = tech[1];
        }

        public void WriteCustomAttribute()
        {
            Technician tech = new Technician(_context.Listen360, 1000);
            tech[1] = "foo";
        }
    }
}
