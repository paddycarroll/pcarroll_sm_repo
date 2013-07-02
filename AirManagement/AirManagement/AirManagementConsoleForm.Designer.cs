namespace AirManagement
{
    partial class AirManagement
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
            this.AIR1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.A1CH0 = new System.Windows.Forms.Button();
            this.A1CH1 = new System.Windows.Forms.Button();
            this.AIR2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.A2CH0 = new System.Windows.Forms.Button();
            this.A2CH1 = new System.Windows.Forms.Button();
            this.AIR3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.A3CH0 = new System.Windows.Forms.Button();
            this.A3CH1 = new System.Windows.Forms.Button();
            this.process1 = new System.Diagnostics.Process();
            this.ButtonRefresh = new System.Windows.Forms.Timer(this.components);
            this.TA1CH0 = new System.Windows.Forms.ToolTip(this.components);
            this.TA1CH1 = new System.Windows.Forms.ToolTip(this.components);
            this.TA2CH0 = new System.Windows.Forms.ToolTip(this.components);
            this.TA2CH1 = new System.Windows.Forms.ToolTip(this.components);
            this.TA3CH0 = new System.Windows.Forms.ToolTip(this.components);
            this.TA3CH1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.AIR1.SuspendLayout();
            this.AIR2.SuspendLayout();
            this.AIR3.SuspendLayout();
            this.SuspendLayout();
            // 
            // AIR1
            // 
            this.AIR1.Controls.Add(this.label1);
            this.AIR1.Controls.Add(this.A1CH0);
            this.AIR1.Controls.Add(this.A1CH1);
            this.AIR1.Controls.Add(this.progressBar1);
            this.AIR1.Location = new System.Drawing.Point(39, 28);
            this.AIR1.Name = "AIR1";
            this.AIR1.Size = new System.Drawing.Size(55, 178);
            this.AIR1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "AIR1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // A1CH0
            // 
            this.A1CH0.Enabled = false;
            this.A1CH0.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.A1CH0.Location = new System.Drawing.Point(3, 16);
            this.A1CH0.Name = "A1CH0";
            this.A1CH0.Size = new System.Drawing.Size(49, 23);
            this.A1CH0.TabIndex = 0;
            this.A1CH0.Text = "0";
            this.A1CH0.UseVisualStyleBackColor = true;
            this.A1CH0.Click += new System.EventHandler(this.A1CH0_Click);
            // 
            // A1CH1
            // 
            this.A1CH1.Enabled = false;
            this.A1CH1.Location = new System.Drawing.Point(3, 45);
            this.A1CH1.Name = "A1CH1";
            this.A1CH1.Size = new System.Drawing.Size(49, 23);
            this.A1CH1.TabIndex = 1;
            this.A1CH1.Text = "1";
            this.A1CH1.UseVisualStyleBackColor = true;
            this.A1CH1.Click += new System.EventHandler(this.A1CH1_Click);
            // 
            // AIR2
            // 
            this.AIR2.Controls.Add(this.label2);
            this.AIR2.Controls.Add(this.A2CH0);
            this.AIR2.Controls.Add(this.A2CH1);
            this.AIR2.Location = new System.Drawing.Point(109, 28);
            this.AIR2.Name = "AIR2";
            this.AIR2.Size = new System.Drawing.Size(55, 178);
            this.AIR2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "AIR2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // A2CH0
            // 
            this.A2CH0.Enabled = false;
            this.A2CH0.Location = new System.Drawing.Point(3, 16);
            this.A2CH0.Name = "A2CH0";
            this.A2CH0.Size = new System.Drawing.Size(49, 23);
            this.A2CH0.TabIndex = 0;
            this.A2CH0.Text = "0";
            this.A2CH0.UseVisualStyleBackColor = true;
            this.A2CH0.Click += new System.EventHandler(this.A2CH0_Click);
            // 
            // A2CH1
            // 
            this.A2CH1.Enabled = false;
            this.A2CH1.Location = new System.Drawing.Point(3, 45);
            this.A2CH1.Name = "A2CH1";
            this.A2CH1.Size = new System.Drawing.Size(49, 23);
            this.A2CH1.TabIndex = 1;
            this.A2CH1.Text = "1";
            this.A2CH1.UseVisualStyleBackColor = true;
            this.A2CH1.Click += new System.EventHandler(this.A2CH1_Click);
            // 
            // AIR3
            // 
            this.AIR3.BackColor = System.Drawing.SystemColors.Desktop;
            this.AIR3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AIR3.Controls.Add(this.label3);
            this.AIR3.Controls.Add(this.A3CH0);
            this.AIR3.Controls.Add(this.A3CH1);
            this.AIR3.Location = new System.Drawing.Point(183, 28);
            this.AIR3.Name = "AIR3";
            this.AIR3.Size = new System.Drawing.Size(55, 178);
            this.AIR3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "AIR3";
            // 
            // A3CH0
            // 
            this.A3CH0.Enabled = false;
            this.A3CH0.Location = new System.Drawing.Point(3, 16);
            this.A3CH0.Name = "A3CH0";
            this.A3CH0.Size = new System.Drawing.Size(49, 23);
            this.A3CH0.TabIndex = 0;
            this.A3CH0.Text = "0";
            this.A3CH0.UseVisualStyleBackColor = true;
            this.A3CH0.Click += new System.EventHandler(this.A3CH0_Click);
            // 
            // A3CH1
            // 
            this.A3CH1.Enabled = false;
            this.A3CH1.Location = new System.Drawing.Point(3, 45);
            this.A3CH1.Name = "A3CH1";
            this.A3CH1.Size = new System.Drawing.Size(49, 23);
            this.A3CH1.TabIndex = 1;
            this.A3CH1.Text = "1";
            this.A3CH1.UseVisualStyleBackColor = true;
            this.A3CH1.Click += new System.EventHandler(this.A3CH1_Click);
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Enabled = true;
            this.ButtonRefresh.Interval = 10000;
            this.ButtonRefresh.Tick += new System.EventHandler(this.ButtonRefresh_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 182);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 74);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(49, 13);
            this.progressBar1.TabIndex = 3;
            // 
            // AirManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 246);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AIR1);
            this.Controls.Add(this.AIR2);
            this.Controls.Add(this.AIR3);
            this.Name = "AirManagement";
            this.Text = "Air Management Console v 0.1";
            this.Load += new System.EventHandler(this.AirManagement_Load);
            this.AIR1.ResumeLayout(false);
            this.AIR1.PerformLayout();
            this.AIR2.ResumeLayout(false);
            this.AIR2.PerformLayout();
            this.AIR3.ResumeLayout(false);
            this.AIR3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel AIR1;
        private System.Windows.Forms.Button A1CH0;
        private System.Windows.Forms.Button A1CH1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel AIR2;
        private System.Windows.Forms.Button A2CH0;
        private System.Windows.Forms.Button A2CH1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel AIR3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button A3CH0;
        private System.Windows.Forms.Button A3CH1;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Timer ButtonRefresh;
        private System.Windows.Forms.ToolTip TA1CH0;
        private System.Windows.Forms.ToolTip TA1CH1;
        private System.Windows.Forms.ToolTip TA2CH0;
        private System.Windows.Forms.ToolTip TA2CH1;
        private System.Windows.Forms.ToolTip TA3CH0;
        private System.Windows.Forms.ToolTip TA3CH1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

