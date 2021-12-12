unit ServerMain;

interface

uses
  Forms, Vcl.Controls, Vcl.StdCtrls, System.Classes, wclBluetooth,
  wclConnections, Common;

type
  TfmServerMain = class(TForm)
    btListen: TButton;
    btClose: TButton;
    lbLog: TListBox;
    btClear: TButton;
    wclBluetoothManager: TwclBluetoothManager;
    wclRfCommServer: TwclRfCommServer;
    procedure wclBluetoothManagerBeforeClose(Sender: TObject);
    procedure wclBluetoothManagerAfterOpen(Sender: TObject);
    procedure wclRfCommServerListen(Sender: TObject);
    procedure wclRfCommServerDisconnect(Sender: TObject;
      const Client: TwclRfCommServerClientConnection; const Reason: Integer);
    procedure wclRfCommServerDestroyProcessor(Sender: TObject;
      const Connection: TwclServerClientDataConnection);
    procedure wclRfCommServerCreateProcessor(Sender: TObject;
      const Connection: TwclServerClientDataConnection);
    procedure wclRfCommServerConnect(Sender: TObject;
      const Client: TwclRfCommServerClientConnection; const Error: Integer);
    procedure wclRfCommServerClosed(Sender: TObject; const Reason: Integer);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure btClearClick(Sender: TObject);
    procedure btListenClick(Sender: TObject);
    procedure btCloseClick(Sender: TObject);

  private
    procedure Trace(const str: string); overload;
    procedure Trace(const str: string; const Error: Integer); overload;

    procedure ByteReceived(Sender: TObject; const Data: Byte);
    procedure UInt16Received(Sender: TObject; const Data: UInt16);
    procedure UInt32Received(Sender: TObject; const Data: UInt32);
    procedure UInt64Received(Sender: TObject; const Data: UInt64);
    procedure SByteReceived(Sender: TObject; const Data: Int8);
    procedure Int16Received(Sender: TObject; const Data: Int16);
    procedure Int32Received(Sender: TObject; const Data: Int32);
    procedure Int64Received(Sender: TObject; const Data: Int64);
    procedure ArrayReceived(Sender: TObject; const Data: TBytesArray);
    procedure StringReceived(Sender: TObject; const Data: string);

    procedure GetByte(Sender: TObject);
    procedure GetUInt16(Sender: TObject);
    procedure GetUInt32(Sender: TObject);
    procedure GetUInt64(Sender: TObject);
    procedure GetSByte(Sender: TObject);
    procedure GetInt16(Sender: TObject);
    procedure GetInt32(Sender: TObject);
    procedure GetInt64(Sender: TObject);
    procedure GetArray(Sender: TObject);
    procedure GetString(Sender: TObject);
  end;

var
  fmServerMain: TfmServerMain;

implementation

uses
  SysUtils, ServerDataProcessor, Windows, wclErrors;

{$R *.dfm}

{ TfmServerMain }

procedure TfmServerMain.Trace(const str: string);
begin
  lbLog.Items.Add(str);
end;

procedure TfmServerMain.ArrayReceived(Sender: TObject; const Data: TBytesArray);
var
  Hex: string;
  b: Byte;
begin
  Trace('Array received: ' + Length(Data).ToString);
  Hex := '';
  for b in Data do
    Hex := Hex + IntToHex(b, 2);
  lbLog.Items.Add(Hex);
end;

procedure TfmServerMain.btClearClick(Sender: TObject);
begin
  lbLog.Items.Clear();
end;

procedure TfmServerMain.btCloseClick(Sender: TObject);
var
  Res: Integer;
begin
  Res := wclRfCommServer.Close;
  if Res <> WCL_E_SUCCESS then
    Trace('Close failed', Res);
end;

procedure TfmServerMain.btListenClick(Sender: TObject);
var
  Radio: TwclBluetoothRadio;
  Res: Integer;
begin
  Res := wclBluetoothManager.GetClassicRadio(Radio);
  if Res <> WCL_E_SUCCESS then
    Trace('Get classic radio failed', Res)
  else begin
    // Do not forget to switch to connectable and discoverable mode!
    Res := Radio.SetConnectable(True);
    if Res <> WCL_E_SUCCESS then
      Trace('Set connectable failed', Res);
    Res := Radio.SetDiscoverable(True);
    if Res <> WCL_E_SUCCESS then
      Trace('Set discoverable failed', Res);
    Res := wclRfCommServer.Listen(Radio);
    if Res <> WCL_E_SUCCESS then
      Trace('Listen failed', Res);
  end;
end;

procedure TfmServerMain.ByteReceived(Sender: TObject; const Data: Byte);
begin
  Trace('Byte received: ' + IntToHex(Data, 2) + ' (' + Data.ToString + ')');
end;

procedure TfmServerMain.FormCreate(Sender: TObject);
var
  Res: Integer;
begin
  wclRfCommServer.Service := ServiceUuid;

  Res := wclBluetoothManager.Open;
  if Res <> WCL_E_SUCCESS then
    Trace('Bluetooth Manager open failed', Res);
end;

procedure TfmServerMain.FormDestroy(Sender: TObject);
begin
  if wclRfCommServer.State <> ssClosed then
    wclRfCommServer.Close;

  if wclBluetoothManager.Active then
    wclBluetoothManager.Close;
