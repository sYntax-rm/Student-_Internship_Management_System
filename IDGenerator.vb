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



    Function GenerateDepartmentID(department As String) As String

        Dim depID As String = ""

        'College of Computer Studies D001
        'College of Engineering DOO2
        'College of Nursing D003
        'College of Hospitality Management D004
        'College of Education D005
        'College of Business And Accountancy DOO6
        'College of Arts And Sciences D007

        If department = "College of Arts And Sciences" Then
            depID = "D007"
        ElseIf department = "College of Business And Accountancy" Then
            depID = "D006"
        ElseIf department = "College of Education" Then
            depID = "D005"
        ElseIf department = "College of Engineering" Then
            depID = "D002"
        ElseIf department = "College of Computer Studies" Then
            depID = "D001"
        ElseIf department = "College of Hospitality Management" Then
            depID = "D004"
        ElseIf department = "College of Nursing" Then
            depID = "D003"
        End If

        Return depID
    End Function

    Function GenerateCourseID(course As String) As String

        Dim courseID As String = ""
        'BS Information Technology CR001
        'BS Electronics Engineering CR002
        'BS Nursing CR003  
        'BS Computer Science CR004
        'BS Accountancy CR005
        'BS Business Administration Major in Marketing Management CR006
        'BS Entrepreneurship CR007
        'BS Hospitality Management CR008 
        'Bachelor of Elementary Education CR009
        'Bachelor of Secondary Education Major in English CR010
        'Bachelor of Secondary Education Major in Filipino CR011
        'Bachelor of Secondary Education Major in Mathematics CR012
        'AB Psychology CR013


        If course = "BS Information Technology" Then
            courseID = "CR001"
        ElseIf course = "BS Electronics Engineering" Then
            courseID = "CR002"
        ElseIf course = "BS Nursing" Then
            courseID = "CR003"
        ElseIf course = "BS Computer Science" Then
            courseID = "CR004"
        ElseIf course = "BS Accountancy" Then
            courseID = "CR005"
        ElseIf course = "BS Business Administration Major in Marketing Management" Then
            courseID = "CR006"
        ElseIf course = "BS Entrepreneurship" Then
            courseID = "CR007"
        ElseIf course = "BS Hospitality Management" Then
            courseID = "CR008"
        ElseIf course = "Bachelor of Elementary Education" Then
            courseID = "CR009"
        ElseIf course = "Bachelor of Secondary Education Major in English" Then
            courseID = "CR010"
        ElseIf course = "Bachelor of Secondary Education Major in Filipino" Then
            courseID = "CR011"
        ElseIf course = "Bachelor of Secondary Education Major in Mathematics" Then
            courseID = "CR012"
        ElseIf course = "AB Psychology" Then
            courseID = "CR013"
        End If

        Return courseID

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
