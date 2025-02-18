using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace IDT_Reader
{
    static class TCPIP_Communication
    {
        private static TcpClient Client;
        private static NetworkStream ClientStream;
        private static Thread DataReceived;


        private static void LoadTCPIP(string ClinetAddress, int Port)
        {
            //Client = new TcpClient();
            //Client.c
            //TCPIP_Communication.ClientConnection(txt_LocalIP.Text, Convert.ToInt32(txt_Port.Text));
        }

        public static bool PingServer(string ipAddress)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipAddress, 1000); // 1000ms timeout
                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"Ping successful: {reply.RoundtripTime}ms");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Ping failed: {reply.Status}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping error: {ex.Message}");
                return false;
            }
        }





        public static TcpClient ClientConnection(string ClinetAddress, int Port)
        {
            try
            {
                Client = new TcpClient();
                Client.Connect(IPAddress.Parse(ClinetAddress), Port);
                if (Client.Connected)
                {
                    PublicVariables.isConnected = true;
                    ClientStream = Client.GetStream();
                    TheadClientReceivemethod();
                }

                return Client;
            }
            catch (Exception exp)
            {
                PublicTextlog.ShowLog($"ClintError: {exp.Message}");
            }

            return Client = null;
        }
        public static void ClientConnectionClose()
        {
            PublicVariables.isConnected = false;
            ClientStream.Close();
            Client.Close();
            Client.Dispose();
        }


        public static void ClientWriteData(byte[] dataToSend, string Logindata)
        {
            try
            {
                if (Client.Connected && PublicVariables.isLogined)
                {
                    ClientStream.Write(dataToSend, 0, dataToSend.Length);
                    Console.WriteLine($"DataWrite : {string.Concat(BitConverter.ToString(dataToSend).Replace("-", " "))}");
                }
                else
                {
                    string Hex = string.Concat(BitConverter.ToString(dataToSend).Replace("-", ""));
                    if (Hex.Equals("61646D696E403132333435"))
                    {
                        ClientStream.Write(dataToSend, 0, dataToSend.Length);
                        Console.WriteLine($"DataWrite : {string.Concat(BitConverter.ToString(dataToSend).Replace("-", " "))}");
                    }
                }
            }
            catch (Exception exp)
            {
                PublicTextlog.ShowLog($"ClineStreams: {exp.Message}");
            }


        }

        private static void TheadClientReceivemethod()
        {
            DataReceived = new Thread(new ThreadStart(Receivemethod))
            {
                Priority = ThreadPriority.Lowest,
                IsBackground = true
            };
            DataReceived.Start();
        }

        private static void Receivemethod()
        {
            try
            {
                while (PublicVariables.isConnected)
                {
                    int bytesToRead = Client.Available;
                    if (bytesToRead > 0)
                    {
                        // Read incoming data
                        byte[] buffer = new byte[bytesToRead];
                        ClientStream.Read(buffer, 0, bytesToRead);
                        // Enqueue the data for later processing
                        PublicVariables._dataQueue.Enqueue(buffer);
                    }
                }
            }
            catch (Exception exp)
            {
                PublicTextlog.ShowLog("ClientReceive: " + exp.Message);
            }

        }



        #region Server TCP/IP

        static TcpListener ServerListener;
        static List<TcpClient> TcpClients;
        static Timer addClientInList = new Timer();
        static Thread serverReceived;
        static bool isListenerEnbled = false;

        public static void ServerStartup(string ServerAddress, int port)  // this function is used to get data from server using Port  
        {
            try
            {
                ServerListener = new TcpListener(IPAddress.Parse(ServerAddress), port);
                ServerListener.Start();
                
                TcpClients = new List<TcpClient>();

                addClientInList.Elapsed += new ElapsedEventHandler(ServerReceivemethod);
                addClientInList.Interval = (300);
                
                serverReceived = new Thread(new ThreadStart(ListenerThread))
                {
                    Priority = ThreadPriority.Lowest,
                    IsBackground = true
                };
            }
            catch (Exception ex)
            {
                return;
            }
        }


        public static void ServerStart(int port)
        {
            ServerListener = new TcpListener(IPAddress.Any, port);
            ServerListener.Start();
            isListenerEnbled = true;
            serverReceived.Start();
            addClientInList.Enabled = true;
        }

        public static void ServerStop()
        {
            ServerListener.Stop();
            isListenerEnbled = false;
            addClientInList.Enabled = false;
        }


        //
        public static void ListenerThread()
        {
            while (isListenerEnbled)
            {
                try
                {
                    while (ServerListener.Pending())
                    {
                        TcpClient tcpClient = ServerListener.AcceptTcpClient();
                        for (int index = 0; index < TcpClients.Count; ++index)
                        {
                            if (TcpClients[index].Client.RemoteEndPoint.ToString().Split(':')[0] == tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0])
                            {
                                TcpClients.RemoveAt(index);
                                --index;
                            }
                        }
                        TcpClients.Add(tcpClient);
                        Thread.Sleep(1);
                    }
                }
                catch (SocketException ex)
                {
                        break;
                }
                catch (InvalidOperationException ex)
                {
                        break;
                }
                catch (Exception ex)
                {
                }
                Thread.Sleep(5);
            }
        }


        public static void ServerReceivemethod(object sender, ElapsedEventArgs e)
        {
            for (int index1 = 0; index1 < TcpClients.Count; ++index1)
            {
                try
                {
                    TcpClient tcpClient = TcpClients[index1];   // recieve the client Data
                    if (tcpClient.Available > 0)
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        int bytesToRead = tcpClient.Available;
                        if (bytesToRead > 0)
                        {
                            byte[] buffer = new byte[bytesToRead];
                            int len = stream.Read(buffer, 0, bytesToRead);
                            PublicVariables._splitByteQueue.Enqueue(buffer);
                        }
                        //IpAdd = ((IPEndPoint)TcpClients[index1].Client.RemoteEndPoint).Address.ToString();
                    }
                }
                catch (Exception ex)
                {
                    PublicTextlog.ShowLog($"ServerReceivemethod(Recieve_Data)Exception {ex.Message}");
                }
            }

        }


        #endregion
    }
}
