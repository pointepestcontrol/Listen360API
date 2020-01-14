using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    class TestSurvey
    {
        TestUtil.TestContext _ctx;

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
        }

        [Test]
        public void CollectionPathIncludesOrganizationId()
        {
            Survey survey = new Survey(_ctx.Listen360, 1000, null, null);
            Assert.AreEqual("organizations/1000/surveys", survey.CollectionPath);
        }

        [Test]
        public void OrganizationIdAttributeNeverSerialized()
        {
            Survey survey = new Survey(_ctx.Listen360, 1000, null, null);
            Assert.IsFalse(survey.Changes.ContainsKey("organization-id"));
        }

        [Test]
        public void SerializesCompleteDocument()
        {
            Technician tech1 = new Technician(_ctx.Listen360);
            tech1.Reference = "TECH001";
            tech1.Name = "Sue Smith";

            Technician tech2 = new Technician(_ctx.Listen360);
            tech2.Reference = "TECH001";
            tech2.Name = "John Smith";

            Job job = new Job(_ctx.Listen360);
            job.Reference = "INV001";
            job.Performers.Add(tech1);
            job.Performers.Add(tech2);

            Customer customer = new Customer(_ctx.Listen360);
            customer.Reference = "CUST001";

            Franchise franchise = new Franchise(_ctx.Listen360);
            franchise.Reference = "ATLANTA-1";

            Survey survey = new Survey(_ctx.Listen360, 1000, franchise, customer, job);

            string xml = survey.SerializeChanges();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            Assert.AreEqual("survey", xmlDoc.DocumentElement.Name);
            Assert.AreEqual("ATLANTA-1", xmlDoc.DocumentElement.SelectSingleNode("organization/reference").InnerText);
            Assert.AreEqual("CUST001", xmlDoc.DocumentElement.SelectSingleNode("customer/reference").InnerText);
            Assert.AreEqual("INV001", xmlDoc.DocumentElement.SelectSingleNode("job/reference").InnerText);
            Assert.AreEqual(2, xmlDoc.DocumentElement.SelectNodes("job/performers/performer").Count);
        }
    }
}
