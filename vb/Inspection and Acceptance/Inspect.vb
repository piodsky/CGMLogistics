Public Class Inspect_Form
    Dim id As Integer = 0

    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Private Sub Inspect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblDateFrom.Text = ""
        lblDateTo.Text = ""
        ' txtInvoiceNo.Focus()


        datefrom.Format = DateTimePickerFormat.Custom
        datefrom.CustomFormat = "yyyy-MM-dd"

        dateto.Format = DateTimePickerFormat.Custom
        dateto.CustomFormat = "yyyy-MM-dd"


        ' Set the format of bac_received_date DateTimePicker control
        date_bac_received.Format = DateTimePickerFormat.Custom
        date_bac_received.CustomFormat = "yyyy-MM-dd HH:mm:ss"

        ' Set bac_received_date to the current date and time
        date_bac_received.Value = DateTime.Now


        lblBilled.Text = ""
        lblDateFrom.Text = datefrom.Value.ToString("MMMM dd, yyyy")
        lblDateTo.Text = dateto.Value.ToString("MMMM dd, yyyy")

        Me.received1.Text = ""
        Me.received2.Text = ""
        Me.received3.Text = ""
        Me.received4.Text = ""
        Me.received5.Text = ""
        Me.combo_inspect1.Text = ""
        Me.combo_inspect2.Text = ""
        Me.combo_inspect3.Text = ""
        Me.combo_inspect4.Text = ""

        Me.txtInvoiceNo.Text = ""
        Me.txtBarcode.Text = ""
        Me.txtRemarks.Text = ""

        'Me.received1.ResetText()
        'Me.received2.ResetText()
        'Me.received3.ResetText()
        'Me.received4.ResetText()
        'Me.received5.ResetText()
        'Me.combo_inspect1.ResetText()
        'Me.combo_inspect2.ResetText()
        'Me.combo_inspect3.ResetText()

        Me.PO_Number.Text = Inspection_Main_Form.dgvInspect.SelectedRows(0).Cells(0).Value

        If Inspect_Status.Text = "Inspect" Then


            Me.received1.Text = mdiMain.stbUser.Text


            form_load_items()

            btnSave.Enabled = True
            btnEdit.Visible = False
            lblEdit.Visible = False
            lblsave.Text = "Save"
            btnPrintInspection.Enabled = False
            btnPrintDisbursement.Enabled = False

            panelProgressbilling.Visible = False
            lblProgressBilling.Visible = False

        ElseIf Inspect_Status.Text = "View" Then

            exist_form_load_items()

            lblsave.Text = "Save"
            btnEdit.Visible = True
            lblEdit.Visible = True
            btnSave.Enabled = False

            btnPrintInspection.Enabled = True
            btnPrintDisbursement.Enabled = True


            panelProgressbilling.Visible = False
            lblProgressBilling.Visible = False

        ElseIf Inspect_Status.Text = "For Progress Billing" Then


            exist_form_load_items()

            lblsave.Text = "Save"
            btnEdit.Visible = False
            lblEdit.Visible = False

            btnPrintInspection.Enabled = True
            btnPrintDisbursement.Enabled = True

            panelProgressbilling.Visible = True
            lblProgressBilling.Visible = True


        ElseIf Inspect_Status.Text = "Edit/Update" Then


            exist_form_load_items()

            btnPrintInspection.Enabled = True
            btnPrintDisbursement.Enabled = True

            panelProgressbilling.Visible = True
            lblProgressBilling.Visible = True


        End If
    End Sub



    Public Sub save_click()
        Try



            If String.IsNullOrEmpty(combo_inspect1.Text) And String.IsNullOrEmpty(combo_inspect2.Text) And String.IsNullOrEmpty(combo_inspect3.Text) Then
                MsgBox("Please Add atleast one Inspection Officer!")
            Else
                With myquery
                    connect(myquery)
                    .setTable("tbl_inspection")
                    .addValue("1", "isDeleted")
                    .addValue(mdiMain.stbUser.Text, "Edited_by")
                    .runUpdate("PO_Number = '" & PO_Number.Text & "'")
                End With

                With myquery
                    connect(myquery)
                    .setTable("tbl_inspection_items_details")
                    .addValue("1", "isDeleted")
                    .runUpdate("PO_Number = '" & PO_Number.Text & "'")
                End With
                For i As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count - 1

                    With myquery
                        connect(myquery)
                        .setTable("tbl_inspection_items_details")

                        If dgvInspect_ITEMLIST.Item(0, i).Value <> Nothing Then
                            .addValue(dgvInspect_ITEMLIST.Item(0, i).Value, "Item_ID")
                        End If

                        If dgvInspect_ITEMLIST.Item(1, i).Value <> Nothing Then
                            .addValue(dgvInspect_ITEMLIST.Item(1, i).Value, "Unit")
                        End If
                        If dgvInspect_ITEMLIST.Item(2, i).Value <> Nothing Then
                            .addValue(dgvInspect_ITEMLIST.Item(2, i).Value, "Quantity")

                        End If
                        If dgvInspect_ITEMLIST.Item(3, i).Value <> Nothing Then
                            .addValue(dgvInspect_ITEMLIST.Item(3, i).Value, "Items_Description")
                        End If
                        If dgvInspect_ITEMLIST.Item(4, i).Value <> Nothing Then
                            .addValue(dgvInspect_ITEMLIST.Item(4, i).Value, "Unit_Cost")
                        End If
                        If dgvInspect_ITEMLIST.Item(5, i).Value <> Nothing Then
                            .addValue(dgvInspect_ITEMLIST.Item(5, i).Value, "Total_Cost")
                        End If


                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value.ToString, "PO_number")
                        .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Inspected")

                        .runInsert()


                    End With
                Next

                With myquery
                    connect(myquery)
                    .setTable("tbl_inspection")
                    .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value.ToString, "PO_number")
                    .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(1).Value.ToString, "PR_number")
                    .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(2).Value.ToString, "Department")
                    .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(3).Value.ToString, "Supplier_Name")
                    .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(4).Value.ToString, "Supplier_Address")
                    .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(5).Value.ToString, "Purpose")
                    .addValue(combo_inspect1.Text.ToString, "inspected_by_1")
                    .addValue(combo_inspect2.Text.ToString, "inspected_by_2")
                    .addValue(combo_inspect3.Text.ToString, "inspected_by_3")
                    .addValue(combo_inspect4.Text.ToString, "inspected_by_4")

                    .addValue(received1.Text.ToString, "End_User1")
                    .addValue(received2.Text.ToString, "End_User2")
                    .addValue(received3.Text.ToString, "End_User3")
                    .addValue(received4.Text.ToString, "End_User4")
                    .addValue(received5.Text.ToString, "End_User5")
                    .addValue(accept.Text.ToString, "Accepted_by")
                    .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Inspected")

                    .addValue(txtInvoiceNo.Text.ToString, "Invoice_Number")
                    .addValue(txtBarcode.Text.ToString, "BARCODE_Number")
                    .addValue(date_bac_received.Text.ToString, "Date_BAC_Received")
                    .addValue(txtRemarks.Text.ToString, "Remarks")
                    .runInsert()


                End With





                With myquery
                    connect(myquery)
                    .setTable("tbl_purchase_order")
                    .addValue(txtInvoiceNo.Text.ToString, "item_transit_receipt_number")
                    .addValue("YES", "Inspect")
                    .runUpdate("PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows(0).Cells(0).Value & "' and isDeleted =0")


                End With



                'insert()
                combo_inspect1.ResetText()
                combo_inspect2.ResetText()
                combo_inspect3.ResetText()
                combo_inspect4.ResetText()

                received1.ResetText()
                received2.ResetText()
                received3.ResetText()
                received4.ResetText()
                received5.ResetText()


                txtInvoiceNo.ResetText()
                MsgBox("Succesfully saved!")
                Inspection_Main_Form.form_load()
                Me.Close()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub save_click_edit()
        Try
            With myquery
                connect(myquery)
                .setTable("tbl_inspection")
                .addValue("1", "isDeleted")
                .addValue(mdiMain.stbUser.Text, "Edited_by")
                .runUpdate("PO_Number = '" & PO_Number.Text & "'")
            End With

            With myquery
                connect(myquery)
                .setTable("tbl_inspection_items_details")
                .addValue("1", "isDeleted")
                .runUpdate("PO_Number = '" & PO_Number.Text & "'")
            End With

            If String.IsNullOrEmpty(txtInvoiceNo.Text) Then
                MsgBox("Please Add receipt Number!")
                txtInvoiceNo.Focus()
            Else

                If String.IsNullOrEmpty(combo_inspect1.Text) And String.IsNullOrEmpty(combo_inspect2.Text) Then
                    MsgBox("Please Add atleast one Inspection Officer!")
                Else
                    For i As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count - 1

                        With myquery
                            connect(myquery)
                            .setTable("tbl_inspection_items_details")

                            If dgvInspect_ITEMLIST.Item(0, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(0, i).Value, "Item_ID")
                            End If

                            If dgvInspect_ITEMLIST.Item(1, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(1, i).Value, "Unit")
                            End If
                            If dgvInspect_ITEMLIST.Item(2, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(2, i).Value, "Quantity")

                            End If
                            If dgvInspect_ITEMLIST.Item(3, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(3, i).Value, "Items_Description")
                            End If
                            If dgvInspect_ITEMLIST.Item(4, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(4, i).Value, "Unit_Cost")
                            End If
                            If dgvInspect_ITEMLIST.Item(5, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(5, i).Value, "Total_Cost")
                            End If

                            .addValue(txtInvoiceNo.Text.ToString, "item_transit_receipt_number")
                            .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value.ToString, "PO_number")
                            .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Inspected")

                            .runInsert()


                        End With
                    Next

                    With myquery
                        connect(myquery)
                        .setTable("tbl_inspection")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value.ToString, "PO_number")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(1).Value.ToString, "PR_number")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(2).Value.ToString, "Department")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(3).Value.ToString, "Supplier_Name")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(4).Value.ToString, "Supplier_Address")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(5).Value.ToString, "Purpose")

                        .addValue(combo_inspect1.Text.ToString, "inspected_by_1")
                        .addValue(combo_inspect2.Text.ToString, "inspected_by_2")
                        .addValue(combo_inspect3.Text.ToString, "inspected_by_3")
                        .addValue(combo_inspect4.Text.ToString, "inspected_by_4")

                        .addValue(received1.Text.ToString, "End_User1")
                        .addValue(received2.Text.ToString, "End_User2")
                        .addValue(received3.Text.ToString, "End_User3")
                        .addValue(received4.Text.ToString, "End_User4")
                        .addValue(received5.Text.ToString, "End_User5")
                        .addValue(accept.Text.ToString, "Accepted_by")
                        .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Inspected")


                        .addValue(txtInvoiceNo.Text.ToString, "Invoice_Number")
                        .addValue(txtBarcode.Text.ToString, "BARCODE_Number")
                        .addValue(date_bac_received.Text.ToString, "Date_BAC_Received")
                        .addValue(txtRemarks.Text.ToString, "Remarks")
                        .runInsert()


                    End With


                    With myquery
                        connect(myquery)
                        .setTable("tbl_purchase_order_items_details")
                        .addValue(txtInvoiceNo.Text.ToString, "item_transit_receipt_number")
                        .addValue("YES", "Inspect")
                        .runUpdate("PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows(0).Cells(0).Value & "' and isDeleted =0")


                    End With


                    With myquery
                        connect(myquery)
                        .setTable("tbl_purchase_order")
                        .addValue("YES", "Inspect")
                        .runUpdate("PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows(0).Cells(0).Value & "' and isDeleted =0")


                    End With



                    'insert()
                    combo_inspect1.ResetText()
                    combo_inspect2.ResetText()
                    combo_inspect3.ResetText()
                    combo_inspect4.ResetText()
                    received1.ResetText()
                    received2.ResetText()
                    received3.ResetText()
                    received4.ResetText()
                    received5.ResetText()


                    txtInvoiceNo.ResetText()
                    MsgBox("Succesfully Edited!")
                    Inspection_Main_Form.form_load()
                    Me.Close()

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    'Public Sub check_ITR()
    '    Dim myquery As New MyClassLibrary.MyHelper
    '    Dim mydata1 As DataTable
    '    Dim id As Integer = 0

    '    Dim a As Integer



    '    With myquery
    '        connect(myquery)
    '        mydata1 = .runQuery("SELECT item_transit_receipt_number FROM tbl_inspection WHERE Item_Transit_Receipt_Number = '" & txtreceipt.Text.ToString & "'")
    '    End With
    '    For Each mydatarow In mydata1.Rows
    '        With mydatarow
    '            a = CStr(.item("Item_Transit_Receipt_Number"))
    '        End With
    '    Next
    '    If a.ToString = txtreceipt.Text.ToString Then
    '        MsgBox("Item Transit Receipt Number Already Exist")
    '        txtreceipt.Focus()
    '    Else
    '        save_click()
    '    End If

    '    Exit Sub
    'End Sub


    Public Sub exist_form_load_items()
        dgvInspect_ITEMLIST.Rows.Clear()

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT ti.Item_ID, ti.Unit, ti.Quantity, ti.Items_Description, ti.Unit_Cost, ti.Total_Cost, tin.`Invoice_Number`, tin.`BARCODE_Number`, tin.`Date_BAC_Received`, tin.`Remarks`,  tin.`PO_Number`, tin.`End_User1`, tin.`End_User2`, tin.`End_User3`, tin.`End_User4`, tin.`End_User5`, tin.`Inspected_by_1`, tin.`Inspected_by_2`, tin.`Inspected_by_3`, tin.`Inspected_by_4`, tin.`Accepted_by` FROM tbl_inspection_items_details ti INNER JOIN tbl_inspection tin ON tin.`PO_Number` = ti.`PO_Number` WHERE tin.isDeleted = 0 AND ti.isDeleted = 0 AND tin.PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInspect_ITEMLIST
                    .Rows.Add()
                    With .Rows(count)
                        dgvInspect_ITEMLIST.Item(0, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        dgvInspect_ITEMLIST.Item(1, count).Value = mydata.Rows(count).Item("Unit").ToString
                        dgvInspect_ITEMLIST.Item(2, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        dgvInspect_ITEMLIST.Item(3, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        dgvInspect_ITEMLIST.Item(4, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        dgvInspect_ITEMLIST.Item(5, count).Value = mydata.Rows(count).Item("Total_Cost").ToString



                        ' txtInvoiceNo.Text = mydata.Rows(count).Item("Item_Transit_Receipt_Number").ToString
                        txtInvoiceNo.Text = mydata.Rows(count).Item("Invoice_Number").ToString
                        txtBarcode.Text = mydata.Rows(count).Item("BARCODE_Number").ToString
                        date_bac_received.Text = mydata.Rows(count).Item("Date_BAC_Received").ToString
                        txtRemarks.Text = mydata.Rows(count).Item("Remarks").ToString



                        received1.Text = mydata.Rows(count).Item("End_User1").ToString
                        received2.Text = mydata.Rows(count).Item("End_User2").ToString
                        received3.Text = mydata.Rows(count).Item("End_User3").ToString
                        received4.Text = mydata.Rows(count).Item("End_User4").ToString
                        received5.Text = mydata.Rows(count).Item("End_User5").ToString


                        combo_inspect1.Text = mydata.Rows(count).Item("Inspected_by_1").ToString
                        combo_inspect2.Text = mydata.Rows(count).Item("Inspected_by_2").ToString
                        combo_inspect3.Text = mydata.Rows(count).Item("Inspected_by_3").ToString
                        combo_inspect4.Text = mydata.Rows(count).Item("Inspected_by_4").ToString

                        accept.Text = mydata.Rows(count).Item("Accepted_by").ToString
                    End With


                End With
            Next

        End With
        For x As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count
            txtTotalItems.Text = x.ToString
        Next
        sumx()

    End Sub
    Public Sub form_load_items()
        dgvInspect_ITEMLIST.Rows.Clear()

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT Item_ID, Unit, Quantity, Items_Description, Unit_Cost, Total_Cost FROM tbl_purchase_order_items_details WHERE isDeleted = 0 and PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInspect_ITEMLIST
                    .Rows.Add()
                    With .Rows(count)
                        dgvInspect_ITEMLIST.Item(0, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        dgvInspect_ITEMLIST.Item(1, count).Value = mydata.Rows(count).Item("Unit").ToString
                        dgvInspect_ITEMLIST.Item(2, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        dgvInspect_ITEMLIST.Item(3, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        dgvInspect_ITEMLIST.Item(4, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        dgvInspect_ITEMLIST.Item(5, count).Value = mydata.Rows(count).Item("Total_Cost").ToString

                    End With


                End With
            Next

        End With
        For x As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count
            txtTotalItems.Text = x.ToString
        Next
        sumx()

    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Inspect_Status.Text = "Inspect" Then
            save_click()

        ElseIf Inspect_Status.Text = "For Progress Billing" Then
            save_click_progress_billing()

        ElseIf Inspect_Status.Text = "Edit/Update" Then
            save_click_edit()

        Else
            MsgBox("K18 ERROR!")

        End If

    End Sub

    Public Sub sumx()
        'Total sum of total of the Items'
        Try
            Dim total As Decimal = 0
            For i As Decimal = 0 To dgvInspect_ITEMLIST.RowCount - 1
                total += dgvInspect_ITEMLIST.Rows(i).Cells(5).Value
            Next
            txtSubTotal.Text = Decimal.Parse(total).ToString("N2")

        Catch ex As Exception

        End Try



    End Sub

    Private Sub btnPrintInspection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintInspection.Click

        Inspect_Print.ShowDialog()

    End Sub

    Private Sub dgvInspect_ITEMLIST_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInspect_ITEMLIST.CellEndEdit
        If Inspect_Status.Text = "View" Then

            MsgBox("Can't Saved! Click the Edit Button First!")

        Else

            Try



                For Each r As DataGridViewRow In dgvInspect_ITEMLIST.Rows

                    r.Cells(5).Value = r.Cells(2).Value * r.Cells(4).Value

                Next
                sumx()

            Catch ex As Exception
                MsgBox("Please Input Integer!")
            End Try
        End If


    End Sub

    Private Sub btnPrintDisbursement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintDisbursement.Click
        Voucher_Print.ShowDialog()


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MsgBox("Hi!, Please Select Progress Billing!")
    End Sub


    Public Sub save_click_progress_billing()
        Try



            If String.IsNullOrEmpty(txtInvoiceNo.Text) Then
                MsgBox("Please Add receipt Number!")
                txtInvoiceNo.Focus()
            Else

                If String.IsNullOrEmpty(combo_inspect1.Text) And String.IsNullOrEmpty(combo_inspect2.Text) Then
                    MsgBox("Please Add atleast one Inspection Officer!")
                Else
                    For i As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count - 1

                        With myquery
                            connect(myquery)
                            .setTable("tbl_inspection_items_details_progress_billing")

                            If dgvInspect_ITEMLIST.Item(0, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(0, i).Value, "Item_ID")
                            End If

                            If dgvInspect_ITEMLIST.Item(1, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(1, i).Value, "Unit")
                            End If
                            If dgvInspect_ITEMLIST.Item(2, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(2, i).Value, "Quantity")

                            End If
                            If dgvInspect_ITEMLIST.Item(3, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(3, i).Value, "Items_Description")
                            End If
                            If dgvInspect_ITEMLIST.Item(4, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(4, i).Value, "Unit_Cost")
                            End If
                            If dgvInspect_ITEMLIST.Item(5, i).Value <> Nothing Then
                                .addValue(dgvInspect_ITEMLIST.Item(5, i).Value, "Total_Cost")
                            End If

                

                            .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value.ToString, "PO_number")
                            .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
                            .addValue(datefrom.Text.ToString, "Date_From_Billing")
                            .addValue(dateto.Text.ToString, "Date_To_Billing")
                            .addValue(combo_progressbill.Text.ToString, "Progress_Billing")
                            .runInsert()
                        End With
                    Next

                    With myquery
                        connect(myquery)
                        .setTable("tbl_inspection_progress_billing")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value.ToString, "PO_number")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(1).Value.ToString, "PR_number")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(2).Value.ToString, "Department")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(3).Value.ToString, "Supplier_Name")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(4).Value.ToString, "Supplier_Address")
                        .addValue(Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(5).Value.ToString, "Purpose")

                        .addValue(combo_inspect1.Text.ToString, "inspected_by_1")
                        .addValue(combo_inspect2.Text.ToString, "inspected_by_2")
                        .addValue(combo_inspect3.Text.ToString, "inspected_by_3")
                        .addValue(combo_inspect4.Text.ToString, "inspected_by_4")

                        .addValue(received1.Text.ToString, "End_User1")
                        .addValue(received2.Text.ToString, "End_User2")
                        .addValue(received3.Text.ToString, "End_User3")
                        .addValue(received4.Text.ToString, "End_User4")
                        .addValue(received5.Text.ToString, "End_User5")
                        .addValue(accept.Text.ToString, "Accepted_by")


                        .addValue(txtInvoiceNo.Text.ToString, "Invoice_Number")
                        .addValue(txtBarcode.Text.ToString, "BARCODE_Number")
                        .addValue(date_bac_received.Text.ToString, "Date_BAC_Received")
                        .addValue(txtRemarks.Text.ToString, "Remarks")
                        .addValue(combo_progressbill.Text.ToString, "Progress_Billing")
                        .runInsert()


                    End With


                    With myquery
                        connect(myquery)
                        .setTable("tbl_purchase_order_items_details")
                        .addValue(txtInvoiceNo.Text.ToString, "item_transit_receipt_number")
                        .addValue("YES - Progressbilling", "Inspect")
                        .runUpdate("PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows(0).Cells(0).Value & "' and isDeleted =0")


                    End With


                    With myquery
                        connect(myquery)
                        .setTable("tbl_purchase_order")
                        .addValue("YES - Progressbilling", "Inspect")
                        .runUpdate("PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows(0).Cells(0).Value & "' and isDeleted =0")


                    End With

                    combo_inspect1.ResetText()
                    combo_inspect2.ResetText()
                    combo_inspect3.ResetText()
                    combo_inspect4.ResetText()

                    received1.ResetText()
                    received2.ResetText()
                    received3.ResetText()
                    received4.ResetText()
                    received5.ResetText()

                    txtInvoiceNo.ResetText()


                    MsgBox("Succesfully saved! '" & combo_progressbill.Text.ToString & "'")



                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub combo_progressbill_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles combo_progressbill.SelectedIndexChanged
        If Inspect_Status.Text = "For Progress Billing" Then
            'save_items_inspection_progressbilling()
            If dgvInspect_ITEMLIST.Rows.Count <= 0 Then
                MsgBox("Not Billed Yet!! , " & combo_progressbill.Text & "")

                lblBilled.Text = "Not Billed Yet!!"
                lblBilled.ForeColor = Color.Red
                save_items_inspection()
            Else
                MsgBox("Already Billed!! , " & combo_progressbill.Text & "")

                lblBilled.Text = "Already Billed!!"
                lblBilled.ForeColor = Color.Green
                save_items_inspection_progressbilling()

            End If

        End If



    End Sub




    Public Sub save_items_inspection_progressbilling()
        dgvInspect_ITEMLIST.Rows.Clear()
        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT tipb.Item_ID, tipb.Unit, tipb.Quantity, tipb.Items_Description, tipb.Unit_Cost, tipb.Total_Cost, tinpb.`Item_Transit_Receipt_Number`, tinpb.`PO_Number`, tinpb.`End_User1`, tinpb.`End_User2`, tinpb.`End_User3`, tinpb.`End_User4`, tinpb.`End_User5`, tinpb.`Inspected_by_1`, tinpb.`Inspected_by_2`, tinpb.`Inspected_by_3`, tinpb.`Inspected_by_4`, tinpb.`Accepted_by`, tipb.`Progress_Billing`, tipb.`Date_From_Billing`, tipb.`Date_To_Billing` FROM tbl_inspection_items_details_progress_billing tipb INNER JOIN tbl_inspection_progress_billing tinpb ON tinpb.`Progress_Billing` = tipb.`Progress_Billing` WHERE tinpb.isDeleted = 0 AND tinpb.PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "' AND tipb.Progress_Billing = '" & combo_progressbill.Text.ToString & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInspect_ITEMLIST
                    .Rows.Add()
                    With .Rows(count)
                        dgvInspect_ITEMLIST.Item(0, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        dgvInspect_ITEMLIST.Item(1, count).Value = mydata.Rows(count).Item("Unit").ToString
                        dgvInspect_ITEMLIST.Item(2, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        dgvInspect_ITEMLIST.Item(3, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        dgvInspect_ITEMLIST.Item(4, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        dgvInspect_ITEMLIST.Item(5, count).Value = mydata.Rows(count).Item("Total_Cost").ToString

                        txtInvoiceNo.Text = mydata.Rows(count).Item("Item_Transit_Receipt_Number").ToString

                        received1.Text = mydata.Rows(count).Item("End_User1").ToString
                        received2.Text = mydata.Rows(count).Item("End_User2").ToString
                        received3.Text = mydata.Rows(count).Item("End_User3").ToString
                        received4.Text = mydata.Rows(count).Item("End_User4").ToString
                        received5.Text = mydata.Rows(count).Item("End_User5").ToString

                        combo_inspect1.Text = mydata.Rows(count).Item("Inspected_by_1").ToString
                        combo_inspect2.Text = mydata.Rows(count).Item("Inspected_by_2").ToString
                        combo_inspect3.Text = mydata.Rows(count).Item("Inspected_by_3").ToString
                        combo_inspect4.Text = mydata.Rows(count).Item("Inspected_by_4").ToString

                        accept.Text = mydata.Rows(count).Item("Accepted_by").ToString


                        dateto.Text = mydata.Rows(count).Item("Date_To_Billing").ToString
                        datefrom.Text = mydata.Rows(count).Item("Date_From_Billing").ToString
                    End With


                End With
            Next

        End With
        For x As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count
            txtTotalItems.Text = x.ToString
        Next
        sumx()



    End Sub


    Public Sub save_items_inspection()
        dgvInspect_ITEMLIST.Rows.Clear()

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT ti.Item_ID, ti.Unit, ti.Quantity, ti.Items_Description, ti.Unit_Cost, ti.Total_Cost, tin.`Item_Transit_Receipt_Number`, tin.`PO_Number`, tin.`End_User1`, tin.`End_User2`, tin.`End_User3`, tin.`End_User4`, tin.`End_User5`, tin.`Inspected_by_1`, tin.`Inspected_by_2`, tin.`Inspected_by_3`, tin.`Inspected_by_4`, tin.`Accepted_by` FROM tbl_inspection_items_details ti INNER JOIN tbl_inspection tin ON tin.`PO_Number` = ti.`PO_Number` WHERE tin.isDeleted = 0 AND tin.PO_Number = '" & Inspection_Main_Form.dgvInspect.SelectedRows.Item(0).Cells(0).Value & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInspect_ITEMLIST
                    .Rows.Add()
                    With .Rows(count)
                        dgvInspect_ITEMLIST.Item(0, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        dgvInspect_ITEMLIST.Item(1, count).Value = mydata.Rows(count).Item("Unit").ToString
                        dgvInspect_ITEMLIST.Item(2, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        dgvInspect_ITEMLIST.Item(3, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        dgvInspect_ITEMLIST.Item(4, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        dgvInspect_ITEMLIST.Item(5, count).Value = mydata.Rows(count).Item("Total_Cost").ToString

                        txtInvoiceNo.Text = mydata.Rows(count).Item("Item_Transit_Receipt_Number").ToString
                        received1.Text = mydata.Rows(count).Item("End_User1").ToString
                        received2.Text = mydata.Rows(count).Item("End_User2").ToString
                        received3.Text = mydata.Rows(count).Item("End_User3").ToString
                        received4.Text = mydata.Rows(count).Item("End_User4").ToString
                        received5.Text = mydata.Rows(count).Item("End_User5").ToString

                        combo_inspect1.Text = mydata.Rows(count).Item("Inspected_by_1").ToString
                        combo_inspect2.Text = mydata.Rows(count).Item("Inspected_by_2").ToString
                        combo_inspect3.Text = mydata.Rows(count).Item("Inspected_by_3").ToString
                        combo_inspect4.Text = mydata.Rows(count).Item("Inspected_by_4").ToString
                        accept.Text = mydata.Rows(count).Item("Accepted_by").ToString


                    End With


                End With
            Next

        End With
        For x As Integer = 0 To dgvInspect_ITEMLIST.Rows.Count
            txtTotalItems.Text = x.ToString
        Next
        sumx()

    End Sub

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged

        lblDateFrom.Text = datefrom.Value.ToString("MMMM dd, yyyy")

    End Sub

    Private Sub dateto_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateto.ValueChanged

        lblDateTo.Text = dateto.Value.ToString("MMMM dd, yyyy")

    End Sub

  
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Inspection_Login.ShowDialog()

    End Sub


End Class