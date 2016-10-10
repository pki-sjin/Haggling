
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace Haggling.Model
{
    class CCECSHBrowserAutomation : AbstractAutomation
    {
        private string Url = "https://www.ccecsh.com/exchange/";
        private readonly DateTime orginalTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), TimeZoneInfo.Local);
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
                    driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 0));

                    // 执行时间匹配，开始执行
                    try
                    {
                        driver.ExecuteAsyncScript(@"var times=arguments[0];var interval=arguments[1];var price=arguments[2];var quantity=arguments[3];var symbol=arguments[4];var failCount=0;var order=()=>{if(failCount>=times){return;}
$.ajax({type:'POST',url:'/exchange/private/order',data:$.param({price:price,quantity:quantity,symbol:symbol,side:'BUY',type:'LIMIT'}),headers:{CSRFToken:$.md5(document.cookie.match('CSRFToken=\.+?;')[0].split('=')[1].replace(';',''))},error:function(){failCount++;order();}});};$.get('/exchange/public/serverTime').then(function(resp){var date=new Date(resp);var millisecond=date.getMilliseconds();var later=1000-interval-millisecond;setTimeout(()=>{order();},later);});", script.times, script.interval, script.price, script.count, script.code);
                    }
                    catch (Exception)
                    {
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
            var begin = DateTime.Now.Ticks;
            var request = WebRequest.Create("https://www.ccecsh.com/exchange/public/serverTime");
            request.Timeout = 5000;
            var response = request.GetResponse();
            var end = DateTime.Now.Ticks;
            response.Close();
            return end - begin;
        }
    }
}
