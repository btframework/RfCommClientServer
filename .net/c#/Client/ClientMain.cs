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

        private void FClient_OnCreateProcessor(Object Sender, wclClientDataConnection Connection)
        {
            Trace("Creating data processor");
            ClientDataProcessor Proc = new ClientDataProcessor(Connection);

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

            Proc.OnError += ErrorReceived;
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

        #region Error received
        private void ErrorReceived(Object Sender, Int32 Error)
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
            FClient.Service = RfCommClientServer.Commands.ServiceUuid;
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
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteByte(0x11);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendUInt16_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteUInt16(0x1112);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendUInt32_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteUInt32(0x11121384);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendUInt64_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteUInt64(0x1112131415161718);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendSByte_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteSByte(unchecked((SByte)0xF8));
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendInt16_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteInt16(unchecked((Int16)0xF112));
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendInt32_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteInt32(unchecked((Int32)0xF1121314));
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendInt64_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteInt64(unchecked((Int64)0xF112131415161718));
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendArray_Click(Object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Byte[] Arr = new Byte[512];
                for (UInt16 i = 0; i < 512; i++)
                    Arr[i] = wclHelpers.LoByte(i);
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteArray(Arr);
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }

        private void btSendString_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).WriteString("Request from client");
                if (Res != wclErrors.WCL_E_SUCCESS)
                    Trace("Write failed", Res);
            }
        }
        #endregion

        #region Get data
        private void btGetByte_Click(Object sender, EventArgs e)
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

        private void btGetUInt16_Click(Object sender, EventArgs e)
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

        private void btGetUInt32_Click(Object sender, EventArgs e)
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

        private void btGetUInt64_Click(Object sender, EventArgs e)
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

        private void btGetSByte_Click(Object sender, EventArgs e)
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

        private void btGetInt16_Click(Object sender, EventArgs e)
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

        private void btGetInt32_Click(Object sender, EventArgs e)
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

        private void btGetInt64_Click(Object sender, EventArgs e)
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

        private void btGetArray_Click(Object sender, EventArgs e)
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

        private void btGetString_Click(object sender, EventArgs e)
        {
            if (FClient.Processor == null)
                Trace("Data processor not created");
            else
            {
                Int32 Res = ((ClientDataProcessor)FClient.Processor).GetString();
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
