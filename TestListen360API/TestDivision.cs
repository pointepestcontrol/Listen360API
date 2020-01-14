using System;
using NUnit.Framework;
using Listen360API;


namespace TestListen360API
{
    [TestFixture]
    class TestDivision
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
            Division division = new Division(_ctx.Listen360,
                TestUtil.GetElementForXml(Properties.Resources.FakeDivision1));
            Assert.AreEqual(2, division.Id);
            Assert.AreEqual(1, division.ParentId);
            Assert.AreEqual("Northern Region", division.Name);
            Assert.AreEqual("Northern Region", division.ExternalName);
        }

        [Test]
        public void CreatePathIncludesParentId()
        {
            Division division = new Division(_ctx.Listen360, 1);
            Assert.AreEqual("organizations?parent_id=1", division.CreatePath);
        }
    }
}
