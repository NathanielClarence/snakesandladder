Public Class Form1
    Dim rndNum As Integer 'rand number 4 dice & up/down
    Dim rndCard As Integer 'random card selected
    Dim ply As Integer
    Dim player(3) As Player
    Dim activePlayer As Player
    'Dim turn As Integer = 0 'for testing purpose
    Dim activePly As Integer = 0
    Dim board As Board
    Dim e As Integer
    Dim uname As String
    Dim turns As Integer = 1

    'Sub players(ByVal x As Integer)
    '   ply = x
    'End Sub

    Sub setBoard(ByVal x As Board)
        board = x
        Form4.setBoard(x)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Public Sub newGame()
        ply = board.getPlayers() + 1

        For i As Integer = 0 To ply - 1
            player(i) = New Player
            activePlayer = player(i)
            activePlayer.reset()
            activePlayer.initAll()
            Dim spirits As String = "Please enter name for Player "
            spirits = spirits + (i + 1).ToString + ": "
            uname = InputBox(spirits, "Enter Name", "")
            If uname = "" Then
                uname = "Player " + (i + 1).ToString
            End If
            activePlayer.setName(uname)
            Select Case i
                Case 0
                    Label2.Text = activePlayer.getName()
                    Label6.Text = 1
                Case 1
                    Label3.Text = activePlayer.getName()
                    Label7.Text = 1
                Case 2
                    Label4.Text = activePlayer.getName()
                    Label8.Text = 1
                Case 3
                    Label5.Text = activePlayer.getName()
                    Label9.Text = 1
            End Select
            activePly = i
            updatePos()
        Next
        Button2.Enabled = False
        activePly = 0
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        rndNum = CInt(Math.Ceiling(Rnd() * 6))
        Label1.Text = rndNum
        activePlayer = player(activePly)
        activePlayer.setPos(rndNum)
        If (activePlayer.getPos() >= 70) Then
            activePlayer.jump(70)
            'moving 1st time
            updatePos()
            endGame()
        End If

        updatePos()
        ' Threading.Thread.Sleep(100)

        checkLadSnake()
        updatePos()

        Button2.Enabled = True
        Button1.Enabled = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        rndCard = CInt(Math.Ceiling(Rnd() * 100))
        If (rndCard = 31) Then

            rndCard = CInt(Math.Ceiling(Rnd() * 10))
            If (rndCard < 5) Then
                For i As Integer = 0 To ply - 1
                    activePlayer = player(i)
                    activePlayer.reset()
                Next
                MsgBox("All Player got reset card")
            Else
                activePlayer = player(activePly)
                activePlayer.reset()
                MsgBox("You got a reset card")
            End If

        ElseIf (rndCard > 65) Then

            'move card down
            rndNum = CInt(Math.Ceiling(Rnd() * 5))
            activePlayer = player(activePly)
            activePlayer.setPos(-1 * rndNum)
            MsgBox("Move down " + rndNum.ToString + " steps")
            If (activePlayer.getPos() <= 0) Then
                activePlayer.reset()
                updatePos()
            End If

        ElseIf (rndCard > 31) Then

            'move card up
            rndNum = CInt(Math.Ceiling(Rnd() * 5))
            activePlayer = player(activePly)
            activePlayer.setPos(rndNum)
            MsgBox("Move up " + rndNum.ToString + " steps")
            If (activePlayer.getPos() >= 70) Then
                activePlayer.jump(70)
                updatePos()
                endGame()
            End If

        ElseIf (rndCard < 11) Then

            'stop active player next round
            activePlayer = player(activePly)
            activePlayer.setBlock()
            MsgBox("You cannot move next turn")

        ElseIf (rndCard < 31) Then

            'switch snake and ladder
            board.setReverse()
            If board.getReverse Then
                Console.WriteLine("R")
                MsgBox("Snake and ladder reversed")
            Else
                Console.WriteLine("N")
                MsgBox("Snake and ladder back to normal")
            End If

            For i As Integer = 0 To ply - 1
                activePlayer = player(i)
                checkLadSnake()
                updatePos()

            Next

        End If

            'moving 2nd time
        activePlayer = player(activePly)

        updatePos()
        'Threading.Thread.Sleep(100)

            'check after moving
            checkLadSnake()
            updatePos()

            'change player
            If (activePly < (ply - 1)) Then
                activePly = activePly + 1
            Else
            activePly = 0
            turns += 1
            End If

            endTurn()
    End Sub


    Private Sub endTurn()
        'code here for and others
        Button2.Enabled = False
        Button1.Enabled = True
        activePlayer = player(activePly)
        If activePlayer.getBlock = True Then
            MsgBox(activePlayer.getName() + " cannot move in this round")
            activePlayer.setBlock()
            If (activePly < (ply - 1)) Then
                activePly = activePly + 1
            Else
                activePly = 0
                'turns += 1
            End If
        End If

    End Sub

    Private Sub endGame()

        MsgBox("We have a winner!!")
        Form4.leadBoard(player)
        Console.WriteLine("This one is done")
    End Sub

    Private Sub updatePos()
        Select Case activePly
            Case 0
                activePlayer = player(0)
                Label6.Text = activePlayer.getPos()
                moveIt(activePly, CInt(Label6.Text))
            Case 1
                activePlayer = player(1)
                Label7.Text = activePlayer.getPos()
                moveIt(activePly, CInt(Label7.Text))
            Case 2
                activePlayer = player(2)
                Label8.Text = activePlayer.getPos()
                moveIt(activePly, CInt(Label8.Text))
            Case 3
                activePlayer = player(3)
                Label9.Text = activePlayer.getPos()
                moveIt(activePly, CInt(Label9.Text))
        End Select


    End Sub

    Private Sub checkLadSnake()
        For i As Integer = 0 To 3
            If board.getReverse() = False Then
                If activePlayer.getPos() = board.startPoint(i) Then
                    activePlayer.jump(board.endPoint(i))
                    Threading.Thread.Sleep(200)
                    Exit For
                End If
            Else
                If activePlayer.getPos() = board.endPoint(i) Then
                    activePlayer.jump(board.startPoint(i))
                    Threading.Thread.Sleep(200)
                    Exit For
                End If
            End If
        Next
    End Sub

    'ready to move to Uctrl
    Dim modulo As Integer
    Dim divide As Integer
    Dim posX As Integer
    Dim posY As Integer
    Dim baseX = New Integer() {3, 44, 3, 44}
    Dim baseY = New Integer() {346, 346, 387, 387}
    Dim reverseX = New Integer() {946, 987, 946, 987}

    Private Sub moveIt(ByVal player As Integer, ByVal steps As Integer)
        modulo = steps Mod 14
        divide = steps \ 14

        divide = Math.Floor(divide)
        modulo = Math.Floor(modulo)

        Select Case divide
            Case 0, 2, 4
                If modulo = 0 Then
                    posX = baseX(player)
                    divide -= 1
                Else
                    posX = baseX(player) + ((modulo - 1) * 86)
                End If
                posY = baseY(player) - ((divide) * 86)
            Case 1, 3
                If modulo = 0 Then
                    'masih error
                    posX = reverseX(player)
                    divide -= 1
                Else
                    posX = reverseX(player) - ((modulo - 3) * 86)
                End If
                posY = baseY(player) - ((divide) * 86)
        End Select

        Select Case player
            Case 0
                'oldPoint = BoardControl1.PictureBox1.Location
                newPoint = New Point(posX, posY)
                Console.WriteLine("asdf")
                'an()

                'Threading.Thread.Sleep(1000)
                BoardControl1.PictureBox1.Location = newPoint

                Console.WriteLine(oldPoint.X.ToString + "  x  " + oldPoint.Y.ToString)
                Console.WriteLine(posX.ToString + "   " + posY.ToString)
                Timer1.Enabled = False
            Case 1
                oldPoint = BoardControl1.PictureBox1.Location
                BoardControl1.PictureBox2.Location = New Point(posX, posY)
                Console.WriteLine(posX.ToString + "   " + posY.ToString)
            Case 2
                oldPoint = BoardControl1.PictureBox1.Location
                BoardControl1.PictureBox3.Location = New Point(posX, posY)
                Console.WriteLine(posX.ToString + "   " + posY.ToString)
            Case 3
                oldPoint = BoardControl1.PictureBox1.Location
                BoardControl1.PictureBox4.Location = New Point(posX, posY)
                Console.WriteLine(posX.ToString + "   " + posY.ToString)
        End Select

        BoardControl1.PictureBox1.Refresh()
        BoardControl1.PictureBox2.Refresh()
        BoardControl1.PictureBox3.Refresh()
        BoardControl1.PictureBox4.Refresh()
    End Sub

    'unimplemented
    Dim moveX As Integer
    Dim moveY As Integer
    Dim rX As Integer
    Dim rY As Integer
    Dim oldPoint As Point
    Dim newPoint As Point

    Private Sub an()
        oldPoint = BoardControl1.PictureBox1.Location
        newPoint = New Point(posX, posY)

        moveX = newPoint.X - oldPoint.X
        moveY = newPoint.Y - oldPoint.Y
        rX = Math.Ceiling(moveX \ 10)
        rY = Math.Ceiling(moveY \ 10)
        Console.WriteLine("draw")
        Timer1.Interval = 100

        Console.WriteLine("time")
        Timer1.Enabled = True

        Console.WriteLine("break")
        
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Select Case activePly
            Case 0
                newPoint = New Point((oldPoint.X + rX), (oldPoint.Y + rY))
                oldPoint = New Point(newPoint.X, newPoint.Y)
                BoardControl1.PictureBox1.Location = newPoint

                'Console.WriteLine(oldPoint.X.ToString + "  x  " + oldPoint.Y.ToString)
                'Console.WriteLine(posX.ToString + "   " + posY.ToString)
                Console.WriteLine("repeat")
            Case 1
                oldPoint = BoardControl1.PictureBox1.Location
                BoardControl1.PictureBox2.Location = New Point(posX, posY)
                'Console.WriteLine(posX.ToString + "   " + posY.ToString)
            Case 2
                oldPoint = BoardControl1.PictureBox1.Location
                BoardControl1.PictureBox3.Location = New Point(posX, posY)
                'Console.WriteLine(posX.ToString + "   " + posY.ToString)
            Case 3
                oldPoint = BoardControl1.PictureBox1.Location
                BoardControl1.PictureBox4.Location = New Point(posX, posY)
                'Console.WriteLine(posX.ToString + "   " + posY.ToString)
        End Select
    End Sub
End Class
