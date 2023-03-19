unit ClientDataProcessor;

interface

uses
  wclConnections, Common;

type
  TClientDataProcessor = class sealed(TwclCustomClientDataProcessor)
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

    FOnError: TErrorEvent;

    procedure ArrayReceived(Sender: TObject; const Data: TBytesArray);
    procedure StringReceived(Sender: TObject; const Data: string);
    procedure Int64Received(Sender: TObject; const Data: Int64);
    procedure Int32Received(Sender: TObject; const Data: Int32);
    procedure Int16Received(Sender: TObject; const Data: Int16);
    procedure SByteReceived(Sender: TObject; const Data: Int8);
    procedure UInt64Received(Sender: TObject; const Data: UInt64);
    procedure UInt32Received(Sender: TObject; const Data: UInt32);
    procedure UInt16Received(Sender: TObject; const Data: UInt16);
    procedure ByteReceived(Sender: TObject; const Data: Byte);
    procedure ErrorReceived(Sender: TObject; const Error: Integer);

    function Write(const Data: TBytesArray): Integer;

  public
    constructor Create(const Connection: TwclClientDataConnection); override;
    destructor Destroy; override;
    
    procedure ProcessData(const Data: Pointer; const Size: Cardinal); override;
    
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

    function GetByte: Integer;
    function GetSByte: Integer;
    function GetUInt16: Integer;
    function GetInt16: Integer;
    function GetUInt32: Integer;
    function GetInt32: Integer;
    function GetUInt64: Integer;
    function GetInt64: Integer;

    function GetArray: Integer;
    function GetString: Integer;

    property OnByteReceived: TByteReceived read FOnByteReceived
      write FOnByteReceived;
    property OnUInt16Received: TUInt16Received read FOnUInt16Received
      write FOnUInt16Received;
    property OnUInt32Received: TUInt32Received read FOnUInt32Received
      write FOnUInt32Received;
    property OnUInt64Received: TUInt64Received read FOnUInt64Received
      write FOnUInt64Received;
    property OnSByteReceived: TSByteReceived read FOnSByteReceived
      write FOnSByteReceived;
    property OnInt16Received: TInt16Received read FOnInt16Received
      write FOnInt16Received;
    property OnInt32Received: TInt32Received read FOnInt32Received
      write FOnInt32Received;
    property OnInt64Received: TInt64Received read FOnInt64Received
      write FOnInt64Received;
    property OnArrayReceived: TArrayReceived read FOnArrayReceived
      write FOnArrayReceived;
    property OnStringReceived: TStringReceived read FOnStringReceived
      write FOnStringReceived;

    property OnError: TErrorEvent read FOnError write FOnError;
  end;

implementation

uses
  Windows, wclErrors;

{ TClientDataProcessor }

procedure TClientDataProcessor.ArrayReceived(Sender: TObject;
  const Data: TBytesArray);
begin
  if Assigned(FOnArrayReceived) then
    FOnArrayReceived(Self, Data);
end;

procedure TClientDataProcessor.ByteReceived(Sender: TObject; const Data: Byte);
begin
  if Assigned(FOnByteReceived) then
    FOnByteReceived(Self, Data);
end;

constructor TClientDataProcessor.Create(
  const Connection: TwclClientDataConnection);
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

  FDecoder.OnError := ErrorReceived;

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

  FOnError := nil;
end;

destructor TClientDataProcessor.Destroy;
begin
  FDecoder.Free;

  inherited;
end;

procedure TClientDataProcessor.ErrorReceived(Sender: TObject;
  const Error: Integer);
begin
  if Assigned(FOnError) then
    FOnError(Self, Error);
end;

function TClientDataProcessor.GetArray: Integer;
begin
  Result := Write(CreateGet(CMD_GET_ARRAY));
end;

function TClientDataProcessor.GetByte: Integer;
begin
  Result := Write(CreateGet(CMD_GET_BYTE));
