namespace AppStarter
{
	partial class MainForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.lvApps = new System.Windows.Forms.ListView();
			this.colApp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnConfig = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.searchTimer = new System.Windows.Forms.Timer(this.components);
			this.btnReloadIndex = new System.Windows.Forms.Button();
			this.btnQuit = new System.Windows.Forms.Button();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.btnList = new System.Windows.Forms.Button();
			this.firstApp = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblIndexLoadInfo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.mainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.firstApp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// lvApps
			// 
			this.lvApps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colApp});
			this.lvApps.HideSelection = false;
			this.lvApps.Location = new System.Drawing.Point(8, 56);
			this.lvApps.MultiSelect = false;
			this.lvApps.Name = "lvApps";
			this.lvApps.Size = new System.Drawing.Size(486, 55);
			this.lvApps.TabIndex = 0;
			this.lvApps.UseCompatibleStateImageBehavior = false;
			this.lvApps.View = System.Windows.Forms.View.SmallIcon;
			this.lvApps.DoubleClick += new System.EventHandler(this.lvApps_DoubleClick);
			this.lvApps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvApps_KeyDown);
			// 
			// colApp
			// 
			this.colApp.Text = "App";
			this.colApp.Width = 250;
			// 
			// btnConfig
			// 
			this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConfig.Location = new System.Drawing.Point(8, 117);
			this.btnConfig.Name = "btnConfig";
			this.btnConfig.Size = new System.Drawing.Size(48, 23);
			this.btnConfig.TabIndex = 1;
			this.btnConfig.Text = "Config";
			this.btnConfig.UseVisualStyleBackColor = true;
			this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(446, 117);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(48, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Hide";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(8, 11);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(412, 28);
			this.txtSearch.TabIndex = 3;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
			// 
			// searchTimer
			// 
			this.searchTimer.Tick += new System.EventHandler(this.searchTimer_Tick);
			// 
			// btnReloadIndex
			// 
			this.btnReloadIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReloadIndex.Location = new System.Drawing.Point(62, 117);
			this.btnReloadIndex.Name = "btnReloadIndex";
			this.btnReloadIndex.Size = new System.Drawing.Size(48, 23);
			this.btnReloadIndex.TabIndex = 4;
			this.btnReloadIndex.Text = "Index";
			this.btnReloadIndex.UseVisualStyleBackColor = true;
			this.btnReloadIndex.Click += new System.EventHandler(this.btnReloadIndex_Click);
			// 
			// btnQuit
			// 
			this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnQuit.Location = new System.Drawing.Point(392, 117);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(48, 23);
			this.btnQuit.TabIndex = 5;
			this.btnQuit.Text = "Quit";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.BackColor = System.Drawing.Color.White;
			this.mainPanel.Controls.Add(this.btnList);
			this.mainPanel.Controls.Add(this.firstApp);
			this.mainPanel.Controls.Add(this.pictureBox1);
			this.mainPanel.Controls.Add(this.lblIndexLoadInfo);
			this.mainPanel.Controls.Add(this.label1);
			this.mainPanel.Controls.Add(this.btnQuit);
			this.mainPanel.Controls.Add(this.txtSearch);
			this.mainPanel.Controls.Add(this.btnReloadIndex);
			this.mainPanel.Controls.Add(this.lvApps);
			this.mainPanel.Controls.Add(this.btnConfig);
			this.mainPanel.Controls.Add(this.btnClose);
			this.mainPanel.Location = new System.Drawing.Point(1, 1);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(503, 47);
			this.mainPanel.TabIndex = 6;
			this.mainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseDown);
			this.mainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseMove);
			this.mainPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseUp);
			// 
			// btnList
			// 
			this.btnList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnList.Location = new System.Drawing.Point(116, 117);
			this.btnList.Name = "btnList";
			this.btnList.Size = new System.Drawing.Size(32, 23);
			this.btnList.TabIndex = 10;
			this.btnList.Text = "List";
			this.btnList.UseVisualStyleBackColor = true;
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			// 
			// firstApp
			// 
			this.firstApp.Location = new System.Drawing.Point(426, 9);
			this.firstApp.Name = "firstApp";
			this.firstApp.Size = new System.Drawing.Size(32, 32);
			this.firstApp.TabIndex = 9;
			this.firstApp.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Image = global::AppStarter.Properties.Resources.ic_keyboard_arrow_down_32px;
			this.pictureBox1.Location = new System.Drawing.Point(463, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(31, 31);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// lblIndexLoadInfo
			// 
			this.lblIndexLoadInfo.AutoSize = true;
			this.lblIndexLoadInfo.Location = new System.Drawing.Point(116, 122);
			this.lblIndexLoadInfo.Name = "lblIndexLoadInfo";
			this.lblIndexLoadInfo.Size = new System.Drawing.Size(0, 13);
			this.lblIndexLoadInfo.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(165, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(173, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Windows + A opens the AppStarter";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(505, 49);
			this.Controls.Add(this.mainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(146, 49);
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AppStarter";
			this.TopMost = true;
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Leave += new System.EventHandler(this.MainForm_Leave);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.firstApp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvApps;
		private System.Windows.Forms.Button btnConfig;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ColumnHeader colApp;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Timer searchTimer;
		private System.Windows.Forms.Button btnReloadIndex;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblIndexLoadInfo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox firstApp;
		private System.Windows.Forms.Button btnList;
	}
}

