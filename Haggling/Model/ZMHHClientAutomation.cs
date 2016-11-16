
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using TestStack.White.Configuration;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
namespace Haggling.Model
{
    class ZMHHClientAutomation : AbstractAutomation
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private string processName = "CLIENT";
        private TestStack.White.UIItems.WindowItems.Window window;
        private TestStack.White.Application application;
        private AutomationElement code1;
        private AutomationElement code2;
        private AutomationElement price;
        private AutomationElement count;
        private AutomationElement reset;
        private AutomationElement submit;

        public ZMHHClientAutomation()
            : base()
        {
            CoreAppXmlConfiguration.Instance.FindWindowTimeout = 1000;
        }

        public override bool execute(Script script)
        {
            var currentTime = DateTime.Now;
            var serverTime = currentTime.ToString("HH:mm:ss");
            if (script.time.Equals(serverTime))
            {
                var hWnd = FindWindow("Tfrm_Dialog", null);
                ShowWindow(hWnd, 9);
                SetForegroundWindow(hWnd);
                var interval = 1000 - currentTime.Millisecond - script.interval;
                Thread.Sleep(interval);
                SendKeys.SendWait("{Enter}");
                return true;
            }
            return false;
        }

        public override int prepare(Script script, DateTime targetTime)
        {
            var hWnd = FindWindow("Tfrm_Dialog", null);
            ShowWindow(hWnd, 9);
            SetForegroundWindow(hWnd);
            Thread.Sleep(1000);
            SendKeys.SendWait("{Esc}");
            clean();
            validate();
            var code1V = (ValuePattern)code1.GetCurrentPattern(ValuePattern.Pattern);
            var code2V = (ValuePattern)code2.GetCurrentPattern(ValuePattern.Pattern);
            var priceV = (ValuePattern)price.GetCurrentPattern(ValuePattern.Pattern);
            var countV = (ValuePattern)count.GetCurrentPattern(ValuePattern.Pattern);
            code2.SetFocus();
            Thread.Sleep(1000);
            for (int i = 0; i < script.jobs.Count; i++) // current only supports one job
            {
                var job = script.jobs[i];
                if (job.side == "BUY")
                {
                    SendKeys.SendWait("{F1}");
                }
                else
                {
                    SendKeys.SendWait("{F2}");
                }

                foreach (var c in job.code.ToCharArray())
                {
                    SendKeys.SendWait(new string(new[] { c }));
                    Thread.Sleep(1000);
                }

                SendKeys.SendWait("{Enter}");
                price.SetFocus();
                priceV.SetValue(job.price);
                SendKeys.SendWait("{Enter}");

                count.SetFocus();
                countV.SetValue(job.count);
                SendKeys.SendWait("{Enter}");
                break;
            }

            SendKeys.SendWait("{Enter}");

            var currentTime = DateTime.Now;
            return (targetTime.Hour - currentTime.Hour) * 3600 * 1000 + (targetTime.Minute - currentTime.Minute) * 60 * 1000 + (targetTime.Second - currentTime.Second) * 1000;
        }

        public override void testScript(Script script)
        {

        }

        public override void clean()
        {
            code1 = null;
            code2 = null;
            price = null;
            count = null;
            reset = null;
            submit = null;
        }

        public override void dispose()
        {

        }

        public override bool validate()
        {
            if (processName != null)
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    try
                    {
                        application = TestStack.White.Application.Attach(processName);
                        window = application.GetWindow(SearchCriteria.ByClassName("Tfrm_Main"), InitializeOption.NoCache);
                        var items = window.AutomationElement.FindAll(TreeScope.Subtree, Condition.TrueCondition);
                        foreach (AutomationElement item in items)
                        {
                            if (item.Current.ClassName == "TS4ComboBox")
                            {
                                if (code1 == null)
                                {
                                    code1 = item;
                                }
                                else if (code2 == null)
                                {
                                    code2 = item;
                                }
                            }
                            else if (item.Current.ClassName == "TS4NumericEdit")
                            {
                                if (count == null)
                                {
                                    count = item;
                                }else if (price == null)
                                {
                                    price = item;
                                }
                            }
                            else if (item.Current.ClassName == "TRzBmpButton" && item.Current.Name == "重填")
                            {
                                if (reset == null)
                                {
                                    reset = item;
                                }
                            }
                            else if (item.Current.ClassName == "TRzBmpButton" && (item.Current.Name == "买入" || item.Current.Name == "卖出"))
                            {
                                if (submit == null)
                                {
                                    submit = item;
                                }
                            } 
                        }
                        
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e.Message);
                        var hWnd = FindWindow("Tfrm_Main", null);
                        ShowWindow(hWnd, 9);
                        SetForegroundWindow(hWnd);
                    }
                }
            }

            return false;
        }

        public override Array getResponseTime()
        {
            return new[] { 0, 0 };
        }
    }
}