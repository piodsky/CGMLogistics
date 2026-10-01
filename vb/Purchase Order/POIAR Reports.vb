Public Class POIAR_Reports
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim loadingForm As frmLoading ' ← reference to the loading form
    Private Enum status
        BUSY
        LOADING
        DONE
        RESULT
    End Enum
    Private Sub POIAR_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadingForm = New frmLoading()
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim x As String = datefrom.Value.ToString("yyyy-MM-dd")
        Dim y As String = dateto.Value.ToString("yyyy-MM-dd")
        With myquery
            connect(myquery)
            sendbgWork("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Supplier_Name, po.Supplier_Address, cost.Total_Cost, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks, po.Date_PO, po.Mode_of_Procurement, po.Place_of_Delivery, po.Reference_No, po.BAC_Resolution_No, po.PO_Date_Printed, po.Note, po.End_User, i.Date_Inspected, i.Invoice_Number, i.Date_BAC_Received, i.BARCODE_Number, i.remarks, i.Inspected_by_1, i.Inspected_by_2, i.Inspected_by_3, i.Inspected_by_4, i.end_user1, i.end_user2, i.end_user3, i.end_user4, i.end_user5, i.Category AS Inspection_Category, i.Category_Explanation, i.Accepted_by FROM tbl_purchase_order po LEFT JOIN tbl_inspection i ON po.PO_Number = i.PO_Number LEFT JOIN (SELECT PO_Number, SUM(Total_Cost) AS Total_Cost FROM tbl_purchase_order_items_details GROUP BY PO_Number) cost ON po.PO_Number = cost.PO_Number WHERE po.Date_Created BETWEEN  '" & x.ToString & "' AND '" & y.ToString & "' AND po.isDeleted = 0 AND i.isDeleted = 0;")
            'dgvView.DataSource = mydata

            'dgvView.Refresh()
        End With
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

        'Export Data to Excel, Docx'
        Try
            With SaveFileDialog1
                .Filter = ("Documents | *.xls; *.docx; *.doc;")
                .FilterIndex = 1
            End With
            'Clear the file name
            SaveFileDialog1.FileName = "File Name"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.UltraGridExcelExporter1.Export(Me.dgvView, SaveFileDialog1.FileName)
                MsgBox("Successfully Exported!")
                Process.Start(SaveFileDialog1.FileName)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sendbgWork(ByVal query As String)

        Try
            setStatus(status.LOADING)
            loadingForm.Show() ' Show the loading form

            Dim bg As New System.ComponentModel.BackgroundWorker
            AddHandler bg.DoWork, AddressOf bgWork
            AddHandler bg.RunWorkerCompleted, AddressOf bgWork_Complete

            bg.RunWorkerAsync(query)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub bgWork_Complete(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Try
            loadingForm.Hide() ' Hide the loading form

            Dim mydata As New DataTable
            mydata = e.Result
            dgvView.DataSource = Nothing
            dgvView.DataSource = mydata

            setStatus(status.RESULT)
            mydata = Nothing
        Catch ex As Exception
            loadingForm.Hide()
        End Try
    End Sub

    Private Sub bgWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Try
            Dim query As String = e.Argument
            Dim mydata As New DataTable
            Dim myquery As New MyClassLibrary.MyHelper
            connect(myquery)
            mydata = myquery.runQuery(query)
            e.Result = mydata
        Catch ex As Exception
        End Try
    End Sub

    Private Sub setStatus(ByVal stat As status)

        Try

            Select Case stat
                Case status.BUSY
                    Me.lblStatus.Text = "BUSY..."
                Case status.LOADING
                    Me.lblStatus.Text = "LOADING..."
                Case status.DONE
                    Me.lblStatus.Text = "DONE..."
                Case status.RESULT
                    Me.lblStatus.Text = CStr(dgvView.Rows.Count) & " result(s) found."

            End Select

        Catch ex As Exception

        End Try
    End Sub
End Class