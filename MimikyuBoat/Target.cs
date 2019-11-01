using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimikyuBoat
{
    class Target {
        public int cp;
        public int hp;
        public int mp;

        public int hpBarStart;

        public int? hpRow = null;
        public int? mpRow = null;
        public int? cpRow = null;

        public string imagePath = "temp/target.jpeg";

        #region singleton
        private static Target _instance;
        public static Target Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Target();
                }
                return _instance;
            }
        }
        #endregion
    }
}
