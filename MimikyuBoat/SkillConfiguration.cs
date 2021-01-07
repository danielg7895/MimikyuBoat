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

namespace Shizui
{
    public partial class SkillConfiguration : Form
    {
        public Exception SKILL_ID_NOT_FOUND { get; private set; }

        Query query;

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

        List<string> allSkills = new List<string>();
        public void LoadSkillList()
        {
            allSkills = new List<string>(); // por defecto tiene cargado todes los skllils

            string xmlPath = @"config/skills.xml";
            XDocument doc = XDocument.Load(xmlPath);
            foreach (XElement skill in doc.Root.Descendants("skill"))
            {
                if (skill.Element("operateType").Value == "P") continue; // ignoro skills pasivos
                string skillName = skill.Attribute("name").Value;

                if (skillName == "") continue;  // ignoro skills rotos.

                if (!allSkills.Contains(skillName)) {
                    allSkills.Add(skillName);
                }
            }

            // Inicializo el query para futuro uso.
            query = new Query(allSkills);
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
            // obtengo la data del skill desde el xml de skills.
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
            shortCutButton.Enabled = true;
            reuseTimeTextBox.Enabled = true;
            conditionComboBox.Enabled = true;
            textBox1.Enabled = true;
            //skillOptionsPanel.Enabled = true;
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
            //skillOptionsPanel.Enabled = false;
            shortCutButton.Enabled = false;
            reuseTimeTextBox.Enabled = false;
            conditionComboBox.Enabled = false;
            textBox1.Enabled = false;

            Skill skill = GetSkill(textBox2.Text);
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

        List<string> matchSkills;
        bool shouldSearch = true;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            TextBox t = sender as TextBox;
            if (t == null) return;

            if (t.Text.Length >= 0 && shouldSearch)
            {
                matchSkills = query.PerformQuery(t.Text);
                // agrego los resultados obtenidos de la query directamente en la lista.
                listBox1.Items.AddRange(matchSkills.ToArray());
                listBox1.Visible = true;
                listBox1.Enabled = true;
            }
            else
            {
                shouldSearch = true;
                query.Reset();
            }
            Console.WriteLine("asd");

        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("salien2");
            HideListbox();
        }


        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("selectedchange");
            shouldSearch = false;
            textBox2.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            HideListbox();
        }

        void HideListbox()
        {

            Console.WriteLine("escondien2");
            listBox1.Visible = false;
        }

    }
}
