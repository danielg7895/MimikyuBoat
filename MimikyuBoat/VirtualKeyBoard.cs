using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shizui
{

    class VirtualKeyBoard
    {

        public enum VirtualKey
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
            VK_CANCEL = 0x03,
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            VK_CLEAR = 0x0c,
            VK_RETURN = 0x0d,
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_ESCAPE = 0x1b,
            VK_SPACE = 0x20,
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,
            VK_SELECT = 0x29,
            VK_EXECUTE = 0x2b,
            VK_SNAPSHOT = 0x2c,
            VK_INSERT = 0x2d,
            VK_DELETE = 0x2e,
            VK_HELP = 0x2f,
            VK_0 = 0x30,
            VK_1 = 0x31,
            VK_2 = 0x32,
            VK_3 = 0x33,
            VK_4 = 0x34,
            VK_5 = 0x35,
            VK_6 = 0x36,
            VK_7 = 0x37,
            VK_8 = 0x38,
            VK_9 = 0x39,
            VK_A = 0x41,
            VK_B = 0x42,
            VK_C = 0x43,
            VK_D = 0x44,
            VK_E = 0x45,
            VK_F = 0x46,
            VK_G = 0x47,
            VK_H = 0x48,
            VK_I = 0x49,
            VK_J = 0x4a,
            VK_K = 0x4b,
            VK_L = 0x4c,
            VK_M = 0x4d,
            VK_N = 0x4e,
            VK_O = 0x4f,
            VK_P = 0x50,
            VK_Q = 0x51,
            VK_R = 0x52,
            VK_S = 0x53,
            VK_T = 0x54,
            VK_U = 0x55,
            VK_V = 0x56,
            VK_W = 0x57,
            VK_X = 0x58,
            VK_Y = 0x59,
            VK_Z = 0x5a,
            VK_LWIN = 0x5b,
            VK_RWIN = 0x5c,
            VK_APPS = 0x5d,
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6a,
            VK_ADD = 0x6b,
            VK_SEPARATOR = 0x6c,
            VK_SUBTRACT = 0x6d,
            VK_DECIMAL = 0x6e,
            VK_DIVIDE = 0x6f,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7a,
            VK_F12 = 0x7b,

            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            VK_OEM_EQU = 0x92,
            VK_LSHIFT = 0xa0,
            VK_RSHIFT = 0xa1,
            VK_LCONTROL = 0xa2,
            VK_RCONTROL = 0xa3,
            VK_LMENU = 0xa4,
            VK_RMENU = 0xa5,

            VK_OEM_1 = 0xba,
            VK_OEM_PLUS = 0xbb,
            VK_OEM_COMMA = 0xbc,
            VK_OEM_MINUS = 0xbd,
            VK_OEM_PERIOD = 0xbe,
            VK_OEM_2 = 0xbf,        
            VK_OEM_3 = 0xc0,        
            VK_OEM_4 = 0xdb,        
            VK_OEM_5 = 0xdc,        
            VK_OEM_6 = 0xdd,        
            VK_OEM_7 = 0xde,        
            VK_OEM_8 = 0xdf
        }

        [Flags]
        public enum KeyMessageCodes
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101
        }

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        public static extern int OemKeyScan(char wOemChar); // para obtener las scancode de las keys.

        // ======================================= AGREGADO POR HOT FIX EVENTO ==========================
        [DllImport("gdi32.dll")]
        private unsafe static extern bool SetDeviceGammaRamp(Int32 hdc, void* ramp);

        private static bool initialized = false;
        private static Int32 hdc;


        private static void InitializeClass()
        {
            if (initialized)
                return;

            //Get the hardware device context of the screen, we can do
            //this by getting the graphics object of null (IntPtr.Zero)
            //then getting the HDC and converting that to an Int32.
            hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();

            initialized = true;
        }


        public unsafe bool SetBrightness(short brightness)
        {
            InitializeClass();

            if (brightness > 255)
                brightness = 255;

            if (brightness < 0)
                brightness = 0;

            short* gArray = stackalloc short[3 * 256];
            short* idx = gArray;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 256; i++)
                {
                    int arrayVal = i * (brightness + 128);

                    if (arrayVal > 65535)
                        arrayVal = 65535;

                    *idx = (short)arrayVal;
                    idx++;
                }
            }

            //For some reason, this always returns false?
            bool retVal = SetDeviceGammaRamp(hdc, gArray);

            //Memory allocated through stackalloc is automatically free'd
            //by the CLR.

            return retVal;
        }
        public void ActivateWindow(IntPtr processHandle)
        {
            PostMessage(processHandle, 0x006, 1, 0x0); // WM_ACTIVATE
            SetBrightness(128);
            Debug.Write("Ventana activada!");
        }

        // ======================================= FIN AGREGADO POR HOT FIX EVENTO ==========================

        public void SendKeyToProcess(IntPtr processHandle, VirtualKey key, int timePress = 100)
        {
            // Convierto la key recibida a char para poder luego obtener su scancode!
            KeysConverter keysConverter = new KeysConverter();
            char keyChar = (char)key; // (char)int.Parse(keysConverter.ConvertToString((int)key));

            // Obtengo el scancode de la key
            int scanCode = OemKeyScan(keyChar);

            // el lparam del l2 esta compuesto con 1bit seteado en key repeat count (bit 0-16) y
            // el scancode esta en los bit 16-23 (1 byte). entonces:
            uint lparam = 0;
            uint scanCodeParam = (uint)scanCode << 16; // me corro 16 bits porque es la posicion del scanCode
            uint keyRepeat = 1; // por defecto repito una vez!, no me corro porque son los primeros 16 bits
            lparam = lparam | scanCodeParam | keyRepeat;

            PostMessage(processHandle, (uint)KeyMessageCodes.WM_KEYDOWN, (uint)key, lparam);
            Thread.Sleep(timePress);
            PostMessage(processHandle, (uint)KeyMessageCodes.WM_KEYUP, (uint)key, lparam);
        }

        public void LongPressButton(IntPtr processHandle, List<Dictionary<string, int>> listKeyData)
        {
            // Loopeo la lista y a medida que el reloj avanza voy apretando las keys en orden.
            // Cada key tiene un tiempo de comienzo y un tiempo de final, el reloj coordina cuando
            // se aprietan las keys y cuando se detienen.
            // initialized funciona como marcador de cuando debe mandarse KeyPressUp y cuando KeyPressDown
            // breakwhile se le suma 1 cuando termina una key de ser apretada,
            // Cuando todas las keys de la lista suman 1 a breakwhile, significa q todas las keys fueron apretadas y hay que cortar el while.
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int breakWhile = 0;
            while (breakWhile != listKeyData.Count())
            {
                foreach (Dictionary<string, int> keyData in listKeyData)
                {

                    if (keyData["startAt"] <= watch.ElapsedMilliseconds && keyData["finished"] == 0)
                    {
                        PressKeyDown(processHandle, (VirtualKey)keyData["key"]);
                    }

                    if (keyData["endAt"] <= watch.ElapsedMilliseconds && keyData["finished"] == 0)
                    {
                        keyData["finished"] = 1;
                        breakWhile ++;
                        PressKeyUp(processHandle, (VirtualKey)keyData["key"]);
                    }
                }
                Thread.Sleep(10);
            }
            watch.Stop();
        }

        public void LongPressButton2(VirtualKey key, int ms)
        {
            IntPtr hwnd = BotSettings.L2_WINDOW_HANDLE;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            PressKeyDown(hwnd, key);
            while (watch.ElapsedMilliseconds < ms)
            {
                PressKeyDown(hwnd, key, true, 0);
                Thread.Sleep(15);
            }
            PressKeyUp(hwnd, key);
        }


        public void PressKeyDown(IntPtr processHandle, VirtualKey key, bool fRepeat = false, int timePress = 100)
        {
            // fRepeat -> se coloca en uno cuando se mantiene apretada la key
            // pero previamente debio haber sido enviada con fRepeat en 0
            // fRepeat esta en bit 30. https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keydown

            char keyChar = (char)key; 
            int scanCode = OemKeyScan(keyChar);
            
            uint lparam = 0;
            uint scanCodeParam = (uint)scanCode << 16; // me corro 16 bits porque es la posicion del scanCode
            uint keyRepeat = 1;
            lparam = lparam | scanCodeParam | keyRepeat;
            
            if (fRepeat)
                lparam = (1 << 30) | lparam;

            PostMessage(processHandle, (uint)KeyMessageCodes.WM_KEYDOWN, (uint)key, lparam);
            if (timePress > 0)
                Thread.Sleep(timePress);
        }

        public void PressKeyUp(IntPtr processHandle, VirtualKey key)
        {
            // Convierto la key recibida a char para poder luego obtener su scancode!
            KeysConverter keysConverter = new KeysConverter();
            char keyChar = (char)key; // (char)int.Parse(keysConverter.ConvertToString((int)key));

            // Obtengo el scancode de la key
            int scanCode = OemKeyScan(keyChar);

            // el lparam del l2 esta compuesto con 1bit seteado en key repeat count (bit 0-16) y
            // el scancode esta en los bit 16-23 (1 byte). entonces:
            uint lparam = 0;
            uint scanCodeParam = (uint)scanCode << 16; // me corro 16 bits porque es la posicion del scanCode
            uint keyRepeat = 1; // por defecto repito una vez!, no me corro porque son los primeros 16 bits
            lparam = lparam | scanCodeParam | keyRepeat;

            PostMessage(processHandle, (uint)KeyMessageCodes.WM_KEYUP, (uint)key, lparam);
        }

    }
}
