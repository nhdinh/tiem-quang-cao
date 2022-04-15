using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TiemQuangCao.Entities
{
	public class Connection
	{
		private static SqlConnection _connection;

		private Connection()
		{
		}

		public static SqlConnection Instance
		{
			get
			{
				if (Connection._connection == null)
				{
					Connection._connection = new SqlConnection(AppSettings.ConnectionString);
				}

				return Connection._connection;
			}
		}

		public static List<PaymentVoucher> GetTenLatest()
		{
			using var conn = Connection.Instance;
			conn.Open();

			var vouchers = conn.Query<PaymentVoucher>("SELECT TOP 10 * FROM PaymentVoucher ORDER BY PV_Id DESC").ToList();

			return vouchers;
		}
	}
}
