using System.ComponentModel;

namespace IDT_Reader
{
    [DefaultPropertyAttribute("Name")]
    public class ComponentModel
    {
        private string _rfpower;
        private string _scantime;
        private string _readertype;
        private string _uhfmoduleid;
        private string _readerprofile;
        private string _drmconfiguration;
        private string _readertemperature;
        private string _epctidlength;
        private string _tagwritepower;
        private string _modulebaudrate;
        private string _readerbaudrate;

        private string _antennaconfiguration;
        private string _antennacheck;
        private string _antennareturnloss;
        private string _1stantennapower;
        private string _2ndantennapower;
        private string _3rdantennapower;
        private string _4thantennapower;
        private string _5thantennapower;
        private string _6thantennapower;
        private string _7thantennapower;
        private string _8thantennapower;
        private string _9thantennapower;
        private string _10thantennapower;
        private string _11thantennapower;
        private string _12thantennapower;
        private string _13thantennapower;
        private string _14thantennapower;
        private string _15thantennapower;
        private string _16thantennapower;
        private string _1stantennareturnloss;
        private string _2ndantennareturnloss;
        private string _3rdantennareturnloss;
        private string _4thantennareturnloss;

        private string _mac;
        private string _readerlocalip;
        private string _gatwway;
        private string _subnet;
        private string _dns;
        private string _localport;
        private string _buzzerdelay;
        private string _staticip;
        private string _serverport;
        private string _ethernetmodes;
        private string _readeruniqueid;

        private string _gpi0;
        private string _gpoA;
        private string _gpoB;

        private string _readerworkmode;
        private string _invreadpausetime;
        private string _tagprotocol;
        private string _tagfiltertime;
        private string _tagreadstatus;
        private string _qvalue;
        private string _session;
        private string _heartbeattime;
        private string _maxscanretrytime;
        private string _workingrffreqband;

        private string _tagcustompassword;


        // Name property with category attribute and   
        // description attribute added   
        // Properties
        [Category("Reader Settings"), Description("RF Power")]
        public string RFPower { get => _rfpower; set => _rfpower = value; }

        [Category("Reader Settings"), Description("Scan Time")]
        public string ScanTime { get => _scantime; set => _scantime = value; }

        [Category("Reader Settings"), Description("Reader Type")]
        public string ReaderType { get => _readertype; set => _readertype = value; }

        [Category("Reader Settings"), Description("UHF Module ID")]
        public string UHFModuleID { get => _uhfmoduleid; set => _uhfmoduleid = value; }

        [Category("Reader Settings"), Description("Reader Profile")]
        public string ReaderProfile { get => _readerprofile; set => _readerprofile = value; }

        [Category("Reader Settings"), Description("DRM Configuration")]
        public string DRMConfiguration { get => _drmconfiguration; set => _drmconfiguration = value; }

        [Category("Reader Settings"), Description("Reader Temperature")]
        public string ReaderTemperature { get => _readertemperature; set => _readertemperature = value; }

        [Category("Reader Settings"), Description("EPC TID Length")]
        public string EPCTIDLength { get => _epctidlength; set => _epctidlength = value; }

        [Category("Reader Settings"), Description("Tag Write Power")]
        public string TagWritePower { get => _tagwritepower; set => _tagwritepower = value; }

        [Category("Reader Settings"), Description("Module Baud Rate")]
        public string ModuleBaudRate { get => _modulebaudrate; set => _modulebaudrate = value; }

        [Category("Reader Settings"), Description("Reader Baud Rate")]
        public string ReaderBaudRate { get => _readerbaudrate; set => _readerbaudrate = value; }

        [Category("Reader Settings"), Description("MAC Address")]
        public string MAC { get => _mac; set => _mac = value; }

        [Category("Reader Connection Settings"), Description("Reader Local IP")]
        public string ReaderLocalIP { get => _readerlocalip; set => _readerlocalip = value; }

        [Category("Reader Connection Settings"), Description("Gateway")]
        public string Gateway { get => _gatwway; set => _gatwway = value; }

        [Category("Reader Connection Settings"), Description("Subnet")]
        public string Subnet { get => _subnet; set => _subnet = value; }

        [Category("Reader Connection Settings"), Description("DNS")]
        public string DNS { get => _dns; set => _dns = value; }

        [Category("Reader Connection Settings"), Description("Local Port")]
        public string LocalPort { get => _localport; set => _localport = value; }

        [Category("Reader Settings"), Description("Buzzer Delay")]
        public string BuzzerDelay { get => _buzzerdelay; set => _buzzerdelay = value; }

        [Category("Reader Connection Settings"), Description(" IP")]
        public string ServerIP { get => _staticip; set => _staticip = value; }

        [Category("Reader Connection Settings"), Description("Server Port")]
        public string ServerPort { get => _serverport; set => _serverport = value; }

        [Category("Reader Connection Settings"), Description("Ethernet Modes")]
        public string EthernetModes { get => _ethernetmodes; set => _ethernetmodes = value; }

        [Category("Reader Settings"), Description("Reader Unique ID")]
        public string ReaderUniqueID { get => _readeruniqueid; set => _readeruniqueid = value; }

        [Category("GPI&&GPO Settings"), Description("GPI 0")]
        public string GPI0 { get => _gpi0; set => _gpi0 = value; }

