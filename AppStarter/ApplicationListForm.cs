using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppStarter
{
	internal partial class ApplicationListForm : Form
	{
		public Config Config { get; set; }

		public ApplicationListForm(Config config)
		{
			InitializeComponent();

			Config = config;

			// ApplicationPaths
			this.Config.ApplicationIndex.ForEach(path =>
			{
				this.txtAppPaths.Text += path;
				this.txtAppPaths.Text += Environment.NewLine;
			});

			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			string buffer = this.txtAppPaths.Text;
			buffer = buffer.Replace(";", Environment.NewLine);
			string[] paths = buffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			this.Config.ApplicationIndex.Clear();
			foreach (string path in paths)
			{
				this.Config.ApplicationIndex.Add(path);
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
}
