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

            Proc.OnByteReceived += Proc_OnByteReceived;
            Proc.OnUInt16Received += Proc_OnUInt16Received;
            Proc.OnUInt32Received += Proc_OnUInt32Received;
            Proc.OnUInt64Received += Proc_OnUInt64Received;
            Proc.OnSByteReceived += Proc_OnSByteReceived;
            Proc.OnInt16Received += Proc_OnInt16Received;
            Proc.OnInt32Received += Proc_OnInt32Received;
            Proc.OnInt64Received += Proc_OnInt64Received;
            Proc.OnArrayReceived += Proc_OnArrayReceived;

            Proc.OnGetByte += Proc_OnGetByte;
            Proc.OnGetUIn16 += Proc_OnGetUIn16;
            Proc.OnGetUIn32 += Proc_OnGetUIn32;
            Proc.OnGetUInt64 += Proc_OnGetUInt64;
            Proc.OnGetSByte += Proc_OnGetSByte;
            Proc.OnGetInt16 += Proc_OnGetInt16;
            Proc.OnGetInt32 += Proc_OnGetInt32;
            Proc.OnGetInt64 += Proc_OnGetInt64;
            Proc.OnGetArray += Proc_OnGetArray;
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
        private void Proc_OnByteReceived(object Sender, byte Data)
        {
            Trace("Byte received: " + Data.ToString());
        }

        private void Proc_OnUInt16Received(Object Sender, UInt16 Data)
        {
            Trace("UInt16 received: " + Data.ToString());
        }

        private void Proc_OnUInt32Received(Object Sender, UInt32 Data)
        {
            Trace("UInt32 received: " + Data.ToString());
        }

        private void Proc_OnUInt64Received(Object Sender, UInt64 Data)
        {
            Trace("UInt64 received: " + Data.ToString());
        }

        private void Proc_OnSByteReceived(Object Sender, SByte Data)
        {
            Trace("SByte received: " + Data.ToString());
        }

        private void Proc_OnInt16Received(Object Sender, Int16 Data)
        {
            Trace("Int16 received: " + Data.ToString());
        }

        private void Proc_OnInt32Received(Object Sender, Int32 Data)
        {
            Trace("Int32 received: " + Data.ToString());
        }

        private void Proc_OnInt64Received(Object Sender, Int64 Data)
        {
            Trace("Int64 received: " + Data.ToString());
        }

        private void Proc_OnArrayReceived(Object Sender, Byte[] Data)
        {
            Trace("Array received: " + Data.Length.ToString());
        }
        #endregion

        #region Get data.
        private void Proc_OnGetByte(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((Byte)0x25);
        }

        private void Proc_OnGetUIn16(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((UInt16)0x2525);
        }

        private void Proc_OnGetUIn32(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((UInt32)0x25252525);
        }

        private void Proc_OnGetUInt64(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((UInt64)0x2525252525252525);
        }

        private void Proc_OnGetSByte(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((SByte)0x25);
        }

        private void Proc_OnGetInt16(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((Int16)0x2525);
        }

        private void Proc_OnGetInt32(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((Int32)0x25252525);
        }

        private void Proc_OnGetInt64(Object Sender)
        {
            ((ServerDataProcessor)Sender).WriteData((Int64)0x2525252525252525);
        }

        private void Proc_OnGetArray(Object Sender)
        {
            Byte[] Arr = new Byte[512];
            for (UInt16 i = 0; i < 511; i++)
                Arr[i] = wclHelpers.LoByte(i);
            Int32 Res = ((ServerDataProcessor)Sender).WriteData(Arr);
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Write failed", Res);
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
            FServer.Service = new Guid("{CA80C97C-06B3-4E65-9CEE-65BB0B11BC92}");
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
