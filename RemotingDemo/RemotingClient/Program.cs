using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Ipc;

using RemotingClass;


namespace RemotingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //HttpChannel channel = new HttpChannel();
            //TcpChannel channel = new TcpChannel();
            IpcChannel channel = new IpcChannel();
            ChannelServices.RegisterChannel(channel, false);

            // Registers the remote class. (This could be done with a
            // configuration file instead of a direct call.)
            RemotingConfiguration.RegisterWellKnownClientType(
                Type.GetType("RemotingClass.ClassTobeRemoting, RemotingClass"),
                "ipc://localhost:8080/object1uri");

            // Instead of creating a new object, this obtains a reference
            // to the server's single instance of the ServiceClass object.
            ClassTobeRemoting object1 = new ClassTobeRemoting();

            try
            {
                Console.WriteLine("ServerTime: " + object1.GetServerTime());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception of type: " + ex.ToString() + " occurred.");
                Console.WriteLine("Details: " + ex.Message);
            }
        }
    }
}
