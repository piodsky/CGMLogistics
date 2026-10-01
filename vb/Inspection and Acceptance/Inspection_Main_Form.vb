Public Class Inspection_Main_Form

    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Dim po_id As String

    Private Sub Inspection_Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combo_search.Text = "PO_Number"
        datefrom.Value = (Now.Year & "-" & Now.Month & "-01")
        dateto.Value = (Now.Year & "-" & Now.Month & "-" & Now.Day)
        form_load()
        groupadvancesearch.Enabled = False
    End Sub

    Public Sub form_load()
        dgvInspect.Rows.Clear()



        Dim dateto1 As Date = datefrom.Value
        Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
        Dim dateto2 As Date = dateto.Value
        Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
        Dim space As String = ""

        With myquery
            connect(myquery)
            mydata = .runQuery("Select po.PO_Number, po.PR_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Date_Created, po.Inspect from tbl_purchase_order po Where date_created BETWEEN '" & x.ToString & "' AND '" & y.ToString & "' and po.isDeleted = 0 and po.PO_Number <> '" & space & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInspect
                    .Rows.Add()
                    With .Rows(count)

                        dgvInspect.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                        dgvInspect.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                        dgvInspect.Item(2, count).Value = mydata.Rows(count).Item("Department").ToString
                        dgvInspect.Item(3, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                        dgvInspect.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                        dgvInspect.Item(5, count).Value = mydata.Rows(count).Item("Purpose").ToString
                        dgvInspect.Item(6, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                        dgvInspect.Item(7, count).Value = mydata.Rows(count).Item("Inspect").ToString

                        If dgvInspect.Rows(count).Cells(7).Value = "NO" Then

                            dgvInspect.Rows(count).Cells(8).Value = False
                        Else
                            dgvInspect.Rows(count).Cells(8).Value = True
                        End If


                    End With


                End With
            Next
            dgvInspect.Columns(0).DefaultCellStyle.ForeColor = Color.Green
            dgvInspect.Columns(1).DefaultCellStyle.ForeColor = Color.Blue
        End With
    End Sub

    Public Sub form_load_items()
        dgvPO_ITEMLIST.Rows.Clear()

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT Item_ID, Unit, Items_Description,Quantity, Unit_Cost, Total_Cost FROM tbl_purchase_order_items_details WHERE isDeleted = 0 and PO_Number = '" & dgvInspect.SelectedRows(0).Cells(0).Value & "'")
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



                    End With


                End With
            Next

        End With
    End Sub
    Private Sub dgvInspect_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInspect.CellClick
        check()
        form_load_items()
    End Sub

    Private Sub dgvInspect_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvInspect.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvInspect.CurrentCell = dgvInspect(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?
            check()
        End If
    End Sub

    Public Sub check()
        Dim myquery As New MyClassLibrary.MyHelper
        Dim mydata As DataTable

        connect(myquery)

        mydata = myquery.runQuery("Select PO_Number from tbl_inspection where PO_Number = '" & dgvInspect.SelectedRows.Item(0).Cells(0).Value & "'")
        If mydata.Rows.Count Then
            For Each mydatarow In mydata.Rows

                po_id = mydatarow.Item("PO_Number")

                'End If
            Next
        End If


        If po_id = dgvInspect.SelectedRows.Item(0).Cells(0).Value Then
            ViewInspectedItemsToolStripMenuItem.Enabled = True
            InspectToolStripMenuItem.Enabled = False
            ForProgressBillingToolStripMenuItem.Enabled = True

        Else
            ViewInspectedItemsToolStripMenuItem.Enabled = False
            InspectToolStripMenuItem.Enabled = True
            ForProgressBillingToolStripMenuItem.Enabled = False

        End If
    End Sub
    Private Sub InspectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InspectToolStripMenuItem.Click
        With Inspect_Form
            .Inspect_Status.Text = "Inspect"
            .ShowDialog()
            .Focus()
        End With
    End Sub
    Private Sub ViewInspectedItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewInspectedItemsToolStripMenuItem.Click
        With Inspect_Form
            .Inspect_Status.Text = "View"
            .ShowDialog()
            .Focus()
        End With
    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click



        If chkadvancesearch.Checked = True Then




            dgvInspect.Rows.Clear()
            dgvPO_ITEMLIST.Rows.Clear()
            Dim space As String = ""

            Dim dateto1 As Date = datefrom.Value
            Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
            Dim dateto2 As Date = dateto.Value
            Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
            Try
                With myquery
                    connect(myquery)
                    mydata = .runQuery("Select po.PO_Number, po.PR_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Date_Created, po.Inspect from tbl_purchase_order po where po.isDeleted = 0 and po.PO_Number <> '" & space & "' and Date_Created BETWEEN '" & x.ToString & "' AND '" & y.ToString & "'")
                    For count As Integer = 0 To mydata.Rows.Count - 1
                        With dgvInspect
                            .Rows.Add()
                            With .Rows(count)

                                dgvInspect.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                                dgvInspect.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                                dgvInspect.Item(2, count).Value = mydata.Rows(count).Item("Department").ToString
                                dgvInspect.Item(3, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                                dgvInspect.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                                dgvInspect.Item(5, count).Value = mydata.Rows(count).Item("Purpose").ToString
                                dgvInspect.Item(6, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                                dgvInspect.Item(7, count).Value = mydata.Rows(count).Item("Inspect").ToString

                                If dgvInspect.Rows(count).Cells(7).Value = "NO" Then

                                    dgvInspect.Rows(count).Cells(8).Value = False
                                Else
                                    dgvInspect.Rows(count).Cells(8).Value = True
                                End If


                            End With


                        End With
                    Next
                    dgvInspect.Columns(0).DefaultCellStyle.ForeColor = Color.Green
                    dgvInspect.Columns(1).DefaultCellStyle.ForeColor = Color.Blue
                End With
            Catch ex As Exception

            End Try


        ElseIf chkadvancesearch.Checked = False Then


            dgvInspect.Rows.Clear()
            dgvPO_ITEMLIST.Rows.Clear()
            Dim space As String = ""
            With myquery
                connect(myquery)
                mydata = .runQuery("Select po.PO_Number, po.PR_Number, po.Department, po.Supplier_Name, po.Supplier_Address, po.Purpose, po.Date_Created, po.Inspect from tbl_purchase_order po Where po.isDeleted = 0 and po.PO_Number <> '" & space & "' and " & combo_search.Text.ToString & " LIKE '%" & txtSearchBox.Text.ToString & "%'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvInspect
                        .Rows.Add()
                        With .Rows(count)

                            dgvInspect.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvInspect.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvInspect.Item(2, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvInspect.Item(3, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvInspect.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Address").ToString
                            dgvInspect.Item(5, count).Value = mydata.Rows(count).Item("Purpose").ToString
                            dgvInspect.Item(6, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvInspect.Item(7, count).Value = mydata.Rows(count).Item("Inspect").ToString

                            If dgvInspect.Rows(count).Cells(7).Value = "NO" Then

                                dgvInspect.Rows(count).Cells(8).Value = False
                            Else
                                dgvInspect.Rows(count).Cells(8).Value = True
                            End If


                        End With


                    End With
                Next
                dgvInspect.Columns(0).DefaultCellStyle.ForeColor = Color.Green
                dgvInspect.Columns(1).DefaultCellStyle.ForeColor = Color.Blue
            End With


        Else
            MsgBox("ERROR K18!")
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchBox.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub



    Private Sub ForProgressBillingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForProgressBillingToolStripMenuItem.Click
        With Inspect_Form
            .Inspect_Status.Text = "For Progress Billing"
            .ShowDialog()
            .Focus()
        End With
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        form_load()
    End Sub

    Private Sub dgvInspect_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvInspect.KeyDown, dgvInspect.KeyUp
        check()
        form_load_items()
    End Sub

    Private Sub chkadvancesearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkadvancesearch.CheckedChanged
        If chkadvancesearch.Checked = True Then
            groupadvancesearch.Enabled = True
        ElseIf chkadvancesearch.Checked = False Then
            groupadvancesearch.Enabled = False
        Else
            MsgBox("ERROR K18!")
        End If
    End Sub
End Class