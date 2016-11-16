
using Haggling.Properties;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
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

        public override int prepare(Script script, DateTime targetTime)
        {
            var request = WebRequest.Create("https://www.ccecsh.com/exchange/public/serverTime");
            var response = request.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            var responseContent = streamReader.ReadToEnd();
            response.Close();
            var currentTime = orginalTime.AddMilliseconds(long.Parse(responseContent));
            
            return (targetTime.Hour - currentTime.Hour) * 3600 * 1000 + (targetTime.Minute - currentTime.Minute) * 60 * 1000 + (targetTime.Second - currentTime.Second) * 1000;
        }

        public override bool execute(Script script)
        {
            try
            {
                string serverTime = null;
                try
                {
                    serverTime = (string)driver.ExecuteAsyncScript(@"var done=arguments[arguments.length-1];$.get('/exchange/public/serverTime').then(function(resp){var date=new Date(resp);var time=date.toTimeString().match('\.+? ')[0].trim();done(time);});");
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }

                if (script.time.Equals(serverTime))
                {
                    driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 0));

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
                        driver.ExecuteAsyncScript(Resources.order, script.times, script.interval, script.orderWait, script.jobs.Count, prices, quantities, symbols, sides);
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e);
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

        public override Array getResponseTime()
        {
            driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 500));
            ReadOnlyCollection<object> times = null;
            try
            {
                times = (ReadOnlyCollection<object>)driver.ExecuteAsyncScript(@"var done=arguments[arguments.length-1];var start=new Date();$.get('/exchange/public/serverTime').then(function(resp){var end=new Date();var server=new Date(resp);done([end-start, server-end]);});");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }

            if (times != null)
            {
                return new[] { times[0], times[1] };
            }
            else
            {
                return new[] { 0, 0 };
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

        public override void testScript(Script script)
        {
            foreach (var job in script.jobs)
            {

                var jobTask = new JobTask(job, this.cookieString, this.CSRFToken, 1);
                jobTask.run();
            }
        }

        public void sellAndBuy(Script script)
        {

            for (int i = 0; i < script.jobs.Count; i++)
            {
                var job = script.jobs[i];
                try
                {
                    driver.ExecuteAsyncScript(Resources.sellbuy, job.code, job.count);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }
            }
        }

        public long getOrderResponse(Script script)
        {
            driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 500));
            long time = 0;
            for (int i = 0; i < script.jobs.Count; i++)
            {
                var job = script.jobs[i];
                try
                {
                    time = (long)driver.ExecuteAsyncScript(Resources.speed, job.code, job.price);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }
            }
            return time;
        }

        public void readLogs()
        {
            driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 500));
            try
            {
                ReadOnlyCollection<object> logs = null;
                try
                {
                    logs = (ReadOnlyCollection<object>)driver.ExecuteAsyncScript(Resources.log);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }
                if (logs != null && logs.Count > 0)
                {
                    var content = "";
                    foreach (string log in logs)
                    {
                        dynamic obj = JsonConvert.DeserializeObject(log);
                        var time = obj.time;
                        var data = obj.data;
                        if (data.transactTime != null)
                        {
                            var transactTime = orginalTime.AddMilliseconds((long)data.transactTime);
                            data.transactTime = transactTime.ToString(@"yyyy-MM-dd HH:mm:ss.FFF");
                        }
                        var currentTime = orginalTime.AddMilliseconds((long)time);
                        content += currentTime.ToString(@"yyyy-MM-dd HH:mm:ss.FFF");
                        content += ":";
                        content += JsonConvert.SerializeObject(data);
                        content += "\r\n";
                    }

                    File.AppendAllText(Environment.CurrentDirectory + @"\log.txt", content);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }
        }
    }
}