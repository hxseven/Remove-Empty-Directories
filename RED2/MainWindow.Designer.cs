namespace RED2
{
	partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ilFolderIcons = new System.Windows.Forms.ImageList(this.components);
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.lbFastModeInfo = new System.Windows.Forms.Label();
            this.btnShowLog = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlIcons = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.pbProgressStatus = new System.Windows.Forms.ProgressBar();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.cmStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanOnlyThisDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.protectFolderFromBeingDeletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unprotectFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.proToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.lblPickAFolder = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.gbDeleteMode = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbDeleteMode = new System.Windows.Forms.ComboBox();
            this.groupBoxExplorerIntegration = new System.Windows.Forms.GroupBox();
            this.lblReqAdmin = new System.Windows.Forms.Label();
            this.btnExplorerRemove = new System.Windows.Forms.Button();
            this.btnExplorerIntegrate = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cbFastSearchMode = new System.Windows.Forms.CheckBox();
            this.cbHideScanErrors = new System.Windows.Forms.CheckBox();
            this.cbIgnoreErrors = new System.Windows.Forms.CheckBox();
            this.cbClipboardDetection = new System.Windows.Forms.CheckBox();
            this.cbIgnoreHiddenFolders = new System.Windows.Forms.CheckBox();
            this.cbIgnore0kbFiles = new System.Windows.Forms.CheckBox();
            this.tabIgnoreList = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.tbIgnoreFolders = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.gbAdvancedSettings = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cbKeepSystemFolders = new System.Windows.Forms.CheckBox();
            this.nuFolderAge = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.nuPause = new System.Windows.Forms.NumericUpDown();
            this.nuMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nuInfiniteLoopDetectionCount = new System.Windows.Forms.NumericUpDown();
            this.btnResetConfig = new System.Windows.Forms.Button();
            this.btnCopyDebugInfo = new System.Windows.Forms.Button();
            this.gbIgnoreFilenames = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbIgnoreFiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.llGithub = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tbCredits = new System.Windows.Forms.TextBox();
            this.lblRedStats = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.lbAppTitle = new System.Windows.Forms.Label();
            this.tcMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.pnlIcons.SuspendLayout();
            this.cmStrip.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.gbDeleteMode.SuspendLayout();
            this.groupBoxExplorerIntegration.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.tabIgnoreList.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.gbAdvancedSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuFolderAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuInfiniteLoopDetectionCount)).BeginInit();
            this.gbIgnoreFilenames.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilFolderIcons
            // 
            this.ilFolderIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFolderIcons.ImageStream")));
            this.ilFolderIcons.TransparentColor = System.Drawing.Color.White;
            this.ilFolderIcons.Images.SetKeyName(0, "trash");
            this.ilFolderIcons.Images.SetKeyName(1, "cancel");
            this.ilFolderIcons.Images.SetKeyName(2, "deleted");
            this.ilFolderIcons.Images.SetKeyName(3, "folder");
            this.ilFolderIcons.Images.SetKeyName(4, "folder_hidden");
            this.ilFolderIcons.Images.SetKeyName(5, "folder_lock");
            this.ilFolderIcons.Images.SetKeyName(6, "folder_lock_trash_files");
            this.ilFolderIcons.Images.SetKeyName(7, "folder_trash_files");
            this.ilFolderIcons.Images.SetKeyName(8, "folder_warning");
            this.ilFolderIcons.Images.SetKeyName(9, "help");
            this.ilFolderIcons.Images.SetKeyName(10, "home");
            this.ilFolderIcons.Images.SetKeyName(11, "search");
            this.ilFolderIcons.Images.SetKeyName(12, "folder_hidden_trash_files");
            this.ilFolderIcons.Images.SetKeyName(13, "preferences");
            this.ilFolderIcons.Images.SetKeyName(14, "exit");
            this.ilFolderIcons.Images.SetKeyName(15, "protected_icon");
            this.ilFolderIcons.Images.SetKeyName(16, "filter");
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tabSearch);
            this.tcMain.Controls.Add(this.tabSettings);
            this.tcMain.Controls.Add(this.tabIgnoreList);
            this.tcMain.Controls.Add(this.tabAdvanced);
            this.tcMain.Controls.Add(this.tabAbout);
            this.tcMain.ImageList = this.ilFolderIcons;
            this.tcMain.Location = new System.Drawing.Point(9, 7);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(10, 5);
            this.tcMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tcMain.SelectedIndex = 0;
            this.tcMain.ShowToolTips = true;
            this.tcMain.Size = new System.Drawing.Size(706, 575);
            this.tcMain.TabIndex = 18;
            // 
            // tabSearch
            // 
            this.tabSearch.AccessibleDescription = "";
            this.tabSearch.AccessibleName = "";
            this.tabSearch.Controls.Add(this.lbFastModeInfo);
            this.tabSearch.Controls.Add(this.btnShowLog);
            this.tabSearch.Controls.Add(this.btnCancel);
            this.tabSearch.Controls.Add(this.btnScan);
            this.tabSearch.Controls.Add(this.btnDelete);
            this.tabSearch.Controls.Add(this.pnlIcons);
            this.tabSearch.Controls.Add(this.label6);
            this.tabSearch.Controls.Add(this.lbStatus);
            this.tabSearch.Controls.Add(this.pbProgressStatus);
            this.tabSearch.Controls.Add(this.tvFolders);
            this.tabSearch.Controls.Add(this.btnExit);
            this.tabSearch.Controls.Add(this.tbFolder);
            this.tabSearch.Controls.Add(this.btnChooseFolder);
            this.tabSearch.Controls.Add(this.lblPickAFolder);
            this.tabSearch.ImageKey = "search";
            this.tabSearch.Location = new System.Drawing.Point(4, 27);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch.Size = new System.Drawing.Size(698, 544);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Find";
            this.tabSearch.ToolTipText = "Search for empty directories";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // lbFastModeInfo
            // 
            this.lbFastModeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFastModeInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbFastModeInfo.ForeColor = System.Drawing.Color.Gray;
            this.lbFastModeInfo.Location = new System.Drawing.Point(107, 257);
            this.lbFastModeInfo.Name = "lbFastModeInfo";
            this.lbFastModeInfo.Size = new System.Drawing.Size(346, 13);
            this.lbFastModeInfo.TabIndex = 18;
            this.lbFastModeInfo.Text = "[Fast mode is enabled, results will be shown after the process is finished]";
            this.lbFastModeInfo.Visible = false;
            // 
            // btnShowLog
            // 
            this.btnShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowLog.Enabled = false;
            this.btnShowLog.Location = new System.Drawing.Point(380, 498);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(79, 34);
            this.btnShowLog.TabIndex = 17;
            this.btnShowLog.Text = "Show &logs";
            this.btnShowLog.UseVisualStyleBackColor = true;
            this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Enabled = false;
            this.btnCancel.ImageKey = "cancel";
            this.btnCancel.ImageList = this.ilFolderIcons;
            this.btnCancel.Location = new System.Drawing.Point(276, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnScan.ImageKey = "search";
            this.btnScan.ImageList = this.ilFolderIcons;
            this.btnScan.Location = new System.Drawing.Point(13, 497);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(130, 35);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "&Find directories";
            this.btnScan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageKey = "trash";
            this.btnDelete.ImageList = this.ilFolderIcons;
            this.btnDelete.Location = new System.Drawing.Point(150, 497);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 35);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete matches";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlIcons
            // 
            this.pnlIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIcons.BackColor = System.Drawing.SystemColors.Info;
            this.pnlIcons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIcons.Controls.Add(this.label1);
            this.pnlIcons.Controls.Add(this.panel5);
            this.pnlIcons.Controls.Add(this.panel3);
            this.pnlIcons.Controls.Add(this.label18);
            this.pnlIcons.Controls.Add(this.panel2);
            this.pnlIcons.Controls.Add(this.label17);
            this.pnlIcons.Controls.Add(this.panel1);
            this.pnlIcons.Controls.Add(this.label12);
            this.pnlIcons.Location = new System.Drawing.Point(557, 60);
            this.pnlIcons.Name = "pnlIcons";
            this.pnlIcons.Size = new System.Drawing.Size(126, 406);
            this.pnlIcons.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Protected";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.Location = new System.Drawing.Point(8, 381);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(15, 15);
            this.panel5.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(8, 325);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 2);
            this.panel3.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 359);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "Will be deleted";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Location = new System.Drawing.Point(8, 358);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(15, 15);
            this.panel2.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(24, 336);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Will not be touched";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(8, 335);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(15, 15);
            this.panel1.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Icon description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Please choose a directory to check:";
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatus.Location = new System.Drawing.Point(149, 475);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(532, 13);
            this.lbStatus.TabIndex = 13;
            this.lbStatus.Text = "Status text";
            // 
            // pbProgressStatus
            // 
            this.pbProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbProgressStatus.Location = new System.Drawing.Point(14, 475);
            this.pbProgressStatus.Name = "pbProgressStatus";
            this.pbProgressStatus.Size = new System.Drawing.Size(129, 13);
            this.pbProgressStatus.TabIndex = 12;
            // 
            // tvFolders
            // 
            this.tvFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFolders.ContextMenuStrip = this.cmStrip;
            this.tvFolders.ImageKey = "folder";
            this.tvFolders.ImageList = this.ilFolderIcons;
            this.tvFolders.Location = new System.Drawing.Point(14, 60);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageKey = "folder";
            this.tvFolders.Size = new System.Drawing.Size(533, 406);
            this.tvFolders.TabIndex = 3;
            this.tvFolders.DoubleClick += new System.EventHandler(this.tvFolders_DoubleClick);
            // 
            // cmStrip
            // 
            this.cmStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.scanOnlyThisDirectoryToolStripMenuItem,
            this.toolStripSeparator4,
            this.toolStripExpandAll,
            this.toolStripCollapseAll,
            this.toolStripSeparator1,
            this.protectFolderFromBeingDeletedToolStripMenuItem,
            this.unprotectFolderToolStripMenuItem,
            this.toolStripSeparator3,
            this.proToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem});
            this.cmStrip.Name = "cmStrip";
            this.cmStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmStrip.ShowImageMargin = false;
            this.cmStrip.Size = new System.Drawing.Size(202, 204);
            this.cmStrip.Opening += new System.ComponentModel.CancelEventHandler(this.cmStrip_Opening);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openFolderToolStripMenuItem.Text = "&Open";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // scanOnlyThisDirectoryToolStripMenuItem
            // 
            this.scanOnlyThisDirectoryToolStripMenuItem.Name = "scanOnlyThisDirectoryToolStripMenuItem";
            this.scanOnlyThisDirectoryToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.scanOnlyThisDirectoryToolStripMenuItem.Text = "&Search only this directory";
            this.scanOnlyThisDirectoryToolStripMenuItem.Click += new System.EventHandler(this.scanOnlyThisDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(198, 6);
            // 
            // toolStripExpandAll
            // 
            this.toolStripExpandAll.Name = "toolStripExpandAll";
            this.toolStripExpandAll.Size = new System.Drawing.Size(201, 22);
            this.toolStripExpandAll.Text = "&Expand all";
            this.toolStripExpandAll.Click += new System.EventHandler(this.toolStripExpandAll_Click);
            // 
            // toolStripCollapseAll
            // 
            this.toolStripCollapseAll.Name = "toolStripCollapseAll";
            this.toolStripCollapseAll.Size = new System.Drawing.Size(201, 22);
            this.toolStripCollapseAll.Text = "&Collapse all";
            this.toolStripCollapseAll.Click += new System.EventHandler(this.toolStripCollapseAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // protectFolderFromBeingDeletedToolStripMenuItem
            // 
            this.protectFolderFromBeingDeletedToolStripMenuItem.Name = "protectFolderFromBeingDeletedToolStripMenuItem";
            this.protectFolderFromBeingDeletedToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.protectFolderFromBeingDeletedToolStripMenuItem.Text = "&Protect from deletion (once)";
            this.protectFolderFromBeingDeletedToolStripMenuItem.Click += new System.EventHandler(this.protectFolderFromBeingDeletedToolStripMenuItem_Click);
            // 
            // unprotectFolderToolStripMenuItem
            // 
            this.unprotectFolderToolStripMenuItem.Name = "unprotectFolderToolStripMenuItem";
            this.unprotectFolderToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.unprotectFolderToolStripMenuItem.Text = "&Unprotect";
            this.unprotectFolderToolStripMenuItem.Click += new System.EventHandler(this.unprotectFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // proToolStripMenuItem
            // 
            this.proToolStripMenuItem.Name = "proToolStripMenuItem";
            this.proToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.proToolStripMenuItem.Text = "Add to permanent &ignore list";
            this.proToolStripMenuItem.Click += new System.EventHandler(this.proToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.ImageKey = "exit";
            this.btnExit.ImageList = this.ilFolderIcons;
            this.btnExit.Location = new System.Drawing.Point(558, 498);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(126, 34);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "&Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.AccessibleDescription = "Root directory";
            this.tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolder.Location = new System.Drawing.Point(14, 34);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(533, 20);
            this.tbFolder.TabIndex = 1;
            this.tbFolder.Text = "C:\\";
            this.tbFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbFolder_MouseDoubleClick);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseFolder.Location = new System.Drawing.Point(558, 33);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(126, 21);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "Browse...";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // lblPickAFolder
            // 
            this.lblPickAFolder.AutoSize = true;
            this.lblPickAFolder.Location = new System.Drawing.Point(10, 13);
            this.lblPickAFolder.Name = "lblPickAFolder";
            this.lblPickAFolder.Size = new System.Drawing.Size(0, 13);
            this.lblPickAFolder.TabIndex = 3;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.gbDeleteMode);
            this.tabSettings.Controls.Add(this.groupBoxExplorerIntegration);
            this.tabSettings.Controls.Add(this.gbOptions);
            this.tabSettings.ImageKey = "preferences";
            this.tabSettings.Location = new System.Drawing.Point(4, 27);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(698, 544);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.ToolTipText = "Application settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // gbDeleteMode
            // 
            this.gbDeleteMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDeleteMode.Controls.Add(this.label15);
            this.gbDeleteMode.Controls.Add(this.cbDeleteMode);
            this.gbDeleteMode.Location = new System.Drawing.Point(14, 358);
            this.gbDeleteMode.Name = "gbDeleteMode";
            this.gbDeleteMode.Size = new System.Drawing.Size(669, 84);
            this.gbDeleteMode.TabIndex = 20;
            this.gbDeleteMode.TabStop = false;
            this.gbDeleteMode.Text = "Delete mode";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(204, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "How should empty directories be deleted?";
            // 
            // cbDeleteMode
            // 
            this.cbDeleteMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDeleteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeleteMode.FormattingEnabled = true;
            this.cbDeleteMode.Location = new System.Drawing.Point(19, 45);
            this.cbDeleteMode.Name = "cbDeleteMode";
            this.cbDeleteMode.Size = new System.Drawing.Size(631, 21);
            this.cbDeleteMode.TabIndex = 7;
            // 
            // groupBoxExplorerIntegration
            // 
            this.groupBoxExplorerIntegration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExplorerIntegration.Controls.Add(this.lblReqAdmin);
            this.groupBoxExplorerIntegration.Controls.Add(this.btnExplorerRemove);
            this.groupBoxExplorerIntegration.Controls.Add(this.btnExplorerIntegrate);
            this.groupBoxExplorerIntegration.Controls.Add(this.label16);
            this.groupBoxExplorerIntegration.Location = new System.Drawing.Point(14, 461);
            this.groupBoxExplorerIntegration.Name = "groupBoxExplorerIntegration";
            this.groupBoxExplorerIntegration.Size = new System.Drawing.Size(669, 71);
            this.groupBoxExplorerIntegration.TabIndex = 14;
            this.groupBoxExplorerIntegration.TabStop = false;
            this.groupBoxExplorerIntegration.Text = "Windows Explorer integration";
            // 
            // lblReqAdmin
            // 
            this.lblReqAdmin.AutoSize = true;
            this.lblReqAdmin.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblReqAdmin.Location = new System.Drawing.Point(16, 43);
            this.lblReqAdmin.Name = "lblReqAdmin";
            this.lblReqAdmin.Size = new System.Drawing.Size(314, 13);
            this.lblReqAdmin.TabIndex = 26;
            this.lblReqAdmin.Text = "You need to start the application as an Admin user to change this";
            // 
            // btnExplorerRemove
            // 
            this.btnExplorerRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerRemove.Location = new System.Drawing.Point(532, 24);
            this.btnExplorerRemove.Name = "btnExplorerRemove";
            this.btnExplorerRemove.Size = new System.Drawing.Size(119, 34);
            this.btnExplorerRemove.TabIndex = 25;
            this.btnExplorerRemove.Text = "Uninstall";
            this.btnExplorerRemove.UseVisualStyleBackColor = true;
            this.btnExplorerRemove.Click += new System.EventHandler(this.btnExplorerRemove_Click);
            // 
            // btnExplorerIntegrate
            // 
            this.btnExplorerIntegrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerIntegrate.Location = new System.Drawing.Point(407, 24);
            this.btnExplorerIntegrate.Name = "btnExplorerIntegrate";
            this.btnExplorerIntegrate.Size = new System.Drawing.Size(119, 34);
            this.btnExplorerIntegrate.TabIndex = 24;
            this.btnExplorerIntegrate.Text = "Install";
            this.btnExplorerIntegrate.UseVisualStyleBackColor = true;
            this.btnExplorerIntegrate.Click += new System.EventHandler(this.btnExplorerIntegrate_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(268, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Integrate RED into the Windows Explorer context menu";
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.label25);
            this.gbOptions.Controls.Add(this.label11);
            this.gbOptions.Controls.Add(this.label21);
            this.gbOptions.Controls.Add(this.cbFastSearchMode);
            this.gbOptions.Controls.Add(this.cbHideScanErrors);
            this.gbOptions.Controls.Add(this.cbIgnoreErrors);
            this.gbOptions.Controls.Add(this.cbClipboardDetection);
            this.gbOptions.Controls.Add(this.cbIgnoreHiddenFolders);
            this.gbOptions.Controls.Add(this.cbIgnore0kbFiles);
            this.gbOptions.Location = new System.Drawing.Point(14, 12);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(669, 330);
            this.gbOptions.TabIndex = 19;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "General options";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label25.Location = new System.Drawing.Point(36, 170);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(439, 26);
            this.label25.TabIndex = 22;
            this.label25.Text = "Whenever you start or switch into the application it will check if your clipboard" +
    " contains\r\na path to a directory (e.g. C:/test) and if it detects a directory it" +
    " will use it as the root directory";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(36, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(409, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "An empty file means files with zero bytes this option also applies to multiple em" +
    "pty files.";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label21.Location = new System.Drawing.Point(36, 105);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(586, 26);
            this.label21.TabIndex = 19;
            this.label21.Text = resources.GetString("label21.Text");
            // 
            // cbFastSearchMode
            // 
            this.cbFastSearchMode.AutoSize = true;
            this.cbFastSearchMode.Location = new System.Drawing.Point(19, 86);
            this.cbFastSearchMode.Name = "cbFastSearchMode";
            this.cbFastSearchMode.Size = new System.Drawing.Size(75, 17);
            this.cbFastSearchMode.TabIndex = 18;
            this.cbFastSearchMode.Text = "Fast mode";
            this.cbFastSearchMode.UseVisualStyleBackColor = true;
            // 
            // cbHideScanErrors
            // 
            this.cbHideScanErrors.AutoSize = true;
            this.cbHideScanErrors.Location = new System.Drawing.Point(19, 288);
            this.cbHideScanErrors.Name = "cbHideScanErrors";
            this.cbHideScanErrors.Size = new System.Drawing.Size(238, 17);
            this.cbHideScanErrors.TabIndex = 16;
            this.cbHideScanErrors.Tag = "hide_scan_errors";
            this.cbHideScanErrors.Text = "Hide search errors (like access denied errors)";
            this.cbHideScanErrors.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreErrors
            // 
            this.cbIgnoreErrors.AutoSize = true;
            this.cbIgnoreErrors.Location = new System.Drawing.Point(19, 252);
            this.cbIgnoreErrors.Name = "cbIgnoreErrors";
            this.cbIgnoreErrors.Size = new System.Drawing.Size(157, 17);
            this.cbIgnoreErrors.TabIndex = 15;
            this.cbIgnoreErrors.Tag = "ignore_deletion_errors";
            this.cbIgnoreErrors.Text = "Ignore errors during deletion\r\n";
            this.cbIgnoreErrors.UseVisualStyleBackColor = true;
            // 
            // cbClipboardDetection
            // 
            this.cbClipboardDetection.AutoSize = true;
            this.cbClipboardDetection.Location = new System.Drawing.Point(19, 151);
            this.cbClipboardDetection.Name = "cbClipboardDetection";
            this.cbClipboardDetection.Size = new System.Drawing.Size(162, 17);
            this.cbClipboardDetection.TabIndex = 5;
            this.cbClipboardDetection.Tag = "clipboard_detection";
            this.cbClipboardDetection.Text = "Detect paths in the clipboard";
            this.cbClipboardDetection.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreHiddenFolders
            // 
            this.cbIgnoreHiddenFolders.AutoSize = true;
            this.cbIgnoreHiddenFolders.Location = new System.Drawing.Point(19, 216);
            this.cbIgnoreHiddenFolders.Name = "cbIgnoreHiddenFolders";
            this.cbIgnoreHiddenFolders.Size = new System.Drawing.Size(142, 17);
            this.cbIgnoreHiddenFolders.TabIndex = 4;
            this.cbIgnoreHiddenFolders.Tag = "ignore_hidden";
            this.cbIgnoreHiddenFolders.Text = "Ignore hidden directories";
            this.cbIgnoreHiddenFolders.UseVisualStyleBackColor = true;
            // 
            // cbIgnore0kbFiles
            // 
            this.cbIgnore0kbFiles.AutoSize = true;
            this.cbIgnore0kbFiles.Checked = true;
            this.cbIgnore0kbFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnore0kbFiles.Location = new System.Drawing.Point(19, 34);
            this.cbIgnore0kbFiles.Name = "cbIgnore0kbFiles";
            this.cbIgnore0kbFiles.Size = new System.Drawing.Size(268, 17);
            this.cbIgnore0kbFiles.TabIndex = 3;
            this.cbIgnore0kbFiles.Tag = "ignore_0kb_files";
            this.cbIgnore0kbFiles.Text = "Directories with empty files will be considered empty";
            this.cbIgnore0kbFiles.UseVisualStyleBackColor = true;
            // 
            // tabIgnoreList
            // 
            this.tabIgnoreList.Controls.Add(this.label22);
            this.tabIgnoreList.Controls.Add(this.tbIgnoreFolders);
            this.tabIgnoreList.Controls.Add(this.label10);
            this.tabIgnoreList.ImageKey = "filter";
            this.tabIgnoreList.Location = new System.Drawing.Point(4, 27);
            this.tabIgnoreList.Name = "tabIgnoreList";
            this.tabIgnoreList.Padding = new System.Windows.Forms.Padding(3);
            this.tabIgnoreList.Size = new System.Drawing.Size(698, 544);
            this.tabIgnoreList.TabIndex = 4;
            this.tabIgnoreList.Text = "Ignore list";
            this.tabIgnoreList.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label22.Location = new System.Drawing.Point(14, 38);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(435, 13);
            this.label22.TabIndex = 14;
            this.label22.Text = "Use one item per line and you can specify a directory name or a full path like \"C" +
    ":/example\".";
            // 
            // tbIgnoreFolders
            // 
            this.tbIgnoreFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIgnoreFolders.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIgnoreFolders.Location = new System.Drawing.Point(14, 64);
            this.tbIgnoreFolders.Multiline = true;
            this.tbIgnoreFolders.Name = "tbIgnoreFolders";
            this.tbIgnoreFolders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIgnoreFolders.Size = new System.Drawing.Size(668, 467);
            this.tbIgnoreFolders.TabIndex = 6;
            this.tbIgnoreFolders.WordWrap = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(458, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "When a directory name matches an item of this list it will be skipped (including " +
    "all subdirectories).";
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.gbAdvancedSettings);
            this.tabAdvanced.Controls.Add(this.btnResetConfig);
            this.tabAdvanced.Controls.Add(this.btnCopyDebugInfo);
            this.tabAdvanced.Controls.Add(this.gbIgnoreFilenames);
            this.tabAdvanced.Controls.Add(this.label8);
            this.tabAdvanced.ImageKey = "(none)";
            this.tabAdvanced.Location = new System.Drawing.Point(4, 27);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdvanced.Size = new System.Drawing.Size(698, 544);
            this.tabAdvanced.TabIndex = 3;
            this.tabAdvanced.Text = "Advanced settings";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // gbAdvancedSettings
            // 
            this.gbAdvancedSettings.Controls.Add(this.label20);
            this.gbAdvancedSettings.Controls.Add(this.label26);
            this.gbAdvancedSettings.Controls.Add(this.label27);
            this.gbAdvancedSettings.Controls.Add(this.label19);
            this.gbAdvancedSettings.Controls.Add(this.cbKeepSystemFolders);
            this.gbAdvancedSettings.Controls.Add(this.nuFolderAge);
            this.gbAdvancedSettings.Controls.Add(this.label7);
            this.gbAdvancedSettings.Controls.Add(this.label24);
            this.gbAdvancedSettings.Controls.Add(this.nuPause);
            this.gbAdvancedSettings.Controls.Add(this.nuMaxDepth);
            this.gbAdvancedSettings.Controls.Add(this.label4);
            this.gbAdvancedSettings.Controls.Add(this.label3);
            this.gbAdvancedSettings.Controls.Add(this.nuInfiniteLoopDetectionCount);
            this.gbAdvancedSettings.Location = new System.Drawing.Point(15, 17);
            this.gbAdvancedSettings.Name = "gbAdvancedSettings";
            this.gbAdvancedSettings.Size = new System.Drawing.Size(668, 160);
            this.gbAdvancedSettings.TabIndex = 30;
            this.gbAdvancedSettings.TabStop = false;
            this.gbAdvancedSettings.Text = "Advanced settings";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label20.Location = new System.Drawing.Point(16, 134);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(314, 13);
            this.label20.TabIndex = 19;
            this.label20.Text = "This gives you time to stop the process but is not really necessary";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label26.Location = new System.Drawing.Point(357, 79);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(246, 13);
            this.label26.TabIndex = 28;
            this.label26.Text = "This allows you to ignore freshly created directories";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label27.Location = new System.Drawing.Point(16, 78);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(259, 13);
            this.label27.TabIndex = 29;
            this.label27.Text = "RED will only able to find empty directories that are N ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(357, 56);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(167, 13);
            this.label19.TabIndex = 26;
            this.label19.Text = "Skip folders less than N hours old:";
            // 
            // cbKeepSystemFolders
            // 
            this.cbKeepSystemFolders.AutoSize = true;
            this.cbKeepSystemFolders.Checked = true;
            this.cbKeepSystemFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepSystemFolders.Location = new System.Drawing.Point(248, 26);
            this.cbKeepSystemFolders.Name = "cbKeepSystemFolders";
            this.cbKeepSystemFolders.Size = new System.Drawing.Size(15, 14);
            this.cbKeepSystemFolders.TabIndex = 2;
            this.cbKeepSystemFolders.Tag = "keep_system_dirs";
            this.cbKeepSystemFolders.UseVisualStyleBackColor = true;
            // 
            // nuFolderAge
            // 
            this.nuFolderAge.Location = new System.Drawing.Point(588, 52);
            this.nuFolderAge.Margin = new System.Windows.Forms.Padding(2);
            this.nuFolderAge.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.nuFolderAge.Name = "nuFolderAge";
            this.nuFolderAge.Size = new System.Drawing.Size(53, 20);
            this.nuFolderAge.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Pause between each deletion in milliseconds";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 26);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(190, 13);
            this.label24.TabIndex = 27;
            this.label24.Text = "Skip system directories (recommended)";
            // 
            // nuPause
            // 
            this.nuPause.Location = new System.Drawing.Point(249, 109);
            this.nuPause.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuPause.Name = "nuPause";
            this.nuPause.Size = new System.Drawing.Size(53, 20);
            this.nuPause.TabIndex = 8;
            // 
            // nuMaxDepth
            // 
            this.nuMaxDepth.Location = new System.Drawing.Point(249, 52);
            this.nuMaxDepth.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nuMaxDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nuMaxDepth.Name = "nuMaxDepth";
            this.nuMaxDepth.Size = new System.Drawing.Size(53, 20);
            this.nuMaxDepth.TabIndex = 7;
            this.nuMaxDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Infinite-loop detection: Stop after N errors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Max directory nesting depth (-1 = infinite):";
            // 
            // nuInfiniteLoopDetectionCount
            // 
            this.nuInfiniteLoopDetectionCount.Location = new System.Drawing.Point(588, 115);
            this.nuInfiniteLoopDetectionCount.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuInfiniteLoopDetectionCount.Name = "nuInfiniteLoopDetectionCount";
            this.nuInfiniteLoopDetectionCount.Size = new System.Drawing.Size(53, 20);
            this.nuInfiniteLoopDetectionCount.TabIndex = 21;
            this.nuInfiniteLoopDetectionCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnResetConfig
            // 
            this.btnResetConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetConfig.Location = new System.Drawing.Point(318, 505);
            this.btnResetConfig.Name = "btnResetConfig";
            this.btnResetConfig.Size = new System.Drawing.Size(183, 27);
            this.btnResetConfig.TabIndex = 23;
            this.btnResetConfig.Text = "Reset settings to default values";
            this.btnResetConfig.UseVisualStyleBackColor = true;
            this.btnResetConfig.Click += new System.EventHandler(this.btnResetConfig_Click);
            // 
            // btnCopyDebugInfo
            // 
            this.btnCopyDebugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyDebugInfo.Location = new System.Drawing.Point(15, 505);
            this.btnCopyDebugInfo.Name = "btnCopyDebugInfo";
            this.btnCopyDebugInfo.Size = new System.Drawing.Size(297, 27);
            this.btnCopyDebugInfo.TabIndex = 24;
            this.btnCopyDebugInfo.Text = "Copy debugging information to clipboard (for error reports)";
            this.btnCopyDebugInfo.UseVisualStyleBackColor = true;
            this.btnCopyDebugInfo.Click += new System.EventHandler(this.btnCopyDebugInfo_Click);
            // 
            // gbIgnoreFilenames
            // 
            this.gbIgnoreFilenames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbIgnoreFilenames.Controls.Add(this.label23);
            this.gbIgnoreFilenames.Controls.Add(this.label14);
            this.gbIgnoreFilenames.Controls.Add(this.tbIgnoreFiles);
            this.gbIgnoreFilenames.Controls.Add(this.label2);
            this.gbIgnoreFilenames.Location = new System.Drawing.Point(15, 194);
            this.gbIgnoreFilenames.Name = "gbIgnoreFilenames";
            this.gbIgnoreFilenames.Size = new System.Drawing.Size(668, 303);
            this.gbIgnoreFilenames.TabIndex = 20;
            this.gbIgnoreFilenames.TabStop = false;
            this.gbIgnoreFilenames.Text = "Filenames to ignore";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(15, 60);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(357, 78);
            this.label23.TabIndex = 13;
            this.label23.Text = resources.GetString("label23.Text");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Info;
            this.label14.ForeColor = System.Drawing.Color.DarkRed;
            this.label14.Location = new System.Drawing.Point(484, 64);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(5);
            this.label14.Size = new System.Drawing.Size(165, 62);
            this.label14.TabIndex = 12;
            this.label14.Text = "Warning: Use  this feature with \r\ncare, a bad pattern could \r\npotentially cause a" +
    "ccidental \r\ndeletion of important files.";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbIgnoreFiles
            // 
            this.tbIgnoreFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIgnoreFiles.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIgnoreFiles.Location = new System.Drawing.Point(18, 150);
            this.tbIgnoreFiles.Multiline = true;
            this.tbIgnoreFiles.Name = "tbIgnoreFiles";
            this.tbIgnoreFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIgnoreFiles.Size = new System.Drawing.Size(631, 139);
            this.tbIgnoreFiles.TabIndex = 5;
            this.tbIgnoreFiles.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(546, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(318, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "ms";
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.llGithub);
            this.tabAbout.Controls.Add(this.linkLabel2);
            this.tabAbout.Controls.Add(this.linkLabel1);
            this.tabAbout.Controls.Add(this.tbCredits);
            this.tabAbout.Controls.Add(this.lblRedStats);
            this.tabAbout.Controls.Add(this.label5);
            this.tabAbout.Controls.Add(this.llWebsite);
            this.tabAbout.Controls.Add(this.lbAppTitle);
            this.tabAbout.ImageKey = "help";
            this.tabAbout.Location = new System.Drawing.Point(4, 27);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(698, 544);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.ToolTipText = "Shows the help and about screen";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // llGithub
            // 
            this.llGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llGithub.AutoSize = true;
            this.llGithub.Location = new System.Drawing.Point(11, 103);
            this.llGithub.Name = "llGithub";
            this.llGithub.Size = new System.Drawing.Size(38, 13);
            this.llGithub.TabIndex = 28;
            this.llGithub.TabStop = true;
            this.llGithub.Text = "Github";
            this.llGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGithub_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(134, 103);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(128, 13);
            this.linkLabel2.TabIndex = 27;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Feedback / Report a bug";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(134, 81);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(94, 13);
            this.linkLabel1.TabIndex = 26;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Check for updates";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tbCredits
            // 
            this.tbCredits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCredits.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCredits.Location = new System.Drawing.Point(14, 131);
            this.tbCredits.Multiline = true;
            this.tbCredits.Name = "tbCredits";
            this.tbCredits.ReadOnly = true;
            this.tbCredits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCredits.Size = new System.Drawing.Size(669, 400);
            this.tbCredits.TabIndex = 25;
            this.tbCredits.Text = resources.GetString("tbCredits.Text");
            this.tbCredits.WordWrap = false;
            // 
            // lblRedStats
            // 
            this.lblRedStats.AutoSize = true;
            this.lblRedStats.Location = new System.Drawing.Point(11, 56);
            this.lblRedStats.Name = "lblRedStats";
            this.lblRedStats.Size = new System.Drawing.Size(165, 13);
            this.lblRedStats.TabIndex = 20;
            this.lblRedStats.Text = "{Deleted N dirs placeholder label}";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(304, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "Created by Jonas John and a some contributors (see Github).";
            // 
            // llWebsite
            // 
            this.llWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llWebsite.AutoSize = true;
            this.llWebsite.Location = new System.Drawing.Point(11, 81);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(93, 13);
            this.llWebsite.TabIndex = 19;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "Project homepage\r\n";
            this.llWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebsite_LinkClicked);
            // 
            // lbAppTitle
            // 
            this.lbAppTitle.AutoSize = true;
            this.lbAppTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbAppTitle.Location = new System.Drawing.Point(11, 15);
            this.lbAppTitle.Name = "lbAppTitle";
            this.lbAppTitle.Size = new System.Drawing.Size(213, 15);
            this.lbAppTitle.TabIndex = 0;
            this.lbAppTitle.Text = "Remove Empty Directories - Version ";
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 591);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remove Empty Directories";
            this.Activated += new System.EventHandler(this.fMain_Activated);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.fMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.fMain_DragEnter);
            this.tcMain.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.pnlIcons.ResumeLayout(false);
            this.pnlIcons.PerformLayout();
            this.cmStrip.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.gbDeleteMode.ResumeLayout(false);
            this.gbDeleteMode.PerformLayout();
            this.groupBoxExplorerIntegration.ResumeLayout(false);
            this.groupBoxExplorerIntegration.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.tabIgnoreList.ResumeLayout(false);
            this.tabIgnoreList.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.gbAdvancedSettings.ResumeLayout(false);
            this.gbAdvancedSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuFolderAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuInfiniteLoopDetectionCount)).EndInit();
            this.gbIgnoreFilenames.ResumeLayout(false);
            this.gbIgnoreFilenames.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tabAbout;
		private System.Windows.Forms.Label lbAppTitle;
		private System.Windows.Forms.CheckBox cbIgnore0kbFiles;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbIgnoreFiles;
		private System.Windows.Forms.CheckBox cbKeepSystemFolders;
		private System.Windows.Forms.CheckBox cbIgnoreHiddenFolders;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tabSearch;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.ProgressBar pbProgressStatus;
		private System.Windows.Forms.TreeView tvFolders;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbFolder;
		private System.Windows.Forms.Button btnChooseFolder;
		private System.Windows.Forms.Label lblPickAFolder;
		private System.Windows.Forms.Button btnScan;
		private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.LinkLabel llWebsite;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nuMaxDepth;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nuPause;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbIgnoreFilenames;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.TextBox tbIgnoreFolders;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ImageList ilFolderIcons;
        private System.Windows.Forms.Panel pnlIcons;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblRedStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ContextMenuStrip cmStrip;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem protectFolderFromBeingDeletedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unprotectFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem proToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbClipboardDetection;
        private System.Windows.Forms.Button btnShowLog;
        private System.Windows.Forms.ComboBox cbDeleteMode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbCredits;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox cbIgnoreErrors;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nuInfiniteLoopDetectionCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnResetConfig;
        private System.Windows.Forms.ToolStripMenuItem scanOnlyThisDirectoryToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxExplorerIntegration;
        private System.Windows.Forms.Button btnExplorerRemove;
        private System.Windows.Forms.Button btnExplorerIntegrate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblReqAdmin;
        private System.Windows.Forms.CheckBox cbHideScanErrors;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btnCopyDebugInfo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nuFolderAge;
		private System.Windows.Forms.CheckBox cbFastSearchMode;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabPage tabIgnoreList;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbDeleteMode;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripExpandAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripCollapseAll;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.LinkLabel llGithub;
        private System.Windows.Forms.Label lbFastModeInfo;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox gbAdvancedSettings;
    }
}

