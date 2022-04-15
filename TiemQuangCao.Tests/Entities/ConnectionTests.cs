using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiemQuangCao.Entities;

namespace TiemQuangCao.Tests.Entities
{
	[TestClass()]
	public class ConnectionTests
	{
		[TestMethod()]
		public void GetConnectionTest()
		{
			SqlConnection conn = Connection.Instance;
			Assert.IsNotNull(conn);

			try
			{
				conn.Open();
			}
			finally
			{
				Assert.IsTrue(conn.State == System.Data.ConnectionState.Open);
				conn.Close();
			}
		}

		[TestMethod()]
		public void GetTenLatestTest()
		{
			var vouchers = Connection.GetTenLatest();
			Assert.AreEqual(vouchers.Count, 10);
		}
	}
}
