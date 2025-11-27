Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class RoundedButton
    Inherits Button

    Public Sub New()
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.BackColor = Color.LightBlue
        Me.ForeColor = Color.Black
    End Sub

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        Dim graphics As Graphics = pevent.Graphics
        graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
        Dim path As GraphicsPath = New GraphicsPath()
        Dim radius As Integer = 33

        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        Me.Region = New Region(path)

        ' Fill background
        Using brush As New SolidBrush(Me.BackColor)
            graphics.FillPath(brush, path)
        End Using

        ' Draw border
        ' Using pen As New Pen(Color.Black, 2)
        'graphics.DrawPath(pen, path)
        'End Using

        ' Draw text
        TextRenderer.DrawText(graphics, Me.Text, Me.Font, rect, Me.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
    End Sub
End Class
