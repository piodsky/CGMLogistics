Public Class Transfer_Form

    Inherits Windows.Forms.Form

    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim id As Integer = 0
    Dim isClosing As Boolean
    Dim mydata_name, mydata_department, mydata_classification, mydata_position As DataTable
 
    Private Sub Transfer_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        connect(myquery)
        po_date.Format = DateTimePickerFormat.Custom
        po_date.CustomFormat = "yyyy-MM-dd HH:mm:ss"


        txtAccountableFROM.Enabled = False
        txtFundCluster.Text = CGSO_Inventory.txtSource.Text
        txtAccountableFROM.Text = CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(12).Value
    End Sub

    Private Sub btnITRSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnITRSave.Click


        Dim mydataitr As DataTable = myquery.runQuery("SELECT max(ITR_ID) from auto_itr")

        Dim id As Integer = mydataitr.Rows(0).Item(0) + 1

        Dim PTRITRNumber As String = "ITR" & Now.Year & "" & Now.Month & "" & id.ToString("0000")
        Dim itemid As String = ""



        Dim frmPTRITRNumber As New PTR_ITR_NUMBER
        With frmPTRITRNumber
            .PTRICSnumbergenerated = PTRITRNumber
            .ShowDialog()
        End With
        CGSO_Inventory.txtPTRITRNumber.Text = PTRITRNumber


        Dim ITRGeneratorReport As New rptViewerITR
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing
        Dim myDs As New DataSet



        'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

        crFile = ("Forms\Crystal reports\Inventory Inventory Transfer Report.rpt")

        rpTitle = "Inventory Transfer Report"


        Dim sql_string_string As String
        Dim _row = 0
        For i As Integer = 0 To CGSO_Inventory.dgvNewInventory.Rows.Count - 1
            If CGSO_Inventory.dgvNewInventory.Rows(i).Cells(0).Value = True Then
                If _row = 0 Then
                    sql_string_string = sql_string_string & CGSO_Inventory.dgvNewInventory.Rows(i).Cells(1).Value.ToString
                ElseIf _row > 0 Then
                    sql_string_string = sql_string_string & "," & "" & CGSO_Inventory.dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                End If
                _row += 1
                'Else
                '    MsgBox("Please Check Items to Print!")
                '    Exit Sub
            End If
        Next



        With myquery
            connect(myquery)

            .setTable("tbl_inventory_items_details")

            .addValue(txtAccountableTO.Text.ToString, "Accountable_Officer")
            .addValue(combo_position.Text.ToString, "Position")
            .addValue(combo_entity_departments.Text.ToString, "Location")
            .addValue(txtFundCluster.Text.ToString, "Fund_Cluster")
            .addValue(po_date.Text.ToString, "Date_Transfered")

            .runUpdate("INV_ID IN (" & sql_string_string & ")")
            MsgBox("Successfuly Saved!")
        End With

        Try

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, NumberSeries FROM tbl_inventory_items_details ti WHERE PO_Number = '" & CGSO_Inventory.txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)


            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)

        Catch ex As Exception

        End Try


        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Inventory_Inventory_Transfer_Report
        rptDoc.SetDataSource(myDs)

        rptDoc.SetParameterValue("Department", combo_entity_departments.Text)

        rptDoc.SetParameterValue("Fund_Cluster", txtFundCluster.Text)
        rptDoc.SetParameterValue("accountable_from", txtAccountableFROM.Text)
        rptDoc.SetParameterValue("accountable_to", txtAccountableTO.Text)


        rptDoc.SetParameterValue("CGSO_Officer", approved_by.Text)
        rptDoc.SetParameterValue("CGSO_Position", approved_designation.Text)

        rptDoc.SetParameterValue("Warehouse_Officer", issued_by.Text)
        rptDoc.SetParameterValue("Warehouse_Position", issued_designation.Text)


        rptDoc.SetParameterValue("Accountable_Officer", txtAccountableTO.Text)
        rptDoc.SetParameterValue("Accountable_Position", combo_position.Text)


        rptDoc.SetParameterValue("PTR_Number", PTRITRNumber)
        rptDoc.SetParameterValue("Purpose", CGSO_Inventory.purpose.Text)
        rptDoc.SetParameterValue("Initial", CGSO_Inventory.txtInitial.Text)

        rptDoc.SetParameterValue("TotalCost", CGSO_Inventory.txtTotal_Amount.Text)

        rptDoc.SetParameterValue("Supplier_Name", CGSO_Inventory.combo_supplier.Text)


        rptDoc.SetParameterValue("Date_Entry", CGSO_Inventory.Date_Entry.Text)

        rptDoc.SetParameterValue("ReasonforTransfer", txtReasonforTransfer.Text)

        With ITRGeneratorReport
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

        CGSO_Inventory.Close()


    End Sub
 
    Private Sub btnPTRSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPTRSave.Click
      



            Dim mydatapar As DataTable = myquery.runQuery("SELECT max(PTR_ID) from auto_ptr")

            Dim id As Integer = mydatapar.Rows(0).Item(0) + 1

        Dim PTRITRNumber As String = "PTR" & Now.Year & "" & Now.Month & "" & id.ToString("0000")
            Dim itemid As String = ""



        Dim frmPTRITRNumber As New PTR_ITR_NUMBER
            With frmPTRITRNumber
            .PTRICSnumbergenerated = PTRITRNumber
                .ShowDialog()
            End With
        CGSO_Inventory.txtPTRITRNumber.Text = PTRITRNumber


            Dim PTRGeneratorReport As New rptViewerPTR
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

        crFile = ("Forms\Crystal reports\Inventory Property Transfer Report.rpt")

            rpTitle = "Property Transfer Report"


        Dim sql_string_string As String
        Dim _row = 0
        For i As Integer = 0 To CGSO_Inventory.dgvNewInventory.Rows.Count - 1
            If CGSO_Inventory.dgvNewInventory.Rows(i).Cells(0).Value = True Then
                If _row = 0 Then
                    sql_string_string = sql_string_string & CGSO_Inventory.dgvNewInventory.Rows(i).Cells(1).Value.ToString
                ElseIf _row > 0 Then
                    sql_string_string = sql_string_string & "," & "" & CGSO_Inventory.dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                End If
                _row += 1
                'Else
                '    MsgBox("Please Check Items to Print!")
                '    Exit Sub
            End If
        Next



        With myquery
            connect(myquery)

            .setTable("tbl_inventory_items_details")

            .addValue(txtAccountableTO.Text.ToString, "Accountable_Officer")
            .addValue(combo_position.Text.ToString, "Position")
            .addValue(combo_entity_departments.Text.ToString, "Location")
            .addValue(txtFundCluster.Text.ToString, "Fund_Cluster")
            .addValue(po_date.Text.ToString, "Date_Transfered")

            .runUpdate("INV_ID IN (" & sql_string_string & ")")
            MsgBox("Successfuly Saved!")
        End With


            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, NumberSeries FROM tbl_inventory_items_details ti WHERE PO_Number = '" & CGSO_Inventory.txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)


            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_Property_Transfer_Report
            rptDoc.SetDataSource(myDs)

        rptDoc.SetParameterValue("Department", combo_entity_departments.Text)

        rptDoc.SetParameterValue("Fund_Cluster", txtFundCluster.Text)
            rptDoc.SetParameterValue("accountable_from", txtAccountableFROM.Text)
            rptDoc.SetParameterValue("accountable_to", txtAccountableTO.Text)


            rptDoc.SetParameterValue("CGSO_Officer", approved_by.Text)
            rptDoc.SetParameterValue("CGSO_Position", approved_designation.Text)

            rptDoc.SetParameterValue("Warehouse_Officer", issued_by.Text)
            rptDoc.SetParameterValue("Warehouse_Position", issued_designation.Text)

        rptDoc.SetParameterValue("Accountable_Officer", txtAccountableTO.Text)
        rptDoc.SetParameterValue("Accountable_Position", combo_position.Text)

            rptDoc.SetParameterValue("PTR_Number", PTRITRNumber)

            rptDoc.SetParameterValue("Initial", CGSO_Inventory.txtInitial.Text)
        rptDoc.SetParameterValue("Purpose", CGSO_Inventory.purpose.Text)

        rptDoc.SetParameterValue("TotalCost", CGSO_Inventory.txtTotal_Amount.Text)

        rptDoc.SetParameterValue("Supplier_Name", CGSO_Inventory.combo_supplier.Text)

        rptDoc.SetParameterValue("Date_Entry", CGSO_Inventory.Date_Entry.Text)

        rptDoc.SetParameterValue("ReasonforTransfer", txtReasonforTransfer.Text)

            With PTRGeneratorReport
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