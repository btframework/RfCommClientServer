using System;

namespace RfCommClientServer
{
    #region Delegates.
    #region Data received delegates.
    internal delegate void ByteReceived(Object Sender, Byte Data);
    internal delegate void UInt16Received(Object Sender, UInt16 Data);
    internal delegate void UInt32Received(Object Sender, UInt32 Data);
    internal delegate void UInt64Received(Object Sender, UInt64 Data);
    internal delegate void SByteReceived(Object Sender, SByte Data);
    internal delegate void Int16Received(Object Sender, Int16 Data);
    internal delegate void Int32Received(Object Sender, Int32 Data);
    internal delegate void Int64Received(Object Sender, Int64 Data);
    internal delegate void ArrayReceived(Object Sender, Byte[] Data);
    #endregion
    #endregion

    internal static class Commands
    {
        #region Command flags
        public const Byte CMD_FLAG = 0x0F;

        public const Byte CMD_FLAG_SEND = 0x00;
        public const Byte CMD_FLAG_GET = 0x40;
        public const Byte CMD_FLAG_ERROR = 0x80;
        #endregion

        #region Commands.
        public const Byte CMD_BYTE = 0x01;
        public const Byte CMD_UINT16 = 0x02;
        public const Byte CMD_UINT32 = 0x03;
        public const Byte CMD_UINT64 = 0x04;
        public const Byte CMD_SBYTE = 0x05;
        public const Byte CMD_INT16 = 0x06;
        public const Byte CMD_INT32 = 0x07;
        public const Byte CMD_INT64 = 0x08;
        public const Byte CMD_ARRAY = 0x09;

        public const Byte CMD_ERROR = 0x0F;
        #endregion

        #region Send commands.
        public const Byte CMD_SEND_BYTE = CMD_FLAG_SEND | CMD_BYTE;
        public const Byte CMD_SEND_UINT16 = CMD_FLAG_SEND | CMD_UINT16;
        public const Byte CMD_SEND_UINT32 = CMD_FLAG_SEND | CMD_UINT32;
        public const Byte CMD_SEND_UINT64 = CMD_FLAG_SEND | CMD_UINT64;
        public const Byte CMD_SEND_SBYTE = CMD_FLAG_SEND | CMD_SBYTE;
        public const Byte CMD_SEND_INT16 = CMD_FLAG_SEND | CMD_INT16;
        public const Byte CMD_SEND_INT32 = CMD_FLAG_SEND | CMD_INT32;
        public const Byte CMD_SEND_INT64 = CMD_FLAG_SEND | CMD_INT64;
        public const Byte CMD_SEND_ARRAY = CMD_FLAG_SEND | CMD_ARRAY;
        #endregion

        #region Get commands.
        public const Byte CMD_GET_BYTE = CMD_FLAG_GET | CMD_BYTE;
        public const Byte CMD_GET_UINT16 = CMD_FLAG_GET | CMD_UINT16;
        public const Byte CMD_GET_UINT32 = CMD_FLAG_GET | CMD_UINT32;
        public const Byte CMD_GET_UINT64 = CMD_FLAG_GET | CMD_UINT64;
        public const Byte CMD_GET_SBYTE = CMD_FLAG_GET | CMD_SBYTE;
        public const Byte CMD_GET_INT16 = CMD_FLAG_GET | CMD_INT16;
        public const Byte CMD_GET_INT32 = CMD_FLAG_GET | CMD_INT32;
        public const Byte CMD_GET_INT64 = CMD_FLAG_GET | CMD_INT64;
        public const Byte CMD_GET_ARRAY = CMD_FLAG_GET | CMD_ARRAY;
        #endregion

        #region Error commands.
        public const Byte CMD_ERROR_CODE = CMD_FLAG_ERROR | CMD_ERROR;
        #endregion

        public static Boolean IsCmdSend(Byte Cmd)
        {
            return ((Cmd & CMD_FLAG_GET) == 0 && (Cmd & CMD_FLAG_ERROR) == 0);
        }

        public static Boolean IsCmdGet(Byte Cmd)
        {
            return ((Cmd & CMD_FLAG_GET) != 0);
        }

        public static Boolean IsCmdError(Byte Cmd)
        {
            return ((Cmd & CMD_FLAG_ERROR) != 0);
        }
    };
}