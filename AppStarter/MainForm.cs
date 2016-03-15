using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppStarter
{
	public partial class MainForm : Form
	{
		private static object locker = new Object();

		internal Config Config { get; set; }
		internal List<App> Apps { get; set; }
		internal ImageList AppIconsBig { get; set; }
		internal ImageList AppIconsSmall { get; set; }

		private readonly Hotkey _hotkeyShow = new Hotkey();
		private readonly Hotkey _hotkeyHide = new Hotkey();
		
		private bool _doClosing = false;

		private NotifyIcon symbol = new NotifyIcon();
		private ContextMenuStrip cmSymbol = new ContextMenuStrip();
		private ToolStripMenuItem menuItemOpen = new ToolStripMenuItem();
		private ToolStripMenuItem menuItemClose = new ToolStripMenuItem();

		public MainForm()
		{
			InitializeComponent();

			this._hotkeyShow.Value = (Keys)524288 | (Keys)System.Enum.Parse(typeof(Keys), "A");
			this._hotkeyShow.Pressed += _hotkeyShow_Pressed;
			
			this._hotkeyHide.Value = (Keys)524288 | (Keys)System.Enum.Parse(typeof(Keys), "Q");
			this._hotkeyHide.Pressed += _hotkeyHide_Pressed;

			this.InitSymbol();

			this.searchTimer.Enabled = false;
			this.searchTimer.Stop();

			this.searchTimer.Interval = 1000;

			this.Config = new Config();

			this.FocusTextbox();
		}

		private void InitApplicationIndex()
		{
			lock (locker)
			{
				if (Config.ApplicationIndex == null
				    || Config.ApplicationIndex.Count == 0)
				{
					Cursor tmp = this.Cursor;
					this.Invoke(() => { this.Cursor = Cursors.WaitCursor; });
					this.lblIndexLoadInfo.Invoke(() => { this.lblIndexLoadInfo.Text = ""; });

					Config.ApplicationIndex = new List<string>();

					foreach (string path in this.Config.ApplicationPaths)
					{
						var file = Config.IsRecursive(path);

						Config.ApplicationIndex.AddRange(
							Directory
								.EnumerateFiles(file.Item1, "*.*", (
									file.Item2 
										? SearchOption.AllDirectories 
										: (
											Config.RecursiveDirectoryScan
												? SearchOption.AllDirectories
												: SearchOption.TopDirectoryOnly
										)
										))
								.Where(this.FileTypePassed)
								.ToList()
							);
					}

					this.lblIndexLoadInfo.Invoke(() => { this.lblIndexLoadInfo.Text = "*"; });
					this.Invoke(() => { this.Cursor = tmp; });

					Config.Save();
				}
			}
		}

		private void InitSymbol()
		{
			this.symbol.Icon = this.Icon;
			this.symbol.Visible = true;

			this.symbol.DoubleClick += (o, e) =>
			{
				this.Show();
				this.Activate();
			};

			this.menuItemOpen.Text = "Open AppStarter";
			this.menuItemOpen.Enabled = false;
			this.cmSymbol.Items.Add(this.menuItemOpen);

			this.cmSymbol.Items.Add(new ToolStripSeparator());

			this.menuItemClose.Text = "Exit";
			this.cmSymbol.Items.Add(this.menuItemClose);

			this.symbol.ContextMenuStrip = this.cmSymbol;

			this.menuItemOpen.Click += (o, e) =>
			{
				this.Show();
				this.Activate();
			};

			this.menuItemClose.Click += (o, e) =>
			{
				this.ExitApp();
			};

			this.VisibleChanged += (o, e) =>
			{
				this.menuItemOpen.Enabled = !this.Visible;
			};

			this.Resize += (o, e) =>
			{
				this.menuItemOpen.Enabled = !this.Visible;
			};

			this.LostFocus += (o, e) =>
			{
				this.Hide();
			};

			this.GotFocus += (o, e) =>
			{
				this.Show();
				this.Activate();
			};
		}

		private void InitApplications()
		{
			string search = this.txtSearch.Text;

			this.Apps = new List<App>();

			this.AppIconsBig = new ImageList
			{
				ColorDepth = ColorDepth.Depth32Bit, 
				ImageSize = new Size(32, 32)
			};

			//this.AppIconsSmall = new ImageList
			//{
			//	ColorDepth = ColorDepth.Depth32Bit,
			//	ImageSize = new Size(16, 16)
			//};

			this.InitApplicationIndex();

			// load apps
			List<string> files = Config.ApplicationIndex
				.Where(file => file != null && Path.GetFileNameWithoutExtension(file).ToLower().Contains(search))
				.ToList();

			// order apps
			var unique = files
				.Where(file => Path.GetFileNameWithoutExtension(file).ToLower() == search)
				.ToList();
			var near = files
				.Where(file => Path.GetFileNameWithoutExtension(file).ToLower().StartsWith(search))
				.OrderBy(file => file.Length)
				.ToList();
			var could = files
				.Except(unique)
				.Except(near)
				.ToList();

			files = new List<string>();
			files.AddRange(unique);
			files.AddRange(near);
			files.AddRange(could);

			foreach (var file in files)
			{
				var fileInfo = new FileInfo(file);

				if (!IsExcluded(fileInfo.FullName))
				{
					this.Apps.Add(new App()
					{
						Name = fileInfo.Name,
						Path = fileInfo.FullName
					});
				}
			}

			bool showIcon = false;

			this.lvApps.Invoke(() =>
			{
				this.lvApps.Items.Clear();

				this.AppIconsBig.Images.Clear();
				//this.AppIconsSmall.Images.Clear();

				this.lvApps.LargeImageList = this.AppIconsBig;
				//this.lvApps.SmallImageList = this.AppIconsSmall;

				this.AppIconsBig.Images.Add("null", Resource._null);
				//this.AppIconsSmall.Images.Add("null", Resource._null);
			});

			// 5 or less: show icon
			if (this.Apps.Count < 5)
			{
				// load apps into listview
				// also add icons to imagelist
				showIcon = true;

				this.lvApps.Invoke(() =>
				{
					this.lvApps.Items.Clear();
					this.lvApps.View = View.LargeIcon;
				});
			}
			// more then 5: show list
			else
			{
				this.lvApps.Invoke(() =>
				{
					this.lvApps.Items.Clear();
					this.lvApps.View = View.List;
				});
			}

			if (this.Apps.Count > 0)
			{
				var firstFoundApp = this.Apps[0];
				string imageKey = null;
				
				this.firstApp.Invoke(() =>
				{
					Icon iconBig = IconExtract.GetIcon(firstFoundApp.Path, false);

					if (iconBig != null)
					{
						firstApp.Image = iconBig.ToBitmap();
					}
				});
			}

			foreach (var app in this.Apps)
			{
				string imageKey = null;

				if (showIcon)
				{
					Icon iconBig = IconExtract.GetIcon(app.Path, false);
					if (iconBig != null)
					{
						this.lvApps.Invoke(() =>
						{
							this.AppIconsBig.Images.Add(app.Name, iconBig);
						});
						imageKey = app.Name;
					}

					//Icon iconSmall = IconExtract.GetIcon(app.Path, true);
					//if (iconSmall != null)
					//{
					//	this.lvApps.Invoke(() =>
					//	{
					//		this.AppIconsSmall.Images.Add(app.Name, iconSmall);
					//	});
					//	imageKey = app.Name;
					//}
				}

				this.lvApps.Invoke(() =>
				{
					this.lvApps.Items.Add(
						new ListViewItem
						{
							Name = app.Name,
							Text = Path.GetFileNameWithoutExtension(app.Name),
							Tag = app.Path,
							ImageKey = imageKey
						});
				});
			}
		}

		private bool FileTypePassed(string filename)
		{
			string ext = Path.GetExtension(filename);

			return this.Config.FileTypes.Any(type => !string.IsNullOrWhiteSpace(ext) && type.Contains(ext));
		}

		private bool IsExcluded(string fullName)
		{
			string filePath = Path.GetDirectoryName(fullName);

			return this.Config.ExcludePaths.Any(path => filePath != null && filePath.Contains(path));
		}

		private void ExitApp()
		{
			this._doClosing = true;

			this.Config?.Dispose();

			this.symbol.Visible = false;
			this.symbol = null;

			this.Close();
			Application.Exit();
		}

		private void InitApplicationIndexAsync()
		{
			this.InitApplicationIndex();
			this.InitApplications();
		}

		private void FocusTextbox()
		{
			this.Activate();
			this.BringToFront();
			this.TopMost = true;
			this.txtSearch.Select(0, this.txtSearch.Text.Length);
			this.txtSearch.Focus();
		}
	}
}
