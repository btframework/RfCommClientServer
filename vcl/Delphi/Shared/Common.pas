unit Common;

interface

type
  UInt16 = type Word;
  UInt32 = type Cardinal;
  SByte = type Shortint;
  Int16 = type Smallint;
  Int32 = type Integer;

  TByteArray = array of Byte;

  TByteReceived = procedure(Sender: TObject; const Data: Byte) of object;
  TUInt16Received = procedure(Sender: TObject; const Data: UInt16) of object;
  TUInt32Received = procedure(Sender: TObject; const Data: UInt32) of object;
  TUInt64Received = procedure(Sender: TObject; const Data: UInt64) of object;
  TSByteReceived = procedure(Sender: TObject; const Data: SByte) of object;
  TInt16Received = procedure(Sender: TObject; const Data: Int16) of object;
  TInt32Received = procedure(Sender: TObject; const Data: Int32) of object;
  TInt64Received = procedure(Sender: TObject; const Data: Int64) of object;
  TArrayReceived = procedure(Sender: TObject; const Data: TByteArray) of object;
  TStringReceived = procedure(Sender: TObject; const Data: string) of object;

  TDataEvent = procedure(Sender: TObject) of object;

  TErrorEvent = procedure(Sender: TObject; const Error: Int32) of object;

const
  CMD_FLAG = $0F;

  CMD_FLAG_SEND = $00;
  CMD_FLAG_GET = $40;
  CMD_FLAG_ERROR = $80;

  CMD_BYTE = $01;
  CMD_UINT16 = $02;
  CMD_UINT32 = $03;
  CMD_UINT64 = $04;
  CMD_SBYTE = $05;
  CMD_INT16 = $06;
  CMD_INT32 = $07;
  CMD_INT64 = $08;
  CMD_ARRAY = $09;
  CMD_STRING = $0A;

  CMD_ERROR = $0F;

  CMD_SEND_BYTE = CMD_FLAG_SEND or CMD_BYTE;
  CMD_SEND_UINT16 = CMD_FLAG_SEND or CMD_UINT16;
  CMD_SEND_UINT32 = CMD_FLAG_SEND or CMD_UINT32;
  CMD_SEND_UINT64 = CMD_FLAG_SEND or CMD_UINT64;
  CMD_SEND_SBYTE = CMD_FLAG_SEND or CMD_SBYTE;
  CMD_SEND_INT16 = CMD_FLAG_SEND or CMD_INT16;
  CMD_SEND_INT32 = CMD_FLAG_SEND or CMD_INT32;
  CMD_SEND_INT64 = CMD_FLAG_SEND or CMD_INT64;
  CMD_SEND_ARRAY = CMD_FLAG_SEND or CMD_ARRAY;
  CMD_SEND_STRING = CMD_FLAG_SEND or CMD_STRING;

  CMD_GET_BYTE = CMD_FLAG_GET or CMD_BYTE;
  CMD_GET_UINT16 = CMD_FLAG_GET or CMD_UINT16;
  CMD_GET_UINT32 = CMD_FLAG_GET or CMD_UINT32;
  CMD_GET_UINT64 = CMD_FLAG_GET or CMD_UINT64;
  CMD_GET_SBYTE = CMD_FLAG_GET or CMD_SBYTE;
  CMD_GET_INT16 = CMD_FLAG_GET or CMD_INT16;
  CMD_GET_INT32 = CMD_FLAG_GET or CMD_INT32;
  CMD_GET_INT64 = CMD_FLAG_GET or CMD_INT64;
  CMD_GET_ARRAY = CMD_FLAG_GET or CMD_ARRAY;
  CMD_GET_STRING = CMD_FLAG_GET or CMD_STRING;

  CMD_ERROR_CODE = CMD_FLAG_ERROR or CMD_ERROR;

  ServiceUuid: TGUID = '{CA80C97C-06B3-4E65-9CEE-65BB0B11BC92}';

function IsCmdSend(const Cmd: Byte): Boolean;
function IsCmdGet(const Cmd: Byte): Boolean;
function IsCmdError(const Cmd: Byte): Boolean;

function Create(const Data: Byte; const Signed: Boolean): TByteArray; overload;
function Create(const Data: UInt16; const Signed: Boolean): TByteArray; overload;
function Create(const Data: UInt32; const Signed: Boolean): TByteArray; overload;
function Create(const Data: UInt64; const Signed: Boolean): TByteArray; overload;
function Create(const Data: TByteArray): TByteArray; overload;
function Create(const Data: string): TByteArray; overload;
function Create(const Error: Int32): TByteArray; overload;
function Create(const Cmd: Byte): TByteArray; overload;

