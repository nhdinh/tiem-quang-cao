using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiemQuangCao;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiemQuangCao.Tests
{
    [TestClass()]
    public class FrmMainTests
    {
        [TestMethod()]
        public void FrmMainTest()
        {
            var frm = new FrmMain();
            Assert.AreEqual(frm.Name, "FrmMain");
            Assert.AreEqual(frm.Text, "Main");
        }
    }
}