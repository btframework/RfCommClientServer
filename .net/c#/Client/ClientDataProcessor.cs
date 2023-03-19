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
        private void ArrayReceived(Object Sender, Byte[] Data)
        {
            if (OnArrayReceived != null)
                OnArrayReceived(this, Data);
        }

        private void StringReceived(Object Sender, String Data)
        {
            if (OnStringReceived != null)
                OnStringReceived(this, Data);
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

        private void ErrorReceived(Object Sender, Int32 Error)
        {
            if (OnError != null)
                OnError(this, Error);
        }
        #endregion

        public override void ProcessData(Byte[] Data)
        {
            FDecoder.ProcessData(Data);
        }

        public ClientDataProcessor(wclClientDataConnection Connection)
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

            FDecoder.OnError += ErrorReceived;

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
        #endregion

        #region Get data
        public Int32 GetByte()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_BYTE));
        }

        public Int32 GetSByte()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_SBYTE));
        }

        public Int32 GetUInt16()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_UINT16));
        }

        public Int32 GetInt16()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_INT16));
        }

        public Int32 GetUInt32()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_UINT32));
        }

        public Int32 GetInt32()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_INT32));
        }

        public Int32 GetUInt64()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_UINT64));
        }

        public Int32 GetInt64()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_INT64));
        }
        
        public Int32 GetArray()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_ARRAY));
        }

        public Int32 GetString()
        {
            return Write(CommandBuilder.CreateGet(Commands.CMD_GET_STRING));
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

        #region Error events.
        public event ErrorEvent OnError;
        #endregion
        #endregion
    }
}