type
  TCommandDecoder = class
  private
    FBuffer: TByteArray;

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
    
    FOnError: TErrorEvent;

    procedure DoByteReceived(const Data: Byte);
    procedure DoUInt16Received(const Data: UInt16);
    procedure DoUInt32Received(const Data: UInt32);
    procedure DoUInt64Received(const Data: UInt64);
    procedure DoSByteReceived(const Data: SByte);
    procedure DoInt16Received(const Data: Int16);
    procedure DoInt32Received(const Data: Int32);
    procedure DoInt64Received(const Data: Int64);
    procedure DoArrayReceived(const Data: TByteArray);
    procedure DoStringReceived(const Data: string);

    procedure DoGetByte;
    procedure DoGetUInt16;
    procedure DoGetUInt32;
    procedure DoGetUInt64;
    procedure DoGetSByte;
    procedure DoGetInt16;
    procedure DoGetInt32;
    procedure DoGetInt64;
    procedure DoGetArray;
    procedure DoGetString;

    procedure DoError(const Error: Int32);

    procedure DecodeSendCommand(const Data: TByteArray);
    procedure DecodeGetCommand(const Data: TByteArray);
    procedure DecodeErrorCommand(const Data: TByteArray);
    procedure DecodeData(const Data: TByteArray);

    procedure DataReceived;

  public
    constructor Create;

    procedure ProcessData(const Data: TByteArray);

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

    property OnError: TErrorEvent read FOnError write FOnError;
  end;

implementation

uses
  Windows;

function IsCmdSend(const Cmd: Byte): Boolean;
begin
  Result := (((Cmd and CMD_FLAG_GET) = 0) and ((Cmd and CMD_FLAG_ERROR) = 0));
end;

function IsCmdGet(const Cmd: Byte): Boolean;
begin
  Result := ((Cmd and CMD_FLAG_GET) <> 0);
end;

function IsCmdError(const Cmd: Byte): Boolean;
begin
  Result := ((Cmd and CMD_FLAG_ERROR) <> 0);
end;

function Create(const Data: Byte; const Signed: Boolean): TByteArray; overload;
begin
  SetLength(Result, 4);
  Result[0] := $00;
  Result[1] := $04;
  if Signed then
    Result[2] := CMD_SEND_SBYTE
  else
    Result[2] := CMD_SEND_BYTE;
  Result[3] := Data;
end;

function Create(const Data: UInt16; const Signed: Boolean): TByteArray; overload;
begin
  SetLength(Result, 5);
  Result[0] := $00;
  Result[1] := $05;
  if Signed then
    Result[2] := CMD_SEND_INT16
  else
    Result[2] := CMD_SEND_UINT16;
  Result[3] := HIBYTE(Data);
  Result[4] := LOBYTE(Data);
end;

function Create(const Data: UInt32; const Signed: Boolean): TByteArray; overload;
begin
  SetLength(Result, 7);
  Result[0] := $00;
  Result[1] := $07;
  if Signed then
    Result[2] := CMD_SEND_INT32
  else
    Result[2] := CMD_SEND_UINT32;
  Result[3] := HIBYTE(HIWORD(Data));
  Result[4] := LOBYTE(HIWORD(Data));
  Result[5] := HIBYTE(LOWORD(Data));
  Result[6] := LOBYTE(LOWORD(Data));
end;

function Create(const Data: UInt64; const Signed: Boolean): TByteArray; overload;
var
  Hi: UInt32;
  Lo: UInt32;
begin
  Hi := UInt32(Data shr 32);
  Lo := UInt32(Data and $00000000FFFFFFFF);
  SetLength(Result, 11);
  Result[0] := $00;
  Result[1] := $0B;
  if Signed then
    Result[2] := CMD_SEND_INT64
  else
    Result[2] := CMD_SEND_UINT64;
  Result[3] := HIBYTE(HIWORD(Hi));
  Result[4] := LOBYTE(HIWORD(Hi));
  Result[5] := HIBYTE(LOWORD(Hi));
  Result[6] := LOBYTE(LOWORD(Hi));
  Result[7] := HIBYTE(HIWORD(Lo));
  Result[8] := LOBYTE(HIWORD(Lo));
  Result[9] := HIBYTE(LOWORD(Lo));
  Result[10] := LOBYTE(LOWORD(Lo));
end;

function Create(const Data: TByteArray): TByteArray; overload;
var
  Len: UInt16;
begin
  Len := UInt16(3 + Length(Data));
  SetLength(Result, Len);
  Result[0] := HIBYTE(Len);
  Result[1] := LOBYTE(Len);
  Result[2] := CMD_SEND_ARRAY;
  CopyMemory(@Result[3], Pointer(Data), Length(Data));
end;

function Create(const Data: string): TByteArray; overload;
var
  Str: UTF8String;
  Len: UInt16;
begin
  Str := UTF8Encode(WideString(Data));
  Len := UInt16(3 + Length(Str));
  SetLength(Result, Len);
  Result[0] := HIBYTE(Len);
  Result[1] := LOBYTE(Len);
  Result[2] := CMD_SEND_STRING;
  CopyMemory(@Result[3], Pointer(Str), Length(Str));
