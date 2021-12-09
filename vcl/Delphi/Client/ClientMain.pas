unit ClientMain;

interface

uses
  Forms, StdCtrls, Controls, ComCtrls, Classes, wclBluetooth, Common,
  wclConnections;

type
  TfmClientMain = class(TForm)
    btDiscover: TButton;
    lvDevices: TListView;
    btSendByte: TButton;
    btSendUInt16: TButton;
    btSendUint32: TButton;
    btSendUInt64: TButton;
    btSendArray: TButton;
    btSendSByte: TButton;
    btSendInt16: TButton;
    btSendInt32: TButton;
    btSendInt64: TButton;
    btSendString: TButton;
    btGetByte: TButton;
    btGetUInt16: TButton;
    btGetUInt32: TButton;
    btGetUInt64: TButton;
    btGetArray: TButton;
    btGetSByte: TButton;
    btGetInt16: TButton;
    btGetInt32: TButton;
    btGetInt64: TButton;
    btGetString: TButton;
    btClear: TButton;
    lbLog: TListBox;
    wclBluetoothManager: TwclBluetoothManager;
    wclRfCommClient: TwclRfCommClient;
    btConnect: TButton;
    btDisconnect: TButton;
    procedure wclBluetoothManagerBeforeClose(Sender: TObject);
    procedure wclBluetoothManagerAfterOpen(Sender: TObject);
    procedure wclBluetoothManagerDiscoveringStarted(Sender: TObject;
      const Radio: TwclBluetoothRadio);
    procedure wclBluetoothManagerDeviceFound(Sender: TObject;
      const Radio: TwclBluetoothRadio; const Address: Int64);
    procedure wclBluetoothManagerDiscoveringCompleted(Sender: TObject;
      const Radio: TwclBluetoothRadio; const Error: Integer);
    procedure wclRfCommClientConnect(Sender: TObject;
      const Error: Integer);
    procedure wclRfCommClientDisconnect(Sender: TObject;
      const Reason: Integer);
    procedure wclRfCommClientCreateProcessor(Sender: TObject;
      const Connection: TwclClientDataConnection);
    procedure wclRfCommClientDestroyProcessor(Sender: TObject;
      const Connection: TwclClientDataConnection);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure btClearClick(Sender: TObject);
    procedure btDiscoverClick(Sender: TObject);
    procedure btConnectClick(Sender: TObject);
    procedure btDisconnectClick(Sender: TObject);
    procedure btSendByteClick(Sender: TObject);
    procedure btSendUInt16Click(Sender: TObject);
    procedure btSendUint32Click(Sender: TObject);
    procedure btSendUInt64Click(Sender: TObject);
    procedure btSendSByteClick(Sender: TObject);
    procedure btSendInt16Click(Sender: TObject);
    procedure btSendInt32Click(Sender: TObject);
    procedure btSendInt64Click(Sender: TObject);
    procedure btSendArrayClick(Sender: TObject);
    procedure btSendStringClick(Sender: TObject);
    procedure btGetByteClick(Sender: TObject);
    procedure btGetUInt16Click(Sender: TObject);
    procedure btGetUInt32Click(Sender: TObject);
    procedure btGetUInt64Click(Sender: TObject);
    procedure btGetSByteClick(Sender: TObject);
    procedure btGetInt16Click(Sender: TObject);
    procedure btGetInt32Click(Sender: TObject);
    procedure btGetInt64Click(Sender: TObject);
    procedure btGetArrayClick(Sender: TObject);
    procedure btGetStringClick(Sender: TObject);

  private
    procedure Trace(const str: string); overload;
    procedure Trace(const str: string; const Error: Int32); overload;

    procedure ByteReceived(Sender: TObject; const Data: Byte);
    procedure UInt16Received(Sender: TObject; const Data: UInt16);
    procedure UInt32Received(Sender: TObject; const Data: UInt32);
    procedure UInt64Received(Sender: TObject; const Data: UInt64);
    procedure SByteReceived(Sender: TObject; const Data: SByte);
    procedure Int16Received(Sender: TObject; const Data: Int16);
    procedure Int32Received(Sender: TObject; const Data: Int32);
    procedure Int64Received(Sender: TObject; const Data: Int64);
    procedure ArrayReceived(Sender: TObject; const Data: TByteArray);
    procedure StringReceived(Sender: TObject; const Data: string);

    procedure ErrorReceived(Sender: TObject; const Error: Int32);
  end;

var
  fmClientMain: TfmClientMain;

implementation

uses
  SysUtils, wclErrors, ClientDataProcessor, Windows;

{$R *.dfm}

{ TfmClientMain }

procedure TfmClientMain.Trace(const str: string);
begin
  lbLog.Items.Add(str);
end;

