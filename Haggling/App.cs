using Haggling.Factory;
using Haggling.Model;
using Haggling.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Threading;
using System.Windows.Forms;
using TestStack.White.Factory;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace Haggling
{
    public partial class App : Form
    {
        private Factor factor = new Factor();

        private AbstractAutomation aa = null;

        private Script script = new Script();

        public App()
        {
            InitializeComponent();
            var agents = new[] { new { Text = "上文众申", Value = AgentType.CCECSH } };
            this.agent.DataSource = agents;
            this.agent.SelectedIndex = -1;
        }

        private void Start_Web_Click(object sender, EventArgs e)
        {
            using (var driverService = InternetExplorerDriverService.CreateDefaultService())
            {
                driverService.HideCommandPromptWindow = true;
                IWebDriver driver = new InternetExplorerDriver(driverService);
                driver.Url = "https://www.ccecsh.com/exchange/";

                while (true)
                {
                    try
                    {
                        driver.FindElement(By.XPath("//input[@class='f-center ng-scope']"));
                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                var searchInput = driver.FindElement(By.Id("searchKeyword"));

                var buyForm = driver.FindElement(By.Name("buyform"));
                var buyInPrice = buyForm.FindElement(By.Name("price"));
                var buyInCount = buyForm.FindElement(By.Id("buy_order"));
                var buySubmit = buyForm.FindElement(By.XPath("//input[@class='subbtn subbtn-red']"));

                searchInput.SendKeys("6001002");
                buyInPrice.SendKeys("1400");
                buyInCount.SendKeys("1000");

                buySubmit.SendKeys(OpenQA.Selenium.Keys.Enter);
            }
        }

        private void Start_UI_Click(object sender, EventArgs e)
        {
            var application = TestStack.White.Application.Attach(@"BIJIE");
            var window = application.GetWindow(SearchCriteria.ByAutomationId("FormMain"), InitializeOption.WithCache);

            if (window.DisplayState == DisplayState.Minimized)
            {
                window.Focus(DisplayState.Restored);
            }

            window.Focus(DisplayState.Maximized);

            var panelBottom = window.ModalWindow(SearchCriteria.ByAutomationId("panelBottom"), InitializeOption.WithCache);
            var frmDealMain = panelBottom.ModalWindow(SearchCriteria.ByAutomationId("FrmDealMain"), InitializeOption.WithCache);
            var panelDeal = frmDealMain.ModalWindow(SearchCriteria.ByAutomationId("panelDeal"), InitializeOption.WithCache);
            var frmDealBuy = panelDeal.ModalWindow(SearchCriteria.ByAutomationId("FrmDealBuy"), InitializeOption.WithCache);
            var panelDeal2 = frmDealBuy.ModalWindow(SearchCriteria.ByAutomationId("panelDeal"), InitializeOption.WithCache);

            var code = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("txtCode"), InitializeOption.WithCache);
            var price = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("txtPrice"), InitializeOption.WithCache);
            var count = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("txtCount"), InitializeOption.WithCache);


            var reset = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("btn_setting"), InitializeOption.WithCache);
            var submit = panelDeal2.ModalWindow(SearchCriteria.ByAutomationId("btn_sure"), InitializeOption.WithCache);


            reset.Click();

            code.SetValue("6001002");

            code.Focus();
            code.DoubleClick();
            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.BACKSPACE);
            Keyboard.Instance.Send("6001002", code.ActionListener);

            price.Focus();
            price.DoubleClick();
            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.BACKSPACE);
            Keyboard.Instance.Send("66.00", price.ActionListener);

            count.Focus();
            count.DoubleClick();
            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.BACKSPACE);
            Keyboard.Instance.Send("1000", count.ActionListener);

            submit.Click();

            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.ESCAPE);

            Console.Out.WriteLine("finish");
        }

        private void validateLaunchState()
        {
            this.launchButton.Enabled = false;
            if (factor.Type == AgentType.UNDEFINED)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_AGENT;
                return;
            }

            if (factor.Mode == ModeType.UNDEFINED)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_MODE;
                return;
            }
            else if (factor.Mode == ModeType.BROWSER)
            {
                this.launchButton.Enabled = true;
                this.statusContent.Text = Resources.STATUS_CONTENT_BROWSER;
                this.launchButton.Text = Resources.LAUNCH_BUTTON_TITLE;
            }
            else if (factor.Mode == ModeType.CLIENT)
            {
                this.launchButton.Enabled = true;
                this.statusContent.Text = Resources.STATUS_CONTENT_CLIENT;
                this.launchButton.Text = Resources.VALIDATE_BUTTON_TITLE;
            }
        }

        private void agent_SelectedIndexChanged(object sender, EventArgs e)
        {

            factor.Type = this.agent.SelectedValue == null ? AgentType.UNDEFINED : (AgentType)this.agent.SelectedValue;
            validateLaunchState();
        }

        private void browserMode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.browserMode.Checked)
            {
                factor.Mode = ModeType.BROWSER;
                validateLaunchState();
            }
        }

        private void clientMode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.clientMode.Checked)
            {
                factor.Mode = ModeType.CLIENT;
                validateLaunchState();
            }
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                aa.dispose();
                aa = null;
            }

            aa = AutomationFactory.Create(this.factor);
            if (aa == null)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_FAILED;
            }
            else
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_CHECKING;
                this.launchButton.Enabled = false;
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        while (!aa.validate())
                        {
                            Thread.Sleep(5000);
                            this.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_FAILED;
                                this.enableScript(false);
                            });
                        }
                        aa.validate();
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_SUCCESS;
                            this.enableScript(true);
                            // 验证成功后，运行守护线程保证浏览器正常运行
                            this.sync.Start();
                        });
                    }
                    catch (Exception)
                    {
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.statusContent.Text = Resources.STATUS_CONTENT_BROWSER_CLOSE;
                            this.enableScript(false);
                            this.launchButton.Enabled = true;
                        });
                    }
                })).Start();
            }
        }

        private void enableScript(bool enabled)
        {
            this.time.Enabled = enabled;
            this.code.Enabled = enabled;
            this.price.Enabled = enabled;
            this.count.Enabled = enabled;
            this.times.Enabled = enabled;
            this.interval.Enabled = enabled;
            this.executeScript.Enabled = enabled;
        }

        private void startScript()
        {
            this.aa.clean();
            this.executeScript.Text = "停止";
            this.executeScript.Tag = "1";
            this.scriptTimer.Start();
        }

        private void stopScript()
        {
            this.executeScript.Text = "执行";
            this.executeScript.Tag = "0";
            this.scriptTimer.Stop();
            this.sync.Stop();
        }

        private void executeScript_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                var tag = this.executeScript.Tag as string;
                if (tag == "0")
                {
                    // 执行
                    if (string.IsNullOrWhiteSpace(this.time.Text)
                    || string.IsNullOrWhiteSpace(this.code.Text)
                    || string.IsNullOrWhiteSpace(this.price.Text)
                    || string.IsNullOrWhiteSpace(this.count.Text))
                    {
                        this.statusContent.Text = Resources.STATUS_CONTENT_INPUT;
                        return;
                    }

                    script.time = this.time.Text;
                    script.code = this.code.Text;
                    script.price = this.price.Text;
                    script.count = this.count.Text;
                    script.times = Decimal.ToInt32(this.times.Value);
                    script.interval = this.interval.IntValue;
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTING;
                    this.startScript();
                }
                else if (tag == "1")
                {
                    // 停止
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_STOP;
                    this.stopScript();
                }
            }
        }

        private void scriptTimer_Tick(object sender, EventArgs e)
        {
            if (aa != null)
            {
                try
                {
                    var result = aa.execute(script);
                    if (result)
                    {
                        // 执行脚本成功
                        this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_SUCCESS;
                        this.stopScript();
                    }
                    else
                    {
                        // 继续执行
                    }
                }
                catch (Exception)
                {
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_FAILED;
                    this.stopScript();
                    this.enableScript(false);
                    this.launchButton.Enabled = true;
                }
            }
        }

        private void sync_Tick(object sender, EventArgs e)
        {
            if (aa != null)
            {
                try
                {
                    aa.validate();
                }
                catch (Exception)
                {
                    // 异常终止
                    this.statusContent.Text = Resources.STATUS_CONTENT_BROWSER_CLOSE;
                    this.enableScript(false);
                    this.launchButton.Enabled = true;
                }
            }
        }

        protected override void DestroyHandle()
        {
            if (aa != null)
            {
                this.stopScript();
                aa.dispose();
            }

            base.DestroyHandle();
            Environment.Exit(0);
        }
    }
}