
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Diagnostics;
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
                int millisecond = 0;
                try{
                    var ticks = (long)driver.ExecuteAsyncScript(@"var done=arguments[0];$.get('/exchange/public/serverTime').then(function(resp){done(resp);});");
                    var time = orginalTime.AddMilliseconds(ticks);
                    millisecond = time.Millisecond;
                    serverTime = time.ToLongTimeString();
                }catch(Exception e){
                    Console.Out.WriteLine(e);
                }

                if (script.time.Equals(serverTime))
                {
                    driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 0, 0, 0));
                    
                    // 执行时间匹配，开始执行
                    try
                    {
                        driver.ExecuteAsyncScript(@"var millisecond=arguments[0];var times=arguments[1];var interval=arguments[2];var price=arguments[3];var quantity=arguments[4];var symbol=arguments[5];var threshold=interval<100?100:interval;var order=()=>{$.ajax({type:'POST',url:'/exchange/private/order',data:$.param({price:price,quantity:quantity,symbol:symbol,side:'BUY',type:'LIMIT'}),headers:{CSRFToken:$.md5(document.cookie.match('CSRFToken=\.+?;')[0].split('=')[1].replace(';',''))}});};if(millisecond>1000-threshold){for(var i=0;i<times;i++){setTimeout(order,interval*i);}}else{var later=1000-millisecond-threshold;setTimeout(()=>{for(var i=0;i<times;i++){setTimeout(order,interval*i);}},later);}", millisecond, script.times, script.interval, script.price, script.count, script.code);
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
    }
}
