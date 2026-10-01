Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.Cursors
Imports System.Xml
Imports System.Drawing

Public Class Choose_User

    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim a As String = " "
    Dim id_user As Integer = 0


    Private Sub Choose_User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtID.ResetText()
        txtf_name.ResetText()
        txts_name.ResetText()

        form_load()

        dgvUser.Columns(0).FillWeight = 100
        dgvUser.Columns(1).FillWeight = 130
        dgvUser.Columns(2).FillWeight = 400
        dgvUser.Columns(3).FillWeight = 400
    End Sub
   
     

    Public Sub form_load()

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT PIID as No, ID_NUM, CONCAT(firstname,'" & a.ToString & "', middlename,'" & a.ToString & "', surname,'" & a.ToString & "', NameExt) AS Name, ResidentialAdd as Address, Sex FROM hrmis.tblpersonalinformation where id_num > ' '")
            ''mydata = .runQuery("SELECT PIID as No, ID_NUM, CONCAT(firstname,'" & a.ToString & "', middlename,'" & a.ToString & "', surname,'" & a.ToString & "', NameExt) AS Name, ResidentialAdd as Address, Sex FROM hrmis.tblpersonalinformation")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With

    End Sub
    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress, txtf_name.KeyPress, txts_name.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub
   
    Public Sub id()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT PIID as No, ID_NUM, CONCAT(firstname,'" & a.ToString & "', middlename,'" & a.ToString & "', surname,'" & a.ToString & "', NameExt) AS Name, ResidentialAdd as Address, Sex FROM hrmis.tblpersonalinformation WHERE ID_NUM LIKE '%" & txtID.Text & "%' and id_num > ' '")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With
    End Sub
    Public Sub f_name()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT PIID as No, ID_NUM, CONCAT(firstname,'" & a.ToString & "', middlename,'" & a.ToString & "', surname,'" & a.ToString & "', NameExt) AS Name, ResidentialAdd as Address, Sex FROM hrmis.tblpersonalinformation WHERE firstname LIKE '%" & txtf_name.Text & "%' and id_num > ' '")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With
    End Sub
    Public Sub s_name()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT PIID as No, ID_NUM, CONCAT(firstname,'" & a.ToString & "', middlename,'" & a.ToString & "', surname,'" & a.ToString & "', NameExt) AS Name, ResidentialAdd as Address, Sex FROM hrmis.tblpersonalinformation WHERE surname LIKE '%" & txts_name.Text & "%' and id_num > ' '")
            dgvUser.DataSource = mydata
            dgvUser.Refresh()
        End With
    End Sub


  

    Private Sub dgvUser_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUser.CellClick
        id_load()
    End Sub

    Private Sub dgvUser_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvUser.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvUser.CurrentCell = dgvUser(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?
            id_load()
        End If
    End Sub


    Private Sub txtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtID.TextChanged
       
        id()
    End Sub

    Private Sub txtf_name_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtf_name.TextChanged

        txts_name.ResetText()
        txtID.ResetText()
        f_name()
    End Sub

    Private Sub txts_name_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txts_name.TextChanged
        
        s_name()
    End Sub

    Private Sub txtID_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txts_name.Enter, txtID.Enter, txtf_name.Enter
        txtID.ResetText()
        txts_name.ResetText()
        txtf_name.ResetText()
    End Sub

    Public Sub id_load()
        Dim dgvr_dgvUser As DataGridViewRow
        dgvr_dgvUser = dgvUser.SelectedRows.Item(0)
        id_user = dgvr_dgvUser.Cells(0).Value

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT * FROM hrmis.tblpersonalinformation WHERE PIID = '" & id_user.ToString & "'")
        End With

        For Each mydatarow In mydata.Rows
            With mydatarow

                id_number1.Text = CStr(.item("PIID"))
                id_number2.Text = CStr(.item("ID_NUM"))

            End With
        Next
    End Sub

    Private Sub dgvUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvUser.KeyDown
        e.Handled = True
    End Sub

   
    Private Sub dgvUser_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUser.SelectionChanged

    End Sub

    Private Sub UseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseToolStripMenuItem.Click
        test()
        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUse.Click
        test()
       
        Sys_User.btnAdd.Visible = False
        Sys_User.btnUpdate.Visible = False
        Sys_User.txtPIID.Visible = False
        Sys_User.txtIDNumber.Visible = False
        Sys_User.piid.Visible = False
        Sys_User.IDnumber.Visible = False
        If Sys_User.btnSave.Text = "Update" Then
            Sys_User.txtPIID.Visible = True
            Sys_User.txtIDNumber.Visible = True
            Sys_User.piid.Visible = True
            Sys_User.IDnumber.Visible = True
        End If
    End Sub
    Public Sub test()
        Dim myquery1 As New MyClassLibrary.MyHelper
        Dim mydata1 As DataTable
        Dim i As Integer
        With myquery1
            connect(myquery1)
            mydata1 = .runQuery("Select PIID From Users Where PIID = '" & id_number1.Text.ToString & "'")
        End With
        For Each mydatarow In mydata1.Rows
            With mydatarow

                i = CStr(.item("PIID"))


            End With
          
        Next
        If Sys_User.btnSave.Text = "Update" Then
            Sys_User.ID_num.Text = Me.id_number1.Text
            Sys_User.id_num2.Text = Me.id_number2.Text
            Sys_User.txtPIID.Text = Me.id_number1.Text
            Sys_User.txtIDNumber.Text = Me.id_number2.Text
            Me.Close()
        Else
            If i.ToString = id_number1.Text.ToString Then
                MsgBox("This User has already been registered", MsgBoxStyle.Critical)
                Exit Sub
            ElseIf i.ToString <> id_number1.Text.ToString Then

                Me.Close()
                Sys_User.ID_num.Text = Me.id_number1.Text
                Sys_User.id_num2.Text = Me.id_number2.Text
                Sys_User.txtPIID.Text = Me.id_number1.Text
                Sys_User.txtIDNumber.Text = Me.id_number2.Text
            End If
            Sys_User.txtPIID.Enabled = False
        End If
      
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        txts_name.ResetText()
        txtf_name.ResetText()
        txtID.ResetText()
    End Sub

  
    Private Sub AssignToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssignToolStripMenuItem.Click
        Assign_Office.ShowDialog()
    End Sub
End Class