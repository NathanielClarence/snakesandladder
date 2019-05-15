Public Class Board
    Private reverse As Boolean = False
    Private players As Integer
    Public startPoint = New Integer() {31, 11, 23, 67}
    Public endPoint = New Integer() {2, 37, 49, 17}

    Public Sub setPlayers(ByVal ply As Integer)
        players = ply
    End Sub

    Public Function getPlayers() As Integer
        Return players
    End Function

    Public Sub setReverse()
        If (reverse = False) Then
            reverse = True
        ElseIf (reverse = True) Then
            reverse = False
        End If
    End Sub

    Public Function getReverse() As Boolean
        Return reverse
    End Function
End Class
