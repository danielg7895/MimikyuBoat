using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MimikyuBoat
{
    class Utils
    {
        // clase encargada de metodos basuras intermediarios con las forms que no quiero meter en otros lados.
        Form1 form;


        #region singleton
        private static Utils _instance;
        public static Utils Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Utils();
                }
                return _instance;
            }
        }
        #endregion

        public Utils()
        {
            form = (Form1)Application.OpenForms[0];
        }

        public void ConsoleWrite(string text)
        {
            form.ConsoleWrite(text);
        }

    }
}