procedure TfmClientMain.Trace(const str: string; const Error: Int32);
begin
  Trace(str + ': 0x' + IntToHex(Error, 8));
end;

procedure TfmClientMain.wclBluetoothManagerBeforeClose(Sender: TObject);
begin
  Trace('Bluetooth Manager closed');
end;

procedure TfmClientMain.wclBluetoothManagerAfterOpen(Sender: TObject);
begin
  Trace('Bluetooth Manager opened.');
end;

procedure TfmClientMain.wclBluetoothManagerDiscoveringStarted(
  Sender: TObject; const Radio: TwclBluetoothRadio);
begin
  Trace('Discovering started');
  lvDevices.Items.Clear;
end;

procedure TfmClientMain.wclBluetoothManagerDeviceFound(Sender: TObject;
  const Radio: TwclBluetoothRadio; const Address: Int64);
var
  Item: TListItem;
begin
  Item := lvDevices.Items.Add;
  Item.Caption := IntToHex(Address, 12);
end;

procedure TfmClientMain.wclBluetoothManagerDiscoveringCompleted(
  Sender: TObject; const Radio: TwclBluetoothRadio; const Error: Integer);
var
  i: Integer;
  Item: TListItem;
  Mac: Int64;
  Name: string;
  Res: Integer;
begin
  Trace('Discovering completed with result', Error);
  if lvDevices.Selected = nil then
    Trace('No Bluetooth devices found')
  else begin
    for i := 0 to lvDevices.Items.Count - 1 do begin
      Item := lvDevices.Items[i];
      Mac := StrToInt64('$' + Item.Caption);
      Res := Radio.GetRemoteName(Mac, Name);
      if Res <> WCL_E_SUCCESS then
        Name :=  'Error: 0x' + IntToHex(Res, 8);
      Item.SubItems.Add(Name);
    end;
  end;
end;

procedure TfmClientMain.wclRfCommClientConnect(Sender: TObject;
  const Error: Integer);
begin
  if Error = WCL_E_SUCCESS then
    Trace('Client connected')
  else
    Trace('Connect failed', Error);
end;

procedure TfmClientMain.wclRfCommClientDisconnect(Sender: TObject;
  const Reason: Integer);
begin
  Trace('Client disconnected', Reason);
end;

procedure TfmClientMain.wclRfCommClientCreateProcessor(Sender: TObject;
  const Connection: TwclClientDataConnection);
var
  Proc: TClientDataProcessor;
begin
  Trace('Creating data processor');
  Proc := TClientDataProcessor.Create(Connection);

  Proc.OnByteReceived := ByteReceived;
  Proc.OnUInt16Received := UInt16Received;
  Proc.OnUInt32Received := UInt32Received;
  Proc.OnUInt64Received := UInt64Received;
  Proc.OnSByteReceived := SByteReceived;
  Proc.OnInt16Received := Int16Received;
  Proc.OnInt32Received := Int32Received;
  Proc.OnInt64Received := Int64Received;
  Proc.OnArrayReceived := ArrayReceived;
  Proc.OnStringReceived := StringReceived;

  Proc.OnError := ErrorReceived;
end;

procedure TfmClientMain.wclRfCommClientDestroyProcessor(Sender: TObject;
  const Connection: TwclClientDataConnection);
begin
  if Connection.Processor <> nil then begin
    Trace('Destroy data processor');
    Connection.Processor.Free;
  end;
end;

procedure TfmClientMain.ByteReceived(Sender: TObject; const Data: Byte);
begin
  Trace('Byte received: ' + IntToHex(Data, 2) + ' (' + IntToStr(Data));
end;

procedure TfmClientMain.UInt16Received(Sender: TObject;
  const Data: UInt16);
begin
  Trace('UInt16 received: ' + IntToHex(Data, 4) + ' (' + IntToStr(Data));
end;

procedure TfmClientMain.UInt32Received(Sender: TObject;
  const Data: UInt32);
begin
  Trace('UInt32 received: ' + IntToHex(Data, 8) + ' (' + IntToStr(Data));
end;

procedure TfmClientMain.UInt64Received(Sender: TObject;
  const Data: UInt64);
begin
  Trace('UInt64 received: ' + IntToHex(Data, 16) + ' (' + IntToStr(Data) + ')');
end;

procedure TfmClientMain.SByteReceived(Sender: TObject; const Data: SByte);
begin
  Trace('SByte received: ' + IntToHex(Data, 2) + ' (' + IntToStr(Data) + ')');
end;

procedure TfmClientMain.Int16Received(Sender: TObject; const Data: Int16);
begin
  Trace('Int16 received: ' + IntToHex(Data, 4) + ' (' + IntToStr(Data) + ')');
end;

