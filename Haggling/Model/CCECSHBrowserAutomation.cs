
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Threading;
namespace Haggling.Model
{
    class CCECSHBrowserAutomation : AbstractAutomation
    {
        private string Url = "https://www.ccecsh.com/exchange/";
        private InternetExplorerDriverService driverService;
        private InternetExplorerDriver driver;


        public CCECSHBrowserAutomation()
            : base()
        {
        }


        public override bool execute(Script script)
        {
            try
            {
                var serverTimeElement = driver.FindElement(By.XPath("//span[@class='servertime ng-binding']"));
                var serverTime = serverTimeElement.Text.Split(' ')[1];
                if (script.time.Equals(serverTime))
                {
                    // 执行时间匹配，开始执行

                    return true;
                }
                else
                {
                    // 执行时间不匹配，等待下一次
                    return false;
                }
            }
            catch (Exception e)
            {
                // 出现异常，终止脚本
                throw e;
            }
        }

        public override void dispose()
        {
            try
            {
                driver.Dispose();
                driverService.Dispose();
            }
            catch (Exception)
            {

            }
        }

        public override bool validate()
        {
            try
            {
                if (this.driverService == null)
                {
                    this.driverService = InternetExplorerDriverService.CreateDefaultService();
                    this.driverService.HideCommandPromptWindow = true;
                }

                if (this.driver == null)
                {
                    driver = new InternetExplorerDriver(this.driverService);
                    driver.Url = this.Url;
                }
                driver.FindElement(By.ClassName("accountid"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