end;

function Create(const Error: Int32): TByteArray; overload;
begin
  SetLength(Result, 7);
  Result[0] := $00;
  Result[1] := $07;
  Result[2] := CMD_ERROR_CODE;
  Result[3] := HIBYTE(HIWORD(Error));
  Result[4] := LOBYTE(HIWORD(Error));
  Result[5] := HIBYTE(HIWORD(Error));
  Result[6] := LOBYTE(HIWORD(Error));
end;

function Create(const Cmd: Byte): TByteArray; overload;
begin
  SetLength(Result, 3);
  Result[0] := $00;
  Result[1] := $03;
  Result[2] := Cmd;
end;

{ TCommandDecoder }

constructor TCommandDecoder.Create;
begin
  FBuffer := nil;

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

  FOnError := nil;
end;

procedure TCommandDecoder.DataReceived;
var
  Len: UInt16;
  Data: TByteArray;
begin
  while Length(FBuffer) > 2 do begin
    // Data length includes length bytes!
    Len := (FBuffer[0] shl 8) or FBuffer[1];
    if Len > Length(FBuffer) then
      Break;

    SetLength(Data, Len - 2);
    CopyMemory(Pointer(Data), @FBuffer[2], Len - 2);
    DecodeData(Data);

    if Len = Length(FBuffer) then begin
      FBuffer := nil;
      Break;
    end;

    Data := nil;
    SetLength(Data, Length(FBuffer) - Len);
    CopyMemory(Pointer(Data), @FBuffer[Len], Length(FBuffer) - Len);
    FBuffer := Data;
  end;
end;

procedure TCommandDecoder.DecodeData(const Data: TByteArray);
var
  Cmd: Byte;
begin
  // It is guaranteed that the Data length at least 2 bytes.
  Cmd := Data[0];
  if IsCmdSend(Cmd) then
    DecodeSendCommand(Data)
  else begin
    if IsCmdGet(Cmd) then
      DecodeGetCommand(Data)
    else begin
      if IsCmdError(Cmd) then
        DecodeErrorCommand(Data);
    end;
  end;
end;

procedure TCommandDecoder.DecodeErrorCommand(const Data: TByteArray);
var
  Error: Int32;
begin
  if Length(Data) = 5 then begin
    if Data[0] = CMD_ERROR_CODE then begin
      Error := (Data[1] shl 24) or (Data[2] shl 16) or (Data[3] shl 8) or Data[4];
      DoError(Error);
    end;
  end;
end;

procedure TCommandDecoder.DecodeGetCommand(const Data: TByteArray);
begin
  if Length(Data) = 1  then begin
    case Data[0] and CMD_FLAG of
      CMD_BYTE: DoGetByte;
      CMD_UINT16: DoGetUInt16;
      CMD_UINT32: DoGetUInt32;
      CMD_UINT64: DoGetUInt64;
      CMD_SBYTE: DoGetSByte;
      CMD_INT16: DoGetInt16;
      CMD_INT32: DoGetInt32;
      CMD_INT64: DoGetInt64;
      CMD_ARRAY: DoGetArray;
      CMD_STRING: DoGetString;
    end;
  end;
end;

procedure TCommandDecoder.DecodeSendCommand(const Data: TByteArray);
var
  Arr: TByteArray;
begin
  case Data[0] and CMD_FLAG of
    CMD_BYTE:
      if Length(Data) = 2 then
        DoByteReceived(Data[1]);

    CMD_UINT16:
      if Length(Data) = 3 then
        DoUInt16Received((Data[1] shl 8) or Data[2]);

    CMD_UINT32:
      if Length(Data) = 5 then begin
        DoUInt32Received((Data[1] shl 24) or (Data[2] shl 16) or
          (Data[3] shl 8) or Data[4]);
      end;

    CMD_UINT64:
      if Length(Data) = 9 then begin
        DoUInt64Received((UInt64(Data[1]) shl 56) or (UInt64(Data[2]) shl 48) or
          (UInt64(Data[3]) shl 40) or (UInt64(Data[4]) shl 32) or
          (UInt64(Data[5]) shl 24) or (UInt64(Data[6]) shl 16) or
          (UInt64(Data[7]) shl 8) or UInt64(Data[8]));
      end;

    CMD_SBYTE:
      if Length(Data) = 2 then
        DoSByteReceived(Data[1]);

    CMD_INT16:
      if Length(Data) = 3 then
        DoInt16Received((Data[1] shl 8) or Data[2]);

    CMD_INT32:
      if Length(Data) = 5 then begin
        DoInt32Received((Data[1] shl 24) or (Data[2] shl 16) or
          (Data[3] shl 8) or Data[4]);
      end;

    CMD_INT64:
      if Length(Data) = 9 then begin
        DoInt64Received((Int64(Data[1]) shl 56) or (Int64(Data[2]) shl 48) or
          (Int64(Data[3]) shl 40) or (Int64(Data[4]) shl 32) or
          (Int64(Data[5]) shl 24) or (Int64(Data[6]) shl 16) or
          (Int64(Data[7]) shl 8) or Int64(Data[8]));
      end;

    CMD_ARRAY:
      if Length(Data) > 1 then begin
        SetLength(Arr, Length(Data) - 1);
        CopyMemory(Arr, @Data[1], Length(Data) - 1);
        DoArrayReceived(Arr);
      end;

    CMD_STRING:
      if Length(Data) > 1 then
        DoStringReceived(UTF8ToWideString(UTF8String(@Data[1])));
  end;
