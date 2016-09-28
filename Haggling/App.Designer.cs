namespace Haggling
{
    partial class App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.launchState = new System.Windows.Forms.GroupBox();
            this.statusContent = new System.Windows.Forms.Label();
            this.statusTitle = new System.Windows.Forms.Label();
            this.launchButton = new System.Windows.Forms.Button();
            this.modeTitle = new System.Windows.Forms.Label();
            this.clientMode = new System.Windows.Forms.RadioButton();
            this.browserMode = new System.Windows.Forms.RadioButton();
            this.agentTitle = new System.Windows.Forms.Label();
            this.agent = new System.Windows.Forms.ComboBox();
            this.scriptState = new System.Windows.Forms.GroupBox();
            this.interval = new Haggling.Control.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.times = new System.Windows.Forms.NumericUpDown();
            this.executeScript = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.count = new Haggling.Control.NumericTextBox();
            this.price = new Haggling.Control.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.code = new Haggling.Control.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.DateTimePicker();
            this.scriptTimer = new System.Windows.Forms.Timer(this.components);
            this.sync = new System.Windows.Forms.Timer(this.components);
            this.launchState.SuspendLayout();
            this.scriptState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.times)).BeginInit();
            this.SuspendLayout();
            // 
            // launchState
            // 
            this.launchState.Controls.Add(this.statusContent);
            this.launchState.Controls.Add(this.statusTitle);
            this.launchState.Controls.Add(this.launchButton);
            this.launchState.Controls.Add(this.modeTitle);
            this.launchState.Controls.Add(this.clientMode);
            this.launchState.Controls.Add(this.browserMode);
            this.launchState.Controls.Add(this.agentTitle);
            this.launchState.Controls.Add(this.agent);
            this.launchState.Location = new System.Drawing.Point(12, 12);
            this.launchState.Name = "launchState";
            this.launchState.Size = new System.Drawing.Size(502, 171);
            this.launchState.TabIndex = 0;
            this.launchState.TabStop = false;
            this.launchState.Text = "启动状态";
            // 
            // statusContent
            // 
            this.statusContent.AutoEllipsis = true;
            this.statusContent.AutoSize = true;
            this.statusContent.Location = new System.Drawing.Point(62, 80);
            this.statusContent.Name = "statusContent";
            this.statusContent.Size = new System.Drawing.Size(0, 17);
            this.statusContent.TabIndex = 0;
            // 
            // statusTitle
            // 
            this.statusTitle.AutoSize = true;
            this.statusTitle.Location = new System.Drawing.Point(6, 80);
            this.statusTitle.Name = "statusTitle";
            this.statusTitle.Size = new System.Drawing.Size(50, 17);
            this.statusTitle.TabIndex = 0;
            this.statusTitle.Text = "状态：";
            // 
            // launchButton
            // 
            this.launchButton.Location = new System.Drawing.Point(421, 19);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(75, 30);
            this.launchButton.TabIndex = 4;
            this.launchButton.Text = global::Haggling.Properties.Resources.LAUNCH_BUTTON_TITLE;
            this.launchButton.UseVisualStyleBackColor = true;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // modeTitle
            // 
            this.modeTitle.AutoSize = true;
            this.modeTitle.Location = new System.Drawing.Point(203, 26);
            this.modeTitle.Name = "modeTitle";
            this.modeTitle.Size = new System.Drawing.Size(50, 17);
            this.modeTitle.TabIndex = 0;
            this.modeTitle.Text = "模式：";
            // 
            // clientMode
            // 
            this.clientMode.AutoSize = true;
            this.clientMode.Enabled = false;
            this.clientMode.Location = new System.Drawing.Point(259, 51);
            this.clientMode.Name = "clientMode";
            this.clientMode.Size = new System.Drawing.Size(71, 21);
            this.clientMode.TabIndex = 3;
            this.clientMode.TabStop = true;
            this.clientMode.Text = global::Haggling.Properties.Resources.CLIENT_TITLE;
            this.clientMode.UseVisualStyleBackColor = true;
            this.clientMode.CheckedChanged += new System.EventHandler(this.clientMode_CheckedChanged);
            // 
            // browserMode
            // 
            this.browserMode.AutoSize = true;
            this.browserMode.Location = new System.Drawing.Point(259, 24);
            this.browserMode.Name = "browserMode";
            this.browserMode.Size = new System.Drawing.Size(71, 21);
            this.browserMode.TabIndex = 2;
            this.browserMode.TabStop = true;
            this.browserMode.Text = global::Haggling.Properties.Resources.BROWSER_TITLE;
            this.browserMode.UseVisualStyleBackColor = true;
            this.browserMode.CheckedChanged += new System.EventHandler(this.browserMode_CheckedChanged);
            // 
            // agentTitle
            // 
            this.agentTitle.AutoSize = true;
            this.agentTitle.Location = new System.Drawing.Point(6, 26);
            this.agentTitle.Name = "agentTitle";
            this.agentTitle.Size = new System.Drawing.Size(64, 17);
            this.agentTitle.TabIndex = 0;
            this.agentTitle.Text = "代理商：";
            // 
            // agent
            // 
            this.agent.DisplayMember = "Text";
            this.agent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.agent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.agent.Location = new System.Drawing.Point(76, 23);
            this.agent.MaximumSize = new System.Drawing.Size(200, 0);
            this.agent.MinimumSize = new System.Drawing.Size(100, 0);
            this.agent.Name = "agent";
            this.agent.Size = new System.Drawing.Size(121, 24);
            this.agent.TabIndex = 1;
            this.agent.ValueMember = "Value";
            this.agent.SelectedIndexChanged += new System.EventHandler(this.agent_SelectedIndexChanged);
            // 
            // scriptState
            // 
            this.scriptState.Controls.Add(this.interval);
            this.scriptState.Controls.Add(this.label6);
            this.scriptState.Controls.Add(this.label5);
            this.scriptState.Controls.Add(this.times);
            this.scriptState.Controls.Add(this.executeScript);
            this.scriptState.Controls.Add(this.label4);
            this.scriptState.Controls.Add(this.count);
            this.scriptState.Controls.Add(this.price);
            this.scriptState.Controls.Add(this.label3);
            this.scriptState.Controls.Add(this.code);
            this.scriptState.Controls.Add(this.label2);
            this.scriptState.Controls.Add(this.label1);
            this.scriptState.Controls.Add(this.time);
            this.scriptState.Location = new System.Drawing.Point(12, 189);
            this.scriptState.Name = "scriptState";
            this.scriptState.Size = new System.Drawing.Size(502, 178);
            this.scriptState.TabIndex = 100;
            this.scriptState.TabStop = false;
            this.scriptState.Text = "脚本参数";
            // 
            // interval
            // 
            this.interval.AllowSpace = false;
            this.interval.Enabled = false;
            this.interval.Location = new System.Drawing.Point(336, 84);
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(68, 22);
            this.interval.TabIndex = 12;
            this.interval.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "间隔(毫秒)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "次数";
            // 
            // times
            // 
            this.times.Enabled = false;
            this.times.Location = new System.Drawing.Point(336, 57);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(68, 22);
            this.times.TabIndex = 9;
            this.times.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // executeScript
            // 
            this.executeScript.Enabled = false;
            this.executeScript.Location = new System.Drawing.Point(421, 142);
            this.executeScript.Name = "executeScript";
            this.executeScript.Size = new System.Drawing.Size(75, 30);
            this.executeScript.TabIndex = 8;
            this.executeScript.Tag = "0";
            this.executeScript.Text = "执行";
            this.executeScript.UseVisualStyleBackColor = true;
            this.executeScript.Click += new System.EventHandler(this.executeScript_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "买入数量：";
            // 
            // count
            // 
            this.count.AllowSpace = false;
            this.count.Enabled = false;
            this.count.Location = new System.Drawing.Point(93, 112);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(121, 22);
            this.count.TabIndex = 6;
            // 
            // price
            // 
            this.price.AllowSpace = false;
            this.price.Enabled = false;
            this.price.Location = new System.Drawing.Point(93, 84);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(121, 22);
            this.price.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "买入价格：";
            // 
            // code
            // 
            this.code.AllowSpace = false;
            this.code.Enabled = false;
            this.code.Location = new System.Drawing.Point(93, 56);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(121, 22);
            this.code.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "商品代码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "执行时间：";
            // 
            // time
            // 
            this.time.CustomFormat = "HH:mm:ss";
            this.time.Enabled = false;
            this.time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time.Location = new System.Drawing.Point(93, 28);
            this.time.Name = "time";
            this.time.ShowUpDown = true;
            this.time.Size = new System.Drawing.Size(121, 22);
            this.time.TabIndex = 0;
            this.time.Value = new System.DateTime(2016, 9, 19, 9, 29, 59, 0);
            // 
            // scriptTimer
            // 
            this.scriptTimer.Interval = 500;
            this.scriptTimer.Tick += new System.EventHandler(this.scriptTimer_Tick);
            // 
            // sync
            // 
            this.sync.Interval = 5000;
            this.sync.Tick += new System.EventHandler(this.sync_Tick);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 379);
            this.Controls.Add(this.scriptState);
            this.Controls.Add(this.launchState);
            this.MaximumSize = new System.Drawing.Size(544, 424);
            this.MinimumSize = new System.Drawing.Size(544, 424);
            this.Name = "App";
            this.Text = "Haggling";
            this.launchState.ResumeLayout(false);
            this.launchState.PerformLayout();
            this.scriptState.ResumeLayout(false);
            this.scriptState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.times)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox launchState;
        private System.Windows.Forms.RadioButton clientMode;
        private System.Windows.Forms.RadioButton browserMode;
        private System.Windows.Forms.Label agentTitle;
        private System.Windows.Forms.Label modeTitle;
        private System.Windows.Forms.Label statusContent;
        private System.Windows.Forms.Label statusTitle;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.ComboBox agent;
        private System.Windows.Forms.GroupBox scriptState;
        private System.Windows.Forms.DateTimePicker time;
        private Haggling.Control.NumericTextBox price;
        private System.Windows.Forms.Label label3;
        private Haggling.Control.NumericTextBox code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private Haggling.Control.NumericTextBox count;
        private System.Windows.Forms.Button executeScript;
        private System.Windows.Forms.Timer scriptTimer;
        private System.Windows.Forms.Timer sync;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown times;
        private Control.NumericTextBox interval;
        private System.Windows.Forms.Label label6;
    }
}

