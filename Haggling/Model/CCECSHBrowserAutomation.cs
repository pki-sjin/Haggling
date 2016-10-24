﻿
using Haggling.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
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
                        driver.ExecuteAsyncScript(Resources.order, script.times, script.interval, script.jobs.Count, prices, quantities, symbols, sides);
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

        public override long getResponseTime()
        {
            driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 500));
            long time = 0;
            try
            {
                time = (long)driver.ExecuteAsyncScript(@"var done=arguments[0];var start=new Date();$.get('/exchange/public/serverTime').then(function(resp){var end=new Date();done(end-start);});");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }

            return time;
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
    }
}