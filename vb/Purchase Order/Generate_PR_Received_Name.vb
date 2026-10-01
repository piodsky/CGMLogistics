Public Class Generate_PR_Received_Name
    Dim id As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        With myquery
            connect(myquery)
            .setTable("tbl_purchase_order")
            .addValue(combo_pr_received_name.Text, "PR_Received_Name")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")
        End With

        MsgBox("Successfully Added! PR Received Name.")
        PO_Main_Form.form_load()
        Me.Close()
    End Sub

    Private Sub Generate_PR_Received_Name_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class