using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiemQuangCao.Infrastructures.SqlBrowserClient
{
	public record SqlServerInstance(string ServerName, string InstanceName, bool IsClustered, string Version, int TcpPort, string NamedPipe);
}
