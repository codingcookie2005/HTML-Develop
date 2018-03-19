Public Class Form2
    Public Shared d As String = Char.ConvertFromUtf32(34)
    Public Shared lf As String = Char.ConvertFromUtf32(10)
    Public Shared code As String
    Private Shared Function HexConverter(ByVal c As Color) As String
        Return "#" & c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2")
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tf As Boolean
        If RichTextBox1.Text = "" Then
            tf = False
        Else
            tf = True
        End If
        If tf = False And CheckBox1.Checked = True Then
            code = "<html><body style = " + d + "background-color: " + HexConverter(RichTextBox1.BackColor) + d + ">" + "</body></html>"
            RichTextBox2.Text = code
        ElseIf tf = True And CheckBox1.Checked = True And CheckBox2.Checked = True Then
            code = "<html><body style = " + d + "background-color: " + HexConverter(RichTextBox1.BackColor) + d + ">" + "<u><" + ComboBox1.Text + ">" + RichTextBox1.Text + "</" + ComboBox1.Text + "></u></body></html>"
            RichTextBox2.Text = code
        ElseIf tf = True And CheckBox1.Checked = True And CheckBox2.Checked = False Then
            code = "<html><body style = " + d + "background-color: " + HexConverter(RichTextBox1.BackColor) + d + ">" + "<" + ComboBox1.Text + ">" + RichTextBox1.Text + "</" + ComboBox1.Text + "></body></html>"
            RichTextBox2.Text = code
        ElseIf tf = True And CheckBox1.Checked = False And CheckBox2.Checked = False Then
            code = "<html><" + ComboBox1.Text + ">" + RichTextBox1.Text + "</" + ComboBox1.Text + "></html>"
            RichTextBox2.Text = code
        ElseIf tf = True And CheckBox1.Checked = False And CheckBox2.Checked = True Then
            code = "<html><u><" + ComboBox1.Text + ">" + RichTextBox1.Text + "</" + ComboBox1.Text + "></u></html>"
            RichTextBox2.Text = code
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Button2.Visible = True
        Else
            Button2.Visible = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ff As New ColorDialog()
        ff.ShowDialog()
        If ff.Color <> Nothing Then
            RichTextBox1.BackColor = ff.Color

        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If ComboBox1.Text = "" Then
            MsgBox("Select A ComboBox item.")
            CheckBox2.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If RichTextBox1.Text = "" Then
            MsgBox("Add Text in Richtextbox")
            ComboBox1.SelectedIndex = 0
        End If
    End Sub
End Class