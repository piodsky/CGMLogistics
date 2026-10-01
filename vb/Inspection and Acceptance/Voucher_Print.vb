Public Class Voucher_Print
    Dim id As Integer = 0

    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Private Sub Voucher_Print_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtAccountant.Text = Inspection_Main_Form.city_accountant.Text
        txtTreasurer.Text = Inspection_Main_Form.city_treasurer.Text
        txtMayor.Text = Inspection_Main_Form.city_mayor.Text
        txtSupplierName.Text = Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(3).Value
        txtSupplierAddress.Text = Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(4).Value
        txtTIN.Text = ""
        txtDepartment.Text = ""

        Inspect_Status.Text = Inspect_Form.Inspect_Status.Text
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click


        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing
        Dim myDs As New DataSet
        Dim InspectionGeneratorReport As New rpt_Viewer 'vb viewer



        rpTitle = "Disbursement Voucher"


        If Inspect_Status.Text = "View" Then


            With myquery
                connect(myquery)
                mydata = myquery.runQuery("SELECT SUM(Total_Cost) as Total_Cost FROM tbl_inspection_items_details WHERE PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' and isDeleted = 0")
                mydata.TableName = "tbl_inspection_items_details"
                myDs.Tables.Add(mydata)

            End With

        ElseIf Inspect_Status.Text = "For Progress Billing" Then

            With myquery
                connect(myquery)
                mydata = myquery.runQuery("SELECT SUM(Total_Cost) as Total_Cost FROM tbl_inspection_items_details_progress_billing WHERE PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' and Progress_Billing = '" & Inspect_Form.combo_progressbill.Text & "' and isDeleted = 0")
                mydata.TableName = "tbl_inspection_items_details"
                myDs.Tables.Add(mydata)

            End With
        End If
        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Disbursement_Voucher
        rptDoc.SetDataSource(myDs)

        'para ni sa txt box same parameter name
        rptDoc.SetParameterValue("TIN", txtTIN.Text)
        rptDoc.SetParameterValue("Department", txtDepartment.Text)
        rptDoc.SetParameterValue("Supplier", txtSupplierName.Text)
        rptDoc.SetParameterValue("Address", txtSupplierAddress.Text)
        rptDoc.SetParameterValue("Category", combo_category.Text)
        rptDoc.SetParameterValue("Category_Explanation", combo_category_explanation.Text)
        rptDoc.SetParameterValue("Accountant", txtAccountant.Text)
        rptDoc.SetParameterValue("Treasurer", txtTreasurer.Text)
        rptDoc.SetParameterValue("Mayor", txtMayor.Text)


        With myquery
            connect(myquery)
            .setTable("tbl_inspection")
            .addValue(combo_category.Text, "Category")
            .addValue(combo_category_explanation.Text, "Category_Explanation")

            .runUpdate("PO_Number ='" & Inspect_Form.PO_Number.Text & "'")

        End With

        With InspectionGeneratorReport
            .blnDataSource = True
            .ReportSource = rptSource
            .ReportPath = crFile
            .ReportTitle = rpTitle
            .crptViewer.ReportSource = rptDoc
            .WindowState = FormWindowState.Maximized
            .crptViewer.Zoom(75)
            .ShowDialog()
            .crptViewer.Refresh()
            .Dispose()


        End With

        Me.Close()
    End Sub


End Class