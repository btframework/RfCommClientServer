using System;

using wclCommon;
using wclCommunication;

using RfCommClientServer;

namespace RfCommClient
{
    internal class ClientDataProcessor : wclCustomClientDataProcessor
    {
        private CommandDecoder FDecoder;

        #region Data decoder events.
        private void FDecoder_OnArrayReceived(Object Sender, Byte[] Data)
        {
            if (OnArrayReceived != null)
                OnArrayReceived(this, Data);
        }

        private void FDecoder_OnStringReceived(Object Sender, String Data)
        {
            if (OnStringReceived != null)
                OnStringReceived(this, Data);
        }

        private void FDecoder_OnInt64Received(Object Sender, Int64 Data)
        {
            if (OnInt64Received != null)
                OnInt64Received(this, Data);
        }

        private void FDecoder_OnInt32Received(Object Sender, Int32 Data)
        {
            if (OnInt32Received != null)
                OnInt32Received(this, Data);
        }

        private void FDecoder_OnInt16Received(Object Sender, Int16 Data)
        {
            if (OnInt16Received != null)
                OnInt16Received(this, Data);
        }

        private void FDecoder_OnSByteReceived(Object Sender, SByte Data)
        {
            if (OnSByteReceived != null)
                OnSByteReceived(this, Data);
        }

        private void FDecoder_OnUInt64Received(Object Sender, UInt64 Data)
        {
            if (OnUInt64Received != null)
                OnUInt64Received(this, Data);
        }

        private void FDecoder_OnUInt32Received(Object Sender, UInt32 Data)
        {
            if (OnUInt32Received != null)
                OnUInt32Received(this, Data);
        }

        private void FDecoder_OnUInt16Received(Object Sender, UInt16 Data)
        {
            if (OnUInt16Received != null)
                OnUInt16Received(this, Data);
        }

        private void FDecoder_OnByteReceived(Object Sender, Byte Data)
        {
            if (OnByteReceived != null)
                OnByteReceived(this, Data);
        }

        private void FDecoder_OnError(Object Sender, Int32 Error)
        {
            if (OnError != null)
                OnError(this, Error);
        }
        #endregion

        protected override void ProcessData(Byte[] Data)
        {
            FDecoder.ProcessData(Data);
        }

        public ClientDataProcessor(wclClientDataConnection Connection)
            : base(Connection)
        {
            FDecoder = new CommandDecoder();

            FDecoder.OnByteReceived += FDecoder_OnByteReceived;
            FDecoder.OnUInt16Received += FDecoder_OnUInt16Received;
            FDecoder.OnUInt32Received += FDecoder_OnUInt32Received;
            FDecoder.OnUInt64Received += FDecoder_OnUInt64Received;
            FDecoder.OnSByteReceived += FDecoder_OnSByteReceived;
            FDecoder.OnInt16Received += FDecoder_OnInt16Received;
            FDecoder.OnInt32Received += FDecoder_OnInt32Received;
            FDecoder.OnInt64Received += FDecoder_OnInt64Received;
            FDecoder.OnArrayReceived += FDecoder_OnArrayReceived;
            FDecoder.OnStringReceived += FDecoder_OnStringReceived;

            FDecoder.OnError += FDecoder_OnError;

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

            OnError = null;
        }

        #region Write data
        #region Unsigned
        public Int32 WriteData(Byte Data)
        {
            return Write(CommandBuilder.Create(Data, false));
        }

        public Int32 WriteData(UInt16 Data)
        {
            return Write(CommandBuilder.Create(Data, false));
        }

        public Int32 WriteData(UInt32 Data)
        {
            return Write(CommandBuilder.Create(Data, false));
        }

        public Int32 WriteData(UInt64 Data)
        {
            return Write(CommandBuilder.Create(Data, false));
        }
        #endregion

        #region Signed
        public Int32 WriteData(SByte Data)
        {
            return Write(CommandBuilder.Create((Byte)Data, true));
        }

        public Int32 WriteData(Int16 Data)
        {
            return Write(CommandBuilder.Create((UInt16)Data, true));
        }

        public Int32 WriteData(Int32 Data)
        {
            return Write(CommandBuilder.Create((UInt32)Data, true));
        }

        public Int32 WriteData(Int64 Data)
        {
            return Write(CommandBuilder.Create((UInt64)Data, true));
        }
        #endregion

        #region Array
        public Int32 WriteData(Byte[] Data)
        {
            if (Data == null || Data.Length == 0 || (UInt32)Data.Length > UInt16.MaxValue - 3)
                return wclErrors.WCL_E_INVALID_ARGUMENT;

            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteData(String Data)
        {
            if (Data == null || Data.Length == 0 || (UInt32)Data.Length > UInt16.MaxValue - 3)
                return wclErrors.WCL_E_INVALID_ARGUMENT;

            return Write(CommandBuilder.Create(Data));
        }
        #endregion
        #endregion

        #region Get data
        #region Sigend
        public Int32 GetSByte()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_SBYTE));
        }

        public Int32 GetInt16()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_INT16));
        }

        public Int32 GetInt32()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_INT32));
        }

        public Int32 GetInt64()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_INT64));
        }
        #endregion

        #region Unsigend
        public Int32 GetByte()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_BYTE));
        }

        public Int32 GetUInt16()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_UINT16));
        }

        public Int32 GetUInt32()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_UINT32));
        }

        public Int32 GetUInt64()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_UINT64));
        }
        #endregion

        #region Array
        public Int32 GetArray()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_ARRAY));
        }

        public Int32 GetString()
        {
            return Write(CommandBuilder.Create(Commands.CMD_GET_STRING));
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
        public event StringReceived OnStringReceived;
        #endregion

        #region Error events.
        public event ErrorEvent OnError;
        #endregion
        #endregion
    }}
