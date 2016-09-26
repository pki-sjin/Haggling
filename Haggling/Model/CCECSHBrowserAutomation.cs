
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Haggling.Model
{
    class CCECSHBrowserAutomation : AbstractAutomation
    {
        private string Url = "https://www.ccecsh.com/exchange/";
        private ChromeDriverService driverService;
        private ChromeDriver driver;
        

        public CCECSHBrowserAutomation()
            : base()
        {
        }


        public override bool execute(Script script)
        {
            try
            {
                string serverTime = null;
                try{
                    serverTime = (string)driver.ExecuteAsyncScript(@"var done=arguments[0];$.get('/exchange/public/serverTime').then(function(resp){var date=new Date(resp);var time=date.toTimeString().match('\.+? ')[0].trim();done(time);});");
                }catch(Exception e){
                    Console.Out.WriteLine(e);
                }

                if (script.time.Equals(serverTime))
                {
                    driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 0));
                    // 执行时间匹配，开始执行
                    new Thread(new ThreadStart(() => {
                        for (int i = 0; i < script.times; i++)
                        {
                            var task = new Task(() =>
                            {
                                try
                                {
                                    driver.ExecuteAsyncScript(@"$.ajax({type:'POST',url:'/exchange/private/order',data:$.param({price:arguments[0],quantity:arguments[1],symbol:arguments[2],side:'BUY',type:'LIMIT'}),headers:{CSRFToken:$.md5(document.cookie.match('CSRFToken=\.+?;')[0].split('=')[1].replace(';',''))}});", script.price, script.count, script.code);
                                }
                                catch (Exception)
                                {
                                }
                            });
                            task.Start();
                            Thread.Sleep(script.interval);
                        }
                    })).Start();
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
                driver.Close();
                driver.Dispose();
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
    }
}
