unit ServerDataProcessor;

interface

uses
  wclConnections, Common;

type
  TServerDataProcessor = class(TwclCustomServerClientDataProcessor)
  private
    FDecoder: TCommandDecoder;

    FOnByteReceived: TByteReceived;
    FOnUInt16Received: TUInt16Received;
    FOnUInt32Received: TUInt32Received;
    FOnUInt64Received: TUInt64Received;
    FOnSByteReceived: TSByteReceived;
    FOnInt16Received: TInt16Received;
    FOnInt32Received: TInt32Received;
    FOnInt64Received: TInt64Received;
    FOnArrayReceived: TArrayReceived;
    FOnStringReceived: TStringReceived;

    FOnGetByte: TDataEvent;
    FOnGetUInt16: TDataEvent;
    FOnGetUInt32: TDataEvent;
    FOnGetUInt64: TDataEvent;
    FOnGetSByte: TDataEvent;
    FOnGetInt16: TDataEvent;
    FOnGetInt32: TDataEvent;
    FOnGetInt64: TDataEvent;
    FOnGetArray: TDataEvent;
    FOnGetString: TDataEvent;

    procedure GetByte(Sender: TObject);
    procedure GetSByte(Sender: TObject);
    procedure GetUInt16(Sender: TObject);
    procedure GetInt16(Sender: TObject);
    procedure GetUInt32(Sender: TObject);
    procedure GetInt32(Sender: TObject);
    procedure GetUInt64(Sender: TObject);
    procedure GetInt64(Sender: TObject);
    procedure GetArray(Sender: TObject);
    procedure GetString(Sender: TObject);

    procedure ByteReceived(Sender: TObject; const Data: Byte);
    procedure SByteReceived(Sender: TObject; const Data: Int8);
    procedure UInt16Received(Sender: TObject; const Data: UInt16);
    procedure Int16Received(Sender: TObject; const Data: Int16);
    procedure UInt32Received(Sender: TObject; const Data: UInt32);
    procedure Int32Received(Sender: TObject; const Data: Int32);
    procedure UInt64Received(Sender: TObject; const Data: UInt64);
    procedure Int64Received(Sender: TObject; const Data: Int64);
    procedure ArrayReceived(Sender: TObject; const Data: TBytesArray);
    procedure StringReceived(Sender: TObject; const Data: string);

    function Write(const Data: TBytesArray): Integer;

  protected
    procedure ProcessData(const Data: Pointer; const Size: Cardinal); override;

  public
    constructor Create(
      const Connection: TwclServerClientDataConnection); override;
    destructor Destroy; override;

    function WriteByte(const Data: Byte): Integer;
    function WriteSByte(const Data: Int8): Integer;
    function WriteUInt16(const Data: UInt16): Integer;
    function WriteInt16(const Data: Int16): Integer;
    function WriteUInt32(const Data: UInt32): Integer;
    function WriteInt32(const Data: Int32): Integer;
    function WriteUInt64(const Data: UInt64): Integer;
    function WriteInt64(const Data: Int64): Integer;
    function WriteArray(const Data: TBytesArray): Integer;
    function WriteString(const Data: string): Integer;
    function WriteError(const Error: Integer): Integer;

  public
    property OnByteReceived: TByteReceived read FOnByteReceived write FOnByteReceived;
    property OnUInt16Received: TUInt16Received read FOnUInt16Received write FOnUInt16Received;
    property OnUInt32Received: TUInt32Received read FOnUInt32Received write FOnUInt32Received;
    property OnUInt64Received: TUInt64Received read FOnUInt64Received write FOnUInt64Received;
    property OnSByteReceived: TSByteReceived read FOnSByteReceived write FOnSByteReceived;
    property OnInt16Received: TInt16Received read FOnInt16Received write FOnInt16Received;
    property OnInt32Received: TInt32Received read FOnInt32Received write FOnInt32Received;
    property OnInt64Received: TInt64Received read FOnInt64Received write FOnInt64Received;
    property OnArrayReceived: TArrayReceived read FOnArrayReceived write FOnArrayReceived;
    property OnStringReceived: TStringReceived read FOnStringReceived write FOnStringReceived;

    property OnGetByte: TDataEvent read FOnGetByte write FOnGetByte;
    property OnGetUInt16: TDataEvent read FOnGetUInt16 write FOnGetUInt16;
    property OnGetUInt32: TDataEvent read FOnGetUInt32 write FOnGetUInt32;
    property OnGetUInt64: TDataEvent read FOnGetUInt64 write FOnGetUInt64;
    property OnGetSByte: TDataEvent read FOnGetSByte write FOnGetSByte;
    property OnGetInt16: TDataEvent read FOnGetInt16 write FOnGetInt16;
    property OnGetInt32: TDataEvent read FOnGetInt32 write FOnGetInt32;
    property OnGetInt64: TDataEvent read FOnGetInt64 write FOnGetInt64;
    property OnGetArray: TDataEvent read FOnGetArray write FOnGetArray;
    property OnGetString: TDataEvent read FOnGetString write FOnGetString;
  end;

