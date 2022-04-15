using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Xml;
using TiemQuangCao.Infrastructures.SqlBrowserClient;

namespace TiemQuangCao
{
	public class Helpers
	{
		private static readonly int SQL_BROWSER_PORT = 1434;

		/// <summary>
		/// Convert github tag in string format to Version instance.
		/// Github tag has format of v0.0.1. This is the release tag of the repository.
		/// This function will parse the tag and create an instance of Version with those version number.
		/// 
		/// If input tag is null or empty or not in specified string, return null
		/// </summary>
		/// <param name="tagName">a github tag string in format v*.*.*</param>
		/// <returns>instance of Version</returns>
		public static Version GithubTagToVersion(string tagName)
		{
			if (string.IsNullOrWhiteSpace(tagName)) return null;

			string strRegex = @"^v([0-9]+)\.([0-9]+)\.([0-9]+)((\.[0-9]+)?)$";
			var regexMatch = new Regex(strRegex);
			if (regexMatch.IsMatch(tagName))
			{
				tagName = tagName.Replace("v", "");
				int[] versions = tagName.Split(".").Select(v => int.Parse(v)).ToArray();

				if (versions.Length == 2)
					return new Version(versions[0], versions[1], 0);
				else if (versions.Length == 3)
					return new Version(versions[0], versions[1], versions[2]);
				else if (versions.Length == 4)
					return new Version(versions[0], versions[1], versions[2], versions[3]);
				else return null;
			}

			return null;
		}

		/// <summary>
		/// Check if this windows service is up and running
		/// </summary>
		/// <param name="serviceName"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public static ServiceControllerStatus? IsServiceUp(string serviceName)
		{
			ServiceController serviceController = new ServiceController(serviceName);
			if (serviceController != null) {
				return serviceController.Status;
			}

			return null;
		}

		/// <summary>
		/// Using system discovery and scan for all available sqlserver servers
		/// </summary>
		/// <returns>Return list of servers</returns>
		/// <exception cref="NotImplementedException"></exception>
		public static List<SqlServerInstance> GetServers()
		{
			byte[] _getInstancesMessage = new byte[1] { 2 };
			using var client = new UdpBroadcastMessage(SQL_BROWSER_PORT, _getInstancesMessage, new TimeSpan(0, 0, 5));

			var responses = client.GetResponse();

			var parserRegex =
				new Regex(
					@"[^;]*ServerName;(?<ServerName>[\w\d]+);InstanceName;(?<InstanceName>[\w\d]+);IsClustered;(?<IsClustered>[\w]+);Version;(?<Version>[\d]+\.[\d]+\.[\d]+\.[\d]+)(;tcp;(?<tcp>\d+))?(;np;(?<np>[^;]+))?;;");
			var instances = new List<SqlServerInstance>();

			Func<Match, SqlServerInstance> mapFromMatchFunc = match => new SqlServerInstance(match.Groups["ServerName"].Captures[0].Value,
				match.Groups["InstanceName"].Captures[0].Value,
				match.Groups["IsClustered"].Captures[0].Value != "No",
				match.Groups["Version"].Captures[0].Value,
				match.Groups["tcp"].Captures.Count > 0 ? int.Parse(match.Groups["tcp"].Captures[0].Value) : 0,
				match.Groups["np"].Captures.Count > 0 ? match.Groups["np"].Captures[0].Value : string.Empty);

			return responses
				.SelectMany(s => parserRegex.Matches(s))
				.Select(mapFromMatchFunc)
				.ToList();
		}

		public static void ListServers(DbProviderFactory factory)
		{
			// This procedure is provider-agnostic, and can list
			// instances of any provider's servers. Of course, 
			// not all providers can create a data source enumerator,
			// so it's best to check the CanCreateDataSourceEnumerator 
			// property before attempting to list the data sources.
			if (factory.CanCreateDataSourceEnumerator)
			{
				DbDataSourceEnumerator instance = factory.CreateDataSourceEnumerator();
				DataTable table = instance.GetDataSources();

				foreach (DataRow row in table.Rows)
				{
					Console.WriteLine("{0}\\{1}",
						row["ServerName"], row["InstanceName"]);
				}
			}
		}

		/// <summary>
		/// Make a connection test to specified database
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns>Success (true) else False</returns>
		public static bool TestConnectionString(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
				return false;

			try
			{
				using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
				{
					conn.Open();
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Get RuntimeIdentifier of the current process
		/// </summary>
		/// <returns>a string represent for RuntimeIdentifier</returns>
		public static string GetRuntimeIdentifier()
		{
			string os = "";
			if (OperatingSystem.IsWindows()) os = "win";
			if (RuntimeInformation.OSArchitecture == Architecture.X64)
				return os + "-x64";
			else if (RuntimeInformation.OSArchitecture == Architecture.X86)
				return os + "-x86";

			return "unknown";
		}

		/// <summary>
		/// Make appCast file for AutoUpdater.NET and save it to somewhere specified by appCastFilePath.
		/// The format of appCast is XML, and contains needed information for AutoUpdater.NET to update the application.
		/// </summary>
		/// <param name="latestVersion">the latest remote version</param>
		/// <param name="downloadUrl">asset download URL</param>
		/// <param name="appCastFilePath">appCast file location</param>
		public static void MakeAppCastFile(Version latestVersion, string downloadUrl, string appCastFilePath)
		{
			using XmlWriter writer = XmlWriter.Create(appCastFilePath);

			writer.WriteStartElement("item");
			writer.WriteElementString("version", latestVersion.ToString());
			writer.WriteElementString("url", downloadUrl);
			writer.WriteElementString("mandator", "true");
		}
	}
}
