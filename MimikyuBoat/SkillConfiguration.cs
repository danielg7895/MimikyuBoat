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
        public SkillConfiguration()
        {
            InitializeComponent();
            //this.Shown += new System.EventHandler(this.FormLoaded);

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
                Debug.WriteLine(skill.Attribute("name").Value);
                this.Invoke((MethodInvoker)delegate
                {
                    skillComboBox.Items.Add(skill.Attribute("name").Value);

                });
            }
        }

        private void SkillComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            KeyDown -= ShortCutAssign;
        }
    }
}
