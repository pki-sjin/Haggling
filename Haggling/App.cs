using Haggling.Factory;
using Haggling.Model;
using Haggling.Properties;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Haggling
{
    public partial class App : Form
    {
        private readonly DateTime orginalTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), TimeZoneInfo.Local);
        
        private Factor factor = new Factor();

        private AbstractAutomation aa = null;

        private Script script = new Script();

        public App()
        {
            InitializeComponent();
            var agents = new[] { new { Text = "上文众申", Value = AgentType.CCECSH }, new { Text = "郑棉黄海", Value = AgentType.ZMHH } };
            this.agent.DataSource = agents;
            this.agent.SelectedIndex = -1;
            var sides = new[] { new { Text = "买", Value = "BUY" }, new { Text = "卖", Value = "SELL" } };
            var sideColumn = this.scriptData.Columns["side"] as DataGridViewComboBoxColumn;
            sideColumn.DataSource = sides;
            sideColumn.DisplayMember = "Text";
            sideColumn.ValueMember = "Value";
        }

        private void validateLaunchState()
        {
            this.launchButton.Enabled = false;
            if (factor.Type == AgentType.UNDEFINED)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_AGENT;
                return;
            }

            if (factor.Mode == ModeType.UNDEFINED)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_MODE;
                return;
            }
            else if (factor.Mode == ModeType.BROWSER)
            {
                this.launchButton.Enabled = true;
                this.statusContent.Text = Resources.STATUS_CONTENT_BROWSER;
                this.launchButton.Text = Resources.LAUNCH_BUTTON_TITLE;
            }
            else if (factor.Mode == ModeType.CLIENT)
            {
                this.launchButton.Enabled = true;
                this.statusContent.Text = Resources.STATUS_CONTENT_CLIENT;
                this.launchButton.Text = Resources.VALIDATE_BUTTON_TITLE;
            }
        }

        private void agent_SelectedIndexChanged(object sender, EventArgs e)
        {

            factor.Type = this.agent.SelectedValue == null ? AgentType.UNDEFINED : (AgentType)this.agent.SelectedValue;
            if (factor.Type == AgentType.CCECSH)
            {
                this.browserMode.Visible = true;
                this.clientMode.Visible = false;
                this.clientMode.Checked = false;
                if (this.browserMode.Checked)
                {
                    factor.Mode = ModeType.BROWSER;
                }
                else
                {
                    factor.Mode = ModeType.UNDEFINED;
                }
            }
            else if (factor.Type == AgentType.ZMHH)
            {
                factor.Mode = ModeType.UNDEFINED;
                this.browserMode.Checked = false;
                this.browserMode.Visible = false;
                this.clientMode.Visible = true;
                if (this.clientMode.Checked)
                {
                    factor.Mode = ModeType.CLIENT;
                }
                else
                {
                    factor.Mode = ModeType.UNDEFINED;
                }
            }
            validateLaunchState();
        }

        private void browserMode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.browserMode.Checked)
            {
                factor.Mode = ModeType.BROWSER;
                validateLaunchState();
            }
        }

        private void clientMode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.clientMode.Checked)
            {
                factor.Mode = ModeType.CLIENT;
                validateLaunchState();
            }
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                aa.dispose();
                aa = null;
            }

            aa = AutomationFactory.Create(this.factor);
            if (aa == null)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_FAILED;
            }
            else
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_CHECKING;
                this.launchButton.Enabled = false;
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        while (!aa.validate())
                        {
                            Thread.Sleep(5000);
                            this.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_FAILED;
                                this.enableScript(false);
                            });
                        }
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.statusContent.Text = Resources.STATUS_CONTENT_CHECK_SUCCESS;
                            this.enableScript(true);
                            var a = aa as CCECSHBrowserAutomation;
                            if (a != null)
                            {
                                a.getHeader();
                            }
                            // 验证成功后，运行守护线程保证浏览器正常运行
                            this.sync.Start();
                        });
                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.statusContent.Text = Resources.STATUS_CONTENT_BROWSER_CLOSE + "\r\n" + ex.Message;
                            this.enableScript(false);
                            this.launchButton.Enabled = true;
                        });
                    }
                })).Start();
            }
        }

        private void enableScript(bool enabled)
        {
            this.time.Enabled = enabled;
            this.scriptData.Enabled = enabled;
            this.times.Enabled = enabled;
            this.interval.Enabled = enabled;
            this.executeScript.Enabled = enabled;
            this.responseRefreshButton.Enabled = enabled;
            this.textScript.Enabled = enabled;
            this.codeInSB.Enabled = enabled;
            this.countInSB.Enabled = enabled;
            this.executeInSB.Enabled = enabled;
            this.speedCode.Enabled = enabled;
            this.speedPrice.Enabled = enabled;
            this.speedExecute.Enabled = enabled;
            this.readLogs.Enabled = enabled;
            this.orderWait.Enabled = enabled;
        }

        private void startScript()
        {
            this.aa.clean();
            this.executeScript.Text = "停止";
            this.executeScript.Tag = "1";
            this.scriptTimer.Start();
        }

        private void stopScript()
        {
            this.executeScript.Text = "执行";
            this.executeScript.Tag = "0";
            this.scriptTimer.Stop();
        }

        private void executeScript_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                var tag = this.executeScript.Tag as string;
                if (tag == "0")
                {
                    // 执行
                    if (string.IsNullOrWhiteSpace(this.time.Text)
                    || this.scriptData.Rows.Count == 0)
                    {
                        this.statusContent.Text = Resources.STATUS_CONTENT_INPUT;
                        return;
                    }

                    script.time = this.time.Text;
                    script.jobs.Clear();
                    for (int i = 0, length = this.scriptData.Rows.Count - 1; i < length; i++)
                    {
                        var row = this.scriptData.Rows[i];
                        var code = row.Cells["code"].Value as string;
                        var price = row.Cells["price"].Value as string;
                        var count = row.Cells["count"].Value as string;
                        var side = row.Cells["side"].Value as string;
                        if (string.IsNullOrWhiteSpace(code)
                            || string.IsNullOrWhiteSpace(price)
                            || string.IsNullOrWhiteSpace(count)
                            || string.IsNullOrWhiteSpace(side)
                            || string.IsNullOrWhiteSpace(this.interval.Text)
                            || string.IsNullOrWhiteSpace(this.orderWait.Text))
                        {
                            this.statusContent.Text = Resources.STATUS_CONTENT_INPUT;
                            return;
                        }
                        var job = new Job();
                        job.code = code;
                        job.price = price;
                        job.count = count;
                        job.side = side;
                        this.script.jobs.Add(job);
                    }
                    script.times = Decimal.ToInt32(this.times.Value);
                    script.interval = this.interval.IntValue;
                    script.orderWait = this.orderWait.IntValue;
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTING;

                    // prepare to execute
                    this.executeScript.Enabled = false;
                    var intervalTime = this.aa.prepare(script, DateTime.Parse(this.time.Text));

                    if (intervalTime < 0)
                    {
                        this.statusContent.Text = Resources.STATUS_TIME_ERROR;
                    }
                    else if (intervalTime <= 10 * 1000)
                    {
                        this.startScript();
                    }
                    else
                    {
                        this.alarm.Interval = intervalTime - 10 * 1000;
                        this.alarm.Start();
                        this.executeScript.Text = "停止";
                        this.executeScript.Tag = "1";
                    }
                    this.executeScript.Enabled = true;
                }
                else if (tag == "1")
                {
                    // 停止
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_STOP;
                    this.alarm.Stop();
                    this.stopScript();
                }
            }
        }

        private void scriptTimer_Tick(object sender, EventArgs e)
        {
            if (aa != null)
            {
                try
                {
                    var result = aa.execute(script);
                    if (result)
                    {
                        // 执行脚本成功
                        this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_SUCCESS;
                        this.stopScript();
                    }
                    else
                    {
                        // 继续执行
                    }
                }
                catch (Exception)
                {
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_FAILED;
                    this.stopScript();
                    this.enableScript(false);
                    this.launchButton.Enabled = true;
                }
            }
        }

        private void sync_Tick(object sender, EventArgs e)
        {
            if (aa != null)
            {
                try
                {
                    aa.validate();
                }
                catch (Exception ex)
                {
                    // 异常终止
                    this.statusContent.Text = Resources.STATUS_CONTENT_BROWSER_CLOSE + "\r\n" + ex.Message;
                    this.enableScript(false);
                    this.launchButton.Enabled = true;
                    this.sync.Stop();
                }
            }
        }

        protected override void DestroyHandle()
        {
            if (aa != null)
            {
                this.stopScript();
                this.sync.Stop();
                aa.dispose();
            }

            base.DestroyHandle();
            Environment.Exit(0);
        }

        private void responseRefreshButton_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                this.responseTime.Text = aa.getResponseTime().GetValue(0) + "ms";
                this.deviationTime.Text = aa.getResponseTime().GetValue(1) + "ms";
            }
        }

        private void textScript_Click(object sender, EventArgs e)
        {
            if (this.scriptData.Rows.Count == 0)
            {
                this.statusContent.Text = Resources.STATUS_CONTENT_INPUT;
                return;
            }

            script.jobs.Clear();
            for (int i = 0, length = this.scriptData.Rows.Count - 1; i < length; i++)
            {
                var row = this.scriptData.Rows[i];
                var code = row.Cells["code"].Value as string;
                var price = row.Cells["price"].Value as string;
                var count = row.Cells["count"].Value as string;
                var side = row.Cells["side"].Value as string;
                if (string.IsNullOrWhiteSpace(code)
                    || string.IsNullOrWhiteSpace(price)
                    || string.IsNullOrWhiteSpace(count)
                    || string.IsNullOrWhiteSpace(side))
                {
                    this.statusContent.Text = Resources.STATUS_CONTENT_INPUT;
                    return;
                }
                var job = new Job();
                job.code = code;
                job.price = price;
                job.count = count;
                job.side = side;
                this.script.jobs.Add(job);
            }

            aa.testScript(this.script);
        }

        private void executeInSB_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                var code = this.codeInSB.Text;
                var count = this.countInSB.Text;
                script.jobs.Clear();
                var job = new Job();
                job.code = code;
                job.count = count;
                this.script.jobs.Add(job);
                var a = aa as CCECSHBrowserAutomation;
                if (a != null)
                {
                    a.sellAndBuy(script);
                }
            }
        }

        private void alarm_Tick(object sender, EventArgs e)
        {
            this.startScript();
            this.alarm.Stop();
        }

        private void speedExecute_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                var code = this.speedCode.Text;
                var price = this.speedPrice.Text;
                script.jobs.Clear();
                var job = new Job();
                job.code = code;
                job.price = price;
                this.script.jobs.Add(job);
                var a = aa as CCECSHBrowserAutomation;
                if (a != null)
                {
                    this.speedResponseTime.Text = a.getOrderResponse(script) + "ms";
                }
            }
        }

        private void readLogs_Click(object sender, EventArgs e)
        {
            if (aa != null)
            {
                var a = aa as CCECSHBrowserAutomation;
                if (a != null)
                {
                    a.readLogs();
                }
                MessageBox.Show(this, "读取成功\r\n" + Environment.CurrentDirectory + @"\log.txt", "信息");
            }
        }
    }
}