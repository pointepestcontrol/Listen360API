using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    class TestJob
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
            Job job = new Job(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeJob1));
            Assert.AreEqual(1, job.OrganizationId);
            Assert.AreEqual(5, job.CustomerId);
            Assert.AreEqual("job-ref-1", job.Reference);
            Assert.AreEqual("Completed", job.Status);
            Assert.AreEqual(575.37, job.Value);
            Assert.AreEqual("USD", job.Currency);
            Assert.AreEqual(new DateTime(2009, 12, 08, 15, 00, 00), job.CommencedAt.Value.ToUniversalTime());
            Assert.AreEqual(new DateTime(2009, 12, 08, 17, 30, 00), job.CompletedAt.Value.ToUniversalTime());

            Assert.NotNull(job.Performers);
            Assert.AreEqual(1, job.Performers.Count);
            Assert.IsInstanceOf(typeof(Technician), job.Performers[0]);
        }

        [Test]
        public void CollectionPathIncludesOrganizationId()
        {
            Job job = new Job(_ctx.Listen360, 1000, 20000);
            Assert.AreEqual("organizations/1000/jobs", job.CollectionPath);
        }

        [Test]
        public void OrganizationIdAttributeNeverSerialized()
        {
            Job job = new Job(_ctx.Listen360, 1000, 20000);
            Assert.IsFalse(job.Changes.ContainsKey("organization-id"));
        }

        [Test]
        public void SerializesPerformerIds()
        {
            Technician tech = new Technician(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeTechnician1));

            Job job = new Job(_ctx.Listen360, 1000, 20000);
            job.Performers.Add(tech);

            string xml = job.SerializeChanges();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            Assert.AreEqual("job", xmlDoc.DocumentElement.Name);
            Assert.AreEqual("20000", xmlDoc.DocumentElement.SelectSingleNode("customer-id").InnerText);
            Assert.AreEqual(tech.Id.ToString(), xmlDoc.DocumentElement.SelectSingleNode("performers/performer/id").InnerText);
        }

        [Test]
        public void ForceFeedback()
        {
            Job job = new Job(_ctx.Listen360, 1000, 20000);
            job.ForceFeedback = true;

            string xml = job.SerializeChanges();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            Assert.AreEqual("job", xmlDoc.DocumentElement.Name);
            Assert.AreEqual("20000", xmlDoc.DocumentElement.SelectSingleNode("customer-id").InnerText);
            Assert.AreEqual("True", xmlDoc.DocumentElement.SelectSingleNode("force-feedback").InnerText);
        }
    }
}
