using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AirManagement
{
    public partial class AEControl : Form
    {
        private Button ExitButton;
        private Button Redraw;
        private Timer healthtest;
        private Dictionary<String, Engine> Engines;
        public AEControl()
        {
            this.InitializeComponent();
        }
        private void AEControl_Load(object sender, EventArgs e)
        {


        }
        private void chb_click(object sender, EventArgs e)
        {
            DispatchAction((ChannelButton)sender);
        }
        
        private void DispatchAction(ChannelButton sender)
        {
            Channel c = sender.channel;
            if (File.Exists(c.GetStopFile()))
            {
                c.start();
            }
            else
            {
                c.stop();
            }
            // todo reload stuff
        }

        private void InitializeComponent()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ExitButton = new System.Windows.Forms.Button();
            this.Redraw = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(818, 238);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // Redraw
            // 
            this.Redraw.Location = new System.Drawing.Point(734, 238);
            this.Redraw.Name = "Redraw";
            this.Redraw.Size = new System.Drawing.Size(75, 23);
            this.Redraw.TabIndex = 1;
            this.Redraw.Text = "Redraw";
            this.Redraw.UseVisualStyleBackColor = true;
            this.Redraw.Click += new System.EventHandler(this.Redraw_Click);
            // 
            // AEControl
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(905, 273);
            this.Controls.Add(this.Redraw);
            this.Controls.Add(this.ExitButton);
            this.Name = "AEControl";
            this.ResumeLayout(false);
            this.SuspendLayout();
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Engines = Program.engines;
            this.components = new System.ComponentModel.Container();
            this.ClientSize = new System.Drawing.Size(777, 246);
            this.Name = "AirManagement";
            this.Text = "Air Management Console v 1.0";
            this.Load += new System.EventHandler(this.AEControl_Load);
            foreach (Engine en in Engines.Values)
            {
                EngineControl ec = new EngineControl(this, en);
            }
            this.PerformAutoScale();
            this.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Redraw_Click(object sender, EventArgs e)
        {
            this.PerformLayout();
            foreach (Control ct in this.Controls)
            {
                ct.PerformLayout();
            }

            this.PerformAutoScale();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            
        }
    }
    class InfoPanel : FlowLayoutPanel
    {
        public Label ErrName;
        public ProgressBar IoMeter;
        public ProgressBar CpuMeter;
        public Process PlayProc;
        InfoPanel() 
        {
            ErrName = new Label();
            IoMeter = new ProgressBar();
            CpuMeter = new ProgressBar();
            PlayProc = new Process();

        }
        public InfoPanel(Channel ch) : this()
        {
            ErrName.Location = new System.Drawing.Point(3, 0);
            ErrName.Size = new System.Drawing.Size(20, 50);
            this.Controls.Add(ErrName);
            this.Controls.Add(IoMeter);
            this.Controls.Add(CpuMeter);
        }
    }
    partial class AEControl
    {
        private System.ComponentModel.IContainer components = null;

        // Create a layoutControl that contains buttons (Channels from an Engine)
        class ChannelButton : Button
        {
            public Channel channel;
            public ChannelButton()
            {
                channel = new Channel();
            }
            public ChannelButton(Channel ch,AEControl me)
            {
                channel = ch;
                this.Text = "CH" + ch.number.ToString() + ":" + ch.name;
                this.TextAlign = ContentAlignment.MiddleLeft;
                this.Click += new EventHandler(me.chb_click);
                this.BackColor = System.Drawing.SystemColors.ButtonFace;
                me.Controls.Add(this);
                this.SuspendLayout();
                this.Location = new System.Drawing.Point(10, 100 + (ch.number * 60));
                this.Name = ch.name;
                this.Size = new System.Drawing.Size(100, 20);
                this.TabIndex = ch.number;
//                this.PerformLayout();
            
            }
        }
        // Make the LayoutControl for an engine
        class EngineControl : FlowLayoutPanel
        {
            private Dictionary<ushort, ChannelButton> ChannelButtons;
            public EngineControl()
            {
                ChannelButtons = new Dictionary<ushort, ChannelButton>();
            }

            public EngineControl(AEControl me,Engine e)
                : this()
            {
                Label label = new System.Windows.Forms.Label();
                me.Controls.Add(label);
                label.Text = e.name;
                label.AutoSize = true;
                label.Location = new System.Drawing.Point(39 + ((e.id - 1) * 120), 18);
                label.Name = e.name;
                label.Size = new System.Drawing.Size(31, 13);
                label.TabIndex = e.id;
                label.Text = e.name;
                foreach (Channel c in e.Channels.Values)
                {
                    ChannelButtons.Add(c.number, new ChannelButton(c,(AEControl) me));
                    this.Controls.Add(ChannelButtons[c.number]);
                }
                me.Controls.Add(this);
                this.SuspendLayout();
                this.Location = new System.Drawing.Point(39+((e.id-1)*120), 28);
                this.Name = e.name;
                this.Size = new System.Drawing.Size(115, 100);
                this.TabIndex = e.id;
                this.BackColor = System.Drawing.SystemColors.Desktop;
                this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

                this.PerformLayout();
            }
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


        }

    }
}


