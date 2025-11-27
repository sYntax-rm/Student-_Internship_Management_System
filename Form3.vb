Imports MySql.Data.MySqlClient
Imports Mysqlx.Notice

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Student Icon for Button
        Dim picStudentIcon As New PictureBox()

        picStudentIcon.Name = "picStudentIcon"
        picStudentIcon.Size = New Size(32, 32)
        picStudentIcon.Location = New Point(5, 5)
        picStudentIcon.SizeMode = PictureBoxSizeMode.Zoom
        picStudentIcon.BackColor = Color.Transparent

        'Nilagay ko
        btnStudents.Controls.Add(picStudentIcon)
        picStudentIcon.BringToFront()

        'Button for Hover
        ' Button style
        btnStudents.FlatStyle = FlatStyle.Flat
        btnStudents.UseVisualStyleBackColor = False
        btnStudents.FlatAppearance.BorderSize = 0
        btnStudents.FlatAppearance.MouseOverBackColor = Color.DarkSeaGreen
        btnStudents.FlatAppearance.MouseDownBackColor = Color.Honeydew
        btnStudents.BackColor = Color.FromArgb(188, 221, 203)

        'Panel add Edit Location - STUDENT
        'Add
        pnlAddNewStudentRecord.Location = New Point(90, 78)
        pnlAddNewStudentRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditStudentRecord.Location = New Point(90, 78)
        pnlEditStudentRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Evaluation
        'Add
        pnlAddNewInternshipEvaluationRecord.Location = New Point(90, 78)
        pnlAddNewInternshipEvaluationRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditInternshipEvaluationRecord.Location = New Point(90, 78)
        pnlEditInternshipEvaluationRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Company
        'Add
        pnlAddNewCompanyRecord.Location = New Point(90, 78)
        pnlAddNewCompanyRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditCompanyRecord.Location = New Point(90, 78)
        pnlEditCompanyRecord.Size = New Size(1100, 750)


        'Panel add Edit Location - Sub Buttons
        'Add
        pnlAddNewCompanyandCompanyContact.Location = New Point(317, 256)
        pnlAddNewCompanyandCompanyContact.Size = New Size(565, 340)
        'Edit
        pnlEditCompanyCompanyContact.Location = New Point(317, 256)
        pnlEditCompanyCompanyContact.Size = New Size(565, 340)

        'Panel add Edit Location - Company Contact
        'Add
        pnlAddNewCompanyContactRecord.Location = New Point(90, 78)
        pnlAddNewCompanyContactRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditCompanyContactRecord.Location = New Point(90, 78)
        pnlEditCompanyContactRecord.Size = New Size(1100, 750)

        'Panel add Edit Location - Faculty
        'Add
        pnlAddNewFacultyRecord.Location = New Point(90, 78)
        pnlAddNewFacultyRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditFacultyRecord.Location = New Point(90, 78)
        pnlEditFacultyRecord.Size = New Size(1100, 750)

        'For buttons
        hidePanel()


    End Sub
    Private Sub hidePanel()
        pnlStudentInformation.Hide()
        pnlAddNewStudentRecord.Hide()
        pnlEditStudentRecord.Hide()
        pnlInternshipInformation.Hide()
        pnlEvaluationInformation.Hide()
        pnlAddNewInternshipEvaluationRecord.Hide()
        pnlEditInternshipEvaluationRecord.Hide()
        pnlCompanyInformation.Hide()
        pnlAddNewCompanyandCompanyContact.Hide()
        pnlEditCompanyCompanyContact.Hide()
        pnlAddNewCompanyRecord.Hide()
        pnlEditCompanyRecord.Hide()
        pnlCompanyContactInformation.Hide()
        pnlAddNewCompanyContactRecord.Hide()
        pnlEditCompanyContactRecord.Hide()
        pnlFacultyInformation.Hide()
        pnlAddNewFacultyRecord.Hide()
        pnlEditFacultyRecord.Hide()
        pnlVisitInformation.Hide()
        pnlAddNewVisitRecord.Hide()
        pnlEditVisitRecord.Hide()
        pnlSummaryReport.Hide()
    End Sub

    Private Sub clearBox(ParamArray boxes() As TextBox)
        For Each txt In boxes
            txt.Clear()

        Next

    End Sub

    Private Sub loadStudentRecord()
        Dim dt As DataTable = loadTable("SELECT 
                                                s.student_id AS 'Student ID',
                                                s.first_name AS 'First Name',
                                                s.last_name AS 'Last Name',
                                                s.gender AS 'Gender',
                                                s.section_name AS 'Section',
                                                s.contact_no AS 'Contact Number',
                                                s.email AS 'Email',
                                                d.department_name AS 'Department',
                                                c.course_name AS 'Course'
                                                FROM student s
                                                INNER JOIN department d ON s.department_id = d.department_id
                                                INNER JOIN course c ON s.course_id = c.course_id;"
                                               )

        If Not dt.Columns.Contains("Hidden") Then
            dt.Columns.Add("Hidden", GetType(Boolean))
        End If

        ' Default all rows to not hidden
        For Each row As DataRow In dt.Rows
            row("Hidden") = False
        Next

        ' Bind the DataTable to the DataGridView using a DataView
        Dim dv As New DataView(dt)
        dv.RowFilter = "Hidden = False"
        dgvStudentFiles.DataSource = dv
        dgvStudentFiles.Columns("Hidden").Visible = False
    End Sub

    Private Function validStudentInputBx(email As TextBox, ParamArray txtBoxes() As TextBox) As Boolean

        For Each tb In txtBoxes
            If tb.Text = "" Or email.Text = "" Then
                MessageBox.Show("Please Fill Out The Boxes", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If Not email.Text.Contains("@plpasig.edu.ph") Then
            MessageBox.Show("Email must be a School email", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmail2.Clear()
            Return False
        End If

        Return True
    End Function

    Private Sub loadGenderComboBx(cmb As ComboBox)
        Dim dt As New DataTable()
        dt.Columns.Add("value")
        dt.Rows.Add("Male")
        dt.Rows.Add("Female")
        dt.Rows.Add("Other")

        cmb.DataSource = dt
        cmb.DisplayMember = "value"
        cmb.ValueMember = "value"
    End Sub

    Private Sub loadDepartmentComboBx(cmb As ComboBox)
        Dim dt As DataTable = LoadCombo("SELECT department_id, department_name FROM department")

        cmb.DataSource = dt
        cmb.DisplayMember = "department_name" 'what user sees
        cmb.ValueMember = "department_id"     'actual value you use in DB
    End Sub

    Private Sub loadSectionComboBx(cmb As ComboBox)
        Dim dt As DataTable = LoadCombo("SELECT section_name FROM section")

        cmb.DataSource = dt
        cmb.DisplayMember = "section_name" 'what user sees
        cmb.ValueMember = "section_name"   'actual value you use in DB
    End Sub

    Private Sub loadCourseComboBx(cmb As ComboBox)
        Dim dt As DataTable = LoadCombo("SELECT course_id, course_name FROM course")

        cmb.DataSource = dt
        cmb.DisplayMember = "course_name" 'what user sees
        cmb.ValueMember = "course_id"   'actual value you use in DB

    End Sub


    Private Sub PictureBox3_Click_1(sender As Object, e As EventArgs)
        OpenFileDialog1.Filter = "images files(*.bmp; *.jpg; *.png) | *.bmp; *.jpg; *.png"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            pctBoxIcon.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub


    'Home

    Private Sub pctBoxExit1_Click(sender As Object, e As EventArgs) Handles pctBoxExit1.Click
        Dim result = MessageBox.Show("Are you sure you want to exit the program?",
                                     "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub pctBoxHide1_Click(sender As Object, e As EventArgs) Handles pctBoxHide1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim result As DialogResult = MessageBox.Show(
        "Are you sure you want to log out?",
        "Confirm Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result = DialogResult.Yes Then
            ' Show login form
            Form1.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub lblName_Click(sender As Object, e As EventArgs) Handles lblName.Click

    End Sub

    Private Sub lblSignInAs_Click(sender As Object, e As EventArgs) Handles lblSignInAs.Click

    End Sub

    Private Sub pctBoxIcon_Click(sender As Object, e As EventArgs) Handles pctBoxIcon.Click

    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click

    End Sub

    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        hidePanel()
        pnlStudentInformation.Show()
        lblTotalRecords1.Text = countStudentRecord()
        loadStudentRecord()

    End Sub
    'BUTTON HOVER

    'STUDENT BUTTON HOVER
    Private Sub btnStudents_MouseEnter(sender As Object, e As EventArgs) Handles btnStudents.MouseEnter
        btnStudents.BackColor = Color.FromArgb(192, 255, 192)
        picStudentIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnStudents_MouseLeave(sender As Object, e As EventArgs) Handles btnStudents.MouseLeave
        btnStudents.BackColor = Color.FromArgb(188, 221, 203)
        picStudentIcon.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnStudents_MouseDown(sender As Object, e As MouseEventArgs) Handles btnStudents.MouseDown
        btnStudents.BackColor = Color.Honeydew
        picStudentIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnStudents_MouseUp(sender As Object, e As MouseEventArgs) Handles btnStudents.MouseUp
        btnStudents.BackColor = Color.FromArgb(192, 255, 192)
        picStudentIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnInternships_Click(sender As Object, e As EventArgs) Handles btnInternships.Click
        hidePanel()
        pnlInternshipInformation.Show()
    End Sub

    'BUTTON HOVER

    'INTERNSHIPS BUTTON HOVER 
    Private Sub btnInternships_MouseEnter(sender As Object, e As EventArgs) Handles btnInternships.MouseEnter
        btnInternships.BackColor = Color.FromArgb(192, 255, 192)
        picInternshipIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnInternships_MouseLeave(sender As Object, e As EventArgs) Handles btnInternships.MouseLeave
        btnInternships.BackColor = Color.FromArgb(188, 221, 203)
        picInternshipIcon.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnInternships_MouseDown(sender As Object, e As MouseEventArgs) Handles btnInternships.MouseDown
        btnInternships.BackColor = Color.Honeydew
        picInternshipIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnInternships_MouseUp(sender As Object, e As MouseEventArgs) Handles btnInternships.MouseUp
        btnInternships.BackColor = Color.FromArgb(192, 255, 192)
        picInternshipIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnEvaluation_Click(sender As Object, e As EventArgs) Handles btnEvaluation.Click
        hidePanel()
        pnlEvaluationInformation.Show()
    End Sub

    'BUTTON HOVER

    'EVALUATION BUTTON HOVER 
    Private Sub btnEvaluation_MouseEnter(sender As Object, e As EventArgs) Handles btnEvaluation.MouseEnter
        btnEvaluation.BackColor = Color.FromArgb(192, 255, 192)
        picEvaluationIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnEvaluation_MouseLeave(sender As Object, e As EventArgs) Handles btnEvaluation.MouseLeave
        btnEvaluation.BackColor = Color.FromArgb(188, 221, 203)
        picEvaluationIcon.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnEvaluation_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEvaluation.MouseDown
        btnEvaluation.BackColor = Color.Honeydew
        picEvaluationIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnEvaluation_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEvaluation.MouseUp
        btnEvaluation.BackColor = Color.FromArgb(192, 255, 192)
        picEvaluationIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        hidePanel()
        pnlCompanyInformation.Show()

    End Sub

    'BUTTON HOVER

    'COMPANY BUTTON HOVER 
    Private Sub btnCompany_MouseEnter(sender As Object, e As EventArgs) Handles btnCompany.MouseEnter
        btnCompany.BackColor = Color.FromArgb(192, 255, 192)
        picCompanyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnCompany_MouseLeave(sender As Object, e As EventArgs) Handles btnCompany.MouseLeave
        btnCompany.BackColor = Color.FromArgb(188, 221, 203)
        picCompanyIcon.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnCompany_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCompany.MouseDown
        btnCompany.BackColor = Color.Honeydew
        picCompanyIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnCompany_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCompany.MouseUp
        btnCompany.BackColor = Color.FromArgb(192, 255, 192)
        picCompanyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnFaculty_Click(sender As Object, e As EventArgs) Handles btnFaculty.Click
        hidePanel()
        pnlFacultyInformation.Show()

    End Sub

    'BUTTON HOVER

    'FACULTY BUTTON HOVER 
    Private Sub btnFaculty_MouseEnter(sender As Object, e As EventArgs) Handles btnFaculty.MouseEnter
        btnFaculty.BackColor = Color.FromArgb(192, 255, 192)
        picFacultyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnFaculty_MouseLeave(sender As Object, e As EventArgs) Handles btnFaculty.MouseLeave
        btnFaculty.BackColor = Color.FromArgb(188, 221, 203)
        picFacultyIcon.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnFaculty_MouseDown(sender As Object, e As MouseEventArgs) Handles btnFaculty.MouseDown
        btnFaculty.BackColor = Color.Honeydew
        picFacultyIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnFaculty_MouseUp(sender As Object, e As MouseEventArgs) Handles btnFaculty.MouseUp
        btnFaculty.BackColor = Color.FromArgb(192, 255, 192)
        picFacultyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub


    Private Sub btnVisitLog_Click(sender As Object, e As EventArgs) Handles btnVisitLog.Click
        hidePanel()
        pnlVisitInformation.Show()

    End Sub

    'BUTTON HOVER

    'VISIT BUTTON HOVER 
    Private Sub btnVisitLog_MouseEnter(sender As Object, e As EventArgs) Handles btnVisitLog.MouseEnter
        btnVisitLog.BackColor = Color.FromArgb(192, 255, 192)
        picVisitIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnVisitLog_MouseLeave(sender As Object, e As EventArgs) Handles btnVisitLog.MouseLeave
        btnVisitLog.BackColor = Color.FromArgb(188, 221, 203)
        picVisitIcon.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnVisitLog_MouseDown(sender As Object, e As MouseEventArgs) Handles btnVisitLog.MouseDown
        btnVisitLog.BackColor = Color.Honeydew
        picVisitIcon.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnVisitLog_MouseUp(sender As Object, e As MouseEventArgs) Handles btnVisitLog.MouseUp
        btnVisitLog.BackColor = Color.FromArgb(192, 255, 192)
        picVisitIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnSummaryReport_Click(sender As Object, e As EventArgs) Handles btnSummaryReport.Click
        hidePanel()
        pnlSummaryReport.Show()

    End Sub


    'BUTTON HOVER

    'SUMMARY BUTTON HOVER 
    Private Sub btnSummaryReport_MouseEnter(sender As Object, e As EventArgs) Handles btnSummaryReport.MouseEnter
        btnSummaryReport.BackColor = Color.FromArgb(192, 255, 192)
        picSummary.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnSummaryReport_MouseLeave(sender As Object, e As EventArgs) Handles btnSummaryReport.MouseLeave
        btnSummaryReport.BackColor = Color.FromArgb(188, 221, 203)
        picSummary.BackColor = Color.FromArgb(188, 221, 203)

    End Sub

    Private Sub btnSummaryReport_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSummaryReport.MouseDown
        btnSummaryReport.BackColor = Color.Honeydew
        picSummary.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnSummaryReport_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSummaryReport.MouseUp
        btnSummaryReport.BackColor = Color.FromArgb(192, 255, 192)
        picSummary.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub pnlTtotalInterns_Paint(sender As Object, e As PaintEventArgs) Handles pnlTtotalInterns.Paint

    End Sub

    Private Sub pnlCompleted_Paint(sender As Object, e As PaintEventArgs) Handles pnlCompleted.Paint

    End Sub

    Private Sub pnlOnGoing_Paint(sender As Object, e As PaintEventArgs) Handles pnlOnGoing.Paint

    End Sub

    Private Sub pnlDataModel1_Paint(sender As Object, e As PaintEventArgs) Handles pnlDataModel1.Paint

    End Sub

    Private Sub pnlDataModel2_Paint(sender As Object, e As PaintEventArgs) Handles pnlDataModel2.Paint

    End Sub


    'Student Logs
    '   Private Sub pnlStudentLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlStudentLogs.Paint
    ' End Sub


    Private Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        If txtSearchStudentID1.Text = "" Then
            MessageBox.Show("Please Enter Student ID",
                                     "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        dgvStudentSearch.DataSource = searchStudentTable(txtSearchStudentID1.Text.Trim())

    End Sub

    Private Sub dgvStudentLogs1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudentSearch.CellContentClick

    End Sub

    Private Sub dgvStudentFiles1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudentFiles.CellContentClick

    End Sub

    Private Sub lblTotalRecords1_Click(sender As Object, e As EventArgs) Handles lblTotalRecords1.Click

    End Sub

    Private Sub btnImport1_Click(sender As Object, e As EventArgs) Handles btnImport1.Click
        Dim ofd As New OpenFileDialog
        Dim csvAdded As Boolean
        ofd.Filter = "CSV Files (*.csv)|*.csv"

        If ofd.ShowDialog() = DialogResult.OK Then
            Dim dt As DataTable = LoadCSV(ofd.FileName)
            csvAdded = addStudentsFromCsv(dt)
        End If

        If Not csvAdded Then
            MessageBox.Show("Importing Failed, Check The Format Properly", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        MessageBox.Show("Import Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()
        lblTotalRecords1.Text = countStudentRecord()

    End Sub

    Private Sub btnAdd1_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click
        pnlAddNewStudentRecord.Show()
        txtStudentID2.Text = GenerateStudentID()
        loadCourseComboBx(cmbCourse2)
        loadDepartmentComboBx(cmbDepartment2)
        loadSectionComboBx(cmbSection2)
        loadGenderComboBx(cmbGender2)

    End Sub

    Private Sub btnEdit1_Click(sender As Object, e As EventArgs) Handles btnEdit1.Click
        pnlEditStudentRecord.Show()
        loadCourseComboBx(cmbCourse3)
        loadDepartmentComboBx(cmbDepartment3)
        loadSectionComboBx(cmbSection3)
        loadGenderComboBx(cmbGender3)
    End Sub

    Private Sub btnHide1_Click(sender As Object, e As EventArgs) Handles btnHide1.Click
        If dgvStudentFiles.SelectedRows.Count = 0 Then
            MessageBox.Show(
            "Please Select Row to Hide", "DGV", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Sub
        End If

        Dim dv As DataView = CType(dgvStudentFiles.DataSource, DataView)
        Dim dt As DataTable = dv.Table

        For Each row As DataGridViewRow In dgvStudentFiles.SelectedRows
            Dim dr As DataRow = CType(row.DataBoundItem, DataRowView).Row
            dr("Hidden") = True
        Next

        dv.RowFilter = "Hidden = False"

    End Sub
    Private Sub btnShow1_Click_1(sender As Object, e As EventArgs) Handles btnShow1.Click
        Dim dv As DataView = CType(dgvStudentFiles.DataSource, DataView)
        dv.RowFilter = ""
    End Sub


    'Add New Student Record

    ' Private Sub pnlAddNewStudentRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewStudentRecord.Paint

    ' End Sub
    Private Sub txtStudentID2_TextChanged(sender As Object, e As EventArgs) Handles txtStudentID2.TextChanged

    End Sub

    Private Sub txtFName2_TextChanged(sender As Object, e As EventArgs) Handles txtFName2.TextChanged

    End Sub

    Private Sub txtLName2_TextChanged(sender As Object, e As EventArgs) Handles txtLName2.TextChanged

    End Sub

    Private Sub cmbGender2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGender2.SelectedIndexChanged

    End Sub
    Private Sub cmbSection2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtContactNumber2_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber2.TextChanged

    End Sub

    Private Sub txtEmail2_TextChanged(sender As Object, e As EventArgs) Handles txtEmail2.TextChanged

    End Sub

    Private Sub cmbDepartment2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment2.SelectedIndexChanged

    End Sub

    Private Sub cmbProgram2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse2.SelectedIndexChanged

    End Sub

    Private Sub btnCancel2_Click(sender As Object, e As EventArgs) Handles btnCancel2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlStudentInformation.Show()
            pnlAddNewStudentRecord.Hide()
        End If
    End Sub

    Private Sub btnAdd2_Click(sender As Object, e As EventArgs) Handles btnAdd2.Click
        If Not validStudentInputBx(txtEmail2, txtFName2, txtLName2, txtContactNumber2) Then Exit Sub

        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.No Then
            pnlStudentInformation.Show()
            pnlAddNewStudentRecord.Hide()
            Exit Sub
        End If

        Dim isAdded As Boolean = addStudentRecord(txtStudentID2.Text, txtFName2.Text, txtLName2.Text, cmbGender2.SelectedValue,
                                                  cmbSection2.SelectedValue, txtContactNumber2.Text, txtEmail2.Text,
                                                  cmbDepartment2.SelectedValue, cmbCourse2.SelectedValue)

        If Not isAdded Then
            MessageBox.Show("Adding Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        clearBox(txtStudentID2, txtFName2, txtLName2, txtContactNumber2, txtEmail2)
        MessageBox.Show("Successfully Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()

    End Sub

    'Edit STudent Record

    ' Private Sub pnlEditStudentRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditStudentRecord.Paint

    ' End Sub

    Private Sub txtSearchID3_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID3.TextChanged

    End Sub

    Private Sub btnSearch3_Click(sender As Object, e As EventArgs) Handles btnSearch3.Click
        Dim dT As DataTable = GetStudentInfo(txtSearchID3.Text)

        If dT.Rows.Count = 0 Then
            MessageBox.Show("Student not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim row As DataRow = dT.Rows(0)

        txtStudentID3.Text = row("student_id").ToString
        txtFName3.Text = row("first_name").ToString
        txtLName3.Text = row("last_name").ToString
        cmbGender3.SelectedValue = row("gender").ToString
        cmbSection3.SelectedValue = row("section_name").ToString
        txtContactNumber3.Text = row("contact_no").ToString
        txtEmail3.Text = row("email").ToString
        cmbDepartment3.SelectedValue = row("department_id").ToString
        cmbCourse3.SelectedValue = row("course_id").ToString

    End Sub

    Private Sub txtStudentID3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtFName3_TextChanged(sender As Object, e As EventArgs) Handles txtFName3.TextChanged

    End Sub

    Private Sub txtLName3_TextChanged(sender As Object, e As EventArgs) Handles txtLName3.TextChanged

    End Sub

    Private Sub cmbGender3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGender3.SelectedIndexChanged

    End Sub

    Private Sub txtContactNumber3_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber3.TextChanged

    End Sub

    Private Sub txtEmail3_TextChanged(sender As Object, e As EventArgs) Handles txtEmail3.TextChanged

    End Sub

    Private Sub cmbDepartment3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment3.SelectedIndexChanged

    End Sub

    Private Sub cmbProgram3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse3.SelectedIndexChanged

    End Sub

    Private Sub btnCancel3_Click(sender As Object, e As EventArgs) Handles btnCancel3.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlStudentInformation.Show()
            pnlEditStudentRecord.Hide()
        End If
    End Sub

    Private Sub btnEdit3_Click(sender As Object, e As EventArgs) Handles btnEdit3.Click
        If Not validStudentInputBx(txtEmail3, txtFName3, txtLName3, txtContactNumber3) Then Exit Sub

        Dim result As DialogResult = MessageBox.Show("Do you want to edit this record?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If result = DialogResult.No Then
            pnlStudentInformation.Show()
            pnlEditStudentRecord.Hide()
            Exit Sub
        End If

        Dim isEdit As Boolean = updateStudentRecord(txtStudentID3.Text, txtFName3.Text, txtLName3.Text, cmbGender3.SelectedValue,
                                                   cmbSection3.SelectedValue, txtContactNumber3.Text, txtEmail3.Text,
                                                   cmbDepartment3.SelectedValue, cmbCourse3.SelectedValue)

        If Not isEdit Then
            MessageBox.Show("Editing Failed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        clearBox(txtStudentID3, txtFName3, txtLName3, txtContactNumber3, txtEmail3)
        MessageBox.Show("Successfully Edited", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        loadStudentRecord()

    End Sub

    'Internship Logs

    ' Private Sub pnlInternshipLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlInternshipLogs.Paint

    ' End Sub

    Private Sub txtSearchID4_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID4.TextChanged

    End Sub

    Private Sub btnSearch4_Click(sender As Object, e As EventArgs) Handles btnSearch4.Click

    End Sub

    Private Sub dgvInternshipLogs4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInternshipLogs4.CellContentClick

    End Sub

    Private Sub dgvInternshipFiles4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInternshipFiles4.CellContentClick

    End Sub

    Private Sub lblTotalRecords4_Click(sender As Object, e As EventArgs) Handles lblTotalRecords4.Click

    End Sub


    'Evaluation Logs

    ' Private Sub pnlEvaluationLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlEvaluationLogs.Paint

    ' End Sub
    Private Sub txtSearchID5_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID5.TextChanged

    End Sub

    Private Sub btnSearch5_Click(sender As Object, e As EventArgs) Handles btnSearch5.Click

    End Sub

    Private Sub dgvEvaluationLogs5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEvaluationLogs5.CellContentClick

    End Sub

    Private Sub dgvEvaluationFiles5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEvaluationFiles5.CellContentClick

    End Sub

    Private Sub lblTotalRecords5_Click(sender As Object, e As EventArgs) Handles lblTotalRecords5.Click

    End Sub

    Private Sub btnAdd5_Click(sender As Object, e As EventArgs) Handles btnAdd5.Click
        pnlAddNewInternshipEvaluationRecord.Show()
    End Sub

    Private Sub btnEdit5_Click(sender As Object, e As EventArgs) Handles btnEdit5.Click
        pnlEditInternshipEvaluationRecord.Show()
    End Sub

    Private Sub btnDelete5_Click(sender As Object, e As EventArgs) Handles btnDelete5.Click
        Dim result As DialogResult = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            pnlEvaluationInformation.Show()
        End If
    End Sub

    'Add new intern record

    '  Private Sub pnlAddNewInternshipRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewInternshipEvaluationRecord.Paint

    '  End Sub
    Private Sub txtEvaluationID6_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationID6.TextChanged

    End Sub

    Private Sub txtInternshipID6_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID6.TextChanged

    End Sub

    Private Sub txtFacultyID6_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID6.TextChanged

    End Sub

    Private Sub txtEvaluationReport6_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationReport6.TextChanged

    End Sub

    Private Sub cmbStatus6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus6.SelectedIndexChanged

    End Sub

    Private Sub btnCancel6_Click(sender As Object, e As EventArgs) Handles btnCancel6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlInternshipInformation.Show()
            pnlAddNewInternshipEvaluationRecord.Hide()
        End If
    End Sub

    Private Sub btnAdd6_Click(sender As Object, e As EventArgs) Handles btnAdd6.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlInternshipInformation.Show()
            pnlAddNewInternshipEvaluationRecord.Hide()
        End If
    End Sub

    'Edit intern eval record
    '   Private Sub pnlEditInternshipEvaluationRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditInternshipEvaluationRecord.Paint

    '   End Sub
    Private Sub txtSearchID7_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID7.TextChanged

    End Sub

    Private Sub btnSearch7_Click(sender As Object, e As EventArgs) Handles btnSearch7.Click

    End Sub
    Private Sub txtEvaluationID7_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationID7.TextChanged

    End Sub

    Private Sub txtInternshipID7_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID7.TextChanged

    End Sub

    Private Sub txtFacultyID7_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID7.TextChanged

    End Sub

    Private Sub txtEvaluationReport7_TextChanged(sender As Object, e As EventArgs) Handles txtEvaluationReport7.TextChanged

    End Sub

    Private Sub cmbStatus7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus7.SelectedIndexChanged

    End Sub

    Private Sub btnCancel7_Click(sender As Object, e As EventArgs) Handles btnCancel7.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlEvaluationInformation.Show()
            pnlEditInternshipEvaluationRecord.Hide()
        End If

    End Sub

    Private Sub btnEdit7_Click(sender As Object, e As EventArgs) Handles btnEdit7.Click
        Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If result = DialogResult.OK Then
            pnlEvaluationInformation.Show()
            pnlEditInternshipEvaluationRecord.Hide()
        End If

    End Sub

    'CompanyLogs

    '  Private Sub pnlCompanyLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlCompanyLogs.Paint

    '  End Sub
    Private Sub txtSearchID8_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID8.TextChanged

    End Sub
    Private Sub btnSearc8_TextChanged(sender As Object, e As EventArgs) Handles btnSearch8.TextChanged

    End Sub

    Private Sub dgvCompanyLogs8_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyLogs8.CellContentClick

    End Sub

    Private Sub dgvCompanyFiles8_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyFiles8.CellContentClick

    End Sub

    Private Sub lblTotalRecords8_Click(sender As Object, e As EventArgs) Handles lblTotalRecords8.Click

    End Sub
    Private Sub btnViewContacts8_Click(sender As Object, e As EventArgs) Handles btnViewContacts8.Click
        hidePanel()
        pnlCompanyContactInformation.Show()
    End Sub
    Private Sub btnAdd8_Click(sender As Object, e As EventArgs) Handles btnAdd8.Click
        pnlAddNewCompanyandCompanyContact.Show()
    End Sub

    Private Sub btnEdit8_Click(sender As Object, e As EventArgs) Handles btnEdit8.Click
        pnlEditCompanyCompanyContact.Show()
    End Sub

    Private Sub btnDelete8_Click(sender As Object, e As EventArgs) Handles btnDelete8.Click
        Dim result As DialogResult = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new record company or contact

    '  Private Sub pnlAddNewCompanyandCompanyContact_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewCompanyandCompanyContact.Paint

    '  End Sub

    Private Sub btnAddCompany8_Click(sender As Object, e As EventArgs) Handles btnAddCompany8.Click
        pnlAddNewCompanyRecord.Show()
        pnlAddNewCompanyandCompanyContact.Hide()
    End Sub

    Private Sub btnAddCompanyContact8_Click(sender As Object, e As EventArgs) Handles btnAddCompanyContact8.Click

        ' 1. Show parent panel
        pnlCompanyContactInformation.Show()
        pnlCompanyContactInformation.BringToFront()

        ' 2. Show the inside panel
        pnlAddNewCompanyContactRecord.Show()
        pnlAddNewCompanyContactRecord.BringToFront()

        ' 3. Hide the old panel
        pnlCompanyInformation.Hide()

    End Sub

    'Edit company contact or sompany
    Private Sub pnlEditCompanyCompanyContact_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditCompanyCompanyContact.Paint

    End Sub

    Private Sub btnEditCompany8_Click(sender As Object, e As EventArgs) Handles btnEditCompany8.Click
        pnlEditCompanyRecord.Show()
        pnlEditCompanyCompanyContact.Hide()
    End Sub

    Private Sub btnEditCompanyContact8_Click(sender As Object, e As EventArgs) Handles btnEditCompanyContact8.Click
        pnlEditCompanyContactRecord.Hide()
        pnlEditCompanyRecord.Show()

        ' 1. Show parent panel
        pnlCompanyContactInformation.Show()
        pnlCompanyContactInformation.BringToFront()

        ' 2. Show the inside panel
        pnlEditCompanyContactRecord.Show()
        pnlEditCompanyContactRecord.BringToFront()

        ' 3. Hide the old panel
        pnlCompanyInformation.Hide()


    End Sub

    'Add company record
    ' Private Sub pnlAddNewCompanyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewCompanyRecord.Paint

    ' End Sub
    Private Sub txtCompanyID9_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyID9.TextChanged

    End Sub

    Private Sub txtCompanyName9_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyName9.TextChanged

    End Sub

    Private Sub txtAddress9_TextChanged(sender As Object, e As EventArgs) Handles txtAddress9.TextChanged

    End Sub

    Private Sub txtIndustryType9_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryType9.TextChanged

    End Sub

    Private Sub txtContactNumber9_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber9.TextChanged

    End Sub

    Private Sub txtEmail9_TextChanged(sender As Object, e As EventArgs) Handles txtEmail9.TextChanged

    End Sub

    Private Sub btnCancel9_Click(sender As Object, e As EventArgs) Handles btnCancel9.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlAddNewCompanyRecord.Hide()
        End If

    End Sub

    Private Sub btnAdd9_Click(sender As Object, e As EventArgs) Handles btnAdd9.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlAddNewCompanyRecord.Show()
        End If
    End Sub


    'Edit COmpany Record

    ' Private Sub pnlEditCompanyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditCompanyRecord.Paint

    ' End Sub


    Private Sub txtSearchID10_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID10.TextChanged

    End Sub

    Private Sub btnSearch10_Click(sender As Object, e As EventArgs) Handles btnSearch10.Click

    End Sub

    Private Sub txtCompanyID10_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyID10.TextChanged

    End Sub

    Private Sub txtCompanyName10_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyName10.TextChanged

    End Sub

    Private Sub txtAddress10_TextChanged(sender As Object, e As EventArgs) Handles txtAddress10.TextChanged

    End Sub

    Private Sub txtIndustryType10_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryType10.TextChanged

    End Sub

    Private Sub txtContactNumber10_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber10.TextChanged

    End Sub

    Private Sub txtEmail10_TextChanged(sender As Object, e As EventArgs) Handles txtEmail10.TextChanged

    End Sub

    Private Sub btnCancel10_Click(sender As Object, e As EventArgs) Handles btnCancel10.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlEditCompanyRecord.Hide()

        End If
    End Sub

    Private Sub btnEdit10_Click(sender As Object, e As EventArgs) Handles btnEdit10.Click
        Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If result = DialogResult.OK Then
            pnlAddNewCompanyandCompanyContact.Hide()
            pnlCompanyInformation.Show()
            pnlEditCompanyRecord.Hide()
        End If
    End Sub

    'company contact logs
    'Private Sub pnlCompanyContactLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlCompanyContactLogs.Paint

    'End Sub

    Private Sub txtSearchID11_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID11.TextChanged

    End Sub

    Private Sub btnSearch11_Click(sender As Object, e As EventArgs) Handles btnSearch11.Click

    End Sub

    Private Sub dgvCompanyContactLogs11_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyContactLogs11.CellContentClick

    End Sub

    Private Sub dgvCompanyContactFiles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompanyContactFiles.CellContentClick

    End Sub

    Private Sub lblTotalRecords11_Click(sender As Object, e As EventArgs) Handles lblTotalRecords11.Click

    End Sub

    Private Sub btnAdd11_Click(sender As Object, e As EventArgs) Handles btnAdd11.Click
        pnlCompanyInformation.Show()                  ' show parent
        pnlAddNewCompanyandCompanyContact.Show()      ' show child
        pnlAddNewCompanyandCompanyContact.BringToFront()

        pnlCompanyContactInformation.Show()

        'Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)


        'If result = DialogResult.Yes Then
        'pnlCompanyInformation.Show()
        'pnlCompanyContactInformation.Hide()
        'pnlAddNewCompanyandCompanyContact.Hide()
        'End If
    End Sub

    Private Sub btnEdit11_Click(sender As Object, e As EventArgs) Handles btnEdit11.Click
        pnlCompanyContactInformation.Show()          ' parent
        pnlEditCompanyCompanyContact.Show()          ' child
        pnlEditCompanyCompanyContact.BringToFront()

        pnlAddNewCompanyContactRecord.Hide()

        'Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'If result = DialogResult.OK Then
        'pnlCompanyInformation.Show()
        'pnlCompanyContactInformation.Hide()
        'pnlAddNewCompanyandCompanyContact.Hide()
        'End If

    End Sub

    Private Sub btnDelete11_Click(sender As Object, e As EventArgs) Handles btnDelete11.Click
        Dim result As DialogResult = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new company contacyt
    'Private Sub pnlAddNewCompanyContactRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewCompanyContactRecord.Paint

    'End Sub

    Private Sub txtCompanyContactID12_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyContactID12.TextChanged

    End Sub

    Private Sub txtCompany12_TextChanged(sender As Object, e As EventArgs) Handles txtCompany12.TextChanged

    End Sub

    Private Sub txtPosition12_TextChanged(sender As Object, e As EventArgs) Handles txtPosition12.TextChanged

    End Sub

    Private Sub txtFName12_TextChanged(sender As Object, e As EventArgs) Handles txtFName12.TextChanged

    End Sub

    Private Sub txtLName12_TextChanged(sender As Object, e As EventArgs) Handles txtLName12.TextChanged

    End Sub

    Private Sub txtContactNumber12_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber12.TextChanged

    End Sub

    Private Sub txtEmail12_TextChanged(sender As Object, e As EventArgs) Handles txtEmail12.TextChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlCompanyInformation.Show()
            pnlAddNewCompanyContactRecord.Hide()
            pnlAddNewCompanyandCompanyContact.Hide()
        End If

    End Sub

    Private Sub btnAdd12_Click(sender As Object, e As EventArgs) Handles btnAdd12.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlCompanyInformation.Show()
            pnlAddNewCompanyContactRecord.Hide()
            pnlAddNewCompanyandCompanyContact.Hide()
        End If
    End Sub

    'Edit company contact rec
    'Private Sub pnlEditCompanyContactRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditCompanyContactRecord.Paint

    'End Sub

    Private Sub txtSearchID13_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID13.TextChanged

    End Sub

    Private Sub btnSearch13_Click(sender As Object, e As EventArgs) Handles btnSearch13.Click

    End Sub

    Private Sub txtCompanyContactID13_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyContactID13.TextChanged

    End Sub

    Private Sub txtCompany13_TextChanged(sender As Object, e As EventArgs) Handles txtCompany13.TextChanged

    End Sub

    Private Sub txtPosition13_TextChanged(sender As Object, e As EventArgs) Handles txtPosition13.TextChanged

    End Sub
    Private Sub txtFName_TextChanged(sender As Object, e As EventArgs) Handles txtFName.TextChanged

    End Sub
    Private Sub txtLName_TextChanged(sender As Object, e As EventArgs) Handles txtLName.TextChanged

    End Sub

    Private Sub txtContactNumber_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber.TextChanged

    End Sub

    Private Sub txtEmail13_TextChanged(sender As Object, e As EventArgs) Handles txtEmail13.TextChanged

    End Sub

    Private Sub btnCancel13_Click(sender As Object, e As EventArgs) Handles btnCancel13.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlEditCompanyContactRecord.Hide()
            pnlCompanyContactInformation.Show()
        End If
    End Sub

    Private Sub btnEdit13_Click(sender As Object, e As EventArgs) Handles btnEdit13.Click
        Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If result = DialogResult.OK Then
            pnlEditCompanyContactRecord.Show()
            pnlCompanyContactInformation.Show()
        End If

    End Sub

    'Faculty Logs

    '  Private Sub pnlFacultyLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlFacultyLogs.Paint

    '  End Sub

    Private Sub txtSearchID14_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID14.TextChanged

    End Sub

    Private Sub btnSearch14_Click(sender As Object, e As EventArgs) Handles btnSearch14.Click

    End Sub

    Private Sub dgvFacultyLogs14_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFacultyLogs14.CellContentClick

    End Sub

    Private Sub dgvFacultyFiles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFacultyFiles.CellContentClick

    End Sub

    Private Sub lblTotalRecords14_Click(sender As Object, e As EventArgs) Handles lblTotalRecords14.Click

    End Sub

    Private Sub btnAdd14_Click(sender As Object, e As EventArgs) Handles btnAdd14.Click
        pnlAddNewFacultyRecord.Show()
    End Sub

    Private Sub btnEdit14_Click(sender As Object, e As EventArgs) Handles btnEdit14.Click
        pnlEditFacultyRecord.Show()
    End Sub

    Private Sub btnDelete14_Click(sender As Object, e As EventArgs) Handles btnDelete14.Click
        Dim result As DialogResult = MessageBox.Show(
       "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new faculty record

    '  Private Sub pnlAddNewFacultyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewFacultyRecord.Paint

    '  End Sub

    Private Sub txtFacultyID15_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID15.TextChanged

    End Sub

    Private Sub txtFName15_TextChanged(sender As Object, e As EventArgs) Handles txtFName15.TextChanged

    End Sub

    Private Sub txtLName15_TextChanged(sender As Object, e As EventArgs) Handles txtLName15.TextChanged

    End Sub

    Private Sub txtPosition15_TextChanged(sender As Object, e As EventArgs) Handles txtPosition15.TextChanged

    End Sub

    Private Sub txtDepartmentID15_TextChanged(sender As Object, e As EventArgs) Handles txtDepartmentID15.TextChanged

    End Sub

    Private Sub txtContactNumber15_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber15.TextChanged

    End Sub

    Private Sub btnCancel15_Click(sender As Object, e As EventArgs) Handles btnCancel15.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlFacultyInformation.Show()
            pnlAddNewFacultyRecord.Hide()

        End If

    End Sub

    Private Sub btnAdd15_Click(sender As Object, e As EventArgs) Handles btnAdd15.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlFacultyInformation.Show()
            pnlAddNewFacultyRecord.Hide()
        End If

    End Sub

    'Edit faculty Record

    Private Sub pnlEditFacultyRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditFacultyRecord.Paint

    End Sub

    Private Sub txtSearchID16_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID16.TextChanged

    End Sub

    Private Sub btnSearch16_Click(sender As Object, e As EventArgs) Handles btnSearch16.Click

    End Sub

    Private Sub txtFacultyID16_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID16.TextChanged

    End Sub

    Private Sub txtFName16_TextChanged(sender As Object, e As EventArgs) Handles txtFName16.TextChanged

    End Sub

    Private Sub txtLName16_TextChanged(sender As Object, e As EventArgs) Handles txtLName16.TextChanged

    End Sub

    Private Sub txtPosition16_TextChanged(sender As Object, e As EventArgs) Handles txtPosition16.TextChanged

    End Sub

    Private Sub txtDepartmentID16_TextChanged(sender As Object, e As EventArgs) Handles txtDepartmentID16.TextChanged

    End Sub

    Private Sub txtContactNumber16_TextChanged(sender As Object, e As EventArgs) Handles txtContactNumber16.TextChanged

    End Sub

    Private Sub btnCancel16_Click(sender As Object, e As EventArgs) Handles btnCancel16.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlFacultyInformation.Show()
            pnlEditFacultyRecord.Hide()
        End If

    End Sub

    Private Sub btnEdit16_Click(sender As Object, e As EventArgs) Handles btnEdit16.Click
        Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If result = DialogResult.OK Then
            pnlFacultyInformation.Show()
            pnlEditFacultyRecord.Hide()
        End If

    End Sub

    'visit logs

    Private Sub pnlVisitLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlVisitInformation.Paint

    End Sub

    Private Sub txtSearchID17_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID17.TextChanged

    End Sub

    Private Sub btnSearch17_Click(sender As Object, e As EventArgs) Handles btnSearch17.Click

    End Sub

    Private Sub dgvVisitLogs17_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVisitLogs17.CellContentClick

    End Sub

    Private Sub dgvVisitLogFiles17_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVisitLogFiles17.CellContentClick

    End Sub

    Private Sub lblTotalRecords17_Click(sender As Object, e As EventArgs) Handles lblTotalRecords17.Click

    End Sub

    Private Sub btnAdd17_Click(sender As Object, e As EventArgs) Handles btnAdd17.Click
        pnlAddNewVisitRecord.Show()
    End Sub

    Private Sub btnEdit17_Click(sender As Object, e As EventArgs) Handles btnEdit17.Click
        pnlEditVisitRecord.Show()
    End Sub

    Private Sub btnDelete17_Click(sender As Object, e As EventArgs) Handles btnDelete17.Click
        Dim result As DialogResult = MessageBox.Show(
    "Are you sure you want to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)

        If result = DialogResult.Yes Then
            ' Your delete code here
            ' Example: DeleteRecord()
        End If
    End Sub

    'Add new visit logs

    'Private Sub pnlAddNewInternship_Paint(sender As Object, e As PaintEventArgs) Handles pnlAddNewVisitRecord.Paint

    ' End Sub

    Private Sub txtVisitID18_TextChanged(sender As Object, e As EventArgs) Handles txtVisitID18.TextChanged

    End Sub

    Private Sub txtInternshipID18_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID18.TextChanged

    End Sub

    Private Sub txtFacultyID18_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID18.TextChanged

    End Sub

    Private Sub dtpVisitDate18_ValueChanged(sender As Object, e As EventArgs) Handles dtpVisitDate18.ValueChanged

    End Sub

    Private Sub txtScore18_TextChanged(sender As Object, e As EventArgs) Handles txtScore18.TextChanged

    End Sub

    Private Sub txtRemarks18_TextChanged(sender As Object, e As EventArgs) Handles txtRemarks18.TextChanged

    End Sub
    Private Sub btnCancel18_Click(sender As Object, e As EventArgs) Handles btnCancel18.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlAddNewVisitRecord.Hide()
        End If

    End Sub
    Private Sub btnAdd18_Click(sender As Object, e As EventArgs) Handles btnAdd18.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlAddNewVisitRecord.Hide()
        End If
    End Sub


    'Edit visit record
    ' Private Sub pnlEditVisitRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditVisitRecord.Paint

    ' End Sub

    Private Sub txtSearchID19_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID19.TextChanged

    End Sub

    Private Sub btnSearch19_Click(sender As Object, e As EventArgs) Handles btnSearch19.Click

    End Sub

    Private Sub txtVisitID19_TextChanged(sender As Object, e As EventArgs) Handles txtVisitID19.TextChanged

    End Sub

    Private Sub txtInternshipID19_TextChanged(sender As Object, e As EventArgs) Handles txtInternshipID19.TextChanged

    End Sub

    Private Sub txtFacultyID19_TextChanged(sender As Object, e As EventArgs) Handles txtFacultyID19.TextChanged

    End Sub

    Private Sub dtpVisitDate19_ValueChanged(sender As Object, e As EventArgs) Handles dtpVisitDate19.ValueChanged

    End Sub

    Private Sub txtScore19_TextChanged(sender As Object, e As EventArgs) Handles txtScore19.TextChanged

    End Sub

    Private Sub txtRemarks19_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCancel19_Click(sender As Object, e As EventArgs) Handles btnCancel19.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlEditVisitRecord.Hide()
        End If

    End Sub
    Private Sub btnEdit19_Click(sender As Object, e As EventArgs) Handles btnEdit19.Click
        Dim result As DialogResult = MessageBox.Show("Record has been successfully edited.", "Edit Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If result = DialogResult.OK Then
            pnlVisitInformation.Show()
            pnlEditVisitRecord.Hide()
        End If

    End Sub

    'summary report

    Private Sub pnlSummaryReport_Paint(sender As Object, e As PaintEventArgs) Handles pnlSummaryReport.Paint

    End Sub

    Private Sub txtSearchID20_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID20.TextChanged

    End Sub

    Private Sub btnSearch20_Click(sender As Object, e As EventArgs) Handles btnSearch20.Click

    End Sub

    Private Sub flpSummaryReport20_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub lblStudentName_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblStudentID20_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblOverallSummary20_Click(sender As Object, e As EventArgs) Handles lblOverallSummary20.Click

    End Sub

    Private Sub pnlSummaryReport20_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub picStudentIcon_Click(sender As Object, e As EventArgs) Handles picStudentIcon.Click

    End Sub


















    '1529, 902
End Class