procedure TfmClientMain.Int32Received(Sender: TObject; const Data: Int32);
begin
  Trace('Int32 received: ' + IntToHex(Data, 8) + ' (' + IntToStr(Data) + ')');
end;

procedure TfmClientMain.Int64Received(Sender: TObject; const Data: Int64);
begin
  Trace('Int64 received: ' + IntToHex(Data, 16) + ' (' + IntToStr(Data) + ')');
end;

procedure TfmClientMain.ArrayReceived(Sender: TObject;
  const Data: Common.TByteArray);
var
  Hex: string;
  i: Integer;
begin
  Trace('Array received: ' + IntToStr(Length(Data)));
  Hex := '';
  for i := 0 to Length(Data) - 1 do
    Hex := Hex + IntToHex(Data[i], 2);
  lbLog.Items.Add(Hex);
end;

procedure TfmClientMain.StringReceived(Sender: TObject;
  const Data: string);
begin
  Trace('String received: ' + Data);
end;

procedure TfmClientMain.ErrorReceived(Sender: TObject; const Error: Int32);
begin
  Trace('Error received', Error);
end;

procedure TfmClientMain.FormCreate(Sender: TObject);
var
  Res: Integer;
begin
  wclRfCommClient.Service := ServiceUuid;

  Res := wclBluetoothManager.Open;
  if Res <> WCL_E_SUCCESS then
    Trace('Bluetooth Manager open failed', Res);
end;

procedure TfmClientMain.FormDestroy(Sender: TObject);
begin
  if wclRfCommClient.State <> csDisconnected then
    wclRfCommClient.Disconnect;

  if wclBluetoothManager.Active then
    wclBluetoothManager.Close;
end;

procedure TfmClientMain.btClearClick(Sender: TObject);
begin
  lbLog.Items.Clear;
end;

procedure TfmClientMain.btDiscoverClick(Sender: TObject);
var
  Radio: TwclBluetoothRadio;
  Res: Integer;
begin
  Res := wclBluetoothManager.GetClassicRadio(Radio);
  if Res <> WCL_E_SUCCESS then
    Trace('Get classic radio failed', Res)
  else begin
    Res := Radio.Discover(10, dkClassic);
    if Res <> WCL_E_SUCCESS then
      Trace('Start classic discovering failed', Res);
  end;
end;

procedure TfmClientMain.btGetArrayClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetArray;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetByteClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetByte;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetInt16Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetInt16;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetInt32Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetInt32;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetInt64Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetInt64;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetSByteClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetSByte;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetStringClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetString;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetUInt16Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetUInt16;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetUInt32Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetUInt32;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btGetUInt64Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).GetUInt64;
    if Res <> WCL_E_SUCCESS then
      Trace('Get failed', Res);
  end;
end;

procedure TfmClientMain.btConnectClick(Sender: TObject);
var
  Radio: TwclBluetoothRadio;
  Res: Integer;
begin
  if wclRfCommClient.State <> csDisconnected then
    Trace('Client connected or connecting')
  else begin
    if lvDevices.Selected = nil then
      Trace('Select device')
    else begin
      Res := wclBluetoothManager.GetClassicRadio(Radio);
      if Res <> WCL_E_SUCCESS then
        Trace('Get classic radio failed', Res)
      else begin
        wclRfCommClient.Address := StrToInt64('$' + lvDevices.Selected.Caption);
        Res := wclRfCommClient.Connect(Radio);
        if Res <> WCL_E_SUCCESS then
          Trace('Connect failed', Res);
      end;
    end;
  end;
end;

procedure TfmClientMain.btDisconnectClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.State = csDisconnected then
    Trace('Client disconnected')
  else begin
    Res := wclRfCommClient.Disconnect;
    if Res <> WCL_E_SUCCESS then
      Trace('Disconnect failed', Res);
  end;
end;

procedure TfmClientMain.btSendArrayClick(Sender: TObject);
var
  Arr: Common.TByteArray;
  i: UInt16;
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    SetLength(Arr, 512);
    for i := 0 to 511 do
      Arr[i] := LOBYTE(i);
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(Arr);
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendByteClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(Byte($11));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendInt16Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(Int16($F112));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendInt32Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(Int32($F1121314));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendInt64Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(Int64($F112131415161718));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendSByteClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(SByte($F8));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendStringClick(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData('Request from client');
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendUInt16Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(UInt16($1112));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendUint32Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(UInt32($11121384));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

procedure TfmClientMain.btSendUInt64Click(Sender: TObject);
var
  Res: Integer;
begin
  if wclRfCommClient.Processor = nil then
    Trace('Data processor not created')
  else begin
    Res := TClientDataProcessor(wclRfCommClient.Processor).WriteData(UInt64($1112131415161718));
    if Res <> WCL_E_SUCCESS then
      Trace('Write failed', Res);
  end;
end;

end.
