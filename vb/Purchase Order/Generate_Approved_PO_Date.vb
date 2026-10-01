Public Class Generate_Approved_PO_Date
    Dim id As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        With myquery
            connect(myquery)
            .setTable("tbl_purchase_order")
            .addValue(approved_po_date.Text.ToString, "Date_Approved_PO")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")


        End With
        MsgBox("Successfully Added! Date Approved PO!.")
        PO_Main_Form.form_load()
        Me.Close()
    End Sub

    Private Sub Generate_Approved_PO_Date_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        approved_po_date.Format = DateTimePickerFormat.Custom
        approved_po_date.CustomFormat = "yyyy-MM-dd HH:mm:ss"
    End Sub
End Class