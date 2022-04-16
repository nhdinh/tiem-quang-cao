﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Octokit;
using TiemQuangCao.Properties;
using Application = System.Windows.Forms.Application;

namespace TiemQuangCao
{
	internal static class Program
	{

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool isDebug = false;
#if DEBUG
			isDebug = true;
#endif

			// since debugging source always be the latest version, therefore no need to check for update in debug mode
			bool autoUpdate = true && !isDebug;
			if (autoUpdate)
			{
				TryUpdateApplication();
			}

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// check for ConnectionString
			var appSettings = new Settings();
			if (string.IsNullOrEmpty(appSettings.ConnectionString) || !Helpers.TestConnectionString(appSettings.ConnectionString))
				Application.Run(new FrmBrowseDatabase());
			else Application.Run(new FrmMain());
		}

		/// <summary>
		/// Try getting update information from github releases
		/// </summary>
		static async void TryUpdateApplication()
		{
			var client = new GitHubClient(new ProductHeaderValue(BuildConstants.GITHUB_REPOSITORY));
			var releases = await client.Repository.Release.GetAll(BuildConstants.GITHUB_USERNAME, BuildConstants.GITHUB_REPOSITORY);

			// stop the process if cannot get release information from github
			if (releases == null)
				return;

			if (releases.Count > 0)
			{
				var latestRelease = releases[0];

				// get remote version
				var latestVersion = Helpers.GithubTagToVersion(latestRelease.TagName);
				var productVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

				string downloadFileName = string.Format(BuildConstants.GITHUB_ASSET_NAME, latestRelease.TagName, Helpers.GetRuntimeIdentifier());

				// if there is only source files in release, then stop the updater
				if (latestRelease.Assets.Count <= 2) return;

				var downloadAsset = latestRelease.Assets.Where(a => a.Name.StartsWith(downloadFileName)).First();

				// if both latestVersion and downloadAsset are not null
				if (latestVersion != null && downloadAsset != null)
				{
					// create xml file for updating
					string appCastFilePath = Path.Join(Path.GetTempPath(), "update.xml");
					Helpers.MakeAppCastFile(latestVersion, downloadAsset.BrowserDownloadUrl, appCastFilePath);

					AutoUpdater.Start(appCastFilePath);
				}
			}

		}
	}
}