implementation

uses
  Windows, wclErrors;

{ TServerDataProcessor }

procedure TServerDataProcessor.ArrayReceived(Sender: TObject;
  const Data: TBytesArray);
begin
  if Assigned(FOnArrayReceived) then
    FOnArrayReceived(Self, Data);
end;

procedure TServerDataProcessor.ByteReceived(Sender: TObject; const Data: Byte);
begin
  if Assigned(FOnByteReceived) then
    FOnByteReceived(Self, Data);
end;

constructor TServerDataProcessor.Create(
  const Connection: TwclServerClientDataConnection);
begin
  inherited;

  FDecoder := TCommandDecoder.Create;

  FDecoder.OnByteReceived := ByteReceived;
  FDecoder.OnUInt16Received := UInt16Received;
  FDecoder.OnUInt32Received := UInt32Received;
  FDecoder.OnUInt64Received := UInt64Received;
  FDecoder.OnSByteReceived := SByteReceived;
  FDecoder.OnInt16Received := Int16Received;
  FDecoder.OnInt32Received := Int32Received;
  FDecoder.OnInt64Received := Int64Received;
  FDecoder.OnArrayReceived := ArrayReceived;
  FDecoder.OnStringReceived := StringReceived;

  FDecoder.OnGetByte := GetByte;
  FDecoder.OnGetUInt16 := GetUInt16;
  FDecoder.OnGetUInt32 := GetUInt32;
  FDecoder.OnGetUInt64 := GetUInt64;
  FDecoder.OnGetSByte := GetSByte;
  FDecoder.OnGetInt16 := GetInt16;
  FDecoder.OnGetInt32 := GetInt32;
  FDecoder.OnGetInt64 := GetInt64;
  FDecoder.OnGetArray := GetArray;
  FDecoder.OnGetString := GetString;

  FOnByteReceived := nil;
  FOnUInt16Received := nil;
  FOnUInt32Received := nil;
  FOnUInt64Received := nil;
  FOnSByteReceived := nil;
  FOnInt16Received := nil;
  FOnInt32Received := nil;
  FOnInt64Received := nil;
  FOnArrayReceived := nil;
  FOnStringReceived := nil;

  FOnGetByte := nil;
  FOnGetUInt16 := nil;
  FOnGetUInt32 := nil;
  FOnGetUInt64 := nil;
  FOnGetSByte := nil;
  FOnGetInt16 := nil;
  FOnGetInt32 := nil;
  FOnGetInt64 := nil;
  FOnGetArray := nil;
  FOnGetString := nil;
end;

destructor TServerDataProcessor.Destroy;
begin
  FDecoder.Free;

  inherited;
end;

procedure TServerDataProcessor.GetArray(Sender: TObject);
begin
  if Assigned(FOnGetArray) then
    FOnGetArray(Self);
end;

procedure TServerDataProcessor.GetByte(Sender: TObject);
begin
  if Assigned(FOnGetByte) then
    FOnGetByte(Self);
end;

procedure TServerDataProcessor.GetInt16(Sender: TObject);
begin
  if Assigned(FOnGetInt16) then
    FOnGetInt16(Self);
end;

procedure TServerDataProcessor.GetInt32(Sender: TObject);
begin
  if Assigned(FOnGetInt32) then
    FOnGetInt32(Self);