end;

function TClientDataProcessor.GetInt16: Integer;
begin
  Result := Write(CreateGet(CMD_GET_INT16));
end;

function TClientDataProcessor.GetInt32: Integer;
begin
  Result := Write(CreateGet(CMD_GET_INT32));
end;

function TClientDataProcessor.GetInt64: Integer;
begin
  Result := Write(CreateGet(CMD_GET_INT64));
end;

function TClientDataProcessor.GetSByte: Integer;
begin
  Result := Write(CreateGet(CMD_GET_SBYTE));
end;

function TClientDataProcessor.GetString: Integer;
begin
  Result := Write(CreateGet(CMD_GET_STRING));
end;

function TClientDataProcessor.GetUInt16: Integer;
begin
  Result := Write(CreateGet(CMD_GET_UINT16));
end;

function TClientDataProcessor.GetUInt32: Integer;
begin
  Result := Write(CreateGet(CMD_GET_UINT32));
end;

function TClientDataProcessor.GetUInt64: Integer;
begin
  Result := Write(CreateGet(CMD_GET_UINT64));
end;

procedure TClientDataProcessor.Int16Received(Sender: TObject;
  const Data: Int16);
begin
  if Assigned(FOnInt16Received) then
    FOnInt16Received(Self, Data);
end;

procedure TClientDataProcessor.Int32Received(Sender: TObject;
  const Data: Int32);
begin
  if Assigned(FOnInt32Received) then
    FOnInt32Received(Self, Data);
end;

procedure TClientDataProcessor.Int64Received(Sender: TObject;
  const Data: Int64);
begin
  if Assigned(FOnInt64Received) then
    FOnInt64Received(Self, Data);
end;

procedure TClientDataProcessor.ProcessData(const Data: Pointer;
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

procedure TClientDataProcessor.SByteReceived(Sender: TObject; const Data: Int8);
begin
  if Assigned(FOnSByteReceived) then
    FOnSByteReceived(Self, Data);
end;

procedure TClientDataProcessor.StringReceived(Sender: TObject;
  const Data: string);
begin
  if Assigned(FOnStringReceived) then
    FOnStringReceived(Self, Data);
end;

procedure TClientDataProcessor.UInt16Received(Sender: TObject;
  const Data: UInt16);
begin
  if Assigned(FOnUInt16Received) then
    FOnUInt16Received(Self, Data);
end;

procedure TClientDataProcessor.UInt32Received(Sender: TObject;
  const Data: UInt32);
begin
  if Assigned(FOnUInt32Received) then
    FOnUInt32Received(Self, Data);
end;

procedure TClientDataProcessor.UInt64Received(Sender: TObject;
  const Data: UInt64);
begin
  if Assigned(FOnUInt64Received) then
    FOnUInt64Received(Self, Data);
end;

function TClientDataProcessor.Write(const Data: TBytesArray): Integer;
begin
  Result := inherited Write(Pointer(Data), Length(Data));
end;

function TClientDataProcessor.WriteArray(const Data: TBytesArray): Integer;
begin
  if (Length(Data) = 0) or (Length(Data) > High(Word) - 3) then
    Result := WCL_E_INVALID_ARGUMENT
  else
    Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteByte(const Data: Byte): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteInt16(const Data: Int16): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteInt32(const Data: Int32): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteInt64(const Data: Int64): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteSByte(const Data: Int8): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteString(const Data: string): Integer;
begin
  if (Length(Data) = 0) or (Length(Data) > High(Word) - 3) then
    Result := WCL_E_INVALID_ARGUMENT
  else
    Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteUInt16(const Data: UInt16): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteUInt32(const Data: UInt32): Integer;
begin
  Result := Write(Common.Create(Data));
end;

function TClientDataProcessor.WriteUInt64(const Data: UInt64): Integer;
begin
  Result := Write(Common.Create(Data));
end;

end.
