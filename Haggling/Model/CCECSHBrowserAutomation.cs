
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Threading.Tasks;
namespace Haggling.Model
{
    class CCECSHBrowserAutomation : AbstractAutomation
    {
        private readonly DateTime orginalTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), TimeZoneInfo.Local);
        
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
                var millisecond = 0;
                try
                {
                    var request = WebRequest.Create("https://www.ccecsh.com/exchange/public/serverTime");
                    var response = request.GetResponse();
                    var streamReader = new StreamReader(response.GetResponseStream());
                    var responseContent = streamReader.ReadToEnd();
                    response.Close();
                    var currentTime = orginalTime.AddMilliseconds(long.Parse(responseContent));
                    millisecond = currentTime.Millisecond;
                    serverTime = currentTime.ToLongTimeString();
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }

                if (script.time.Equals(serverTime))
                {
                    var prices = new string[script.jobs.Count];
                    var quantities = new string[script.jobs.Count];
                    var symbols = new string[script.jobs.Count];
                    var sides = new string[script.jobs.Count];

                    for (int i = 0; i < script.jobs.Count; i++)
                    {
                        var job = script.jobs[i];
                        prices[i] = job.price;
                        quantities[i] = job.count;
                        symbols[i] = job.code;
                        sides[i] = job.side;
                    }

                    // 执行时间匹配，开始执行
                    try
                    {
                        driver.ExecuteAsyncScript(@"var done=arguments[arguments.length-1];var times=arguments[0];var interval=arguments[1];var count=arguments[2];var prices=arguments[3];var quantities=arguments[4];var symbols=arguments[5];var sides=arguments[6];for(var i=0;i<count;i++){var request=()=>{var price=prices[i];var quantity=quantities[i];var symbol=symbols[i];var side=sides[i];var failCount=0;var order=()=>{if(failCount>=times){return;}
$.ajax({type:'POST',url:'/exchange/private/order',data:$.param({price:price,quantity:quantity,symbol:symbol,side:side,type:'LIMIT'}),headers:{CSRFToken:$.md5(document.cookie.match('CSRFToken=\.+?;')[0].split('=')[1].replace(';',''))},error:function(){failCount++;order();}});};$.get('/exchange/public/serverTime').then(function(resp){var date=new Date(resp);var millisecond=date.getMilliseconds();var later=1000-interval-millisecond;setTimeout(()=>{order();},later);});}
request();}
done(0);", script.times, script.interval, script.jobs.Count, prices, quantities, symbols, sides);
                    }
                    catch (Exception)
                    {
                    }
                    //try
                    //{
                    //    var later = 1000 - millisecond - script.interval;
                    //    if (later < 0)
                    //    {
                    //        later = 0;
                    //    }
                    //    Thread.Sleep(later);
                    //}
                    //catch (Exception)
                    //{
                    //}

                    //foreach (var job in script.jobs)
                    //{

                    //    var jobTask = new JobTask(job, this.cookieString, this.CSRFToken, script.times);
                    //    jobTask.run();
                    //}

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

        public void testScript(Script script)
        {
            foreach (var job in script.jobs)
            {

                var jobTask = new JobTask(job, this.cookieString, this.CSRFToken, 1);
                jobTask.run();
            }
        }
    }
}