end;

procedure TfmServerMain.GetArray(Sender: TObject);
var
  Arr: TBytesArray;
  i: UInt16;
  Res: Integer;
begin
  SetLength(Arr, 512);
  for i := 0 to 511 do
    Arr[i] := LOBYTE(i);
  Res := TServerDataProcessor(Sender).WriteArray(Arr);
  if Res <> WCL_E_SUCCESS then
    Trace('Write failed', Res);
end;

procedure TfmServerMain.GetByte(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteByte($F5);
end;

procedure TfmServerMain.GetInt16(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteInt16(Int16($F551));
end;

procedure TfmServerMain.GetInt32(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteInt32(Int32($F5515253));
end;

procedure TfmServerMain.GetInt64(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteInt64(Int64($F551525354555657));
end;

procedure TfmServerMain.GetSByte(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteSByte(Int8($F5));
end;

procedure TfmServerMain.GetString(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteString('Answer from server');
end;

procedure TfmServerMain.GetUInt16(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteUInt16($F551);
end;

procedure TfmServerMain.GetUInt32(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteUInt32($F5515253);
end;

procedure TfmServerMain.GetUInt64(Sender: TObject);
begin
  TServerDataProcessor(Sender).WriteUInt64($F551525354555657);
end;

procedure TfmServerMain.Int16Received(Sender: TObject; const Data: Int16);
begin
  Trace('Int16 received: ' + IntToHex(Word(Data), 4) + ' (' +
    Data.ToString() + ')');
end;

procedure TfmServerMain.Int32Received(Sender: TObject; const Data: Int32);
begin
  Trace('Int32 received: ' + IntToHex(Data, 8) + ' (' + Data.ToString + ')');
end;

procedure TfmServerMain.Int64Received(Sender: TObject; const Data: Int64);
begin
  Trace('Int64 received: ' + IntToHex(Data, 6) + ' (' + Data.ToString + ')');
end;

procedure TfmServerMain.SByteReceived(Sender: TObject; const Data: Int8);
begin
  Trace('SByte received: ' + IntToHex(Byte(Data), 2) + ' (' +
    Data.ToString + ')');
end;

procedure TfmServerMain.StringReceived(Sender: TObject; const Data: string);
begin
  Trace('String received: ' + Data);
end;

procedure TfmServerMain.Trace(const str: string; const Error: Integer);
begin
  Trace(str + ': 0x' + IntToHex(Error, 8));
end;

procedure TfmServerMain.UInt16Received(Sender: TObject; const Data: UInt16);
begin
  Trace('UInt16 received: ' + IntToHex(Data, 4) + ' (' + Data.ToString + ')');
end;

procedure TfmServerMain.UInt32Received(Sender: TObject; const Data: UInt32);
begin
  Trace('UInt32 received: ' + IntToHex(Data, 8) + ' (' + Data.ToString + ')');
end;

procedure TfmServerMain.UInt64Received(Sender: TObject; const Data: UInt64);
begin
  Trace('UInt64 received: ' + IntToHex(Data, 16) + ' (' + Data.ToString + ')');
end;

procedure TfmServerMain.wclBluetoothManagerAfterOpen(Sender: TObject);
begin
  Trace('Bluetooth Manager opened.');
end;

procedure TfmServerMain.wclBluetoothManagerBeforeClose(Sender: TObject);
begin
  Trace('Bluetooth Manager closed');
end;

procedure TfmServerMain.wclRfCommServerClosed(Sender: TObject;
  const Reason: Integer);
begin
  Trace('Server closed');
end;

procedure TfmServerMain.wclRfCommServerConnect(Sender: TObject;
  const Client: TwclRfCommServerClientConnection; const Error: Integer);
begin
  Trace('Client connect', Error);
end;

procedure TfmServerMain.wclRfCommServerCreateProcessor(Sender: TObject;
  const Connection: TwclServerClientDataConnection);
var
  Proc: TServerDataProcessor;
begin
  Trace('Creating data processor');
  Proc := TServerDataProcessor.Create(Connection);

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

  Proc.OnGetByte := GetByte;
  Proc.OnGetUInt16 := GetUInt16;
  Proc.OnGetUInt32 := GetUInt32;
  Proc.OnGetUInt64 := GetUInt64;
  Proc.OnGetSByte := GetSByte;
  Proc.OnGetInt16 := GetInt16;
  Proc.OnGetInt32 := GetInt32;
  Proc.OnGetInt64 := GetInt64;
  Proc.OnGetArray := GetArray;
  Proc.OnGetString := GetString;
end;

procedure TfmServerMain.wclRfCommServerDestroyProcessor(Sender: TObject;
  const Connection: TwclServerClientDataConnection);
begin
  if Connection.Processor <> nil then begin
    Trace('Destroy data processor');
    Connection.Processor.Free;
  end;
end;

procedure TfmServerMain.wclRfCommServerDisconnect(Sender: TObject;
  const Client: TwclRfCommServerClientConnection; const Reason: Integer);
begin
  Trace('Client disconnected', Reason);
end;

procedure TfmServerMain.wclRfCommServerListen(Sender: TObject);
begin
  Trace('Server listening');
end;

end.
