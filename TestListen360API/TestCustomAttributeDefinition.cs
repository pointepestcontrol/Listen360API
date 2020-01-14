using System;
using System.Text;

using NUnit.Framework;
using Listen360API;

namespace TestListen360API
{
    [TestFixture]
    class TestCustomAttributeDefinition
    {
        TestUtil.TestContext _ctx;
        CustomAttributeDefinition _attrDef;

        [SetUp]
        public void SetUp()
        {
            _ctx = new TestUtil.TestContext();
            _attrDef = new CustomAttributeDefinition(_ctx.Listen360, TestUtil.GetElementForXml(Properties.Resources.FakeCustomAttributeDefinition1));
        }

        [Test]
        public void Construct()
        {
            Assert.AreEqual(1000, _attrDef.OrganizationId);
            Assert.AreEqual("Source", _attrDef.Name);
            Assert.AreEqual("Job", _attrDef.AttachedTo);
            Assert.AreEqual(1, _attrDef.Ordinal);
            Assert.IsFalse(_attrDef.IsRestrictedToPredefinedChoices);
            Assert.IsFalse(_attrDef.IsMandatory);
        }

        [Test]
        public void CannotSave()
        {
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(TrySave));
        }

        public void TrySave()
        {
            _attrDef.Save();
        }

        [Test]
        public void CannotReload()
        {
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(TryReload));
        }

        public void TryReload()
        {
            _attrDef.Reload();
        }

        [Test]
        public void CannotDelete()
        {
            Assert.Throws(typeof(NotSupportedException), new TestDelegate(TryDelete));
        }

        public void TryDelete()
        {
            _attrDef.Delete();
        }
    }
}
