Public Class Generate_Category
    Dim id As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable


    Private Sub Generate_Category_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        With myquery
            connect(myquery)
            .setTable("tbl_purchase_order")
            .addValue(combo_category.Text, "Category")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")
        End With

        MsgBox("Successfully Added! Category.")
        PO_Main_Form.form_load()
        Me.Close()
    End Sub
End Class