end;

procedure TServerDataProcessor.GetInt64(Sender: TObject);
begin
  if Assigned(FOnGetInt64) then
    FOnGetInt64(Self);
end;

procedure TServerDataProcessor.GetSByte(Sender: TObject);
begin
  if Assigned(FOnGetSByte) then
    FOnGetSByte(Self);
end;

procedure TServerDataProcessor.GetString(Sender: TObject);
begin
  if Assigned(FOnGetString) then
    FOnGetString(Self);
end;

procedure TServerDataProcessor.GetUInt16(Sender: TObject);
begin
  if Assigned(FOnGetUInt16) then
    FOnGetUInt16(Self);
end;

procedure TServerDataProcessor.GetUInt32(Sender: TObject);
begin
  if Assigned(FOnGetUInt32) then
    FOnGetUInt32(Self);
end;

procedure TServerDataProcessor.GetUInt64(Sender: TObject);
begin
  if Assigned(FOnGetUInt64) then
    FOnGetUInt64(Self);
end;

procedure TServerDataProcessor.Int16Received(Sender: TObject;
  const Data: Int16);
begin
  if Assigned(FOnInt16Received) then
    FOnInt16Received(Self, Data);
end;

procedure TServerDataProcessor.Int32Received(Sender: TObject;
  const Data: Int32);
begin
  if Assigned(FOnInt32Received) then
    FOnInt32Received(Self, Data);
end;

procedure TServerDataProcessor.Int64Received(Sender: TObject;
  const Data: Int64);
begin
  if Assigned(FOnInt64Received) then
    OnInt64Received(Self, Data);
end;

procedure TServerDataProcessor.ProcessData(const Data: Pointer;
  const Size: Cardinal);
var
  ArrData: TBytesArray;
begin
  if (Data <> nil) and (Size > 0) then begin
    SetLength(ArrData, Size);
    CopyMemory(Pointer(ArrData), Data, Size);
    FDecoder.ProcessData(ArrData);
  end;
end;

procedure TServerDataProcessor.SByteReceived(Sender: TObject; const Data: Int8);
begin
  if Assigned(FOnSByteReceived) then
    FOnSByteReceived(Self, Data);
end;

procedure TServerDataProcessor.StringReceived(Sender: TObject;
  const Data: string);
begin
  if Assigned(FOnStringReceived) then
    FOnStringReceived(Self, Data);
end;

procedure TServerDataProcessor.UInt16Received(Sender: TObject;
  const Data: UInt16);
begin
  if Assigned(FOnUInt16Received) then
    FOnUInt16Received(Self, Data);
end;

procedure TServerDataProcessor.UInt32Received(Sender: TObject;
  const Data: UInt32);
begin
  if Assigned(FOnUInt32Received) then
    FOnUInt32Received(Self, Data);
end;

procedure TServerDataProcessor.UInt64Received(Sender: TObject;
  const Data: UInt64);
begin
  if Assigned(FOnUInt64Received) then
    FOnUInt64Received(Self, Data);
end;

function TServerDataProcessor.Write(const Data: TBytesArray): Integer;
begin
  Result := inherited Write(Pointer(Data), Length(Data));
end;

function TServerDataProcessor.WriteArray(const Data: TBytesArray): Integer;
begin
  if (Length(Data) = 0) or (Length(Data) > High(UInt16) - 3) then
    Result := WCL_E_INVALID_ARGUMENT
  else
    Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteByte(const Data: Byte): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteError(const Error: Integer): Integer;
begin
  Result := Write(TCommandBuilder.CreateError(Error));
end;

function TServerDataProcessor.WriteInt16(const Data: Int16): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteInt32(const Data: Int32): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteInt64(const Data: Int64): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteSByte(const Data: Int8): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteString(const Data: string): Integer;
begin
  if (Length(Data) = 0) or (Length(Data) > High(UInt16) - 3) then
    Result := WCL_E_INVALID_ARGUMENT
  else
    Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteUInt16(const Data: UInt16): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteUInt32(const Data: UInt32): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

function TServerDataProcessor.WriteUInt64(const Data: UInt64): Integer;
begin
  Result := Write(TCommandBuilder.Create(Data));
end;

end.
