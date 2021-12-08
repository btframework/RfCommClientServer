Friend Class ServerDataProcessor
    Inherits wclCustomServerClientDataProcessor

    Private FDecoder As CommandDecoder

#Region "Data decoder events."
    Private Sub FDecoder_OnGetArray(Sender As Object)
        If OnGetArrayEvent IsNot Nothing Then
            RaiseEvent OnGetArray(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetInt64(Sender As Object)
        If OnGetInt64Event IsNot Nothing Then
            RaiseEvent OnGetInt64(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetInt32(Sender As Object)
        If OnGetInt32Event IsNot Nothing Then
            RaiseEvent OnGetInt32(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetInt16(Sender As Object)
        If OnGetInt16Event IsNot Nothing Then
            RaiseEvent OnGetInt16(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetSByte(Sender As Object)
        If OnGetSByteEvent IsNot Nothing Then
            RaiseEvent OnGetSByte(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetUInt64(Sender As Object)
        If OnGetUInt64Event IsNot Nothing Then
            RaiseEvent OnGetUInt64(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetUIn32(Sender As Object)
        If OnGetUIn32Event IsNot Nothing Then
            RaiseEvent OnGetUIn32(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetUIn16(Sender As Object)
        If OnGetUIn16Event IsNot Nothing Then
            RaiseEvent OnGetUIn16(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetByte(Sender As Object)
        If OnGetByteEvent IsNot Nothing Then
            RaiseEvent OnGetByte(Me)
        End If
    End Sub

    Private Sub FDecoder_OnGetString(Sender As Object)
        If OnGetStringEvent IsNot Nothing Then
            RaiseEvent OnGetString(Me)
        End If
    End Sub

    Private Sub FDecoder_OnArrayReceived(Sender As Object, Data As Byte())
        If OnArrayReceivedEvent IsNot Nothing Then
            RaiseEvent OnArrayReceived(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnInt64Received(Sender As Object, Data As Int64)
        If OnInt64ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt64Received(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnInt32Received(Sender As Object, Data As Int32)
        If OnInt32ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt32Received(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnInt16Received(Sender As Object, Data As Int16)
        If OnInt16ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt16Received(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnSByteReceived(Sender As Object, Data As SByte)
        If OnSByteReceivedEvent IsNot Nothing Then
            RaiseEvent OnSByteReceived(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnUInt64Received(Sender As Object, Data As UInt64)
        If OnUInt64ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt64Received(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnUInt32Received(Sender As Object, Data As UInt32)
        If OnUInt32ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt32Received(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnUInt16Received(Sender As Object, Data As UInt16)
        If OnUInt16ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt16Received(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnByteReceived(Sender As Object, Data As Byte)
        If OnByteReceivedEvent IsNot Nothing Then
            RaiseEvent OnByteReceived(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnStringReceived(Sender As Object, Data As String)
        If OnStringReceivedEvent IsNot Nothing Then
            RaiseEvent OnStringReceived(Me, Data)
        End If
    End Sub
#End Region

    Protected Overrides Sub ProcessData(Data As Byte())
        FDecoder.ProcessData(Data)
    End Sub

    Public Sub New(Connection As wclServerClientDataConnection)
        MyBase.New(Connection)

        FDecoder = New CommandDecoder()

        AddHandler FDecoder.OnByteReceived, AddressOf FDecoder_OnByteReceived
        AddHandler FDecoder.OnUInt16Received, AddressOf FDecoder_OnUInt16Received
        AddHandler FDecoder.OnUInt32Received, AddressOf FDecoder_OnUInt32Received
        AddHandler FDecoder.OnUInt64Received, AddressOf FDecoder_OnUInt64Received
        AddHandler FDecoder.OnSByteReceived, AddressOf FDecoder_OnSByteReceived
        AddHandler FDecoder.OnInt16Received, AddressOf FDecoder_OnInt16Received
        AddHandler FDecoder.OnInt32Received, AddressOf FDecoder_OnInt32Received
        AddHandler FDecoder.OnInt64Received, AddressOf FDecoder_OnInt64Received
        AddHandler FDecoder.OnArrayReceived, AddressOf FDecoder_OnArrayReceived
        AddHandler FDecoder.OnStringReceived, AddressOf FDecoder_OnStringReceived

        AddHandler FDecoder.OnGetByte, AddressOf FDecoder_OnGetByte
        AddHandler FDecoder.OnGetUIn16, AddressOf FDecoder_OnGetUIn16
        AddHandler FDecoder.OnGetUIn32, AddressOf FDecoder_OnGetUIn32
        AddHandler FDecoder.OnGetUInt64, AddressOf FDecoder_OnGetUInt64
        AddHandler FDecoder.OnGetSByte, AddressOf FDecoder_OnGetSByte
        AddHandler FDecoder.OnGetInt16, AddressOf FDecoder_OnGetInt16
        AddHandler FDecoder.OnGetInt32, AddressOf FDecoder_OnGetInt32
        AddHandler FDecoder.OnGetInt64, AddressOf FDecoder_OnGetInt64
        AddHandler FDecoder.OnGetArray, AddressOf FDecoder_OnGetArray
        AddHandler FDecoder.OnGetString, AddressOf FDecoder_OnGetString

        OnByteReceivedEvent = Nothing
        OnUInt16ReceivedEvent = Nothing
        OnUInt32ReceivedEvent = Nothing
        OnUInt64ReceivedEvent = Nothing
        OnSByteReceivedEvent = Nothing
        OnInt16ReceivedEvent = Nothing
        OnInt32ReceivedEvent = Nothing
        OnInt64ReceivedEvent = Nothing
        OnArrayReceivedEvent = Nothing
        OnStringReceivedEvent = Nothing

        OnGetByteEvent = Nothing
        OnGetUIn16Event = Nothing
        OnGetUIn32Event = Nothing
        OnGetUInt64Event = Nothing
        OnGetSByteEvent = Nothing
        OnGetInt16Event = Nothing
        OnGetInt32Event = Nothing
        OnGetInt64Event = Nothing
        OnGetArrayEvent = Nothing
        OnGetStringEvent = Nothing
    End Sub

#Region "Write data"
#Region "Unsigned"
    Public Overloads Function WriteData(Data As Byte) As Int32
        Return Write(CommandBuilder.Create(Data, False))
    End Function

    Public Overloads Function WriteData(Data As UInt16) As Int32
        Return Write(CommandBuilder.Create(Data, False))
    End Function

    Public Overloads Function WriteData(Data As UInt32) As Int32
        Return Write(CommandBuilder.Create(Data, False))
    End Function

    Public Overloads Function WriteData(Data As UInt64) As Int32
        Return Write(CommandBuilder.Create(Data, False))
    End Function
#End Region

#Region "Signed"
    Public Overloads Function WriteData(Data As SByte) As Int32
        Return Write(CommandBuilder.Create(CByte(Data), True))
    End Function

    Public Overloads Function WriteData(Data As Int16) As Int32
        Return Write(CommandBuilder.Create(CUShort(Data), True))
    End Function

    Public Overloads Function WriteData(Data As Int32) As Int32
        Return Write(CommandBuilder.Create(CUInt(Data), True))
    End Function

    Public Overloads Function WriteData(Data As Int64) As Int32
        Return Write(CommandBuilder.Create(CULng(Data), True))
    End Function
#End Region

#Region "Array"
    Public Overloads Function WriteData(Data As Byte()) As Int32
        If Data Is Nothing Or Data.Length = 0 Or CUInt(Data.Length) > UInt16.MaxValue - 3 Then
            Return wclErrors.WCL_E_INVALID_ARGUMENT
        End If

        Return Write(CommandBuilder.Create(Data))
    End Function

    Public Overloads Function WriteData(Data As String) As Int32
        If Data Is Nothing Or Data.Length = 0 Or CUInt(Data.Length) > UInt16.MaxValue - 3 Then
            Return wclErrors.WCL_E_INVALID_ARGUMENT
        End If

        Return Write(CommandBuilder.Create(Data))
    End Function
#End Region

#Region "Send Error"
    Public Function SendError([Error] As Int32) As Int32
        Return Write(CommandBuilder.Create([Error]))
    End Function
#End Region
#End Region

#Region "Events."
#Region "Received events."
    Public Event OnByteReceived As ByteReceived
    Public Event OnUInt16Received As UInt16Received
    Public Event OnUInt32Received As UInt32Received
    Public Event OnUInt64Received As UInt64Received
    Public Event OnSByteReceived As SByteReceived
    Public Event OnInt16Received As Int16Received
    Public Event OnInt32Received As Int32Received
    Public Event OnInt64Received As Int64Received
    Public Event OnArrayReceived As ArrayReceived
    Public Event OnStringReceived As StringReceived
#End Region

#Region "Get events."
    Public Event OnGetByte As DataEvent
    Public Event OnGetUIn16 As DataEvent
    Public Event OnGetUIn32 As DataEvent
    Public Event OnGetUInt64 As DataEvent
    Public Event OnGetSByte As DataEvent
    Public Event OnGetInt16 As DataEvent
    Public Event OnGetInt32 As DataEvent
    Public Event OnGetInt64 As DataEvent
    Public Event OnGetArray As DataEvent
    Public Event OnGetString As DataEvent
#End Region
#End Region
End Class