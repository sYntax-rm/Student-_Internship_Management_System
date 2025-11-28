Public Class FormLogin
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        roundCorners(Me)
        pnlForgotPass.Hide()

    End Sub
    Private Sub roundCorners(obj As Form)

        Dim DGP As New Drawing2D.GraphicsPath
        DGP.StartFigure()
        'top left corners
        DGP.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        DGP.AddLine(40, 0, obj.Width - 40, 0)

        'top right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, 0, 40, 40), -90, 90)
        DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)

        'buttom right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90)
        DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)

        'buttom left corner
        DGP.AddArc(New Rectangle(0, obj.Height - 40, 40, 40), 90, 90)
        DGP.CloseFigure()

        obj.Region = New Region(DGP)
    End Sub

    Private Function validNewInfoInput()
        If txtResetEmail.Text = "" OrElse txtNewPassword.Text = "" OrElse txtOldPassword.Text = "" Then
            MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If Not txtResetEmail.Text.Contains("@gmail.com") Then
            MessageBox.Show("Email must be a Gmail address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmailLogIn.Clear()

            Return False
        End If

        If txtOldPassword.Text.Length < 8 Then
            MessageBox.Show("Invalid Length, Old Password Must be Above 8 Characters",
                            "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPasswordLogIn.Clear()
            Return False
        End If

        If txtNewPassword.Text.Length < 8 Then
            MessageBox.Show("Weak New Password, Must be above 8 Characters",
                            "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPasswordLogIn.Clear()
            Return False
        End If

        Return True

    End Function

    Private Function validLogInInput() As Boolean
        If txtEmailLogIn.Text = "admin" And txtPasswordLogIn.Text = "admin" Then
            Return True
        End If

        If txtEmailLogIn.Text = "" OrElse txtPasswordLogIn.Text = "" Then
            MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If Not txtEmailLogIn.Text.Contains("@gmail.com") Then
            MessageBox.Show("Email must be a Gmail address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmailLogIn.Clear()

            Return False
        End If

        If txtPasswordLogIn.Text.Length < 8 Then
            MessageBox.Show("Invalid Length, Password Must be Above 8 Characters",
                            "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPasswordLogIn.Clear()
            Return False
        End If

        Return True
    End Function

    Private Sub pctBox3_Click(sender As Object, e As EventArgs) Handles pctBoxExit1.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBox4_Click(sender As Object, e As EventArgs) Handles pctBoxHide1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lnklblForgotPass_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblChangePass.LinkClicked
        pnlForgotPass.Show()
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If Not validNewInfoInput() Then Exit Sub

        If Not validAccount(txtResetEmail.Text, txtOldPassword.Text) Then
            MessageBox.Show("Incorrect Email or Old Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtResetEmail.Clear()
            txtOldPassword.Clear()
            Exit Sub
        End If

        If Not resetAccount(txtResetEmail.Text, txtNewPassword.Text) Then
            MessageBox.Show("Cannot Found your account", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtResetEmail.Clear()
            txtOldPassword.Clear()
            txtNewPassword.Clear()
            Exit Sub
        End If

        MessageBox.Show("Successfully Updated Password", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
        txtResetEmail.Clear()
        txtOldPassword.Clear()
        txtNewPassword.Clear()

    End Sub

    Private Sub lnklblLogIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblLogIn.LinkClicked
        pnlForgotPass.Hide()
    End Sub

    Private Sub lnklblSignUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnklblSignUp.LinkClicked
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub pctBox7_Click(sender As Object, e As EventArgs) Handles pctBoxExit.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBox6_Click(sender As Object, e As EventArgs) Handles pctBoxHide.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Not validLogInInput() Then Exit Sub



        If Not validAccount(txtEmailLogIn.Text, txtPasswordLogIn.Text) Then
            MessageBox.Show("Incorrect Email or Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmailLogIn.Clear()
            txtPasswordLogIn.Clear()
            Exit Sub
        End If

        MessageBox.Show("Successfully Log in, Welcome!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        txtEmailLogIn.Clear()
        txtPasswordLogIn.Clear()
        Form3.Show()
        Me.Hide()

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
