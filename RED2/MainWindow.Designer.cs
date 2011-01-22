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
            this.cbIntegrateIntoWindowsExplorer = new System.Windows.Forms.CheckBox();
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.protectFolderFromBeingDeletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unprotectFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.proToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.lblPickAFolder = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbIgnoreFolders = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnShowConfig = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nuMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nuPause = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbIgnoreFiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gpOptions = new System.Windows.Forms.GroupBox();
            this.cbClipboardDetection = new System.Windows.Forms.CheckBox();
            this.cbSimulateDeletion = new System.Windows.Forms.CheckBox();
            this.cbIgnoreHiddenFolders = new System.Windows.Forms.CheckBox();
            this.cbKeepSystemFolders = new System.Windows.Forms.CheckBox();
            this.cbIgnore0kbFiles = new System.Windows.Forms.CheckBox();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.btnCheckForUpdates = new System.Windows.Forms.Button();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.lblRedStats = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.lbAppTitle = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.cbDeleteMode = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tcMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.pnlIcons.SuspendLayout();
            this.cmStrip.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gpOptions.SuspendLayout();
            this.tabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbIntegrateIntoWindowsExplorer
            // 
            this.cbIntegrateIntoWindowsExplorer.AutoSize = true;
            this.cbIntegrateIntoWindowsExplorer.Location = new System.Drawing.Point(16, 61);
            this.cbIntegrateIntoWindowsExplorer.Name = "cbIntegrateIntoWindowsExplorer";
            this.cbIntegrateIntoWindowsExplorer.Size = new System.Drawing.Size(287, 17);
            this.cbIntegrateIntoWindowsExplorer.TabIndex = 1;
            this.cbIntegrateIntoWindowsExplorer.Tag = "explorer_integration";
            this.cbIntegrateIntoWindowsExplorer.Text = "Integrate RED into the Windows Explorer context menu";
            this.cbIntegrateIntoWindowsExplorer.UseVisualStyleBackColor = true;
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
            this.tcMain.Location = new System.Drawing.Point(7, 6);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tcMain.SelectedIndex = 0;
            this.tcMain.ShowToolTips = true;
            this.tcMain.Size = new System.Drawing.Size(578, 523);
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
            this.tabSearch.Location = new System.Drawing.Point(4, 23);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch.Size = new System.Drawing.Size(570, 496);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Scan";
            this.tabSearch.ToolTipText = "Search for empty directories";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // btnShowLog
            // 
            this.btnShowLog.Location = new System.Drawing.Point(336, 452);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(73, 34);
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
            this.btnCancel.Location = new System.Drawing.Point(232, 452);
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
            this.btnScan.Location = new System.Drawing.Point(10, 452);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(98, 35);
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
            this.btnDelete.Location = new System.Drawing.Point(114, 452);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 35);
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
            this.pnlIcons.Location = new System.Drawing.Point(447, 58);
            this.pnlIcons.Name = "pnlIcons";
            this.pnlIcons.Size = new System.Drawing.Size(115, 362);
            this.pnlIcons.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Protected";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Blue;
            this.panel5.Location = new System.Drawing.Point(8, 337);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(15, 15);
            this.panel5.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(8, 281);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(98, 2);
            this.panel3.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 315);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "Will be deleted";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Location = new System.Drawing.Point(8, 314);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(15, 15);
            this.panel2.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(25, 292);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Will not touched";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(8, 291);
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
            this.label12.Size = new System.Drawing.Size(108, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Icon descriptions:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Please choose a folder to scan:";
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(166, 425);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 13);
            this.lbStatus.TabIndex = 13;
            // 
            // pbProgressStatus
            // 
            this.pbProgressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbProgressStatus.Location = new System.Drawing.Point(10, 425);
            this.pbProgressStatus.Name = "pbProgressStatus";
            this.pbProgressStatus.Size = new System.Drawing.Size(153, 13);
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
            this.tvFolders.Location = new System.Drawing.Point(9, 58);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageKey = "folder";
            this.tvFolders.Size = new System.Drawing.Size(432, 362);
            this.tvFolders.TabIndex = 3;
            this.tvFolders.DoubleClick += new System.EventHandler(this.tvFolders_DoubleClick);
            this.tvFolders.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseClick);
            // 
            // cmStrip
            // 
            this.cmStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.protectFolderFromBeingDeletedToolStripMenuItem,
            this.unprotectFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.proToolStripMenuItem});
            this.cmStrip.Name = "cmStrip";
            this.cmStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmStrip.ShowImageMargin = false;
            this.cmStrip.Size = new System.Drawing.Size(188, 104);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openFolderToolStripMenuItem.Text = "&Open folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // protectFolderFromBeingDeletedToolStripMenuItem
            // 
            this.protectFolderFromBeingDeletedToolStripMenuItem.Name = "protectFolderFromBeingDeletedToolStripMenuItem";
            this.protectFolderFromBeingDeletedToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.protectFolderFromBeingDeletedToolStripMenuItem.Text = "Protect folder (once)";
            this.protectFolderFromBeingDeletedToolStripMenuItem.Click += new System.EventHandler(this.protectFolderFromBeingDeletedToolStripMenuItem_Click);
            // 
            // unprotectFolderToolStripMenuItem
            // 
            this.unprotectFolderToolStripMenuItem.Name = "unprotectFolderToolStripMenuItem";
            this.unprotectFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.unprotectFolderToolStripMenuItem.Text = "Unprotect folder";
            this.unprotectFolderToolStripMenuItem.Click += new System.EventHandler(this.unprotectFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // proToolStripMenuItem
            // 
            this.proToolStripMenuItem.Name = "proToolStripMenuItem";
            this.proToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.proToolStripMenuItem.Text = "Protect folder (every time)";
            this.proToolStripMenuItem.Click += new System.EventHandler(this.proToolStripMenuItem_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.ImageKey = "exit";
            this.btnExit.ImageList = this.ilFolderIcons;
            this.btnExit.Location = new System.Drawing.Point(447, 452);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(115, 34);
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
            this.tbFolder.Location = new System.Drawing.Point(9, 30);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(486, 20);
            this.tbFolder.TabIndex = 1;
            this.tbFolder.Text = "C:\\";
            this.tbFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbFolder_MouseDoubleClick);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseFolder.Location = new System.Drawing.Point(499, 30);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(63, 20);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "Browse...";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // lblPickAFolder
            // 
            this.lblPickAFolder.AutoSize = true;
            this.lblPickAFolder.Location = new System.Drawing.Point(6, 13);
            this.lblPickAFolder.Name = "lblPickAFolder";
            this.lblPickAFolder.Size = new System.Drawing.Size(0, 13);
            this.lblPickAFolder.TabIndex = 3;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.gpOptions);
            this.tabSettings.Controls.Add(this.groupBox4);
            this.tabSettings.Controls.Add(this.groupBox3);
            this.tabSettings.Controls.Add(this.groupBox2);
            this.tabSettings.ImageKey = "preferences";
            this.tabSettings.Location = new System.Drawing.Point(4, 23);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(570, 496);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.ToolTipText = "Application settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.tbIgnoreFolders);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(254, 161);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(294, 221);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Skip these folders";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label11.Location = new System.Drawing.Point(13, 199);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(154, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "(Put each pattern in a new line)";
            // 
            // tbIgnoreFolders
            // 
            this.tbIgnoreFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIgnoreFolders.Location = new System.Drawing.Point(16, 47);
            this.tbIgnoreFolders.Multiline = true;
            this.tbIgnoreFolders.Name = "tbIgnoreFolders";
            this.tbIgnoreFolders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIgnoreFolders.Size = new System.Drawing.Size(260, 149);
            this.tbIgnoreFolders.TabIndex = 6;
            this.tbIgnoreFolders.WordWrap = false;
            this.tbIgnoreFolders.TextChanged += new System.EventHandler(this.tbIgnoreFolders_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Ignore these folders:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnShowConfig);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.nuMaxDepth);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.nuPause);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(15, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(533, 93);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Advanced";
            // 
            // btnShowConfig
            // 
            this.btnShowConfig.Location = new System.Drawing.Point(11, 64);
            this.btnShowConfig.Name = "btnShowConfig";
            this.btnShowConfig.Size = new System.Drawing.Size(181, 23);
            this.btnShowConfig.TabIndex = 7;
            this.btnShowConfig.Text = "Show config file";
            this.btnShowConfig.UseVisualStyleBackColor = true;
            this.btnShowConfig.Click += new System.EventHandler(this.btnShowConfig_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Max depth (-1 = infinite):";
            // 
            // nuMaxDepth
            // 
            this.nuMaxDepth.Location = new System.Drawing.Point(13, 38);
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
            this.nuMaxDepth.Size = new System.Drawing.Size(176, 20);
            this.nuMaxDepth.TabIndex = 7;
            this.nuMaxDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nuMaxDepth.ValueChanged += new System.EventHandler(this.nuMaxDepth_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(378, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "milli seconds";
            // 
            // nuPause
            // 
            this.nuPause.Location = new System.Drawing.Point(255, 38);
            this.nuPause.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nuPause.Name = "nuPause";
            this.nuPause.Size = new System.Drawing.Size(120, 20);
            this.nuPause.TabIndex = 8;
            this.nuPause.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nuPause.ValueChanged += new System.EventHandler(this.nuPause_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(252, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Pause between each delete process:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbIgnoreFiles);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(15, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 221);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ignore these files";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(6, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "(Put each pattern in a new line)";
            // 
            // tbIgnoreFiles
            // 
            this.tbIgnoreFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIgnoreFiles.Location = new System.Drawing.Point(11, 47);
            this.tbIgnoreFiles.Multiline = true;
            this.tbIgnoreFiles.Name = "tbIgnoreFiles";
            this.tbIgnoreFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIgnoreFiles.Size = new System.Drawing.Size(196, 149);
            this.tbIgnoreFiles.TabIndex = 5;
            this.tbIgnoreFiles.WordWrap = false;
            this.tbIgnoreFiles.TextChanged += new System.EventHandler(this.tbIgnoreFiles_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = "Mark folders as empty if they only contain \r\nfiles that matches these patterns:";
            // 
            // gpOptions
            // 
            this.gpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpOptions.Controls.Add(this.label15);
            this.gpOptions.Controls.Add(this.cbDeleteMode);
            this.gpOptions.Controls.Add(this.cbClipboardDetection);
            this.gpOptions.Controls.Add(this.cbSimulateDeletion);
            this.gpOptions.Controls.Add(this.cbIntegrateIntoWindowsExplorer);
            this.gpOptions.Controls.Add(this.cbIgnoreHiddenFolders);
            this.gpOptions.Controls.Add(this.cbKeepSystemFolders);
            this.gpOptions.Controls.Add(this.cbIgnore0kbFiles);
            this.gpOptions.Location = new System.Drawing.Point(10, 6);
            this.gpOptions.Name = "gpOptions";
            this.gpOptions.Size = new System.Drawing.Size(533, 149);
            this.gpOptions.TabIndex = 19;
            this.gpOptions.TabStop = false;
            this.gpOptions.Text = "General options";
            // 
            // cbClipboardDetection
            // 
            this.cbClipboardDetection.AutoSize = true;
            this.cbClipboardDetection.Checked = true;
            this.cbClipboardDetection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbClipboardDetection.Location = new System.Drawing.Point(337, 87);
            this.cbClipboardDetection.Name = "cbClipboardDetection";
            this.cbClipboardDetection.Size = new System.Drawing.Size(162, 17);
            this.cbClipboardDetection.TabIndex = 5;
            this.cbClipboardDetection.Tag = "clipboard_detection";
            this.cbClipboardDetection.Text = "Detect paths in the clipboard";
            this.cbClipboardDetection.UseVisualStyleBackColor = true;
            // 
            // cbSimulateDeletion
            // 
            this.cbSimulateDeletion.AutoSize = true;
            this.cbSimulateDeletion.Checked = true;
            this.cbSimulateDeletion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSimulateDeletion.Location = new System.Drawing.Point(337, 113);
            this.cbSimulateDeletion.Name = "cbSimulateDeletion";
            this.cbSimulateDeletion.Size = new System.Drawing.Size(182, 17);
            this.cbSimulateDeletion.TabIndex = 1;
            this.cbSimulateDeletion.Tag = "simulate_deletion";
            this.cbSimulateDeletion.Text = "Simulate deletion (for Debugging)";
            this.cbSimulateDeletion.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreHiddenFolders
            // 
            this.cbIgnoreHiddenFolders.AutoSize = true;
            this.cbIgnoreHiddenFolders.Location = new System.Drawing.Point(337, 61);
            this.cbIgnoreHiddenFolders.Name = "cbIgnoreHiddenFolders";
            this.cbIgnoreHiddenFolders.Size = new System.Drawing.Size(146, 17);
            this.cbIgnoreHiddenFolders.TabIndex = 4;
            this.cbIgnoreHiddenFolders.Tag = "ignore_hidden";
            this.cbIgnoreHiddenFolders.Text = "Don\'t scan hidden folders";
            this.cbIgnoreHiddenFolders.UseVisualStyleBackColor = true;
            // 
            // cbKeepSystemFolders
            // 
            this.cbKeepSystemFolders.AutoSize = true;
            this.cbKeepSystemFolders.Checked = true;
            this.cbKeepSystemFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepSystemFolders.Location = new System.Drawing.Point(16, 113);
            this.cbKeepSystemFolders.Name = "cbKeepSystemFolders";
            this.cbKeepSystemFolders.Size = new System.Drawing.Size(213, 17);
            this.cbKeepSystemFolders.TabIndex = 2;
            this.cbKeepSystemFolders.Tag = "keep_system_dirs";
            this.cbKeepSystemFolders.Text = "Keep system directories (recommended)";
            this.cbKeepSystemFolders.UseVisualStyleBackColor = true;
            // 
            // cbIgnore0kbFiles
            // 
            this.cbIgnore0kbFiles.AutoSize = true;
            this.cbIgnore0kbFiles.Checked = true;
            this.cbIgnore0kbFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnore0kbFiles.Location = new System.Drawing.Point(16, 87);
            this.cbIgnore0kbFiles.Name = "cbIgnore0kbFiles";
            this.cbIgnore0kbFiles.Size = new System.Drawing.Size(272, 17);
            this.cbIgnore0kbFiles.TabIndex = 3;
            this.cbIgnore0kbFiles.Tag = "ignore_0kb_files";
            this.cbIgnore0kbFiles.Text = "Directories with files that are all 0 KB count as empty";
            this.cbIgnore0kbFiles.UseVisualStyleBackColor = true;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.btnCheckForUpdates);
            this.tabAbout.Controls.Add(this.linkLabel3);
            this.tabAbout.Controls.Add(this.linkLabel4);
            this.tabAbout.Controls.Add(this.lblRedStats);
            this.tabAbout.Controls.Add(this.panel4);
            this.tabAbout.Controls.Add(this.label16);
            this.tabAbout.Controls.Add(this.label19);
            this.tabAbout.Controls.Add(this.label14);
            this.tabAbout.Controls.Add(this.label13);
            this.tabAbout.Controls.Add(this.pictureBox1);
            this.tabAbout.Controls.Add(this.label5);
            this.tabAbout.Controls.Add(this.label4);
            this.tabAbout.Controls.Add(this.llWebsite);
            this.tabAbout.Controls.Add(this.lbAppTitle);
            this.tabAbout.Controls.Add(this.linkLabel1);
            this.tabAbout.Controls.Add(this.linkLabel2);
            this.tabAbout.ImageKey = "help";
            this.tabAbout.Location = new System.Drawing.Point(4, 23);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(570, 506);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.ToolTipText = "Shows the help and about screen";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // btnCheckForUpdates
            // 
            this.btnCheckForUpdates.Location = new System.Drawing.Point(414, 11);
            this.btnCheckForUpdates.Name = "btnCheckForUpdates";
            this.btnCheckForUpdates.Size = new System.Drawing.Size(134, 39);
            this.btnCheckForUpdates.TabIndex = 24;
            this.btnCheckForUpdates.Text = "&Check for updates... (opens browser)";
            this.btnCheckForUpdates.UseVisualStyleBackColor = true;
            this.btnCheckForUpdates.Click += new System.EventHandler(this.btnCheckForUpdates_Click);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(23, 252);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(179, 13);
            this.linkLabel3.TabIndex = 23;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Coffee icon by victorward (at sxc.hu)";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(23, 200);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(158, 13);
            this.linkLabel4.TabIndex = 21;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "NuoveXT (Icons - GPL License)";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // lblRedStats
            // 
            this.lblRedStats.AutoSize = true;
            this.lblRedStats.Location = new System.Drawing.Point(118, 106);
            this.lblRedStats.Name = "lblRedStats";
            this.lblRedStats.Size = new System.Drawing.Size(166, 13);
            this.lblRedStats.TabIndex = 20;
            this.lblRedStats.Text = "RED deleted 0 directories for you!";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Location = new System.Drawing.Point(18, 137);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(530, 2);
            this.panel4.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(15, 290);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 14);
            this.label16.TabIndex = 12;
            this.label16.Text = "Hints:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(23, 311);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(206, 13);
            this.label19.TabIndex = 11;
            this.label19.Text = "Right click on a folder to get more options.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(118, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(176, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "RED is Open Source (GPL License)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(15, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 14);
            this.label13.TabIndex = 7;
            this.label13.Text = "Credits (Things used):";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(18, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 100);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(118, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "Copyright © 2006 - 2011 Jonas John";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Website:";
            // 
            // llWebsite
            // 
            this.llWebsite.AutoSize = true;
            this.llWebsite.Location = new System.Drawing.Point(164, 78);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(94, 13);
            this.llWebsite.TabIndex = 19;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "www.jonasjohn.de";
            this.llWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebsite_LinkClicked);
            // 
            // lbAppTitle
            // 
            this.lbAppTitle.AutoSize = true;
            this.lbAppTitle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbAppTitle.Location = new System.Drawing.Point(118, 18);
            this.lbAppTitle.Name = "lbAppTitle";
            this.lbAppTitle.Size = new System.Drawing.Size(209, 14);
            this.lbAppTitle.TabIndex = 0;
            this.lbAppTitle.Text = "Remove Empty Directories - Version ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(23, 226);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(253, 13);
            this.linkLabel1.TabIndex = 22;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "famfamfam.com (Icons - CC - Attribution 2.5 License)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(23, 176);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(152, 13);
            this.linkLabel2.TabIndex = 20;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Nuvola (Icons - LGPL License)";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // cbDeleteMode
            // 
            this.cbDeleteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeleteMode.FormattingEnabled = true;
            this.cbDeleteMode.Location = new System.Drawing.Point(97, 25);
            this.cbDeleteMode.Name = "cbDeleteMode";
            this.cbDeleteMode.Size = new System.Drawing.Size(423, 21);
            this.cbDeleteMode.TabIndex = 7;
            this.cbDeleteMode.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Delete mode:";
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 540);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuPause)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpOptions.ResumeLayout(false);
            this.gpOptions.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.CheckBox cbIntegrateIntoWindowsExplorer;
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
		private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblRedStats;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Button btnCheckForUpdates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ContextMenuStrip cmStrip;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem protectFolderFromBeingDeletedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unprotectFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem proToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbSimulateDeletion;
        private System.Windows.Forms.CheckBox cbClipboardDetection;
        private System.Windows.Forms.Button btnShowConfig;
        private System.Windows.Forms.Button btnShowLog;
        private System.Windows.Forms.ComboBox cbDeleteMode;
        private System.Windows.Forms.Label label15;
	}
}

