using Microsoft.VisualStudio.TestTools.UnitTesting;

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
