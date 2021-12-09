using System;
using System.Windows.Forms;

using wclCommon;
using wclCommunication;
using wclBluetooth;

namespace RfCommServer
{
    public partial class fmServerMain : Form
    {
        private wclBluetoothManager FManager;
        private wclRfCommServer FServer;

        #region Helper methods.
        private void Trace(String str)
        {
            lbLog.Items.Add(str);
        }

        private void Trace(String str, Int32 Error)
        {
            Trace(str + ": 0x" + Error.ToString("X8"));
        }
        #endregion

        #region Bluetooth Manager events.
        private void FManager_BeforeClose(Object sender, EventArgs e)
        {
            Trace("Bluetooth Manager closed");
        }

        private void FManager_AfterOpen(Object sender, EventArgs e)
        {
            Trace("Bluetooth Manager opened.");
        }
        #endregion

        #region Server events.
        private void FServer_OnListen(Object sender, EventArgs e)
        {
            Trace("Server listening");
        }

        private void FServer_OnDisconnect(Object Sender, wclRfCommServerClientConnection Client, Int32 Reason)
        {
            Trace("Client disconnected", Reason);
        }

        private void FServer_OnDestroyProcessor(Object Sender, wclServerClientDataConnection Connection)
        {
            if (Connection.Processor != null)
            {
                Trace("Destroy data processor");
                Connection.Processor.Dispose();
            }
        }

        private void FServer_OnCreateProcessor(Object Sender, wclServerClientDataConnection Connection)
        {
            Trace("Creating data processor");
            ServerDataProcessor Proc = new ServerDataProcessor(Connection);

            Proc.OnByteReceived += ByteReceived;
            Proc.OnUInt16Received += UInt16Received;
            Proc.OnUInt32Received += UInt32Received;
            Proc.OnUInt64Received += UInt64Received;
            Proc.OnSByteReceived += SByteReceived;
            Proc.OnInt16Received += Int16Received;
            Proc.OnInt32Received += Int32Received;
            Proc.OnInt64Received += Int64Received;
            Proc.OnArrayReceived += ArrayReceived;
            Proc.OnStringReceived += StringReceived;

            Proc.OnGetByte += GetByte;
            Proc.OnGetUInt16 += GetUInt16;
            Proc.OnGetUInt32 += GetUInt32;
            Proc.OnGetUInt64 += GetUInt64;
            Proc.OnGetSByte += GetSByte;
            Proc.OnGetInt16 += GetInt16;
            Proc.OnGetInt32 += GetInt32;
            Proc.OnGetInt64 += GetInt64;
            Proc.OnGetArray += GetArray;
            Proc.OnGetString += GetString;
        }

        private void FServer_OnConnect(Object Sender, wclRfCommServerClientConnection Client, Int32 Error)
        {
            Trace("Client connect", Error);
        }

        private void FServer_OnClosed(Object Sender, Int32 Reason)
        {
            Trace("Server closed");
        }
        #endregion

        #region Data processor events.
        #region Data received.
        private void ByteReceived(Object Sender, Byte Data)
        {
            Trace("Byte received: " + Data.ToString("X2") + " (" + Data.ToString() + ")");
        }

        private void UInt16Received(Object Sender, UInt16 Data)
        {
            Trace("UInt16 received: " + Data.ToString("X4") + " (" + Data.ToString() + ")");
        }

        private void UInt32Received(Object Sender, UInt32 Data)
        {
            Trace("UInt32 received: " + Data.ToString("X8") + " (" + Data.ToString() + ")");
        }

        private void UInt64Received(Object Sender, UInt64 Data)
        {
            Trace("UInt64 received: " + Data.ToString("X16") + " (" + Data.ToString() + ")");
        }

        private void SByteReceived(Object Sender, SByte Data)
        {
            Trace("SByte received: " + Data.ToString("X2") + " (" + Data.ToString() + ")");
        }

        private void Int16Received(Object Sender, Int16 Data)
        {
            Trace("Int16 received: " + Data.ToString("X4") + " (" + Data.ToString() + ")");
        }

