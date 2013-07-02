using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;

namespace AirManagement
{
    static class Program
    {
        public static Dictionary<String, Engine> engines;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AEControl());
            Application.Run(new AirManagement());
        }
        public static void Init()
        {
            engines = new Dictionary<String, Engine>();
            String file = "C:\\deluxe\\DMCSystemsManagement\\AirManConfig.xml";
            System.Xml.Linq.XDocument _xdoc;
            XmlDocument xdoc = new XmlDocument();
            try
            {
                _xdoc = System.Xml.Linq.XDocument.Load(file);
                xdoc.Load(file);
            }
            catch (Exception e)
            {
                //System.Console.WriteLine("cant find XML file" + file);
                throw (e);
            }

            var _rp = xdoc.SelectSingleNode("/Meta/Semaphore/Location");
            String path = (String)xdoc.SelectSingleNode("/AIRS/Meta/Semaphore/Location").InnerText;
            var _eles = from _e in _xdoc.Descendants("AIR") where _e.Name == "AIR" select _e;

            foreach (var item in _eles)
            {
                engines.Add((String)item.Attribute("Name"),new Engine((String)item.Attribute("Name"),UInt16.Parse((String)item.Attribute("Id"))));
                engines[(String)item.Attribute("Name")].RemotePath = "\\\\" + (String)item.Attribute("Hostname") + path;
                var _chann = from _e in item.Descendants() where _e.Name == "CHANN" select _e;
                foreach (var chann in _chann)
                {
                    engines[ (String) item.Attribute("Name")].AddChannel((String) chann.Attribute("Name"),Convert.ToUInt16( chann.Attribute("Id").Value));
                }
            }
        }
    }
}
