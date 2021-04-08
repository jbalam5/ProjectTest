using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TestCore.BussinesLayer
{
    public class CLS_webBrowser
    {
        public WebBrowser webBrowser;
        public Boolean isLoadComplete = false;

        public CLS_webBrowser(WebBrowser webBrowser)
        {
            if (webBrowser != null)
            {
                this.webBrowser = webBrowser;
            }
            else
            {
                this.webBrowser = new WebBrowser();
                this.webBrowser.DocumentCompleted += SATWebBrowser_DocumentCompleted;
            }
        }

        public void Navigate(string url)
        {
            isLoadComplete = false;
            this.webBrowser.Navigate(url);
        }

        private void SATWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                Console.WriteLine(webBrowser.Url.ToString());

                if (webBrowser.Document == null) return;

                if (webBrowser.ReadyState != WebBrowserReadyState.Complete || e.Url.AbsolutePath != (sender as WebBrowser).Url.AbsolutePath) return;

                System.Threading.Thread.Sleep(1000);

                Application.DoEvents();

                isLoadComplete = true;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
