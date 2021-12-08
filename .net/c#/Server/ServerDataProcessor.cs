using System;

using wclCommon;
using wclCommunication;

using RfCommClientServer;

namespace RfCommServer
{
    internal delegate void DataEvent(Object Sender);

    internal class ServerDataProcessor : wclCustomServerClientDataProcessor
    {
        private Byte[] FBuffer;

        #region Write data
        private Int32 IntWriteData(Byte Data, Boolean Signed)
        {
            Byte[] Cmd = new Byte[4];
            Cmd[0] = 0x00;
            Cmd[1] = 0x04;
            if (Signed)
                Cmd[2] = Commands.CMD_SEND_SBYTE;
            else
                Cmd[2] = Commands.CMD_SEND_BYTE;
            Cmd[3] = Data;

            return Write(Cmd);
        }

        private Int32 IntWriteData(UInt16 Data, Boolean Signed)
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

            return Write(Cmd);
        }

        private Int32 IntWriteData(UInt32 Data, Boolean Signed)
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

            return Write(Cmd);
        }

        private Int32 IntWriteData(UInt64 Data, Boolean Signed)
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

            return Write(Cmd);
        }
        #endregion

        #region Data processing
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
                        DoUInt64Received((UInt32)((Data[1] << 56) | (Data[2] << 48) | (Data[3] << 40) |
                            (Data[4] << 32) | (Data[5] << 24) | (Data[6] << 16) |
                            (Data[7] << 8) | Data[8]));
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
                        DoInt64Received((Int32)((Data[1] << 56) | (Data[2] << 48) | (Data[3] << 40) |
                            (Data[4] << 32) | (Data[5] << 24) | (Data[6] << 16) |
                            (Data[7] << 8) | Data[8]));
                    break;

                case Commands.CMD_ARRAY:
                    if (Data.Length > 1)
                    {
                        Byte[] Arr = new Byte[Data.Length - 1];
                        Array.Copy(Data, 1, Arr, 0, Data.Length - 1);
                        DoArrayReceived(Arr);
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
        #endregion

        #region Inherited methods.
        protected override void ProcessData(Byte[] Data)
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
        #endregion

        #region Event processors
        #region Received events.
        protected virtual void DoByteReceived(Byte Data)
        {
            if (OnByteReceived != null)
                OnByteReceived(this, Data);
        }

        protected virtual void DoUInt16Received(UInt16 Data)
        {
            if (OnUInt16Received != null)
                OnUInt16Received(this, Data);
        }

        protected virtual void DoUInt32Received(UInt32 Data)
        {
            if (OnUInt32Received != null)
                OnUInt32Received(this, Data);
        }

        protected virtual void DoUInt64Received(UInt64 Data)
        {
            if (OnUInt64Received != null)
                OnUInt64Received(this, Data);
        }

        protected virtual void DoSByteReceived(SByte Data)
        {
            if (OnSByteReceived != null)
                OnSByteReceived(this, Data);
        }

        protected virtual void DoInt16Received(Int16 Data)
        {
            if (OnInt16Received != null)
                OnInt16Received(this, Data);
        }
        protected virtual void DoInt32Received(Int32 Data)
        {
            if (OnInt32Received != null)
                OnInt32Received(this, Data);
        }

        protected virtual void DoInt64Received(Int64 Data)
        {
            if (OnInt64Received != null)
                OnInt64Received(this, Data);
        }

        protected virtual void DoArrayReceived(Byte[] Data)
        {
            if (OnArrayReceived != null)
                OnArrayReceived(this, Data);
        }
        #endregion

        #region Get events
        protected virtual void DoGetByte()
        {
            if (OnGetByte != null)
                OnGetByte(this);
        }

        protected virtual void DoGetUInt16()
        {
            if (OnGetUIn16 != null)
                OnGetUIn16(this);
        }

        protected virtual void DoGetUInt32()
        {
            if (OnGetUIn32 != null)
                OnGetUIn32(this);
        }

        protected virtual void DoGetUInt64()
        {
            if (OnGetUInt64 != null)
                OnGetUInt64(this);
        }

        protected virtual void DoGetSByte()
        {
            if (OnGetSByte != null)
                OnGetSByte(this);
        }

        protected virtual void DoGetInt16()
        {
            if (OnGetInt16 != null)
                OnGetInt16(this);
        }

        protected virtual void DoGetInt32()
        {
            if (OnGetInt32 != null)
                OnGetInt32(this);
        }

        protected virtual void DoGetInt64()
        {
            if (OnGetInt64 != null)
                OnGetInt64(this);
        }

        protected virtual void DoGetArray()
        {
            if (OnGetArray != null)
                OnGetArray(this);
        }
        #endregion
        #endregion

        public ServerDataProcessor(wclServerClientDataConnection Connection)
            : base(Connection)
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

            OnGetByte = null;
            OnGetUIn16 = null;
            OnGetUIn32 = null;
            OnGetUInt64 = null;
            OnGetSByte = null;
            OnGetInt16 = null;
            OnGetInt32 = null;
            OnGetInt64 = null;
            OnGetArray = null;
        }

        #region Write data
        #region Unsigned
        public Int32 WriteData(Byte Data)
        {
            return IntWriteData(Data, false);
        }

        public Int32 WriteData(UInt16 Data)
        {
            return IntWriteData(Data, false);
        }

        public Int32 WriteData(UInt32 Data)
        {
            return IntWriteData(Data, false);
        }

        public Int32 WriteData(UInt64 Data)
        {
            return IntWriteData(Data, false);
        }
        #endregion

        #region Signed
        public Int32 WriteData(SByte Data)
        {
            return IntWriteData((Byte)Data, true);
        }

        public Int32 WriteData(Int16 Data)
        {
            return IntWriteData((UInt16)Data, true);
        }

        public Int32 WriteData(Int32 Data)
        {
            return IntWriteData((UInt32)Data, true);
        }

        public Int32 WriteData(Int64 Data)
        {
            return IntWriteData((UInt64)Data, true);
        }
        #endregion

        #region Array
        public Int32 WriteData(Byte[] Data)
        {
            if (Data == null || Data.Length == 0 || Data.Length > UInt16.MaxValue - 3)
                return wclErrors.WCL_E_INVALID_ARGUMENT;

            UInt16 Len = (UInt16)(3 + Data.Length);
            Byte[] Cmd = new Byte[Len];
            Cmd[0] = wclHelpers.HiByte(Len);
            Cmd[1] = wclHelpers.LoByte(Len);
            Cmd[2] = Commands.CMD_SEND_ARRAY;
            Array.Copy(Data, 0, Cmd, 3, Data.Length);

            return Write(Cmd);
        }
        #endregion

        #region Send error
        public Int32 SendError(Int32 Error)
        {
            Byte[] Cmd = new Byte[7];
            Cmd[0] = 0x00;
            Cmd[1] = 0x07;
            Cmd[2] = Commands.CMD_ERROR_CODE;
            Cmd[3] = wclHelpers.HiByte(wclHelpers.HiWord((UInt32)Error));
            Cmd[4] = wclHelpers.LoByte(wclHelpers.HiWord((UInt32)Error));
            Cmd[5] = wclHelpers.HiByte(wclHelpers.LoWord((UInt32)Error));
            Cmd[6] = wclHelpers.LoByte(wclHelpers.LoWord((UInt32)Error));
            return Write(Cmd);
        }
        #endregion
        #endregion

        #region Events.
        #region Received events.
        public event ByteReceived OnByteReceived;
        public event UInt16Received OnUInt16Received;
        public event UInt32Received OnUInt32Received;
        public event UInt64Received OnUInt64Received;
        public event SByteReceived OnSByteReceived;
        public event Int16Received OnInt16Received;
        public event Int32Received OnInt32Received;
        public event Int64Received OnInt64Received;
        public event ArrayReceived OnArrayReceived;
        #endregion

        #region Get events.
        public event DataEvent OnGetByte;
        public event DataEvent OnGetUIn16;
        public event DataEvent OnGetUIn32;
        public event DataEvent OnGetUInt64;
        public event DataEvent OnGetSByte;
        public event DataEvent OnGetInt16;
        public event DataEvent OnGetInt32;
        public event DataEvent OnGetInt64;
        public event DataEvent OnGetArray;
        #endregion 
        #endregion
    };
}
