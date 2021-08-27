using Gecko;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RaspagemAbrasel
{
	public partial class Form1 : Form
	{
		public List<string> ListaLinks { get; set; }
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Xpcom.Initialize("Firefox");
			var geckoWebBrowser = new GeckoWebBrowser { Dock = DockStyle.Fill };
			PnGeckoFx.Controls.Add(geckoWebBrowser);
			geckoWebBrowser.Navigate("https://abrasel.com.br/");
			geckoWebBrowser.DocumentCompleted += GeckoWebBrowser_DocumentCompleted;
		}

		private void GeckoWebBrowser_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
		{
			MessageBox.Show("Fim!");
		}
	}
}
