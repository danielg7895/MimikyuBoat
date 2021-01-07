using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shizui
{
    class CrossThreadHelper
    {
    }

    public delegate void crossThreadFunc();

    /*
    public void DoWork()
    {

        // Debug.WriteLine("invoke rekired");
        // We're on a thread other than the GUI thread
        // crossThreadFunc funcdel = new crossThreadFunc(crossThreadFuncExecute);
        // form1.BeginInvoke(funcdel);

        MethodInvoker inv = delegate
        {
            form1.ConsoleWrite("kappa");
        };

        form1.Invoke(inv);

    }

    public void crossThreadFuncExecute()
    {
        form1.ConsoleWrite("que onda wachoooooyuohdoibasoidbasiodbsa ghai");
    }
    public void DoWork()
    {
    form1.Invoke((MethodInvoker)delegate { form1.ConsoleWrite("awa"); });

    MethodInvoker inv = delegate
    {
    form1.ConsoleWrite("kappa");
    };

    form1.Invoke(inv);

    }
    */
}
