using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
