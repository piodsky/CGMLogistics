
Public Class Add_Purchase_Order

    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim mydata2 As DataTable
    Shared random As New Random()
    Public category As String = ""




    Private Sub Add_Purchase_Order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        connect(myquery)


        If mdiMain.role.Text = 1 Or mdiMain.role.Text = 2 Or mdiMain.role.Text = 3 Or mdiMain.role.Text = 4 Then
            btnSave.Enabled = True

        Else
            btnSave.Enabled = False

        End If
        'dgvPO.Columns("Total_Cost").ReadOnly = True
        'dgvPO.Columns("Items_Description").ReadOnly = True
        dgvPO.Columns("Item_ID").ReadOnly = True

        po_date.Format = DateTimePickerFormat.Custom
        po_date.CustomFormat = "yyyy-MM-dd HH:mm:ss"



        If PO_Status.Text = "Add" Then
            dgvPO.Rows.Clear()
            txt_pr_number.ResetText()
            combo_dept.ResetText()
            combo_section.ResetText()
            combo_div.ResetText()
            combo_supplier.ResetText()
            combo_supplier_address.ResetText()
            po_date.ResetText()
            purpose.ResetText()

            lblsave.Text = "Save"

            lblpo.Visible = False
            txtPONUMBER.Visible = False
        ElseIf PO_Status.Text = "Edit/Update" Then
            lblsave.Text = "Update"
            form_load_exist()
            lblpo.Visible = True
            txtPONUMBER.Visible = True
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles combo_dept.KeyPress, combo_section.KeyPress, combo_div.KeyPress, purpose.KeyPress, combo_supplier_address.KeyPress, combo_supplier.KeyPress, txt_pr_number.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub btnUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUse.Click
        PPMP_Items.ShowDialog()
    End Sub

    Private Sub dgvPO_ItemsList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPO.CellEndEdit


        Try



            For Each r As DataGridViewRow In dgvPO.Rows

                If r.Cells(3).Value = "" And r.Cells(4).Value = "" Then

                Else



                    r.Cells(5).Value = r.Cells(3).Value * r.Cells(4).Value
                End If
            Next
            For x As Integer = 0 To dgvPO.Rows.Count
                txtTotalItems.Text = x.ToString
            Next
            sumx()

        Catch ex As Exception
            MsgBox("Please Input Integer!")
        End Try

    End Sub


    Public Sub sumx()
        'Total sum of total of the Items'
        Try

            'If dgvPO.Rows(i).Cells(3).Value = "" And dgvPO.Rows(i).Cells(4).Value = "" Then
            '    total += dgvPO.Rows(i).Cells(5).Value
            'Else


            Dim total As Decimal = 0
            For i As Decimal = 0 To dgvPO.RowCount - 1

                total += dgvPO.Rows(i).Cells(5).Value




            Next
            txtSubTotal.Text = Decimal.Parse(total).ToString("N2")
        Catch ex As Exception

        End Try





    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If PO_Status.Text = "View" Then

            MsgBox("For Viewing Purposes Only!!!")

        Else

            For i As Integer = (Me.po_details.Controls.Count - 1) To 0 Step -1
                If Me.po_details.Controls(i).Text.ToString = "" Then
                    MsgBox("Please leave no blank in each entry", MsgBoxStyle.Information)
                    Me.po_details.Controls(i).Focus()
                    Exit Sub
                End If
            Next i

            If dgvPO.Rows.Count = 0 Then
                MsgBox("Please Put Items Before Place Order!!!")
            Else

                If PO_Status.Text = "Add" Then
                    save_po()


                ElseIf PO_Status.Text = "Edit/Update" Then
                    save_po_update()
                End If

            End If
        End If
    End Sub



    Public Sub save_po()

        Dim mydatapo As DataTable = myquery.runQuery("SELECT max(ID) from tbl_purchase_order")

        Dim id As Integer = mydatapo.Rows(0).Item(0) + 1

        Dim ponumber As String = category.ToString & "-" & "sys" & "" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
        Dim itemid As String = ""
        Dim mydatacompare As DataTable





        With myquery

            .setTable("tbl_purchase_order")

            'missin PR NUMBER and Canvas ID
            .addValue(ponumber.ToString, "PO_Number")
            .addValue(txt_pr_number.Text, "PR_Number")

            .addValue(combo_pr_received_name.Text, "PR_Received_Name")
            .addValue(combo_category.Text, "Category")

            .addValue(combo_dept.Text, "Department")
            .addValue(combo_div.Text, "Division")
            .addValue(combo_section.Text, "Section")

            .addValue(Ref_Number.Text, "Reference_No")
            .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")

            .addValue(combo_supplier.Text, "Supplier_Name")
            .addValue(combo_supplier_address.Text, "Supplier_Address")

            .addValue(po_date.Text.ToString, "Date_PO")
            .addValue(purpose.Text, "Purpose")


            .addValue(mdiMain.stbUser.Text, "End_User")
            .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
            .runInsert()
        End With

        For i As Integer = 0 To dgvPO.Rows.Count - 1

            With myquery



                mydatacompare = .runQuery("SELECT * FROM tbl_ppmp_bac_items WHERE Items_Description='" + AddSlashes(dgvPO.Item(2, i).Value.ToString) + "'")
                If mydatacompare.Rows.Count > 0 Then
                    .setTable("tbl_purchase_order_items_details")

                    If dgvPO.Item(0, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(0, i).Value, "Item_ID")
                    End If

                    If dgvPO.Item(1, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(1, i).Value, "Unit")
                    End If
                    If dgvPO.Item(2, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(2, i).Value, "Items_Description")

                    End If
                    If dgvPO.Item(3, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(3, i).Value, "Quantity")
                    End If
                    If dgvPO.Item(4, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(4, i).Value, "Unit_Cost")
                    End If
                    If dgvPO.Item(5, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(5, i).Value, "Total_Cost")
                    End If
                    If dgvPO.Item(6, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(6, i).Value, "NumberSeries")
                    End If



                    .addValue(ponumber.ToString, "PO_Number")
                    .addValue(po_date.Text.ToString, "Date_PO")
                    .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
                    .addValue(mdiMain.stbUser.Text, "End_User")
                    .runInsert()

                Else
                    'NEW ITEM

                    .setTable("tbl_ppmp_bac_items")
                    If dgvPO.Item(1, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(1, i).Value, "Unit")
                    End If
                    If dgvPO.Item(2, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(2, i).Value, "Items_Description")
                    End If
                    If dgvPO.Item(4, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(4, i).Value, "Unit_Cost")
                    End If
                    .addValue(0, "isDeleted")
                    .runInsert()

                    'GETTING NEW ITEM ID OF ADDED ITEMS
                    mydatacompare = .runQuery("SELECT * FROM tbl_ppmp_bac_items WHERE Items_Description='" + AddSlashes(dgvPO.Item(2, i).Value.ToString) + "'")
                    If mydatacompare.Rows.Count > 0 Then
                        itemid = mydatacompare.Rows(0).Item("Item_ID")
                    Else
                        'do nothing
                    End If

                    'ADD NEW ITEMS TO DETAILS WITH NEW ITEM ID
                    .setTable("tbl_purchase_order_items_details")

                    If itemid <> Nothing Then
                        .addValue(itemid, "Item_ID")
                    End If

                    If dgvPO.Item(1, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(1, i).Value, "Unit")
                    End If
                    If dgvPO.Item(2, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(2, i).Value, "Items_Description")

                    End If
                    If dgvPO.Item(3, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(3, i).Value, "Quantity")
                    End If
                    If dgvPO.Item(4, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(4, i).Value, "Unit_Cost")
                    End If
                    If dgvPO.Item(5, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(5, i).Value, "Total_Cost")
                    End If

                    If dgvPO.Item(6, i).Value <> Nothing Then
                        .addValue(dgvPO.Item(6, i).Value, "NumberSeries")
                    End If

                    .addValue(ponumber.ToString, "PO_Number")
                    .addValue(po_date.Text.ToString, "Date_PO")
                    .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
                    .addValue(mdiMain.stbUser.Text, "End_User")
                    .runInsert()

                End If
            End With
        Next

        MsgBox("Successfully Saved!")
        'MsgBox(ponumber, MsgBoxStyle.OkOnly, "System Generated PO Number")

        Dim frmPO As New PO_Number_Result
        With frmPO
            .po = ponumber
            .ShowDialog()
        End With


        Me.Close()
        PO_Main_Form.form_load()

    End Sub
    Private Function AddSlashes(ByVal InputTxt As String) As String
        ' List of characters handled:
        ' \000 null
        ' \010 backspace
        ' \011 horizontal tab
        ' \012 new line
        ' \015 carriage return
        ' \032 substitute
        ' \042 double quote
        ' \047 single quote
        ' \134 backslash
        ' \140 grave accent

        Dim Result As String = InputTxt

        Try
            Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, "[\000\010\011\012\015\032\042\047\134\140]", "\$0")
        Catch Ex As Exception
            ' handle any exception here
            Console.WriteLine(Ex.Message)
        End Try

        Return Result
    End Function
    Public Sub save_po_update()


        Dim mydatapo As DataTable = myquery.runQuery("SELECT max(ID) from tbl_purchase_order")
        Dim id As Integer = mydatapo.Rows(0).Item(0) + 1
        Dim ponumber As String = category.ToString & "-" & "sys" & "" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
        Dim itemid As String = ""
        Dim mydatacompare As DataTable

        With myquery

            .setTable("tbl_purchase_order")
            .addValue("1", "isDeleted")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")

        End With

        With myquery

            .setTable("tbl_purchase_order")

            'missin PR NUMBER and Canvas ID
            .addValue(PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value.ToString, "PO_Number")
            .addValue(txt_pr_number.Text, "PR_Number")


            .addValue(combo_pr_received_name.Text, "PR_Received_Name")
            .addValue(combo_category.Text, "Category")

            .addValue(Ref_Number.Text, "Reference_No")
            .addValue(BAC_Resolution_Number.Text, "BAC_Resolution_No")

            .addValue(supplier_date_received.Text, "Date_Received_Supplier")
            .addValue(coa_date_received.Text, "Date_COA_Received")
            .addValue(approved_po_date.Text, "Date_Approved_PO")
            .addValue(remarks.Text, "Remarks")


            .addValue(combo_dept.Text, "Department")
            .addValue(combo_div.Text, "Division")
            .addValue(combo_section.Text, "Section")
            .addValue(combo_supplier.Text, "Supplier_Name")
            .addValue(combo_supplier_address.Text, "Supplier_Address")

            .addValue(po_date.Text.ToString, "Date_PO")
            .addValue(purpose.Text, "Purpose")


            .addValue(mdiMain.stbUser.Text, "Edited_By")
            .addValue(mdiMain.stbUser.Text, "End_User")
            .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
            .runInsert()

        End With

        With myquery

            .setTable("tbl_purchase_order_items_details")
            .addValue("1", "isDeleted")
            .runUpdate("PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
        End With

        Try

            For i As Integer = 0 To dgvPO.Rows.Count - 1

                With myquery



                    mydatacompare = .runQuery("SELECT * FROM tbl_ppmp_bac_items WHERE Items_Description='" + AddSlashes(dgvPO.Item(2, i).Value.ToString) + "'")
                    If mydatacompare.Rows.Count > 0 Then
                        .setTable("tbl_purchase_order_items_details")

                        If dgvPO.Item(0, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(0, i).Value, "Item_ID")
                        End If

                        If dgvPO.Item(1, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(1, i).Value, "Unit")
                        End If
                        If dgvPO.Item(2, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(2, i).Value, "Items_Description")

                        End If
                        If dgvPO.Item(3, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(3, i).Value, "Quantity")
                        End If
                        If dgvPO.Item(4, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(4, i).Value, "Unit_Cost")
                        End If
                        If dgvPO.Item(5, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(5, i).Value, "Total_Cost")
                        End If
                        If dgvPO.Item(6, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(6, i).Value, "NumberSeries")
                        End If


                        .addValue(PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value.ToString, "PO_Number")
                        .addValue(po_date.Text.ToString, "Date_PO")
                        .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
                        .addValue(mdiMain.stbUser.Text, "End_User")
                        .runInsert()

                    Else
                        'NEW ITEM

                        .setTable("tbl_ppmp_bac_items")
                        If dgvPO.Item(1, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(1, i).Value, "Unit")
                        End If
                        If dgvPO.Item(2, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(2, i).Value, "Items_Description")
                        End If
                        If dgvPO.Item(4, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(4, i).Value, "Unit_Cost")
                        End If
                        .addValue(0, "isDeleted")
                        .runInsert()


                        'GETTING NEW ITEM ID OF ADDED ITEMS
                        mydatacompare = .runQuery("SELECT * FROM tbl_ppmp_bac_items WHERE Items_Description='" + AddSlashes(dgvPO.Item(2, i).Value.ToString) + "'")
                        If mydatacompare.Rows.Count > 0 Then
                            itemid = mydatacompare.Rows(0).Item("Item_ID")
                        Else
                            'do nothing
                        End If

                        'ADD NEW ITEMS TO DETAILS WITH NEW ITEM ID

                        .setTable("tbl_purchase_order_items_details")

                        If itemid <> Nothing Then
                            .addValue(itemid, "Item_ID")
                        End If

                        If dgvPO.Item(1, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(1, i).Value, "Unit")
                        End If
                        If dgvPO.Item(2, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(2, i).Value, "Items_Description")

                        End If
                        If dgvPO.Item(3, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(3, i).Value, "Quantity")
                        End If
                        If dgvPO.Item(4, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(4, i).Value, "Unit_Cost")

                        End If
                        If dgvPO.Item(5, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(5, i).Value, "Total_Cost")
                        End If
                        If dgvPO.Item(6, i).Value <> Nothing Then
                            .addValue(dgvPO.Item(6, i).Value, "NumberSeries")
                        End If


                        .addValue(PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value.ToString, "PO_Number")
                        .addValue(po_date.Text.ToString, "Date_PO")
                        .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
                        .addValue(mdiMain.stbUser.Text, "End_User")
                        .runInsert()

                    End If
                End With
            Next

            MsgBox("Successfully Updated!")

            Me.Close()
            PO_Main_Form.form_load()

        Catch ex As Exception
            MsgBox("Please dont leave Blanks!")
        End Try



    End Sub

    Public Sub form_load_exist()
        dgvPO.Rows.Clear()


        With myquery

            'mydata = .runQuery("SELECT Unit,Items_Description, Quantity, Unit_Cost, Total_Cost, PO_Number, PO_Number, Department, Division, Section, Supplier_Name, Supplier_Address, Date_PO, Purpose, Requested_by, PO_Number, Date_Created FROM tbl_purchase_order WHERE isDeleted =0 and PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
            mydata = .runQuery("SELECT tp.`PO_Number`, tp.`PR_Number`, tp.`Department`, tp.`Division`, tp.`Section`, tp.`Supplier_Name`, tp.`Supplier_Address`, tp.`Date_Received_Supplier`, tp.`PR_Received_Name`, tp.`Category`, tp.`Date_Received_Supplier`, tp.`Date_COA_Received`, tp.`Date_Approved_PO`, tp.`Remarks`, tp.`Date_PO`, tp.`Purpose`, tp.`Requested_by`, tp.Reference_No, tp.BAC_Resolution_No, tpd.`Item_ID`, tpd.`Items_Description`, tpd.`Unit`, tpd.`Quantity`, tpd.`Unit_Cost`, tpd.`Total_Cost`, tpd.`NumberSeries` FROM tbl_purchase_order tp INNER JOIN tbl_purchase_order_items_details tpd ON tp.PO_Number = tpd.PO_Number WHERE tp.isDeleted = 0 AND tpd.isDeleted = 0 AND tp.PO_Number = '" & PO_Main_Form.dgvPO_LIST.SelectedRows(0).Cells(0).Value & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvPO
                    .Rows.Add()
                    With .Rows(count)

                        dgvPO.Item(0, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        dgvPO.Item(1, count).Value = mydata.Rows(count).Item("Unit").ToString
                        dgvPO.Item(2, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        dgvPO.Item(3, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        dgvPO.Item(4, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        dgvPO.Item(5, count).Value = mydata.Rows(count).Item("Total_Cost").ToString
                        dgvPO.Item(6, count).Value = mydata.Rows(count).Item("NumberSeries").ToString



                        txt_pr_number.Text = mydata.Rows(count).Item("PR_Number").ToString
                        combo_pr_received_name.Text = mydata.Rows(count).Item("PR_Received_Name").ToString
                        combo_category.Text = mydata.Rows(count).Item("Category").ToString

                        combo_dept.Text = mydata.Rows(count).Item("Department").ToString
                        combo_div.Text = mydata.Rows(count).Item("Division").ToString
                        combo_section.Text = mydata.Rows(count).Item("Section").ToString
                        combo_supplier.Text = mydata.Rows(count).Item("Supplier_Name").ToString
                        combo_supplier_address.Text = mydata.Rows(count).Item("Supplier_Address").ToString
                        po_date.Text = mydata.Rows(count).Item("Date_PO").ToString
                        purpose.Text = mydata.Rows(count).Item("Purpose").ToString

                     
                        supplier_date_received.Text = mydata.Rows(count).Item("Date_Received_Supplier").ToString
                        coa_date_received.Text = mydata.Rows(count).Item("Date_COA_Received").ToString
                        approved_po_date.Text = mydata.Rows(count).Item("Date_Approved_PO").ToString

                        Ref_Number.Text = mydata.Rows(count).Item("Reference_No").ToString
                        BAC_Resolution_Number.Text = mydata.Rows(count).Item("BAC_Resolution_No").ToString

                        remarks.Text = mydata.Rows(count).Item("Remarks").ToString
                   
                        txtPONUMBER.Text = mydata.Rows(count).Item("PO_Number").ToString
                    End With

                End With

            Next
            ' dgvPPMPItemsList.Columns(0).Visible = False


        End With

        For x As Integer = 0 To dgvPO.Rows.Count
            txtTotalItems.Text = x.ToString
        Next

        sumx()
    End Sub
    Private Sub AddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRow.Click
        If dgvPO.Rows.Count > 0 Then
            Dim i As Integer
            i = dgvPO.SelectedCells.Item(0).RowIndex + 1
            dgvPO.Rows.Insert(i)
        Else
            dgvPO.Rows.Add()

        End If

    End Sub




    Private Sub RemoveItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveItemsToolStripMenuItem.Click

        Dim z As Integer = dgvPO.CurrentRow.Index
        dgvPO.Rows.RemoveAt(z)
        For x As Integer = 0 To dgvPO.Rows.Count
            txtTotalItems.Text = x.ToString
        Next
        sumx()

    End Sub

    Public Sub ids()
        Dim dep_ids, sec_ids, div_ids, supplier_ids, supplier_address As DataTable

        With myquery
            connect(myquery)
            dep_ids = .runQuery("SELECT * FROM ref_department WHERE Department = '" & combo_dept.Text.ToString & "'")
            div_ids = .runQuery("SELECT * FROM ref_division WHERE Division = '" & combo_div.Text.ToString & "'")
            sec_ids = .runQuery("SELECT * FROM ref_section WHERE Section = '" & combo_section.Text.ToString & "'")
            supplier_ids = .runQuery("SELECT * FROM ref_supplier WHERE Supplier_Name ='" & combo_supplier.Text.ToString & "'")
            supplier_address = .runQuery("SELECT * FROM ref_supplier WHERE Supplier_Name ='" & combo_supplier.Text.ToString & "'")
        End With
        For Each mydatarow In dep_ids.Rows
            With mydatarow
                dep_id.Text = CStr(.item("ID"))

            End With
        Next
        For Each mydatarow In sec_ids.Rows
            With mydatarow
                sec_id.Text = CStr(.item("ID"))

            End With
        Next

        For Each mydatarow In div_ids.Rows
            With mydatarow
                div_id.Text = CStr(.item("ID"))

            End With
        Next
        For Each mydatarow In supplier_ids.Rows
            With mydatarow
                supplier_id.Text = CStr(.item("SupplierID"))

            End With
        Next
        For Each mydatarow In supplier_address.Rows
            With mydatarow
                Try
                    combo_supplier_address.Text = CStr(.item("Supplier_Address"))
                Catch ex As Exception

                End Try


            End With
        Next

    End Sub

    Private Sub combo_dept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_dept.SelectedIndexChanged
        ids()
    End Sub

    Private Sub combo_div_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_div.SelectedIndexChanged
        ids()
    End Sub

    Private Sub combo_section_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_section.SelectedIndexChanged
        ids()
    End Sub

    Private Sub combo_supplier_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_supplier.SelectedIndexChanged
        ids()
    End Sub

    Private Sub dgvPO_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvPO.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvPO.CurrentCell = dgvPO(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?

        End If
    End Sub
    Private copiedValues As New List(Of String) ' Store the copied values here
    Private Sub dgvPO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvPO.KeyDown
        ' Check if Ctrl + C is pressed to copy the selected cells' values
        If e.Control AndAlso e.KeyCode = Keys.C Then
            ' Clear previously copied values
            copiedValues.Clear()

            ' Loop through the selected cells and add their values to the list
            For Each cell As DataGridViewCell In dgvPO.SelectedCells
                If cell.Value IsNot Nothing Then
                    copiedValues.Add(cell.Value.ToString())
                End If
            Next


        End If

        ' Check if Ctrl + V is pressed to paste the copied values into the selected cell
        If e.Control AndAlso e.KeyCode = Keys.V Then
            ' Check if a cell is selected to paste the value
            If dgvPO.CurrentCell IsNot Nothing AndAlso copiedValues.Count > 0 Then
                ' Get the currently selected cell (the target for pasting)
                Dim currentRow As Integer = dgvPO.CurrentCell.RowIndex
                Dim currentColumn As Integer = dgvPO.CurrentCell.ColumnIndex

                ' Paste the copied values into the selected cells
                For i As Integer = 0 To Math.Min(copiedValues.Count - 1, dgvPO.Columns.Count - currentColumn - 1)
                    ' Ensure we don't go out of bounds
                    If currentColumn + i < dgvPO.Columns.Count Then
                        dgvPO.Rows(currentRow).Cells(currentColumn + i).Value = copiedValues(i)
                    End If
                Next

            End If
        End If
    End Sub

    Private Sub dgvPO_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvPO.KeyUp
        If e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True
            SendKeys.Send("{DOWN}")
        End If

        If e.Modifiers = Keys.Shift AndAlso e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True
            SendKeys.Send("{UP}")
        End If
    End Sub


    Private Sub AddRowNumberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRowNumberToolStripMenuItem.Click
        Dim itemrow As Integer = 0
        For Each r As DataGridViewRow In dgvPO.Rows

            If r.Cells(3).Value = "" And r.Cells(4).Value = "" Then

            Else



                r.Cells(6).Value = +itemrow
            End If
        Next
    End Sub




End Class