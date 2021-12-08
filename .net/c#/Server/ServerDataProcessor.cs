using System;

using wclCommon;
using wclCommunication;

using RfCommClientServer;

namespace RfCommServer
{
    internal class ServerDataProcessor : wclCustomServerClientDataProcessor
    {
        private CommandDecoder FDecoder;

        #region Data decoder events.
        private void FDecoder_OnGetArray(Object Sender)
        {
            if (OnGetArray != null)
                OnGetArray(this);
        }

        private void FDecoder_OnGetInt64(Object Sender)
        {
            if (OnGetInt64 != null)
                OnGetInt64(this);
        }

        private void FDecoder_OnGetInt32(Object Sender)
        {
            if (OnGetInt32 != null)
                OnGetInt32(this);
        }

        private void FDecoder_OnGetInt16(Object Sender)
        {
            if (OnGetInt16 != null)
                OnGetInt16(this);
        }

        private void FDecoder_OnGetSByte(Object Sender)
        {
            if (OnGetSByte != null)
                OnGetSByte(this);
        }

        private void FDecoder_OnGetUInt64(Object Sender)
        {
            if (OnGetUInt64 != null)
                OnGetUInt64(this);
        }

        private void FDecoder_OnGetUIn32(Object Sender)
        {
            if (OnGetUIn32 != null)
                OnGetUIn32(this);
        }

        private void FDecoder_OnGetUIn16(Object Sender)
        {
            if (OnGetUIn16 != null)
                OnGetUIn16(this);
        }

        private void FDecoder_OnGetByte(Object Sender)
        {
            if (OnGetByte != null)
                OnGetByte(this);
        }

        private void FDecoder_OnGetString(Object Sender)
        {
            if (OnGetString != null)
                OnGetString(this);
        }

        private void FDecoder_OnArrayReceived(Object Sender, Byte[] Data)
        {
            if (OnArrayReceived != null)
                OnArrayReceived(this, Data);
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

        private void FDecoder_OnStringReceived(Object Sender, String Data)
        {
            if (OnStringReceived != null)
                OnStringReceived(this, Data);
        }
        #endregion

        protected override void ProcessData(Byte[] Data)
        {
            FDecoder.ProcessData(Data);
        }

        public ServerDataProcessor(wclServerClientDataConnection Connection)
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

            FDecoder.OnGetByte += FDecoder_OnGetByte;
            FDecoder.OnGetUIn16 += FDecoder_OnGetUIn16;
            FDecoder.OnGetUIn32 += FDecoder_OnGetUIn32;
            FDecoder.OnGetUInt64 += FDecoder_OnGetUInt64;
            FDecoder.OnGetSByte += FDecoder_OnGetSByte;
            FDecoder.OnGetInt16 += FDecoder_OnGetInt16;
            FDecoder.OnGetInt32 += FDecoder_OnGetInt32;
            FDecoder.OnGetInt64 += FDecoder_OnGetInt64;
            FDecoder.OnGetArray += FDecoder_OnGetArray;
            FDecoder.OnGetString += FDecoder_OnGetString;

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
            OnGetUIn16 = null;
            OnGetUIn32 = null;
            OnGetUInt64 = null;
            OnGetSByte = null;
            OnGetInt16 = null;
            OnGetInt32 = null;
            OnGetInt64 = null;
            OnGetArray = null;
            OnGetString = null;
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

        #region Send error
        public Int32 SendError(Int32 Error)
        {
            return Write(CommandBuilder.Create(Error));
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
        public event DataEvent OnGetString;
        #endregion 
        #endregion
    }
}
