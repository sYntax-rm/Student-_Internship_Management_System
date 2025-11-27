Imports MySql.Data.MySqlClient

Module Import
    Public Function LoadCSV(path As String) As DataTable
        Dim dataTable As New DataTable()

        Try
            Dim lines = IO.File.ReadAllLines(path)
            ' Add columns based on the first row (header)
            Dim header = lines(0).Split(","c)

            For Each col In header
                dataTable.Columns.Add(col.Trim())
            Next

            ' Add data rows
            For i As Integer = 1 To lines.Length - 1
                Dim values() = lines(i).Split(","c)
                Dim dr As DataRow = dataTable.NewRow()

                For j As Integer = 0 To Math.Min(values.Length - 1, dataTable.Columns.Count - 1)
                    dr(j) = values(j).Trim()
                Next

                dataTable.Rows.Add(dr)
            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

    Public Function addStudentsFromCsv(dt As DataTable) As Boolean

        Try
            Using con As MySqlConnection = GetConnection()
                con.Open()
                For Each row As DataRow In dt.Rows
                    Using cmd As New MySqlCommand("
                        INSERT INTO student 
                        (student_id, first_name, last_name, gender, section_name, contact_no, email, department_id, course_id)
                        VALUES
                        (@id,        @fn,           @ln, @gender, @sec, @contact, @email, @dep, @course)
                    ", con)

                        cmd.Parameters.AddWithValue("@id", GenerateStudentID())
                        cmd.Parameters.AddWithValue("@fn", row("first_name").ToString)
                        cmd.Parameters.AddWithValue("@ln", row("last_name").ToString)
                        cmd.Parameters.AddWithValue("@gender", row("gender").ToString)
                        cmd.Parameters.AddWithValue("@sec", row("section_name").ToString)
                        cmd.Parameters.AddWithValue("@contact", row("contact_no").ToString)
                        cmd.Parameters.AddWithValue("@email", row("email").ToString)
                        cmd.Parameters.AddWithValue("@dep", row("department_id").ToString)
                        cmd.Parameters.AddWithValue("@course", row("course_id").ToString)

                        cmd.ExecuteNonQuery()
                    End Using
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function

End Module
