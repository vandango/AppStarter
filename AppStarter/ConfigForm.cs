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
	public partial class ConfigForm : Form
	{
		internal Config Config { get; set; }

		public ConfigForm()
		{
			InitializeComponent();

			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

			this.Config = new Config();

			// ApplicationPaths
			this.Config.ApplicationPaths.ForEach(path =>
			{
				this.txtAppPaths.Text += path;
				this.txtAppPaths.Text += Environment.NewLine;
			});

			// ExcludePaths
			this.Config.ExcludePaths.ForEach(path =>
			{
				this.txtExcludePaths.Text += path;
				this.txtExcludePaths.Text += Environment.NewLine;
			});

			// FileTypes
			this.Config.FileTypes.ForEach(path =>
			{
				this.txtFiletypes.Text += path;
				this.txtFiletypes.Text += Environment.NewLine;
			});

			this.chkRecursiveDirectoryScan.Checked = this.Config.RecursiveDirectoryScan;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			// ApplicationPaths
			string buffer = this.txtAppPaths.Text;
			buffer = buffer.Replace(";", Environment.NewLine);
			string[] paths = buffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			this.Config.ApplicationPaths.Clear();
			foreach (string path in paths)
			{
				this.Config.ApplicationPaths.Add(path);
			}

			// ExcludePaths
			string bufferExclude = this.txtExcludePaths.Text;
			bufferExclude = bufferExclude.Replace(";", Environment.NewLine);
			string[] excludePaths = bufferExclude.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			this.Config.ExcludePaths.Clear();
			foreach (string path in excludePaths)
			{
				this.Config.ExcludePaths.Add(path);
			}

			// ExcludePaths
			string bufferFileTypes = this.txtFiletypes.Text;
			bufferFileTypes = bufferFileTypes.Replace(";", Environment.NewLine);
			string[] fileTypes = bufferFileTypes.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			this.Config.FileTypes.Clear();
			foreach (string fileType in fileTypes)
			{
				this.Config.FileTypes.Add(fileType);
			}

			this.Config.RecursiveDirectoryScan = this.chkRecursiveDirectoryScan.Checked;

			this.Config.Save();
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
}
