using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using TiemQuangCao.Properties;

namespace TiemQuangCao.Entities
{
	public class DataStore : IDataStore
	{
		private static IDataStore _instance;
		private IDbConnection _connection;

		private DataStore(string connectionString)
		{
			// check if the input connectionString is null or empty, then get the connectionString from settings
			if (string.IsNullOrEmpty(connectionString))
			{
				var settings = new Settings();
				connectionString = settings.ConnectionString;
			}


			this._connection = new SqlConnection(connectionString);
		}

		public IDbConnection Connection
		{
			get { return this._connection; }
			private set
			{
				this._connection = value;
				this._connection.Open();
			}
		}

		public static IDataStore GetStore(string connectionString="")
		{
			if (_instance == null)
				_instance = new DataStore(connectionString);

			return _instance;
		}

		public static List<PaymentVoucher> GetTenLatest(IDbConnection conn)
		{
			if (conn.State != ConnectionState.Open)
				conn.Open();

			var vouchers = conn.Query<PaymentVoucher>("SELECT TOP 10 * FROM PaymentVoucher ORDER BY PV_Id DESC").ToList();

			return vouchers;
		}
	}
}
