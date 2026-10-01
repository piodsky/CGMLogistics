Imports System.Windows.Forms.Cursors
Imports System.Xml
Imports System.Drawing

Public Class add_time_po
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim Currentvalue As Date = Nothing
    Dim mydata As DataTable
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim sql_string As String
    Private Sub add_time_po_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combo_incharge.Text = PO_Main_Form.city_mayor.Text


        connect(myquery)
        mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")

        With myfetcher
            .setDatatable(mydata)
            .getData(combo_MODE.Text, "Mode_of_Procurement")
            .getData(combo_PLACE.Text, "Place_of_Delivery")

            .getData(date_printed.Text, "PO_Date_Printed")
            .getData(txtNOTE.Text, "Note")


            .getData(Ref_Number.Text, "Reference_No")
            .getData(BAC_Resolution_Number.Text, "BAC_Resolution_No")
            '.getData(combo_days.Text, "PO_Working_Days")
            '.getData(combo_payment.Text, "PO_Payment_Term")
     
        End With
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If mdiMain.role.Text <> 7 Then
            If combo_days.Text = "As Needed" Then '"As Needed" And "Saturday" Then
                'Or "Sunday" Or "Monday" Or "Tuesday" Or "Wednesday" Or "Thursday" Or "Friday" Or "Daily" Or "Weekly" Or "Monthly" Or "Yearly" Then

                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text.ToString, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text.ToString, "Place_of_Delivery")
                    '.addValue(Ref_Number.Text, "Reference_No")
                    '.addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")



                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With

            ElseIf combo_days.Text <> "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text.ToString, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text.ToString, "Place_of_Delivery")
                    '.addValue(Ref_Number.Text, "Reference_No")
                    '.addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With
            Else

                Dim new_date As Date = Now
                For x As Integer = 0 To combo_days.Text - 1
                    new_date = new_date.AddDays(1)
                    If new_date.ToString("dddd") = "Saturday" Or new_date.ToString("dddd") = "Sunday" Then
                        x = x - 1
                    End If
                Next

                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text.ToString, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text.ToString, "Place_of_Delivery")
                    ' .addValue(Ref_Number.Text, "Reference_No")
                    ' .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")
                    .addValue(new_date.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Incurred")
                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With
            End If



            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"






            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With

            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)

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

        End If
        


    End Sub



    Private Sub btnPrintbyLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintbyLot.Click
        If mdiMain.role.Text <> 7 Then
            If combo_days.Text = "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    ' .addValue(Ref_Number.Text, "Reference_No")
                    '.addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With


            ElseIf combo_days.Text <> "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    '.addValue(Ref_Number.Text, "Reference_No")
                    ' .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With

            Else

                Dim new_date As Date = Now
                For x As Integer = 0 To combo_days.Text - 1
                    new_date = new_date.AddDays(1)
                    If new_date.ToString("dddd") = "Saturday" Or new_date.ToString("dddd") = "Sunday" Then
                        x = x - 1
                    End If
                Next

                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    ' .addValue(Ref_Number.Text, "Reference_No")
                    ' .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")
                    .addValue(new_date.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Incurred")
                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With
            End If



            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"





            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With


            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report_By_LOT
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)

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
        Else

            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"





            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With


            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report_By_LOT
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)

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
        End If
        


    End Sub

    Private Sub btnPrintPublicBidding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPublicBidding.Click
        If mdiMain.role.Text <> 7 Then
       
            If combo_days.Text = "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    ' .addValue(Ref_Number.Text, "Reference_No")
                    ' .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With


            ElseIf combo_days.Text <> "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    '  .addValue(Ref_Number.Text, "Reference_No")
                    ' .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With

            Else

                Dim new_date As Date = Now
                For x As Integer = 0 To combo_days.Text - 1
                    new_date = new_date.AddDays(1)
                    If new_date.ToString("dddd") = "Saturday" Or new_date.ToString("dddd") = "Sunday" Then
                        x = x - 1
                    End If
                Next

                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    '.addValue(Ref_Number.Text, "Reference_No")
                    '.addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")
                    .addValue(new_date.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Incurred")
                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With
            End If



            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"





            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With


            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report_Public_Bidding
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)
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
        Else

            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"





            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With


            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report_Public_Bidding
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)
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
        End If
    End Sub

    Private Sub btnPrintPublicBiddingbyLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPublicBiddingbyLot.Click
        If mdiMain.role.Text <> 7 Then


            If combo_days.Text = "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    '.addValue(Ref_Number.Text, "Reference_No")
                    '.addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With


            ElseIf combo_days.Text <> "As Needed" Then
                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    '.addValue(Ref_Number.Text, "Reference_No")
                    ' .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")

                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With

            Else

                Dim new_date As Date = Now
                For x As Integer = 0 To combo_days.Text - 1
                    new_date = new_date.AddDays(1)
                    If new_date.ToString("dddd") = "Saturday" Or new_date.ToString("dddd") = "Sunday" Then
                        x = x - 1
                    End If
                Next

                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(combo_MODE.Text, "Mode_of_Procurement")
                    .addValue(combo_PLACE.Text, "Place_of_Delivery")
                    ' .addValue(Ref_Number.Text, "Reference_No")
                    '.addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")
                    .addValue(txtNOTE.Text, "Note")
                    .addValue(combo_days.Text + " Working Days", "PO_Working_Days")
                    .addValue(combo_payment.Text + " Working Days Payment Term", "PO_Payment_Term")
                    .addValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Printed")
                    .addValue(new_date.ToString("yyyy-MM-dd HH:mm:ss"), "PO_Date_Incurred")
                    .runUpdate("PO_Number ='" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "'")

                End With
            End If



            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"





            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With


            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report_Public_Bidding_By_LOT
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)

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

        Else



            Dim dtpo As New DataTable
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet
            Dim PurchaseOrderGeneratorReport As New rptViewerPO 'vb viewer



            rpTitle = "Purchase Order Report"





            With myquery
                mydata = myquery.runQuery("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Section, po.Supplier_Name, po.Supplier_Address, po.Date_PO, po.Purpose, po.Requested_by, po.Mode_of_Procurement, po.Place_of_Delivery, po.Bac_Resolution_No, po.Reference_No, po.PO_Working_Days, po.Purpose, po.End_User FROM tbl_purchase_order po INNER JOIN ref_department rd ON po.`Department` = rd.`ID` INNER JOIN ref_division rdi ON po.`Division` = rdi.`ID` INNER JOIN ref_section rs ON po.`Section` = rs.`ID` WHERE po.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' and po.isDeleted = 0")
                mydata.TableName = "tbl_purchase_order"
                myDs.Tables.Add(mydata)

            End With


            With myquery
                mydata = myquery.runQuery("SELECT PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries FROM tbl_purchase_order_items_details WHERE PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows.Item(0).Cells(0).Value & "' AND isDeleted = 0")
                mydata.TableName = "tbl_purchase_order_items_details"
                myDs.Tables.Add(mydata)

            End With




            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Purchase_Order_Report_Public_Bidding_By_LOT
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Ref_Number", Ref_Number.Text)
            rptDoc.SetParameterValue("BAC_Reso", BAC_Resolution_Number.Text)
            rptDoc.SetParameterValue("initial", txtInitial.Text)
            rptDoc.SetParameterValue("city_mayor", combo_incharge.Text)
            rptDoc.SetParameterValue("Days", combo_days.Text)
            rptDoc.SetParameterValue("PDays", combo_payment.Text)
            rptDoc.SetParameterValue("Place_of_Delivery", combo_PLACE.Text)
            rptDoc.SetParameterValue("Date_Printed", date_printed.Text)
            rptDoc.SetParameterValue("Note", txtNOTE.Text)
            rptDoc.SetParameterValue("Mode_of_Procurement", combo_MODE.Text)

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
        End If
    End Sub

End Class
