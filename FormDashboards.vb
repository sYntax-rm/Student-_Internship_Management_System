Imports MySql.Data.MySqlClient
Imports Mysqlx.Notice

Public Class FormDashboards
    Private lastRowStudent As Integer = -1
    Private lastRowInternship As Integer = -1
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

        'Panel add Edit Location - Internship
        'Update
        pnlUpdateInternshipRecord.Location = New Point(104, 101)
        pnlUpdateInternshipRecord.Size = New Size(1083, 700)


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

        'Panel add Edit Location - Visit
        'Add
        pnlAddNewVisitRecord.Location = New Point(90, 78)
        pnlAddNewVisitRecord.Size = New Size(1100, 750)
        'Edit
        pnlEditVisitRecord.Location = New Point(90, 78)
        pnlEditVisitRecord.Size = New Size(1100, 750)

        'For Rounding Home Button
        Dim path As New Drawing2D.GraphicsPath()
        path.AddEllipse(0, 0, btnHome.Width, btnHome.Height)
        btnHome.Region = New Region(path)

        'Home Load
        pnlHome.Show()
        displayCount()

        'For buttons
        hidePanel()


        ' Set icon sa form at taskbar
        'Me.Icon = New System.Drawing.Icon("D:\vbnet_programs\Finals\Student Internship (OJT) Management System-20251121T034957Z-1-001\Student Internship (OJT) Management System\Resources\internship_icon.ico")
        'Me.ShowIcon = True




        'FOR COLUMNS STYLES INTERNSHIPS
        ' 1. LOAD DATA FROM DATABASE
        Dim dt As DataTable = searchInterTable("")   ' or full load query mo

        ' 2. SET DATASOURCE OF DGV
        dgvInternshipFiles4.DataSource = dt

        ' 3. APPLY STYLE (COLUMNS EXIST NA)
        dgvInternshipFiles4Styles(dgvInternshipFiles4)

        'FOR CMB HOVER

        For Each cmb As ComboBox In {cmbGender2, cmbGender3,
                                     cmbSection2, cmbSection3,
                                     cmbDepartment2, cmbDepartment3,
                                     cmbCourse2, cmbCourse3,
                                     cmbCompanyInternship, cmbCompanyContactInternship,
                                     cmbStatusUpdateInternship}
            cmb.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler cmb.DrawItem, AddressOf DrawComboItem


            cmb.DrawMode = DrawMode.OwnerDrawFixed
            cmb.DropDownStyle = ComboBoxStyle.DropDownList
            AddHandler cmb.DrawItem, AddressOf DrawComboItem
        Next
        cmbCompanyInternship.DropDownStyle = ComboBoxStyle.DropDown
        cmbCompanyContactInternship.DropDownStyle = ComboBoxStyle.DropDown

    End Sub


    Private Sub hidePanel()
        pnlStudentInformation.Hide()
        pnlAddNewStudentRecord.Hide()
        pnlEditStudentRecord.Hide()
        pnlInternshipInformation.Hide()
        pnlUpdateInternshipRecord.Hide()
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

    Private Sub displayCount()
        lblHomePending.Text = countPendingIntern()
        lblHomeOngoing.Text = countOngoingIntern()
        lblHomeCompleted.Text = countCompletedIntern()
        lblHomeTotalInterns.Text = countIntern()
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

        'Styles for dgv
        dgvStudentFilesStyles(dgvStudentFiles, dt)

    End Sub

    End Sub

    'Search Student Style DGV
    Private Sub dgvStudentSearchsStyles(dgv As DataGridView)

    Private Sub loadInternRecord()
        'Dim dt As DataTable = loadTable("SELECT * FROM vinternship_record") use this to hide the pending
        Dim dt As DataTable = loadTable("SELECT * FROM vall_internship")
        dgvInternshipFiles4.DataSource = dt
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
            FormLogin.Show()
            Me.Hide()
        End If
    End Sub

    'BUTTON HOVER
    'BUTTON LOGOUT  HOVER STUDENTS
    Private Sub btnLogout_MouseEnter(sender As Object, e As EventArgs) Handles btnLogout.MouseEnter
        btnLogout.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnLogout_MouseLeave(sender As Object, e As EventArgs) Handles btnLogout.MouseLeave
        btnLogout.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnLogout_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLogout.MouseDown
        btnLogout.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnLogout_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLogout.MouseUp
        btnLogout.BackColor = Color.FromArgb(97, 144, 118)
    End Sub


    Private Sub lblName_Click(sender As Object, e As EventArgs) Handles lblHomeFName.Click

    End Sub

    Private Sub lblSignInAs_Click(sender As Object, e As EventArgs) Handles lblSignInAs.Click

    End Sub

    Private Sub pctBoxIcon_Click(sender As Object, e As EventArgs)

    End Sub
    'BUTTON HOVER

    'HOME BUTTON HOVER
    Private Sub btnHome_MouseEnter(sender As Object, e As EventArgs) Handles btnHome.MouseEnter
        btnHome.BackColor = Color.FromArgb(192, 255, 192)
        picHome.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnHome_MouseLeave(sender As Object, e As EventArgs) Handles btnHome.MouseLeave
        btnHome.BackColor = Color.FromArgb(218, 239, 228)
        picHome.BackColor = Color.FromArgb(218, 239, 228)

    End Sub

    Private Sub btnHome_MouseDown(sender As Object, e As MouseEventArgs) Handles btnHome.MouseDown
        btnHome.BackColor = Color.Honeydew
        picHome.BackColor = Color.Honeydew '
    End Sub

    Private Sub btnHome_MouseUp(sender As Object, e As MouseEventArgs) Handles btnHome.MouseUp
        btnHome.BackColor = Color.FromArgb(192, 255, 192)
        picHome.BackColor = Color.FromArgb(192, 255, 192)
    End Sub
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        hidePanel()
        pnlHome.Show()
        displayCount()
    End Sub
    Private Sub lblHomeDashboard_Click(sender As Object, e As EventArgs) Handles lblHomeDashboard.Click
        hidePanel()
        pnlHome.Show()
        displayCount()
    End Sub

    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        hidePanel()
        pnlStudentInformation.Show()
        lblTotalRecords1.Text = countStudentRecord()
        loadStudentRecord()
        pnlHome.Hide()
    End Sub
    'BUTTON HOVER

    'STUDENT BUTTON HOVER
    Private Sub btnStudents_MouseEnter(sender As Object, e As EventArgs) Handles btnStudents.MouseEnter
        btnStudents.BackColor = Color.FromArgb(192, 255, 192)
        picStudentIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnStudents_MouseLeave(sender As Object, e As EventArgs) Handles btnStudents.MouseLeave
        btnStudents.BackColor = Color.FromArgb(218, 239, 228)
        picStudentIcon.BackColor = Color.FromArgb(218, 239, 228)

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
        pnlHome.Hide()
        pnlInternshipInformation.Show()
        loadInternRecord()
    End Sub

    'BUTTON HOVER

    'INTERNSHIPS BUTTON HOVER 
    Private Sub btnInternships_MouseEnter(sender As Object, e As EventArgs) Handles btnInternships.MouseEnter
        btnInternships.BackColor = Color.FromArgb(192, 255, 192)
        picInternshipIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnInternships_MouseLeave(sender As Object, e As EventArgs) Handles btnInternships.MouseLeave
        btnInternships.BackColor = Color.FromArgb(218, 239, 228)
        picInternshipIcon.BackColor = Color.FromArgb(218, 239, 228)

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
        pnlHome.Hide()
        LoadEvaluationDGV()
    End Sub

    'BUTTON HOVER

    'EVALUATION BUTTON HOVER 
    Private Sub btnEvaluation_MouseEnter(sender As Object, e As EventArgs) Handles btnEvaluation.MouseEnter
        btnEvaluation.BackColor = Color.FromArgb(192, 255, 192)
        picEvaluationIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnEvaluation_MouseLeave(sender As Object, e As EventArgs) Handles btnEvaluation.MouseLeave
        btnEvaluation.BackColor = Color.FromArgb(218, 239, 228)
        picEvaluationIcon.BackColor = Color.FromArgb(218, 239, 228)

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
        pnlHome.Hide()
    End Sub

    'BUTTON HOVER

    'COMPANY BUTTON HOVER 
    Private Sub btnCompany_MouseEnter(sender As Object, e As EventArgs) Handles btnCompany.MouseEnter
        btnCompany.BackColor = Color.FromArgb(192, 255, 192)
        picCompanyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnCompany_MouseLeave(sender As Object, e As EventArgs) Handles btnCompany.MouseLeave
        btnCompany.BackColor = Color.FromArgb(218, 239, 228)
        picCompanyIcon.BackColor = Color.FromArgb(218, 239, 228)

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
        pnlHome.Hide()
    End Sub

    'BUTTON HOVER

    'FACULTY BUTTON HOVER 
    Private Sub btnFaculty_MouseEnter(sender As Object, e As EventArgs) Handles btnFaculty.MouseEnter
        btnFaculty.BackColor = Color.FromArgb(192, 255, 192)
        picFacultyIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnFaculty_MouseLeave(sender As Object, e As EventArgs) Handles btnFaculty.MouseLeave
        btnFaculty.BackColor = Color.FromArgb(218, 239, 228)
        picFacultyIcon.BackColor = Color.FromArgb(218, 239, 228)

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
        pnlHome.Hide()
    End Sub

    'BUTTON HOVER

    'VISIT BUTTON HOVER 
    Private Sub btnVisitLog_MouseEnter(sender As Object, e As EventArgs) Handles btnVisitLog.MouseEnter
        btnVisitLog.BackColor = Color.FromArgb(192, 255, 192)
        picVisitIcon.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnVisitLog_MouseLeave(sender As Object, e As EventArgs) Handles btnVisitLog.MouseLeave
        btnVisitLog.BackColor = Color.FromArgb(218, 239, 228)
        picVisitIcon.BackColor = Color.FromArgb(218, 239, 228)

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
        pnlHome.Hide()
    End Sub


    'BUTTON HOVER

    'SUMMARY BUTTON HOVER 
    Private Sub btnSummaryReport_MouseEnter(sender As Object, e As EventArgs) Handles btnSummaryReport.MouseEnter
        btnSummaryReport.BackColor = Color.FromArgb(192, 255, 192)
        picSummary.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub btnSummaryReport_MouseLeave(sender As Object, e As EventArgs) Handles btnSummaryReport.MouseLeave
        btnSummaryReport.BackColor = Color.FromArgb(218, 239, 228)
        picSummary.BackColor = Color.FromArgb(218, 239, 228)

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

    Private Sub pnlDataModel2_Paint(sender As Object, e As PaintEventArgs)

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

        'Styles for dgv
        dgvStudentSearchsStyles(dgvStudentSearch)

    End Sub
    'BUTTON HOVER
    'BUTTON SEARCH 1 HOVER STUDENTS
    Private Sub btnSearch1_MouseEnter(sender As Object, e As EventArgs) Handles btnSearch1.MouseEnter
        btnSearch1.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearch1_MouseLeave(sender As Object, e As EventArgs) Handles btnSearch1.MouseLeave
        btnSearch1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearch1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearch1.MouseDown
        btnSearch1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearch1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch1.MouseUp
        btnSearch1.BackColor = Color.FromArgb(8, 48, 25)
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

    'BUTTON HOVER
    'BUTTON ADD 1 HOVER STUDENTS
    Private Sub btnAdd1_MouseEnter(sender As Object, e As EventArgs) Handles btnAdd1.MouseEnter
        btnAdd1.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnAdd1_MouseLeave(sender As Object, e As EventArgs) Handles btnAdd1.MouseLeave
        btnAdd1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnAdd1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnAdd1.MouseDown
        btnAdd1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnAdd1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnAdd1.MouseUp
        btnAdd1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnEdit1_Click(sender As Object, e As EventArgs) Handles btnEdit1.Click
        pnlEditStudentRecord.Show()
        loadCourseComboBx(cmbCourse3)
        loadDepartmentComboBx(cmbDepartment3)
        loadSectionComboBx(cmbSection3)
        loadGenderComboBx(cmbGender3)
    End Sub

    'BUTTON HOVER
    'BUTTON EDIT 1 HOVER STUDENTS
    Private Sub btnEdit1_MouseEnter(sender As Object, e As EventArgs) Handles btnEdit1.MouseEnter
        btnEdit1.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnEdit1_MouseLeave(sender As Object, e As EventArgs) Handles btnEdit1.MouseLeave
        btnEdit1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnEdit1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEdit1.MouseDown
        btnEdit1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnEdit1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEdit1.MouseUp
        btnEdit1.BackColor = Color.FromArgb(97, 144, 118)
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

    Private Sub btnCancel2_Click(sender As Object, e As EventArgs) Handles btnCancel2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlStudentInformation.Show()
            pnlAddNewStudentRecord.Hide()
        End If
    End Sub

    'BUTTON HOVER
    'BUTTON CANCEL 2 HOVER STUDENTS
    Private Sub btnCancel2_MouseEnter(sender As Object, e As EventArgs) Handles btnCancel2.MouseEnter
        btnCancel2.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnCancel2_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel2.MouseLeave
        btnCancel2.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancel2_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCancel2.MouseDown
        btnCancel2.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnCancel2_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCancel2.MouseUp
        btnCancel2.BackColor = Color.FromArgb(8, 48, 25)
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

    'BUTTON HOVER
    'BUTTON ADD 2 HOVER STUDENTS
    Private Sub btnAdd2_MouseEnter(sender As Object, e As EventArgs) Handles btnAdd2.MouseEnter
        btnAdd2.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnAdd2_MouseLeave(sender As Object, e As EventArgs) Handles btnAdd2.MouseLeave
        btnAdd2.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnAdd2_MouseDown(sender As Object, e As MouseEventArgs) Handles btnAdd2.MouseDown
        btnAdd2.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnAdd2_MouseUp(sender As Object, e As MouseEventArgs) Handles btnAdd2.MouseUp
        btnAdd2.BackColor = Color.FromArgb(97, 144, 118)
    End Sub


    'Edit STudent Record

    ' Private Sub pnlEditStudentRecord_Paint(sender As Object, e As PaintEventArgs) Handles pnlEditStudentRecord.Paint

    ' End Sub



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


    'BUTTON HOVER
    'BUTTON SEARCH 3 HOVER STUDENTS
    Private Sub btnSearch3_MouseEnter(sender As Object, e As EventArgs) Handles btnSearch3.MouseEnter
        btnSearch3.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearch3_MouseLeave(sender As Object, e As EventArgs) Handles btnSearch3.MouseLeave
        btnSearch3.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearch3_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearch3.MouseDown
        btnSearch3.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearch3_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch3.MouseUp
        btnSearch3.BackColor = Color.FromArgb(8, 48, 25)
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

    'BUTTON HOVER
    'BUTTON CANCEL 3 HOVER STUDENTS
    Private Sub btnCancel3_MouseEnter(sender As Object, e As EventArgs) Handles btnCancel3.MouseEnter
        btnCancel3.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnCancel3_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel3.MouseLeave
        btnCancel3.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancel3_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCancel3.MouseDown
        btnCancel3.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnCancel3_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCancel3.MouseUp
        btnCancel3.BackColor = Color.FromArgb(8, 48, 25)
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

    'BUTTON HOVER
    'BUTTON EDIT 3 HOVER STUDENTS
    Private Sub btnEdit3_MouseEnter(sender As Object, e As EventArgs) Handles btnEdit3.MouseEnter
        btnEdit3.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnEdit3_MouseLeave(sender As Object, e As EventArgs) Handles btnEdit3.MouseLeave
        btnEdit3.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnEdit3_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEdit3.MouseDown
        btnEdit3.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnEdit3_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEdit3.MouseUp
        btnEdit3.BackColor = Color.FromArgb(97, 144, 118)
    End Sub


    'Internship Logs

    ' Private Sub pnlInternshipLogs_Paint(sender As Object, e As PaintEventArgs) Handles pnlInternshipLogs.Paint

    ' End Sub

    Private Sub txtSearchID4_TextChanged(sender As Object, e As EventArgs) Handles txtSearchID4.TextChanged

    End Sub

    Private Sub btnSearch4_Click(sender As Object, e As EventArgs) Handles btnSearch4.Click
        If txtSearchID4.Text.Trim() = "" Then
            MessageBox.Show("Please Enter Student ID or Name",
                        "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = searchInterTable(txtSearchID4.Text.Trim())

        If dt.Rows.Count > 0 Then
            ' --- show only the FIRST ROW ---
            Dim oneRow As DataTable = dt.Clone()          ' same structure
            oneRow.ImportRow(dt.Rows(0))                  ' add first row only
            dgvInternshipLogs4.DataSource = oneRow
        Else
            dgvInternshipLogs4.DataSource = Nothing
            MessageBox.Show("No record found!",
                        "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    'BUTTON HOVER
    'BUTTON SEARCH 4 HOVER INTERNSHIPS
    Private Sub btnSearch4_MouseEnter(sender As Object, e As EventArgs) Handles btnSearch4.MouseEnter
        btnSearch4.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearch4_MouseLeave(sender As Object, e As EventArgs) Handles btnSearch4.MouseLeave
        btnSearch4.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearch4_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearch4.MouseDown
        btnSearch4.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearch4_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch4.MouseUp
        btnSearch4.BackColor = Color.FromArgb(8, 48, 25)
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


    Private Sub btnSearch5_Click(sender As Object, e As EventArgs) Handles btnSearch5.Click
        Dim searchText As String = txtSearchID5.Text.Trim()

        If searchText = "" Then
            MessageBox.Show("Please enter a value to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = searchEvaluationTable(searchText)

        If dt.Rows.Count = 0 Then
            MessageBox.Show("No matching records found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dgvEvaluationLogs5.DataSource = Nothing
            Exit Sub
        End If

        dgvEvaluationLogs5.DataSource = dt

        ' Optional: DataGridView settings
        dgvEvaluationLogs5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEvaluationLogs5.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEvaluationLogs5.MultiSelect = False
        dgvEvaluationLogs5.ReadOnly = True
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
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlInternshipInformation.Show()
            pnlAddNewInternshipEvaluationRecord.Hide()
        End If
    End Sub

    Private Sub btnAdd6_Click(sender As Object, e As EventArgs) Handles btnAdd6.Click
        ' Validate required fields
        If String.IsNullOrWhiteSpace(txtInternshipID6.Text) OrElse
       String.IsNullOrWhiteSpace(txtEvaluationReport6.Text) OrElse
       cmbStatus6.Text = "" Then

            MessageBox.Show("Please complete all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Confirm add
        Dim result = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then
            Exit Sub
        End If

        ' Get grade (optional)
        Dim gradeValue As Decimal? = Nothing
        If nudEvaluationGrade.Value > 0 Then
            gradeValue = nudEvaluationGrade.Value
        End If

        ' Call function to save to database
        Dim success As Boolean = AddEvaluationRecord(
                                                        txtInternshipID6.Text.Trim(),
                                                        txtEvaluationReport6.Text.Trim(),
                                                         cmbStatus6.Text,
             If(nudEvaluationGrade.Value > 0, nudEvaluationGrade.Value, CType(Nothing, Decimal?))
)

        If success Then
            MessageBox.Show("Evaluation added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadEvaluationDGV()

            ' Clear form for next entry
            txtEvaluationID6.Text = ""    ' auto-generated
            txtInternshipID6.Clear()
            txtEvaluationReport6.Clear()
            cmbStatus6.SelectedIndex = -1
            nudEvaluationGrade.Value = 0

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
        pnlHome.Hide()
    End Sub
    Private Sub btnAdd8_Click(sender As Object, e As EventArgs) Handles btnAdd8.Click
        pnlAddNewCompanyandCompanyContact.Show()
        pnlHome.Hide()
    End Sub

    Private Sub btnEdit8_Click(sender As Object, e As EventArgs) Handles btnEdit8.Click
        pnlEditCompanyCompanyContact.Show()
        pnlHome.Hide()
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
        pnlCompanyInformation.Show()

        pnlCompanyContactInformation.Hide() ' ← hide the other parent panel

        pnlAddNewCompanyandCompanyContact.Show()
        pnlAddNewCompanyandCompanyContact.BringToFront()


    End Sub

    Private Sub btnEdit11_Click(sender As Object, e As EventArgs) Handles btnEdit11.Click
        ' Hide everything — safest reset
        hidePanel()

        ' Now show ONLY the Company Information (the master container)
        pnlCompanyInformation.Show()

        ' Then show ONLY the EDIT panel
        pnlEditCompanyCompanyContact.Show()
        pnlEditCompanyCompanyContact.BringToFront()




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
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            pnlVisitInformation.Show()
            pnlAddNewVisitRecord.Hide()
        End If

    End Sub
    Private Sub btnAdd18_Click(sender As Object, e As EventArgs) Handles btnAdd18.Click
        Dim result = MessageBox.Show("Do you want to add this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

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
        hidePanel()
        pnlStudentInformation.Show()
        lblTotalRecords1.Text = countStudentRecord()
        loadStudentRecord()
        pnlHome.Hide()
    End Sub

    'Update Internship Record
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        pnlUpdateInternshipRecord.Show()

    End Sub

    'BUTTON HOVER
    'BUTTON UPDATE HOVER INTERNSHIPS
    Private Sub btnUpdate_MouseEnter(sender As Object, e As EventArgs) Handles btnUpdate.MouseEnter
        btnUpdate.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnUpdate_MouseLeave(sender As Object, e As EventArgs) Handles btnUpdate.MouseLeave
        btnUpdate.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnUpdate_MouseDown(sender As Object, e As MouseEventArgs) Handles btnUpdate.MouseDown
        btnUpdate.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnUpdate_MouseUp(sender As Object, e As MouseEventArgs) Handles btnUpdate.MouseUp
        btnUpdate.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancelUpdate_Click(sender As Object, e As EventArgs) Handles btnCancelUpdate.Click
        Dim result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ClearUpdateForm()
            pnlUpdateInternshipRecord.Hide()
            pnlInternshipInformation.Show()
        End If
    End Sub


    Private Sub picHome_Click(sender As Object, e As EventArgs) Handles picHome.Click
        hidePanel()
        pnlHome.Show()
    End Sub

    Private Sub picInternshipIcon_Click(sender As Object, e As EventArgs) Handles picInternshipIcon.Click
        hidePanel()
        pnlInternshipInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picEvaluationIcon_Click(sender As Object, e As EventArgs) Handles picEvaluationIcon.Click
        hidePanel()
        pnlEvaluationInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picCompanyIcon_Click(sender As Object, e As EventArgs) Handles picCompanyIcon.Click
        hidePanel()
        pnlCompanyInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picFacultyIcon_Click(sender As Object, e As EventArgs) Handles picFacultyIcon.Click
        hidePanel()
        pnlFacultyInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picVisitIcon_Click(sender As Object, e As EventArgs) Handles picVisitIcon.Click
        hidePanel()
        pnlVisitInformation.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picSummary_Click(sender As Object, e As EventArgs) Handles picSummary.Click
        hidePanel()
        pnlSummaryReport.Show()
        pnlHome.Hide()
    End Sub

    Private Sub picImport1_Click(sender As Object, e As EventArgs) Handles picImport1.Click
        Dim ofd As New OpenFileDialog
        Dim csvAdded As Boolean
        ofd.Filter = "CSV Files (*.csv)|*.csv"

        If ofd.ShowDialog = DialogResult.OK Then
            Dim dt = LoadCSV(ofd.FileName)
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

    Private Sub picShow1_Click(sender As Object, e As EventArgs) Handles picShow1.Click
        Dim dv = CType(dgvStudentFiles.DataSource, DataView)
        dv.RowFilter = ""
    End Sub

    Private Sub picHide1_Click(sender As Object, e As EventArgs) Handles picHide1.Click
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

    Private Sub cmbSection3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection3.SelectedIndexChanged

    End Sub

    Public Sub DrawComboItem(sender As Object, e As DrawItemEventArgs)
        If e.Index < 0 Then Exit Sub

        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()

        Dim bgColor As Color
        Dim textColor As Color

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            bgColor = Color.FromArgb(218, 239, 228)  ' highlight
            textColor = Color.FromArgb(8, 48, 25)
        Else
            bgColor = Color.White                    ' normal
            textColor = Color.FromArgb(8, 48, 25)
        End If

        Using b As New SolidBrush(bgColor)
            e.Graphics.FillRectangle(b, e.Bounds)
        End Using

        ' CORRECT way to get item text
        Dim text As String = combo.GetItemText(combo.Items(e.Index))

        Using b As New SolidBrush(textColor)
            e.Graphics.DrawString(text, e.Font, b, e.Bounds)
        End Using

        e.DrawFocusRectangle()
    End Sub



    'BUTTON HOVER
    'BUTTON SEARCH INTERNSHIPS HOVER UPDATE
    Private Sub btnSearchInternship_MouseEnter(sender As Object, e As EventArgs) Handles btnSearchInternship.MouseEnter
        btnSearchInternship.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnSearchInternship_MouseLeave(sender As Object, e As EventArgs) Handles btnSearchInternship.MouseLeave
        btnSearch1.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnSearchInternship_MouseDown(sender As Object, e As MouseEventArgs) Handles btnSearchInternship.MouseDown
        btnSearch1.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnSearchInternship_MouseUp(sender As Object, e As MouseEventArgs) Handles btnSearch1.MouseUp
        btnSearch1.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    'BUTTON HOVER
    'BUTTON  CANCEL UPDATE HOVER INTERNSHIPS
    Private Sub btnCancelUpdate_MouseEnter(sender As Object, e As EventArgs) Handles btnCancelUpdate.MouseEnter
        btnCancelUpdate.BackColor = Color.FromArgb(8, 48, 25)

    End Sub

    Private Sub btnCancelUpdate_MouseLeave(sender As Object, e As EventArgs) Handles btnCancelUpdate.MouseLeave
        btnCancelUpdate.BackColor = Color.FromArgb(97, 144, 118)
    End Sub

    Private Sub btnCancelUpdate_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCancelUpdate.MouseDown
        btnCancelUpdate.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnCancelUpdate_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCancelUpdate.MouseUp
        btnCancelUpdate.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    'BUTTON HOVER
    'BUTTON UPDATE RECORD HOVER INTERNSHIPS
    Private Sub btnUpdateRecord_MouseEnter(sender As Object, e As EventArgs) Handles btnUpdateRecord.MouseEnter
        btnUpdateRecord.BackColor = Color.FromArgb(97, 144, 118)

    End Sub

    Private Sub btnUpdateRecord_MouseLeave(sender As Object, e As EventArgs) Handles btnUpdateRecord.MouseLeave
        btnUpdateRecord.BackColor = Color.FromArgb(8, 48, 25)
    End Sub

    Private Sub btnUpdateRecord_MouseDown(sender As Object, e As MouseEventArgs) Handles btnUpdateRecord.MouseDown
        btnUpdateRecord.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub btnUpdateRecord_MouseUp(sender As Object, e As MouseEventArgs) Handles btnUpdateRecord.MouseUp
        btnUpdateRecord.BackColor = Color.FromArgb(97, 144, 118)
    End Sub





    'CODE FOR UPDATE INTERNSHIP RECORD - IRIS

    ' ----- Module-level variable -----
    Private loadedInternshipID As String = ""
    ' ----- Search Internship -----
    Private Sub btnSearchInternship_Click(sender As Object, e As EventArgs) Handles btnSearchInternship.Click
        If txtboxInternshipID.Text.Trim() = "" Then
            MessageBox.Show("Please enter Internship ID!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dt As DataTable = searchInterTable(txtboxInternshipID.Text.Trim())
        If dt.Rows.Count = 0 Then
            MessageBox.Show("No internship record found!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim row As DataRow = dt.Rows(0)

        ' Fill textboxes
        txtFNameUpdateInternship.Text = row("First Name").ToString()
        txtLNameUpdateInternship.Text = row("Last Name").ToString()
        txtboxInternshipID.Text = row("Internship ID").ToString()

        ' Track loaded ID
        loadedInternshipID = txtboxInternshipID.Text.Trim()

        ' Load ComboBoxes and auto-select
        LoadCompanies(row("Company Name").ToString())
        LoadSupervisors(row("Supervisor Last Name").ToString())

        ' Set dates
        SetDatePicker(dtpStartDate, If(IsDBNull(row("Start_Date")), Nothing, CDate(row("Start_Date"))))
        SetDatePicker(dtpEndDate, If(IsDBNull(row("End_Date")), Nothing, CDate(row("End_Date"))))

        ' Set status
        cmbStatusUpdateInternship.Text = row("Status").ToString()
        cmbStatusUpdateInternship_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    ' ----- Load Companies ComboBox -----
    Private Sub LoadCompanies(selectedCompany As String)
        cmbCompanyInternship.Items.Clear()
        cmbCompanyInternship.DropDownStyle = ComboBoxStyle.DropDownList ' <-- non-editable

        Dim companies As New HashSet(Of String)

        Using con As MySqlConnection = GetConnection()
            con.Open()
            Dim cmd As New MySqlCommand("SELECT company_name FROM company", con)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim compName As String = reader("company_name").ToString()
                If Not companies.Contains(compName) Then
                    companies.Add(compName)
                    cmbCompanyInternship.Items.Add(compName)
                End If
            End While
        End Using

        ' Auto-select
        If selectedCompany <> "" AndAlso cmbCompanyInternship.Items.Contains(selectedCompany) Then
            cmbCompanyInternship.Text = selectedCompany
        End If
    End Sub

    ' ----- Load Supervisors ComboBox -----
    Private Sub LoadSupervisors(selectedSupervisor As String)
        cmbCompanyContactInternship.Items.Clear()
        cmbCompanyContactInternship.DropDownStyle = ComboBoxStyle.DropDownList ' <-- non-editable

        Dim dict As New Dictionary(Of String, List(Of String)) ' key = last name, value = list of first names

        Using con As MySqlConnection = GetConnection()
            con.Open()
            Dim cmd As New MySqlCommand("SELECT contact_last_name, contact_first_name FROM company_contact", con)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim lastName As String = reader("contact_last_name").ToString()
                Dim firstName As String = reader("contact_first_name").ToString()

                If dict.ContainsKey(lastName) Then
                    dict(lastName).Add(firstName)
                Else
                    dict.Add(lastName, New List(Of String) From {firstName})
                End If
            End While
        End Using

        ' Populate ComboBox
        For Each kvp In dict
            If kvp.Value.Count = 1 Then
                cmbCompanyContactInternship.Items.Add(kvp.Key)
            Else
                For Each fName In kvp.Value
                    cmbCompanyContactInternship.Items.Add($"{kvp.Key}, {fName}")
                Next
            End If
        Next

        ' Auto-select
        If selectedSupervisor <> "" AndAlso cmbCompanyContactInternship.Items.Contains(selectedSupervisor) Then
            cmbCompanyContactInternship.Text = selectedSupervisor
        End If
    End Sub


    ' ----- Status Logic -----
    Private Sub cmbStatusUpdateInternship_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatusUpdateInternship.SelectedIndexChanged
        Select Case cmbStatusUpdateInternship.Text
            Case "Pending"
                ' Start date: only if empty
                If dtpStartDate.CustomFormat = " " Then
                    dtpStartDate.Value = Date.Today
                    dtpStartDate.Enabled = False
                End If
                dtpEndDate.Enabled = False
                If dtpEndDate.CustomFormat = " " Then
                    dtpEndDate.Value = Date.Today
                    dtpEndDate.Enabled = False
                End If

            Case "Ongoing"
                dtpStartDate.Enabled = True
                ClearDatePicker(dtpEndDate)
                dtpEndDate.Enabled = False

            Case "Completed"
                dtpStartDate.Enabled = False
                dtpEndDate.Enabled = True
                If dtpEndDate.CustomFormat = " " Then SetDatePicker(dtpEndDate, Date.Today)
        End Select
    End Sub

    ' ----- Clear / Set DateTimePickers -----
    Private Sub ClearDatePicker(dtp As DateTimePicker)
        dtp.CustomFormat = " "
        dtp.Format = DateTimePickerFormat.Custom
    End Sub

    Private Sub SetDatePicker(dtp As DateTimePicker, dtValue As Date)
        dtp.CustomFormat = "yyyy-MM-dd"
        dtp.Format = DateTimePickerFormat.Custom
        dtp.Value = dtValue
    End Sub

    ' ----- Update Record Button -----
    Private Sub btnUpdateRecord_Click(sender As Object, e As EventArgs) Handles btnUpdateRecord.Click
        ' Case-insensitive, trimmed comparison
        If txtboxInternshipID.Text.Trim().ToUpper() <> loadedInternshipID.Trim().ToUpper() Then
            MessageBox.Show("Error: You can only update the originally loaded Internship ID.",
                        "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Call update function
        Dim success As Boolean = updateInternshipRecord(
                                txtboxInternshipID.Text,
                                cmbCompanyInternship.Text,
                                cmbCompanyContactInternship.Text,
                                dtpStartDate.Value,
                                dtpEndDate.Value,
                                cmbStatusUpdateInternship.Text
                             )

        If success Then
            MessageBox.Show("Internship record updated successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh DataGridView
            dgvInternshipFiles4.DataSource = searchInterTable("")

            ' Clear form
            ClearUpdateForm()
        End If
    End Sub

    ' ----- Cancel / Clear Form -----
    Private Sub ClearUpdateForm()
        txtboxInternshipID.Clear()
        txtFNameUpdateInternship.Clear()
        txtLNameUpdateInternship.Clear()
        cmbCompanyInternship.SelectedIndex = -1
        cmbCompanyContactInternship.SelectedIndex = -1
        dtpStartDate.CustomFormat = " "
        dtpEndDate.CustomFormat = " "
        cmbStatusUpdateInternship.SelectedIndex = -1
        loadedInternshipID = ""
    End Sub

    'EVALUATION DGV
    Private Sub LoadEvaluationDGV(Optional searchItem As String = "")
        dgvEvaluationFiles5.DataSource = LoadEvaluationTable()
        dgvEvaluationFiles5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEvaluationFiles5.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEvaluationFiles5.MultiSelect = False
        dgvEvaluationFiles5.ReadOnly = True
    End Sub

    'EVALUATION ADD
    Private Sub LoadEvaluationInfo(evaluationID As String)
        Dim dt As DataTable = GetEvaluationInfo(evaluationID)
        If dt.Rows.Count > 0 Then
            txtEvaluationID6.Text = dt.Rows(0)("evaluation_id").ToString()
            txtInternshipID6.Text = dt.Rows(0)("internship_id").ToString()
            txtEvaluationReport6.Text = dt.Rows(0)("evaluation_report").ToString()
            cmbStatus6.Text = dt.Rows(0)("evaluation_status").ToString()

            If Not IsDBNull(dt.Rows(0)("grade")) Then
                nudEvaluationGrade.Value = Convert.ToDecimal(dt.Rows(0)("grade"))
            Else
                nudEvaluationGrade.Value = 0
            End If
        End If
    End Sub

    Private Sub pnlAddNewInternshipEvaluationRecord_VisibleChanged(sender As Object, e As EventArgs) Handles pnlAddNewInternshipEvaluationRecord.VisibleChanged
        If pnlAddNewInternshipEvaluationRecord.Visible Then
            txtEvaluationID6.Text = GenerateEvaluationID()
            txtEvaluationID6.ReadOnly = True
            txtInternshipID6.Clear()
            txtEvaluationReport6.Clear()
            nudEvaluationGrade.Value = 0
        End If
    End Sub






    '1529, 902
End Class
