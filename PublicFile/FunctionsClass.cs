using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace IDT_Reader
{
    internal static class FunctionsClass
    {
        static string[] KeyList = { "Length", "Antenna", "RSSI", "reserveMemory", "epcMemory", "tidMemory", "userMemory" };
        private static string[] ByteArrayToBinary(byte[] byteArray)
        {
            string binaryString = "";

            foreach (byte b in byteArray)
            {
                // Convert byte to 8-bit binary string
                binaryString += Convert.ToString(b, 2).PadLeft(8, '0');
            }
            // Convert binary string to a string array where each element is a character
            string[] stringArray = binaryString.ToCharArray().Select(c => c.ToString()).ToArray();

            return stringArray;
        }

        private static byte[] GetBytes(byte[] data, ref int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            index += length;
            return result;
        }
        public static bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public static Dictionary<string, string> SpitAllBody(byte[] allMemory)
        {
            var valuePairs = new Dictionary<string, string>();

            try
            {
                int memoryIndex = 3;
                int tagIdIndex = 5;

                // Extract header and EPCID
                byte[] header = GetBytes(allMemory, ref memoryIndex, 1);
                byte tagIdLength = allMemory[4];
                byte[] tagId = GetBytes(allMemory, ref tagIdIndex, tagIdLength);
                valuePairs["EPCID"] = BitConverter.ToString(tagId).Replace("-", "");

                // Parse memory data
                string[] availableMemory = ByteArrayToBinary(header);
                ParseMemorySections(valuePairs, availableMemory, ref tagIdIndex, allMemory);
            }
            catch (Exception ex)
            {
                // Ensure all keys exist in case of exceptions
                foreach (string key in KeyList)
                    if (!valuePairs.ContainsKey(key))
                        valuePairs[key] = "";
            }

            return valuePairs;
        }


        private static void ParseMemorySections(Dictionary<string, string> valuePairs, string[] availableMemory, ref int tagIdIndex, byte[] allMemory)
        {
            if (availableMemory[0] == "1")
            {
                byte length = allMemory[tagIdIndex++];
                valuePairs.Add("Length", length.ToString());
                PublicVariables._MemoryCheck["Length"] = true;
            }
            else
            { valuePairs.Add("Length", ""); PublicVariables._MemoryCheck["Length"] = false; }

            if (availableMemory[1] == "1")
            {
                byte antenna = allMemory[tagIdIndex++];
                valuePairs.Add("Antenna", antenna.ToString());
                PublicVariables._MemoryCheck["Antenna"] = true;
            }
            else
            { valuePairs.Add("Antenna", ""); PublicVariables._MemoryCheck["Antenna"] = false; }

            if (availableMemory[2] == "1")
            {
                byte rssi = allMemory[tagIdIndex++];
                valuePairs.Add("RSSI", rssi.ToString("X2"));
                PublicVariables._MemoryCheck["RSSI"] = true;
            }
            else
            { valuePairs.Add("RSSI", ""); PublicVariables._MemoryCheck["RSSI"] = false; }

            if (availableMemory[3].Equals("1")) //Reserve Memory 
            {
                byte index = allMemory[tagIdIndex++];
                byte[] Reserve = GetBytes(allMemory, ref tagIdIndex, index);
                valuePairs.Add("reserveMemory", BitConverter.ToString(Reserve).Replace("-", ""));
                PublicVariables._MemoryCheck["reserveMemory"] = true;
            }
            else
            { valuePairs.Add("reserveMemory", ""); PublicVariables._MemoryCheck["reserveMemory"] = false; }

            if (availableMemory[4].Equals("1"))//EPC Memory
            {
                byte index = allMemory[tagIdIndex++];
                byte[] Reserve = GetBytes(allMemory, ref tagIdIndex, index);
                valuePairs.Add("epcMemory", BitConverter.ToString(Reserve).Replace("-", ""));
                PublicVariables._MemoryCheck["epcMemory"] = true;
            }
            else
            { valuePairs.Add("epcMemory", ""); PublicVariables._MemoryCheck["epcMemory"] = false; }

            if (availableMemory[5].Equals("1")) // TID Memory
            {

                byte index = allMemory[tagIdIndex++];
                byte[] Reserve = GetBytes(allMemory, ref tagIdIndex, index);
                valuePairs.Add("tidMemory", BitConverter.ToString(Reserve).Replace("-", ""));
                PublicVariables._MemoryCheck["tidMemory"] = true;
            }
            else
            { valuePairs.Add("tidMemory", ""); PublicVariables._MemoryCheck["tidMemory"] = false; }

            if (availableMemory[6].Equals("1")) //User Memory
            {
                byte index = allMemory[tagIdIndex++];
                byte[] Reserve = GetBytes(allMemory, ref tagIdIndex, index);
                valuePairs.Add("userMemory", BitConverter.ToString(Reserve).Replace("-", ""));
                PublicVariables._MemoryCheck["userMemory"] = true;
            }
            else
            { valuePairs.Add("userMemory", ""); PublicVariables._MemoryCheck["userMemory"] = false; }
        }




        public static List<byte[]> SplitAtByteLoop(byte[] dataReceived,byte hex)
        {
            List<byte[]> sections = new List<byte[]>();

            try
            {
                int index = 0;
                while (index < dataReceived.Length)
                {
                    int foundIndex = Array.IndexOf(dataReceived, hex, index);
                    if (foundIndex < 0) break;

                    int startIndex = foundIndex - 2;
                    if (startIndex < 0) break;

                    int length = dataReceived[startIndex];
                    if (startIndex + length > dataReceived.Length) break;

                    byte[] segment = new byte[length];
                    Array.Copy(dataReceived, startIndex, segment, 0, length);
                    sections.Add(segment);

                    index = startIndex + length;
                }
            }
            catch (Exception ex)
            {
                //ShowLog("Error splitting buffer: " + ex.ToString());
            }
            return sections;
        }

        //


        public static List<byte[]> SplitAtRowByteLoop(byte[] dataReceived, byte byteValue)
        {
            List<byte[]> sections = new List<byte[]>();

            try
            {
                int index = 0;
                while (index < dataReceived.Length)
                {
                    int foundIndex = Array.IndexOf(dataReceived, byteValue, index);
                    if (foundIndex < 0) break;

                    int startIndex = foundIndex - 2;
                    if (startIndex < 0) break;

                    int length = dataReceived[startIndex];
                    if (startIndex + length > dataReceived.Length) break;

                    byte[] segment = new byte[length];
                    Array.Copy(dataReceived, startIndex, segment, 0, length);
                    sections.Add(segment);

                    index = startIndex + length;
                }
            }
            catch (Exception ex)
            {
                //ShowLog("Error splitting buffer: " + ex.ToString());
            }
            return sections;
        }

        // Helper method to convert a hex string (e.g., "01 23 45") to a byte array
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(i => i % 2 == 0)
                             .Select(i => Convert.ToByte(hex.Substring(i, 2), 16))
                             .ToArray();
        }


        public static string ConvertHexToAscii(string hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string hs = hexString.Substring(i, 2);
                    uint decval = Convert.ToUInt32(hs, 16);
                    char character = Convert.ToChar(decval);
                    ascii += character;
                }
                return ascii;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static Dictionary<string, string> LoadReaderConfig(byte[] byteArray)
        {
            string hex1 = string.Concat(BitConverter.ToString(byteArray).Replace("-", " "));

            // Initialize the dictionary to store the data
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            // Add entries to the dictionary based on the specified positions and sizes
            dataDictionary["Length of Data"] = ByteArrayToHex(byteArray, 0, 1); // Byte[0]
            dataDictionary["Reader Address"] = ByteArrayToHex(byteArray, 1, 1); // Byte[1]
            dataDictionary["Response Code"] = ByteArrayToHex(byteArray, 2, 1); // Byte[2]
            dataDictionary["Request Status"] = ByteArrayToHex(byteArray, 3, 1); // Byte[3]

            dataDictionary["RF Power"] = hexToDecimal(ByteArrayToHex(byteArray, 4, 1),1); // Byte[4]
            dataDictionary["Scan Time"] = hexToDecimal(ByteArrayToHex(byteArray, 5, 1),1); // Byte[5]
            dataDictionary["Antenna Configuration"] = hexToDecimal(ByteArrayToHex(byteArray, 6, 1),1); // Byte[6]
            dataDictionary["Antenna Check"] = hexToDecimal(ByteArrayToHex(byteArray, 7, 1),1); // Byte[7]
            dataDictionary["Working RF Frequency Band"] = hexToDecimal(ByteArrayToHex(byteArray, 8, 1),1); // Byte[8]
            dataDictionary["Read Type"] = hexToDecimal(ByteArrayToHex(byteArray, 9, 1),1); // Byte[9]
            dataDictionary["UHF Module ID"] = hexToDecimal(ByteArrayToHex(byteArray, 10, 4),1); // Byte[10-13]
            dataDictionary["Antenna Return Loss"] = hexToDecimal(ByteArrayToHex(byteArray, 14, 1),1); // Byte[14]
            dataDictionary["GPI [0]"] = hexToDecimal(ByteArrayToHex(byteArray, 15, 1),1); // Byte[15]
            dataDictionary["GPOA [0]"] = hexToDecimal(ByteArrayToHex(byteArray, 16, 1),1); // Byte[16]
            dataDictionary["GPOB [0]"] = hexToDecimal(ByteArrayToHex(byteArray, 17, 1),1); // Byte[17]
            dataDictionary["Reader Work Mode"] = hexToDecimal(ByteArrayToHex(byteArray, 18, 1),1); // Byte[18]
            dataDictionary["TAG Protocol"] = hexToDecimal(ByteArrayToHex(byteArray, 19, 1),1); // Byte[19]
            dataDictionary["Inv. Read Pause Time"] = hexToDecimal(ByteArrayToHex(byteArray, 20, 1),1); // Byte[20]
            dataDictionary["TAG Filter Time"] = hexToDecimal(ByteArrayToHex(byteArray, 21, 1),1); // Byte[21]
            dataDictionary["TAG Read Status"] = hexToDecimal(ByteArrayToHex(byteArray, 22, 1),1); // Byte[22]
            dataDictionary["Q Value"] = hexToDecimal(ByteArrayToHex(byteArray, 23, 1),1); // Byte[23]
            dataDictionary["Session"] = hexToDecimal(ByteArrayToHex(byteArray, 24, 1),1); // Byte[24]
            dataDictionary["Heartbeat Time"] = hexToDecimal(ByteArrayToHex(byteArray, 25, 1),1); // Byte[25]
            dataDictionary["Max. Scan Retry Time"] = hexToDecimal(ByteArrayToHex(byteArray, 26, 1),1); // Byte[26]
            dataDictionary["TAG Custom Password"] = hexToDecimal(ByteArrayToHex(byteArray, 27, 4),1); // Byte[27-30]
            dataDictionary["Reader Profile"] = hexToDecimal(ByteArrayToHex(byteArray, 31, 1),1); // Byte[31]
            dataDictionary["DRM Configuration"] = hexToDecimal(ByteArrayToHex(byteArray, 32, 1),1); // Byte[32]
            dataDictionary["Reader Temperature"] = hexToDecimal(ByteArrayToHex(byteArray, 33, 1),1); // Byte[33]
            dataDictionary["EPC TID Length"] = hexToDecimal(ByteArrayToHex(byteArray, 34, 1),1); // Byte[34]
            dataDictionary["TAG Write Power"] = hexToDecimal(ByteArrayToHex(byteArray, 35, 1),1); // Byte[35]
            dataDictionary["1st Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 36, 1),1); // Byte[36]
            dataDictionary["2nd Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 37, 1),1); // Byte[37]
            dataDictionary["3rd Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 38, 1),1); // Byte[38]
            dataDictionary["4th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 39, 1),1); // Byte[39]
            dataDictionary["5th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 40, 1),1); // Byte[40]
            dataDictionary["6th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 41, 1),1); // Byte[41]
            dataDictionary["7th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 42, 1),1); // Byte[42]
            dataDictionary["8th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 43, 1),1); // Byte[43]
            dataDictionary["9th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 44, 1),1); // Byte[44]
            dataDictionary["10th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 45, 1),1); // Byte[45]
            dataDictionary["11th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 46, 1),1); // Byte[46]
            dataDictionary["12th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 47, 1),1); // Byte[47]
            dataDictionary["13th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 48, 1),1); // Byte[48]
            dataDictionary["14th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 49, 1),1); // Byte[49]
            dataDictionary["15th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 50, 1),1); // Byte[50]
            dataDictionary["16th Antenna Power"] = hexToDecimal(ByteArrayToHex(byteArray, 51, 1),1); // Byte[51]
            dataDictionary["Module Baud Rate"] = hexToDecimal(ByteArrayToHex(byteArray, 52, 1),1); // Byte[52]
            dataDictionary["Reader Baud Rate"] = hexToDecimal(ByteArrayToHex(byteArray, 53, 1),1); // Byte[53]
            dataDictionary["1st Antenna Return Loss"] = hexToDecimal(ByteArrayToHex(byteArray, 54, 1),1); // Byte[54]
            dataDictionary["2nd Antenna Return Loss"] = hexToDecimal(ByteArrayToHex(byteArray, 55, 1),1); // Byte[55]
            dataDictionary["3rd Antenna Return Loss"] = hexToDecimal(ByteArrayToHex(byteArray, 56, 1),1); // Byte[56]
            dataDictionary["4th Antenna Return Loss"] = hexToDecimal(ByteArrayToHex(byteArray, 57, 1),1); // Byte[57]
            dataDictionary["MAC"] = hexToDecimal(ByteArrayToHex(byteArray, 58, 6),1); // Byte[58-63]
            dataDictionary["Reader local IP"] = hexToDecimal(ByteArrayToHex(byteArray, 64, 4),2); // Byte[64-67]
            dataDictionary["Gateway"] = hexToDecimal(ByteArrayToHex(byteArray, 68, 4),2); // Byte[68-71]
            dataDictionary["Subnet"] = hexToDecimal(ByteArrayToHex(byteArray, 72, 4),2); // Byte[72-75]
            dataDictionary["DNS"] = hexToDecimal(ByteArrayToHex(byteArray, 76, 4),2); // Byte[76-79]
            dataDictionary["Local port"] = hexToDecimal(ByteArrayToHex(byteArray, 80, 2),3); // Byte[80-81]
            dataDictionary["Buzzer Delay"] = hexToDecimal(ByteArrayToHex(byteArray, 82, 1),1); // Byte[82]
            dataDictionary["StaticIP"] = hexToDecimal(ByteArrayToHex(byteArray, 83, 4),2); // Byte[83-86]
            dataDictionary["Server Port"] = hexToDecimal(ByteArrayToHex(byteArray, 87, 4),3); // Byte[87-90]
            dataDictionary["Ethernet Modes"] = hexToDecimal(ByteArrayToHex(byteArray, 91, 1),1); // Byte[91]
            dataDictionary["Reader Unique ID"] = hexToDecimal(ByteArrayToHex(byteArray, 92, 12),1); // Byte[92-103]
            dataDictionary["CRC-16"] = ByteArrayToHex(byteArray, 104, 2); // Byte[104-105]

            return dataDictionary;
        }

        static string hexToDecimal(string hexString, int value)
        {
            string _decimal = "";
            switch (value)
            {
                case 1:
                    // Split the hex string into pairs of two
                    for (int i = 0; i < hexString.Length; i += 2)
                    {
                        string hexPair = hexString.Substring(i, 2);

                        // Convert the hex pair to decimal
                        int decimalValue = Convert.ToInt32(hexPair, 16);

                        _decimal += decimalValue.ToString();
                    }
                    //return _decimal;
                    break;
                case 2:

                    // Split the hex string into 2-digit segments
                    string[] hexSegments = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        hexSegments[i] = hexString.Substring(i * 2, 2);
                    }

                    // Convert each hex segment to decimal
                    int[] decimalSegments = new int[4];
                    for (int i = 0; i < 4; i++)
                    {
                        decimalSegments[i] = Convert.ToInt32(hexSegments[i], 16);
                    }

                    // Combine the decimal segments into a DNS address format
                    _decimal = string.Join(".", decimalSegments);

                    break;
                case 3:
                    // Combine the full hex value into one decimal number
                    int port = Convert.ToInt32(hexString, 16);
                    _decimal = port.ToString();
                    break;
                default:
                    break;
            }
            return _decimal;
        }



        // Helper method to convert byte array to hex string (to match the data structure format)
        static string ByteArrayToHex(byte[] byteArray, int start, int length)
        {
            return BitConverter.ToString(byteArray, start, length).Replace("-", "");
        }


        public static string ByteToHexString(byte[] data, int len)
        {
            // convert the Byte to Hexa String 
            string str = string.Empty;
            for (int index = 0; index < len; ++index)
                str += string.Format("{0:X2}", (object)data[index]);
            return str;
        }

        public static string GetLocalIPAddress()
        {
            string localIP = "Not Found";
            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // IPv4
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

       public static bool ContainsSequence(byte[] mainArray, byte[] subArray)
        {
            if (subArray.Length > mainArray.Length)
                return false;

            for (int i = 0; i <= mainArray.Length - subArray.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < subArray.Length; j++)
                {
                    if (mainArray[i + j] != subArray[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return true;
            }
            return false;
        }
    }
}
