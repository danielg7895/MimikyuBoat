using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading;

namespace MimikyuBoat
{
    public partial class SkillConfiguration : Form
    {
        public Exception SKILL_ID_NOT_FOUND { get; private set; }

        public SkillConfiguration()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.FormLoaded);

        }

        void FormLoaded(object sender, EventArgs e)
        {
            Thread loader = new Thread(new ThreadStart(LoadSkillList));
            loader.Start();
        }

        public void LoadSkillList()
        {
            string xmlPath = @"config/skills.xml";
            XDocument doc = XDocument.Load(xmlPath);
            foreach (XElement skill in doc.Root.Descendants("skill"))
            {
                if (skill.Element("operateType").Value == "P") continue; // ignoro skills pasivos
                this.Invoke((MethodInvoker)delegate
                {
                    skillComboBox.Items.Add(skill.Attribute("name").Value);

                });
            }
        }

        public int GetSkillID(string name)
        {
            XDocument doc = XDocument.Load(BotSettings.SKILL_XML_PATH);
            foreach (XElement element in doc.Root.Descendants("skill"))
            {
                if (element.Attribute("name").Value == name)
                {
                    // skill encontrado
                    int id = int.Parse(element.Attribute("id").Value);
                    return id;
                }
            }
            throw SKILL_ID_NOT_FOUND; // no deberia pasar
        }

        public Skill GetSkill(string name)
        {
            Skill skill;
            XDocument doc = XDocument.Load(BotSettings.SKILL_XML_PATH);
            foreach (XElement element in doc.Root.Descendants("skill"))
            {
                if (element.Attribute("name").Value == name)
                {
                    // skill encontrado
                    int id = int.Parse(element.Attribute("id").Value);
                    string targetType = element.Element("targetType").Value;
                    string affectScope = element.Element("affectScope").Value;

                    skill = new Skill(id, name, targetType);
                    skill.affectScope = affectScope;
                    return skill;
                }
            }
            return null;
        }

        private void SkillComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            skillOptionsPanel.Enabled = true;
        }

        private void ShortCutButton_Click(object sender, EventArgs e)
        {
            shortCutButton.Text = "Presiona una tecla";
            shortCutButton.BackColor = Color.FromArgb(255, 223, 234);
            shortCutButton.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128);
            shortCutButton.FlatAppearance.BorderSize = 4;

            KeyDown += ShortCutAssign;
        }

        void ShortCutAssign(object sender, KeyEventArgs e)
        {
            shortCutButton.Text = e.KeyCode.ToString();
            shortCutButton.BackColor = Color.White;
            shortCutButton.FlatAppearance.BorderColor = Color.FromArgb(146, 180, 222);
            shortCutButton.FlatAppearance.BorderSize = 2;
            Debug.WriteLine(e.KeyValue);

            KeyDown -= ShortCutAssign;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skillOptionsPanel.Enabled = false;

            Skill skill = GetSkill(skillComboBox.Text);
            Skill.Condition skillCondition = (Skill.Condition)Enum.Parse(typeof(Skill.Condition), conditionComboBox.SelectedItem.ToString());
            skill.SetUsageCondition(skillCondition);
            skill.reuseTime = int.Parse(reuseTimeTextBox.Text);
            skill.IsEnabled = true;

            // Agrego el skill al player.
            Player.Instance.AddSkill(skill);

            // agrego el skill a la tabla de skills (datagrid)
            int index = skillListDataGrid.Rows.Add();
            skillListDataGrid.Rows[index].Cells[0].Value = true; // checkbox skill enable.
            skillListDataGrid.Rows[index].Cells[1].Value = skill.id;
            skillListDataGrid.Rows[index].Cells[2].Value = skill.name;
            skillListDataGrid.Rows[index].Cells[3].Value = skill.targetType;
            skillListDataGrid.Rows[index].Cells[4].Value = skill.reuseTime;
            skillListDataGrid.Rows[index].Cells[5].Value = skill.GetUsageCondition().ToString();
            skillListDataGrid.Rows[index].Cells[6].Value = shortCutButton.Text; // skill shortcut
        }

        private void SkillConfiguration_Load(object sender, EventArgs e)
        {
            // agrego los condition al combobox
            conditionComboBox.DataSource = Enum.GetNames(typeof(Skill.Condition));
        }

        private void skillListDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void skillListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1) // 0 porque es el id del checkbox
            {
                // seteo el checkbox del data grid porque la unica forma de saber si esta checkeado o no es haciendolo uno mismo
                int rowIndex = e.RowIndex;
                string skillName = (string)skillListDataGrid.Rows[rowIndex].Cells[2].Value;
                DataGridViewCheckBoxCell dgCheckBox = (DataGridViewCheckBoxCell)skillListDataGrid.Rows[rowIndex].Cells[0];

                if (dgCheckBox.Value == null) dgCheckBox.Value = false;

                if (dgCheckBox.Value.ToString() == "False") dgCheckBox.Value = true;
                if (dgCheckBox.Value.ToString() == "True") dgCheckBox.Value = false;

                // seteo el skill como activado o desactivado.
                Player.Instance.SetSkillEnabledState(skillName, (bool)dgCheckBox.Value);
            }

        }
    }
}
