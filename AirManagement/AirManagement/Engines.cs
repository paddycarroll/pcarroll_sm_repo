using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;


namespace AirManagement
{
    public class Channel
    {
        public String name;
        public UInt16 number;
        private Engine dad;
        public Channel() { }
        public Channel(String name, UInt16 number,Engine dad)
        {
            this.name = name;
            this.number = number;
            this.dad = dad;
        }
        public String GetStopFile()
        {
            return dad.getRemotePath() + "\\stop-" + number.ToString();
        }
        public String GetStartFile()
        {
            return dad.getRemotePath() + "\\start-" + number.ToString();
        }
        public void stop()
        {
            File.Delete(GetStartFile());
            File.Create(GetStopFile()).Close();
        }
        public void start()
        {
            File.Create(GetStartFile()).Close();
        }
    }
    public class Engine
    {
        public String name;
        public String hostname;
        public UInt16 id;
        public String RemotePath;
        public Dictionary <UInt16,Channel> Channels;
        public Engine(String name, UInt16 id)
        {
            this.name = name;
            this.id = id;
            Channels = new Dictionary<UInt16,Channel>();
        }
        public void AddChannel(String name, UInt16 number)
        {
            Channels.Add(number, new Channel(name, number, this));
        }
        public Engine()
        {
            Channels = new Dictionary<UInt16, Channel>();
        }
        public String getRemotePath()
        {
            return this.RemotePath;
        }
        public bool HealthTest()
        {
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(name, 100);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch (Exception e)
            {
                //System.Console.WriteLine(e.Message);
                return false;
            }
            return result;

        }
    }
}
