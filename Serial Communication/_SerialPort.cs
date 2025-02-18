using System;
using System.IO.Ports;

namespace IDT_Reader
{
    internal static class _SerialPort
    {
        private static SerialPort serialPort = new SerialPort();

        public static void LoadCOMPort()
        {
            try
            {
                serialPort = new SerialPort
                {
                    PortName = AUTOCOMPort(),
                    BaudRate = 57000,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    ReadTimeout = 1000
                };
                serialPort.DataReceived += DataReceivedHandler;
            }
            catch { }

        }

        public static string AUTOCOMPort()
        {
            // Get last available COM port
            string[] ports = SerialPort.GetPortNames();
            return ports.Length > 0 ? ports[ports.Length - 1] : string.Empty;
        }


        public static SerialPort OpenSerialPort(string COM, int baurate)
        {
            try
            {
                if (!COM.Equals("AUTO"))
                {
                    serialPort.PortName = COM;
                    serialPort.BaudRate = baurate;
                }
                serialPort.Open();
            }
            catch (Exception exp)
            {
                PublicTextlog.ShowLog("SerialConnection: " + exp.Message);
            }

            if (serialPort.IsOpen)
            {
                return serialPort;
            }
            return serialPort;
        }
        public static SerialPort CloseSerialPort()
        {
            try
            {
                serialPort.DataReceived -= DataReceivedHandler;
                serialPort.Close();
            }
            catch (Exception exp)
            {
                PublicTextlog.ShowLog("Serialclose: " + exp.Message);
            }


            return serialPort;
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = serialPort.BytesToRead;
                if (bytesToRead > 0)
                {
                    // Read incoming data
                    byte[] buffer = new byte[bytesToRead];
                    serialPort.Read(buffer, 0, bytesToRead);

                    // Enqueue the data for later processing
                    PublicVariables._dataQueue.Enqueue(buffer);
                }
            }
            catch (Exception exp)
            {
                PublicTextlog.ShowLog("Receive: " + exp.Message);
            }
        }



        public static void DataWriteHandler(byte[] dataToSend, string Logindata)
        {
            try
            {
                if (serialPort.IsOpen && PublicVariables.isLogined)
                {
                    Console.WriteLine($"DataWrite : {string.Concat(BitConverter.ToString(dataToSend).Replace("-", " "))}");
                    serialPort.Write(dataToSend, 0, dataToSend.Length);
                }
                else
                {
                    string Hex = string.Concat(BitConverter.ToString(dataToSend).Replace("-", ""));
                    if (Hex.Equals("61646D696E403132333435"))
                    {
                        serialPort.Write(dataToSend, 0, dataToSend.Length);
                        Console.WriteLine($"DataWrite : {string.Concat(BitConverter.ToString(dataToSend).Replace("-", " "))}");
                    }
                }
            }
            catch (Exception ex)
            {
                PublicTextlog.ShowLog(ex.Message);

                if (!serialPort.IsOpen)
                {
                    PublicTextlog.ShowLog("Attempting to reconnect to serial port...");
                    //OpenCloseSerialPort(1);
                }
            }
        }


    }
}
