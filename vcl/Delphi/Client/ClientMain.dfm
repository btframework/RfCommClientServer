object fmClientMain: TfmClientMain
  Left = 0
  Top = 0
  BorderStyle = bsSingle
  Caption = 'RFCOMM Client Demo Application'
  ClientHeight = 573
  ClientWidth = 437
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object btDiscover: TButton
    Left = 8
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Discover'
    TabOrder = 0
    OnClick = btDiscoverClick
  end
  object lvDevices: TListView
    Left = 8
    Top = 39
    Width = 337
    Height = 106
    Columns = <
      item
        Caption = 'Address'
        Width = 150
      end
      item
        Caption = 'Name'
        Width = 150
      end>
    GridLines = True
    ReadOnly = True
    RowSelect = True
    TabOrder = 1
    ViewStyle = vsReport
  end
  object btSendByte: TButton
    Left = 8
    Top = 151
    Width = 75
    Height = 25
    Caption = 'Send Byte'
    TabOrder = 2
    OnClick = btSendByteClick
  end
  object btSendUint16: TButton
    Left = 89
    Top = 151
    Width = 75
    Height = 25
    Caption = 'Send UInt16'
    TabOrder = 3
    OnClick = btSendUint16Click
  end
  object btSendUInt32: TButton
    Left = 170
    Top = 151
    Width = 75
    Height = 25
    Caption = 'Send UInt32'
    TabOrder = 4
    OnClick = btSendUInt32Click
  end
  object btSendUInt64: TButton
    Left = 251
    Top = 151
    Width = 75
    Height = 25
    Caption = 'Send UInt64'
    TabOrder = 5
    OnClick = btSendUInt64Click
  end
  object btSendArray: TButton
    Left = 351
    Top = 151
    Width = 75
    Height = 25
    Caption = 'Send Array'
    TabOrder = 6
    OnClick = btSendArrayClick
  end
  object btConnect: TButton
    Left = 351
    Top = 39
    Width = 75
    Height = 25
    Caption = 'Connect'
    TabOrder = 7
    OnClick = btConnectClick
  end
  object btDisconnect: TButton
    Left = 351
    Top = 70
    Width = 75
    Height = 25
    Caption = 'Disconnect'
    TabOrder = 8
    OnClick = btDisconnectClick
  end
  object btSendSByte: TButton
    Left = 8
    Top = 182
    Width = 75
    Height = 25
    Caption = 'Send SByte'
    TabOrder = 9
    OnClick = btSendSByteClick
  end
  object btSendInt16: TButton
    Left = 89
    Top = 182
    Width = 75
    Height = 25
    Caption = 'Send Int16'
    TabOrder = 10
    OnClick = btSendInt16Click
  end
  object btSendInt32: TButton
    Left = 170
    Top = 182
    Width = 75
    Height = 25
    Caption = 'Send Int32'
    TabOrder = 11
    OnClick = btSendInt32Click
  end
  object btSendInt64: TButton
    Left = 251
    Top = 182
    Width = 75
    Height = 25
    Caption = 'Send Int64'
    TabOrder = 12
    OnClick = btSendInt64Click
  end
  object btSendString: TButton
    Left = 351
    Top = 182
    Width = 75
    Height = 25
    Caption = 'Send String'
    TabOrder = 13
    OnClick = btSendStringClick
  end
  object btGetByte: TButton
    Left = 8
    Top = 224
    Width = 75
    Height = 25
    Caption = 'Get Byte'
    TabOrder = 14
    OnClick = btGetByteClick
  end
  object btGetUInt16: TButton
    Left = 89
    Top = 224
    Width = 75
    Height = 25
    Caption = 'Get UInt16'
    TabOrder = 15
    OnClick = btGetUInt16Click
  end
  object btGetUInt32: TButton
    Left = 170
    Top = 224
    Width = 75
    Height = 25
    Caption = 'Get UInt32'
    TabOrder = 16
    OnClick = btGetUInt32Click
  end
  object btGetUInt64: TButton
    Left = 251
    Top = 224
    Width = 75
    Height = 25
    Caption = 'Get UInt64'
    TabOrder = 17
    OnClick = btGetUInt64Click
  end
  object btGetArray: TButton
    Left = 351
    Top = 224
    Width = 75
    Height = 25
    Caption = 'Get Array'
    TabOrder = 18
    OnClick = btGetArrayClick
  end
  object btGetSByte: TButton
    Left = 8
    Top = 255
    Width = 75
    Height = 25
    Caption = 'Get SByte'
    TabOrder = 19
    OnClick = btGetSByteClick
  end
  object btGetInt16: TButton
    Left = 89
    Top = 255
    Width = 75
    Height = 25
    Caption = 'Get Int16'
    TabOrder = 20
    OnClick = btGetInt16Click
  end
  object btGetInt32: TButton
    Left = 170
    Top = 255
    Width = 75
    Height = 25
    Caption = 'Get Int32'
    TabOrder = 21
    OnClick = btGetInt32Click
  end
  object btGetInt64: TButton
    Left = 251
    Top = 255
    Width = 75
    Height = 25
    Caption = 'Get Int64'
    TabOrder = 22
    OnClick = btGetInt64Click
  end
  object btGetString: TButton
    Left = 351
    Top = 255
    Width = 75
    Height = 25
    Caption = 'Get String'
    TabOrder = 23
    OnClick = btGetStringClick
  end
  object btClear: TButton
    Left = 351
    Top = 296
    Width = 75
    Height = 25
    Caption = 'Clear'
    TabOrder = 24
    OnClick = btClearClick
  end
  object lbLog: TListBox
    Left = 8
    Top = 327
    Width = 418
    Height = 238
    ItemHeight = 13
    TabOrder = 25
  end
  object wclBluetoothManager: TwclBluetoothManager
    AfterOpen = wclBluetoothManagerAfterOpen
    BeforeClose = wclBluetoothManagerBeforeClose
    OnDeviceFound = wclBluetoothManagerDeviceFound
    OnDiscoveringCompleted = wclBluetoothManagerDiscoveringCompleted
    OnDiscoveringStarted = wclBluetoothManagerDiscoveringStarted
    Left = 72
    Top = 376
  end
  object wclRfCommClient: TwclRfCommClient
    Authentication = False
    OnConnect = wclRfCommClientConnect
    OnCreateProcessor = wclRfCommClientCreateProcessor
    OnDestroyProcessor = wclRfCommClientDestroyProcessor
    OnDisconnect = wclRfCommClientDisconnect
    Left = 184
    Top = 376
  end
end
