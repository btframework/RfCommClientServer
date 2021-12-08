Friend Class ClientDataProcessor
    Inherits wclCustomClientDataProcessor

    Private FDecoder As CommandDecoder

#Region "Data decoder events."
    Private Sub FDecoder_OnArrayReceived(Sender As Object, Data As Byte())
        If OnArrayReceivedEvent IsNot Nothing Then
            RaiseEvent OnArrayReceived(Me, Data)
        End If
    End Sub

    Private Sub FDecoder_OnStringReceived(Sender As Object, Data As String)
        If OnStringReceivedEvent IsNot Nothing Then
            RaiseEvent OnStringReceived(Me, Data)
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

    Private Sub FDecoder_OnError(Sender As Object, [Error] As Int32)
        If OnErrorEvent IsNot Nothing Then
            RaiseEvent OnError(Me, [Error])
        End If
    End Sub
#End Region

    Protected Overrides Sub ProcessData(Data As Byte())
        FDecoder.ProcessData(Data)
    End Sub

    Public Sub New(Connection As wclClientDataConnection)
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

        AddHandler FDecoder.OnError, AddressOf FDecoder_OnError

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

        OnErrorEvent = Nothing
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
#End Region

#Region "Get data"
#Region "Sigend"
    Public Function GetSByte() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_SBYTE))
    End Function

    Public Function GetInt16() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_INT16))
    End Function

    Public Function GetInt32() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_INT32))
    End Function

    Public Function GetInt64() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_INT64))
    End Function
#End Region

#Region "Unsigend"
    Public Function GetByte() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_BYTE))
    End Function

    Public Function GetUInt16() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_UINT16))
    End Function

    Public Function GetUInt32() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_UINT32))
    End Function

    Public Function GetUInt64() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_UINT64))
    End Function
#End Region

#Region "Array"
    Public Function GetArray() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_ARRAY))
    End Function

    Public Function GetString() As Int32
        Return Write(CommandBuilder.Create(Commands.CMD_GET_STRING))
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

#Region "Error events."
    Public Event OnError As ErrorEvent
#End Region
#End Region
End Class
