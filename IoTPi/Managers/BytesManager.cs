using System;
using System.Collections.Generic;
using System.Text;

namespace IoTPi.Managers
{
    public class BytesManager
    {
        public static byte ParseOneByte(byte byte1)
        {
            //I leave this here in case we need to process some data in the future
            return byte1;
        }

        public static UInt32 ParseTwoBytes(byte byte0, byte byte1)
        {
            return (UInt32)byte0 << 8 | (UInt32)byte1;
        }

        public static UInt32 ParseThreeBytes(byte byte0, byte byte1, byte byte2)
        {
            return (UInt32)byte0 << 16 | (UInt32)byte1 << 8 | (UInt32)byte2;
        }

        public static int ByteToLong(byte byte0, byte byte1, byte byte2, byte byte3)
        {
            return byte0 << 24 | byte1 << 16 | byte2 << 8 | byte3;
        }

        public static char ParseSign(byte byte0)
        {
            byte sign = ParseOneByte(byte0);
            if (sign == 0x2B)
            {
                return '+';
            }
            else if (sign == 0x2D)
            {
                return '-';
            }
            else if (sign == 0x58)
            {
                return 'X';
            }
            else
            {
                return 'F';
            }
        }
    }
}
