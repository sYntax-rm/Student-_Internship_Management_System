Public Class DGVHelper
    ' Para sa tracking ng hover row per DGV
    Private Shared hoverRows As New Dictionary(Of DataGridView, Integer)

    ' Method para i-bind ang DataTable at i-style ang DGV
    Public Shared Sub BindAndStyleDGV(dgv As DataGridView, dt As DataTable, Optional columnWidths As Dictionary(Of String, Integer) = Nothing)
        ' Filter Hidden column if exists
        If dt.Columns.Contains("Hidden") Then
            Dim dv As New DataView(dt)
            dv.RowFilter = "Hidden = False"
            dgv.DataSource = dv
            dgv.Columns("Hidden").Visible = False
        Else
            dgv.DataSource = dt
        End If

        ' Set column widths
        If columnWidths IsNot Nothing Then
            For Each colName In columnWidths.Keys
                If dgv.Columns.Contains(colName) Then
                    dgv.Columns(colName).Width = columnWidths(colName)
                End If
            Next
        End If

        ' Header styles
        dgv.EnableHeadersVisualStyles = False
        dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None
        dgv.RowHeadersDefaultCellStyle.BackColor = Color.MintCream
        dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.LightYellow
        dgv.RowHeadersWidth = 20

        ' Selection color same as normal
        dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor
        dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor

        ' Clear selection & remove highlight from first cell
        dgv.ClearSelection()
        dgv.CurrentCell = Nothing

        ' Initialize hover tracking
        If Not hoverRows.ContainsKey(dgv) Then
            hoverRows.Add(dgv, -1)
            AddHandler dgv.CellMouseEnter, AddressOf CellMouseEnter
            AddHandler dgv.CellMouseLeave, AddressOf CellMouseLeave
        End If
    End Sub

    ' Hover events
    Private Shared Sub CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        If e.RowIndex >= 0 Then
            Dim lastRow As Integer = hoverRows(dgv)
            If lastRow >= 0 AndAlso lastRow <> e.RowIndex Then
                dgv.Rows(lastRow).DefaultCellStyle.BackColor = Color.White
            End If
            dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Honeydew
            hoverRows(dgv) = e.RowIndex
        End If
    End Sub

    Private Shared Sub CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim lastRow As Integer = hoverRows(dgv)
        If lastRow >= 0 Then
            dgv.Rows(lastRow).DefaultCellStyle.BackColor = Color.White
            hoverRows(dgv) = -1
        End If
    End Sub
End Class
