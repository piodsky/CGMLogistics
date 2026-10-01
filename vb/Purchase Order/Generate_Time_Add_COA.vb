Public Class Generate_Time_Add_COA
    Dim id As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim lbl_auto As String

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        With myquery
            connect(myquery)
            .setTable("tbl_purchase_order")
            .addValue(coa_date.Text.ToString, "Date_COA_Received")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

        End With
        MsgBox("Successfully Added! Date COA Received.")
        PO_Main_Form.form_load()
        Me.Close()
    End Sub

    Private Sub Generate_Time_Add_COA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        coa_date.Format = DateTimePickerFormat.Custom
        coa_date.CustomFormat = "yyyy-MM-dd HH:mm:ss"
    End Sub
End Class