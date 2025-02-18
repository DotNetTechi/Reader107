using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace IDT_Reader
{
    public static class PublicVariables
    {
        public static ConcurrentQueue<byte[]> _dataQueue = new ConcurrentQueue<byte[]>(); // Thread-safe queue for received data
        public static ConcurrentQueue<byte[]> _splitByteQueue = new ConcurrentQueue<byte[]>(); // Thread-safe queue for received data
        public static bool Inventory, OnClick;
        public static bool isReceivingPacket, isReceivedPacket;

        public static bool isConnected = false;
        public static bool isLogined = false;
        public static bool isReadSpecifictag = false;
 
        
        public static byte[] sequenceToFind = { 0x11, 0x00, 0xEE, 0x00 };
        public static byte[] packetBuffer, packet_Buffer;
        //public static string[] MemoryCheck;
        
        public static Dictionary<string, bool> _MemoryCheck = new Dictionary<string, bool>
        {
                { "Length", false },
                { "Antenna", false },
                { "RSSI", false },
                { "reserveMemory", false },
                { "epcMemory", false },
                { "tidMemory", false },
                { "userMemory", false }
        };
        // { "Length", "Antenna", "RSSI", "reserveMemory", "epcMemory", "tidMemory", "userMemory" };
        public static string COM_Selecting;
        public static int PTotal = 0, count = 0, CTotal = 0;
        //
        public static Stopwatch stopwatch = new Stopwatch();

        // Variables for ComboBox items
        public static readonly string[] baudRates = { "9600", "19200", "38400", "57600", "115200" };
        public static readonly string[] powerOptions = { "03", "04", "05", "06", "07", "08" };  // Adjust as needed for real power options
        public static readonly string[] scanTimes = { "5", "6", "7", "8", "9", "10" };  // Example times
        public static readonly string[] scanIntervals = { "1", "2", "3", "4", "5" };  // Example intervals
        public static readonly string[] tagStorageOptions = { "1", "2", "3", "4", "5" };  // Example tag storage options 
        public static List<string> AddRange(int Start, int End)
        {
            List<string> value = new List<string>();
            for (int i = Start; i < End; i++)
            {
                value.Add(i.ToString());
            }
            return value;
        }

       



        #region Antenna Power

        public static int Antenna1Power=0;
        public static int Antenna2Power=0;
        public static int Antenna3Power=0;
        public static int Antenna4Power=0;
        public static int Antenna5Power=0;
        public static int Antenna6Power=0;
        public static int Antenna7Power=0;
        public static int Antenna8Power=0;
        public static int Antenna9Power=0;
        public static int Antenna10Power=0;
        public static int Antenna11Power=0;
        public static int Antenna12Power=0;
        public static int Antenna13Power=0;
        public static int Antenna14Power=0;
        public static int Antenna15Power=0;
        public static int Antenna16Power=0;




        public static TrackBar Antenna1 = new TrackBar();
        public static TrackBar Antenna2 = new TrackBar();
        public static TrackBar Antenna3 = new TrackBar();
        public static TrackBar Antenna4 = new TrackBar();
        public static TrackBar Antenna5 = new TrackBar();
        public static TrackBar Antenna6 = new TrackBar();
        public static TrackBar Antenna7 = new TrackBar();
        public static TrackBar Antenna8 = new TrackBar();
        public static TrackBar Antenna9 = new TrackBar();
        public static TrackBar Antenna10 = new TrackBar();
        public static TrackBar Antenna11 = new TrackBar();
        public static TrackBar Antenna12 = new TrackBar();
        public static TrackBar Antenna13 = new TrackBar();
        public static TrackBar Antenna14 = new TrackBar();
        public static TrackBar Antenna15 = new TrackBar();
        public static TrackBar Antenna16 = new TrackBar();


        public static TextBox Antenna1Value = new TextBox();
        public static TextBox Antenna2Value = new TextBox();
        public static TextBox Antenna3Value = new TextBox();
        public static TextBox Antenna4Value = new TextBox();
        public static TextBox Antenna5Value = new TextBox();
        public static TextBox Antenna6Value = new TextBox();
        public static TextBox Antenna7Value = new TextBox();
        public static TextBox Antenna8Value = new TextBox();
        public static TextBox Antenna9Value = new TextBox();
        public static TextBox Antenna10Value = new TextBox();
        public static TextBox Antenna11Value = new TextBox();
        public static TextBox Antenna12Value = new TextBox();
        public static TextBox Antenna13Value = new TextBox();
        public static TextBox Antenna14Value = new TextBox();
        public static TextBox Antenna15Value = new TextBox();
        public static TextBox Antenna16Value = new TextBox();

        #endregion
    }
}
