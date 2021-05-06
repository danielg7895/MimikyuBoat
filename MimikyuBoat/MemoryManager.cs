using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shizui
{
    class MemoryManager
    {
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        ProcessModule NWindowDLL = null;

        #region singleton
        private static MemoryManager _instance;
        public static MemoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MemoryManager();
                }
                return _instance;
            }
        }
        #endregion

        public void Run()
        {
            var modules = BotSettings.L2_PROCESS.Modules;

            foreach (ProcessModule module in modules)
            {
                if (module.ModuleName == "NWindow.DLL")
                {
                    NWindowDLL = module;
                }
            }
            GetPlayerHp();
            GetTargetHp();
        }

        public int GetPlayerHp()
        {
            return 100;
            byte[] buffer = new byte[4];
            int bytesRead = 0;
            int playerHp = 0;

            try
            {
                IntPtr l2hwnd = BotSettings.L2_PROCESS_HANDLER;

                var baseHPAddress = NWindowDLL.BaseAddress + 0x01386C30; 
                //Console.WriteLine(baseHPAddress);

                ReadProcessMemory(l2hwnd, baseHPAddress, buffer, buffer.Length, ref bytesRead);
                IntPtr baseHPAddress2 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress2);

                ReadProcessMemory(l2hwnd, baseHPAddress2 + 0x2C, buffer, buffer.Length, ref bytesRead);
                IntPtr baseHPAddress3 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress3);

                ReadProcessMemory(l2hwnd, baseHPAddress3 + 0x10, buffer, buffer.Length, ref bytesRead);
                playerHp = BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(playerHp);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error al leer memoria para la HP");
            }

            Player.Instance.hp = playerHp;
            return playerHp;
        }


        public int GetTargetHp()
        {
            return 100;
            byte[] buffer = new byte[4];
            int bytesRead = 0;
            int targetHp = 0;

            try
            {
                //Console.WriteLine("################$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                IntPtr l2hwnd = BotSettings.L2_PROCESS_HANDLER;
                var baseHPAddress = NWindowDLL.BaseAddress + 0x01386B80;
                //Console.WriteLine(baseHPAddress);
                ReadProcessMemory(l2hwnd, baseHPAddress, buffer, buffer.Length, ref bytesRead);

                IntPtr baseHPAddress2 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress2);
                ReadProcessMemory(l2hwnd, baseHPAddress2 + 0xD4, buffer, buffer.Length, ref bytesRead);

                IntPtr baseHPAddress3 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress3);
                ReadProcessMemory(l2hwnd, baseHPAddress3 + 0x8D0, buffer, buffer.Length, ref bytesRead);

                IntPtr baseHPAddress4 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress4);
                ReadProcessMemory(l2hwnd, baseHPAddress4 + 0x3C, buffer, buffer.Length, ref bytesRead);

                IntPtr baseHPAddress5 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress5);
                ReadProcessMemory(l2hwnd, baseHPAddress5 + 0xC4, buffer, buffer.Length, ref bytesRead);

                IntPtr baseHPAddress6 = (IntPtr)BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(baseHPAddress6);
                ReadProcessMemory(l2hwnd, baseHPAddress6 + 0x220, buffer, buffer.Length, ref bytesRead);

                targetHp = BitConverter.ToInt32(buffer, 0);
                //Console.WriteLine(targetHp);

                //Console.WriteLine("################$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error al leer memoria para la HP");
            }
            Target.Instance.hp = targetHp;

            return targetHp;
        }

    }
}
