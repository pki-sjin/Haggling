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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.textScript = new System.Windows.Forms.Button();
            this.scriptData = new System.Windows.Forms.DataGridView();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.side = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.responseRefreshButton = new System.Windows.Forms.Button();
            this.responseTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.interval = new Haggling.Control.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.times = new System.Windows.Forms.NumericUpDown();
            this.executeScript = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.DateTimePicker();
            this.scriptTimer = new System.Windows.Forms.Timer(this.components);
            this.sync = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.executeInSB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.countInSB = new Haggling.Control.NumericTextBox();
            this.codeInSB = new Haggling.Control.NumericTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.speedResponseTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.speedExecute = new System.Windows.Forms.Button();
            this.价格 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.speedPrice = new Haggling.Control.NumericTextBox();
            this.speedCode = new Haggling.Control.NumericTextBox();
            this.alarm = new System.Windows.Forms.Timer(this.components);
            this.launchState.SuspendLayout();
            this.scriptState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scriptData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.times)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.launchState.Size = new System.Drawing.Size(758, 171);
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
            this.launchButton.Text = "启动";
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
            this.clientMode.Text = "客户端";
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
            this.browserMode.Text = "浏览器";
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
            this.scriptState.Controls.Add(this.textScript);
            this.scriptState.Controls.Add(this.scriptData);
            this.scriptState.Controls.Add(this.responseRefreshButton);
            this.scriptState.Controls.Add(this.responseTime);
            this.scriptState.Controls.Add(this.label7);
            this.scriptState.Controls.Add(this.interval);
            this.scriptState.Controls.Add(this.label6);
            this.scriptState.Controls.Add(this.label5);
            this.scriptState.Controls.Add(this.times);
            this.scriptState.Controls.Add(this.executeScript);
            this.scriptState.Controls.Add(this.label1);
            this.scriptState.Controls.Add(this.time);
            this.scriptState.Location = new System.Drawing.Point(6, 6);
            this.scriptState.Name = "scriptState";
            this.scriptState.Size = new System.Drawing.Size(738, 358);
            this.scriptState.TabIndex = 100;
            this.scriptState.TabStop = false;
            this.scriptState.Text = "脚本参数";
            // 
            // textScript
            // 
            this.textScript.Enabled = false;
            this.textScript.Location = new System.Drawing.Point(657, 260);
            this.textScript.Name = "textScript";
            this.textScript.Size = new System.Drawing.Size(75, 30);
            this.textScript.TabIndex = 17;
            this.textScript.Text = "测试";
            this.textScript.UseVisualStyleBackColor = true;
            this.textScript.Visible = false;
            this.textScript.Click += new System.EventHandler(this.textScript_Click);
            // 
            // scriptData
            // 
            this.scriptData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scriptData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.code,
            this.price,
            this.count,
            this.side});
            this.scriptData.Enabled = false;
            this.scriptData.Location = new System.Drawing.Point(9, 55);
            this.scriptData.Name = "scriptData";
            this.scriptData.RowTemplate.Height = 24;
            this.scriptData.Size = new System.Drawing.Size(723, 199);
            this.scriptData.TabIndex = 16;
            // 
            // code
            // 
            dataGridViewCellStyle4.NullValue = null;
            this.code.DefaultCellStyle = dataGridViewCellStyle4;
            this.code.HeaderText = "商品代码";
            this.code.MaxInputLength = 7;
            this.code.Name = "code";
            // 
            // price
            // 
            dataGridViewCellStyle5.NullValue = null;
            this.price.DefaultCellStyle = dataGridViewCellStyle5;
            this.price.HeaderText = "价格";
            this.price.Name = "price";
            // 
            // count
            // 
            dataGridViewCellStyle6.NullValue = null;
            this.count.DefaultCellStyle = dataGridViewCellStyle6;
            this.count.HeaderText = "数量";
            this.count.Name = "count";
            // 
            // side
            // 
            this.side.HeaderText = "交易";
            this.side.Name = "side";
            // 
            // responseRefreshButton
            // 
            this.responseRefreshButton.Enabled = false;
            this.responseRefreshButton.Location = new System.Drawing.Point(150, 260);
            this.responseRefreshButton.Name = "responseRefreshButton";
            this.responseRefreshButton.Size = new System.Drawing.Size(75, 30);
            this.responseRefreshButton.TabIndex = 15;
            this.responseRefreshButton.Text = "刷新";
            this.responseRefreshButton.UseVisualStyleBackColor = true;
            this.responseRefreshButton.Click += new System.EventHandler(this.responseRefreshButton_Click);
            // 
            // responseTime
            // 
            this.responseTime.AutoSize = true;
            this.responseTime.Location = new System.Drawing.Point(90, 267);
            this.responseTime.Name = "responseTime";
            this.responseTime.Size = new System.Drawing.Size(34, 17);
            this.responseTime.TabIndex = 14;
            this.responseTime.Text = "0ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "响应时间：";
            // 
            // interval
            // 
            this.interval.AllowSpace = false;
            this.interval.Enabled = false;
            this.interval.Location = new System.Drawing.Point(151, 323);
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(74, 22);
            this.interval.TabIndex = 12;
            this.interval.Text = "200";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 326);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "临界值(毫秒)：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "重试次数：";
            // 
            // times
            // 
            this.times.Enabled = false;
            this.times.Location = new System.Drawing.Point(151, 296);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(74, 22);
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
            this.executeScript.Location = new System.Drawing.Point(657, 319);
            this.executeScript.Name = "executeScript";
            this.executeScript.Size = new System.Drawing.Size(75, 30);
            this.executeScript.TabIndex = 8;
            this.executeScript.Tag = "0";
            this.executeScript.Text = "执行";
            this.executeScript.UseVisualStyleBackColor = true;
            this.executeScript.Click += new System.EventHandler(this.executeScript_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
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
            this.time.Location = new System.Drawing.Point(93, 27);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 189);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 400);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.scriptState);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(750, 371);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "抢单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.executeInSB);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.countInSB);
            this.tabPage2.Controls.Add(this.codeInSB);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(750, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "对倒";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // executeInSB
            // 
            this.executeInSB.Enabled = false;
            this.executeInSB.Location = new System.Drawing.Point(9, 113);
            this.executeInSB.Name = "executeInSB";
            this.executeInSB.Size = new System.Drawing.Size(75, 30);
            this.executeInSB.TabIndex = 16;
            this.executeInSB.Tag = "0";
            this.executeInSB.Text = "执行";
            this.executeInSB.UseVisualStyleBackColor = true;
            this.executeInSB.Click += new System.EventHandler(this.executeInSB_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "数量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "商品代码";
            // 
            // countInSB
            // 
            this.countInSB.AllowSpace = false;
            this.countInSB.Enabled = false;
            this.countInSB.Location = new System.Drawing.Point(76, 62);
            this.countInSB.Name = "countInSB";
            this.countInSB.Size = new System.Drawing.Size(117, 22);
            this.countInSB.TabIndex = 15;
            // 
            // codeInSB
            // 
            this.codeInSB.AllowSpace = false;
            this.codeInSB.Enabled = false;
            this.codeInSB.Location = new System.Drawing.Point(76, 22);
            this.codeInSB.Name = "codeInSB";
            this.codeInSB.Size = new System.Drawing.Size(117, 22);
            this.codeInSB.TabIndex = 13;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.speedResponseTime);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.speedExecute);
            this.tabPage3.Controls.Add(this.价格);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.speedPrice);
            this.tabPage3.Controls.Add(this.speedCode);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(750, 371);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "下单测速";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // speedResponseTime
            // 
            this.speedResponseTime.AutoSize = true;
            this.speedResponseTime.Location = new System.Drawing.Point(90, 161);
            this.speedResponseTime.Name = "speedResponseTime";
            this.speedResponseTime.Size = new System.Drawing.Size(34, 17);
            this.speedResponseTime.TabIndex = 23;
            this.speedResponseTime.Text = "0ms";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "响应时间：";
            // 
            // speedExecute
            // 
            this.speedExecute.Enabled = false;
            this.speedExecute.Location = new System.Drawing.Point(9, 107);
            this.speedExecute.Name = "speedExecute";
            this.speedExecute.Size = new System.Drawing.Size(75, 30);
            this.speedExecute.TabIndex = 21;
            this.speedExecute.Tag = "0";
            this.speedExecute.Text = "执行";
            this.speedExecute.UseVisualStyleBackColor = true;
            this.speedExecute.Click += new System.EventHandler(this.speedExecute_Click);
            // 
            // 价格
            // 
            this.价格.AutoSize = true;
            this.价格.Location = new System.Drawing.Point(6, 65);
            this.价格.Name = "价格";
            this.价格.Size = new System.Drawing.Size(36, 17);
            this.价格.TabIndex = 19;
            this.价格.Text = "数量";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "商品代码";
            // 
            // speedPrice
            // 
            this.speedPrice.AllowSpace = false;
            this.speedPrice.Enabled = false;
            this.speedPrice.Location = new System.Drawing.Point(76, 62);
            this.speedPrice.Name = "speedPrice";
            this.speedPrice.Size = new System.Drawing.Size(117, 22);
            this.speedPrice.TabIndex = 20;
            // 
            // speedCode
            // 
            this.speedCode.AllowSpace = false;
            this.speedCode.Enabled = false;
            this.speedCode.Location = new System.Drawing.Point(76, 22);
            this.speedCode.Name = "speedCode";
            this.speedCode.Size = new System.Drawing.Size(117, 22);
            this.speedCode.TabIndex = 18;
            // 
            // alarm
            // 
            this.alarm.Tick += new System.EventHandler(this.alarm_Tick);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 605);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.launchState);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "App";
            this.Text = "Haggling";
            this.launchState.ResumeLayout(false);
            this.launchState.PerformLayout();
            this.scriptState.ResumeLayout(false);
            this.scriptState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scriptData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.times)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button executeScript;
        private System.Windows.Forms.Timer scriptTimer;
        private System.Windows.Forms.Timer sync;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown times;
        private Control.NumericTextBox interval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button responseRefreshButton;
        private System.Windows.Forms.Label responseTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView scriptData;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewComboBoxColumn side;
        private System.Windows.Forms.Button textScript;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button executeInSB;
        private Control.NumericTextBox countInSB;
        private System.Windows.Forms.Label label3;
        private Control.NumericTextBox codeInSB;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Timer alarm;
        private System.Windows.Forms.Button speedExecute;
        private System.Windows.Forms.Label 价格;
        private System.Windows.Forms.Label label8;
        private Control.NumericTextBox speedPrice;
        private Control.NumericTextBox speedCode;
        private System.Windows.Forms.Label speedResponseTime;
        private System.Windows.Forms.Label label9;
    }
}

