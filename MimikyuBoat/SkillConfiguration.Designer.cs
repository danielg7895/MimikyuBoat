namespace MimikyuBoat
{
    partial class SkillConfiguration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.separator = new System.Windows.Forms.Label();
            this.skillListDataGrid = new System.Windows.Forms.DataGridView();
            this.skillComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statsConfigButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.shortCutButton = new System.Windows.Forms.Button();
            this.skillOptionsPanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.reuseTimeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reuseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shortcut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteSkill = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.skillListDataGrid)).BeginInit();
            this.skillOptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // separator
            // 
            this.separator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator.Location = new System.Drawing.Point(12, 110);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(626, 2);
            this.separator.TabIndex = 0;
            // 
            // skillListDataGrid
            // 
            this.skillListDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.skillListDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.skillListDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skillListDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.active,
            this.id,
            this.name,
            this.targetType,
            this.reuseTime,
            this.condition,
            this.shortcut,
            this.deleteSkill});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(224)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.skillListDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.skillListDataGrid.Location = new System.Drawing.Point(12, 126);
            this.skillListDataGrid.Name = "skillListDataGrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.skillListDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.skillListDataGrid.RowHeadersVisible = false;
            this.skillListDataGrid.Size = new System.Drawing.Size(626, 175);
            this.skillListDataGrid.TabIndex = 1;
            this.skillListDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.skillListDataGrid_CellClick);
            this.skillListDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.skillListDataGrid_CellValueChanged);
            // 
            // skillComboBox
            // 
            this.skillComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.skillComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.skillComboBox.BackColor = System.Drawing.Color.AliceBlue;
            this.skillComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.skillComboBox.FormattingEnabled = true;
            this.skillComboBox.Items.AddRange(new object[] {
            "TEST",
            "hola",
            "haha",
            "nope",
            "entre cuerdas azules",
            "ikoo z",
            "nandemonai",
            "uwu"});
            this.skillComboBox.Location = new System.Drawing.Point(68, 326);
            this.skillComboBox.Name = "skillComboBox";
            this.skillComboBox.Size = new System.Drawing.Size(367, 24);
            this.skillComboBox.TabIndex = 2;
            this.skillComboBox.SelectedIndexChanged += new System.EventHandler(this.SkillComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Skill:";
            // 
            // statsConfigButton
            // 
            this.statsConfigButton.BackColor = System.Drawing.Color.White;
            this.statsConfigButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.statsConfigButton.FlatAppearance.BorderSize = 2;
            this.statsConfigButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.statsConfigButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.statsConfigButton.Location = new System.Drawing.Point(536, 326);
            this.statsConfigButton.Name = "statsConfigButton";
            this.statsConfigButton.Size = new System.Drawing.Size(89, 25);
            this.statsConfigButton.TabIndex = 25;
            this.statsConfigButton.Text = "Cancelar";
            this.statsConfigButton.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(180)))), ((int)(((byte)(222)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(441, 326);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 25);
            this.button1.TabIndex = 26;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // shortCutButton
            // 
            this.shortCutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(234)))));
            this.shortCutButton.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.shortCutButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.shortCutButton.FlatAppearance.BorderSize = 4;
            this.shortCutButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Thistle;
            this.shortCutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shortCutButton.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shortCutButton.Location = new System.Drawing.Point(413, 3);
            this.shortCutButton.Name = "shortCutButton";
            this.shortCutButton.Size = new System.Drawing.Size(184, 33);
            this.shortCutButton.TabIndex = 27;
            this.shortCutButton.Text = "Shortcut sin asignar";
            this.shortCutButton.UseVisualStyleBackColor = false;
            this.shortCutButton.Click += new System.EventHandler(this.ShortCutButton_Click);
            // 
            // skillOptionsPanel
            // 
            this.skillOptionsPanel.BackColor = System.Drawing.Color.White;
            this.skillOptionsPanel.Controls.Add(this.textBox1);
            this.skillOptionsPanel.Controls.Add(this.label5);
            this.skillOptionsPanel.Controls.Add(this.conditionComboBox);
            this.skillOptionsPanel.Controls.Add(this.reuseTimeTextBox);
            this.skillOptionsPanel.Controls.Add(this.label4);
            this.skillOptionsPanel.Controls.Add(this.shortCutButton);
            this.skillOptionsPanel.Enabled = false;
            this.skillOptionsPanel.Location = new System.Drawing.Point(25, 357);
            this.skillOptionsPanel.Name = "skillOptionsPanel";
            this.skillOptionsPanel.Size = new System.Drawing.Size(600, 195);
            this.skillOptionsPanel.TabIndex = 28;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(416, 78);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 23);
            this.textBox1.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Condicion:";
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.conditionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.conditionComboBox.BackColor = System.Drawing.Color.AliceBlue;
            this.conditionComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conditionComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Location = new System.Drawing.Point(77, 78);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(333, 24);
            this.conditionComboBox.TabIndex = 34;
            // 
            // reuseTimeTextBox
            // 
            this.reuseTimeTextBox.Location = new System.Drawing.Point(7, 20);
            this.reuseTimeTextBox.MaxLength = 5;
            this.reuseTimeTextBox.Name = "reuseTimeTextBox";
            this.reuseTimeTextBox.Size = new System.Drawing.Size(64, 20);
            this.reuseTimeTextBox.TabIndex = 33;
            this.reuseTimeTextBox.Text = "2000";
            this.reuseTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Tiempo de reuso (milisegundos)";
            // 
            // active
            // 
            this.active.HeaderText = "";
            this.active.Name = "active";
            this.active.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.active.Width = 30;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Width = 40;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // targetType
            // 
            this.targetType.HeaderText = "Target Type";
            this.targetType.Name = "targetType";
            this.targetType.ToolTipText = "Tipo de target (propio, enemigos, terreno, etc)";
            // 
            // reuseTime
            // 
            this.reuseTime.HeaderText = "Tiempo Reuso";
            this.reuseTime.Name = "reuseTime";
            // 
            // condition
            // 
            this.condition.HeaderText = "Condicion";
            this.condition.Name = "condition";
            this.condition.Width = 140;
            // 
            // shortcut
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue;
            this.shortcut.DefaultCellStyle = dataGridViewCellStyle2;
            this.shortcut.HeaderText = "Shortcut";
            this.shortcut.Name = "shortcut";
            this.shortcut.Width = 60;
            // 
            // deleteSkill
            // 
            this.deleteSkill.HeaderText = "Eliminar";
            this.deleteSkill.Name = "deleteSkill";
            this.deleteSkill.Text = "X";
            this.deleteSkill.Width = 50;
            // 
            // SkillConfiguration
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 564);
            this.Controls.Add(this.skillOptionsPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statsConfigButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skillComboBox);
            this.Controls.Add(this.skillListDataGrid);
            this.Controls.Add(this.separator);
            this.KeyPreview = true;
            this.Name = "SkillConfiguration";
            this.Text = "SkillConfiguration";
            this.Load += new System.EventHandler(this.SkillConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.skillListDataGrid)).EndInit();
            this.skillOptionsPanel.ResumeLayout(false);
            this.skillOptionsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.DataGridView skillListDataGrid;
        private System.Windows.Forms.ComboBox skillComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button statsConfigButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button shortCutButton;
        private System.Windows.Forms.Panel skillOptionsPanel;
        private System.Windows.Forms.TextBox reuseTimeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox conditionComboBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn active;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetType;
        private System.Windows.Forms.DataGridViewTextBoxColumn reuseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn condition;
        private System.Windows.Forms.DataGridViewTextBoxColumn shortcut;
        private System.Windows.Forms.DataGridViewButtonColumn deleteSkill;
    }
}