Public Class PO_Reports

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
    Private Sub PO_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadingForm = New frmLoading()
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim x As String = datefrom.Value.ToString("yyyy-MM-dd")
        Dim y As String = dateto.Value.ToString("yyyy-MM-dd")


        With myquery
            connect(myquery)
            sendbgWork("SELECT po.PO_Number, po.PR_Number, po.Barcode_Number, po.Department, po.Division, po.Supplier_Name, po.Supplier_Address, pod.Total_Cost, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks, po.Date_PO, po.Mode_of_Procurement, po.Place_of_Delivery, po.Reference_No, po.BAC_Resolution_No, po.PO_Date_Printed, po.Note, po.End_User FROM tbl_purchase_order po JOIN (SELECT PO_Number, SUM(Total_Cost) AS Total_Cost FROM tbl_purchase_order_items_details WHERE isDeleted = 0 GROUP BY PO_Number) pod ON po.PO_Number = pod.PO_Number WHERE po.Date_Created BETWEEN '" & x & "' AND '" & y & "' AND po.isDeleted = 0")
            'dgvView.DataSource = mydata

            'dgvView.Refresh()
        End With





    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            With SaveFileDialog1
                .Filter = ("Documents | *.xls; *.docx; *.doc;")
                .FilterIndex = 1
            End With
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


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click


        With myquery
            connect(myquery)
            sendbgWork("SELECT po.PO_Number, po.PR_Number, po.Department, po.Division, po.Supplier_Name, po.Supplier_Address, SUM(Total_Cost) AS Total_Cost, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks, po.Date_PO, po.Mode_of_Procurement, po.Place_of_Delivery, po.Reference_No, po.BAC_Resolution_No, po.PO_Date_Printed, po.Note, po.End_User FROM tbl_purchase_order_items_details poid  INNER JOIN tbl_purchase_order po ON poid.`PO_Number` = po.`PO_Number` WHERE po.isDeleted = 0 AND poid.`isDeleted` = 0 GROUP BY PO_Number")
            'dgvView.DataSource = mydata

            'dgvView.Refresh()
        End With

    End Sub
End Class