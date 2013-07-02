using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

namespace DMCSystemsManagement
{
    public enum State :byte {
        good = 0,
        warning = 1,
        critical = 2,
        fatal = 3
    }
    public enum MirroringRole : byte 
    {
        PRINCIPAL=1,
        MIRROR=2,
        WITNESS=0,
        INDETERMINATE =3
    }
    public enum MirroringState : byte 
    {
        DISCONNECTED=1,
        SUSPENDED=0,
        SYNCHRONIZING=2,
        PENDING_FAILOVER=3,
        SYNCHRONIZED=4,
        UNSYNCHRONIZED=5,
        SYNC_SYNCHRONIZED=6,
        NULL=99
    //0 = Suspended
    //1 = Disconnected from the other partner
    //2 = Synchronizing
    //3 = Pending Failover
    //4 = Synchronized 
    //5 = The partners are not synchronized. Failover is not possible now.
    //6 = The partners are synchronized. Failover is potentially possible. For information about the requirements for failover //see, Synchronous Database Mirroring (High-Safety Mode).
    //NULL = Database is inaccessible or is not mirrored.
    }
    // this class defines a group of SQL instances within which we have databases
    public class SQLResilientState
    {
        public State state;
        public Dictionary<String , Sqserver> Servers {get;set;}

        public Dictionary<String,String> principals;
   
        // the server class representes individual servers, the primary, mirror and witness

        // default test ctor
        public SQLResilientState()
        {
            principals = new Dictionary<String,String>();
            // read list of servers
            Servers = new Dictionary<String, Sqserver>();
            Refresh();
        }
        public void Refresh()
        {
            String file = "C:\\deluxe\\DMCSystemsManagement\\SqLAlertRoles.xml";
            System.Xml.Linq.XDocument _xdoc;
            try
            {
                _xdoc = System.Xml.Linq.XDocument.Load(file);
            }
            catch (Exception e)
            {
                //System.Console.WriteLine("cant find XML file" + file);
                throw (e);
            }
            // get the servers
            var _eles = from _e in _xdoc.Descendants("Server") where _e.Name == "Server" select _e;

            foreach (var item in _eles)
            {
                String server = (String)item.Attribute("id");
                String role = (String)item.Attribute("role");
                if (!Servers.ContainsKey(server))
                {
                    Servers.Add(server, new Sqserver(server, role));
                }
                else
                {
                    Servers[server].Refresh();
                }
            }
            foreach (Sqserver s in Servers.Values)
            {
                foreach (Instance i in s.instances.Values)
                {
                    if (i.role == MirroringRole.PRINCIPAL)
                    {
                        if(principals.ContainsKey(i.name))
                        {
                            principals.Remove(i.name);
                        }
                        principals.Add(i.name, s.name);
                    }

                }
            }

        }

