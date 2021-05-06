namespace Shizui
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
            this.startButton = new System.Windows.Forms.Button();
            this.consoleDebugTextBox = new System.Windows.Forms.TextBox();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.hpTargetLabel = new System.Windows.Forms.Label();
            this.hpPlayerLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.useSpoilCheckBox = new System.Windows.Forms.CheckBox();
            this.spoilTimesTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.pickupDelayTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pickupTimesTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.assistCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.alwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.TabContainer = new System.Windows.Forms.TabControl();
            this.botTabPage = new System.Windows.Forms.TabPage();
            this.botConfigTabPanel = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.l2ProcessComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectedWindowName = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.addUserButton = new System.Windows.Forms.Button();
            this.separator = new System.Windows.Forms.Label();
            this.configLoadButton = new System.Windows.Forms.Button();
            this.configSaveButton = new System.Windows.Forms.Button();
            this.configurationLoadLabel = new System.Windows.Forms.Label();
            this.updateIntervalUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.recoverMPStandTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.recoverMPCheckBox = new System.Windows.Forms.CheckBox();
            this.recoverMPSitTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.autoPotCheckBox = new System.Windows.Forms.CheckBox();
            this.autoPotTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.containerPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.TabContainer.SuspendLayout();
            this.botTabPage.SuspendLayout();
            this.botConfigTabPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalUpDown)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.startButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.startButton.FlatAppearance.BorderSize = 4;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.startButton.Location = new System.Drawing.Point(212, 383);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(221, 53);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // consoleDebugTextBox
            // 
            this.consoleDebugTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.consoleDebugTextBox.Font = new System.Drawing.Font("Roboto", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleDebugTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(152)))), ((int)(((byte)(203)))));
            this.consoleDebugTextBox.Location = new System.Drawing.Point(2, 442);
            this.consoleDebugTextBox.Multiline = true;
            this.consoleDebugTextBox.Name = "consoleDebugTextBox";
            this.consoleDebugTextBox.ReadOnly = true;
            this.consoleDebugTextBox.Size = new System.Drawing.Size(660, 158);
            this.consoleDebugTextBox.TabIndex = 7;
            // 
            // containerPanel
            // 
            this.containerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.containerPanel.Controls.Add(this.hpTargetLabel);
            this.containerPanel.Controls.Add(this.hpPlayerLabel);
            this.containerPanel.Controls.Add(this.panel2);
            this.containerPanel.Controls.Add(this.label4);
            this.containerPanel.Controls.Add(this.label5);
            this.containerPanel.Controls.Add(this.alwaysOnTopCheckBox);
            this.containerPanel.Controls.Add(this.consoleDebugTextBox);
            this.containerPanel.Controls.Add(this.startButton);
            this.containerPanel.Location = new System.Drawing.Point(0, 0);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(665, 603);
            this.containerPanel.TabIndex = 9;
            // 
            // hpTargetLabel
            // 
            this.hpTargetLabel.AutoSize = true;
            this.hpTargetLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.hpTargetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpTargetLabel.ForeColor = System.Drawing.Color.IndianRed;
            this.hpTargetLabel.Location = new System.Drawing.Point(567, 3);
            this.hpTargetLabel.Name = "hpTargetLabel";
            this.hpTargetLabel.Size = new System.Drawing.Size(79, 25);
            this.hpTargetLabel.TabIndex = 33;
            this.hpTargetLabel.Text = "HP: 0%";
            // 
            // hpPlayerLabel
            // 
            this.hpPlayerLabel.AutoSize = true;
            this.hpPlayerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.hpPlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hpPlayerLabel.ForeColor = System.Drawing.Color.IndianRed;
            this.hpPlayerLabel.Location = new System.Drawing.Point(135, 3);
            this.hpPlayerLabel.Name = "hpPlayerLabel";
            this.hpPlayerLabel.Size = new System.Drawing.Size(79, 25);
            this.hpPlayerLabel.TabIndex = 32;
            this.hpPlayerLabel.Text = "HP: 0%";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.useSpoilCheckBox);
            this.panel2.Controls.Add(this.spoilTimesTextBox);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.pickupDelayTextBox);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.pickupTimesTextBox);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.assistCheckBox);
            this.panel2.Font = new System.Drawing.Font("Roboto", 10F);
            this.panel2.Location = new System.Drawing.Point(369, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 166);
            this.panel2.TabIndex = 31;
            // 
            // useSpoilCheckBox
            // 
            this.useSpoilCheckBox.AutoSize = true;
            this.useSpoilCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.useSpoilCheckBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.useSpoilCheckBox.Location = new System.Drawing.Point(13, 96);
            this.useSpoilCheckBox.Name = "useSpoilCheckBox";
            this.useSpoilCheckBox.Size = new System.Drawing.Size(69, 17);
            this.useSpoilCheckBox.TabIndex = 32;
            this.useSpoilCheckBox.Text = "Use spoil";
            this.useSpoilCheckBox.UseVisualStyleBackColor = true;
            this.useSpoilCheckBox.CheckedChanged += new System.EventHandler(this.useSpoilCheckBox_CheckedChanged);
            // 
            // spoilTimesTextBox
            // 
            this.spoilTimesTextBox.Enabled = false;
            this.spoilTimesTextBox.Location = new System.Drawing.Point(222, 111);
            this.spoilTimesTextBox.MaxLength = 3;
            this.spoilTimesTextBox.Name = "spoilTimesTextBox";
            this.spoilTimesTextBox.Size = new System.Drawing.Size(55, 24);
            this.spoilTimesTextBox.TabIndex = 31;
            this.spoilTimesTextBox.TextChanged += new System.EventHandler(this.spoilTimesTextBox_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Gainsboro;
            this.label17.Location = new System.Drawing.Point(10, 114);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 17);
            this.label17.TabIndex = 30;
            this.label17.Text = "Spoil times";
            // 
            // pickupDelayTextBox
            // 
            this.pickupDelayTextBox.Location = new System.Drawing.Point(222, 66);
            this.pickupDelayTextBox.MaxLength = 3;
            this.pickupDelayTextBox.Name = "pickupDelayTextBox";
            this.pickupDelayTextBox.Size = new System.Drawing.Size(55, 24);
            this.pickupDelayTextBox.TabIndex = 29;
            this.pickupDelayTextBox.TextChanged += new System.EventHandler(this.pickupDelayTextBox_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Gainsboro;
            this.label16.Location = new System.Drawing.Point(6, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(181, 17);
            this.label16.TabIndex = 28;
            this.label16.Text = "Delay between pickups (ms)";
            // 
            // pickupTimesTextBox
            // 
            this.pickupTimesTextBox.Location = new System.Drawing.Point(222, 38);
            this.pickupTimesTextBox.MaxLength = 3;
            this.pickupTimesTextBox.Name = "pickupTimesTextBox";
            this.pickupTimesTextBox.Size = new System.Drawing.Size(55, 24);
            this.pickupTimesTextBox.TabIndex = 27;
            this.pickupTimesTextBox.TextChanged += new System.EventHandler(this.pickupTimesTextBox_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Gainsboro;
            this.label15.Location = new System.Drawing.Point(10, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 17);
            this.label15.TabIndex = 26;
            this.label15.Text = "Pickup times";
            // 
            // assistCheckBox
            // 
            this.assistCheckBox.AutoSize = true;
            this.assistCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.assistCheckBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.assistCheckBox.Location = new System.Drawing.Point(9, 13);
            this.assistCheckBox.Name = "assistCheckBox";
            this.assistCheckBox.Size = new System.Drawing.Size(109, 17);
            this.assistCheckBox.TabIndex = 25;
            this.assistCheckBox.Text = "Activar asistencia";
            this.assistCheckBox.UseVisualStyleBackColor = true;
            this.assistCheckBox.CheckedChanged += new System.EventHandler(this.asistCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "PLAYER";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(442, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "TARGET";
            // 
            // alwaysOnTopCheckBox
            // 
            this.alwaysOnTopCheckBox.AutoSize = true;
            this.alwaysOnTopCheckBox.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.alwaysOnTopCheckBox.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.alwaysOnTopCheckBox.Location = new System.Drawing.Point(513, 419);
            this.alwaysOnTopCheckBox.Name = "alwaysOnTopCheckBox";
            this.alwaysOnTopCheckBox.Size = new System.Drawing.Size(146, 17);
            this.alwaysOnTopCheckBox.TabIndex = 9;
            this.alwaysOnTopCheckBox.Text = "Ventana siempre visible";
            this.alwaysOnTopCheckBox.UseVisualStyleBackColor = true;
            this.alwaysOnTopCheckBox.CheckedChanged += new System.EventHandler(this.WindowVisibilityCheckBox_CheckedChanged);
            // 
            // TabContainer
            // 
            this.TabContainer.Controls.Add(this.botTabPage);
            this.TabContainer.Controls.Add(this.botConfigTabPanel);
            this.TabContainer.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TabContainer.ItemSize = new System.Drawing.Size(42, 18);
            this.TabContainer.Location = new System.Drawing.Point(5, 5);
            this.TabContainer.Margin = new System.Windows.Forms.Padding(0);
            this.TabContainer.Multiline = true;
            this.TabContainer.Name = "TabContainer";
            this.TabContainer.Padding = new System.Drawing.Point(0, 0);
            this.TabContainer.SelectedIndex = 0;
            this.TabContainer.Size = new System.Drawing.Size(676, 628);
            this.TabContainer.TabIndex = 25;
            // 
            // botTabPage
            // 
            this.botTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.botTabPage.Controls.Add(this.containerPanel);
            this.botTabPage.Location = new System.Drawing.Point(4, 22);
            this.botTabPage.Name = "botTabPage";
            this.botTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.botTabPage.Size = new System.Drawing.Size(668, 602);
            this.botTabPage.TabIndex = 0;
            this.botTabPage.Text = "Bot";
            // 
            // botConfigTabPanel
            // 
            this.botConfigTabPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.botConfigTabPanel.Controls.Add(this.panel3);
            this.botConfigTabPanel.Location = new System.Drawing.Point(4, 22);
            this.botConfigTabPanel.Name = "botConfigTabPanel";
            this.botConfigTabPanel.Padding = new System.Windows.Forms.Padding(3);
            this.botConfigTabPanel.Size = new System.Drawing.Size(668, 602);
            this.botConfigTabPanel.TabIndex = 2;
            this.botConfigTabPanel.Text = "Configuracion del bot";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.panel3.Controls.Add(this.l2ProcessComboBox);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.selectedWindowName);
            this.panel3.Controls.Add(this.userNameTextBox);
            this.panel3.Controls.Add(this.addUserButton);
            this.panel3.Controls.Add(this.separator);
            this.panel3.Controls.Add(this.configLoadButton);
            this.panel3.Controls.Add(this.configSaveButton);
            this.panel3.Controls.Add(this.configurationLoadLabel);
            this.panel3.Controls.Add(this.updateIntervalUpDown);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.panel11);
            this.panel3.Controls.Add(this.checkedListBox2);
            this.panel3.Location = new System.Drawing.Point(0, -2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(666, 559);
            this.panel3.TabIndex = 10;
            // 
            // l2ProcessComboBox
            // 
            this.l2ProcessComboBox.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.l2ProcessComboBox.FormattingEnabled = true;
            this.l2ProcessComboBox.Location = new System.Drawing.Point(460, 30);
            this.l2ProcessComboBox.Name = "l2ProcessComboBox";
            this.l2ProcessComboBox.Size = new System.Drawing.Size(188, 24);
            this.l2ProcessComboBox.TabIndex = 41;
            this.l2ProcessComboBox.SelectedIndexChanged += new System.EventHandler(this.l2ProcessComboBox_SelectedIndexChanged);
            this.l2ProcessComboBox.Click += new System.EventHandler(this.l2ProcessComboBox_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label10.Location = new System.Drawing.Point(275, 294);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 31);
            this.label10.TabIndex = 39;
            this.label10.Text = "INFO";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(4, 328);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 166);
            this.panel1.TabIndex = 38;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Roboto", 10F);
            this.label14.ForeColor = System.Drawing.Color.Gainsboro;
            this.label14.Location = new System.Drawing.Point(3, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 17);
            this.label14.TabIndex = 40;
            this.label14.Text = "Accion /assist -> F10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Roboto", 10F);
            this.label9.ForeColor = System.Drawing.Color.Gainsboro;
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 17);
            this.label9.TabIndex = 32;
            this.label9.Text = "Target HP = 0 -> 3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 10F);
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(3, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 17);
            this.label1.TabIndex = 39;
            this.label1.Text = "Macro con nombre PJ a asistir -> F9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Roboto", 10F);
            this.label8.ForeColor = System.Drawing.Color.Gainsboro;
            this.label8.Location = new System.Drawing.Point(3, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 31;
            this.label8.Text = "Pickup -> 4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 10F);
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Targets -> 6 7 8 9 0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Roboto", 10F);
            this.label7.ForeColor = System.Drawing.Color.Gainsboro;
            this.label7.Location = new System.Drawing.Point(3, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "ShortCut condicional -> F11";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 10F);
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Pocion -> 5";
            // 
            // selectedWindowName
            // 
            this.selectedWindowName.AutoSize = true;
            this.selectedWindowName.Font = new System.Drawing.Font("Roboto", 10F);
            this.selectedWindowName.ForeColor = System.Drawing.Color.Gainsboro;
            this.selectedWindowName.Location = new System.Drawing.Point(460, 9);
            this.selectedWindowName.Name = "selectedWindowName";
            this.selectedWindowName.Size = new System.Drawing.Size(98, 17);
            this.selectedWindowName.TabIndex = 36;
            this.selectedWindowName.Text = "Seleccionar L2";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.userNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userNameTextBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.userNameTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.userNameTextBox.Location = new System.Drawing.Point(224, 13);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(148, 17);
            this.userNameTextBox.TabIndex = 36;
            this.userNameTextBox.Text = "NINGUNO";
            // 
            // addUserButton
            // 
            this.addUserButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.addUserButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.addUserButton.FlatAppearance.BorderSize = 3;
            this.addUserButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.addUserButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.addUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addUserButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addUserButton.ForeColor = System.Drawing.Color.Black;
            this.addUserButton.Location = new System.Drawing.Point(20, 63);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(185, 34);
            this.addUserButton.TabIndex = 35;
            this.addUserButton.Text = "Agregar nuevo usuario";
            this.addUserButton.UseVisualStyleBackColor = false;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // separator
            // 
            this.separator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator.Location = new System.Drawing.Point(20, 107);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(626, 2);
            this.separator.TabIndex = 34;
            // 
            // configLoadButton
            // 
            this.configLoadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.configLoadButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.configLoadButton.FlatAppearance.BorderSize = 4;
            this.configLoadButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.configLoadButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.configLoadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.configLoadButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configLoadButton.ForeColor = System.Drawing.Color.Black;
            this.configLoadButton.Location = new System.Drawing.Point(461, 63);
            this.configLoadButton.Name = "configLoadButton";
            this.configLoadButton.Size = new System.Drawing.Size(185, 34);
            this.configLoadButton.TabIndex = 32;
            this.configLoadButton.Text = "Cargar configuracion";
            this.configLoadButton.UseVisualStyleBackColor = false;
            this.configLoadButton.Click += new System.EventHandler(this.configLoadButton_Click);
            // 
            // configSaveButton
            // 
            this.configSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.configSaveButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.configSaveButton.FlatAppearance.BorderSize = 4;
            this.configSaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.configSaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.configSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.configSaveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configSaveButton.ForeColor = System.Drawing.Color.Black;
            this.configSaveButton.Location = new System.Drawing.Point(239, 63);
            this.configSaveButton.Name = "configSaveButton";
            this.configSaveButton.Size = new System.Drawing.Size(185, 34);
            this.configSaveButton.TabIndex = 31;
            this.configSaveButton.Text = "Guardar configuracion";
            this.configSaveButton.UseVisualStyleBackColor = false;
            this.configSaveButton.Click += new System.EventHandler(this.configSaveButton_Click);
            // 
            // configurationLoadLabel
            // 
            this.configurationLoadLabel.AutoSize = true;
            this.configurationLoadLabel.Font = new System.Drawing.Font("Roboto", 10F);
            this.configurationLoadLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.configurationLoadLabel.Location = new System.Drawing.Point(6, 13);
            this.configurationLoadLabel.Name = "configurationLoadLabel";
            this.configurationLoadLabel.Size = new System.Drawing.Size(101, 17);
            this.configurationLoadLabel.TabIndex = 30;
            this.configurationLoadLabel.Text = "Usuario actual:";
            // 
            // updateIntervalUpDown
            // 
            this.updateIntervalUpDown.BackColor = System.Drawing.Color.White;
            this.updateIntervalUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.updateIntervalUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateIntervalUpDown.ForeColor = System.Drawing.Color.Black;
            this.updateIntervalUpDown.Location = new System.Drawing.Point(5, 535);
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
            this.updateIntervalUpDown.TabIndex = 29;
            this.updateIntervalUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.updateIntervalUpDown.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 8F);
            this.label6.ForeColor = System.Drawing.Color.Gainsboro;
            this.label6.Location = new System.Drawing.Point(2, 520);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Intervalo de actualizacion (ms)";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.recoverMPStandTextBox);
            this.panel10.Controls.Add(this.label11);
            this.panel10.Controls.Add(this.recoverMPCheckBox);
            this.panel10.Controls.Add(this.recoverMPSitTextBox);
            this.panel10.Controls.Add(this.label12);
            this.panel10.Enabled = false;
            this.panel10.Location = new System.Drawing.Point(6, 175);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(210, 68);
            this.panel10.TabIndex = 15;
            // 
            // recoverMPStandTextBox
            // 
            this.recoverMPStandTextBox.Location = new System.Drawing.Point(152, 43);
            this.recoverMPStandTextBox.MaxLength = 3;
            this.recoverMPStandTextBox.Name = "recoverMPStandTextBox";
            this.recoverMPStandTextBox.Size = new System.Drawing.Size(55, 22);
            this.recoverMPStandTextBox.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Roboto", 10F);
            this.label11.ForeColor = System.Drawing.Color.Gainsboro;
            this.label11.Location = new System.Drawing.Point(3, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "Pararse cuando MP >=";
            // 
            // recoverMPCheckBox
            // 
            this.recoverMPCheckBox.AutoSize = true;
            this.recoverMPCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.recoverMPCheckBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.recoverMPCheckBox.Location = new System.Drawing.Point(3, 3);
            this.recoverMPCheckBox.Name = "recoverMPCheckBox";
            this.recoverMPCheckBox.Size = new System.Drawing.Size(134, 21);
            this.recoverMPCheckBox.TabIndex = 11;
            this.recoverMPCheckBox.Text = "Recargar MP  (%)";
            this.recoverMPCheckBox.UseVisualStyleBackColor = true;
            // 
            // recoverMPSitTextBox
            // 
            this.recoverMPSitTextBox.Location = new System.Drawing.Point(152, 18);
            this.recoverMPSitTextBox.MaxLength = 3;
            this.recoverMPSitTextBox.Name = "recoverMPSitTextBox";
            this.recoverMPSitTextBox.Size = new System.Drawing.Size(55, 22);
            this.recoverMPSitTextBox.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Roboto", 10F);
            this.label12.ForeColor = System.Drawing.Color.Gainsboro;
            this.label12.Location = new System.Drawing.Point(3, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(155, 17);
            this.label12.TabIndex = 12;
            this.label12.Text = "Sentarse cuando MP <=";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.autoPotCheckBox);
            this.panel11.Controls.Add(this.autoPotTextBox);
            this.panel11.Controls.Add(this.label13);
            this.panel11.Enabled = false;
            this.panel11.Location = new System.Drawing.Point(6, 119);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(210, 51);
            this.panel11.TabIndex = 14;
            // 
            // autoPotCheckBox
            // 
            this.autoPotCheckBox.AutoSize = true;
            this.autoPotCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.autoPotCheckBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.autoPotCheckBox.Location = new System.Drawing.Point(3, 4);
            this.autoPotCheckBox.Name = "autoPotCheckBox";
            this.autoPotCheckBox.Size = new System.Drawing.Size(127, 21);
            this.autoPotCheckBox.TabIndex = 11;
            this.autoPotCheckBox.Text = "Auto HP Pot (%)";
            this.autoPotCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoPotTextBox
            // 
            this.autoPotTextBox.Location = new System.Drawing.Point(152, 22);
            this.autoPotTextBox.MaxLength = 3;
            this.autoPotTextBox.Name = "autoPotTextBox";
            this.autoPotTextBox.Size = new System.Drawing.Size(55, 22);
            this.autoPotTextBox.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Roboto", 10F);
            this.label13.ForeColor = System.Drawing.Color.Gainsboro;
            this.label13.Location = new System.Drawing.Point(0, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(150, 17);
            this.label13.TabIndex = 12;
            this.label13.Text = "AutoPot cuando HP <=";
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.checkedListBox2.Enabled = false;
            this.checkedListBox2.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkedListBox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "Ventana siempre visible",
            "Auto /attack (para guerreros)",
            "Usar /nextTarget",
            "Intentar salir de \"cannot see target\"",
            "Pausar bot si me baja la CP"});
            this.checkedListBox2.Location = new System.Drawing.Point(458, 141);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(202, 42);
            this.checkedListBox2.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(687, 639);
            this.Controls.Add(this.TabContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Shizui";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.containerPanel.ResumeLayout(false);
            this.containerPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.TabContainer.ResumeLayout(false);
            this.botTabPage.ResumeLayout(false);
            this.botConfigTabPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalUpDown)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox consoleDebugTextBox;
        private System.Windows.Forms.Panel containerPanel;
        private System.Windows.Forms.CheckBox alwaysOnTopCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl TabContainer;
        private System.Windows.Forms.TabPage botTabPage;
        private System.Windows.Forms.TabPage botConfigTabPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox recoverMPStandTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox recoverMPCheckBox;
        private System.Windows.Forms.TextBox recoverMPSitTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.CheckBox autoPotCheckBox;
        private System.Windows.Forms.TextBox autoPotTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.NumericUpDown updateIntervalUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button configLoadButton;
        private System.Windows.Forms.Button configSaveButton;
        private System.Windows.Forms.Label configurationLoadLabel;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.Label selectedWindowName;
        private System.Windows.Forms.CheckBox assistCheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label hpPlayerLabel;
        private System.Windows.Forms.Label hpTargetLabel;
        private System.Windows.Forms.TextBox pickupDelayTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox pickupTimesTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox useSpoilCheckBox;
        private System.Windows.Forms.TextBox spoilTimesTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox l2ProcessComboBox;
    }
}

