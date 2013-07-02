using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DMCSystemsManagement;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (true)
            {
                StringBuilder MirrStatus = new StringBuilder();
                var listener = new HttpListener
                {
                    Prefixes =
				{
					"http://+:8082/DMCSystemsManagement/"
				}
                };
                listener.Start();
                SQLResilientState rsh = new SQLResilientState();
                while (true)
                {
                    var httpListenerContext = listener.GetContext();
                    HttpListenerResponse response = httpListenerContext.Response;
                    if (httpListenerContext.Request.Url.ToString().Contains("SQLMirrorState"))
                    {
                        MirrStatus = new StringBuilder();
                        MirrStatus.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" +
                                            "<DMCSystemsManagement>\r\n" +
                                            "  <SqlResilienceState>\r\n" );
                        rsh.Refresh();
                        MirrStatus.Append("   <Principals>");
                        foreach (KeyValuePair<String, String> p in rsh.principals)
                        {
                            MirrStatus.Append("        <" + p.Key + " server=\""+ p.Value+"\"/>\r\n");
                        }
                        MirrStatus.Append("   </Principals>");
                        MirrStatus.Append("   <Servers>\r\n");

                        foreach (Sqserver s in rsh.Servers.Values)
                        {
                            MirrStatus.Append("<" + s.name + " state=\""+ s.ServerState+"\"/>\r\n");
                            MirrStatus.Append("<Instances>\r\n");
                            foreach (Instance i in s.instances.Values)
                            {
                                MirrStatus.Append("<" + i.name + " role=\""+ i.role + "\" state=\"" + i.state+ "\"/>\r\n");
                            }
                            MirrStatus.Append("</Instances>\r\n");
                        }
                        MirrStatus.Append("   </Servers>\r\n"+
                                          "  </SqlResilienceState>\r\n" +
                                          "</DMCSystemsManagement>\r\n" );
                    }

                    Console.WriteLine(httpListenerContext.Request.Url);
                    Console.WriteLine(MirrStatus);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(MirrStatus.ToString());
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                    httpListenerContext.Response.Close();
                }

            }
            else
            {
                SQLResilientState rsa = new SQLResilientState();
            }
        }
    }
}
