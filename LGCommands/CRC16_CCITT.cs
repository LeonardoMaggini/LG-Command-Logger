using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCommands
    {
    public enum InitialCrcValue
    {
    NonZero1 = 0xFFFF,
    NonZero2 = 0x1D0F,
    Zeros = 0
    }

 public   class CRC16_CCITT
        {
        private  InitialCrcValue InitialValue = 0;
        private const UInt16 poly = 0x1021;
        private UInt16[] table = new UInt16[0x100 - 1];

        public CRC16_CCITT(InitialCrcValue initialValue)
            {
            this.InitialValue =initialValue;

            for (int i = 0; i <= this.table.Length - 1;i++ )
                {
                UInt16 temp = 0;
                UInt16 a = (UInt16)(i << 8);
                for (int j = 0; j <= 7; j++)
                    {
                    if (((temp ^ a) & 0x8000) != 0)
                        {
                        temp = (UInt16)((temp << 1) ^ poly);
                        }
                    else
                        {
                        temp = (UInt16)(temp << 1);
                        }
                    a = (UInt16)(a << 1);
                    }
                this.table[i] = temp;
                }

            }

        public UInt16 CRC_CCITT_UInt16(byte[] bytes)
            {
            UInt16 crc =(UInt16) this.InitialValue;

            for (int i = 0; i <= bytes.Length - 1; i++)
                {
                crc = (UInt16)(((crc << 8) ^ this.table[((crc >> 8) ^ (0xFF & bytes[i]))] ));
                }
            return crc;
            }


        public byte[] CRC_CCITT(byte[] bytes)
            {
            UInt16 crc = CRC_CCITT_UInt16(bytes);
            return new byte[]{(Byte)(crc >> 8), (byte)(crc & 0xFF)};
            }

        }
    }


/*
Public Class CRC16CCITT

    Public Overloads Function CRC_CCITT(ByVal bytes As Byte()) As Byte()
        Dim crc As UInt16 = CRC_CCITT_Uint16(bytes)
        Return New Byte() {CByte((crc >> 8)), CByte((crc And &HFF))}
    End Function

    Private initialValue As UInt16 = 0
    Private Const poly As UInt16 = &H1021
    Private table As UInt16() = New UInt16(&H100 - 1) {}
End Class



*/