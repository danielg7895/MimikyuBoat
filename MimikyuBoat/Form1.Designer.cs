namespace MimikyuBoat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.playerPanelContainer = new System.Windows.Forms.Panel();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.playerStatsMarker = new System.Windows.Forms.Panel();
            this.playerImage = new System.Windows.Forms.PictureBox();
            this.targetPanelContainer = new System.Windows.Forms.Panel();
            this.targetPanel = new System.Windows.Forms.Panel();
            this.targetStatsMarker = new System.Windows.Forms.Panel();
            this.targetImage = new System.Windows.Forms.PictureBox();
            this.playerButtonAreaConfig = new System.Windows.Forms.Button();
            this.targetButtonAreaConfig = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.consoleDebugTextBox = new System.Windows.Forms.TextBox();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.statsConfigButton = new System.Windows.Forms.Button();
            this.statsConfigLabel = new System.Windows.Forms.Label();
            this.statsSaveButton = new System.Windows.Forms.Button();
            this.statsCancelButton = new System.Windows.Forms.Button();
            this.zoneComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.recoverMPStandTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.recoverMPCheckBox = new System.Windows.Forms.CheckBox();
            this.recoverMPSitTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.autoPotCheckBox = new System.Windows.Forms.CheckBox();
            this.autoPotHPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.windowVisibilityCheckBox = new System.Windows.Forms.CheckBox();
            this.TabContainer = new System.Windows.Forms.TabControl();
            this.botTabPage = new System.Windows.Forms.TabPage();
            this.skillConfigTabPage = new System.Windows.Forms.TabPage();
            this.updateIntervalUpDown = new System.Windows.Forms.NumericUpDown();
            this.playerPanelContainer.SuspendLayout();
            this.playerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerImage)).BeginInit();
            this.targetPanelContainer.SuspendLayout();
            this.targetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetImage)).BeginInit();
            this.containerPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TabContainer.SuspendLayout();
            this.botTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // playerPanelContainer
            // 
            this.playerPanelContainer.BackColor = System.Drawing.Color.AliceBlue;
            this.playerPanelContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playerPanelContainer.Controls.Add(this.playerPanel);
            this.playerPanelContainer.Location = new System.Drawing.Point(3, 29);
            this.playerPanelContainer.Name = "playerPanelContainer";
            this.playerPanelContainer.Size = new System.Drawing.Size(224, 100);
            this.playerPanelContainer.TabIndex = 0;
            // 
            // playerPanel
            // 
            this.playerPanel.BackColor = System.Drawing.Color.Transparent;
            this.playerPanel.Controls.Add(this.playerStatsMarker);
            this.playerPanel.Controls.Add(this.playerImage);
            this.playerPanel.Location = new System.Drawing.Point(40, 14);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(123, 72);
            this.playerPanel.TabIndex = 20;
            // 
            // playerStatsMarker
            // 
            this.playerStatsMarker.BackColor = System.Drawing.Color.Cyan;
            this.playerStatsMarker.Location = new System.Drawing.Point(3, 28);
            this.playerStatsMarker.Name = "playerStatsMarker";
            this.playerStatsMarker.Size = new System.Drawing.Size(120, 10);
            this.playerStatsMarker.TabIndex = 19;
            // 
            // playerImage
            // 
            this.playerImage.Location = new System.Drawing.Point(1, 1);
            this.playerImage.Name = "playerImage";
            this.playerImage.Size = new System.Drawing.Size(72, 37);
            this.playerImage.TabIndex = 0;
            this.playerImage.TabStop = false;
            // 
            // targetPanelContainer
            // 
            this.targetPanelContainer.BackColor = System.Drawing.Color.AliceBlue;
            this.targetPanelContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.targetPanelContainer.Controls.Add(this.targetPanel);
            this.targetPanelContainer.Location = new System.Drawing.Point(439, 29);
            this.targetPanelContainer.Name = "targetPanelContainer";
            this.targetPanelContainer.Size = new System.Drawing.Size(224, 100);
            this.targetPanelContainer.TabIndex = 1;
            // 
            // targetPanel
            // 
            this.targetPanel.BackColor = System.Drawing.Color.Transparent;
            this.targetPanel.Controls.Add(this.targetStatsMarker);
            this.targetPanel.Controls.Add(this.targetImage);
            this.targetPanel.Location = new System.Drawing.Point(65, 15);
            this.targetPanel.Name = "targetPanel";
            this.targetPanel.Size = new System.Drawing.Size(123, 72);
            this.targetPanel.TabIndex = 21;
            // 
            // targetStatsMarker
            // 
            this.targetStatsMarker.BackColor = System.Drawing.Color.Cyan;
            this.targetStatsMarker.Location = new System.Drawing.Point(3, 28);
            this.targetStatsMarker.Name = "targetStatsMarker";
            this.targetStatsMarker.Size = new System.Drawing.Size(120, 10);
            this.targetStatsMarker.TabIndex = 19;
            // 
            // targetImage
            // 
            this.targetImage.Location = new System.Drawing.Point(1, 1);
            this.targetImage.Name = "targetImage";
            this.targetImage.Size = new System.Drawing.Size(72, 37);
            this.targetImage.TabIndex = 0;
            this.targetImage.TabStop = false;
            // 
            // playerButtonAreaConfig
            // 
            this.playerButtonAreaConfig.BackColor = System.Drawing.Color.White;
            this.playerButtonAreaConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.playerButtonAreaConfig.FlatAppearance.BorderSize = 2;
            this.playerButtonAreaConfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.playerButtonAreaConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playerButtonAreaConfig.Location = new System.Drawing.Point(3, 135);
            this.playerButtonAreaConfig.Name = "playerButtonAreaConfig";
            this.playerButtonAreaConfig.Size = new System.Drawing.Size(105, 36);
            this.playerButtonAreaConfig.TabIndex = 2;
            this.playerButtonAreaConfig.Text = "Configurar area";
            this.playerButtonAreaConfig.UseVisualStyleBackColor = false;
            this.playerButtonAreaConfig.Click += new System.EventHandler(this.PlayerButtonAreaConfig_Click);
            // 
            // targetButtonAreaConfig
            // 
            this.targetButtonAreaConfig.BackColor = System.Drawing.Color.White;
            this.targetButtonAreaConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.targetButtonAreaConfig.FlatAppearance.BorderSize = 2;
            this.targetButtonAreaConfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.targetButtonAreaConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.targetButtonAreaConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.targetButtonAreaConfig.Location = new System.Drawing.Point(439, 135);
            this.targetButtonAreaConfig.Name = "targetButtonAreaConfig";
            this.targetButtonAreaConfig.Size = new System.Drawing.Size(105, 36);
            this.targetButtonAreaConfig.TabIndex = 3;
            this.targetButtonAreaConfig.Text = "Configurar area";
            this.targetButtonAreaConfig.UseVisualStyleBackColor = false;
            this.targetButtonAreaConfig.Click += new System.EventHandler(this.TargetButtonAreaConfig_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.AliceBlue;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.startButton.FlatAppearance.BorderSize = 4;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Papyrus", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(212, 415);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(221, 57);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // consoleDebugTextBox
            // 
            this.consoleDebugTextBox.BackColor = System.Drawing.Color.Black;
            this.consoleDebugTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleDebugTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(152)))), ((int)(((byte)(203)))));
            this.consoleDebugTextBox.Location = new System.Drawing.Point(3, 478);
            this.consoleDebugTextBox.Multiline = true;
            this.consoleDebugTextBox.Name = "consoleDebugTextBox";
            this.consoleDebugTextBox.ReadOnly = true;
            this.consoleDebugTextBox.Size = new System.Drawing.Size(660, 125);
            this.consoleDebugTextBox.TabIndex = 7;
            // 
            // containerPanel
            // 
            this.containerPanel.BackColor = System.Drawing.Color.White;
            this.containerPanel.Controls.Add(this.updateIntervalUpDown);
            this.containerPanel.Controls.Add(this.label6);
            this.containerPanel.Controls.Add(this.statsConfigButton);
            this.containerPanel.Controls.Add(this.statsConfigLabel);
            this.containerPanel.Controls.Add(this.statsSaveButton);
            this.containerPanel.Controls.Add(this.statsCancelButton);
            this.containerPanel.Controls.Add(this.zoneComboBox);
            this.containerPanel.Controls.Add(this.label4);
            this.containerPanel.Controls.Add(this.label5);
            this.containerPanel.Controls.Add(this.panel2);
            this.containerPanel.Controls.Add(this.panel1);
            this.containerPanel.Controls.Add(this.checkedListBox1);
            this.containerPanel.Controls.Add(this.windowVisibilityCheckBox);
            this.containerPanel.Controls.Add(this.playerPanelContainer);
            this.containerPanel.Controls.Add(this.playerButtonAreaConfig);
            this.containerPanel.Controls.Add(this.consoleDebugTextBox);
            this.containerPanel.Controls.Add(this.targetPanelContainer);
            this.containerPanel.Controls.Add(this.startButton);
            this.containerPanel.Controls.Add(this.targetButtonAreaConfig);
            this.containerPanel.Location = new System.Drawing.Point(0, 0);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(666, 606);
            this.containerPanel.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 415);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Intervalo de actualizacion (ms)";
            // 
            // statsConfigButton
            // 
            this.statsConfigButton.BackColor = System.Drawing.Color.White;
            this.statsConfigButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.statsConfigButton.FlatAppearance.BorderSize = 2;
            this.statsConfigButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.statsConfigButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statsConfigButton.Location = new System.Drawing.Point(344, 27);
            this.statsConfigButton.Name = "statsConfigButton";
            this.statsConfigButton.Size = new System.Drawing.Size(89, 25);
            this.statsConfigButton.TabIndex = 24;
            this.statsConfigButton.Text = "Configurar";
            this.statsConfigButton.UseVisualStyleBackColor = false;
            this.statsConfigButton.Click += new System.EventHandler(this.ZoneConfigButton_Click);
            // 
            // statsConfigLabel
            // 
            this.statsConfigLabel.AutoSize = true;
            this.statsConfigLabel.Location = new System.Drawing.Point(238, 13);
            this.statsConfigLabel.Name = "statsConfigLabel";
            this.statsConfigLabel.Size = new System.Drawing.Size(81, 13);
            this.statsConfigLabel.TabIndex = 23;
            this.statsConfigLabel.Text = "Configurar zona";
            // 
            // statsSaveButton
            // 
            this.statsSaveButton.BackColor = System.Drawing.Color.White;
            this.statsSaveButton.Enabled = false;
            this.statsSaveButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.statsSaveButton.FlatAppearance.BorderSize = 2;
            this.statsSaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.statsSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statsSaveButton.Location = new System.Drawing.Point(233, 56);
            this.statsSaveButton.Name = "statsSaveButton";
            this.statsSaveButton.Size = new System.Drawing.Size(89, 36);
            this.statsSaveButton.TabIndex = 22;
            this.statsSaveButton.Text = "Guardar";
            this.statsSaveButton.UseVisualStyleBackColor = false;
            this.statsSaveButton.Click += new System.EventHandler(this.StatsSaveButton_Click);
            // 
            // statsCancelButton
            // 
            this.statsCancelButton.BackColor = System.Drawing.Color.White;
            this.statsCancelButton.Enabled = false;
            this.statsCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.statsCancelButton.FlatAppearance.BorderSize = 2;
            this.statsCancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.statsCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statsCancelButton.Location = new System.Drawing.Point(344, 56);
            this.statsCancelButton.Name = "statsCancelButton";
            this.statsCancelButton.Size = new System.Drawing.Size(89, 36);
            this.statsCancelButton.TabIndex = 21;
            this.statsCancelButton.Text = "Cancelar";
            this.statsCancelButton.UseVisualStyleBackColor = false;
            this.statsCancelButton.Click += new System.EventHandler(this.StatsCancelButton_Click);
            // 
            // zoneComboBox
            // 
            this.zoneComboBox.FormattingEnabled = true;
            this.zoneComboBox.Items.AddRange(new object[] {
            "Player HP",
            "Player MP",
            "Player CP",
            "Target HP"});
            this.zoneComboBox.Location = new System.Drawing.Point(233, 29);
            this.zoneComboBox.Name = "zoneComboBox";
            this.zoneComboBox.Size = new System.Drawing.Size(89, 21);
            this.zoneComboBox.TabIndex = 19;
            this.zoneComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(64, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "PLAYER";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(499, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "TARGET";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.recoverMPStandTextBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.recoverMPCheckBox);
            this.panel2.Controls.Add(this.recoverMPSitTextBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(8, 249);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 74);
            this.panel2.TabIndex = 15;
            // 
            // recoverMPStandTextBox
            // 
            this.recoverMPStandTextBox.Location = new System.Drawing.Point(131, 47);
            this.recoverMPStandTextBox.MaxLength = 3;
            this.recoverMPStandTextBox.Name = "recoverMPStandTextBox";
            this.recoverMPStandTextBox.Size = new System.Drawing.Size(55, 20);
            this.recoverMPStandTextBox.TabIndex = 15;
            this.recoverMPStandTextBox.TextChanged += new System.EventHandler(this.RecoverMPStandTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Pararse cuando MP >=";
            // 
            // recoverMPCheckBox
            // 
            this.recoverMPCheckBox.AutoSize = true;
            this.recoverMPCheckBox.Location = new System.Drawing.Point(0, 3);
            this.recoverMPCheckBox.Name = "recoverMPCheckBox";
            this.recoverMPCheckBox.Size = new System.Drawing.Size(109, 17);
            this.recoverMPCheckBox.TabIndex = 11;
            this.recoverMPCheckBox.Text = "Recargar MP  (%)";
            this.recoverMPCheckBox.UseVisualStyleBackColor = true;
            this.recoverMPCheckBox.CheckedChanged += new System.EventHandler(this.RecoverMPCheckBox_CheckedChanged);
            // 
            // recoverMPSitTextBox
            // 
            this.recoverMPSitTextBox.Location = new System.Drawing.Point(131, 23);
            this.recoverMPSitTextBox.MaxLength = 3;
            this.recoverMPSitTextBox.Name = "recoverMPSitTextBox";
            this.recoverMPSitTextBox.Size = new System.Drawing.Size(55, 20);
            this.recoverMPSitTextBox.TabIndex = 13;
            this.recoverMPSitTextBox.TextChanged += new System.EventHandler(this.RecoverMPSitTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Sentarse cuando MP <=";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.autoPotCheckBox);
            this.panel1.Controls.Add(this.autoPotHPTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 188);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 55);
            this.panel1.TabIndex = 14;
            // 
            // autoPotCheckBox
            // 
            this.autoPotCheckBox.AutoSize = true;
            this.autoPotCheckBox.Location = new System.Drawing.Point(0, 3);
            this.autoPotCheckBox.Name = "autoPotCheckBox";
            this.autoPotCheckBox.Size = new System.Drawing.Size(102, 17);
            this.autoPotCheckBox.TabIndex = 11;
            this.autoPotCheckBox.Text = "Auto HP Pot (%)";
            this.autoPotCheckBox.UseVisualStyleBackColor = true;
            this.autoPotCheckBox.CheckedChanged += new System.EventHandler(this.AutoPotCheckBox_CheckedChanged);
            // 
            // autoPotHPTextBox
            // 
            this.autoPotHPTextBox.Location = new System.Drawing.Point(123, 24);
            this.autoPotHPTextBox.MaxLength = 3;
            this.autoPotHPTextBox.Name = "autoPotHPTextBox";
            this.autoPotHPTextBox.Size = new System.Drawing.Size(55, 20);
            this.autoPotHPTextBox.TabIndex = 13;
            this.autoPotHPTextBox.TextChanged += new System.EventHandler(this.AutoPotHPTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "AutoPot cuando HP <=";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Ventana siempre visible",
            "Auto /attack (para guerreros)",
            "Usar /nextTarget",
            "Intentar salir de \"cannot see target\"",
            "Pausar bot si me baja la CP"});
            this.checkedListBox1.Location = new System.Drawing.Point(439, 177);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(202, 94);
            this.checkedListBox1.TabIndex = 10;
            // 
            // windowVisibilityCheckBox
            // 
            this.windowVisibilityCheckBox.AutoSize = true;
            this.windowVisibilityCheckBox.Location = new System.Drawing.Point(504, 415);
            this.windowVisibilityCheckBox.Name = "windowVisibilityCheckBox";
            this.windowVisibilityCheckBox.Size = new System.Drawing.Size(137, 17);
            this.windowVisibilityCheckBox.TabIndex = 9;
            this.windowVisibilityCheckBox.Text = "Ventana siempre visible";
            this.windowVisibilityCheckBox.UseVisualStyleBackColor = true;
            this.windowVisibilityCheckBox.CheckedChanged += new System.EventHandler(this.WindowVisibilityCheckBox_CheckedChanged);
            // 
            // TabContainer
            // 
            this.TabContainer.Controls.Add(this.botTabPage);
            this.TabContainer.Controls.Add(this.skillConfigTabPage);
            this.TabContainer.ItemSize = new System.Drawing.Size(42, 18);
            this.TabContainer.Location = new System.Drawing.Point(5, 5);
            this.TabContainer.Margin = new System.Windows.Forms.Padding(0);
            this.TabContainer.Multiline = true;
            this.TabContainer.Name = "TabContainer";
            this.TabContainer.Padding = new System.Drawing.Point(0, 0);
            this.TabContainer.SelectedIndex = 0;
            this.TabContainer.Size = new System.Drawing.Size(674, 629);
            this.TabContainer.TabIndex = 25;
            this.TabContainer.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabContainer_Selecting);
            // 
            // botTabPage
            // 
            this.botTabPage.BackColor = System.Drawing.Color.Transparent;
            this.botTabPage.Controls.Add(this.containerPanel);
            this.botTabPage.Location = new System.Drawing.Point(4, 22);
            this.botTabPage.Name = "botTabPage";
            this.botTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.botTabPage.Size = new System.Drawing.Size(666, 603);
            this.botTabPage.TabIndex = 0;
            this.botTabPage.Text = "Bot";
            // 
            // skillConfigTabPage
            // 
            this.skillConfigTabPage.BackColor = System.Drawing.Color.DimGray;
            this.skillConfigTabPage.Location = new System.Drawing.Point(4, 22);
            this.skillConfigTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.skillConfigTabPage.Name = "skillConfigTabPage";
            this.skillConfigTabPage.Size = new System.Drawing.Size(666, 603);
            this.skillConfigTabPage.TabIndex = 1;
            this.skillConfigTabPage.Text = "Skill configuracion";
            // 
            // updateIntervalUpDown
            // 
            this.updateIntervalUpDown.BackColor = System.Drawing.Color.White;
            this.updateIntervalUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.updateIntervalUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateIntervalUpDown.Location = new System.Drawing.Point(11, 432);
            this.updateIntervalUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.updateIntervalUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.updateIntervalUpDown.Name = "updateIntervalUpDown";
            this.updateIntervalUpDown.Size = new System.Drawing.Size(142, 23);
            this.updateIntervalUpDown.TabIndex = 27;
            this.updateIntervalUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.updateIntervalUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.updateIntervalUpDown.ValueChanged += new System.EventHandler(this.UpdateIntervalUpDown_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(686, 640);
            this.Controls.Add(this.TabContainer);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "MimikyuBoat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.playerPanelContainer.ResumeLayout(false);
            this.playerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerImage)).EndInit();
            this.targetPanelContainer.ResumeLayout(false);
            this.targetPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetImage)).EndInit();
            this.containerPanel.ResumeLayout(false);
            this.containerPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TabContainer.ResumeLayout(false);
            this.botTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel playerPanelContainer;
        private System.Windows.Forms.Panel targetPanelContainer;
        private System.Windows.Forms.Button playerButtonAreaConfig;
        private System.Windows.Forms.Button targetButtonAreaConfig;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox consoleDebugTextBox;
        private System.Windows.Forms.Panel containerPanel;
        private System.Windows.Forms.CheckBox windowVisibilityCheckBox;
        private System.Windows.Forms.CheckBox autoPotCheckBox;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox recoverMPCheckBox;
        private System.Windows.Forms.TextBox recoverMPSitTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox autoPotHPTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox recoverMPStandTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel playerStatsMarker;
        private System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.PictureBox playerImage;
        private System.Windows.Forms.Label statsConfigLabel;
        private System.Windows.Forms.Button statsSaveButton;
        private System.Windows.Forms.Button statsCancelButton;
        private System.Windows.Forms.ComboBox zoneComboBox;
        private System.Windows.Forms.Button statsConfigButton;
        private System.Windows.Forms.Panel targetPanel;
        private System.Windows.Forms.Panel targetStatsMarker;
        private System.Windows.Forms.PictureBox targetImage;
        private System.Windows.Forms.TabControl TabContainer;
        private System.Windows.Forms.TabPage botTabPage;
        private System.Windows.Forms.TabPage skillConfigTabPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown updateIntervalUpDown;
    }
}