        [Category("GPI&&GPO Settings"), Description("GPO A")]
        public string GPOA { get => _gpoA; set => _gpoA = value; }

        [Category("GPI&&GPO Settings"), Description("GPO B")]
        public string GPOB { get => _gpoB; set => _gpoB = value; }

        [Category("Reader Settings"), Description("Reader Work Mode")]
        public string ReaderWorkMode { get => _readerworkmode; set => _readerworkmode = value; }

        [Category("Reader Settings"), Description("Inventory Read Pause Time")]
        public string InventoryReadPauseTime { get => _invreadpausetime; set => _invreadpausetime = value; }

        [Category("Reader Settings"), Description("Tag Protocol")]
        public string TagProtocol { get => _tagprotocol; set => _tagprotocol = value; }

        [Category("Reader Settings"), Description("Tag Filter Time")]
        public string TagFilterTime { get => _tagfiltertime; set => _tagfiltertime = value; }

        [Category("Reader Settings"), Description("Tag Read Status")]
        public string TagReadStatus { get => _tagreadstatus; set => _tagreadstatus = value; }

        [Category("Reader Settings"), Description("Q Value")]
        public string QValue { get => _qvalue; set => _qvalue = value; }

        [Category("Reader Settings"), Description("Session")]
        public string Session { get => _session; set => _session = value; }

        [Category("Reader Settings"), Description("Heartbeat Time")]
        public string HeartbeatTime { get => _heartbeattime; set => _heartbeattime = value; }

        [Category("Reader Settings"), Description("Max Scan Retry Time")]
        public string MaxScanRetryTime { get => _maxscanretrytime; set => _maxscanretrytime = value; }

        [Category("Reader Settings"), Description("Working RF Frequency Band")]
        public string WorkingRFFrequencyBand { get => _workingrffreqband; set => _workingrffreqband = value; }

        [Category("Reader Settings"), Description("Tag Custom Password")]
        public string TagCustomPassword { get => _tagcustompassword; set => _tagcustompassword = value; }

        //Antenna
        [Category("Antenna Settings"), Description("Antenna Configuration")]
        public string AntennaConfiguration { get => _antennaconfiguration; set => _antennaconfiguration = value; }

        [Category("Antenna Settings"), Description("Antenna Check")]
        public string AntennaCheck { get => _antennacheck; set => _antennacheck = value; }

        [Category("Antenna Loss Settings"), Description("Antenna Return Loss")]
        public string AntennaReturnLoss { get => _antennareturnloss; set => _antennareturnloss = value; }

        [Category("Antenna Loss Settings"), Description("Antenna Return Loss")]
        public string Firstantennareturnloss { get => _1stantennareturnloss; set => _1stantennareturnloss = value; }

        [Category("Antenna Loss Settings"), Description("Antenna Return Loss")]
        public string SecondReturnLoss { get => _2ndantennareturnloss; set => _2ndantennareturnloss = value; }

        [Category("Antenna Loss Settings"), Description("Antenna Return Loss")]
        public string ThirdAntennaReturnLoss { get => _3rdantennareturnloss; set => _3rdantennareturnloss = value; }

        [Category("Antenna Loss Settings"), Description("Antenna Return Loss")]
        public string FourthAntennaReturnLoss { get => _4thantennareturnloss; set => _4thantennareturnloss = value; }

        [Category("Antenna Power Settings"), Description("First Antenna Power")]
        public string FirstAntennaPower { get => _1stantennapower; set => _1stantennapower = value; }

        [Category("Antenna Power Settings"), Description("Second Antenna Power")]
        public string SecondAntennaPower { get => _2ndantennapower; set => _2ndantennapower = value; }

        [Category("Antenna Power Settings"), Description("Third Antenna Power")]
        public string ThirdAntennaPower { get => _3rdantennapower; set => _3rdantennapower = value; }

        [Category("Antenna Power Settings"), Description("Fourth Antenna Power")]
        public string FourthAntennaPower { get => _4thantennapower; set => _4thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Fifth Antenna Power")]
        public string FifthAntennaPower { get => _5thantennapower; set => _5thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Sixth Antenna Power")]
        public string SixthAntennaPower { get => _6thantennapower; set => _6thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Seventh Antenna Power")]
        public string SeventhAntennaPower { get => _7thantennapower; set => _7thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Eighth Antenna Power")]
        public string EighthAntennaPower { get => _8thantennapower; set => _8thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Ninth Antenna Power")]
        public string NinthAntennaPower { get => _9thantennapower; set => _9thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Tenth Antenna Power")]
        public string TenthAntennaPower { get => _10thantennapower; set => _10thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Eleventh Antenna Power")]
        public string EleventhAntennaPower { get => _11thantennapower; set => _11thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Twelfth Antenna Power")]
        public string TwelfthAntennaPower { get => _12thantennapower; set => _12thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Thirteenth Antenna Power")]
        public string ThirteenthAntennaPower { get => _13thantennapower; set => _13thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Fourteenth Antenna Power")]
        public string FourteenthAntennaPower { get => _14thantennapower; set => _14thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Fifteenth Antenna Power")]
        public string FifteenthAntennaPower { get => _15thantennapower; set => _15thantennapower = value; }

        [Category("Antenna Power Settings"), Description("Sixteenth Antenna Power")]
        public string SixteenthAntennaPower { get => _16thantennapower; set => _16thantennapower = value; }


        public ComponentModel() { }

    }
}
