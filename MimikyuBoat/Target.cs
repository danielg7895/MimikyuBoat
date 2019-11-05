using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimikyuBoat
{
    class Target {
        public int hp;

        public int hpBarStart;

        public int? hpRow = null;
        public int? mpRow = null;
        public int? cpRow = null;
        public bool isDead = false;

        public string imagePath = "temp/target.jpeg";


        #region eventos
        public delegate void OnHPChanged();
        public event OnHPChanged HPChanged;

        public delegate void OnDead();
        public event OnDead Dead; // TODO: pensar un mejor nombre... es horrible esto!
        #endregion 
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

        public void SetHP(int newHP)
        {
            // Seteo nueva hp y llamo a los metodos que necesiten hacer algo con esto.
            this.hp = newHP;
            if (hp == 0)
            {
                isDead = true;
                Dead.Invoke();
                return;
            }
            HPChanged.Invoke();
        }

    }
}
