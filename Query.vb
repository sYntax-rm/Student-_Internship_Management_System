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
                Using cmd As New MySqlCommand("INSERT INTO student 
                                               (student_id, first_name, last_name, gender, section_name, 
                                                contact_no, email, department_id, course_id) 
                                               VALUES 
                                               (@studentID, @first_name, @last_name, @gender, @section, 
                                                @contact, @email, @departmentID, @courseID)", con)
                    con.Open()

                    cmd.Parameters.AddWithValue("@studentID", stdID)
                    cmd.Parameters.AddWithValue("@first_name", fname)
                    cmd.Parameters.AddWithValue("@last_name", lname)
                    cmd.Parameters.AddWithValue("@gender", gen)
                    cmd.Parameters.AddWithValue("@section", sec)
                    cmd.Parameters.AddWithValue("@contact", contact)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@departmentID", depID)
                    cmd.Parameters.AddWithValue("@courseID", crsID)

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
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return result.ToString()
    End Function



End Module
