Public Class New_Archieve
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Private Enum status
        BUSY
        LOADING
        DONE
        RESULT
    End Enum
    Private Sub New_Archieve_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim dateto1 As Date = datefrom.Value
        Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
        Dim dateto2 As Date = dateto.Value
        Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)


        With myquery
            connect(myquery)
            sendbgWork("SELECT are_no AS ARE_No, description, Qty_Breakdown AS QTY, unit, unit_price, PO_No, Date_Acquired, ARE_by, ARE_Position, location AS Department FROM logistic.tblare WHERE Date_Acquired BETWEEN  '" & x.ToString & "' AND '" & y.ToString & "' ORDER BY PO_NO ASC")
            'dgvView.Refresh()
        End With
    End Sub


    Private Sub sendbgWork(ByVal query As String)

        Try



            setStatus(status.LOADING)
            Dim bg As New System.ComponentModel.BackgroundWorker
            AddHandler bg.DoWork, AddressOf bgWork
            AddHandler bg.RunWorkerCompleted, AddressOf bgWork_Complete

            bg.RunWorkerAsync(query)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub bgWork_Complete(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Try


            Dim mydata As New DataTable

            mydata = e.Result
            dgvView.DataSource = Nothing
            dgvView.DataSource = mydata
            'dgvView.Columns(0).Visible = False
            'dgvView.Columns(34).Visible = False
            setStatus(status.RESULT)
            mydata = Nothing
        Catch ex As Exception

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
            sendbgWork("SELECT are_no AS ARE_No, description, Qty_Breakdown AS QTY, unit, unit_price, PO_No, Date_Acquired, ARE_by, ARE_Position, location AS Department FROM logistic.tblare ORDER BY PO_No ASC")
            'dgvView.DataSource = mydata

            'dgvView.Refresh()
        End With

    End Sub
End Class