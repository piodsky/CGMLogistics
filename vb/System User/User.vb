Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.Cursors
Imports System.Xml
Imports System.Drawing

Public Class User
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim a As String = " "
    Private Sub User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_load()
        id_load()
        dgvUser.Columns(0).FillWeight = 30
        dgvUser.Columns(1).FillWeight = 50
        dgvUser.Columns(2).FillWeight = 150
        dgvUser.Columns(1).FillWeight = 100
    End Sub
    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsername.KeyPress, txtf_name.KeyPress, txts_name.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub
    Public Sub user_name()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT users.user_id AS NO,users.User_Name as Username,CONCAT(hrmis.tblpersonalinformation.firstname,'" & a.ToString & "', hrmis.tblpersonalinformation.middlename,'" & a.ToString & "',hrmis.tblpersonalinformation.surname)AS NAME FROM users INNER JOIN hrmis.tblpersonalinformation ON hrmis.tblpersonalinformation.PIID = users.piid WHERE user_name LIKE '%" & txtUsername.Text & "%' ")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With
    End Sub
    Public Sub f_name()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT users.user_id AS NO,users.User_Name as Username,CONCAT(hrmis.tblpersonalinformation.firstname,'" & a.ToString & "', hrmis.tblpersonalinformation.middlename,'" & a.ToString & "',hrmis.tblpersonalinformation.surname)AS NAME FROM users INNER JOIN hrmis.tblpersonalinformation ON hrmis.tblpersonalinformation.PIID = users.piid WHERE firstname LIKE '%" & txtf_name.Text & "%' ")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With
    End Sub
    Public Sub s_name()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT users.user_id AS NO,users.User_Name as Username,CONCAT(hrmis.tblpersonalinformation.firstname,'" & a.ToString & "', hrmis.tblpersonalinformation.middlename,'" & a.ToString & "',hrmis.tblpersonalinformation.surname)AS NAME FROM users INNER JOIN hrmis.tblpersonalinformation ON hrmis.tblpersonalinformation.PIID = users.piid WHERE surname LIKE '%" & txts_name.Text & "%' ")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With
    End Sub


    Public Sub form_load()
        Dim a As String = " "
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT users.user_id as No, users.User_Name as Username, CONCAT(hrmis.tblpersonalinformation.firstname,'" & a.ToString & "', hrmis.tblpersonalinformation.middlename,'" & a.ToString & "',hrmis.tblpersonalinformation.surname) AS NAME, table_role.role AS Role FROM users INNER JOIN hrmis.tblpersonalinformation ON hrmis.tblpersonalinformation.PIID = users.piid INNER JOIN table_role ON table_role.role_id = users.role_id")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
            dgvUser.Columns(0).Visible = False
        End With

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Sys_User.TabControl1.SelectTab(0)
        Sys_User.TabControl1.TabPages(0).Enabled = True
        Sys_User.TabControl1.TabPages(1).Enabled = False
        Sys_User.user_state.text = "add"
        Sys_User.ShowDialog()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Sys_User.user_state.Text = "edit"
        Sys_User.ShowDialog()
       








    End Sub

    Private Sub dgvUser_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUser.CellClick
        id_load()

    End Sub
    Public Sub id_load()
        Dim dgvid As DataGridViewRow
        dgvid = dgvUser.SelectedRows.Item(0)
        id.Text = dgvid.Cells(0).Value

    End Sub

    Private Sub dgvUser_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvUser.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvUser.CurrentCell = dgvUser(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?
            id_load()

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        With myquery
            connect(myquery)
            If MsgBox("Are you sure you want to delete this user?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                mydata = .runQuery("Delete From Users Where user_id = '" & id.Text.ToString & "'")
                MsgBox("Successfully Deleted")
                Me.form_load()
            Else

            End If

        End With
    End Sub

    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        Sys_User.user_state.Text = "add"
        Sys_User.ShowDialog()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        With myquery
            connect(myquery)
            If MsgBox("Are you sure you want to delete this user?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                mydata = .runQuery("Delete From Users Where user_id = '" & id.Text.ToString & "'")
                MsgBox("Successfully Deleted")
                Me.form_load()
            Else

            End If

        End With
    End Sub

    Private Sub txts_name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txts_name.TextChanged
        s_name()
    End Sub

    Private Sub txtf_name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtf_name.TextChanged
        f_name()
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        user_name()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        txtUsername.ResetText()
        txtf_name.ResetText()
        txts_name.ResetText()
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Sys_User.user_state.Text = "edit"
        Sys_User.ShowDialog()

    End Sub

End Class