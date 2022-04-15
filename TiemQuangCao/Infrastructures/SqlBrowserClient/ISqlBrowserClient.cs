using System.Collections.Generic;

namespace TiemQuangCao.Infrastructures.SqlBrowserClient
{
	public interface ISqlBrowserClient
	{
		public void ScanInstances();
		public List<SqlServerInstance> Instances { get; }
	}
}
