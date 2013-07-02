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
            this.A1 = new System.Windows.Forms.FlowLayoutPanel();
            this.A1CH0 = new System.Windows.Forms.Button();
            this.A1CH1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.A2CH0 = new System.Windows.Forms.Button();
            this.A2CH1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.A1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // A1
            // 
            this.A1.Controls.Add(this.label1);
            this.A1.Controls.Add(this.A1CH0);
            this.A1.Controls.Add(this.A1CH1);
            this.A1.Location = new System.Drawing.Point(39, 28);
            this.A1.Name = "A1";
            this.A1.Size = new System.Drawing.Size(55, 178);
            this.A1.TabIndex = 0;
            // 
            // A1CH0
            // 
            this.A1CH0.Location = new System.Drawing.Point(3, 16);
            this.A1CH0.Name = "A1CH0";
            this.A1CH0.Size = new System.Drawing.Size(49, 23);
            this.A1CH0.TabIndex = 0;
            this.A1CH0.Text = "0";
            this.A1CH0.UseVisualStyleBackColor = true;
            // 
            // A1CH1
            // 
            this.A1CH1.Location = new System.Drawing.Point(3, 45);
            this.A1CH1.Name = "A1CH1";
            this.A1CH1.Size = new System.Drawing.Size(49, 23);
            this.A1CH1.TabIndex = 1;
            this.A1CH1.Text = "1";
            this.A1CH1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.A2CH0);
            this.flowLayoutPanel1.Controls.Add(this.A2CH1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(109, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(55, 178);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // A2CH0
            // 
            this.A2CH0.Location = new System.Drawing.Point(3, 16);
            this.A2CH0.Name = "A2CH0";
            this.A2CH0.Size = new System.Drawing.Size(49, 23);
            this.A2CH0.TabIndex = 0;
            this.A2CH0.Text = "0";
            this.A2CH0.UseVisualStyleBackColor = true;
            // 
            // A2CH1
            // 
            this.A2CH1.Location = new System.Drawing.Point(3, 45);
            this.A2CH1.Name = "A2CH1";
            this.A2CH1.Size = new System.Drawing.Size(49, 23);
            this.A2CH1.TabIndex = 1;
            this.A2CH1.Text = "1";
            this.A2CH1.UseVisualStyleBackColor = true;
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
            // AirManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 246);
            this.Controls.Add(this.A1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "AirManagement";
            this.Text = "AirManagement";
            this.A1.ResumeLayout(false);
            this.A1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel A1;
        private System.Windows.Forms.Button A1CH0;
        private System.Windows.Forms.Button A1CH1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button A2CH0;
        private System.Windows.Forms.Button A2CH1;
        private System.Windows.Forms.Label label2;
    }
}