        private void Int32Received(Object Sender, Int32 Data)
        {
            Trace("Int32 received: " + Data.ToString("X8") + " (" + Data.ToString() + ")");
        }

        private void Int64Received(Object Sender, Int64 Data)
        {
            Trace("Int64 received: " + Data.ToString("X16") + " (" + Data.ToString() + ")");
        }

        private void ArrayReceived(Object Sender, Byte[] Data)
        {
            Trace("Array received: " + Data.Length.ToString());
            String Hex = BitConverter.ToString(Data);
            Hex = Hex.Replace("-", "");
            lbLog.Items.Add(Hex);
        }

        private void StringReceived(Object Sender, String Data)
        {
            Trace("String received: " + Data);
        }
        #endregion

        #region Get data.
        private void GetByte(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteByte(0xF5);
        }

        private void GetUInt16(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteUInt16(0xF551);
        }

        private void GetUInt32(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteUInt32(0xF5515253);
        }

        private void GetUInt64(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteUInt64(0xF551525354555657);
        }

        private void GetSByte(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteSByte(unchecked((SByte)0xF5));
        }

        private void GetInt16(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteInt16(unchecked((Int16)0xF551));
        }

        private void GetInt32(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteInt32(unchecked((Int32)0xF5515253));
        }

        private void GetInt64(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteInt64(unchecked((Int64)0xF551525354555657));
        }

        private void GetArray(Object Sender)
        {
            Byte[] Arr = new Byte[512];
            for (UInt16 i = 0; i < 512; i++)
                Arr[i] = wclHelpers.LoByte(i);
            Int32 Res = ((ServerDataProcessor)Sender).WriteArray(Arr);
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Write failed", Res);
        }

        private void GetString(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteString("Answer from server");
        }
        #endregion
        #endregion

        #region Form events.
        private void fmServerMain_Load(Object sender, EventArgs e)
        {
            FManager = new wclBluetoothManager();
            FManager.AfterOpen += FManager_AfterOpen;
            FManager.BeforeClose += FManager_BeforeClose;

            FServer = new wclRfCommServer();
            // Disable authentication and encryption.
            FServer.Authentication = false;
            FServer.Encryption = false;
            // Use custom service's UUID.
            FServer.Service = RfCommClientServer.Commands.ServiceUuid;
            FServer.OnClosed += FServer_OnClosed;
            FServer.OnConnect += FServer_OnConnect;
            FServer.OnCreateProcessor += FServer_OnCreateProcessor;
            FServer.OnDestroyProcessor += FServer_OnDestroyProcessor;
            FServer.OnDisconnect += FServer_OnDisconnect;
            FServer.OnListen += FServer_OnListen;

            Int32 Res = FManager.Open();
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Bluetooth Manager open failed", Res);
        }

        private void fmServerMain_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (FServer.State != wclCommunication.wclServerState.ssClosed)
                FServer.Close();
            FServer = null;

            if (FManager.Active)
                FManager.Close();
            FManager = null;
        }
        #endregion

        #region Buttons events.
        #region Clear
        private void btClear_Click(Object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }
        #endregion

        #region Server buttons
        private void btListen_Click(Object sender, EventArgs e)
        {
            wclBluetoothRadio Radio;
            Int32 Res = FManager.GetClassicRadio(out Radio);
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Get classic radio failed", Res);
            else
            {
                // Do not forget to switch to connectable and discoverable mode!
                Res = Radio.SetConnectable(true);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Set connectable failed", Res);
                Res = Radio.SetDiscoverable(true);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Set discoverable failed", Res);
                Res = FServer.Listen(Radio);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Listen failed", Res);
            }
        }

        private void btClose_Click(Object sender, EventArgs e)
        {
            Int32 Res = FServer.Close();
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Close failed", Res);
        }
        #endregion
        #endregion

        public fmServerMain()
        {
            InitializeComponent();
        }
    }
}
