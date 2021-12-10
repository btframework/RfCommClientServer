object fmServerMain: TfmServerMain
  Left = 0
  Top = 0
  BorderStyle = bsSingle
  Caption = 'RFCOMM Server Demo Application'
  ClientHeight = 583
  ClientWidth = 625
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
  object btListen: TButton
    Left = 8
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Listen'
    TabOrder = 0
    OnClick = btListenClick
  end
  object btClose: TButton
    Left = 89
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Close'
    TabOrder = 1
    OnClick = btCloseClick
  end
  object lbLog: TListBox
    Left = 8
    Top = 39
    Width = 609
    Height = 536
    ItemHeight = 13
    TabOrder = 2
  end
  object btClear: TButton
    Left = 542
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Clear'
    TabOrder = 3
    OnClick = btClearClick
  end
  object wclBluetoothManager: TwclBluetoothManager
    AfterOpen = wclBluetoothManagerAfterOpen
    BeforeClose = wclBluetoothManagerBeforeClose
    Left = 200
    Top = 200
  end
  object wclRfCommServer: TwclRfCommServer
    Authentication = False
    ServiceName = 'WCL SPP Server'
    OnClosed = wclRfCommServerClosed
    OnConnect = wclRfCommServerConnect
    OnCreateProcessor = wclRfCommServerCreateProcessor
    OnDestroyProcessor = wclRfCommServerDestroyProcessor
    OnDisconnect = wclRfCommServerDisconnect
    OnListen = wclRfCommServerListen
    Left = 296
    Top = 336
  end
end
