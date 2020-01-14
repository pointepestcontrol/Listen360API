using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Listen360API;
using NUnit.Framework;

namespace TestListen360API
{
    class TestCustomer
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
            Customer customer = new Customer(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeCustomer1));

            Assert.AreEqual(3, customer.Id);
            Assert.AreEqual(2, customer.OrganizationId);
            Assert.AreEqual("Mrs", customer.Title);
            Assert.AreEqual("Jane", customer.FirstName);
            Assert.AreEqual("Doe", customer.LastName);
            Assert.AreEqual("Acme Corp", customer.CompanyName);
            Assert.AreEqual("jane.doe@example.com", customer.Email);
            Assert.AreEqual("4264465", customer.Reference);
            Assert.AreEqual("Active", customer.Status);
            Assert.AreEqual("Promoter", customer.NetPromoterLabel);

            Assert.AreEqual("1000 Hollywood Blvd", customer.StreetAddress);
            Assert.AreEqual("Hollywood", customer.City);
            Assert.AreEqual("CA", customer.Region);
            Assert.AreEqual("90068", customer.PostalCode);
            Assert.AreEqual("US", customer.Country);

            Assert.AreEqual("111-111-1111", customer.MobilePhoneNumber);
            Assert.AreEqual("222-222-2222", customer.WorkPhoneNumber);
            Assert.AreEqual("333-333-3333", customer.HomePhoneNumber);

            Assert.IsTrue(customer.PermitsContact.Value);
            Assert.IsFalse(customer.LastSurveyBounced.Value);

            Assert.AreEqual(new DateTime(2008, 01, 01, 19, 00, 00), customer.CreatedAt.Value.ToUniversalTime());
        }

        [Test]
        public void CollectionPathIncludesOrganizationId()
        {
            Customer customer = new Customer(_ctx.Listen360, 1000);
            Assert.AreEqual("organizations/1000/customers", customer.CollectionPath);
        }

        [Test]
        public void OrganizationIdAttributeNeverSerialized()
        {
            Customer customer = new Customer(_ctx.Listen360, 1000);
            Assert.IsFalse(customer.Changes.ContainsKey("organization-id"));
        }

        [Test]
        public void SendSurveyInvite()
        {
            _ctx.SetResponseXml("<survey><invited-at type=\"datetime\">2009-12-08T15:06:10-05:00</invited-at></survey>");

            Customer customer = new Customer(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeCustomer1));
            customer.SendSurveyInvite();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/send_survey_invite", _ctx.Url, customer.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.IsTrue(_ctx.WebRequest.MethodSetCalled);
            Assert.AreEqual(HttpVerb.Post, _ctx.WebRequest.MethodSetLastValue);
        }
    }
}
