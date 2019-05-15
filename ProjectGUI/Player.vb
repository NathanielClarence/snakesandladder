Imports System.Collections

Public Class Player

    Private Class sortPositionHelper : Implements IComparer
        Function Compare(ByVal a As Object, ByVal b As Object) As Integer Implements IComparer.Compare
            Dim c1 As Player = CType(a, Player)
            Dim c2 As Player = CType(b, Player)

            If (c1.getPos < c2.getPos) Then
                Return 1
            End If

            If (c1.getPos > c2.getPos) Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class


    Dim pos As Integer
    Dim blocked As Boolean
    Dim name As String

    Public Sub setName(ByVal n As String)
        name = n
    End Sub

    Public Function getName() As String
        Return name
    End Function

    Public Function getPos() As Integer
        Return pos
    End Function

    Public Sub reset()
        pos = 1
    End Sub

    Public Sub setPos(ByVal i As Integer)
        pos = pos + i
        If pos < 1 Then
            pos = 1
        ElseIf pos > 70 Then
            pos = 70
        End If
    End Sub

    Public Sub jump(ByVal i As Integer)
        pos = i
    End Sub

    Public Sub initAll()
        pos = 1
        blocked = False
    End Sub

    Public Function getBlock() As Boolean
        Return blocked
    End Function

    Public Sub setBlock()
        If blocked = False Then
            blocked = True
        Else
            blocked = False
        End If
    End Sub

    Public Shared Function sortPos() As IComparer
        Return CType(New sortPositionHelper(), IComparer)
    End Function
End Class
