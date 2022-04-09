using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using AutoUpdaterDotNET;

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
            string gitHubLatestReleaseUrl = "https://github.com/nhdinh/tiem-quang-cao/releases/latest";
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            string redirectedUrl = null;

            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(gitHubLatestReleaseUrl))
            using (HttpContent content = response.Content)
            {
                // ... Read the response to see if we have the redirected url
                if (response.StatusCode == System.Net.HttpStatusCode.Found)
                {
                    HttpResponseHeaders headers = response.Headers;
                    if (headers != null && headers.Location != null)
                    {
                        redirectedUrl = headers.Location.AbsoluteUri;
                    }
                }
            }

            if (redirectedUrl != null)
            {
                try
                {
                    redirectedUrl += "/update.xml";
                    using (HttpClient client = new HttpClient(handler))
                    using (HttpResponseMessage response = await client.GetAsync(redirectedUrl))
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the response
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            AutoUpdater.Start(redirectedUrl);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // do nothing
                }
            }
        }
    }
}
