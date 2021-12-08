#Region "Delegates"
#Region "Data received delegates"
Friend Delegate Sub ByteReceived(Sender As Object, Data As Byte)
Friend Delegate Sub UInt16Received(Sender As Object, Data As UInt16)
Friend Delegate Sub UInt32Received(Sender As Object, Data As UInt32)
Friend Delegate Sub UInt64Received(Sender As Object, Data As UInt64)
Friend Delegate Sub SByteReceived(Sender As Object, Data As SByte)
Friend Delegate Sub Int16Received(Sender As Object, Data As Int16)
Friend Delegate Sub Int32Received(Sender As Object, Data As Int32)
Friend Delegate Sub Int64Received(Sender As Object, Data As Int64)
Friend Delegate Sub ArrayReceived(Sender As Object, Data() As Byte)
Friend Delegate Sub StringReceived(Sender As Object, Data As String)
#End Region

Friend Delegate Sub DataEvent(Sender As Object)

Friend Delegate Sub ErrorEvent(Sender As Object, [Error] As Int32)
#End Region

Friend Module Commands
#Region "Command flags"
    Public Const CMD_FLAG As Byte = &HF

    Public Const CMD_FLAG_SEND As Byte = &H0
    Public Const CMD_FLAG_GET As Byte = &H40
    Public Const CMD_FLAG_ERROR As Byte = &H80
#End Region

#Region "Commands."
    Public Const CMD_BYTE As Byte = &H1
    Public Const CMD_UINT16 As Byte = &H2
    Public Const CMD_UINT32 As Byte = &H3
    Public Const CMD_UINT64 As Byte = &H4
    Public Const CMD_SBYTE As Byte = &H5
    Public Const CMD_INT16 As Byte = &H6
    Public Const CMD_INT32 As Byte = &H7
    Public Const CMD_INT64 As Byte = &H8
    Public Const CMD_ARRAY As Byte = &H9
    Public Const CMD_STRING As Byte = &HA

    Public Const CMD_ERROR As Byte = &HF
#End Region

#Region "Send commands."
    Public Const CMD_SEND_BYTE As Byte = CMD_FLAG_SEND Or CMD_BYTE
    Public Const CMD_SEND_UINT16 As Byte = CMD_FLAG_SEND Or CMD_UINT16
    Public Const CMD_SEND_UINT32 As Byte = CMD_FLAG_SEND Or CMD_UINT32
    Public Const CMD_SEND_UINT64 As Byte = CMD_FLAG_SEND Or CMD_UINT64
    Public Const CMD_SEND_SBYTE As Byte = CMD_FLAG_SEND Or CMD_SBYTE
    Public Const CMD_SEND_INT16 As Byte = CMD_FLAG_SEND Or CMD_INT16
    Public Const CMD_SEND_INT32 As Byte = CMD_FLAG_SEND Or CMD_INT32
    Public Const CMD_SEND_INT64 As Byte = CMD_FLAG_SEND Or CMD_INT64
    Public Const CMD_SEND_ARRAY As Byte = CMD_FLAG_SEND Or CMD_ARRAY
    Public Const CMD_SEND_STRING As Byte = CMD_FLAG_SEND Or CMD_STRING
#End Region

#Region "Get commands."
    Public Const CMD_GET_BYTE As Byte = CMD_FLAG_GET Or CMD_BYTE
    Public Const CMD_GET_UINT16 As Byte = CMD_FLAG_GET Or CMD_UINT16
    Public Const CMD_GET_UINT32 As Byte = CMD_FLAG_GET Or CMD_UINT32
    Public Const CMD_GET_UINT64 As Byte = CMD_FLAG_GET Or CMD_UINT64
    Public Const CMD_GET_SBYTE As Byte = CMD_FLAG_GET Or CMD_SBYTE
    Public Const CMD_GET_INT16 As Byte = CMD_FLAG_GET Or CMD_INT16
    Public Const CMD_GET_INT32 As Byte = CMD_FLAG_GET Or CMD_INT32
    Public Const CMD_GET_INT64 As Byte = CMD_FLAG_GET Or CMD_INT64
    Public Const CMD_GET_ARRAY As Byte = CMD_FLAG_GET Or CMD_ARRAY
    Public Const CMD_GET_STRING As Byte = CMD_FLAG_GET Or CMD_STRING
