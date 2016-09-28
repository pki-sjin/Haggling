using Haggling.Factory;
using Haggling.Model;
using Haggling.Properties;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Haggling
{
    public partial class App : Form
    {
        private Factor factor = new Factor();

        private AbstractAutomation aa = null;

        private Script script = new Script();

        public App()
        {
            InitializeComponent();
            var agents = new[] { new { Text = "上文众申", Value = AgentType.CCECSH } };
            this.agent.DataSource = agents;
            this.agent.SelectedIndex = -1;
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
            this.code.Enabled = enabled;
            this.price.Enabled = enabled;
            this.count.Enabled = enabled;
            this.times.Enabled = enabled;
            this.interval.Enabled = enabled;
            this.executeScript.Enabled = enabled;
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
                    || string.IsNullOrWhiteSpace(this.code.Text)
                    || string.IsNullOrWhiteSpace(this.price.Text)
                    || string.IsNullOrWhiteSpace(this.count.Text))
                    {
                        this.statusContent.Text = Resources.STATUS_CONTENT_INPUT;
                        return;
                    }

                    script.time = this.time.Text;
                    script.code = this.code.Text;
                    script.price = this.price.Text;
                    script.count = this.count.Text;
                    script.times = Decimal.ToInt32(this.times.Value);
                    script.interval = this.interval.IntValue;
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTING;
                    this.startScript();
                }
                else if (tag == "1")
                {
                    // 停止
                    this.statusContent.Text = Resources.STATUS_CONTENT_EXECUTE_STOP;
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
    }
}