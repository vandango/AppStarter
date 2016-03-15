using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppStarter
{
	public partial class MainForm
	{
		private void _hotkeyShow_Pressed(object sender, EventArgs e)
		{
			//if (!this.Visible)

			//{
			//	this.Show();
			//}
			Show();
			FocusTextbox();
		}

		private void _hotkeyHide_Pressed(object sender, EventArgs e)
		{
			//if (this.Visible)
			//{
			//	this.Hide();
			//}
			Hide();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_hotkeyHide.Pressed -= _hotkeyHide_Pressed;
			_hotkeyShow.Pressed -= _hotkeyShow_Pressed;
			Hide();
			e.Cancel = !_doClosing;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			//this.Close();
			Hide();
		}

		private void btnConfig_Click(object sender, EventArgs e)
		{
			var cfg = new ConfigForm();

			if (cfg.ShowDialog(this) == DialogResult.OK)
			{
				Config = new Config();

				Config.ApplicationIndex = null;

				var thread = new Thread(new ThreadStart(InitApplicationIndexAsync));
				thread.Start();

				//this.InitApplicationIndex();
				//this.InitApplications();
			}

			Show();
			FocusTextbox();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			if (txtSearch.Text.Length > 0
			    && !txtSearch.Text.StartsWith("#"))
			{
				searchTimer.Enabled = false;
				searchTimer.Stop();

				if (txtSearch.Text.Length > 0)
				{
					if (txtSearch.Text.Length == 1)
					{
						searchTimer.Interval = 300;
					}
					else if (txtSearch.Text.Length == 2)
					{
						searchTimer.Interval = 150;
					}
					else if (txtSearch.Text.Length == 3)
					{
						searchTimer.Interval = 50;
					}
					else if (txtSearch.Text.Length > 3)
					{
						searchTimer.Interval = 12;
					}
				}

				searchTimer.Enabled = true;
				searchTimer.Start();
			}
		}

		private void searchTimer_Tick(object sender, EventArgs e)
		{
			var thread = new Thread(new ThreadStart(InitApplications));
			thread.Start();

			searchTimer.Enabled = false;
			searchTimer.Stop();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape)
			{
				Hide();
			}
		}

		private void txtSearch_Leave(object sender, EventArgs e)
		{
			FocusTextbox();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape)
			{
				Hide();
			}

			if (e.KeyData == Keys.Enter
				|| e.KeyData == Keys.Return)
			{
				if (txtSearch.Text.Length > 0
				    && txtSearch.Text.StartsWith("#"))
				{
					try
					{
						AppLoader.LoadAppInThread(txtSearch.Text.Substring(1));
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "ERROR");
					}
				}
				else
				{
					if (lvApps.Items.Count > 0)
					{
						string file = lvApps.Items[0].Tag.ToString();
						Hide();
						AppLoader.LoadAppInThread(file);
					}
				}
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			//this._hotkeyHide.Pressed += _hotkeyHide_Pressed;

			FocusTextbox();

			var thread = new Thread(new ThreadStart(InitApplicationIndexAsync));
			thread.Start();
		}

		private void lvApps_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter
				|| e.KeyData == Keys.Return)
			{
				if (lvApps.Items.Count > 0)
				{
					string file = lvApps.Items[0].Tag.ToString();
					Hide();
					AppLoader.LoadAppInThread(file);
				}
			}
		}

		private void lvApps_DoubleClick(object sender, EventArgs e)
		{
			if (lvApps.SelectedItems.Count > 0)
			{
				string file = lvApps.SelectedItems[0].Tag.ToString();
				AppLoader.LoadAppInThread(file);
			}
		}

		private void btnReloadIndex_Click(object sender, EventArgs e)
		{
			Config.ApplicationIndex = null;

			var thread = new Thread(new ThreadStart(InitApplicationIndexAsync));
			thread.Start();
		}

		private void MainForm_Leave(object sender, EventArgs e)
		{
			Hide();
		}

		private void MainForm_Deactivate(object sender, EventArgs e)
		{
			Hide();
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			ExitApp();
		}

		private void btnList_Click(object sender, EventArgs e)
		{
			var list = new ApplicationListForm(Config);

			if (list.ShowDialog(this) == DialogResult.OK)
			{
				Config.ApplicationIndex = list.Config.ApplicationIndex;

				if (Config.ApplicationIndex != null
				&& Config.ApplicationIndex.Count > 0)
				{
					File.WriteAllLines(Config.IndexFile, Config.ApplicationIndex.ToArray());
				}
			}

			Show();
			FocusTextbox();
		}
	}
}
