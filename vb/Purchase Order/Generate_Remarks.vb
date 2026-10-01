Public Class Generate_Remarks
    Dim id As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim lbl_auto As String
    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        With myquery
            connect(myquery)
            .setTable("tbl_purchase_order")
            .addValue(txtRemarks.Text, "Remarks")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

        End With
        MsgBox("Successfully Added! Remarks.")
        PO_Main_Form.form_load()
        Me.Close()
    End Sub
End Class