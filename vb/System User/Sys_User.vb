Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.Cursors
Imports System.Xml
Imports System.Drawing
Imports System.Security.Cryptography
Imports System.Text
Imports System
Imports System.IO


    Public Class Sys_User
        Dim id = User.id
        Dim myquery As New MyClassLibrary.MyHelper
        Dim mydata, mydata_pic, mydata1 As DataTable
        Public isEditing As Boolean = False
        Public remPic As Boolean = False
    Public picDel As Boolean = False


    Private values As String = ""
    Private table As String = ""
    Private fields As String = ""
    Private conn As MySqlConnection
    Private connstr As String

    Public errExist As Boolean

    Public Function get_connstr() As String
        Return connstr
    End Function
        Private Function makehash(ByVal input As String) As String
        Dim a As MD5 = MD5.Create()

            Dim b As Byte() = a.ComputeHash(Encoding.Default.GetBytes(input))
            Dim c As New StringBuilder()
            Dim i As Integer

        For i = 0 To b.Length - 1
            c.Append(b(i).ToString("x2"))
        Next
            Return c.ToString()
        End Function

        Public Sub reset_text2()
            ID_num.ResetText()
            id_num2.ResetText()
            txt_fname.ResetText()
            txt_mname.ResetText()
            txt_lname.ResetText()
            txt_BD.ResetText()
            txt_BP.ResetText()
            txt_sex.ResetText()
            txt_SS.ResetText()
            txt_H.ResetText()
            txt_W.ResetText()
            txt_BT.ResetText()
            txt_RA.ResetText()
            txt_PA.ResetText()
            txt_email.ResetText()
            txt_CN.ResetText()
            txtPIID.ResetText()
            txtIDNumber.ResetText()
        End Sub
        Public Sub reset_text()
            ID_num.ResetText()
            id_num2.ResetText()
            txt_fname.ResetText()
            txt_mname.ResetText()
            txt_lname.ResetText()
            txt_BD.ResetText()
            txt_BP.ResetText()
            txt_sex.ResetText()
            txt_SS.ResetText()
            txt_H.ResetText()
            txt_W.ResetText()
            txt_BT.ResetText()
            txt_RA.ResetText()
            txt_PA.ResetText()
            txt_email.ResetText()
            txt_CN.ResetText()
            myPic.Image = Nothing

            txt_user.ResetText()
            txt_password.ResetText()
            txt_confirm.ResetText()
            Combo_role.DataSource = Nothing
            TabControl1.SelectTab(0)



        End Sub
        Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_fname.KeyPress, txt_mname.KeyPress, txt_lname.KeyPress
            If Char.IsLetter(e.KeyChar) Then
                e.KeyChar = Char.ToUpper(e.KeyChar)
            End If
        End Sub
        Public Sub disabled_txtbox()
            Browse.Enabled = False
            Remove.Enabled = False
            txt_fname.Enabled = False
            txt_mname.Enabled = False
            txt_lname.Enabled = False
            birthdate.Enabled = False
            txt_BP.Enabled = False
            txt_BD.Enabled = False
            txt_sex.Enabled = False
            txt_SS.Enabled = False
            txt_H.Enabled = False
            txt_W.Enabled = False
            txt_BT.Enabled = False
            txt_RA.Enabled = False
            txt_PA.Enabled = False
            txt_email.Enabled = False
            txt_CN.Enabled = False
            txtPIID.Enabled = False
        txtIDNumber.Enabled = False
       
        End Sub

        Public Sub enabled_txtbox()
            Browse.Enabled = True
            Remove.Enabled = True
            txt_fname.Enabled = True
            txt_mname.Enabled = True
            txt_lname.Enabled = True
            birthdate.Enabled = True
            txt_BP.Enabled = True
            txt_sex.Enabled = True
            txt_SS.Enabled = True
            txt_H.Enabled = True
            txt_W.Enabled = True
            txt_BT.Enabled = True
            txt_RA.Enabled = True
            txt_PA.Enabled = True
            txt_email.Enabled = True
            txt_CN.Enabled = True
            txtPIID.Enabled = True
        txtIDNumber.Enabled = True
       
        End Sub
        Private Sub Sys_User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            birthdate.Format = DateTimePickerFormat.Custom
            birthdate.CustomFormat = "yyyy-MM-dd"
            birthdate.Visible = False
            disabled_txtbox()
            Label19.Visible = False
        Button3.Visible = False
            If user_state.Text = "add" Then
                reset_text()
                form_load()
                btnAdd.Visible = True
                btnSave.Visible = False
            btnUpdate.Visible = True
                txtPIID.Visible = True
                txtIDNumber.Visible = True
                txtPIID.Visible = True
                txtIDNumber.Visible = True
                btnAdd.Text = "New Employee"

            ElseIf user_state.Text = "edit" Then
                TabControl1.SelectTab(1)
                TabControl1.TabPages(1).Enabled = True
                TabControl1.TabPages(0).Enabled = True
                id_()
                combo_load()
                insert_edit()
                btnAdd.Visible = True
                btnSave.Visible = False
                txt_BD.Visible = False
                birthdate.Visible = True
                txtPIID.Visible = False
                txtIDNumber.Visible = False
                piid.Visible = False
                IDnumber.Visible = False
                Button2.Enabled = False
                btnAdd.Text = "Update Employee"
            End If
            Button2.Enabled = True
        End Sub


        Public Sub id_()
            With myquery
                connect(myquery)
                mydata1 = .runQuery("Select * From Users Where user_id = '" & User.id.Text.ToString & "'")
            End With
            For Each mydatarow In mydata1.Rows
                With mydatarow

                    ID_num.Text = CStr(.item("PIID"))


                End With

            Next
        End Sub
        Public Sub insert_edit()

            With myquery
                connect(myquery)
                mydata = .runQuery("SELECT u.user_id, u.piid,u.role_id, tr.role, u.user_name, u.password FROM users u INNER JOIN table_role tr ON u.role_id = tr.role_id WHERE u.user_id = '" & id.Text.ToString & "'")
                'mydata = .runQuery("SELECT * from users where user_id = '" & id.Text.ToString & "' ")

            End With

            For Each mydatarow In mydata.Rows
                With mydatarow
                    txt_user.Text = CStr(.item("user_name"))
                    Combo_role.Text = CStr(.item("role"))
                End With

            Next



        End Sub
        Public Sub form_load()

            TabControl1.TabPages(1).Enabled = False

        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            reset_text()
            Choose_User.ShowDialog()
            Button_next.Enabled = True
            Choose_User.UseToolStripMenuItem.Visible = True
            Choose_User.btnUse.Visible = True

        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_next.Click
            txt_user.Text = id_num2.Text

            TabControl1.SelectedTab = TabPage2

            TabControl1.TabPages(1).Enabled = True
            combo_load()
        End Sub

        Private Sub TextBox17_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
            e.Handled = True


        End Sub


        Private Sub ID_num_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ID_num.TextChanged

            With myquery
                connect(myquery)
            mydata = .runQuery("SELECT * FROM hrmis.tblpersonalinformation tp LEFT JOIN tblpersonalpic pp ON pp.file_name = tp.id_num  WHERE tp.PIID = '" & ID_num.Text.ToString & "'")
            'mydata1 = .runQuery("SELECT pp.file FROM tblpersonalinformation tp INNER JOIN tblpersonalpic pp ON pp.file_name = tp.id_num  WHERE tp.PIID = = '" & ID_num.Text.ToString & "'")

                'myPic.Image = mydata_pic.
            End With



            For Each mydatarow In mydata.Rows
                With mydatarow
                    Dim pic_id As String

                    pic_id = CStr(.item("piid"))
                txt_fname.Text = CStr(.item("FirstName"))
                    txt_mname.Text = CStr(.item("middlename"))
                    txt_lname.Text = CStr(.item("surname"))
                    txt_BD.Text = CStr(.item("birthdate"))
                    txt_BP.Text = CStr(.item("birthplace"))
                    txt_sex.Text = CStr(.item("sex"))
                    txt_SS.Text = CStr(.item("civilstatus"))
                    txt_H.Text = CStr(.item("height"))
                    txt_W.Text = CStr(.item("weight"))
                txt_BT.Text = CStr(.item("bloodtype"))

                    txt_RA.Text = CStr(.item("residentialadd"))
                    txt_PA.Text = CStr(.item("permanentadd"))
                    txt_email.Text = CStr(.item("email"))
                    txt_CN.Text = CStr(.item("cellno"))


                    Try
                        myPic.Image = myquery.getPic(pic_id.ToString, "tblpersonalpic")

                    Catch ex As Exception
                        'MsgBox("No image shown")
                        myPic.ImageLocation = ("/Purchase Request system/Resources/no image.jpg")
                    End Try

                End With
            Next



        End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txt_password.PasswordChar = Nothing
            txt_confirm.PasswordChar = Nothing
        Else
            txt_password.PasswordChar = "•"
            txt_confirm.PasswordChar = "•"
        End If
    End Sub

        Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)



        End Sub
        Public Sub save()
            Try

           

                Dim pass As String
                pass = makehash(txt_password.Text)
                With myquery
                    connect(myquery)
                    .setTable("users")
                    .addValue(ID_num.Text.ToString, "piid")
                    .addValue(role_id.Text.ToString, "role_id")
                    .addValue(txt_user.Text.ToString, "user_name")
                .addValue(pass, "password")
                .addValue(combo_dept.Text, "Department")

                    If user_state.Text = "add" Then
                        .runInsert()

                        default_role()
                        MsgBox("Successfully Saved!")


                    ElseIf user_state.Text = "edit" Then
                        .runUpdate("user_id ='" & id.Text.ToString & "'")
                        default_role()
                        MsgBox("Successfully Edited!")

                    End If


                End With
                txt_user.ResetText()
                Combo_role.ResetText()
                txt_password.ResetText()
                txt_confirm.ResetText()

                Me.Close()
                TabControl1.SelectTab(0)
                TabControl1.TabPages(0).Enabled = True
                TabControl1.TabPages(1).Enabled = False
                User.form_load()

            Catch ex As Exception
                MsgBox(ex)
            End Try

        End Sub

        Public Sub default_role()
            With myquery
                connect(myquery)
                .setTable("system_access_level")
                .addValue(role_id.Text.ToString, "role_id")
                .addValue(ID_num.Text.ToString, "piid")

                If role_id.Text = 1 Then
                    .addValue("1", "System")
                    .addValue("1", "SYSTEM_USER")
                    .addValue("1", "system_access_level")
                    .addValue("1", "maintainance_table")
                    .addValue("1", "personel_administration")
                    .addValue("1", "database_management")
                    .addValue("1", "procurement")
                    .addValue("1", "Purchase_Request")
                    .addValue("1", "canvas_quotation")
                    .addValue("1", "Abstract")
                    .addValue("1", "purchase_order")
                    .addValue("1", "inspection_and_acceptance")
                    .addValue("1", "inventory")
                    .addValue("1", "segregation")
                    .addValue("1", "ics")
                    .addValue("1", "are")
                    .addValue("1", "ARCHIVE")
                    .addValue("1", "warehousing")
                    .addValue("1", "reports")
                ElseIf role_id.Text = 2 Then
                    .addValue("1", "System")
                    .addValue("1", "SYSTEM_USER")
                    .addValue("1", "system_access_level")
                    .addValue("0", "maintainance_table")
                    .addValue("0", "personel_administration")
                    .addValue("0", "database_management")
                    .addValue("1", "procurement")
                    .addValue("1", "Purchase_Request")
                    .addValue("1", "canvas_quotation")
                    .addValue("1", "Abstract")
                    .addValue("1", "purchase_order")
                    .addValue("1", "inspection_and_acceptance")
                    .addValue("1", "inventory")
                    .addValue("1", "segregation")
                    .addValue("1", "ics")
                    .addValue("1", "are")
                    .addValue("1", "ARCHIVE")
                    .addValue("1", "warehousing")
                    .addValue("1", "reports")
                ElseIf role_id.Text = 3 Then
                    .addValue("1", "System")
                    .addValue("0", "SYSTEM_USER")
                    .addValue("0", "system_access_level")
                    .addValue("0", "maintainance_table")
                    .addValue("0", "personel_administration")
                    .addValue("0", "database_management")
                    .addValue("1", "procurement")
                    .addValue("1", "Purchase_Request")
                    .addValue("1", "canvas_quotation")
                    .addValue("1", "Abstract")
                    .addValue("1", "purchase_order")
                    .addValue("1", "inspection_and_acceptance")
                    .addValue("0", "inventory")
                    .addValue("0", "segregation")
                    .addValue("0", "ics")
                    .addValue("0", "are")
                    .addValue("0", "ARCHIVE")
                    .addValue("0", "warehousing")
                    .addValue("1", "reports")
                ElseIf role_id.Text = 4 Then
                    .addValue("1", "System")
                    .addValue("0", "SYSTEM_USER")
                    .addValue("0", "system_access_level")
                    .addValue("0", "maintainance_table")
                    .addValue("0", "personel_administration")
                    .addValue("0", "database_management")
                    .addValue("0", "procurement")
                    .addValue("0", "Purchase_Request")
                    .addValue("0", "canvas_quotation")
                    .addValue("0", "Abstract")
                    .addValue("0", "purchase_order")
                    .addValue("0", "inspection_and_acceptance")
                    .addValue("1", "inventory")
                    .addValue("1", "segregation")
                    .addValue("1", "ics")
                    .addValue("1", "are")
                    .addValue("1", "ARCHIVE")
                    .addValue("0", "warehousing")
                    .addValue("1", "reports")
                ElseIf role_id.Text = 5 Then
                    .addValue("1", "System")
                    .addValue("0", "SYSTEM_USER")
                    .addValue("0", "system_access_level")
                    .addValue("0", "maintainance_table")
                    .addValue("0", "personel_administration")
                    .addValue("0", "database_management")
                    .addValue("0", "procurement")
                    .addValue("0", "Purchase_Request")
                    .addValue("0", "canvas_quotation")
                    .addValue("0", "Abstract")
                    .addValue("0", "purchase_order")
                    .addValue("0", "inspection_and_acceptance")
                    .addValue("0", "inventory")
                    .addValue("0", "segregation")
                    .addValue("0", "ics")
                    .addValue("0", "are")
                    .addValue("0", "ARCHIVE")
                    .addValue("1", "warehousing")
                    .addValue("1", "reports")
                ElseIf role_id.Text = 6 Then
                    .addValue("1", "System")
                    .addValue("0", "SYSTEM_USER")
                    .addValue("0", "system_access_level")
                    .addValue("0", "maintainance_table")
                    .addValue("0", "personel_administration")
                    .addValue("0", "database_management")
                    .addValue("1", "procurement")
                    .addValue("1", "Purchase_Request")
                    .addValue("0", "canvas_quotation")
                    .addValue("0", "Abstract")
                    .addValue("0", "purchase_order")
                    .addValue("0", "inspection_and_acceptance")
                    .addValue("0", "inventory")
                    .addValue("0", "segregation")
                    .addValue("0", "ics")
                    .addValue("0", "are")
                    .addValue("0", "ARCHIVE")
                    .addValue("0", "warehousing")
                    .addValue("1", "reports")
                ElseIf role_id.Text = 7 Then
                    .addValue("1", "System")
                    .addValue("0", "SYSTEM_USER")
                    .addValue("0", "system_access_level")
                    .addValue("0", "maintainance_table")
                    .addValue("0", "personel_administration")
                    .addValue("0", "database_management")
                    .addValue("0", "procurement")
                    .addValue("0", "Purchase_Request")
                    .addValue("0", "canvas_quotation")
                    .addValue("0", "Abstract")
                    .addValue("0", "purchase_order")
                    .addValue("0", "inspection_and_acceptance")
                    .addValue("0", "inventory")
                    .addValue("0", "segregation")
                    .addValue("0", "ics")
                    .addValue("0", "are")
                    .addValue("0", "ARCHIVE")
                    .addValue("0", "warehousing")
                    .addValue("1", "reports")
                End If
                If user_state.Text = "add" Then
                    .runInsert()
                Else
                    .runUpdate("role_id ='" & role_id.Text.ToString & "'")
                End If



            End With
        End Sub

        Public Sub combo_load()
            With myquery
                connect(myquery)
            If mdiMain.role.Text = 1 Then
                mydata = .runQuery("SELECT * FROM table_role")
            ElseIf mdiMain.role.Text = 2 Or 3 Or 5 Then
                mydata = .runQuery("SELECT * FROM table_role WHERE role_id <> 1 AND role_id <> 2")
            End If

            End With
            For Each mydatarow In mydata.Rows
                With mydatarow

                    Combo_role.DataSource = mydata
                    Combo_role.DisplayMember = "role"
                    Combo_role.ValueMember = "role_id"
                    Combo_role.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    Combo_role.AutoCompleteSource = AutoCompleteSource.ListItems
                    Combo_role.Refresh()
                End With
            Next
        End Sub
        Public Sub txt_role_id()

            Dim myquery_role_id As New MyClassLibrary.MyHelper
            Dim mydata_role_id As DataTable

            With myquery_role_id
                connect(myquery_role_id)
                mydata_role_id = .runQuery("SELECT * FROM table_role WHERE role = '" & Combo_role.Text.ToString & "'")
            End With
            For Each mydatarow_role_id In mydata_role_id.Rows
                With mydatarow_role_id
                    role_id.Text = CStr(.item("role_id"))
                End With
            Next
        End Sub


        Private Sub Combo_role_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Combo_role.TextChanged
            txt_role_id()
        End Sub


        Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        End Sub




        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click



        'For i As Integer = (Me.employee_details.Controls.Count - 1) To 0 Step -1
        '    If Me.employee_details.Controls(i).Text.ToString = "" Then
        '        MsgBox("Please leave no blank in each entry", MsgBoxStyle.Information)
        '        Me.employee_details.Controls(i).Focus()
        '        Exit Sub
        '    End If
        'Next i
        With myquery
            connect(myquery)
            .setTable("tblpersonalinformation")

            .addValue(txt_fname.Text.ToString, "FirstName")
            .addValue(txt_mname.Text.ToString, "middlename")
            .addValue(txt_lname.Text.ToString, "surname")
            .addValue(birthdate.Text.ToString, "birthdate")
            .addValue(txt_BP.Text.ToString, "birthplace")
            .addValue(txt_sex.Text.ToString, "sex")
            .addValue(txt_SS.Text.ToString, "civilstatus")
            .addValue(txt_H.Text.ToString, "height")
            .addValue(txt_W.Text.ToString, "weight")
            .addValue(txt_BT.Text.ToString, "bloodtype")
            .addValue(txt_RA.Text.ToString, "residentialadd")
            .addValue(txt_PA.Text.ToString, "permanentadd")
            .addValue(txt_email.Text.ToString, "email")
            .addValue(txt_CN.Text.ToString, "cellno")


            If user_state.Text = "add" And btnSave.Text = "Save" Then
                .addValue(txtPIID.Text.ToString, "PIID")
                .addValue(txtIDNumber.Text.ToString, "ID_NUM")
                .runInsert()
            ElseIf user_state.Text = "add" And btnSave.Text = "Update" Then
                .addValue(txtPIID.Text.ToString, "PIID")
                .addValue(txtIDNumber.Text.ToString, "ID_NUM")
                .runUpdate("piid = '" & txtPIID.Text & "'")

            End If


            If myPic.ImageLocation <> "" Then
                If isEditing And picDel = False Then
                    .updatePic(myPic.ImageLocation, txtIDNumber.Text, "tblpersonalpic", txtPIID.Text)
                    MsgBox("CHANGES has made, Please Restart the Application to make Changes")
                Else
                    .storePic(myPic.ImageLocation, txtIDNumber.Text, "tblpersonalpic", txtPIID.Text)

                End If
            End If
        End With
        MsgBox("Data successfully saved.", MsgBoxStyle.Information)
        isEditing = True

   








            reset_text2()
            btnSave.Visible = False
        btnAdd.Visible = True
        Me.Close()
        End Sub


        Private Sub Upload_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Browse.LinkClicked
            Try
                With OpenFileDialog1
                    .Filter = ("Images | *.png; *.bmp; *.jpg; *.gif; *.ico;")
                    .FilterIndex = 4
                End With
                'Clear the file name
                OpenFileDialog1.FileName = "Select Image"
                If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    myPic.ImageLocation = OpenFileDialog1.FileName
                    myPic.Refresh()
                    remPic = False
                End If

            Catch ex As Exception

            End Try
        End Sub

        Private Sub Remove_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Remove.LinkClicked
            myPic.Image = Nothing
            myPic.ImageLocation = ""
            remPic = True
            myPic.Refresh()
        End Sub


        Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            If txt_user.Text = "" Then
                MsgBox("Invalid Username!")
            Else
                If txt_password.Text > " " Or txt_confirm.Text > " " Then
                    If txt_password.Text = txt_confirm.Text Then
                        not_match.Visible = False
                        save()
                    Else
                        not_match.Visible = True
                    End If

                Else
                    MsgBox("Invalid Password!")
                End If
            End If
        End Sub









    ' ------------------------------------- UPLOADING IMAGE ------------------------    



        Public Function setConnectionErrno(ByVal server As String, ByVal port As String, ByVal user As String, ByVal pass As String, ByVal db As String)
            Dim errnoLog As Boolean = False

            connstr = "server=" & server & ";" _
            & "port=" & port & ";" _
            & "user id=" & user & ";" _
            & "password=" & pass & ";" _
            & "database=" & db & ";"

            Dim mycommand As New MySqlCommand
            Dim myadapter As New MySqlDataAdapter

            conn = New MySqlConnection
            conn.ConnectionString = connstr

            Try
                conn.Open()
            Catch ex As MySqlException
                'MsgBox("Error in setConnectionString: User Access Denied " & user, MsgBoxStyle.Critical)
                errnoLog = True
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
            Return errnoLog

        End Function 'set the connection Test if Good

        Public Sub setConnection(ByVal server As String, ByVal port As String, ByVal user As String, ByVal pass As String, ByVal db As String)
            connstr = "server=" & server & ";" _
            & "port=" & port & ";" _
            & "user id=" & user & ";" _
            & "password=" & pass & ";" _
            & "database=" & db & ";"

            errExist = False

            Dim mycommand As New MySqlCommand
            Dim myadapter As New MySqlDataAdapter

            conn = New MySqlConnection
            conn.ConnectionString = connstr

            Try
                conn.Open()
            Catch ex As MySqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                errExist = True
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Sub 'set the connection

        Public Sub clear()
            values = ""
            table = ""
            fields = ""
        End Sub 'clear all the values before reuse of run functions

        Public Sub addValue(ByVal value As String, ByVal field As String)
            field = field.Replace(",", " ")
            'value = value.Replace("\", "\\")
            value = value.Replace("'", "\'")
            value = Trim(value)
            If value = "NOW()" Or value.Contains("md5(") Then
                value = value.Replace("\'", "'")
                values += "~&^" & value
            ElseIf values = "" Then
                values = "'" & value & "'"
            Else
                values += "~&^" & "'" & value & "'"
            End If

            If fields = "" Then
                fields = "`" + field + "`"
            Else
                fields += "," & "`" + field + "`"
            End If
        End Sub 'adding values with fields for insert and update query

        Public Sub setTable(ByVal mytable As String)
            table = mytable
        End Sub 'set table name

        Public Function runInsert()
            errExist = False
            If values = "" Or table = "" Or fields = "" Then
                Return False
            Else
                Dim mycommand As New MySqlCommand
                Dim myadapter As New MySqlDataAdapter
                Dim mydata As New DataTable
                Dim sql As String

                conn = New MySqlConnection
                conn.ConnectionString = connstr

                values = values.Replace("~&^", ",")

                sql = "INSERT INTO " & table & " (" & fields & ") VALUES(" & values & ");"

                Try
                    conn.Open()
                    Try
                        mycommand.Connection = conn
                        mycommand.CommandText = sql

                        myadapter.SelectCommand = mycommand
                        myadapter.Fill(mydata)
                        clear()
                    Catch ex As MySqlException
                        MsgBox(ex.Message, MsgBoxStyle.Critical)
                        errExist = True
                    End Try
                Catch ex As MySqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    errExist = True
                Finally
                    If conn.State <> ConnectionState.Closed Then conn.Close()
                End Try
                Return True
            End If
        End Function 'executes insert query then clear all the data from class except connstr

        Public Function runUpdate(ByVal condition As String)
            errExist = False
            If values = "" Or table = "" Or fields = "" Then
                Return False
            Else
                Dim mycommand As New MySqlCommand
                Dim myadapter As New MySqlDataAdapter
                Dim mydata As New DataTable
                Dim sql As String
                Dim arg As String = ""
                Dim field() As String
                Dim value() As String
                Dim i As Integer

                field = fields.Split(",")
                value = values.Split("~&^")

                For i = 0 To value.GetUpperBound(0)
                    value(i) = value(i).Replace("&^", "")
                    If i = 0 Then
                        arg += field(i) & "=" & value(i)
                    Else
                        arg += ", " & field(i) & "=" & value(i)
                    End If
                Next

                conn = New MySqlConnection
                conn.ConnectionString = connstr

                sql = "UPDATE " & table & " SET " & arg & " WHERE " & condition

                Try
                    conn.Open()
                    Try
                        mycommand.Connection = conn
                        mycommand.CommandText = sql

                        myadapter.SelectCommand = mycommand
                        myadapter.Fill(mydata)
                        clear()
                    Catch ex As MySqlException
                        'MsgBox(ex.Message, MsgBoxStyle.Critical)
                        errExist = True
                    End Try
                Catch ex As MySqlException
                    'MsgBox(ex.Message, MsgBoxStyle.Critical)
                    errExist = True
                Finally
                    If conn.State <> ConnectionState.Closed Then conn.Close()
                End Try
                Return True
            End If
        End Function 'executes update query with condition parameter then clear all data from class except connstr

        Public Sub getValue(ByVal field As String)
            If fields = "" Then
                fields = field
            Else
                fields += "," & field
            End If
        End Sub 'get the fields to fetch value

        Public Function runSelect(ByVal condition As String)
            Dim mycommand As New MySqlCommand
            Dim myadapter As New MySqlDataAdapter
            Dim mydata As New DataTable
            Dim sqlquery As String = ""
            conn = New MySqlConnection
            conn.ConnectionString = connstr

            sqlquery = "SELECT " + fields + " FROM " + table + " " + condition

            Try
                conn.Open()
                Try
                    mycommand.Connection = conn
                    mycommand.CommandText = sqlquery

                    myadapter.SelectCommand = mycommand
                    myadapter.Fill(mydata)
                    Return mydata
                Catch ex As MySqlException
                    'MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
                    errExist = True
                End Try
            Catch ex As MySqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                errExist = True
            Finally
                If conn.State <> ConnectionState.Closed Then conn.Close()
            End Try
            Return True
        End Function

        Public Function runQuery(ByVal sqlquery As String, Optional ByVal returnWhat As String = "Table")
            Dim mycommand As New MySqlCommand
            Dim myadapter As New MySqlDataAdapter
            Dim mydata As New DataTable

            conn = New MySqlConnection
            conn.ConnectionString = connstr

            Try
                conn.Open()

                mycommand.Connection = conn
                mycommand.CommandText = sqlquery

                myadapter.SelectCommand = mycommand
                myadapter.Fill(mydata)

                Select Case returnWhat
                    Case "Adapter"
                        Return myadapter
                    Case "Table"
                        Return mydata
                End Select

            Catch ex As MySqlException
                MsgBox("Error in openning connection: " & ex.Message, MsgBoxStyle.Critical)
            Finally
                If conn.State <> ConnectionState.Closed Then conn.Close()
            End Try
            Return mydata
        End Function


        ''StorePicture
        Public Function storePic(ByVal location As String, ByVal Filename As String, ByVal tableName As String, ByVal id As String)
            Dim cmd As New MySqlCommand
            Dim SQL As String
            Dim errno101 As Boolean
            Dim FileSize As UInt32
            Dim rawData() As Byte = {}
            Dim fs As FileStream

            Try
                If location <> "" Then
                    fs = New FileStream(location, FileMode.Open, FileAccess.Read)
                    FileSize = fs.Length

                    rawData = New Byte(FileSize) {}
                    fs.Read(rawData, 0, FileSize)
                    fs.Close()
                End If


                conn.Open()

                SQL = "INSERT INTO " & tableName & " VALUES(?ID, ?FileName, ?FileSize, ?File)"

                cmd.Connection = conn
                cmd.CommandText = SQL
                cmd.Parameters.AddWithValue("?ID", id)
                cmd.Parameters.AddWithValue("?FileName", Filename)
                cmd.Parameters.AddWithValue("?FileSize", FileSize)
                cmd.Parameters.AddWithValue("?File", rawData)

                cmd.ExecuteNonQuery()

                'MsgBox("File Inserted into database successfully!", MsgBoxStyle.Critical)

                conn.Close()
                errno101 = True

            Catch ex As Exception
                'MsgBox("There was an error: " & ex.Message, MsgBoxStyle.Critical)
                errno101 = False
            End Try
            Return errno101

        End Function
        ''UpdatePicture
        Public Function updatePic(ByVal location As String, ByVal Filename As String, ByVal tableName As String, ByVal id As String)
            Dim cmd As New MySqlCommand
            Dim SQL As String
            Dim errno101 As Boolean
            Dim FileSize As UInt32
            Dim rawData() As Byte = {}
            Dim fs As FileStream

            Try
                If location <> "" Then
                    fs = New FileStream(location, FileMode.Open, FileAccess.Read)
                    FileSize = fs.Length

                    rawData = New Byte(FileSize) {}
                    fs.Read(rawData, 0, FileSize)
                    fs.Close()
                End If


                conn.Open()

                SQL = "UPDATE " & tableName & " SET file_name = ?FileName ,file_size = ?FileSize ,file = ?File WHERE file_id = ?ID"

                cmd.Connection = conn
                cmd.CommandText = SQL
                cmd.Parameters.AddWithValue("?ID", id)
                cmd.Parameters.AddWithValue("?FileName", Filename)
                cmd.Parameters.AddWithValue("?FileSize", FileSize)
                cmd.Parameters.AddWithValue("?File", rawData)

                cmd.ExecuteNonQuery()

                'MsgBox("File Inserted into database successfully!", MsgBoxStyle.Critical)

                conn.Close()
                errno101 = True

            Catch ex As Exception
                'MsgBox("There was an error: " & ex.Message, MsgBoxStyle.Critical)
                errno101 = False
            End Try
            Return errno101

        End Function
        ''Get Picture
        Public Function getPic(ByVal id As String, ByVal tableName As String)
            Dim cmd As New MySqlCommand
            Dim myData As MySqlDataReader
            Dim SQL As String
            Dim rawData() As Byte
            Dim FileSize As UInt32
            Dim ad As New MemoryStream(100000)
            Dim image As Bitmap

            SQL = "SELECT file_id,file_name, file_size, file FROM " & tableName & " WHERE file_id = ?id"
            Try
                conn.Open()

                cmd.Connection = conn
                cmd.CommandText = SQL
                cmd.Parameters.AddWithValue("?id", id)
                myData = cmd.ExecuteReader

                If Not myData.HasRows Then Throw New Exception("There are no BLOBs to get")

                myData.Read()

                FileSize = myData.GetUInt32(myData.GetOrdinal("file_size"))
                rawData = New Byte(FileSize) {}

                myData.GetBytes(myData.GetOrdinal("file"), 0, rawData, 0, FileSize)
                ad.Write(rawData, 0, FileSize)
                myData.Close()
                conn.Close()

            Catch ex As Exception
                'MsgBox("There was an error: " & ex.Message, MsgBoxStyle.Critical)
            End Try
            image = New Bitmap(ad)
            Return image
        End Function



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        btnAdd.Visible = True
        btnUpdate.Visible = True
        Label19.Visible = False
 
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Button3.Visible = True
        If user_state.Text = "add" Then
            enabled_txtbox()

            txt_BD.Visible = False
            birthdate.Visible = True
            btnSave.Visible = True
            btnUpdate.Visible = False
            Label19.Visible = True
            btnAdd.Visible = False

            Button_next.Visible = False

            btnSave.Text = "Update"
            Label19.Text = "Update Employee"

        ElseIf user_state.Text = "edit" Then
            enabled_txtbox()
            txt_BD.Visible = False
            birthdate.Visible = True
            btnSave.Visible = True
            btnAdd.Visible = False

            Button_next.Visible = False
            Label19.Text = "Update Details"
            btnSave.Text = "Update"

        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Button3.Visible = True
        If user_state.Text = "add" Then
            enabled_txtbox()
            txt_BD.Visible = False
            birthdate.Visible = True
            btnSave.Visible = True
            btnAdd.Visible = False

            Button_next.Visible = False
            Label19.Visible = True
            btnUpdate.Visible = False
            Label19.Text = "Registered New Employee"
            btnSave.Text = "Save"
        ElseIf user_state.Text = "edit" Then
            enabled_txtbox()
            txt_BD.Visible = False
            birthdate.Visible = True
            btnSave.Visible = True
            btnAdd.Visible = False

            Button_next.Visible = False
            Label19.Text = "Update Details"
            btnSave.Text = "Update"

        End If
    End Sub

 
End Class


