Imports Microsoft.VisualBasic.Devices
Imports MySql.Data.MySqlClient

Module IDGenerator

    Function GenerateFacultyID() As String
        Dim currentMax As Integer = 0

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT MAX(CAST(SUBSTRING(faculty_id, 2) AS UNSIGNED)) FROM faculty;", con)
                    con.Open()

                    Dim result = cmd.ExecuteScalar()
                    If IsDBNull(result) Then
                        currentMax = 0
                    Else
                        currentMax = Convert.ToInt32(result)
                    End If

                    con.Close()
                End Using
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Increment by 1 and format with leading zeros
        currentMax += 1

        Return "F" & currentMax.ToString("D3")  ' D3 → 3 digits, e.g. F001
    End Function

    Public Function GenerateStudentID() As String
        Dim currentMax As Integer = 0

        Try
            Using con As MySqlConnection = GetConnection()
                Using cmd As New MySqlCommand("SELECT MAX(CAST(SUBSTRING(student_id, 2) AS UNSIGNED)) FROM student;", con)
                    con.Open()

                    Dim result = cmd.ExecuteScalar()
                    If IsDBNull(result) Then
                        currentMax = 0
                    Else
                        currentMax = Convert.ToInt32(result)
                    End If

                    con.Close()
                End Using
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Increment by 1 and format with leading zeros
        currentMax += 1

        Return "S" & currentMax.ToString("D3")  ' D3 → 3 digits, e.g. F001

    End Function



End Module
