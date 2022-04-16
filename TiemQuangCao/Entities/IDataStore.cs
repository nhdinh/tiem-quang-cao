using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemQuangCao.Entities
{
	public interface IDataStore
	{
		public IDbConnection Connection { get; }
	}
}