#End Region

#Region "Error commands."
    Public Const CMD_ERROR_CODE As Byte = CMD_FLAG_ERROR Or CMD_ERROR
#End Region

#Region "Command checkers"
    Public Function IsCmdSend(Cmd As Byte) As Boolean
        Return ((Cmd And CMD_FLAG_GET) = 0 And (Cmd And CMD_FLAG_ERROR) = 0)
    End Function

    Public Function IsCmdGet(Cmd As Byte) As Boolean
        Return ((Cmd And CMD_FLAG_GET) <> 0)
    End Function

    Public Function IsCmdError(Cmd As Byte) As Boolean
        Return ((Cmd And CMD_FLAG_ERROR) <> 0)
    End Function
#End Region
End Module
Friend Module CommandBuilder
    Public Function Create(Data As Byte, Signed As Boolean) As Byte()
        Dim Cmd As Byte() = New Byte(3) {}
        Cmd(0) = &H0
        Cmd(1) = &H4
        If Signed Then
            Cmd(2) = Commands.CMD_SEND_SBYTE
        Else
            Cmd(2) = Commands.CMD_SEND_BYTE
        End If
        Cmd(3) = Data
        Return Cmd
    End Function

    Public Function Create(Data As UInt16, Signed As Boolean) As Byte()
        Dim Cmd As Byte() = New Byte(4) {}
        Cmd(0) = &H0
        Cmd(1) = &H5
        If Signed Then
            Cmd(2) = Commands.CMD_SEND_INT16
        Else
            Cmd(2) = Commands.CMD_SEND_UINT16
        End If
        Cmd(3) = wclHelpers.HiByte(Data)
        Cmd(4) = wclHelpers.LoByte(Data)
        Return Cmd
    End Function

    Public Function Create(Data As UInt32, Signed As Boolean) As Byte()
        Dim Cmd As Byte() = New Byte(6) {}
        Cmd(0) = &H0
        Cmd(1) = &H7
        If Signed Then
            Cmd(2) = Commands.CMD_SEND_INT32
        Else
            Cmd(2) = Commands.CMD_SEND_UINT32
        End If
        Cmd(3) = wclHelpers.HiByte(wclHelpers.HiWord(Data))
        Cmd(4) = wclHelpers.LoByte(wclHelpers.HiWord(Data))
        Cmd(5) = wclHelpers.HiByte(wclHelpers.LoWord(Data))
        Cmd(6) = wclHelpers.LoByte(wclHelpers.LoWord(Data))
        Return Cmd
    End Function

    Public Function Create(Data As UInt64, Signed As Boolean) As Byte()
        Dim Hi As UInt32 = CUInt(Data >> 32)
        Dim Lo As UInt32 = CUInt(Data And &HFFFFFFFFUI)
        Dim Cmd As Byte() = New Byte(10) {}
        Cmd(0) = &H0
        Cmd(1) = &HB
        If Signed Then
            Cmd(2) = Commands.CMD_SEND_INT64
        Else
            Cmd(2) = Commands.CMD_SEND_UINT64
        End If
        Cmd(3) = wclHelpers.HiByte(wclHelpers.HiWord(Hi))
        Cmd(4) = wclHelpers.LoByte(wclHelpers.HiWord(Hi))
        Cmd(5) = wclHelpers.HiByte(wclHelpers.LoWord(Hi))
        Cmd(6) = wclHelpers.LoByte(wclHelpers.LoWord(Hi))
        Cmd(7) = wclHelpers.HiByte(wclHelpers.HiWord(Lo))
        Cmd(8) = wclHelpers.LoByte(wclHelpers.HiWord(Lo))
        Cmd(9) = wclHelpers.HiByte(wclHelpers.LoWord(Lo))
        Cmd(10) = wclHelpers.LoByte(wclHelpers.LoWord(Lo))
        Return Cmd
    End Function

    Public Function Create(Data As Byte()) As Byte()
        Dim Len As UInt16 = CUShort(3 + Data.Length)
        Dim Cmd As Byte() = New Byte(Len - 1) {}
        Cmd(0) = wclHelpers.HiByte(Len)
        Cmd(1) = wclHelpers.LoByte(Len)
        Cmd(2) = Commands.CMD_SEND_ARRAY
        Array.Copy(Data, 0, Cmd, 3, Data.Length)
        Return Cmd
    End Function

    Public Function Create(Data As String) As Byte()
        Dim Str As Byte() = Text.Encoding.UTF8.GetBytes(Data)
        Dim Len As UInt16 = CUShort(3 + Str.Length)
        Dim Cmd As Byte() = New Byte(Len - 1) {}
        Cmd(0) = wclHelpers.HiByte(Len)
        Cmd(1) = wclHelpers.LoByte(Len)
        Cmd(2) = Commands.CMD_SEND_STRING
        Array.Copy(Str, 0, Cmd, 3, Str.Length)
        Return Cmd
    End Function

    Public Function Create([Error] As Int32) As Byte()
        Dim Cmd As Byte() = New Byte(6) {}
        Cmd(0) = &H0
        Cmd(1) = &H7
        Cmd(2) = Commands.CMD_ERROR_CODE
        Cmd(3) = wclHelpers.HiByte(wclHelpers.HiWord(CUInt([Error])))
        Cmd(4) = wclHelpers.LoByte(wclHelpers.HiWord(CUInt([Error])))
        Cmd(5) = wclHelpers.HiByte(wclHelpers.LoWord(CUInt([Error])))
        Cmd(6) = wclHelpers.LoByte(wclHelpers.LoWord(CUInt([Error])))
        Return Cmd
    End Function

    Public Function Create(Cmd As Byte) As Byte()
        Dim Data As Byte() = New Byte(2) {}
        Data(0) = &H0
        Data(1) = &H3
        Data(2) = Cmd
        Return Data
    End Function
