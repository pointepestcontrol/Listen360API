using System;
using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    [TestFixture]
    public class TestFranchise
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
            Franchise franchise = new Franchise(_ctx.Listen360,
                TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            Assert.AreEqual(2, franchise.Id);
            Assert.AreEqual(1, franchise.ParentId);
            Assert.AreEqual(1, franchise.RootId);
            Assert.AreEqual("Proserv", franchise.Name);
            Assert.AreEqual("Proserv of Atlanta", franchise.ExternalName);
            Assert.AreEqual("joe.franchisee@example.com", franchise.Email);
            Assert.AreEqual("franchise-1", franchise.Reference);
            Assert.AreEqual("Active", franchise.Status);
            Assert.AreEqual("1 North Point Pkwy", franchise.StreetAddress);
            Assert.AreEqual("Alpharetta", franchise.City);
            Assert.AreEqual("GA", franchise.Region);
            Assert.AreEqual("30022", franchise.PostalCode);
            Assert.AreEqual("US", franchise.Country);
            Assert.AreEqual("Eastern Time (US & Canada)", franchise.TimeZone);
            Assert.AreEqual("678-555-1212", franchise.PhoneNumber);
            Assert.AreEqual("http://foo.bar", franchise.Website);
            Assert.AreEqual(30, franchise.MarketingRadiusInMiles);
            Assert.AreEqual(50, franchise.OperatingRadiusInMiles);
            Assert.AreEqual(new DateTime(2008, 01, 01, 17, 59, 50), franchise.CreatedAt.Value.ToUniversalTime());
        }

        [Test]
        public void CreatePathIncludesParentId()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, 1);
            Assert.AreEqual("organizations?parent_id=1", franchise.CreatePath);
        }

        [Test]
        public void HierarchyOperationsNotImplemented()
        {
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(TryFindChildByReference));
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(TryGetChildren));
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(TryGetChildrenWithPage));
        }

        protected void TryFindChildByReference()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, 1);
            franchise.FindChildByReference("REF1");
        }

        protected void TryGetChildren()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, 1);
            franchise.GetChildren();
        }

        protected void TryGetChildrenWithPage()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, 1);
            franchise.GetChildren(1);
        }

        [Test]
        public void GetCustomerById()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));

            TestUtil.SetupResponseXml(_ctx.WebRequest, Properties.Resources.FakeCustomer1);
            ICustomer customer = franchise.GetCustomerById(3);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/customers/3", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(customer);
        }

        [Test]
        public void FindCustomerByReference()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));

            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<customers type=\"array\">{0}</customers>", Properties.Resources.FakeCustomer1));
            ICustomer customer = franchise.FindCustomerByReference("ABC123");

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/customers?reference=ABC123", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(customer);
        }

        [Test]
        public void FindCustomerByReferenceNotFound()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, "<customers type=\"array\"></customers>");
            ICustomer customer = franchise.FindCustomerByReference("ABC123");
            Assert.Null(customer);
        }

        [Test]
        public void GetCustomers()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<customers type=\"array\">{0}</customers>", Properties.Resources.FakeCustomer1));
            ICustomer[] customers = franchise.GetCustomers();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/customers?page=1", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, customers.Length);
            Assert.AreEqual(3, customers[0].Id);
        }

        [Test]
        public void GetCustomersPaginated()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<customers type=\"array\">{0}</customers>", Properties.Resources.FakeCustomer1));
            ICustomer[] customers = franchise.GetCustomers(2);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/customers?page=2", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, customers.Length);
            Assert.AreEqual(3, customers[0].Id);
        }

        [Test]
        public void GetTechnicianById()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));

            TestUtil.SetupResponseXml(_ctx.WebRequest, Properties.Resources.FakeTechnician1);
            ITechnician tech = franchise.GetTechnicianById(3);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/technicians/3", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(tech);
        }

        [Test]
        public void FindTechnicianByReference()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));

            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<technicians type=\"array\">{0}</technicians>", Properties.Resources.FakeTechnician1));
            ITechnician tech = franchise.FindTechnicianByReference("ABC123");

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/technicians?reference=ABC123", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(tech);
        }

        [Test]
        public void FindTechnicianByReferenceNotFound()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeTechnician1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, "<technicians type=\"array\"></technicians>");
            ITechnician tech = franchise.FindTechnicianByReference("ABC123");
            Assert.Null(tech);
        }

        [Test]
        public void GetTechnicians()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<technicians type=\"array\">{0}</technicians>", Properties.Resources.FakeTechnician1));
            ITechnician[] techs = franchise.GetTechnicians();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/technicians?page=1", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, techs.Length);
            Assert.AreEqual(15, techs[0].Id);
        }

        [Test]
        public void GetTechniciansPaginated()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<technicians type=\"array\">{0}</technicians>", Properties.Resources.FakeTechnician1));
            ITechnician[] techs = franchise.GetTechnicians(2);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/technicians?page=2", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, techs.Length);
            Assert.AreEqual(15, techs[0].Id);
        }

        [Test]
        public void GetJobById()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));

            TestUtil.SetupResponseXml(_ctx.WebRequest, Properties.Resources.FakeJob1);
            IJob job = franchise.GetJobById(3);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/jobs/3", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(job);
        }

        [Test]
        public void FindJobByReference()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));

            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<jobs type=\"array\">{0}</jobs>", Properties.Resources.FakeJob1));
            IJob job = franchise.FindJobByReference("ABC123");

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/jobs?reference=ABC123", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.NotNull(job);
        }

        [Test]
        public void FindJobByReferenceNotFound()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeJob1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, "<jobs type=\"array\"></jobs>");
            IJob job = franchise.FindJobByReference("ABC123");
            Assert.Null(job);
        }

        [Test]
        public void GetJobs()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<jobs type=\"array\">{0}</jobs>", Properties.Resources.FakeJob1));
            IJob[] jobs = franchise.GetJobs();

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/jobs?page=1", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, jobs.Length);
            Assert.AreEqual(234, jobs[0].Id);
        }

        [Test]
        public void GetJobsPaginated()
        {
            Franchise franchise = new Franchise(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeFranchise1));
            TestUtil.SetupResponseXml(_ctx.WebRequest, string.Format("<jobs type=\"array\">{0}</jobs>", Properties.Resources.FakeJob1));
            IJob[] jobs = franchise.GetJobs(2);

            Assert.IsTrue(_ctx.WebRequestFactory.CreateWebRequestCalled);
            Assert.AreEqual(string.Format("{0}/{1}/jobs?page=2", _ctx.Url, franchise.Path), _ctx.WebRequestFactory.CreateWebRequestLastUrl);
            Assert.AreEqual(1, jobs.Length);
            Assert.AreEqual(234, jobs[0].Id);
        }
    }
}
