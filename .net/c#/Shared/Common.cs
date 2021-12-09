using System;
using System.Text;

using wclCommon;

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
    internal delegate void StringReceived(Object Sender, String Data);
    #endregion

    internal delegate void DataEvent(Object Sender);

    internal delegate void ErrorEvent(Object Sender, Int32 Error);
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
        public const Byte CMD_STRING = 0x0A;

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
        public const Byte CMD_SEND_STRING = CMD_FLAG_SEND | CMD_STRING;
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
        public const Byte CMD_GET_STRING = CMD_FLAG_GET | CMD_STRING;
        #endregion

        #region Error commands.
        public const Byte CMD_ERROR_CODE = CMD_FLAG_ERROR | CMD_ERROR;
        #endregion

        #region Service's UUID
        public static Guid ServiceUuid = new Guid("{CA80C97C-06B3-4E65-9CEE-65BB0B11BC92}");
        #endregion

        #region Command checkers
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
        #endregion
    };

    internal static class CommandBuilder
    {
        public static Byte[] Create(Byte Data, Boolean Signed)
        {
            Byte[] Cmd = new Byte[4];
            Cmd[0] = 0x00;
            Cmd[1] = 0x04;
            if (Signed)
                Cmd[2] = Commands.CMD_SEND_SBYTE;
            else
                Cmd[2] = Commands.CMD_SEND_BYTE;
            Cmd[3] = Data;
            return Cmd;
        }

        public static Byte[] Create(UInt16 Data, Boolean Signed)
        {
            Byte[] Cmd = new Byte[5];
            Cmd[0] = 0x00;
            Cmd[1] = 0x05;
            if (Signed)
                Cmd[2] = Commands.CMD_SEND_INT16;
            else
                Cmd[2] = Commands.CMD_SEND_UINT16;
            Cmd[3] = wclHelpers.HiByte(Data);
            Cmd[4] = wclHelpers.LoByte(Data);
            return Cmd;
        }

        public static Byte[] Create(UInt32 Data, Boolean Signed)
        {
            Byte[] Cmd = new Byte[7];
            Cmd[0] = 0x00;
            Cmd[1] = 0x07;
            if (Signed)
                Cmd[2] = Commands.CMD_SEND_INT32;
            else
                Cmd[2] = Commands.CMD_SEND_UINT32;
            Cmd[3] = wclHelpers.HiByte(wclHelpers.HiWord(Data));
            Cmd[4] = wclHelpers.LoByte(wclHelpers.HiWord(Data));
            Cmd[5] = wclHelpers.HiByte(wclHelpers.LoWord(Data));
            Cmd[6] = wclHelpers.LoByte(wclHelpers.LoWord(Data));
            return Cmd;
        }

        public static Byte[] Create(UInt64 Data, Boolean Signed)
        {
            UInt32 Hi = (UInt32)(Data >> 32);
            UInt32 Lo = (UInt32)(Data & 0x00000000FFFFFFFF);
            Byte[] Cmd = new Byte[11];
            Cmd[0] = 0x00;
            Cmd[1] = 0x0B;
            if (Signed)
                Cmd[2] = Commands.CMD_SEND_INT64;
            else
                Cmd[2] = Commands.CMD_SEND_UINT64;
            Cmd[3] = wclHelpers.HiByte(wclHelpers.HiWord(Hi));
            Cmd[4] = wclHelpers.LoByte(wclHelpers.HiWord(Hi));
            Cmd[5] = wclHelpers.HiByte(wclHelpers.LoWord(Hi));
            Cmd[6] = wclHelpers.LoByte(wclHelpers.LoWord(Hi));
            Cmd[7] = wclHelpers.HiByte(wclHelpers.HiWord(Lo));
            Cmd[8] = wclHelpers.LoByte(wclHelpers.HiWord(Lo));
            Cmd[9] = wclHelpers.HiByte(wclHelpers.LoWord(Lo));
            Cmd[10] = wclHelpers.LoByte(wclHelpers.LoWord(Lo));
            return Cmd;
        }

        public static Byte[] Create(Byte[] Data)
        {
            UInt16 Len = (UInt16)(3 + Data.Length);
            Byte[] Cmd = new Byte[Len];
            Cmd[0] = wclHelpers.HiByte(Len);
            Cmd[1] = wclHelpers.LoByte(Len);
            Cmd[2] = Commands.CMD_SEND_ARRAY;
            Array.Copy(Data, 0, Cmd, 3, Data.Length);
            return Cmd;
        }

        public static Byte[] Create(String Data)
        {
            Byte[] Str = Encoding.UTF8.GetBytes(Data);
            UInt16 Len = (UInt16)(3 + Str.Length);
            Byte[] Cmd = new Byte[Len];
            Cmd[0] = wclHelpers.HiByte(Len);
            Cmd[1] = wclHelpers.LoByte(Len);
            Cmd[2] = Commands.CMD_SEND_STRING;
            Array.Copy(Str, 0, Cmd, 3, Str.Length);
            return Cmd;
        }

        public static Byte[] Create(Int32 Error)
        {
            Byte[] Cmd = new Byte[7];
            Cmd[0] = 0x00;
            Cmd[1] = 0x07;
            Cmd[2] = Commands.CMD_ERROR_CODE;
            Cmd[3] = wclHelpers.HiByte(wclHelpers.HiWord((UInt32)Error));
            Cmd[4] = wclHelpers.LoByte(wclHelpers.HiWord((UInt32)Error));
            Cmd[5] = wclHelpers.HiByte(wclHelpers.LoWord((UInt32)Error));
            Cmd[6] = wclHelpers.LoByte(wclHelpers.LoWord((UInt32)Error));
            return Cmd;
        }

        public static Byte[] Create(Byte Cmd)
        {
            Byte[] Data = new Byte[3];
            Data[0] = 0x00;
            Data[1] = 0x03;
            Data[2] = Cmd;
            return Data;
        }
    };

    internal class CommandDecoder
    {
        private Byte[] FBuffer;

        private void DoByteReceived(Byte Data)
        {
            if (OnByteReceived != null)
                OnByteReceived(this, Data);
        }

        private void DoUInt16Received(UInt16 Data)
        {
            if (OnUInt16Received != null)
                OnUInt16Received(this, Data);
        }

        private void DoUInt32Received(UInt32 Data)
        {
            if (OnUInt32Received != null)
                OnUInt32Received(this, Data);
        }

        private void DoUInt64Received(UInt64 Data)
        {
            if (OnUInt64Received != null)
                OnUInt64Received(this, Data);
        }

        private void DoSByteReceived(SByte Data)
        {
            if (OnSByteReceived != null)
                OnSByteReceived(this, Data);
        }

        private void DoInt16Received(Int16 Data)
        {
            if (OnInt16Received != null)
                OnInt16Received(this, Data);
        }
        private void DoInt32Received(Int32 Data)
        {
            if (OnInt32Received != null)
                OnInt32Received(this, Data);
        }

        private void DoInt64Received(Int64 Data)
        {
            if (OnInt64Received != null)
                OnInt64Received(this, Data);
        }

        private void DoArrayReceived(Byte[] Data)
        {
            if (OnArrayReceived != null)
                OnArrayReceived(this, Data);
        }

        private void DoStringReceived(String Data)
        {
            if (OnStringReceived != null)
                OnStringReceived(this, Data);
        }

        private void DoGetByte()
        {
            if (OnGetByte != null)
                OnGetByte(this);
        }

        private void DoGetUInt16()
        {
            if (OnGetUInt16 != null)
                OnGetUInt16(this);
        }

        private void DoGetUInt32()
        {
            if (OnGetUInt32 != null)
                OnGetUInt32(this);
        }

        private void DoGetUInt64()
        {
            if (OnGetUInt64 != null)
                OnGetUInt64(this);
        }

        private void DoGetSByte()
        {
            if (OnGetSByte != null)
                OnGetSByte(this);
        }

        private void DoGetInt16()
        {
            if (OnGetInt16 != null)
                OnGetInt16(this);
        }

        private void DoGetInt32()
        {
            if (OnGetInt32 != null)
                OnGetInt32(this);
        }

        private void DoGetInt64()
        {
            if (OnGetInt64 != null)
                OnGetInt64(this);
        }

        private void DoGetArray()
        {
            if (OnGetArray != null)
                OnGetArray(this);
        }

        private void DoGetString()
        {
            if (OnGetString != null)
                OnGetString(this);
        }

        private void DoError(Int32 Error)
        {
            if (OnError != null)
                OnError(this, Error);
        }

        private void DecodeSendCommand(Byte[] Data)
        {
            switch (Data[0] & Commands.CMD_FLAG)
            {
                case Commands.CMD_BYTE:
                    if (Data.Length == 2)
                        DoByteReceived(Data[1]);
                    break;

                case Commands.CMD_UINT16:
                    if (Data.Length == 3)
                        DoUInt16Received((UInt16)((Data[1] << 8) | Data[2]));
                    break;

                case Commands.CMD_UINT32:
                    if (Data.Length == 5)
                        DoUInt32Received((UInt32)((Data[1] << 24) | (Data[2] << 16) | (Data[3] << 8) | Data[4]));
                    break;

                case Commands.CMD_UINT64:
                    if (Data.Length == 9)
                        DoUInt64Received((UInt64)(((UInt64)Data[1] << 56) | ((UInt64)Data[2] << 48) |
                            ((UInt64)Data[3] << 40) | ((UInt64)Data[4] << 32) | (UInt64)Data[5] << 24) |
                            ((UInt64)Data[6] << 16) | ((UInt64)Data[7] << 8) | (UInt64)Data[8]);
                    break;

                case Commands.CMD_SBYTE:
                    if (Data.Length == 2)
                        DoSByteReceived((SByte)Data[1]);
                    break;

                case Commands.CMD_INT16:
                    if (Data.Length == 3)
                        DoInt16Received((Int16)((Data[1] << 8) | Data[2]));
                    break;

                case Commands.CMD_INT32:
                    if (Data.Length == 5)
                        DoInt32Received((Int32)((Data[1] << 24) | (Data[2] << 16) | (Data[3] << 8) | Data[4]));
                    break;

                case Commands.CMD_INT64:
                    if (Data.Length == 9)
                        DoInt64Received((Int64)(((Int64)Data[1] << 56) | ((Int64)Data[2] << 48) |
                            ((Int64)Data[3] << 40) | ((Int64)Data[4] << 32) | ((Int64)Data[5] << 24) | 
                            ((Int64)Data[6] << 16) | ((Int64)Data[7] << 8) | (Int64)Data[8]));
                    break;

                case Commands.CMD_ARRAY:
                    if (Data.Length > 1)
                    {
                        Byte[] Arr = new Byte[Data.Length - 1];
                        Array.Copy(Data, 1, Arr, 0, Data.Length - 1);
                        DoArrayReceived(Arr);
                    }
                    break;

                case Commands.CMD_STRING:
                    if (Data.Length > 1)
                    {
                        String Str = Encoding.UTF8.GetString(Data, 1, Data.Length - 1);
                        DoStringReceived(Str);
                    }
                    break;
            }
        }

        private void DecodeGetCommand(Byte[] Data)
        {
            if (Data.Length == 1)
            {
                switch (Data[0] & Commands.CMD_FLAG)
                {
                    case Commands.CMD_BYTE:
                        DoGetByte();
                        break;

                    case Commands.CMD_UINT16:
                        DoGetUInt16();
                        break;

                    case Commands.CMD_UINT32:
                        DoGetUInt32();
                        break;

                    case Commands.CMD_UINT64:
                        DoGetUInt64();
                        break;

                    case Commands.CMD_SBYTE:
                        DoGetSByte();
                        break;

                    case Commands.CMD_INT16:
                        DoGetInt16();
                        break;

                    case Commands.CMD_INT32:
                        DoGetInt32();
                        break;

                    case Commands.CMD_INT64:
                        DoGetInt64();
                        break;

                    case Commands.CMD_ARRAY:
                        DoGetArray();
                        break;

                    case Commands.CMD_STRING:
                        DoGetString();
                        break;
                }
            }
        }

        private void DecodeErrorCommand(Byte[] Data)
        {
            if (Data.Length == 5)
            {
                if (Data[0] == Commands.CMD_ERROR_CODE)
                {
                    Int32 Error = (Data[1] << 24) | (Data[2] << 16) | (Data[3] << 8) | Data[4];
                    DoError(Error);
                }
            }
        }

        private void DecodeData(Byte[] Data)
        {
            // It is guaranteed that the Data length at least 2 bytes.
            Byte Cmd = Data[0];
            if (Commands.IsCmdSend(Cmd))
                DecodeSendCommand(Data);
            else
            {
                if (Commands.IsCmdGet(Cmd))
                    DecodeGetCommand(Data);
                else
                {
                    if (Commands.IsCmdError(Cmd))
                        DecodeErrorCommand(Data);
                }
            }
        }

        private void DataReceived()
        {
            while (FBuffer != null && FBuffer.Length > 2)
            {
                // Data length includes length bytes!
                UInt16 Len = (UInt16)((FBuffer[0] << 8) | FBuffer[1]);
                if (Len > FBuffer.Length)
                    break;

                Byte[] Data = new Byte[Len - 2];
                Array.Copy(FBuffer, 2, Data, 0, Len - 2);
                DecodeData(Data);

                if (Len == FBuffer.Length)
                {
                    FBuffer = null;
                    break;
                }

                Data = new Byte[FBuffer.Length - Len];
                Array.Copy(FBuffer, Len, Data, 0, FBuffer.Length - Len);
                FBuffer = Data;
            }
        }

        public CommandDecoder()
        {
            FBuffer = null;

            OnByteReceived = null;
            OnUInt16Received = null;
            OnUInt32Received = null;
            OnUInt64Received = null;
            OnSByteReceived = null;
            OnInt16Received = null;
            OnInt32Received = null;
            OnInt64Received = null;
            OnArrayReceived = null;
            OnStringReceived = null;

            OnGetByte = null;
            OnGetUInt16 = null;
            OnGetUInt32 = null;
            OnGetUInt64 = null;
            OnGetSByte = null;
            OnGetInt16 = null;
            OnGetInt32 = null;
            OnGetInt64 = null;
            OnGetArray = null;
            OnGetString = null;

            OnError = null;
        }

        public void ProcessData(Byte[] Data)
        {
            if (Data != null && Data.Length > 0)
            {
                Int32 Ndx;
                if (FBuffer == null)
                {
                    FBuffer = new Byte[Data.Length];
                    Ndx = 0;
                }
                else
                {
                    Ndx = FBuffer.Length;
                    Array.Resize<Byte>(ref FBuffer, Ndx + Data.Length);
                }
                Array.Copy(Data, 0, FBuffer, Ndx, Data.Length);
                DataReceived();
            }
        }

        public event ByteReceived OnByteReceived;
        public event UInt16Received OnUInt16Received;
        public event UInt32Received OnUInt32Received;
        public event UInt64Received OnUInt64Received;
        public event SByteReceived OnSByteReceived;
        public event Int16Received OnInt16Received;
        public event Int32Received OnInt32Received;
        public event Int64Received OnInt64Received;
        public event ArrayReceived OnArrayReceived;
        public event StringReceived OnStringReceived;

        public event DataEvent OnGetByte;
        public event DataEvent OnGetUInt16;
        public event DataEvent OnGetUInt32;
        public event DataEvent OnGetUInt64;
        public event DataEvent OnGetSByte;
        public event DataEvent OnGetInt16;
        public event DataEvent OnGetInt32;
        public event DataEvent OnGetInt64;
        public event DataEvent OnGetArray;
        public event DataEvent OnGetString;

        public event ErrorEvent OnError;
    };
}