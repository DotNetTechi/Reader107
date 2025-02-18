using System;
using System.Text;

namespace IDT_Reader
{
    public static class CRC16MCRF4XX
    {
        // CRC-16/MCRF4XX polynomial: 0x8005
        const ushort Polynomial = 0x8408;
        const ushort InitialValue = 0xFFFF;
        const ushort FinalXOR = 0x0000; // No final XOR required for this algorithm

        // Method to calculate CRC-16/MCRF4XX
        // Method to calculate CRC-16
        public static int CalculateCRC(byte[] data, byte length)
        {
            int crcValue = InitialValue;  // Initialize CRC with preset value

            // Iterate over each byte in the data array
            for (byte i = 0; i < length; i++)
            {
                crcValue ^= data[i];  // XOR the byte with the current CRC value

                // Process each bit of the byte
                for (byte j = 0; j < 8; j++)
                {
                    // If the LSB is 1, apply the polynomial
                    if ((crcValue & 0x0001) != 0)
                    {
                        crcValue = (crcValue >> 1) ^ Polynomial;  // Shift and apply the polynomial
                    }
                    else
                    {
                        crcValue >>= 1;  // Shift CRC value to the right
                    }
                }
            }

            return crcValue;  // Return the final CRC value
        }

        public static string CalculateCRCAsHex(byte[] data)
        {
            int crc = CalculateCRC(data, (byte)data.Length);
            return crc.ToString("X4"); // Return CRC in 4-digit hexadecimal format
        }

        // Verify the CRC value for a given data array
        public static bool VerifyCRC(byte[] data)
        {
            // Convert the hex string to a byte array
            //byte[] data = ConvertHexStringToByteArray(hexString);
            string hexCRC = CalculateCRCAsHex(data);
            return hexCRC.Equals("0000");
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            // Ensure that the string length is even (if not, prepend a 0)
            if (hexString.Length % 2 != 0)
            {
                hexString = "0" + hexString;
            }
            return HexStringToByteArray(hexString);  // Convert the hex string to a byte array
        }

        // Manual method to convert hex string to byte array
        public static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] byteArray = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return byteArray;
        }


        public static string GeneratorCRC(byte[] data)
        {
            //byte[] data = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB };
            string hexString = "";
            // Or get the CRC value as a hex string
            string hexCRC = CalculateCRCAsHex(data);

            // Create a byte array to store the result
            byte[] byteArray = ConvertstringToBytearray(hexCRC);

            byte[] fbyteArray = new byte[] { byteArray[1], byteArray[0] };

            hexString = BitConverter.ToString(fbyteArray).Replace("-", "");

            return hexString;
        }

        public static byte[] ConvertstringToBytearray(string hexString)
        {
            byte[] byteArray = new byte[hexString.Length / 2];
            // Convert each pair of characters into a byte
            for (int i = 0; i < byteArray.Length; i++)
            {
                // Take two characters at a time, convert to byte, and assign to byteArray
                string ui = hexString.Substring(i * 2, 2);
                byteArray[i] = Convert.ToByte(ui, 16);
            }
            return byteArray;
        }


        public static string StringToHex(string input)
        {
            StringBuilder hex = new StringBuilder();
            foreach (char c in input)
            {
                hex.Append(((int)c).ToString("X2")); // Convert to hexadecimal with 2 digits
            }
            return hex.ToString();
        }
    }

}
