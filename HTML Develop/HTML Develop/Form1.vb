Imports FastColoredTextBoxNS
Imports Plugins
Imports System.Reflection

Public Class Form1
    Public Shared menuLightf As Color
    Public Shared menuLightb As Color
    Private closeImage, closeImageAct As Image
    Public Shared code As String

    Private Function ff() As FastColoredTextBox
        Dim rtb As FastColoredTextBox
        rtb = Nothing
        Dim tp As TabPage
        tp = TabControl1.SelectedTab
        If Not tp Is Nothing And tp.Text <> "" Then
            rtb = tp.Controls(0)
        ElseIf tp Is Nothing Or tp.Text = "" Then
            rtb = Nothing
        End If
        Return rtb
    End Function

    Private Sub WithFileNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WithFileNameToolStripMenuItem.Click
        Dim fctb As New FastColoredTextBox()
        Dim tb As New TabPage()
        Dim dlg As New SaveFileDialog()
        dlg.Filter = "HTML Files(*.html)|*.html"
        dlg.ShowDialog()
        If dlg.FileName <> "" Then
            tb.Text = dlg.FileName
            fctb.Language = Language.HTML
            fctb.Dock = DockStyle.Fill
            tb.Controls.Add(fctb)
            TabControl1.TabPages.Add(tb)
        End If
    End Sub
    Private Sub tabControl1_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        e.Graphics.DrawString("X", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4)
        e.Graphics.DrawString(TabControl1.TabPages(e.Index).Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4)
        e.DrawFocusRectangle()
    End Sub
    Private Sub WithoutFileNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WithoutFileNameToolStripMenuItem.Click
        Dim fctb As New FastColoredTextBox()
        Dim tb As New TabPage()
        tb.Text = "Untitled"
        fctb.Language = Language.HTML
        fctb.Dock = DockStyle.Fill
        tb.Controls.Add(fctb)
        TabControl1.TabPages.Add(tb)

    End Sub

    Private Sub TabControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDown
        Dim i As Integer
        For i = 0 To Me.TabControl1.TabPages.Count - 1 Step i + 1
            Dim r As Rectangle = TabControl1.GetTabRect(i)
            Dim closeButton As Rectangle = New Rectangle(r.Right - 15, r.Top + 4, 9, 7)
            If closeButton.Contains(e.Location) Then
                If MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.TabControl1.TabPages.RemoveAt(i)
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim dlg As New OpenFileDialog()
        Dim fctb As New FastColoredTextBox()
        Dim tb As New TabPage()
        dlg.Filter = "HTML Files(*.html)|*.html"
        dlg.ShowDialog()
        If dlg.FileName <> "" Then
            tb.Text = dlg.FileName
            fctb.Language = Language.HTML
            fctb.Dock = DockStyle.Fill
            fctb.Text = System.IO.File.ReadAllText(dlg.FileName)
            tb.Controls.Add(fctb)
            TabControl1.TabPages.Add(tb)
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If TabControl1.TabPages.Count <> 0 Then
            If TabControl1.SelectedTab.Text <> "Untitled" Then
                System.IO.File.WriteAllText(TabControl1.SelectedTab.Text, ff().Text)
            Else
                MessageBox.Show("Sorry because this is an untitled document.Go to File>Save As to save.")
            End If
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If TabControl1.TabPages.Count <> 0 Then
            Dim dlg As New SaveFileDialog()
            dlg.Filter = "HTML Files(*.html)|*.html"
            dlg.ShowDialog()
            If dlg.FileName <> "" Then
                TabControl1.SelectedTab.Text = dlg.FileName
                System.IO.File.WriteAllText(dlg.FileName, ff().Text)
            End If
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        If Not ff() Is Nothing Then
            ff().Undo()
        End If


    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        If Not ff() Is Nothing Then
            ff().Redo()
        End If

    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        If Not ff() Is Nothing Then
            ff().Cut()
        End If

    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If Not ff() Is Nothing Then
            ff().Copy()
        End If

    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If Not ff() Is Nothing Then
            ff().Paste()
        End If
    End Sub

    Private Sub OnServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnServerToolStripMenuItem.Click
        If TabControl1.TabPages.Count <> 0 Then
            If TabControl1.SelectedTab.Text <> "Untitled" Then
                System.Diagnostics.Process.Start(TabControl1.SelectedTab.Text)
            Else
                MessageBox.Show("Please goto Run>On this browser to run an untitled document or click (File>Save As) then click (Run>On server)")
            End If
        End If
    End Sub

    Private Sub OnThisBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnThisBrowserToolStripMenuItem.Click
        If TabControl1.TabPages.Count <> 0 Then
            If Not ff() Is Nothing Then
                Dim tp As New TabPage()
                Dim wb As New WebBrowser()

                wb.Dock = DockStyle.Fill
                wb.DocumentText = ff().Text
                tp.Controls.Add(wb)
                TabControl1.TabPages.Add(tp)
                TabControl1.SelectedTab = tp
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        menuLightf = MenuStrip1.ForeColor
        menuLightb = MenuStrip1.BackColor
        Dim fctb As New FastColoredTextBox()
        Dim tb As New TabPage()
        tb.Text = "Untitled"
        fctb.Language = Language.HTML
        fctb.Dock = DockStyle.Fill
        tb.Controls.Add(fctb)
        TabControl1.TabPages.Add(tb)
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub
    Private Sub TimerEventProcessor(myObject As Object, ByVal myEventArgs As EventArgs) _
                                       Handles Timer1.Tick
        Textb1.Text = DateTime.Now.ToString("HH:mm:ss tt")
    End Sub
    Private Sub LightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LightToolStripMenuItem.Click
        MenuStrip1.ForeColor = menuLightf
        MenuStrip1.BackColor = menuLightb
        If Not TabControl1.SelectedTab Is Nothing Then
            If Not ff() Is Nothing Then
                ff().BackColor = Color.White
                ff().ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub DarkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DarkToolStripMenuItem.Click
        MenuStrip1.ForeColor = Color.White
        MenuStrip1.BackColor = Color.Black

        If Not TabControl1.SelectedTab Is Nothing Then
            If Not ff() Is Nothing Then
                ff().BackColor = Color.Black
                ff().ForeColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub LoadAssemblyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadAssemblyToolStripMenuItem.Click
        Dim pf As New OpenFileDialog()
        pf.Filter = "DLL Files(*.dll)|*.dll"
        If pf.ShowDialog() = DialogResult.OK Then
            Assembly.LoadFrom(pf.FileName)
            For Each a As Assembly In AppDomain.CurrentDomain.GetAssemblies()
                For Each t As Type In a.GetTypes()
                    If t.GetInterface("Plugin") <> Nothing Then
                        Dim p As Plugin
                        p = Activator.CreateInstance(t)
                        MessageBox.Show("Plugin Name = " & p.PluginName())
                        p.run()
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Dim tp As New TabPage()
        Dim tb As New FastColoredTextBox()
        tb.Multiline = True
        tb.Language = Language.CSharp
        tb.Dock = DockStyle.Fill
        tb.ReadOnly = True
        tb.Text = "using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugins; 
using System.Windows.Forms;
namespace Test
{
    public class MyPlugin : Plugin
    {
        public void run()
        {
           //write a program here 
        }
        public string PluginName()
        {
            //return the plugin name
        }
    }
}

-----------------------------------------------------------------
Class library has been tested only in .NET Framework 4.5.In above C# template the reference Plugins can be found at the Plugins.dll and Don't forget to fill the code in run() and PluginName()
You can also change the class name or namespace name"
        tp.Text = "Plugin Help"
        tp.Controls.Add(tb)
        TabControl1.TabPages.Add(tp)
    End Sub



    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        If Not TabControl1.SelectedTab Is Nothing Then
            Dim fd As New FontDialog()
            fd.ShowDialog()
            If Not fd.Font Is Nothing Then
                ff().Font = fd.Font

            End If
        End If
    End Sub

    Private Sub OpenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem1.Click
        Dim ff As New Form2()
        ff.ShowDialog()
    End Sub

    Private Sub CloseCurrentTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseCurrentTabToolStripMenuItem.Click
        If TabControl1.TabPages.Count <> 0 Then
            TabControl1.TabPages.Remove(TabControl1.SelectedTab)
        End If
    End Sub
End Class