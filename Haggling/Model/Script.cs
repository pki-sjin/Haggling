using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Haggling.Model
{
    public class Script
    {
        public string time { get; set; }
        public List<Job> jobs = new List<Job>();
        public int times { get; set; }
        public int interval { get; set; }
    }

    public class Job {
        public string code { get; set; }
        public string price { get; set; }
        public string count { get; set; }
        public string side { get; set; }
    }

    public class JobTask
    {
        private readonly DateTime orginalTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), TimeZoneInfo.Local);
        private readonly Job job;
        private readonly string cookie;
        private readonly string token;
        private readonly int times;
        private readonly int interval;
        private int failCount = 0;
        public JobTask(Job job, string cookie, string token, int times, int interval)
        {
            this.job = job;
            this.cookie = cookie;
            this.token = token;
            this.times = times;
            this.interval = interval;
        }

        public void run()
        {
            try
            {
                var request = WebRequest.Create("https://www.ccecsh.com/exchange/public/serverTime");
                var response = request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                var responseContent = streamReader.ReadToEnd();
                response.Close();
                var millisecond = orginalTime.AddMilliseconds(long.Parse(responseContent)).Millisecond;
                var later = 1000 - interval - millisecond;
                Thread.Sleep(later);
            }
            catch (Exception)
            {
            }

            execute();
        }

        private void execute()
        {
            if (failCount >= times)
            {
                return;
            }

            try
            {
                var request = WebRequest.Create("https://www.ccecsh.com/exchange/private/order");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("CSRFToken", MD5Encrypt(this.token));
                request.Headers.Add("Cookie", this.cookie);
                byte[] data = Encoding.UTF8.GetBytes("price=" + job.price + "&quantity=" + job.count + "&symbol=" + job.code + "&side=" + job.side + "&type=LIMIT");
                request.ContentLength = data.Length;
                var writer = request.GetRequestStream();
                writer.Write(data, 0, data.Length);
                writer.Close();
                var response = request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                var responseContent = streamReader.ReadToEnd();
                response.Close();
            }
            catch (Exception)
            {
                failCount++;
                execute();
            }
        }

        private string MD5Encrypt(string strText)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(strText);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        } 
    }
}
