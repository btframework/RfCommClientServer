object fmClientMain: TfmClientMain
  Left = 286
  Top = 122
  BorderStyle = bsSingle
  Caption = 'fmClientMain'
  ClientHeight = 617
  ClientWidth = 443
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
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
    Top = 40
    Width = 345
    Height = 129
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
    Top = 176
    Width = 75
    Height = 25
    Caption = 'Send Byte'
    TabOrder = 2
    OnClick = btSendByteClick
  end
  object btSendUInt16: TButton
    Left = 96
    Top = 176
    Width = 75
    Height = 25
    Caption = 'Send UInt16'
    TabOrder = 3
    OnClick = btSendUInt16Click
  end
  object btSendUint32: TButton
    Left = 184
    Top = 176
    Width = 75
    Height = 25
    Caption = 'Send UInt32'
    TabOrder = 4
    OnClick = btSendUint32Click
  end
  object btSendUInt64: TButton
    Left = 272
    Top = 176
    Width = 75
    Height = 25
    Caption = 'Send UInt64'
    TabOrder = 5
    OnClick = btSendUInt64Click
  end
  object btSendArray: TButton
    Left = 360
    Top = 176
    Width = 75
    Height = 25
    Caption = 'Send Array'
    TabOrder = 6
    OnClick = btSendArrayClick
  end
  object btSendSByte: TButton
    Left = 8
    Top = 208
    Width = 75
    Height = 25
    Caption = 'Send SByte'
    TabOrder = 7
    OnClick = btSendSByteClick
  end
  object btSendInt16: TButton
    Left = 96
    Top = 208
    Width = 75
    Height = 25
    Caption = 'Send Int16'
    TabOrder = 8
    OnClick = btSendInt16Click
  end
  object btSendInt32: TButton
    Left = 184
    Top = 208
    Width = 75
    Height = 25
    Caption = 'Send Int32'
    TabOrder = 9
    OnClick = btSendInt32Click
  end
  object btSendInt64: TButton
    Left = 272
    Top = 208
    Width = 75
    Height = 25
    Caption = 'Send Int64'
    TabOrder = 10
    OnClick = btSendInt64Click
  end
  object btSendString: TButton
    Left = 360
    Top = 208
    Width = 75
    Height = 25
    Caption = 'Send String'
    TabOrder = 11
    OnClick = btSendStringClick
  end
  object btGetByte: TButton
    Left = 8
    Top = 248
    Width = 75
    Height = 25
    Caption = 'Get Byte'
    TabOrder = 12
    OnClick = btGetByteClick
  end
  object btGetUInt16: TButton
    Left = 96
    Top = 248
    Width = 75
    Height = 25
    Caption = 'Get UInt16'
    TabOrder = 13
    OnClick = btGetUInt16Click
  end
  object btGetUInt32: TButton
    Left = 184
    Top = 248
    Width = 75
    Height = 25
    Caption = 'Get UInt32'
    TabOrder = 14
    OnClick = btGetUInt32Click
  end
  object btGetUInt64: TButton
    Left = 272
    Top = 248
    Width = 75
    Height = 25
    Caption = 'Get UInt64'
    TabOrder = 15
    OnClick = btGetUInt64Click
  end
  object btGetArray: TButton
    Left = 360
    Top = 248
    Width = 75
    Height = 25
    Caption = 'Get Array'
    TabOrder = 16
    OnClick = btGetArrayClick
  end
  object btGetSByte: TButton
    Left = 8
    Top = 280
    Width = 75
    Height = 25
    Caption = 'Get SByte'
    TabOrder = 17
    OnClick = btGetSByteClick
  end
  object btGetInt16: TButton
    Left = 96
    Top = 280
    Width = 75
    Height = 25
    Caption = 'Get Int16'
    TabOrder = 18
    OnClick = btGetInt16Click
  end
  object btGetInt32: TButton
    Left = 184
    Top = 280
    Width = 75
    Height = 25
    Caption = 'Get Int32'
    TabOrder = 19
    OnClick = btGetInt32Click
  end
  object btGetInt64: TButton
    Left = 279
    Top = 279
    Width = 75
    Height = 25
    Caption = 'Get Int64'
    TabOrder = 20
    OnClick = btGetInt64Click
  end
  object btGetString: TButton
    Left = 360
    Top = 279
    Width = 75
    Height = 25
    Caption = 'Get String'
    TabOrder = 21
    OnClick = btGetStringClick
  end
  object btClear: TButton
    Left = 360
    Top = 320
    Width = 75
    Height = 25
    Caption = 'Clear'
    TabOrder = 22
    OnClick = btClearClick
  end
  object lbLog: TListBox
    Left = 0
    Top = 352
    Width = 433
    Height = 257
    ItemHeight = 13
    TabOrder = 23
  end
  object btConnect: TButton
    Left = 360
    Top = 40
    Width = 75
    Height = 25
    Caption = 'Connect'
    TabOrder = 24
    OnClick = btConnectClick
  end
  object btDisconnect: TButton
    Left = 360
    Top = 72
    Width = 75
    Height = 25
    Caption = 'Disconnect'
    TabOrder = 25
    OnClick = btDisconnectClick
  end
  object wclBluetoothManager: TwclBluetoothManager
    AfterOpen = wclBluetoothManagerAfterOpen
    BeforeClose = wclBluetoothManagerBeforeClose
    OnDeviceFound = wclBluetoothManagerDeviceFound
    OnDiscoveringCompleted = wclBluetoothManagerDiscoveringCompleted
    OnDiscoveringStarted = wclBluetoothManagerDiscoveringStarted
    Left = 128
    Top = 424
  end
  object wclRfCommClient: TwclRfCommClient
    Authentication = False
    OnConnect = wclRfCommClientConnect
    OnCreateProcessor = wclRfCommClientCreateProcessor
    OnDestroyProcessor = wclRfCommClientDestroyProcessor
    OnDisconnect = wclRfCommClientDisconnect
    Left = 240
    Top = 416
  end
end
