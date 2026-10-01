Public Class Generate_Time
    Dim id As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim lbl_auto As String

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        With myquery
            connect(myquery)
            .setTable("tbl_purchase_order")
            .addValue(supplier_date.Text.ToString, "Date_Received_Supplier")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")


        End With
        MsgBox("Successfully Added! Date Received Supplier.")
        PO_Main_Form.form_load()
        Me.Close()
    End Sub

    Private Sub Generate_Time_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        supplier_date.Format = DateTimePickerFormat.Custom
        supplier_date.CustomFormat = "yyyy-MM-dd HH:mm:ss"
    End Sub
End Class