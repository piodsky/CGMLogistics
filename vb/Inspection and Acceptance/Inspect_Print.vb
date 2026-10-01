
Public Class Inspect_Print
    Dim id As Integer = 0

    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Private Sub Inspect_Print_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        combo_inspect1.Text = Inspect_Form.combo_inspect1.Text
        combo_inspect2.Text = Inspect_Form.combo_inspect2.Text
        combo_inspect3.Text = Inspect_Form.combo_inspect3.Text
        combo_inspect4.Text = Inspect_Form.combo_inspect4.Text
        combo_office()
        btnSave.Focus()

        Inspect_Status.Text = Inspect_Form.Inspect_Status.Text

    End Sub
   

    Public Sub combo_office()
        Dim office1, office2, office3, office4 As DataTable

        Dim inspectorate1 As String = combo_inspect1.Text
        Dim inspectorate2 As String = combo_inspect2.Text
        Dim inspectorate3 As String = combo_inspect3.Text
        Dim inspectorate4 As String = combo_inspect4.Text

        With myquery
            connect(myquery)

            office1 = .runQuery("Select * from ref_signaturies WHERE Signaturies = '" & inspectorate1 & "'")
            office2 = .runQuery("Select * from ref_signaturies WHERE Signaturies = '" & inspectorate2 & "'")
            office3 = .runQuery("Select * from ref_signaturies WHERE Signaturies = '" & inspectorate3 & "'")
            office4 = .runQuery("Select * from ref_signaturies WHERE Signaturies = '" & inspectorate4 & "'")
        End With

        For Each mydatarow In office1.Rows
            With mydatarow
                combo_inspect_office1.Text = mydatarow.Item("Department")

            End With

        Next
        For Each mydatarow In office2.Rows
            With mydatarow
                combo_inspect_office2.Text = mydatarow.Item("Department")

            End With
        Next
        For Each mydatarow In office3.Rows
            With mydatarow
                combo_inspect_office3.Text = mydatarow.Item("Department")

            End With
        Next
        For Each mydatarow In office4.Rows
            With mydatarow
                combo_inspect_office4.Text = mydatarow.Item("Department")

            End With
        Next

    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing
        Dim myDs As New DataSet
        Dim InspectionGeneratorReport As New rpt_Viewer 'vb viewer



        rpTitle = "Inspection and Acceptance Report"

        If Inspect_Status.Text = "View" Then
            With myquery
                mydata = myquery.runQuery("SELECT Item_Transit_Receipt_Number, Invoice_Number, PO_Number, PR_Number, Department, Supplier_Name, Supplier_Address, End_User1, End_User2, End_User3, End_User4, End_User5, Inspected_by_1, Inspected_by_2, Inspected_by_3, Inspected_by_4, Accepted_by, Purpose FROM tbl_inspection WHERE PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' and isDeleted = 0")
                mydata.TableName = "tbl_inspection"
                myDs.Tables.Add(mydata)

            End With

            With myquery
                mydata = myquery.runQuery("SELECT Item_Transit_Receipt_Number, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost FROM tbl_inspection_items_details WHERE PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' and isDeleted = 0")
                mydata.TableName = "tbl_inspection_items_details"
                myDs.Tables.Add(mydata)

            End With


            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inspection_and_Acceptance_Report
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("Inspectorate1", combo_inspect1.Text)
            rptDoc.SetParameterValue("Inspectorate2", combo_inspect2.Text)
            rptDoc.SetParameterValue("Inspectorate3", combo_inspect3.Text)
            rptDoc.SetParameterValue("Inspectorate4", combo_inspect4.Text)

            rptDoc.SetParameterValue("Department1", combo_inspect_office1.Text)
            rptDoc.SetParameterValue("Department2", combo_inspect_office2.Text)
            rptDoc.SetParameterValue("Department3", combo_inspect_office3.Text)
            rptDoc.SetParameterValue("Department4", combo_inspect_office4.Text)

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

        ElseIf Inspect_Status.Text = "For Progress Billing" Then

            With myquery
                mydata = myquery.runQuery("SELECT Item_Transit_Receipt_Number, PO_Number, PR_Number, Department, Supplier_Name, Supplier_Address, End_User1, End_User2, End_User3, End_User4, End_User5, Inspected_by_1, Inspected_by_2, Inspected_by_3, Inspected_by_4, Delivery_Date, Accepted_by, Purpose FROM tbl_inspection_progress_billing WHERE PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' and Progress_Billing = '" & Inspect_Form.combo_progressbill.Text & "' and isDeleted = 0")
                mydata.TableName = "tbl_inspection"
                myDs.Tables.Add(mydata)

            End With

            With myquery
                mydata = myquery.runQuery("SELECT Item_Transit_Receipt_Number, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost FROM tbl_inspection_items_details_progress_billing WHERE PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' and Progress_Billing = '" & Inspect_Form.combo_progressbill.Text & "' and isDeleted = 0")
                mydata.TableName = "tbl_inspection_items_details"
                myDs.Tables.Add(mydata)

            End With

            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inspection_and_Acceptance_Progress_Billing_Report
            rptDoc.SetDataSource(myDs)


            'para ni sa txt box same parameter name
            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("Inspectorate1", combo_inspect1.Text)
            rptDoc.SetParameterValue("Inspectorate2", combo_inspect2.Text)
            rptDoc.SetParameterValue("Inspectorate3", combo_inspect3.Text)
            'rptDoc.SetParameterValue("Inspectorate4", combo_inspect4.Text)

            rptDoc.SetParameterValue("Department1", combo_inspect_office1.Text)
            rptDoc.SetParameterValue("Department2", combo_inspect_office2.Text)
            rptDoc.SetParameterValue("Department3", combo_inspect_office3.Text)
            'rptDoc.SetParameterValue("Department4", combo_inspect_office4.Text)

            rptDoc.SetParameterValue("DateFromBilling", Inspect_Form.lblDateFrom.Text)
            rptDoc.SetParameterValue("DateToBilling", Inspect_Form.lblDateTo.Text)

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
        End If


     



    End Sub

End Class