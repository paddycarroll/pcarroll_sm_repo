using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AirManagement
{
    public partial class AirManagement : Form
    {
        Dictionary<String, Engine> Engines;


        public AirManagement()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AirManagement_Load(object sender, EventArgs e)
        {
         Engines = Program.engines;
         Reload();
        }

        private void A1CH0_Click(object sender, EventArgs e)
        {
            DispatchAction((Button)sender);
        }
        private void DispatchAction(Button sender)
        {
            foreach(Engine e in Engines.Values)
            {
                if (e.id == Convert.ToUInt16(sender.Name.Substring(1, 1)))
                {
                    Channel c = e.Channels[Convert.ToUInt16(sender.Name.Substring(4, 1))];
                    if (File.Exists(c.GetStopFile()))
                    {
                        c.start();
                    }
                    else
                    {
                        c.stop();
                    }
                    Reload();
                }
            }
        }
        private void Reload()
        {
            foreach (Engine en in Engines.Values)
            {
                Control a = Controls[en.name];
                foreach (Channel ch in en.Channels.Values)
                {
                    String btName = "A" + en.id.ToString() + "CH" + ch.number.ToString();
                    String ttName = "TA" + en.id.ToString() + "CH" + ch.number.ToString();
                    a.Controls[btName].Enabled = true;
                    if (File.Exists(ch.GetStopFile()))
                    {
                        a.Controls[btName].BackColor = Color.Red;
                        if (File.Exists(ch.GetStartFile()))
                        {
                            a.Controls[btName].BackColor = Color.Violet;
                            a.Controls[btName].Enabled = false;
                        }
                    }
                    else if (File.Exists(ch.GetStartFile()))
                    {
                        a.Controls[btName].BackColor = Color.Green;
                    }
                    else
                    {
                        a.Controls[btName].Enabled = false;
                    }
//                    a.Controls[ttName].Text = ch.name;
                }
            }
        }

        private void ButtonRefresh_Tick(object sender, EventArgs e)
        {
            Reload();
        }

        private void A1CH1_Click(object sender, EventArgs e)
        {
            DispatchAction((Button)sender);
        }

        private void A2CH0_Click(object sender, EventArgs e)
        {
            DispatchAction((Button)sender);
        }

        private void A2CH1_Click(object sender, EventArgs e)
        {
            DispatchAction((Button)sender);
        }

        private void A3CH0_Click(object sender, EventArgs e)
        {
            DispatchAction((Button)sender);
        }

        private void A3CH1_Click(object sender, EventArgs e)
        {
            DispatchAction((Button)sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AEControl fo = new AEControl();
            
        }
    }
}
