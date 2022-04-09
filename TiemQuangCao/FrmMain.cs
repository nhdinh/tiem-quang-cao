using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
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
            var latest = releases[0];

            var updateXml = latest.Assets.Where(x => x.Name == "update.xml").FirstOrDefault();
            var updateXmlDownloadUrl = updateXml.BrowserDownloadUrl;

            AutoUpdater.Start(updateXmlDownloadUrl);

        }
    }
}
