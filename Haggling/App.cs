using System;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Threading;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.Factory;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;
using TestStack.White.InputDevices;

namespace Haggling
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
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
    }
}