Public Class Form3
    Dim board As Board

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        board = New Board
        board.setPlayers(1)
        Form1.setBoard(board)
        Me.Hide()
        Form1.newGame()
        Form1.Show()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        board = New Board
        board.setPlayers(2)
        Form1.BoardControl1.PictureBox3.Visible = True
        Form1.setBoard(board)
        Me.Hide()
        Form1.newGame()
        Form1.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        board = New Board
        board.setPlayers(3)
        Form1.BoardControl1.PictureBox3.Visible = True
        Form1.BoardControl1.PictureBox4.Visible = True
        Form1.setBoard(board)
        Me.Hide()
        Form1.newGame()
        Form1.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form2.Show()
        Me.Hide()
    End Sub
End Class