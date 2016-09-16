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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReqAdmin = new System.Windows.Forms.Label();
            this.btnExplorerRemove = new System.Windows.Forms.Button();
            this.btnExplorerIntegrate = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.gpOptions = new System.Windows.Forms.GroupBox();
            this.cbHideScanErrors = new System.Windows.Forms.CheckBox();
            this.cbIgnoreErrors = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbDeleteMode = new System.Windows.Forms.ComboBox();
            this.cbClipboardDetection = new System.Windows.Forms.CheckBox();
            this.cbIgnoreHiddenFolders = new System.Windows.Forms.CheckBox();
            this.cbKeepSystemFolders = new System.Windows.Forms.CheckBox();
            this.cbIgnore0kbFiles = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbIgnoreFolders = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbIgnoreFiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCopyDebugInfo = new System.Windows.Forms.Button();
            this.btnResetConfig = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.nuInfiniteLoopDetectionCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nuMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nuPause = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tbCredits = new System.Windows.Forms.TextBox();
            this.lblRedStats = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.lbAppTitle = new System.Windows.Forms.Label();
            this.nuFolderAge = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.tcMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.pnlIcons.SuspendLayout();
            this.cmStrip.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gpOptions.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuInfiniteLoopDetectionCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).BeginInit();
            this.tabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuFolderAge)).BeginInit();
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
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tabSearch);
            this.tcMain.Controls.Add(this.tabSettings);
            this.tcMain.Controls.Add(this.tabAbout);
            this.tcMain.ImageList = this.ilFolderIcons;
            this.tcMain.Location = new System.Drawing.Point(9, 7);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tcMain.SelectedIndex = 0;
            this.tcMain.ShowToolTips = true;
            this.tcMain.Size = new System.Drawing.Size(821, 644);
            this.tcMain.TabIndex = 18;
            // 
            // tabSearch
            // 
            this.tabSearch.AccessibleDescription = "";
            this.tabSearch.AccessibleName = "";
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
            this.tabSearch.Location = new System.Drawing.Point(4, 25);
            this.tabSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSearch.Size = new System.Drawing.Size(813, 615);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Scan";
            this.tabSearch.ToolTipText = "Search for empty directories";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // btnShowLog
            // 
            this.btnShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowLog.Location = new System.Drawing.Point(448, 556);
            this.btnShowLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(97, 42);
            this.btnShowLog.TabIndex = 17;
            this.btnShowLog.Text = "Show log";
            this.btnShowLog.UseVisualStyleBackColor = true;
            this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Enabled = false;
            this.btnCancel.ImageKey = "cancel";
            this.btnCancel.ImageList = this.ilFolderIcons;
            this.btnCancel.Location = new System.Drawing.Point(309, 556);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 42);
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
            this.btnScan.Location = new System.Drawing.Point(13, 556);
            this.btnScan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(131, 43);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "&Scan folders";
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
            this.btnDelete.Location = new System.Drawing.Point(152, 556);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(149, 43);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete folders";
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
            this.pnlIcons.Location = new System.Drawing.Point(632, 71);
            this.pnlIcons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlIcons.Name = "pnlIcons";
            this.pnlIcons.Size = new System.Drawing.Size(167, 445);
            this.pnlIcons.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 416);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Protected";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.Location = new System.Drawing.Point(11, 415);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(20, 18);
            this.panel5.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(11, 346);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(160, 2);
            this.panel3.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(33, 388);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(101, 17);
            this.label18.TabIndex = 20;
            this.label18.Text = "Will be deleted";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Location = new System.Drawing.Point(11, 386);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 18);
            this.panel2.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(32, 359);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(129, 17);
            this.label17.TabIndex = 18;
            this.label17.Text = "Will not be touched";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(11, 358);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 18);
            this.panel1.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 7);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Icon description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(226, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Please choose a directory to scan:";
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(221, 524);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(74, 17);
            this.lbStatus.TabIndex = 13;
            this.lbStatus.Text = "Status text";
            // 
            // pbProgressStatus
            // 
            this.pbProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbProgressStatus.Location = new System.Drawing.Point(13, 524);
            this.pbProgressStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbProgressStatus.Name = "pbProgressStatus";
            this.pbProgressStatus.Size = new System.Drawing.Size(204, 16);
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
            this.tvFolders.Location = new System.Drawing.Point(13, 71);
            this.tvFolders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageKey = "folder";
            this.tvFolders.Size = new System.Drawing.Size(609, 445);
            this.tvFolders.TabIndex = 3;
            this.tvFolders.DoubleClick += new System.EventHandler(this.tvFolders_DoubleClick);
            // 
            // cmStrip
            // 
            this.cmStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.scanOnlyThisDirectoryToolStripMenuItem,
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
            this.cmStrip.Size = new System.Drawing.Size(263, 178);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.openFolderToolStripMenuItem.Text = "&Open in Explorer";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // scanOnlyThisDirectoryToolStripMenuItem
            // 
            this.scanOnlyThisDirectoryToolStripMenuItem.Name = "scanOnlyThisDirectoryToolStripMenuItem";
            this.scanOnlyThisDirectoryToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.scanOnlyThisDirectoryToolStripMenuItem.Text = "Scan only this directory";
            this.scanOnlyThisDirectoryToolStripMenuItem.Click += new System.EventHandler(this.scanOnlyThisDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(259, 6);
            // 
            // protectFolderFromBeingDeletedToolStripMenuItem
            // 
            this.protectFolderFromBeingDeletedToolStripMenuItem.Name = "protectFolderFromBeingDeletedToolStripMenuItem";
            this.protectFolderFromBeingDeletedToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.protectFolderFromBeingDeletedToolStripMenuItem.Text = "&Protect once";
            this.protectFolderFromBeingDeletedToolStripMenuItem.Click += new System.EventHandler(this.protectFolderFromBeingDeletedToolStripMenuItem_Click);
            // 
            // unprotectFolderToolStripMenuItem
            // 
            this.unprotectFolderToolStripMenuItem.Name = "unprotectFolderToolStripMenuItem";
            this.unprotectFolderToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.unprotectFolderToolStripMenuItem.Text = "&Unprotect";
            this.unprotectFolderToolStripMenuItem.Click += new System.EventHandler(this.unprotectFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(259, 6);
            // 
            // proToolStripMenuItem
            // 
            this.proToolStripMenuItem.Name = "proToolStripMenuItem";
            this.proToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.proToolStripMenuItem.Text = "Add to &ignore list";
            this.proToolStripMenuItem.Click += new System.EventHandler(this.proToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(259, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.deleteToolStripMenuItem.Text = "&Delete incl. all subdirectories (!)";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.ImageKey = "exit";
            this.btnExit.ImageList = this.ilFolderIcons;
            this.btnExit.Location = new System.Drawing.Point(632, 556);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(168, 42);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "&Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolder.Location = new System.Drawing.Point(13, 36);
            this.tbFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(697, 22);
            this.tbFolder.TabIndex = 1;
            this.tbFolder.Text = "C:\\";
            this.tbFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbFolder_MouseDoubleClick);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseFolder.Location = new System.Drawing.Point(716, 36);
            this.btnChooseFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(84, 25);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "Browse...";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // lblPickAFolder
            // 
            this.lblPickAFolder.AutoSize = true;
            this.lblPickAFolder.Location = new System.Drawing.Point(13, 16);
            this.lblPickAFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPickAFolder.Name = "lblPickAFolder";
            this.lblPickAFolder.Size = new System.Drawing.Size(0, 17);
            this.lblPickAFolder.TabIndex = 3;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.groupBox1);
            this.tabSettings.Controls.Add(this.gpOptions);
            this.tabSettings.Controls.Add(this.groupBox4);
            this.tabSettings.Controls.Add(this.groupBox2);
            this.tabSettings.Controls.Add(this.groupBox3);
            this.tabSettings.ImageKey = "preferences";
            this.tabSettings.Location = new System.Drawing.Point(4, 25);
            this.tabSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSettings.Size = new System.Drawing.Size(813, 615);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.ToolTipText = "Application settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblReqAdmin);
            this.groupBox1.Controls.Add(this.btnExplorerRemove);
            this.groupBox1.Controls.Add(this.btnExplorerIntegrate);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Location = new System.Drawing.Point(19, 379);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(773, 73);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Explorer integration";
            // 
            // lblReqAdmin
            // 
            this.lblReqAdmin.AutoSize = true;
            this.lblReqAdmin.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblReqAdmin.Location = new System.Drawing.Point(11, 46);
            this.lblReqAdmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReqAdmin.Name = "lblReqAdmin";
            this.lblReqAdmin.Size = new System.Drawing.Size(342, 17);
            this.lblReqAdmin.TabIndex = 26;
            this.lblReqAdmin.Text = "You need to run RED as Administrator to change this";
            // 
            // btnExplorerRemove
            // 
            this.btnExplorerRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerRemove.Location = new System.Drawing.Point(581, 25);
            this.btnExplorerRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExplorerRemove.Name = "btnExplorerRemove";
            this.btnExplorerRemove.Size = new System.Drawing.Size(172, 33);
            this.btnExplorerRemove.TabIndex = 25;
            this.btnExplorerRemove.Text = "Remove";
            this.btnExplorerRemove.UseVisualStyleBackColor = true;
            this.btnExplorerRemove.Click += new System.EventHandler(this.btnExplorerRemove_Click);
            // 
            // btnExplorerIntegrate
            // 
            this.btnExplorerIntegrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerIntegrate.Location = new System.Drawing.Point(401, 25);
            this.btnExplorerIntegrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExplorerIntegrate.Name = "btnExplorerIntegrate";
            this.btnExplorerIntegrate.Size = new System.Drawing.Size(172, 33);
            this.btnExplorerIntegrate.TabIndex = 24;
            this.btnExplorerIntegrate.Text = "Integrate";
            this.btnExplorerIntegrate.UseVisualStyleBackColor = true;
            this.btnExplorerIntegrate.Click += new System.EventHandler(this.btnExplorerIntegrate_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 25);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(292, 17);
            this.label16.TabIndex = 14;
            this.label16.Text = "Integrate RED into the Explorer context menu";
            // 
            // gpOptions
            // 
            this.gpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpOptions.Controls.Add(this.cbHideScanErrors);
            this.gpOptions.Controls.Add(this.cbIgnoreErrors);
            this.gpOptions.Controls.Add(this.label15);
            this.gpOptions.Controls.Add(this.cbDeleteMode);
            this.gpOptions.Controls.Add(this.cbClipboardDetection);
            this.gpOptions.Controls.Add(this.cbIgnoreHiddenFolders);
            this.gpOptions.Controls.Add(this.cbKeepSystemFolders);
            this.gpOptions.Controls.Add(this.cbIgnore0kbFiles);
            this.gpOptions.Location = new System.Drawing.Point(19, 11);
            this.gpOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpOptions.Name = "gpOptions";
            this.gpOptions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpOptions.Size = new System.Drawing.Size(773, 117);
            this.gpOptions.TabIndex = 19;
            this.gpOptions.TabStop = false;
            this.gpOptions.Text = "General options";
            // 
            // cbHideScanErrors
            // 
            this.cbHideScanErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbHideScanErrors.AutoSize = true;
            this.cbHideScanErrors.Location = new System.Drawing.Point(598, 87);
            this.cbHideScanErrors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbHideScanErrors.Name = "cbHideScanErrors";
            this.cbHideScanErrors.Size = new System.Drawing.Size(135, 21);
            this.cbHideScanErrors.TabIndex = 16;
            this.cbHideScanErrors.Tag = "hide_scan_errors";
            this.cbHideScanErrors.Text = "Hide scan errors";
            this.cbHideScanErrors.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreErrors
            // 
            this.cbIgnoreErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIgnoreErrors.AutoSize = true;
            this.cbIgnoreErrors.Location = new System.Drawing.Point(359, 87);
            this.cbIgnoreErrors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIgnoreErrors.Name = "cbIgnoreErrors";
            this.cbIgnoreErrors.Size = new System.Drawing.Size(210, 21);
            this.cbIgnoreErrors.TabIndex = 15;
            this.cbIgnoreErrors.Tag = "ignore_deletion_errors";
            this.cbIgnoreErrors.Text = "Ignore errors during deletion\r\n";
            this.cbIgnoreErrors.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 27);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 17);
            this.label15.TabIndex = 14;
            this.label15.Text = "Delete mode:";
            // 
            // cbDeleteMode
            // 
            this.cbDeleteMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDeleteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeleteMode.FormattingEnabled = true;
            this.cbDeleteMode.Location = new System.Drawing.Point(103, 22);
            this.cbDeleteMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDeleteMode.Name = "cbDeleteMode";
            this.cbDeleteMode.Size = new System.Drawing.Size(659, 24);
            this.cbDeleteMode.TabIndex = 7;
            // 
            // cbClipboardDetection
            // 
            this.cbClipboardDetection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClipboardDetection.AutoSize = true;
            this.cbClipboardDetection.Location = new System.Drawing.Point(365, 59);
            this.cbClipboardDetection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbClipboardDetection.Name = "cbClipboardDetection";
            this.cbClipboardDetection.Size = new System.Drawing.Size(211, 21);
            this.cbClipboardDetection.TabIndex = 5;
            this.cbClipboardDetection.Tag = "clipboard_detection";
            this.cbClipboardDetection.Text = "Detect paths in the clipboard";
            this.cbClipboardDetection.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreHiddenFolders
            // 
            this.cbIgnoreHiddenFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIgnoreHiddenFolders.AutoSize = true;
            this.cbIgnoreHiddenFolders.Location = new System.Drawing.Point(599, 59);
            this.cbIgnoreHiddenFolders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIgnoreHiddenFolders.Name = "cbIgnoreHiddenFolders";
            this.cbIgnoreHiddenFolders.Size = new System.Drawing.Size(164, 21);
            this.cbIgnoreHiddenFolders.TabIndex = 4;
            this.cbIgnoreHiddenFolders.Tag = "ignore_hidden";
            this.cbIgnoreHiddenFolders.Text = "Ignore hidden folders";
            this.cbIgnoreHiddenFolders.UseVisualStyleBackColor = true;
            // 
            // cbKeepSystemFolders
            // 
            this.cbKeepSystemFolders.AutoSize = true;
            this.cbKeepSystemFolders.Checked = true;
            this.cbKeepSystemFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepSystemFolders.Location = new System.Drawing.Point(13, 87);
            this.cbKeepSystemFolders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbKeepSystemFolders.Name = "cbKeepSystemFolders";
            this.cbKeepSystemFolders.Size = new System.Drawing.Size(279, 21);
            this.cbKeepSystemFolders.TabIndex = 2;
            this.cbKeepSystemFolders.Tag = "keep_system_dirs";
            this.cbKeepSystemFolders.Text = "Skip system directories (recommended)";
            this.cbKeepSystemFolders.UseVisualStyleBackColor = true;
            // 
            // cbIgnore0kbFiles
            // 
            this.cbIgnore0kbFiles.AutoSize = true;
            this.cbIgnore0kbFiles.Checked = true;
            this.cbIgnore0kbFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnore0kbFiles.Location = new System.Drawing.Point(13, 59);
            this.cbIgnore0kbFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIgnore0kbFiles.Name = "cbIgnore0kbFiles";
            this.cbIgnore0kbFiles.Size = new System.Drawing.Size(342, 21);
            this.cbIgnore0kbFiles.TabIndex = 3;
            this.cbIgnore0kbFiles.Tag = "ignore_0kb_files";
            this.cbIgnore0kbFiles.Text = "Directories with files that are 0 KB count as empty";
            this.cbIgnore0kbFiles.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.tbIgnoreFolders);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(416, 138);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(376, 227);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Skip these directories";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label11.Location = new System.Drawing.Point(11, 204);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(359, 17);
            this.label11.TabIndex = 12;
            this.label11.Text = "(One pattern per line - Directory name or path possible)";
            // 
            // tbIgnoreFolders
            // 
            this.tbIgnoreFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIgnoreFolders.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIgnoreFolders.Location = new System.Drawing.Point(11, 44);
            this.tbIgnoreFolders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbIgnoreFolders.Multiline = true;
            this.tbIgnoreFolders.Name = "tbIgnoreFolders";
            this.tbIgnoreFolders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIgnoreFolders.Size = new System.Drawing.Size(353, 155);
            this.tbIgnoreFolders.TabIndex = 6;
            this.tbIgnoreFolders.WordWrap = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(299, 17);
            this.label10.TabIndex = 13;
            this.label10.Text = "Ignore these directory names when searching:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbIgnoreFiles);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(19, 138);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(376, 227);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ignore these files";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.DarkRed;
            this.label14.Location = new System.Drawing.Point(9, 59);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(280, 34);
            this.label14.TabIndex = 12;
            this.label14.Text = "Be careful: A bad pattern could potentially\r\ncause accidental deletion of importa" +
    "nt files.";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(9, 204);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(362, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "(One pattern per line - Regex possible -> see about tab)";
            // 
            // tbIgnoreFiles
            // 
            this.tbIgnoreFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIgnoreFiles.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIgnoreFiles.Location = new System.Drawing.Point(9, 95);
            this.tbIgnoreFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbIgnoreFiles.Multiline = true;
            this.tbIgnoreFiles.Name = "tbIgnoreFiles";
            this.tbIgnoreFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIgnoreFiles.Size = new System.Drawing.Size(357, 105);
            this.tbIgnoreFiles.TabIndex = 5;
            this.tbIgnoreFiles.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 34);
            this.label2.TabIndex = 10;
            this.label2.Text = "Treat directories as empty if they only contain \r\nfiles that match the following " +
    "patterns";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.nuFolderAge);
            this.groupBox3.Controls.Add(this.btnCopyDebugInfo);
            this.groupBox3.Controls.Add(this.btnResetConfig);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.nuInfiniteLoopDetectionCount);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.nuMaxDepth);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.nuPause);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(19, 460);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(773, 141);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advanced";
            // 
            // btnCopyDebugInfo
            // 
            this.btnCopyDebugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyDebugInfo.Location = new System.Drawing.Point(617, 58);
            this.btnCopyDebugInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCopyDebugInfo.Name = "btnCopyDebugInfo";
            this.btnCopyDebugInfo.Size = new System.Drawing.Size(136, 33);
            this.btnCopyDebugInfo.TabIndex = 24;
            this.btnCopyDebugInfo.Text = "Copy debug info";
            this.btnCopyDebugInfo.UseVisualStyleBackColor = true;
            this.btnCopyDebugInfo.Click += new System.EventHandler(this.btnCopyDebugInfo_Click);
            // 
            // btnResetConfig
            // 
            this.btnResetConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetConfig.Location = new System.Drawing.Point(401, 58);
            this.btnResetConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetConfig.Name = "btnResetConfig";
            this.btnResetConfig.Size = new System.Drawing.Size(213, 33);
            this.btnResetConfig.TabIndex = 23;
            this.btnResetConfig.Text = "Reset to default settings";
            this.btnResetConfig.UseVisualStyleBackColor = true;
            this.btnResetConfig.Click += new System.EventHandler(this.btnResetConfig_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(695, 28);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 17);
            this.label13.TabIndex = 22;
            this.label13.Text = "errors";
            // 
            // nuInfiniteLoopDetectionCount
            // 
            this.nuInfiniteLoopDetectionCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nuInfiniteLoopDetectionCount.Location = new System.Drawing.Point(617, 23);
            this.nuInfiniteLoopDetectionCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nuInfiniteLoopDetectionCount.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuInfiniteLoopDetectionCount.Name = "nuInfiniteLoopDetectionCount";
            this.nuInfiniteLoopDetectionCount.Size = new System.Drawing.Size(71, 22);
            this.nuInfiniteLoopDetectionCount.TabIndex = 21;
            this.nuInfiniteLoopDetectionCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Infinite-loop detection: Stop after\r\n";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label20.Location = new System.Drawing.Point(11, 78);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(362, 17);
            this.label20.TabIndex = 19;
            this.label20.Text = "(Gives you time to stop the process - but not necessary)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Max directory nesting depth (-1 = infinite):";
            // 
            // nuMaxDepth
            // 
            this.nuMaxDepth.Location = new System.Drawing.Point(281, 21);
            this.nuMaxDepth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.nuMaxDepth.Size = new System.Drawing.Size(60, 22);
            this.nuMaxDepth.TabIndex = 7;
            this.nuMaxDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 54);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "ms";
            // 
            // nuPause
            // 
            this.nuPause.Location = new System.Drawing.Point(281, 50);
            this.nuPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nuPause.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuPause.Name = "nuPause";
            this.nuPause.Size = new System.Drawing.Size(61, 22);
            this.nuPause.TabIndex = 8;
            this.nuPause.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Pause between each delete process:";
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.linkLabel2);
            this.tabAbout.Controls.Add(this.linkLabel1);
            this.tabAbout.Controls.Add(this.tbCredits);
            this.tabAbout.Controls.Add(this.lblRedStats);
            this.tabAbout.Controls.Add(this.label5);
            this.tabAbout.Controls.Add(this.llWebsite);
            this.tabAbout.Controls.Add(this.lbAppTitle);
            this.tabAbout.ImageKey = "help";
            this.tabAbout.Location = new System.Drawing.Point(4, 25);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(813, 615);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.ToolTipText = "Shows the help and about screen";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(621, 58);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(165, 17);
            this.linkLabel2.TabIndex = 27;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Feedback / Report a bug";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(667, 33);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(123, 17);
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
            this.tbCredits.Location = new System.Drawing.Point(15, 82);
            this.tbCredits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCredits.Multiline = true;
            this.tbCredits.Name = "tbCredits";
            this.tbCredits.ReadOnly = true;
            this.tbCredits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCredits.Size = new System.Drawing.Size(776, 514);
            this.tbCredits.TabIndex = 25;
            this.tbCredits.WordWrap = false;
            // 
            // lblRedStats
            // 
            this.lblRedStats.AutoSize = true;
            this.lblRedStats.Location = new System.Drawing.Point(11, 57);
            this.lblRedStats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRedStats.Name = "lblRedStats";
            this.lblRedStats.Size = new System.Drawing.Size(221, 17);
            this.lblRedStats.TabIndex = 20;
            this.lblRedStats.Text = "RED deleted 0 directories for you!";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Copyright © 2005 - 2011 Jonas John";
            // 
            // llWebsite
            // 
            this.llWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llWebsite.AutoSize = true;
            this.llWebsite.Location = new System.Drawing.Point(679, 10);
            this.llWebsite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(110, 17);
            this.llWebsite.TabIndex = 19;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "RED Homepage\r\n";
            this.llWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebsite_LinkClicked);
            // 
            // lbAppTitle
            // 
            this.lbAppTitle.AutoSize = true;
            this.lbAppTitle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbAppTitle.Location = new System.Drawing.Point(11, 10);
            this.lbAppTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAppTitle.Name = "lbAppTitle";
            this.lbAppTitle.Size = new System.Drawing.Size(264, 16);
            this.lbAppTitle.TabIndex = 0;
            this.lbAppTitle.Text = "Remove Empty Directories - Version ";
            // 
            // nuFolderAge
            // 
            this.nuFolderAge.Location = new System.Drawing.Point(281, 104);
            this.nuFolderAge.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.nuFolderAge.Name = "nuFolderAge";
            this.nuFolderAge.Size = new System.Drawing.Size(61, 22);
            this.nuFolderAge.TabIndex = 25;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 109);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(224, 17);
            this.label19.TabIndex = 26;
            this.label19.Text = "Skip folders less than N hours old:";
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 665);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpOptions.ResumeLayout(false);
            this.gpOptions.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuInfiniteLoopDetectionCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).EndInit();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuFolderAge)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tabSettings;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gpOptions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbIgnoreFolders;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
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
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nuInfiniteLoopDetectionCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnResetConfig;
        private System.Windows.Forms.ToolStripMenuItem scanOnlyThisDirectoryToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExplorerRemove;
        private System.Windows.Forms.Button btnExplorerIntegrate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblReqAdmin;
        private System.Windows.Forms.CheckBox cbHideScanErrors;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btnCopyDebugInfo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nuFolderAge;
	}
}

