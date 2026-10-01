Public Class PO_Main_Form
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata, mydata_total_cost As DataTable
    Dim mydata2 As DataTable
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        With PO_Choices

            .ShowDialog()
            .Focus()
        End With
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        With Add_Purchase_Order
            .PO_Status.Text = "Edit/Update"
            .ShowDialog()
            .Focus()
        End With
    End Sub
    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        If MsgBox("Are you sure you want to DELETE PR_Number = " & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            With myquery
                .setTable("tbl_purchase_order")

                .addValue("1", "isDeleted")

                .addValue(mdiMain.stbUser.Text, "Deleted_By")
                .runUpdate("PO_Number ='" & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
                MsgBox("Successfully Deleted!")
            End With

        End If
        form_load()
    End Sub


    Private Sub PO_Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        connect(myquery)
        datefrom.Value = (Now.Year & "-" & Now.Month & "-01")
        'dateto.Value = (Now.Year & "-" & Now.Month & "-" & Now.Day + 1)

        ' Get the current date
        Dim currentDate As DateTime = DateTime.Now

        ' Add one day to the current date
        Dim nextDate As DateTime = currentDate.AddDays(1)

        ' Set the value to your control
        dateto.Value = nextDate

        combo_search.Text = "PR_Number"



 

        If mdiMain.role.Text = 1 Or mdiMain.role.Text = 2 Or mdiMain.role.Text = 3 Then
            group_admin.Visible = True
            groupadminControl.Visible = True
            btnDel.Enabled = True
            ViewToolStripMenuItem.Enabled = True
        ElseIf mdiMain.role.Text = 7 Or mdiMain.role.Text = 10 Then
            group_admin.Visible = True
            groupadminControl.Visible = True
            btnDel.Enabled = False
            ViewToolStripMenuItem.Enabled = False
            btnNew.Enabled = False
            btnEdit.Enabled = False
            btnPrintObligationRequest.Enabled = False
            group_admin.Visible = False
        Else
            group_admin.Visible = False
            groupadminControl.Visible = False
            btnDel.Enabled = False
            ViewToolStripMenuItem.Enabled = False

        End If
        form_load()


        groupadvancesearch.Enabled = False
    End Sub


    Public Sub form_load()
        dgvPO_LIST.Rows.Clear()

        Dim dateto1 As Date = datefrom.Value
        Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
        Dim dateto2 As Date = dateto.Value
        Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
        With myquery
            mydata = .runQuery("SELECT po.PO_Number, po.PR_Number, po.Barcode_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE date_created BETWEEN '" & x.ToString & "' AND '" & y.ToString & "' AND po.isDeleted = 0 GROUP BY po.PO_Number")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvPO_LIST
                    .Rows.Add()
                    With .Rows(count)

                        dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                        dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                        dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                        dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                        dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                        dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                        dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                        dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                        dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                        dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                        dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                        dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                        dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                        dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                    End With



                End With
            Next
            dgvPO_LIST.Columns(0).DefaultCellStyle.ForeColor = Color.Green
            dgvPO_LIST.Columns(1).DefaultCellStyle.ForeColor = Color.Blue


        End With
    End Sub
 
    Public Sub form_load_items()
        Try
            dgvPO_ITEMLIST.Rows.Clear()

            With myquery

                mydata = .runQuery("SELECT Item_ID, Unit, Items_Description,Quantity, Unit_Cost, Total_Cost FROM tbl_purchase_order_items_details WHERE isDeleted = 0 and PO_Number = '" & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
                mydata_total_cost = .runQuery("SELECT SUM(Total_Cost) as SUM FROM tbl_purchase_order_items_details WHERE isDeleted = 0 and PO_Number = '" & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")

                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_ITEMLIST
                        .Rows.Add()
                        With .Rows(count)
                            dgvPO_ITEMLIST.Item(0, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                            dgvPO_ITEMLIST.Item(1, count).Value = mydata.Rows(count).Item("Unit").ToString
                            dgvPO_ITEMLIST.Item(2, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                            dgvPO_ITEMLIST.Item(3, count).Value = mydata.Rows(count).Item("Quantity").ToString
                            dgvPO_ITEMLIST.Item(4, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                            dgvPO_ITEMLIST.Item(5, count).Value = mydata.Rows(count).Item("Total_Cost").ToString

                            ' txtSubTotal.Text = mydata.Rows(count).Item("SUM").ToString

                        End With


                    End With
                Next





                txtSubTotal.Text = Decimal.Parse(mydata_total_cost.Rows(0).Item("SUM")).ToString("N2")


            End With
        Catch ex As Exception

        End Try
    End Sub

  
    Private Sub dgvPO_LIST_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPO_LIST.CellClick
    

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT Print_PO, Print_Obligation FROM tbl_purchase_order WHERE isDeleted = 0 and PO_Number = '" & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")

            Dim printPOValue As String = mydata.Rows(0).Item("Print_PO").ToString
            Dim printObligationValue As String = mydata.Rows(0).Item("Print_Obligation").ToString

            If printObligationValue = "1" Then
                MsgBox("This Purchase Order is Locked, if you want to Edit this!,. Please Contact your Administrator!")
            End If

            btnEdit.Enabled = (printPOValue = "0")
            ' btnPrintObligationRequest.Enabled = (printObligationValue = "0")
        End With

        form_load_items()
    End Sub

   

    Private Sub btnPrintPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPO.Click
        add_time_po.ShowDialog()
    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If chkadvancesearch.Checked = True Then

            dgvPO_LIST.Rows.Clear()

            Dim dateto1 As Date = datefrom.Value
            Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
            Dim dateto2 As Date = dateto.Value
            Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
            Try

                With myquery

                    mydata = .runQuery("SELECT po.PO_Number, po.PR_Number, po.Barcode_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po FORCE INDEX (Date_Created) WHERE po.isDeleted = 0 and " & combo_search.Text.ToString & " LIKE '%" & txtSearchBox.Text.ToString & "%' and Date_Created BETWEEN '" & x.ToString & "' AND '" & y.ToString & "' GROUP BY po.PO_Number")
                    For count As Integer = 0 To mydata.Rows.Count - 1
                        With dgvPO_LIST
                            .Rows.Add()

                            With .Rows(count)

                                dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                                dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                                dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                                dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                                dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                                dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                                dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                                dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                                dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                                dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                                dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                                dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                                dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                                dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                            End With

                        End With
                    Next
                End With
            Catch ex As Exception


            End Try

        ElseIf chkadvancesearch.Checked = False Then

            dgvPO_LIST.Rows.Clear()

            With myquery

                mydata = .runQuery("SELECT po.PO_Number, po.PR_Number , po.Barcode_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created,po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE po.isDeleted =0 and " & combo_search.Text.ToString & " LIKE '%" & txtSearchBox.Text.ToString & "%'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_LIST
                        .Rows.Add()
                        With .Rows(count)

                            dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                            dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                            dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                            dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                            dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                            dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                            dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                        End With


                    End With
                Next

            End With
        Else
            MsgBox("Error K18!")

        End If



    End Sub

    Private Sub dgvPO_LIST_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPO_LIST.CellDoubleClick
        With Add_Purchase_Order
            .PO_Status.Text = "Edit/Update"
            .ShowDialog()
            .Focus()
        End With
    End Sub

    Private Sub dgvPO_LIST_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvPO_LIST.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvPO_LIST.CurrentCell = dgvPO_LIST(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?
            form_load_items()
        End If
  

    End Sub

    Private Sub btnPrintDisbursement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing
        Dim myDs As New DataSet
        Dim DisbursementVoucher As New rpt_Viewer 'vb viewer


        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Disbursement_Voucher

        With DisbursementVoucher
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
    End Sub

    Private Sub btnPrintObligationRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintObligationRequest.Click
        Obligation_Print.ShowDialog()
    End Sub

    

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchBox.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub ViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        With Add_Purchase_Order
            .PO_Status.Text = "Edit/Update"
            .ShowDialog()
            .Focus()
        End With
    End Sub
    Private Sub AddDateReceivedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDateReceivedToolStripMenuItem.Click
        Generate_Time.ShowDialog()
    End Sub
  
    Private Sub AddCoaDateReceivedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCoaDateReceivedToolStripMenuItem.Click
        Generate_Time_Add_COA.ShowDialog()
    End Sub

    Private Sub AddRemarksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRemarksToolStripMenuItem.Click
        Generate_Remarks.ShowDialog()
    End Sub

    Private Sub AddPRReceivedNameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPRReceivedNameToolStripMenuItem.Click
        Generate_PR_Received_Name.ShowDialog()
    End Sub

    Private Sub AddApprovedPODateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddApprovedPODateToolStripMenuItem.Click
        Generate_Approved_PO_Date.ShowDialog()
    End Sub

    Private Sub dgvPO_LIST_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvPO_LIST.KeyUp, dgvPO_LIST.KeyDown
        form_load_items()
    End Sub


    Private Sub AddCategoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCategoryToolStripMenuItem.Click
        Generate_Category.ShowDialog()
    End Sub
    Private Sub chkadvancesearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkadvancesearch.CheckedChanged
        If chkadvancesearch.Checked = True Then
            groupadvancesearch.Enabled = True
        ElseIf chkadvancesearch.Checked = False Then
            groupadvancesearch.Enabled = False
        Else
            MsgBox("Error K18")

        End If
    End Sub

 
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        form_load()
    End Sub

   
    Private Sub rad_date_received_supplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_date_received_supplier.CheckedChanged
        dgvPO_LIST.Rows.Clear()
        Try

            Dim blank As String = ""
            With myquery

                mydata = .runQuery("SELECT po.PO_Number, po.PR_Number , po.Barcode_Number,  po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE po.isDeleted = 0 and po.Date_Received_Supplier ='" & blank & "'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_LIST
                        .Rows.Add()

                        With .Rows(count)


                            dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                            dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                            dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                            dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                            dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                            dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                            dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                        End With

                    End With
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub


    Private Sub rad_View_ALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_View_ALL.CheckedChanged
        form_load()
    End Sub

    Private Sub rad_Date_Approved_PO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_Date_Approved_PO.CheckedChanged
        dgvPO_LIST.Rows.Clear()
        Try

            Dim blank As String = ""
            With myquery

                mydata = .runQuery("SELECT po.PO_Number, po.PR_Number , po.Barcode_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE po.isDeleted = 0 and po.Date_Approved_PO ='" & blank & "'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_LIST
                        .Rows.Add()

                        With .Rows(count)



                            dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                            dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                            dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                            dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                            dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                            dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                            dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                        End With

                    End With
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rad_Date_COA_Received_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_Date_COA_Received.CheckedChanged
        dgvPO_LIST.Rows.Clear()
        Try

            Dim blank As String = ""
            With myquery

                mydata = .runQuery("SELECT po.PO_Number, po.PR_Number , po.Barcode_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE po.isDeleted = 0 and po.Date_COA_Received ='" & blank & "'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_LIST
                        .Rows.Add()

                        With .Rows(count)



                            dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                            dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                            dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                            dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                            dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                            dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                            dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                        End With

                    End With
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub


    Private Sub rad_Blank_All_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_Blank_All.CheckedChanged
        dgvPO_LIST.Rows.Clear()
        Try

            Dim blank As String = ""
            With myquery

                mydata = .runQuery("SELECT po.PO_Number, po.PR_Number , po.Barcode_Number po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE po.isDeleted = 0 and po.Date_Received_Supplier ='" & blank & "' and po.Date_COA_Received ='" & blank & "' and po.Date_Approved_PO ='" & blank & "'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_LIST
                        .Rows.Add()

                        With .Rows(count)



                            dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                            dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                            dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                            dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                            dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                            dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                            dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString
                        End With

                    End With
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rad_or_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_or.CheckedChanged
        dgvPO_LIST.Rows.Clear()
        Try

            Dim blank As String = ""
            With myquery

                mydata = .runQuery("SELECT po.PO_Number, po.PR_Number, po.Barcode_Number , po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Category, po.Date_Created, po.PR_Received_Name, po.Date_Received_Supplier, po.Date_COA_Received, po.Date_Approved_PO, po.Remarks FROM tbl_purchase_order po WHERE po.isDeleted = 0 and po.Date_Received_Supplier ='" & blank & "' or po.Date_COA_Received ='" & blank & "' or po.Date_Approved_PO ='" & blank & "'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvPO_LIST
                        .Rows.Add()

                        With .Rows(count)


                            dgvPO_LIST.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvPO_LIST.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvPO_LIST.Item(2, count).Value = mydata.Rows(count).Item("Barcode_Number").ToString
                            dgvPO_LIST.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvPO_LIST.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvPO_LIST.Item(5, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvPO_LIST.Item(6, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvPO_LIST.Item(7, count).Value = mydata.Rows(count).Item("Category").ToString
                            dgvPO_LIST.Item(8, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvPO_LIST.Item(9, count).Value = mydata.Rows(count).Item("PR_Received_Name").ToString
                            dgvPO_LIST.Item(10, count).Value = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                            dgvPO_LIST.Item(11, count).Value = mydata.Rows(count).Item("Date_COA_Received").ToString
                            dgvPO_LIST.Item(12, count).Value = mydata.Rows(count).Item("Date_Approved_PO").ToString
                            dgvPO_LIST.Item(13, count).Value = mydata.Rows(count).Item("Remarks").ToString

                        End With

                    End With
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLOCK.Click
        If MsgBox("Are you sure you want to LOCKED this Purchase Order = " & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            With myquery
                .setTable("tbl_purchase_order")

                .addValue("1", "Print_PO")
                .addValue("1", "Print_Obligation")

                .runUpdate("PO_Number ='" & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
                MsgBox("Purchase Order LOCKED!")
            End With

        End If
        form_load()
    End Sub

    Private Sub btnUNLOCK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUNLOCK.Click
        If MsgBox("Are you sure you want to LOCKED this Purchase Order = " & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            With myquery
                .setTable("tbl_purchase_order")

                .addValue("0", "Print_PO")
                .addValue("0", "Print_Obligation")

                .runUpdate("PO_Number ='" & dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
                MsgBox("Purchase Order UNLOCKED!")
            End With

        End If
        form_load()
    End Sub
End Class