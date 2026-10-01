Public Class cgso_login
    Dim mydata1, mydata2, mydata3, mydata4, mydata5 As DataTable
    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Private Sub cgso_login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtUserName.Focus()

    


    End Sub


    Private Sub OKEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKEDIT.Click


        If txtUserName.Text = "EDIT" And txtPassword.Text = "EDIT" Then
            MsgBox("You can now Edit.")
            CGSO_Inventory.btnEdit.Visible = False
            Me.Close()
            txtUserName.Text = ""
            txtPassword.Text = ""
            CGSO_Inventory.inventory_state.Text = "Edit/Update"
            CGSO_Inventory.btnSave.Text = "Update"
        Else
            MsgBox("Wrong username/password!", MsgBoxStyle.Information)
            txtUserName.Focus()
        End If

    End Sub

    Private Sub OKDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKDELETE.Click
   

        If txtUserName.Text = "DELETE" And txtPassword.Text = "DELETE" Then
          
            With myquery
                connect(myquery)
                .setTable("tbl_inventory")
                .addValue("1", "isDeleted")
                .addValue(mdiMain.stbUser.Text, "Deleted_By")

                .runUpdate("PO_Number ='" & CGSO_Inventory.txtPONUMBER.Text & "'")
            End With

            With myquery
                connect(myquery)
                .setTable("tbl_inventory_items_details")
                .addValue("1", "isDeleted")
                .addValue(mdiMain.stbUser.Text, "Deleted_By")

                .runUpdate("PO_Number ='" & CGSO_Inventory.txtPONUMBER.Text & "'")
            End With

            Me.Close()
            CGSO_Main_Form.form_load_inventory()
            CGSO_Inventory.Close()

            txtUserName.Text = ""
            txtPassword.Text = ""

            MsgBox("Succefully Deleted!")

        Else
            MsgBox("Wrong username/password!", MsgBoxStyle.Information)

            txtUserName.Focus()
        End If

    End Sub

    Private Sub Cancellogin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Cancellogin.LinkClicked
        Me.Close()
    End Sub
End Class