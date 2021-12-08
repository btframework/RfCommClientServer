using System;
using System.Windows.Forms;

using wclCommon;
using wclCommunication;
using wclBluetooth;

namespace RfCommClient
{
    public partial class fmClientMain : Form
    {
        private wclBluetoothManager FManager;
        private wclRfCommClient FClient;

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

        private void FManager_OnDiscoveringStarted(Object Sender, wclBluetoothRadio Radio)
        {
            Trace("Discovering started");
            lvDevices.Items.Clear();
        }

        private void FManager_OnDeviceFound(Object Sender, wclBluetoothRadio Radio, Int64 Address)
        {
            lvDevices.Items.Add(Address.ToString("X12"));
        }

        private void FManager_OnDiscoveringCompleted(Object Sender, wclBluetoothRadio Radio, Int32 Error)
        {
            Trace("Discovering completed with result", Error);
            if (lvDevices.Items.Count == 0)
                Trace("No Bluetooth devices found");
            else
            {
                foreach (ListViewItem Item in lvDevices.Items)
                {
                    Int64 Mac = Convert.ToInt64(Item.Text, 16);
                    String Name;
                    Int32 Res = Radio.GetRemoteName(Mac, out Name);
                    if (Res != wclErrors.WCL_E_SUCCESS)
                        Name = "Error: 0x" + Res.ToString("X8");
                    Item.SubItems.Add(Name);
                }
            }
        }
        #endregion

        #region Client events.
        private void FClient_OnConnect(Object Sender, Int32 Error)
        {
            if (Error == wclErrors.WCL_E_SUCCESS)
                Trace("Client connected");
            else
                Trace("Connect failed", Error);
        }

        private void FClient_OnDisconnect(Object Sender, Int32 Reason)
        {
            Trace("Client disconnected", Reason);
        }

        private void FClient_OnData(Object Sender, Byte[] Data)
        {
            Trace("Data received: " + Data.Length);
        }

        private void FClient_OnCreateProcessor(Object Sender, wclClientDataConnection Connection)
        {
            Trace("Creating data processor");
            ClientDataProcessor Proc = new ClientDataProcessor(Connection);

            Proc.OnByteReceived += Proc_OnByteReceived;
            Proc.OnUInt16Received += Proc_OnUInt16Received;
            Proc.OnUInt32Received += Proc_OnUInt32Received;
            Proc.OnUInt64Received += Proc_OnUInt64Received;
            Proc.OnSByteReceived += Proc_OnSByteReceived;
            Proc.OnInt16Received += Proc_OnInt16Received;
            Proc.OnInt32Received += Proc_OnInt32Received;
            Proc.OnInt64Received += Proc_OnInt64Received;
            Proc.OnArrayReceived += Proc_OnArrayReceived;

            Proc.OnError += Proc_OnError;
        }

        private void FClient_OnDestroyProcessor(Object Sender, wclClientDataConnection Connection)
        {
            if (Connection.Processor != null)
            {
                Trace("Destroy data processor");
                Connection.Processor.Dispose();
            }
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

        #region Error received
        private void Proc_OnError(Object Sender, Int32 Error)
        {
            Trace("Error received", Error);
        }
        #endregion
        #endregion

        #region Form events.
        private void fmClientMain_Load(Object sender, EventArgs e)
        {
            FManager = new wclBluetoothManager();
            FManager.AfterOpen += FManager_AfterOpen;
            FManager.BeforeClose += FManager_BeforeClose;
            FManager.OnDiscoveringStarted += FManager_OnDiscoveringStarted;
            FManager.OnDeviceFound += FManager_OnDeviceFound;
            FManager.OnDiscoveringCompleted += FManager_OnDiscoveringCompleted;

            FClient = new wclRfCommClient();
            // Disable authentication and encryption.
            FClient.Authentication = false;
            FClient.Encryption = false;
            // Use custom service's UUID.
            FClient.Service = new Guid("{CA80C97C-06B3-4E65-9CEE-65BB0B11BC92}");
            FClient.OnConnect += FClient_OnConnect;
            FClient.OnDisconnect += FClient_OnDisconnect;
            FClient.OnCreateProcessor += FClient_OnCreateProcessor;
            FClient.OnDestroyProcessor += FClient_OnDestroyProcessor;

            Int32 Res = FManager.Open();
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Bluetooth Manager open failed", Res);
        }

        private void fmClientMain_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (FClient.State != wclClientState.csDisconnected)
                FClient.Disconnect();
            FClient = null;

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

        #region Discovering
        private void btDiscover_Click(Object sender, EventArgs e)
        {
            wclBluetoothRadio Radio;
            Int32 Res = FManager.GetClassicRadio(out Radio);
            if (Res != wclErrors.WCL_E_SUCCESS)
                Trace("Get classic radio failed", Res);
            else
            {
                Res = Radio.Discover(10, wclBluetoothDiscoverKind.dkClassic);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Start classic discovering failed", Res);
            }
        }
        #endregion

        #region Connection
        private void btConnect_Click(Object sender, EventArgs e)
        {
            if (FClient.State != wclClientState.csDisconnected)
                Trace("Client connected or connecting");
            else
            {
                if (lvDevices.SelectedItems == null || lvDevices.SelectedItems.Count == 0)
                    Trace("Select device");
                else
                {
                    wclBluetoothRadio Radio;
                    Int32 Res = FManager.GetClassicRadio(out Radio);
                    if (Res != wclErrors.WCL_E_SUCCESS)
                        Trace("Get classic radio failed", Res);
                    else
                    {
                        FClient.Address = Convert.ToInt64(lvDevices.SelectedItems[0].Text, 16);
                        Res = FClient.Connect(Radio);
                        if (Res != wclErrors.WCL_E_SUCCESS)
                            Trace("Connect failed", Res);
                    }
                }
            }
        }

        private void btDisconnect_Click(Object sender, EventArgs e)
        {
            if (FClient.State == wclClientState.csDisconnected)
                Trace("Client disconnected");
            else
            {
                Int32 Res = FClient.Disconnect();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Disconnect failed", Res);
            }
        }
        #endregion

        #region Send data
        private void btSendByte_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((Byte)0x81);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendUInt16_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((UInt16)0x8181);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendUInt32_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((UInt32)0x81818181);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendUInt64_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((UInt64)0x8181818181818181);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendSByte_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((SByte)0x18);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendInt16_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((Int16)0x1818);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendInt32_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((Int32)0x18181818);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendInt64_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData((Int64)0x1818181818181818);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendArray_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Byte[] Arr = new Byte[512];
                for (UInt16 i = 0; i < 511; i++)
                    Arr[i] = wclHelpers.LoByte(i);
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteData(Arr);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }
        #endregion

        #region Get data
        private void btGetByte_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetByte();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetUInt16_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetUInt16();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetUInt32_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetUInt32();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetUInt64_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetUInt64();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetSByte_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetSByte();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetInt16_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetInt16();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetInt32_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetInt32();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetInt64_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetInt64();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }

        private void btGetArray_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetArray();
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Get failed", Res);
            }
        }
        #endregion
        #endregion

        public fmClientMain()
        {
            InitializeComponent();
        }
    }
}
