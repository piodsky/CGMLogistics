Public Class Obligation_Print
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Private Sub Obligation_Print_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



   
        connect(myquery)
        mydata = myquery.runQuery("SELECT tp.`Barcode_Number`, tp.`PO_Number`, tp.`PR_Number`, tp.`Department`, tp.`Division`, tp.`Section`, tp.`Supplier_Name`, tp.`Supplier_Address`, tp.`Date_Received_Supplier`, tp.`PR_Received_Name`, tp.`Category`, tp.`Date_Received_Supplier`, tp.`Date_COA_Received`, tp.`Date_Approved_PO`, tp.`Remarks`, tp.`Date_PO`, tp.`Purpose`, tp.`Requested_by`, tpd.`Item_ID`, tpd.`Items_Description`, tpd.`Unit`, tpd.`Quantity`, tpd.`Unit_Cost`, tpd.`Total_Cost`, tpd.`NumberSeries` FROM tbl_purchase_order tp INNER JOIN tbl_purchase_order_items_details tpd ON tp.PO_Number = tpd.PO_Number WHERE tp.isDeleted = 0 AND tpd.isDeleted = 0 AND tp.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
       With myfetcher
            .setDatatable(mydata)


            .getData(combo_category.Text, "Category")
            .getData(txtBarcodeNumber.Text, "Barcode_Number")


        End With

        'mydata2 = myquery.runQuery("SELECT SUM(Total_Cost) as Total_Cost, SUM(Quantity) as Quantity FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
        'With myfetcher
        '    .setDatatable(mydata2)


        '    .getData(txtTotalAmount.Text, "Total_Cost")

        'End With

        txtBudget.Text = PO_Main_Form.city_budget.Text
        txtDepartment.Text = PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(3).Value
        txtSupplierName.Text = PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(4).Value
        txtSupplierAddress.Text = PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(5).Value
        txtTotalAmount.Text = PO_Main_Form.txtSubTotal.Text
    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        With myquery

            .setTable("tbl_purchase_order")
            .addValue(txtBarcodeNumber.Text.ToString, "Barcode_Number")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")

        End With

        Dim dtpo As New DataTable
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing
        Dim myDs As New DataSet
        Dim PurchaseOrderGeneratorReport As New rptViewerObligation 'vb viewer



        rpTitle = "Obligation Request"







        With myquery
            connect(myquery)
            mydata = myquery.runQuery("SELECT SUM(Total_Cost) as Total_Cost, SUM(Quantity) as Quantity FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
            mydata.TableName = "tbl_purchase_order_items_details"
            myDs.Tables.Add(mydata)

        End With




        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Obligation_Request_Report
        rptDoc.SetDataSource(myDs)


        'para ni sa txt box same parameter name
        rptDoc.SetParameterValue("supplier_name", txtSupplierName.Text)
        rptDoc.SetParameterValue("supplier_address", txtSupplierAddress.Text)
        rptDoc.SetParameterValue("Department", txtDepartment.Text)
        rptDoc.SetParameterValue("Category", combo_category.Text)
        rptDoc.SetParameterValue("City_Budget", txtBudget.Text)
        rptDoc.SetParameterValue("End_User", combo_end_user.Text)
        rptDoc.SetParameterValue("Position", txtposition.Text)

        With PurchaseOrderGeneratorReport
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

    Private Sub btnSave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave2.Click


        With myquery

            .setTable("tbl_purchase_order")
            .addValue(txtBarcodeNumber.Text.ToString, "Barcode_Number")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")

        End With

        Dim dtpo As New DataTable
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing
        Dim myDs As New DataSet
        Dim PurchaseOrderGeneratorReport As New rptViewerObligation 'vb viewer



        rpTitle = "Obligation Request"







        With myquery
            connect(myquery)
            mydata = myquery.runQuery("SELECT SUM(Total_Cost) as Total_Cost, SUM(Quantity) as Quantity FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
            mydata.TableName = "tbl_purchase_order_items_details"
            myDs.Tables.Add(mydata)

        End With




        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Obligation_Request_Report_Reimbursement
        rptDoc.SetDataSource(myDs)


        'para ni sa txt box same parameter name
        rptDoc.SetParameterValue("total_cost", txtTotalAmount.Text)
        rptDoc.SetParameterValue("supplier_name", txtSupplierName.Text)
        rptDoc.SetParameterValue("supplier_address", txtSupplierAddress.Text)
        rptDoc.SetParameterValue("Department", txtDepartment.Text)
        rptDoc.SetParameterValue("Category", combo_category.Text)
        rptDoc.SetParameterValue("City_Budget", txtBudget.Text)
        rptDoc.SetParameterValue("End_User", combo_end_user.Text)
        rptDoc.SetParameterValue("Position", txtposition.Text)

        With PurchaseOrderGeneratorReport
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