        // here's where we determine the state of the SQL servers as a mirrored setup
        public State ResilientState()
        {
            foreach (KeyValuePair<String, Sqserver> srvr in Servers)
            {
                int stategood = 0;
                int statewarning = 0;
                int statecritical = 0;
                int statefatal = 0;

                // 3 good states is good 
                // 2 is warning
                // 1 is critical
                // 0 is fatal
                switch (srvr.Value.ServerState) 
                {
                        //count the server states
                    case State.good:
                        stategood++;
                        break;
                    case  State.warning:
                        statewarning++;
                        break;
                    case State.critical:
                        statecritical++;
                        break;
                    case State.fatal:
                        statefatal++;
                        break;
                }
                switch (stategood)
                {
                        // decide on the serverstate
                    case 3:
                        state = State.good;
                        break;
                    case 2:
                        state = State.warning;
                        break;
                    case 1:
                        state = State.critical;
                        break;
                    case 0:
                        state = State.fatal;
                        break;
                }
                // count primaries and mirrors
                int prim = 0;
                int mirr = 0;
                foreach (KeyValuePair<String, Instance> inst in srvr.Value.instances)
                {
                    switch (inst.Value.state)
                    {
                        case MirroringState.SYNC_SYNCHRONIZED:
                        case MirroringState.SYNCHRONIZED:
                        case MirroringState.SYNCHRONIZING:
                            this.state = State.good;
                            // x is dummy if it's a MIRROR mirr++ else prim++
                            int x = (inst.Value.role == MirroringRole.MIRROR) ?mirr++:prim++;
                            break;
                    }
                }
                // do we have all our primaries
                if (prim == srvr.Value.instances.Count)
                {
                    // do we have all our mirrors
                    if (mirr == srvr.Value.instances.Count)
                    {
                        // all good, we return the base state
                        return this.state;
                    }
                    else
                        // we dont have all the mirrors but we have all the primaries
                    {
                        return State.critical;
                    }
                }
            } 
            return State.fatal;
        }


    }
    class RavenResilientState
    {

    }
    public class Instance{
        public String name {get; set;}
        public MirroringState state {get; set;}
        public MirroringRole role {get; set;}
        public Instance(String name)
        {
            this.name = name;
        }

    }
            public class Sqserver
        {
            MirroringRole DefaultRole;
            public State ServerState { get; set; }
            public String name;
            public Dictionary<String, Instance> instances { get; set; }
            public void Refresh()
            {
                try{
                    // can ping the server
                    if (this.IsConnected(200))
                    {
                        ServerState = State.good;
                        //System.Console.WriteLine("ping ok");
                    }
                    else
                    {
                        ServerState = State.fatal;
                        return;
                    }

                    // can make an sql connection with the master database
                    // Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
                    String connectionString = "Server=" + this.name + ";Database=master;Trusted_Connection=True;";
                    String dmcdbs = "select a.db,b.mirroring_state,b.mirroring_role from dbo.backup_paths a inner join sys.databases c on a.db = c.name inner join sys.database_mirroring b on b.database_id = c.database_id";
                    String witness = "select database_name from sys.database_mirroring_witnesses";
                    SqlConnection connection = new SqlConnection(connectionString);
                        {
                            // test for witness
                            SqlCommand command = new SqlCommand(witness , connection);
                            SqlDataReader rs;
                            try
                            {
                                command.Connection.Open();
                           
                                rs = command.ExecuteReader();
                            }
                            catch (Exception e)
                            {
                                throw e;
                            } if (rs.HasRows)
                            {
                                // mark the server as a witness with a good state
                                DefaultRole = MirroringRole.WITNESS;
                                ServerState = State.good;
                            }
                            else
                            {
                                ServerState = State.fatal;
                                connection.Close();
                                command = new SqlCommand(dmcdbs, connection);
                                command.Connection.Open();
                                rs = command.ExecuteReader();
                                // determine the instance roles (primary, mirror , down) and update the role variable
                                // for each database in master.dbo.backup_paths
                                instances = new Dictionary<string, Instance>();
                                while (rs.Read())
                                {
                                    String db = rs.GetValue(0).ToString().Trim();
                                    if (!instances.ContainsKey(db))
                                    {
                                        instances.Add(db, new Instance(db));
                                    }
                                    try
                                    {
                                        // can we read the role and the state from the recordset ( null values throw an exception )
                                        instances[db].state = (MirroringState)rs.GetByte(1);
                                        instances[db].role = (MirroringRole)rs.GetByte(2);
                                    }
                                    catch (Exception e)
                                    {
                                        // exception says we don't have a mirroring context
                                        //System.Console.WriteLine(e.Message);
                                        instances[db].role = MirroringRole.INDETERMINATE;
                                        instances[db].state = MirroringState.DISCONNECTED;
                                        // mark the server as bad ( warning )
                                        this.ServerState = State.warning;
                                    }
                                    switch (instances[db].state)
                                    {
                                            // if disconnected or null
                                        case MirroringState.DISCONNECTED:
                                            this.ServerState = State.warning;
                                            break;
                                        case MirroringState.NULL:
                                            this.ServerState = State.warning;
                                            break;
                                        default:
                                            this.ServerState =  this.ServerState == State.warning?State.warning:State.good;
                                            break;
                                    }

                                }
                            }

                        }
                    //
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    throw (e);
                }

            }
            public Sqserver(String name ,String DefRole)
            {
                this.name = name;
                ServerState = State.fatal;
                instances = new Dictionary<string, Instance>();
                DefaultRole = (String.Equals(DefRole.ToUpper(), "MIRROR")) 
                    ? MirroringRole.MIRROR : (String.Equals(DefRole.ToUpper(), "PRINCIPAL")) 
                    ? MirroringRole.PRINCIPAL : (String.Equals(DefRole.ToUpper(), "WITNESS")) 
                    ? MirroringRole.WITNESS : MirroringRole.INDETERMINATE;
                Refresh();              
            }
            private bool IsConnected(Int32 timeout)
            {
                bool result = false;
                Ping p = new Ping();
                try
                {
                    PingReply reply = p.Send(name, timeout);
                    if (reply.Status == IPStatus.Success)
                        return true;
                }
                catch(Exception e) {
                    //System.Console.WriteLine(e.Message);
                    return false; 
                }
                return result;
            }
        };
}