end;

procedure TCommandDecoder.DoArrayReceived(const Data: TByteArray);
begin
  if Assigned(FOnArrayReceived) then
    FOnArrayReceived(Self, Data);
end;

procedure TCommandDecoder.DoByteReceived(const Data: Byte);
begin
  if Assigned(FOnByteReceived) then
    FOnByteReceived(Self, Data);
end;

procedure TCommandDecoder.DoError(const Error: Int32);
begin
  if Assigned(FOnError) then
    FOnError(Self, Error);
end;

procedure TCommandDecoder.DoGetArray;
begin
  if Assigned(FOnGetArray) then
    FOnGetArray(Self);
end;

procedure TCommandDecoder.DoGetByte;
begin
  if Assigned(FOnGetByte) then
    FOnGetByte(Self);
end;

procedure TCommandDecoder.DoGetInt16;
begin
  if Assigned(FOnGetInt16) then
    FOnGetInt16(Self);
end;

procedure TCommandDecoder.DoGetInt32;
begin
  if Assigned(FOnGetInt32) then
    FOnGetInt32(Self);
end;

procedure TCommandDecoder.DoGetInt64;
begin
  if Assigned(FOnGetInt64) then
    FOnGetInt64(Self);
end;

procedure TCommandDecoder.DoGetSByte;
begin
  if Assigned(FOnGetSByte) then
    FOnGetSByte(Self);
end;

procedure TCommandDecoder.DoGetString;
begin
  if Assigned(FOnGetString) then
    FOnGetString(Self);
end;

procedure TCommandDecoder.DoGetUInt16;
begin
  if Assigned(FOnGetUInt16) then
    FOnGetUInt16(Self);
end;

procedure TCommandDecoder.DoGetUInt32;
begin
  if Assigned(FOnGetUInt32) then
    FOnGetUInt32(Self);
end;

procedure TCommandDecoder.DoGetUInt64;
begin
  if Assigned(FOnGetUInt64) then
    FOnGetUInt64(Self);
end;

procedure TCommandDecoder.DoInt16Received(const Data: Int16);
begin
  if Assigned(FOnInt16Received) then
    FOnInt16Received(Self, Data);
end;

procedure TCommandDecoder.DoInt32Received(const Data: Int32);
begin
  if Assigned(FOnInt32Received) then
    FOnInt32Received(Self, Data);
end;

procedure TCommandDecoder.DoInt64Received(const Data: Int64);
begin
  if Assigned(FOnInt64Received) then
    FOnInt64Received(Self, Data);
end;

procedure TCommandDecoder.DoSByteReceived(const Data: SByte);
begin
  if Assigned(FOnSByteReceived) then
    FOnSByteReceived(Self, Data);
end;

procedure TCommandDecoder.DoStringReceived(const Data: string);
begin
  if Assigned(FOnStringReceived) then
    FOnStringReceived(Self, Data);
end;

procedure TCommandDecoder.DoUInt16Received(const Data: UInt16);
begin
  if Assigned(FOnUInt16Received) then
    FOnUInt16Received(Self, Data);
end;

procedure TCommandDecoder.DoUInt32Received(const Data: UInt32);
begin
  if Assigned(FOnUInt32Received) then
    FOnUInt32Received(Self, Data);
end;

procedure TCommandDecoder.DoUInt64Received(const Data: UInt64);
begin
  if Assigned(FOnUInt64Received) then
    FOnUInt64Received(Self, Data);
end;

procedure TCommandDecoder.ProcessData(const Data: TByteArray);
var
  Ndx: Int32;
begin
  if Length(Data) > 0 then begin
    if Length(FBuffer) = 0 then begin
      SetLength(FBuffer, Length(Data));
      Ndx := 0;

    end else begin
      Ndx := Length(FBuffer);
      SetLength(FBuffer, Ndx + Length(Data));
    end;

    CopyMemory(@FBuffer[Ndx], Pointer(Data), Length(Data));
    DataReceived;
  end;
end;

end.
