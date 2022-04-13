using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using System.Xml;
using AutoUpdaterDotNET;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace TiemQuangCao
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            bool autoUpdate = true;
            if (autoUpdate)
            {
                this.tryGetUpdateXml();
            }
        }

        private async void tryGetUpdateXml()
        {
            var client = new GitHubClient(new ProductHeaderValue("tiem-quang-cao"));
            var releases = await client.Repository.Release.GetAll("nhdinh", "tiem-quang-cao");

            // stop the process if cannot get release information from github
            if (releases == null)
                return;

            if (releases.Count > 0)
            {
                var latestRelease = releases[0];

                // get remote version
                var latestVersion = Helpers.GithubTagToVersion(latestRelease.TagName);
                var productVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                string downloadFileName = string.Format("TiemQuangCao-{0}-{1}-Release", latestRelease.TagName, Helpers.GetRuntimeIdentifier());
                var downloadAsset = latestRelease.Assets.Where(a => a.Name.StartsWith(downloadFileName)).First();

                if (latestVersion != null && downloadAsset != null)
                {
                    // create xml file for updating
                    string xmlFilePath = Path.Join(Path.GetTempPath(), "update.xml");
                    using (XmlWriter writer = XmlWriter.Create(xmlFilePath))
                    {

                        writer.WriteStartElement("item");
                        //writer.WriteElementString("version", latestVersion.ToString());
                        writer.WriteElementString("version", "1.0.0");
                        writer.WriteElementString("url", downloadAsset.BrowserDownloadUrl);
                        writer.WriteElementString("mandator", "true");
                    }

                    AutoUpdater.Start(xmlFilePath);
                }
            }

        }
    }
}
