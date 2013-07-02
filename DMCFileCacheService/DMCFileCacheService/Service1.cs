using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace DMCFileCacheService
{
    public partial class DMCFileCacheService : ServiceBase
    {
        public static Int32 CHUNK_SIZE = 1024000;
        private static String[] _namelist;
        public static DMCFileCacheService Me;
        static String _src;
        static String _log;
        static EventLog _ev;
        readonly Timer _cacheManager;
        readonly Thread _httpListen;

        private readonly HttpListener listener = new HttpListener
            {
                Prefixes =
                    {
                        "http://+:8082/DMCSystemsManagement/"
                    }
            };

        public DMCFileCacheService()
        {
            InitializeComponent();
            _src = "DMCFileCache";
            _log = "DMCFileCacheLog";
            Me = this;
            if (!EventLog.SourceExists("DMCFileCache"))
            {
                EventLog.CreateEventSource(_src, _log);
            }
            _ev = new EventLog {Source = _src};
            _cacheManager = new Timer(10000);
            _listWatcher = new Timer(10000);
            _httpListen = new Thread(WebListen);
            // timer setup
            // timer = new ProcessThread();
            // poll the SQLResileintState 
            // if it changes significantly ( the primary for any instance changes )
            // invoke the DNS Change script
        }

        protected override void OnStart(string[] args)
        {
            // Debugger.Launch();
            _listWatcher.Interval = 20000;
            _cacheManager.Interval = 10000;
            _listWatcher.Enabled = true;
            _cacheManager.Enabled = true;
            _cacheManager.Elapsed += CacheRefresh;
            _listWatcher.Elapsed += ReadNameList;
            _ev.WriteEntry("Starting Service Version 1.0.0.0 28/03/2012", EventLogEntryType.Information, 1);
            ReadNameList();
            _listWatcher.Start();
            _cacheManager.Start();
            listener.Start();
            _httpListen.Start();
        }

        protected override void OnStop()
        {
            _ev.WriteEntry("Stopping Service", EventLogEntryType.Information, 2);
        }

        protected void WebListen()
        {
            while (true)
            {
                var httpListenerContext = listener.GetContext();
                var response = httpListenerContext.Response;
                if (httpListenerContext.Request.Url.ToString().Contains("FileCacheRequest"))
                {
                    String filename = httpListenerContext.Request.Url.ToString().Split('&').Last();
                    // Debugger.Launch();
                     byte[]  buffer = Encoding.UTF8.GetBytes(CacheRefresh(filename, _ev) ? "true" : "false");
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
                httpListenerContext.Response.Close();
            }
        }

        public void ReadNameList()
        {
            const string path = @"C:\DMCCache\CacheList.txt";
            if (File.Exists(path))
            {
                try
                {
                    _namelist = File.ReadAllLines(path);
                }
                catch (Exception e)
                {
                    _ev.WriteEntry("Error loading File Cache list", EventLogEntryType.Error, 3);
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                _ev.WriteEntry("File Cache list " + path + " cannot be found Stopping Service", EventLogEntryType.Error,
                              4);
                Stop();
            }
        }

        private void CacheRefresh(object source, ElapsedEventArgs e)
        {
            if (_namelist == null)
                return;
            foreach (string t in _namelist)
            {
                CacheRefresh(t,_ev);
            }
        }

        private void ReadNameList(object source, ElapsedEventArgs e)
        {
            ReadNameList();
        }

        private bool CacheRefresh(String file, EventLog _ev)
        {
            try
            {
                _ev.WriteEntry("Start Refresh Cache on " + file, EventLogEntryType.Information, 6);
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (var br = new BinaryReader(fs, new ASCIIEncoding()))
                    {
                        byte[] chunk = br.ReadBytes(CHUNK_SIZE);
                        while (chunk.Length > 0)
                        {
                            chunk = br.ReadBytes(CHUNK_SIZE);
                        }
                    }
                }
                _ev.WriteEntry("Finish Refresh Cache on " + file, EventLogEntryType.Information, 6);
            }
            catch (Exception ex)
            {
                _ev.WriteEntry("Skipping file (unable to open) " + file, EventLogEntryType.Warning, 5);
                return false;
            }
            return true;
        }
    }
}