Public Class CGSO_Main_Form
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        With CGSO_Inventory
            .inventory_state.Text = "Add"
            .ShowDialog()

            .Focus()
        End With
    End Sub

    Private Sub CGSO_Main_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CGSO_Inventory_Sub_Details.Close()
        CGSO_Inventory.Close()

    End Sub

    Private Sub CGSO_Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        connect(myquery)
        datefrom.Value = (Now.Year & "-" & Now.Month & "-01")
        'dateto.Value = (Now.Year & "-" & Now.Month & "-" & Now.Day + 1)

        ' Get the current date
        Dim currentDate As DateTime = DateTime.Now

        ' Add one day to the current date
        Dim nextDate As DateTime = currentDate.AddDays(1)

        ' Set the value to your control
        dateto.Value = nextDate

        groupadvancesearch.Enabled = False
        form_load_inventory()

        combo_search.Text = "PO_Number"
    End Sub


    Public Sub form_load_inventory()
        dgvInventory.Rows.Clear()

        Dim dateto1 As Date = datefrom.Value
        Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
        Dim dateto2 As Date = dateto.Value
        Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
     
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT Date_Created, PO_Number, PR_Number, Department, Supplier_Name, Purpose FROM tbl_inventory WHERE isDeleted = 0 and date_created BETWEEN '" & x.ToString & "' AND '" & y.ToString & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInventory
                    .Rows.Add()
                    With .Rows(count)
                        dgvInventory.Item(0, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                        dgvInventory.Item(1, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                        dgvInventory.Item(2, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                        dgvInventory.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                        dgvInventory.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                        dgvInventory.Item(5, count).Value = mydata.Rows(count).Item("Purpose").ToString




                    End With


                End With

            Next



        End With

    End Sub

    Private Sub dgvInventory_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInventory.CellDoubleClick
        With CGSO_Inventory
            .inventory_state.Text = "View/Update"
            .ShowDialog()
            .Focus()
        End With
    End Sub

    Private Sub ViewUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewUpdateToolStripMenuItem.Click
        With CGSO_Inventory
            .inventory_state.Text = "View/Update"
            .ShowDialog()
            .Focus()
        End With
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        With CGSO_Inventory
            .inventory_state.Text = "View/Update"
            .ShowDialog()
            .Focus()
        End With
    End Sub

    Private Sub dgvInventory_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvInventory.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvInventory.CurrentCell = dgvInventory(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If chkadvancesearch.Checked = True Then

            dgvInventory.Rows.Clear()

            Dim dateto1 As Date = datefrom.Value
            Dim x = (dateto1.Year & "-" & dateto1.Month & "-" & dateto1.Day)
            Dim dateto2 As Date = dateto.Value
            Dim y = (dateto2.Year & "-" & dateto2.Month & "-" & dateto2.Day)
            Try

                With myquery

                    mydata = .runQuery("SELECT Date_Created, PO_Number, PR_Number, Department, Supplier_Name, Purpose FROM tbl_inventory WHERE isDeleted = 0 and Date_Created BETWEEN '" & x.ToString & "' AND '" & y.ToString & "'")
                    For count As Integer = 0 To mydata.Rows.Count - 1
                        With dgvInventory
                            .Rows.Add()

                            With .Rows(count)

                                dgvInventory.Item(0, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                                dgvInventory.Item(1, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                                dgvInventory.Item(2, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                                dgvInventory.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                                dgvInventory.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                                dgvInventory.Item(5, count).Value = mydata.Rows(count).Item("Purpose").ToString


                            End With
                        End With
                    Next
                End With

            Catch ex As Exception


            End Try

        ElseIf chkadvancesearch.Checked = False Then

            dgvInventory.Rows.Clear()

            With myquery

                mydata = .runQuery("SELECT Date_Created, PO_Number, PR_Number, Department, Supplier_Name, Purpose FROM tbl_inventory WHERE isDeleted = 0 and " & combo_search.Text.ToString & " LIKE '%" & txtSearchBox.Text.ToString & "%'")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvInventory
                        .Rows.Add()
                        With .Rows(count)

                            dgvInventory.Item(0, count).Value = mydata.Rows(count).Item("Date_Created").ToString
                            dgvInventory.Item(1, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                            dgvInventory.Item(2, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                            dgvInventory.Item(3, count).Value = mydata.Rows(count).Item("Department").ToString
                            dgvInventory.Item(4, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                            dgvInventory.Item(5, count).Value = mydata.Rows(count).Item("Purpose").ToString
                        End With

                    End With
                Next

            End With

        Else
            MsgBox("Error K18!")

        End If


    End Sub

    Private Sub chkadvancesearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkadvancesearch.CheckedChanged
        If chkadvancesearch.Checked = True Then
            groupadvancesearch.Enabled = True
        ElseIf chkadvancesearch.Checked = False Then
            groupadvancesearch.Enabled = False
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        form_load_inventory()
    End Sub
End Class