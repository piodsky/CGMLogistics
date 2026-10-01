Public Class Inspection_Login

    Private Sub Inspection_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUserName.Focus()
        OK.Focus()
        OK.Select()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim myquery As New MyClassLibrary.MyHelper
        Dim mydata As DataTable
        Dim mydatarow As DataRow

        connect(myquery)
        mydata = myquery.runQuery("SELECT users.user_id, users.user_name, tr.role_id, tr.role,users.piid FROM users INNER JOIN table_role tr ON tr.role_id = users.role_id WHERE users.user_name='" + txtUserName.Text.ToString + "' AND users.PASSWORD=MD5('" + txtPassword.Text.ToString + "')")
        If mydata.Rows.Count Then
            For Each mydatarow In mydata.Rows
                If mydatarow.Item("user_name").ToString = txtUserName.Text.ToString Then
                    USERID = mydatarow.Item("user_id")
                    USERNAME = mydatarow.Item("user_name")
                    USERLEVEL = mydatarow.Item("role_id")
                  
                End If
            Next
        End If

        If USERLEVEL = 1 Or USERLEVEL = 2 Or USERLEVEL = 3 Then
            If USERNAME.ToString = txtUserName.Text.ToString And USERNAME.ToString <> "" Then
                txtUserName.Focus()

                Me.Close()

                Inspect_Form.Inspect_Status.Text = "Edit/Update"
                Inspect_Form.btnSave.Enabled = True
                Inspect_Form.lblsave.Text = "Update"
                Inspect_Form.lblEdit.Visible = False
                Inspect_Form.btnEdit.Visible = False
                Inspect_Form.btnPrintDisbursement.Enabled = False
                Inspect_Form.btnPrintInspection.Enabled = False
                MsgBox("You can now Edit!")

                txtUserName.Text = ""
                txtPassword.Text = ""

                frmDashboard.roles()
            Else
                MsgBox("Wrong username/password!", MsgBoxStyle.Information)
                txtUserName.Focus()
            End If
        Else
            MsgBox("Your not elegible to log in! Please Contact your Administrator.")
        End If
    
    End Sub

    Private Sub Cancellogin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Cancellogin.LinkClicked
        Me.Close()
    End Sub

End Class