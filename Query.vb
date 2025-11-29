Imports MySql.Data.MySqlClient
Imports Windows.Win32.System

Module Query
    Public Function loadTable(query As String) As DataTable
        Dim dataTable As New DataTable()

        Try

            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function LoadCombo(query As String) As DataTable
        Dim dt As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dt)
                    End Using

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return dt
    End Function

    Public Function searchStudentTable(searchItem As String) As DataTable
        Dim dataTable As New DataTable()

        Dim query As String = "SELECT 
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
                              INNER JOIN course c ON s.course_id = c.course_id
                              WHERE s.first_name LIKE @search OR s.last_name LIKE @search OR
                              s.student_id LIKE @search;"

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@search", "%" & searchItem & "%")
                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function
    Public Function GetStudentInfo(studentID As String) As DataTable
        Dim dataTable As New DataTable()

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("
                SELECT *
                FROM student
                WHERE student_id = @id", con)
                    cmd.Parameters.AddWithValue("@id", studentID)
                    con.Open()

                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function addStudentRecord(stdID As String, fname As String, lname As String, gen As String, sec As String,
                                     contact As String, email As String, depID As String, crsID As String) As Boolean
        Try
            Using con As MySqlConnection = GetConnection()
                con.Open()
                Using trans As MySqlTransaction = con.BeginTransaction()

                    ' Insert student
                    Using studentCmd As New MySqlCommand("
                    INSERT INTO student 
                        (student_id, first_name, last_name, gender, section_name, contact_no, email, department_id, course_id) 
                    VALUES 
                        (@studentID, @first_name, @last_name, @gender, @section, @contact, @email, @departmentID, @courseID)", con, trans)

                        studentCmd.Parameters.AddWithValue("@studentID", stdID)
                        studentCmd.Parameters.AddWithValue("@first_name", fname)
                        studentCmd.Parameters.AddWithValue("@last_name", lname)
                        studentCmd.Parameters.AddWithValue("@gender", gen)
                        studentCmd.Parameters.AddWithValue("@section", sec)
                        studentCmd.Parameters.AddWithValue("@contact", contact)
                        studentCmd.Parameters.AddWithValue("@email", email)
                        studentCmd.Parameters.AddWithValue("@departmentID", depID)
                        studentCmd.Parameters.AddWithValue("@courseID", crsID)

                        studentCmd.ExecuteNonQuery()
                    End Using

                    ' Insert internship for the same student (default status = "Pending")
                    Using internCmd As New MySqlCommand("
                    INSERT INTO internship 
                        (internship_id, student_id, status) 
                    VALUES 
                        (@internID, @studentID, 'Pending')", con, trans)

                        internCmd.Parameters.AddWithValue("@internID", GenerateInternID())
                        internCmd.Parameters.AddWithValue("@studentID", stdID)

                        internCmd.ExecuteNonQuery()
                    End Using

                    ' Commit transaction
                    trans.Commit()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function

    Public Function updateStudentRecord(stdID As String, fname As String, lname As String, gen As String, sec As String,
                                     contact As String, email As String, depID As String, crsID As String) As Boolean


        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("UPDATE student SET first_name = @fname, last_name = @lname, gender = @gen,
                                                section_name = @sec, contact_no = @contact, email = @email, 
                                                department_id = @depID, course_id = @crsID WHERE student_id = @stdID", con)
                    con.Open()

                    cmd.Parameters.AddWithValue("@stdID", stdID)
                    cmd.Parameters.AddWithValue("@fname", fname)
                    cmd.Parameters.AddWithValue("@lname", lname)
                    cmd.Parameters.AddWithValue("@gen", gen)
                    cmd.Parameters.AddWithValue("@sec", sec)
                    cmd.Parameters.AddWithValue("@contact", contact)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@depID", depID)
                    cmd.Parameters.AddWithValue("@crsID", crsID)

                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function
    Public Function countStudentRecord() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(student_id) 
                                               FROM student;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countPendingIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(status) 
                                               FROM internship WHERE status = 'Pending' GROUP BY Status;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countOngoingIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(status) 
                                               FROM internship WHERE status = 'Ongoing' GROUP BY Status;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countCompletedIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(status) 
                                               FROM internship WHERE status = 'Completed' GROUP BY Status;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function

    Public Function countIntern() As String
        Dim result As Integer
        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT COUNT(*) 
                                               FROM internship;", con)
                    con.Open()

                    result = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function



    Public Function searchInterTable(searchItem As String) As DataTable
        Dim dataTable As New DataTable()

        Dim query As String = " SELECT i.internship_id AS 'Internship ID',
                                s.first_name AS 'First Name',
                                s.last_name AS 'Last Name',
                                c.company_name AS 'Company Name',
                                Cc.contact_last_name AS 'Supervisor Last Name',
                                Cc.contact_contact_no AS 'Supervisor Contact',
                                i.start_date AS 'Start_Date',
                                i.end_date AS 'End_Date',
                                i.status AS 'Status'
                                FROM internship i
                                LEFT JOIN student s  ON i.student_id = s.student_id
                                LEFT JOIN company c ON i.company_id = c.company_id
                                LEFT JOIN company_contact Cc ON i.contact_id = Cc.contact_id
                                WHERE s.first_name LIKE @search OR s.last_name LIKE @search OR
                                i.internship_id LIKE @search;
                                "

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand(query, con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@search", "%" & searchItem & "%")
                    Using dA As New MySqlDataAdapter(cmd)
                        dA.Fill(dataTable)
                    End Using
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function



End Module
