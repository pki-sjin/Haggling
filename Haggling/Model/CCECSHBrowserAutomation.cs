
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Haggling.Model
{
    class CCECSHBrowserAutomation : AbstractAutomation
    {
        private string Url = "https://www.ccecsh.com/exchange/";
        private ChromeDriverService driverService;
        private ChromeDriver driver;

        private string cookieString;
        private string CSRFToken;

        public CCECSHBrowserAutomation()
            : base()
        {
        }


        public override bool execute(Script script)
        {
            try
            {
                string serverTime = null;
                try
                {
                    serverTime = (string)driver.ExecuteAsyncScript(@"var done=arguments[0];$.get('/exchange/public/serverTime').then(function(resp){var date=new Date(resp);var time=date.toTimeString().match('\.+? ')[0].trim();done(time);});");
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }

                if (script.time.Equals(serverTime))
                {
                    foreach (var job in script.jobs)
                    {

                        Task task = new Task(() => {
                            var jobTask = new JobTask(job, this.cookieString, this.CSRFToken, script.times, script.interval);
                            jobTask.run();
                        });
                        
                        task.Start();
                    }

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

        public override void clean()
        {
            driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 500));
        }

        public override void dispose()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
            }
            try
            {
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
                    this.driverService = ChromeDriverService.CreateDefaultService();
                    this.driverService.HideCommandPromptWindow = true;
                }

                if (this.driver == null)
                {
                    driver = new ChromeDriver(this.driverService);
                    driver.Manage().Window.Maximize();
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

        public override long getResponseTime()
        {
            try
            {
                var begin = DateTime.Now.Ticks;
                var request = WebRequest.Create("https://www.ccecsh.com/exchange/public/serverTime");
                request.Timeout = 5000;
                var response = request.GetResponse();
                var end = DateTime.Now.Ticks;
                response.Close();
                return end - begin;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void getHeader()
        {
            this.cookieString = "";
            foreach (var cookie in driver.Manage().Cookies.AllCookies)
            {
                this.cookieString += cookie.Name + "=" + cookie.Value + ";";
                if (cookie.Name == "CSRFToken")
                {
                    this.CSRFToken = cookie.Value;
                }
            }
        }
    }
}