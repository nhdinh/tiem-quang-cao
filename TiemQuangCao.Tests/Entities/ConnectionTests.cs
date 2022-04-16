using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Dapper;
using TiemQuangCao.Entities;

namespace TiemQuangCao.Tests.Entities
{
	[TestClass()]
	public class ConnectionTests
	{
		[TestMethod()]
		public void GetConnectionTest()
		{
			IDataStore store = DataStore.GetStore();
			IDbConnection conn = store.Connection;
			Assert.IsNotNull(conn);
		}

		[TestMethod()]
		public void GetTenLatestTest()
		{
			var expected = new List<PaymentVoucher>();
			var connection = new Mock<IDbConnection>();
			connection.SetupDapper(c => c.Query<PaymentVoucher>(It.IsAny<string>(), null, null, true, null, null)).Returns(expected);

			var vouchers = DataStore.GetTenLatest(connection.Object);
			Assert.AreEqual(vouchers.Count, expected.Count);
		}
	}
}
