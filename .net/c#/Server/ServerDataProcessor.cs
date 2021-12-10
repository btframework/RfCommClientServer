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
        private void GetArray(Object Sender)
        {
            if (OnGetArray != null)
                OnGetArray(this);
        }

        private void GetInt64(Object Sender)
        {
            if (OnGetInt64 != null)
                OnGetInt64(this);
        }

        private void GetInt32(Object Sender)
        {
            if (OnGetInt32 != null)
                OnGetInt32(this);
        }

        private void GetInt16(Object Sender)
        {
            if (OnGetInt16 != null)
                OnGetInt16(this);
        }

        private void GetSByte(Object Sender)
        {
            if (OnGetSByte != null)
                OnGetSByte(this);
        }

        private void GetUInt64(Object Sender)
        {
            if (OnGetUInt64 != null)
                OnGetUInt64(this);
        }

        private void GetUInt32(Object Sender)
        {
            if (OnGetUInt32 != null)
                OnGetUInt32(this);
        }

        private void GetUInt16(Object Sender)
        {
            if (OnGetUInt16 != null)
                OnGetUInt16(this);
        }

        private void GetByte(Object Sender)
        {
            if (OnGetByte != null)
                OnGetByte(this);
        }

        private void GetString(Object Sender)
        {
            if (OnGetString != null)
                OnGetString(this);
        }

        private void ArrayReceived(Object Sender, Byte[] Data)
        {
            if (OnArrayReceived != null)
                OnArrayReceived(this, Data);
        }

        private void Int64Received(Object Sender, Int64 Data)
        {
            if (OnInt64Received != null)
                OnInt64Received(this, Data);
        }

        private void Int32Received(Object Sender, Int32 Data)
        {
            if (OnInt32Received != null)
                OnInt32Received(this, Data);
        }

        private void Int16Received(Object Sender, Int16 Data)
        {
            if (OnInt16Received != null)
                OnInt16Received(this, Data);
        }

        private void SByteReceived(Object Sender, SByte Data)
        {
            if (OnSByteReceived != null)
                OnSByteReceived(this, Data);
        }

        private void UInt64Received(Object Sender, UInt64 Data)
        {
            if (OnUInt64Received != null)
                OnUInt64Received(this, Data);
        }

        private void UInt32Received(Object Sender, UInt32 Data)
        {
            if (OnUInt32Received != null)
                OnUInt32Received(this, Data);
        }

        private void UInt16Received(Object Sender, UInt16 Data)
        {
            if (OnUInt16Received != null)
                OnUInt16Received(this, Data);
        }

        private void ByteReceived(Object Sender, Byte Data)
        {
            if (OnByteReceived != null)
                OnByteReceived(this, Data);
        }

        private void StringReceived(Object Sender, String Data)
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

            FDecoder.OnByteReceived += ByteReceived;
            FDecoder.OnUInt16Received += UInt16Received;
            FDecoder.OnUInt32Received += UInt32Received;
            FDecoder.OnUInt64Received += UInt64Received;
            FDecoder.OnSByteReceived += SByteReceived;
            FDecoder.OnInt16Received += Int16Received;
            FDecoder.OnInt32Received += Int32Received;
            FDecoder.OnInt64Received += Int64Received;
            FDecoder.OnArrayReceived += ArrayReceived;
            FDecoder.OnStringReceived += StringReceived;

            FDecoder.OnGetByte += GetByte;
            FDecoder.OnGetUInt16 += GetUInt16;
            FDecoder.OnGetUInt32 += GetUInt32;
            FDecoder.OnGetUInt64 += GetUInt64;
            FDecoder.OnGetSByte += GetSByte;
            FDecoder.OnGetInt16 += GetInt16;
            FDecoder.OnGetInt32 += GetInt32;
            FDecoder.OnGetInt64 += GetInt64;
            FDecoder.OnGetArray += GetArray;
            FDecoder.OnGetString += GetString;

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
        }

        #region Write data
        public Int32 WriteByte(Byte Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteSByte(SByte Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteUInt16(UInt16 Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteInt16(Int16 Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteUInt32(UInt32 Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteInt32(Int32 Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteUInt64(UInt64 Data)
        {
            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteInt64(Int64 Data)
        {
            return Write(CommandBuilder.Create(Data));
        }
        
        public Int32 WriteArray(Byte[] Data)
        {
            if (Data == null || Data.Length == 0 || (UInt32)Data.Length > UInt16.MaxValue - 3)
                return wclErrors.WCL_E_INVALID_ARGUMENT;

            return Write(CommandBuilder.Create(Data));
        }

        public Int32 WriteString(String Data)
        {
            if (Data == null || Data.Length == 0 || (UInt32)Data.Length > UInt16.MaxValue - 3)
                return wclErrors.WCL_E_INVALID_ARGUMENT;

            return Write(CommandBuilder.Create(Data));
        }
        
        public Int32 WriteError(Int32 Error)
        {
            return Write(CommandBuilder.CreateError(Error));
        }
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
        public event DataEvent OnGetUInt16;
        public event DataEvent OnGetUInt32;
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
