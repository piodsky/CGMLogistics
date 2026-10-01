Public Class Inventory_Reports
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Private Enum status
        BUSY
        LOADING
        DONE
        RESULT
    End Enum
    Private Sub Inventory_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        groupICS.Visible = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_ics.CheckedChanged
        groupICS.Visible = True

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_are.CheckedChanged
        groupICS.Visible = False
        lblCategory.Text = "PAR"
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
        If rad_are.Checked = False And rad_ics.Checked = False Then
            MsgBox("Please Choose ICS or PAR")
        Else

            Dim dateto1 As Date = datefrom.Value
            Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
            Dim dateto2 As Date = dateto.Value
            Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
        

            If lblCategory.Text = "PAR" Then
                With myquery
                    connect(myquery)
                    sendbgWork("SELECT ARE_Number, PO_Number, Units, Description, Unit_Cost, Quantity, Total, Serial_No, Tag_No, Purpose, Remarks, ARE_by, ARE_Location, Supplier_Name, Classification, Date_Acquired, End_User, End_User_Role FROM inv_are_items_List Where Date_Acquired BETWEEN  '" & x.ToString & "' AND '" & y.ToString & "'")
                    'dgvView.DataSource = mydata

                    'dgvView.Refresh()
                End With

            ElseIf lblCategory.Text = "ICS" Then
                With myquery
                    connect(myquery)
                    sendbgWork("SELECT ICS_Number, PO_Number, Units, Description, Unit_Cost, Quantity, Total, Serial_No, Tag_No, Purpose, Remarks, ICS_by, ICS_Location, Supplier_Name, Classification, Date_Acquired, End_User, End_User_Role FROM inv_ics_items_List Where Date_Acquired BETWEEN  '" & x.ToString & "' AND '" & y.ToString & "'")
                    'dgvView.DataSource = mydata

                    'dgvView.Refresh()
                End With

            ElseIf lblCategory.Text = "ICSHighValue" Then
                With myquery
                    connect(myquery)
                    sendbgWork("SELECT ICS_Number, PO_Number, Units, Description, Unit_Cost, Quantity, Total, Serial_No, Tag_No, Purpose, Remarks, ICS_by, ICS_Location, Supplier_Name, Classification, Date_Acquired, End_User, End_User_Role FROM inv_ics_items_List Where Date_Acquired BETWEEN  '" & x.ToString & "' AND '" & y.ToString & "' AND unit_cost >= '5000.00'")
                    'dgvView.DataSource = mydata

                    'dgvView.Refresh()
                End With


            ElseIf lblCategory.Text = "ICSLowValue" Then
                With myquery
                    connect(myquery)
                    sendbgWork("SELECT ICS_Number, PO_Number, Units, Description, Unit_Cost, Quantity, Total, Serial_No, Tag_No, Purpose, Remarks, ICS_by, ICS_Location, Supplier_Name, Classification, Date_Acquired, End_User, End_User_Role FROM inv_ics_items_List Where Date_Acquired BETWEEN  '" & x.ToString & "' AND '" & y.ToString & "' AND unit_cost <= '4999.99'")
                    'dgvView.DataSource = mydata

                    'dgvView.Refresh()
                End With
            End If

        End If


    End Sub

  
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If rad_are.Checked = False And rad_ics.Checked = False Then
            MsgBox("Please Choose ICS or PAR")
        Else

            If lblCategory.Text = "PAR" Then
                With myquery
                    connect(myquery)
                    sendbgWork("SELECT ARE_Number, PO_Number, Units, Description, Unit_Cost, Quantity, Total, Serial_No, Tag_No, Purpose, Remarks, ARE_by, ARE_Location, Supplier_Name, Classification, Date_Acquired, End_User, End_User_Role FROM inv_are_items_List")
                    'dgvView.DataSource = mydata

                    'dgvView.Refresh()
                End With

            ElseIf lblCategory.Text = "ICS" Then
                With myquery
                    connect(myquery)
                    sendbgWork("SELECT ICS_Number, PO_Number, Units, Description, Unit_Cost, Quantity, Total, Serial_No, Tag_No, Purpose, Remarks, ICS_by, ICS_Location, Supplier_Name, Classification, Date_Acquired, End_User, End_User_Role FROM inv_ics_items_List")
                    'dgvView.DataSource = mydata

                    'dgvView.Refresh()
                End With
            End If

        End If


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

    
    Private Sub radViewAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radViewAll.CheckedChanged
        lblCategory.Text = "ICS"
    End Sub

    Private Sub redHighValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles redHighValue.CheckedChanged
        lblCategory.Text = "ICSHighValue"
    End Sub

    Private Sub radLowValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radLowValue.CheckedChanged
        lblCategory.Text = "ICSLowValue"
    End Sub
End Class