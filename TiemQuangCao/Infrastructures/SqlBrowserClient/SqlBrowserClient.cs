using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TiemQuangCao.Infrastructures.SqlBrowserClient;

namespace TiemQuangCao.Infrastructure.SqlBrowserClient
{
	public class SqlBrowserClient : ISqlBrowserClient
	{
		private static readonly int SQL_BROWSER_PORT = 1434;

		private readonly byte[] _getInstancesMessage = new byte[1] { 2 };

		public List<SqlServerInstance> Instances { get; private set; }


		public void ScanInstances()
		{
			using var client = new UdpBroadcastMessage(SQL_BROWSER_PORT, _getInstancesMessage, new TimeSpan(0, 0, 5));

			var responses = client.GetResponse();
			Instances = ParseBrowserResponses(responses);
		}

		private List<SqlServerInstance> ParseBrowserResponses(List<string> responses)
		{
			var parserRegex =
				new Regex(
					@"[^;]*ServerName;(?<ServerName>[\w\d]+);InstanceName;(?<InstanceName>[\w\d]+);IsClustered;(?<IsClustered>[\w]+);Version;(?<Version>[\d]+\.[\d]+\.[\d]+\.[\d]+)(;tcp;(?<tcp>\d+))?(;np;(?<np>[^;]+))?;;");
			var instances = new List<SqlServerInstance>();
			return responses
				.SelectMany(s => parserRegex.Matches(s))
				.Select(MapFromMatch)
				.ToList();
		}

		private SqlServerInstance MapFromMatch(Match match) => new SqlServerInstance(match.Groups["ServerName"].Captures[0].Value,
				match.Groups["InstanceName"].Captures[0].Value,
				match.Groups["IsClustered"].Captures[0].Value != "No",
				match.Groups["Version"].Captures[0].Value,
				match.Groups["tcp"].Captures.Count > 0 ? int.Parse(match.Groups["tcp"].Captures[0].Value) : 0,
				match.Groups["np"].Captures.Count > 0 ? match.Groups["np"].Captures[0].Value : string.Empty);
	}
}