End Module

Friend Class CommandDecoder
    Private FBuffer As Byte()

    Private Sub DoByteReceived(Data As Byte)
        If OnByteReceivedEvent IsNot Nothing Then
            RaiseEvent OnByteReceived(Me, Data)
        End If
    End Sub

    Private Sub DoUInt16Received(Data As UInt16)
        If OnUInt16ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt16Received(Me, Data)
        End If
    End Sub

    Private Sub DoUInt32Received(Data As UInt32)
        If OnUInt32ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt32Received(Me, Data)
        End If
    End Sub

    Private Sub DoUInt64Received(Data As UInt64)
        If OnUInt64ReceivedEvent IsNot Nothing Then
            RaiseEvent OnUInt64Received(Me, Data)
        End If
    End Sub

    Private Sub DoSByteReceived(Data As SByte)
        If OnSByteReceivedEvent IsNot Nothing Then
            RaiseEvent OnSByteReceived(Me, Data)
        End If
    End Sub

    Private Sub DoInt16Received(Data As Int16)
        If OnInt16ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt16Received(Me, Data)
        End If
    End Sub

    Private Sub DoInt32Received(Data As Int32)
        If OnInt32ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt32Received(Me, Data)
        End If
    End Sub

    Private Sub DoInt64Received(Data As Int64)
        If OnInt64ReceivedEvent IsNot Nothing Then
            RaiseEvent OnInt64Received(Me, Data)
        End If
    End Sub

    Private Sub DoArrayReceived(Data As Byte())
        If OnArrayReceivedEvent IsNot Nothing Then
            RaiseEvent OnArrayReceived(Me, Data)
        End If
    End Sub

    Private Sub DoStringReceived(Data As String)
        If OnStringReceivedEvent IsNot Nothing Then
            RaiseEvent OnStringReceived(Me, Data)
        End If
    End Sub

    Private Sub DoGetByte()
        If OnGetByteEvent IsNot Nothing Then
            RaiseEvent OnGetByte(Me)
        End If
    End Sub

    Private Sub DoGetUInt16()
        If OnGetUIn16Event IsNot Nothing Then
            RaiseEvent OnGetUIn16(Me)
        End If
    End Sub

    Private Sub DoGetUInt32()
        If OnGetUIn32Event IsNot Nothing Then
            RaiseEvent OnGetUIn32(Me)
        End If
    End Sub

    Private Sub DoGetUInt64()
        If OnGetUInt64Event IsNot Nothing Then
            RaiseEvent OnGetUInt64(Me)
        End If
    End Sub

    Private Sub DoGetSByte()
        If OnGetSByteEvent IsNot Nothing Then
            RaiseEvent OnGetSByte(Me)
        End If
    End Sub

    Private Sub DoGetInt16()
        If OnGetInt16Event IsNot Nothing Then
            RaiseEvent OnGetInt16(Me)
        End If
    End Sub

    Private Sub DoGetInt32()
        If OnGetInt32Event IsNot Nothing Then
            RaiseEvent OnGetInt32(Me)
        End If
    End Sub

    Private Sub DoGetInt64()
        If OnGetInt64Event IsNot Nothing Then
            RaiseEvent OnGetInt64(Me)
        End If
    End Sub

    Private Sub DoGetArray()
        If OnGetArrayEvent IsNot Nothing Then
            RaiseEvent OnGetArray(Me)
        End If
    End Sub

    Private Sub DoGetString()
        If OnGetStringEvent IsNot Nothing Then
            RaiseEvent OnGetString(Me)
        End If
    End Sub

    Private Sub DoError([Error] As Int32)
        If OnErrorEvent IsNot Nothing Then
            RaiseEvent OnError(Me, [Error])
        End If
    End Sub

    Private Sub DecodeSendCommand(Data As Byte())
        Select Case Data(0) And Commands.CMD_FLAG
            Case Commands.CMD_BYTE
                If Data.Length = 2 Then
                    DoByteReceived(Data(1))
                End If

            Case Commands.CMD_UINT16
                If Data.Length = 3 Then
                    DoUInt16Received(CUShort((CUShort(Data(1)) << 8) Or Data(2)))
                End If

            Case Commands.CMD_UINT32
                If Data.Length = 5 Then
                    DoUInt32Received(CUInt((CUInt(Data(1)) << 24) Or (CUInt(Data(2)) << 16) Or
                                     (CUInt(Data(3)) << 8) Or Data(4)))
                End If

            Case Commands.CMD_UINT64
                If Data.Length = 9 Then
                    DoUInt64Received(CULng((CULng(Data(1)) << 56) Or (CULng(Data(2)) << 48) Or
                                     (CULng(Data(3)) << 40) Or (CULng(Data(4)) << 32) Or (CULng(Data(5)) << 24) Or
                                     (CULng(Data(6)) << 16) Or (CULng(Data(7)) << 8) Or CULng(Data(8))))
                End If

            Case Commands.CMD_SBYTE
                If Data.Length = 2 Then
                    DoSByteReceived(CSByte(Data(1)))
                End If

            Case Commands.CMD_INT16
                If Data.Length = 3 Then
                    DoInt16Received(CShort((CShort(Data(1)) << 8)) Or Data(2))
                End If

            Case Commands.CMD_INT32
                If Data.Length = 5 Then
                    DoInt32Received(CInt((CInt(Data(1)) << 24) Or (CInt(Data(2)) << 16) Or
                                    (CInt(Data(3)) << 8) Or Data(4)))
                End If

            Case Commands.CMD_INT64
                If Data.Length = 9 Then
                    DoInt64Received(CLng((CLng(Data(1)) << 56) Or (CLng(Data(2)) << 48) Or
                            (CLng(Data(3)) << 40) Or (CLng(Data(4)) << 32) Or (CLng(Data(5)) << 24) Or
                            (CLng(Data(6)) << 16) Or (CLng(Data(7)) << 8) Or CLng(Data(8))))
                End If

            Case Commands.CMD_ARRAY
                If Data.Length > 1 Then
                    Dim Arr As Byte() = New Byte(Data.Length - 2) {}
                    Array.Copy(Data, 1, Arr, 0, Data.Length - 1)
                    DoArrayReceived(Arr)
                End If

            Case Commands.CMD_STRING
                If Data.Length > 1 Then
                    Dim Str As String = Text.Encoding.UTF8.GetString(Data, 1, Data.Length - 1)
                    DoStringReceived(Str)
                End If
        End Select
    End Sub

    Private Sub DecodeGetCommand(Data As Byte())
        If Data.Length = 1 Then
            Select Case Data(0) And Commands.CMD_FLAG
                Case Commands.CMD_BYTE
                    DoGetByte()

                Case Commands.CMD_UINT16
                    DoGetUInt16()

                Case Commands.CMD_UINT32
                    DoGetUInt32()

                Case Commands.CMD_UINT64
                    DoGetUInt64()

                Case Commands.CMD_SBYTE
                    DoGetSByte()

                Case Commands.CMD_INT16
                    DoGetInt16()

                Case Commands.CMD_INT32
                    DoGetInt32()

                Case Commands.CMD_INT64
                    DoGetInt64()

                Case Commands.CMD_ARRAY
                    DoGetArray()

                Case Commands.CMD_STRING
                    DoGetString()
            End Select
        End If
    End Sub

    Private Sub DecodeErrorCommand(Data As Byte())
        If Data.Length = 5 Then
            If Data(0) = Commands.CMD_ERROR_CODE Then
                Dim [Error] As Int32 = (CInt(Data(1)) << 24) Or (CInt(Data(2)) << 16) Or
                    (CInt(Data(3)) << 8) Or Data(4)
                DoError([Error])
            End If
        End If
    End Sub

    Private Sub DecodeData(Data As Byte())
        ' It Is guaranteed that the Data length at least 2 bytes.
        Dim Cmd As Byte = Data(0)
        If Commands.IsCmdSend(Cmd) Then
            DecodeSendCommand(Data)
        Else
            If Commands.IsCmdGet(Cmd) Then
                DecodeGetCommand(Data)
            Else
                If Commands.IsCmdError(Cmd) Then
                    DecodeErrorCommand(Data)
                End If
            End If
        End If
    End Sub

    Private Sub DataReceived()
        While FBuffer IsNot Nothing And FBuffer.Length > 2
            ' Data length includes length bytes!
            Dim Len As UInt16 = CUShort((CUShort(FBuffer(0)) << 8) Or FBuffer(1))
            If Len > FBuffer.Length Then
                Exit While
            End If

            Dim Data As Byte() = New Byte(Len - 3) {}
            Array.Copy(FBuffer, 2, Data, 0, Len - 2)
            DecodeData(Data)

            If Len = FBuffer.Length Then
                FBuffer = Nothing
                Exit While
            End If

            Data = New Byte(FBuffer.Length - Len - 1) {}
            Array.Copy(FBuffer, Len, Data, 0, FBuffer.Length - Len)
            FBuffer = Data
        End While
    End Sub

    Public Sub New()
        FBuffer = Nothing

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

        OnErrorEvent = Nothing
    End Sub

    Public Sub ProcessData(Data As Byte())
        If Data IsNot Nothing And Data.Length > 0 Then
            Dim Ndx As Int32
            If FBuffer Is Nothing Then
                FBuffer = New Byte(Data.Length - 1) {}
                Ndx = 0
            Else
                Ndx = FBuffer.Length
                Array.Resize(FBuffer, Ndx + Data.Length)
            End If
            Array.Copy(Data, 0, FBuffer, Ndx, Data.Length)
            DataReceived()
        End If
    End Sub

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

    Public Event OnError As ErrorEvent
End Class
