Public Class Form4
    Dim AList As ArrayList
    Dim players(3) As Player
    Dim activePlayer As Player
    Dim board As Board

    Sub setBoard(ByVal x As Board)
        board = x
    End Sub

    Public Sub leadBoard(ByVal ParamArray playerT() As Player)

        For i As Integer = 0 To board.getPlayers
            players(i) = New Player
            players(i) = playerT(i)
            activePlayer = players(i)
            'Console.WriteLine(activePlayer.getName & vbTab & activePlayer.getPos)
        Next

        For i As Integer = board.getPlayers + 1 To 3
            players(i) = New Player
            activePlayer = players(i)
            activePlayer.initAll()
            activePlayer.jump(0)
            activePlayer.setName("Player " + (i + 1).ToString)
        Next

        Array.Sort(players, Player.sortPos)

        ' For i As Integer = 0 To board.getPlayers
        'activePlayer = players(i)
        'Console.WriteLine(activePlayer.getName & vbTab & activePlayer.getPos)
        'Next
        Me.Show()
        Form1.Hide()
        refreshLead()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub refreshLead()
        For i As Integer = 0 To board.getPlayers
            'players(i) = New Player
            activePlayer = players(i)
            Select Case i
                Case 0
                    Label2.Text = activePlayer.getName()
                    Label6.Text = activePlayer.getPos()
                Case 1
                    Label3.Text = activePlayer.getName()
                    Label7.Text = activePlayer.getPos()
                Case 2
                    Label4.Text = activePlayer.getName()
                    Label8.Text = activePlayer.getPos()
                Case 3
                    Label5.Text = activePlayer.getName()
                    Label9.Text = activePlayer.getPos()
            End Select
        Next
    End Sub
End Class