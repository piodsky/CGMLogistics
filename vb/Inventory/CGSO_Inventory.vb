Imports System.Data.OleDb

Public Class CGSO_Inventory
    Inherits Windows.Forms.Form

    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Dim id As Integer = 0
    Dim isClosing As Boolean
    Dim mydata_name, mydata_department, mydata_classification, mydata_position As DataTable
    Dim tbldisplay As New DataTable
    Dim fontencoder As New IDAutomation.NetAssembly.FontEncoder

    Private scAutoComplete1 As New AutoCompleteStringCollection
    Private scAutoComplete2 As New AutoCompleteStringCollection
    Private scAutoComplete3 As New AutoCompleteStringCollection
    Private scAutoComplete4 As New AutoCompleteStringCollection


    Private Sub btnSelectPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPO.Click
        With CGSO_PO_Select
            .ShowDialog()
        End With
    End Sub

    Public Sub sourceload()
        Dim yearnow As Integer = Year(Now)
        mydata_name = myquery.runQuery("SELECT CONCAT(firstname,' ',LEFT(middlename, 1),'. ',surname)AS `NAME` FROM hrmis.`tblpersonalinformation` WHERE CONCAT(firstname,' ',LEFT(middlename, 1),'. ',surname) IS NOT NULL ORDER BY `Name`")
        mydata_department = myquery.runQuery("SELECT Department FROM ref_department")
        mydata_classification = myquery.runQuery("SELECT classification FROM ref_classification")
        mydata_position = myquery.runQuery("SELECT Position from hrmis.tbl_syl_payroll_parent WHERE YEAR = " & yearnow & "")


        With scAutoComplete1
            .Clear()
            For Each dataComboRow In mydata_name.Rows
                .Add(dataComboRow(0))
            Next
        End With
        With scAutoComplete2
            .Clear()
            For Each dataComboRow In mydata_department.Rows
                .Add(dataComboRow(0))
            Next
        End With

        With scAutoComplete3
            .Clear()
            For Each dataComboRow In mydata_classification.Rows
                .Add(dataComboRow(0))
            Next
        End With

        With scAutoComplete4
            .Clear()
            For Each dataComboRow In mydata_position.Rows
                .Add(dataComboRow(0))
            Next
        End With
        AddHandler dgvNewInventory.CellFormatting, AddressOf dgvNewInventory_CellFormatting

    End Sub

   
    Public Sub total_amount()
        Try
            ' Initialize a variable to store the total amount
            Dim total As Decimal = 0

            ' Iterate through each row in the DataGridView
            For i As Decimal = 0 To dgvNewInventory.RowCount - 1
                ' Check if the checkbox in the first column of the current row is checked
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    ' If checked, add the value in the 8th column (index 7) to the total
                    total += dgvNewInventory.Rows(i).Cells(7).Value
                End If
            Next

            ' Display the total amount in a TextBox named txtTotal_Amount
            txtTotal_Amount.Text = Decimal.Parse(total).ToString("N2")
        Catch ex As Exception
            ' Handle any exceptions that may occur (currently empty)
        End Try
    End Sub

    Private Sub CGSO_Inventory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ' Check if the F3 key is pressed
        If e.KeyCode = Keys.F3 Then

            With CGSO_PO_Select
                .ShowDialog()
            End With
        End If
    End Sub


  

    Private Sub CGSO_Inventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.KeyPreview = True
        po_date.Format = DateTimePickerFormat.Custom
        po_date.CustomFormat = "yyyy-MM-dd HH:mm:ss"


        Date_Entry.Format = DateTimePickerFormat.Custom
        Date_Entry.CustomFormat = "yyyy-MM-dd"


        connect(myquery)
        cgso_data()
        sourceload()

        total_amount()

        ' dgvNewInventory.Columns(11).ReadOnly = True
        If inventory_state.Text = "Add" Then

            groupprint.Enabled = False

            dgvNewInventory.Rows.Clear()
            txtDepartment.ResetText()
            txtPRNUMBER.ResetText()
            txtPONUMBER.ResetText()
            combo_category.Text = ""
            combo_mode.Text = ""
            combo_supplier.Text = ""
            combo_supplier_address.Text = ""
            purpose.ResetText()

            txtTotalItems.Text = "0"
            txtSubTotal.Text = "0.00"
            txtBarcode.Text = ""

            btnEdit.Visible = False
            btnDelete.Visible = False
            btnSave.Text = "SAVE"

            txtBarcode.Visible = False
            lblbarcode.Visible = False

        ElseIf inventory_state.Text = "View/Update" Then

            groupprint.Enabled = True

            btnEdit.Visible = True
            btnDelete.Visible = True
            btnSave.Text = "SAVE"
            form_load_exist()

            txtBarcode.Visible = True
            lblbarcode.Visible = True
        End If


    End Sub

   
    Private Sub dgvNewInventory_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvNewInventory.CellFormatting
        ' Check if we are formatting the desired columns (column index 6 or 7)
        If (e.ColumnIndex = 6 Or e.ColumnIndex = 7) AndAlso e.Value IsNot Nothing Then
            Dim decimalValue As Decimal
            If Decimal.TryParse(e.Value.ToString(), decimalValue) Then
                e.Value = decimalValue.ToString("N2")
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Public Sub form_load_exist()
        Try


            dgvNewInventory.Rows.Clear()


            With myquery
                connect(myquery)

                mydata = .runQuery("SELECT tid.`Inv_ID`,tid.`Item_ID`,tid.`Items_Description`,tid.`Unit`,tid.`Quantity`,tid.`Unit_Cost`,tid.`Total_Cost`,tid.Tag_Number,tid.Serial_Number,tid.Inventory_Category,tid.PAR_ICS_Number,tid.`Accountable_Officer`,tid.`Position`,tid.`Location`,tid.`Classification`,tid.`Remarks`,tid.`Condition`,ti.`PO_Number`,ti.`PR_Number`,ti.`Department`,ti.`Category`,ti.`Mode_of_Procurement`,ti.`Supplier_Name`,ti.`Supplier_Address`,ti.`Purpose`, ti.`Source_of_Fund`,ti.`Date_Created`,ti.`PO_Date`,ti.`Date_Entry` ,tid.`NumberSeries`, ti.`Initial`, tid.`PTR_ITR_Number`, tid.`Estimated_Life` FROM tbl_inventory_items_details tid INNER JOIN tbl_inventory ti ON tid.PO_Number = ti.PO_Number WHERE tid.isDeleted = 0 AND ti.isDeleted = 0 AND tid.PO_Number ='" & CGSO_Main_Form.dgvInventory.SelectedRows(0).Cells(1).Value & "' ")
                For count As Integer = 0 To mydata.Rows.Count - 1
                    With dgvNewInventory
                        .Rows.Add()
                        With .Rows(count)

                            dgvNewInventory.Item(1, count).Value = mydata.Rows(count).Item("Inv_ID").ToString
                            dgvNewInventory.Item(2, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                            dgvNewInventory.Item(3, count).Value = mydata.Rows(count).Item("Unit").ToString
                            dgvNewInventory.Item(4, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                            dgvNewInventory.Item(5, count).Value = mydata.Rows(count).Item("Quantity").ToString
                            'Dim unitCost As Decimal = Convert.ToDecimal(mydata.Rows(count).Item("Unit_Cost"))
                            'dgvNewInventory.Item(6, count).Value = unitCost.ToString("N2")
                            dgvNewInventory.Item(6, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                            dgvNewInventory.Item(7, count).Value = mydata.Rows(count).Item("Total_Cost").ToString
                            dgvNewInventory.Item(8, count).Value = mydata.Rows(count).Item("Serial_Number").ToString
                            dgvNewInventory.Item(9, count).Value = mydata.Rows(count).Item("Tag_Number").ToString
                            dgvNewInventory.Item(10, count).Value = mydata.Rows(count).Item("Inventory_Category").ToString
                            dgvNewInventory.Item(11, count).Value = mydata.Rows(count).Item("PAR_ICS_Number").ToString
                            dgvNewInventory.Item(12, count).Value = mydata.Rows(count).Item("Accountable_Officer").ToString
                            dgvNewInventory.Item(13, count).Value = mydata.Rows(count).Item("Position").ToString
                            dgvNewInventory.Item(14, count).Value = mydata.Rows(count).Item("Location").ToString
                            dgvNewInventory.Item(15, count).Value = mydata.Rows(count).Item("Classification").ToString
                            dgvNewInventory.Item(16, count).Value = mydata.Rows(count).Item("Remarks").ToString
                            dgvNewInventory.Item(17, count).Value = mydata.Rows(count).Item("Condition").ToString
                            dgvNewInventory.Item(18, count).Value = mydata.Rows(count).Item("NumberSeries").ToString
                            dgvNewInventory.Item(19, count).Value = mydata.Rows(count).Item("PTR_ITR_Number").ToString
                            dgvNewInventory.Item(20, count).Value = mydata.Rows(count).Item("Estimated_Life").ToString

                            po_date.Text = mydata.Rows(count).Item("PO_Date").ToString
                            Date_Entry.Text = mydata.Rows(count).Item("Date_Entry").ToString
                            txtPONUMBER.Text = mydata.Rows(count).Item("PO_Number").ToString
                            txtPRNUMBER.Text = mydata.Rows(count).Item("PR_Number").ToString
                            combo_category.Text = mydata.Rows(count).Item("Category").ToString
                            combo_mode.Text = mydata.Rows(count).Item("Mode_of_Procurement").ToString

                            combo_supplier.Text = mydata.Rows(count).Item("Supplier_Name").ToString
                            combo_supplier_address.Text = mydata.Rows(count).Item("Supplier_Address").ToString

                            txtInitial.Text = mydata.Rows(count).Item("Initial").ToString
                            purpose.Text = mydata.Rows(count).Item("Purpose").ToString
                            txtSource.Text = mydata.Rows(count).Item("Source_of_Fund").ToString
                            txtDepartment.Text = mydata.Rows(count).Item("Department").ToString



                        End With

                    End With

                Next





            End With

            For x As Integer = 0 To dgvNewInventory.Rows.Count
                txtTotalItems.Text = x.ToString
            Next

            sumx()

        Catch ex As Exception

        End Try


    End Sub


    Private Sub dgvNewInventory_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNewInventory.CellEndEdit

        Try
            For Each r As DataGridViewRow In dgvNewInventory.Rows

                If r.Cells(5).Value = "" And r.Cells(6).Value = "" Then

                Else



                    r.Cells(7).Value = r.Cells(5).Value * r.Cells(6).Value
                End If
            Next

            If CDec(dgvNewInventory.SelectedRows(0).Cells(6).Value) <= "4999.99" Then
                dgvNewInventory.SelectedRows(0).Cells(10).Value = "ICS LOW VALUE"
            ElseIf CDec(dgvNewInventory.SelectedRows(0).Cells(6).Value) >= "5000.00" And CDec(dgvNewInventory.SelectedRows(0).Cells(6).Value) < "49000.00" Then
                dgvNewInventory.SelectedRows(0).Cells(10).Value = "ICS HIGH VALUE"
            ElseIf CDec(dgvNewInventory.SelectedRows(0).Cells(6).Value) >= "49001.01" Then
                dgvNewInventory.SelectedRows(0).Cells(10).Value = "PAR"
            Else
                dgvNewInventory.SelectedRows(0).Cells(10).Value = "ERROR"
            End If
        Catch ex As Exception

        End Try

        total_amount()

    End Sub




    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click



        For i As Integer = (Me.warehouse_details.Controls.Count - 1) To 0 Step -1
            If Me.warehouse_details.Controls(i).Text.ToString = "" Then
                MsgBox("Please leave no blank in each entry", MsgBoxStyle.Information)
                Me.warehouse_details.Controls(i).Focus()
                Exit Sub
            End If
        Next i

        If dgvNewInventory.Rows.Count = 0 Then
            MsgBox("Please Put Items Before Place Order!!!")

        Else

            If inventory_state.Text = "Add" Then

                Dim po_number As String = txtPONUMBER.Text
                Dim y As Integer
                For x As Integer = 0 To CGSO_Main_Form.dgvInventory.Rows.Count - 1
                    If po_number.ToString = CGSO_Main_Form.dgvInventory.Item(1, x).Value.ToString Then
                        y = 1
                        Exit For
                    Else
                        y = 0
                    End If
                Next

                If y = 1 Then

                    MsgBox("These Purchase Order are already Exist! Please Edit or Delete the Existing PO Number!", MsgBoxStyle.Information)
                Else
                    save_inventory()
                End If

            ElseIf inventory_state.Text = "View/Update" Then
                MsgBox("Viewing Purposes Only!")

            ElseIf inventory_state.Text = "Edit/Update" Then
                save_inventory_update()

            End If

        End If




    End Sub


    Public Sub save_inventory()

        'Dim counter As Integer = 0

        'Dim out As Date = po_date.Value

        'Dim mydatapo As DataTable = myquery.runQuery("SELECT max(Inv_ID) from tbl_inventory")

        'Dim id As Integer = mydatapo.Rows(0).Item(0) + 1

        'Dim warehouse_number As String = Category.ToString & "-" & "SYS" & "" & Now.Year & "" & Now.Month & "" & id.ToString


        'clean_data



        With myquery
            connect(myquery)
            .setTable("tbl_inventory")

            .addValue(txtDepartment.Text.ToString, "Department")

            .addValue(txtPONUMBER.Text.ToString, "PO_Number")
            .addValue(txtPRNUMBER.Text.ToString, "PR_Number")

            .addValue(combo_category.Text.ToString, "Category")
            .addValue(combo_mode.Text.ToString, "Mode_of_Procurement")
            .addValue(combo_supplier.Text.ToString, "Supplier_Name")
            .addValue(combo_supplier_address.Text.ToString, "Supplier_Address")
            .addValue(purpose.Text.ToString, "Purpose")
            .addValue(txtSource.Text.ToString, "Source_of_Fund")
            .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
            .addValue(frmDashboard.stbName.Text, "End_User")
            .addValue(txtInitial.Text, "Initial")
            .addValue(Date_Entry.Text.ToString, "Date_Entry")
            .addValue(po_date.Text.ToString, "PO_Date")
            .runInsert()


        End With


        For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
            With myquery
                connect(myquery)
                .setTable("tbl_inventory_items_details")

                'datagrid'

                .addValue(txtPONUMBER.Text.ToString, "PO_Number")
                If dgvNewInventory.Item(1, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(1, i).Value, "Inv_ID")
                End If
                If dgvNewInventory.Item(2, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(2, i).Value, "Item_ID")
                End If
                If dgvNewInventory.Item(3, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(3, i).Value, "Unit")
                End If
                If dgvNewInventory.Item(4, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(4, i).Value, "Items_Description")
                End If
                If dgvNewInventory.Item(5, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(5, i).Value, "Quantity")
                End If
                If dgvNewInventory.Item(6, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(6, i).Value, "Unit_Cost")
                End If
                If dgvNewInventory.Item(7, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(7, i).Value, "Total_Cost")
                End If
                If dgvNewInventory.Item(8, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(8, i).Value, "Serial_Number")
                End If
                If dgvNewInventory.Item(9, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(9, i).Value, "Tag_Number")
                End If
                If dgvNewInventory.Item(10, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(10, i).Value, "Inventory_Category")
                End If
                If dgvNewInventory.Item(11, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(11, i).Value, "PAR_ICS_Number")
                End If
                If dgvNewInventory.Item(12, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(12, i).Value, "Accountable_Officer")
                End If
                If dgvNewInventory.Item(13, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(13, i).Value, "Position")
                End If
                If dgvNewInventory.Item(14, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(14, i).Value, "Location")
                End If
                If dgvNewInventory.Item(15, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(15, i).Value, "Classification")
                End If
                If dgvNewInventory.Item(16, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(16, i).Value, "Remarks")
                End If
                If dgvNewInventory.Item(17, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(17, i).Value, "Condition")
                End If
                If dgvNewInventory.Item(18, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(18, i).Value, "NumberSeries")
                End If
                If dgvNewInventory.Item(20, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(20, i).Value, "Estimated_Life")
                End If


                .addValue(Date_Entry.Text.ToString, "Date_Entry")
                .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")

                .addValue(frmDashboard.stbName.Text, "End_User")

                .runInsert()


            End With

        Next



        MsgBox("Succesfully Added!")


        Me.Close()
        CGSO_Main_Form.form_load_inventory()

    End Sub

    Public Sub save_inventory_update()

        With myquery
            connect(myquery)

            .setTable("tbl_inventory")
            .addValue("1", "isDeleted")
            .runUpdate("PO_Number = '" & txtPONUMBER.Text & "'")

        End With

        With myquery
            connect(myquery)
            .setTable("tbl_inventory")

            .addValue(txtDepartment.Text.ToString, "Department")

            .addValue(txtPONUMBER.Text.ToString, "PO_Number")
            .addValue(txtPRNUMBER.Text.ToString, "PR_Number")

            .addValue(combo_category.Text.ToString, "Category")
            .addValue(combo_mode.Text.ToString, "Mode_of_Procurement")
            .addValue(combo_supplier.Text.ToString, "Supplier_Name")
            .addValue(combo_supplier_address.Text.ToString, "Supplier_Address")
            .addValue(purpose.Text.ToString, "Purpose")
            .addValue(txtSource.Text.ToString, "Source_of_Fund")
            .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")
            .addValue(frmDashboard.stbName.Text, "End_User")
            .addValue(txtInitial.Text, "Initial")
            .addValue(Date_Entry.Text.ToString, "Date_Entry")
            .addValue(po_date.Text.ToString, "PO_Date")
            .runInsert()


        End With


        With myquery
            connect(myquery)

            .setTable("tbl_inventory_items_details")
            .addValue("1", "isDeleted")
            .runUpdate("PO_Number = '" & txtPONUMBER.Text & "'")

        End With

        For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
            With myquery
                connect(myquery)
                .setTable("tbl_inventory_items_details")

                'datagrid'

                .addValue(txtPONUMBER.Text.ToString, "PO_Number")
                'If dgvNewInventory.Item(1, i).Value <> Nothing Then
                '    .addValue(dgvNewInventory.Item(1, i).Value, "Inv_ID")
                'End If
                If dgvNewInventory.Item(2, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(2, i).Value, "Item_ID")
                End If
                If dgvNewInventory.Item(3, i).Value <> Nothing Then

                    .addValue(dgvNewInventory.Item(3, i).Value, "Unit")
                End If
                If dgvNewInventory.Item(4, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(4, i).Value, "Items_Description")
                End If
                If dgvNewInventory.Item(5, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(5, i).Value, "Quantity")
                End If
                If dgvNewInventory.Item(6, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(6, i).Value, "Unit_Cost")
                End If
                If dgvNewInventory.Item(7, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(7, i).Value, "Total_Cost")
                End If
                If dgvNewInventory.Item(8, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(8, i).Value, "Serial_Number")
                End If
                If dgvNewInventory.Item(9, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(9, i).Value, "Tag_Number")
                End If
                If dgvNewInventory.Item(10, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(10, i).Value, "Inventory_Category")
                End If
                If dgvNewInventory.Item(11, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(11, i).Value, "PAR_ICS_Number")
                End If
                If dgvNewInventory.Item(12, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(12, i).Value, "Accountable_Officer")
                End If
                If dgvNewInventory.Item(13, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(13, i).Value, "Position")
                End If
                If dgvNewInventory.Item(14, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(14, i).Value, "Location")
                End If
                If dgvNewInventory.Item(15, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(15, i).Value, "Classification")
                End If
                If dgvNewInventory.Item(16, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(16, i).Value, "Remarks")
                End If
                If dgvNewInventory.Item(17, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(17, i).Value, "Condition")
                End If
                If dgvNewInventory.Item(18, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(18, i).Value, "NumberSeries")
                End If
                If dgvNewInventory.Item(20, i).Value <> Nothing Then
                    .addValue(dgvNewInventory.Item(20, i).Value, "Estimated_Life")
                End If

                .addValue(Date_Entry.Text.ToString, "Date_Entry")
                .addValue(Now.Year & "-" & Now.Month & "-" & Now.Day & "-" & Now.Hour & "-" & Now.Minute & "-" & Now.Second, "Date_Created")

                .addValue(frmDashboard.stbName.Text, "End_User")

                .runInsert()


            End With

        Next



        MsgBox("Succesfully Updated!")


        Me.Close()
        CGSO_Main_Form.form_load_inventory()

    End Sub


    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        Try
            Dim z As Integer = dgvNewInventory.CurrentRow.Index
            dgvNewInventory.Rows.RemoveAt(z)
            For x As Integer = 0 To dgvNewInventory.Rows.Count
                txtTotalItems.Text = x.ToString
            Next
            sumx()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub sumx()
        'Total sum of total of the Items'
        Try

            'If dgvPO.Rows(i).Cells(3).Value = "" And dgvPO.Rows(i).Cells(4).Value = "" Then
            '    total += dgvPO.Rows(i).Cells(5).Value
            'Else


            Dim total As Decimal = 0
            For i As Decimal = 0 To dgvNewInventory.RowCount - 1

                total += dgvNewInventory.Rows(i).Cells(7).Value




            Next
            txtSubTotal.Text = Decimal.Parse(total).ToString("N2")
        Catch ex As Exception

        End Try





    End Sub

    Private Sub AddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRow.Click
        If dgvNewInventory.Rows.Count > 0 Then
            Dim i As Integer
            i = dgvNewInventory.SelectedCells.Item(0).RowIndex + 1
            dgvNewInventory.Rows.Insert(i)
        Else
            dgvNewInventory.Rows.Add()

        End If
    End Sub

    Private Sub DuplicateRowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DuplicateRowToolStripMenuItem.Click
        ' Check if there is a selected row
        If dgvNewInventory.SelectedRows.Count > 0 Then
            ' Get the index of the selected row
            Dim rowIndex As Integer = dgvNewInventory.SelectedRows(0).Index

            ' Clone the selected row
            Dim newRow As DataGridViewRow = DirectCast(dgvNewInventory.Rows(rowIndex).Clone(), DataGridViewRow)
            For Each cell As DataGridViewCell In dgvNewInventory.Rows(rowIndex).Cells
                newRow.Cells(cell.ColumnIndex).Value = cell.Value
            Next

            ' Add the duplicated row to the DataGridView
            dgvNewInventory.Rows.Add(newRow)
        End If
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If MsgBox("Are you sure you want to Delete this PO_Number = " & txtPONUMBER.Text & "? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            cgso_login.OKEDIT.Visible = False
            cgso_login.OKDELETE.Visible = True
            cgso_login.OKDELETE.Focus()
            cgso_login.OKDELETE.Select()

            cgso_login.ShowDialog()
        End If

    End Sub


    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        cgso_login.OKDELETE.Visible = False
        cgso_login.OKEDIT.Visible = True
        cgso_login.OKEDIT.Focus()
        cgso_login.OKEDIT.Select()

        cgso_login.ShowDialog()
    End Sub



    Public Sub cgso_data()
        'Signaturies of GSO
        Dim cgso_data As DataTable

        connect(myquery)
        With myquery
            connect(myquery)
            cgso_data = .runQuery("SELECT * FROM ref_signaturies WHERE id = 6")

        End With
        For Each mydatarow In cgso_data.Rows
            With mydatarow
                txtcgsoofficer.Text = CStr(.item("Signaturies"))
                txtcgsoposition.Text = CStr(.item("Position"))


            End With
        Next



    End Sub


    Private Sub btnPrintTagForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintTagForm.Click


        Dim dt As New DataTable
        Dim Inventory_label_Report As New rpt_Viewer 'vb viewer
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing

        rpTitle = "Inventory Tag"


        'Dim ds As New DataSet1
        'Dim ms As New System.IO.MemoryStream
        'myPic.Image.Save(ms, myPic.Image.RawFormat)
        'Dim byt() As Byte = ms.ToArray
        'ds.DataTable1.Rows.Add(byt)


        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Inventory_Label

        rptDoc.SetParameterValue("PO_Number", txtPONUMBER.Text)

        rptDoc.SetParameterValue("Office", txtDepartment.Text)
        rptDoc.SetParameterValue("Items_Description", dgvNewInventory.SelectedRows(0).Cells(4).Value)
        rptDoc.SetParameterValue("Date_Acquired", Date_Entry.Text)

        rptDoc.SetParameterValue("Quantity", dgvNewInventory.SelectedRows(0).Cells(5).Value)
        rptDoc.SetParameterValue("Condition", dgvNewInventory.SelectedRows(0).Cells(17).Value)
        rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
        '
        ' rptDoc.SetParameterValue("Acquisition_Cost", dgvNewInventory.SelectedRows(0).Cells(6).Value)

        ' Retrieve the value from the DataGridView cell
        Dim cellValue As Decimal = Convert.ToDecimal(dgvNewInventory.SelectedRows(0).Cells(6).Value)

        ' Format the value as a string with two decimal places
        Dim formattedValue As String = cellValue.ToString("N2")

        ' Set the formatted value as a parameter for rptDoc
        rptDoc.SetParameterValue("Acquisition_Cost", formattedValue)

        rptDoc.SetParameterValue("Serial_Number", dgvNewInventory.SelectedRows(0).Cells(8).Value)



        rptDoc.SetParameterValue("CGSO_OFFICER", txtcgsoofficer.Text)
        rptDoc.SetParameterValue("Position", txtcgsoposition.Text)
        rptDoc.SetParameterValue("BARCODE", txtEncoded.Text)

        rptDoc.SetParameterValue("Unit", dgvNewInventory.SelectedRows(0).Cells(3).Value)
        rptDoc.SetParameterValue("Property_Number", dgvNewInventory.SelectedRows(0).Cells(9).Value)




        With Inventory_label_Report
            .blnDataSource = True
            .ReportSource = rptSource
            .ReportPath = crFile
            .ReportTitle = rpTitle
            .crptViewer.ReportSource = rptDoc
            .WindowState = FormWindowState.Maximized
            .crptViewer.Zoom(300)
            .ShowDialog()
            .crptViewer.Refresh()
            .Dispose()


        End With


    End Sub


    Private Sub AddSubDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddSubDetailsToolStripMenuItem.Click
        With CGSO_Inventory_Sub_Details
            .ShowDialog()
        End With
    End Sub




    Private Sub btnPAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPAR.Click
        txtPARICSNumber.Text = dgvNewInventory.SelectedRows(0).Cells(11).Value
        If dgvNewInventory.SelectedRows(0).Cells(11).Value <> "" Then
            If MsgBox("Are you sure you want to REPRINT this PARICS NUMBER = " & txtPARICSNumber.Text & "? " & vbCrLf & vbCrLf & "YES = REPRINT" & vbCrLf & "NO = OVERWRITE", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirmation") = MsgBoxResult.Yes Then
                reprint_PAR()



            ElseIf MsgBoxResult.No Then
                MsgBox("You Choose No. It will Overwrite the PARICS NUMBER!")
                MsgBox("Disabled! Contact your Administrator!")
                'print_PAR()


            End If
        Else
            print_PAR()

        End If
        '    total_selected()
            
    End Sub

    Public Sub print_PAR()
        Try

            Dim mydatapar As DataTable = myquery.runQuery("SELECT max(PAR_ID) from auto_parnew")

            Dim id As Integer = mydatapar.Rows(0).Item(0) + 1

            Dim parnumber As String = "PAR" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
            Dim itemid As String = ""



            Dim frmCGSONumber As New CGSO_NUMBER
            With frmCGSONumber
                .paricsnumbergenerated = parnumber
                .ShowDialog()
            End With

            txtPARICSNumber.Text = parnumber



            Dim PARGeneratorReport As New rptViewerPAR
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            '    rpTitle = "Acknowledgment Receipt for Equipment"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            ' mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_PAR_ARE_LONG_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("PAR_Number", parnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With PARGeneratorReport
                .par = "OVERWRITE"
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
        Catch ex As Exception


        End Try

    End Sub

    Public Sub reprint_PAR()
        Try




            Dim PARGeneratorReport As New rptViewerPAR
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            '    rpTitle = "Acknowledgment Receipt for Equipment"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            ' mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_PAR_ARE_LONG_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("PAR_Number", dgvNewInventory.SelectedRows(0).Cells(11).Value)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With PARGeneratorReport
                .par = "REPRINT"
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
        Catch ex As Exception


        End Try
    End Sub

    Public Sub RENEW_PAR()
       



        Try

         Dim parnumber As String = dgvNewInventory.SelectedRows(0).Cells(11).Value & "-R" & Now.Year


            Dim frmCGSONumber As New CGSO_NUMBER
            With frmCGSONumber
                .paricsnumbergenerated = parnumber
                .ShowDialog()
            End With

            txtPARICSNumber.Text = parnumber



            Dim PARGeneratorReport As New rptViewerPAR
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            '    rpTitle = "Acknowledgment Receipt for Equipment"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            ' mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_PAR_ARE_LONG_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("PAR_Number", parnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With PARGeneratorReport
                .par = "RENEW"
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
        Catch ex As Exception


        End Try
    End Sub
    Private Sub btnICSHighValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICSHighValue.Click
        Try

            Dim mydataics As DataTable = myquery.runQuery("SELECT max(ICS_ID) from auto_icsnew")

            Dim id As Integer = mydataics.Rows(0).Item(0) + 1

            Dim icsnumber As String = "ICS" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
            Dim itemid As String = ""



            Dim frmCGSONumber As New CGSO_NUMBER
            With frmCGSONumber
                .paricsnumbergenerated = icsnumber
                .ShowDialog()
            End With

            txtPARICSNumber.Text = icsnumber

            '    total_selected()

            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")


            rpTitle = "Inventory Custodian Slip High Value"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_High_Value_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("ICS_Number", icsnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)

            With ICSGeneratorReport
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnICSLowValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICSLowValue.Click

        Dim mydataics As DataTable = myquery.runQuery("SELECT max(ICS_ID) from auto_icsnew")

        Dim id As Integer = mydataics.Rows(0).Item(0) + 1

        Dim icsnumber As String = "ICS" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
        Dim itemid As String = ""




        Dim frmCGSONumber As New CGSO_NUMBER
        With frmCGSONumber
            .paricsnumbergenerated = icsnumber
            .ShowDialog()
        End With


        txtPARICSNumber.Text = icsnumber

        Try

            '    total_selected()

            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            rpTitle = "Inventory Custodian Slip Low Value"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
               ' MsgBox(dgvNewInventory.Rows(i).Cells(1).Value.ToString)
            Next



            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_Low_Value_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)



            rptDoc.SetParameterValue("Initial", txtInitial.Text)

            rptDoc.SetParameterValue("ICS_Number", icsnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With ICSGeneratorReport
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvNewInventory_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNewInventory.CellClick
        'Barcode Preview'

        Try

            'lblCost.Text = Decimal.Parse(dgvTag.ActiveRow.Cells(12).Value).ToString("N2")
            'lblCost.Text = dgvTag.ActiveRow.Cells(12).Value

            ' dateoftag.Text = dgvTag.ActiveRow.Cells(8).Value

            txtEncoded.Text = fontencoder.Code128(dgvNewInventory.SelectedRows(0).Cells(11).Value)

            txtBarcode.Text = txtEncoded.Text
            txtBarcode.Font = New System.Drawing.Font("IdAutomationSC128S DEMO", 18, FontStyle.Regular)
        Catch ex As Exception

        End Try

        total_amount()
    End Sub

    Private Sub dgvNewInventory_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvNewInventory.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
            dgvNewInventory.CurrentCell = dgvNewInventory(e.ColumnIndex, e.RowIndex)
            ' Maybe modify the text on your context menu options here?
            total_amount()
        End If


    End Sub

    Private Sub dgvNewInventory_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvNewInventory.EditingControlShowing
        If sender Is Nothing OrElse e Is Nothing Then
            Return
        End If


        'AutoSuggest in Datagrid'
        If dgvNewInventory.Rows.Count > 0 Then

            Try
                '  If ics_state.Text = "View" Then
                'sourceload()
                Select Case dgvNewInventory.Columns(dgvNewInventory.CurrentCell.ColumnIndex).Name
                    Case "Accountable_Officer"

                        With DirectCast(e.Control, TextBox)
                            .CharacterCasing = CharacterCasing.Upper
                            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                            .AutoCompleteSource = AutoCompleteSource.CustomSource
                            .AutoCompleteCustomSource = scAutoComplete1
                        End With


                    Case "Location"


                        With DirectCast(e.Control, TextBox)
                            .CharacterCasing = CharacterCasing.Upper
                            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                            .AutoCompleteSource = AutoCompleteSource.CustomSource
                            .AutoCompleteCustomSource = scAutoComplete2
                        End With


                    Case "Classification"


                        With DirectCast(e.Control, TextBox)
                            .CharacterCasing = CharacterCasing.Upper
                            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                            .AutoCompleteSource = AutoCompleteSource.CustomSource
                            .AutoCompleteCustomSource = scAutoComplete3
                        End With

                    Case "Position"


                        With DirectCast(e.Control, TextBox)
                            .CharacterCasing = CharacterCasing.Upper
                            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                            .AutoCompleteSource = AutoCompleteSource.CustomSource
                            .AutoCompleteCustomSource = scAutoComplete4
                        End With


                    Case Else


                        With DirectCast(e.Control, TextBox)
                            .CharacterCasing = CharacterCasing.Upper
                            .AutoCompleteMode = AutoCompleteMode.None
                        End With
                End Select

                ' End If
                ' End If
            Catch ex As Exception

            End Try
        Else

        End If

        total_amount()
    End Sub


    Private Sub chkPrintAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrintAll.CheckedChanged
        If chkPrintAll.Checked = True Then

            For x As Integer = 0 To dgvNewInventory.Rows.Count - 1

                dgvNewInventory.Rows(x).Cells(0).Value = True


            Next
        ElseIf chkPrintAll.Checked = False Then

            For x As Integer = 0 To dgvNewInventory.Rows.Count - 1

                dgvNewInventory.Rows(x).Cells(0).Value = False


            Next

        End If

        total_amount()
    End Sub

    Private Sub btnICSLowValueBYLOT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICSLowValueBYLOT.Click

        Dim mydataics As DataTable = myquery.runQuery("SELECT max(ICS_ID) from auto_icsnew")

        Dim id As Integer = mydataics.Rows(0).Item(0) + 1

        Dim icsnumber As String = "ICS" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
        Dim itemid As String = ""




        Dim frmCGSONumber As New CGSO_NUMBER
        With frmCGSONumber
            .paricsnumbergenerated = icsnumber
            .ShowDialog()
        End With


        txtPARICSNumber.Text = icsnumber

        Try

            '    total_selected()

            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            rpTitle = "Inventory Custodian Slip Low Value By LOT"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
                ' MsgBox(dgvNewInventory.Rows(i).Cells(1).Value.ToString)
            Next



            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, NumberSeries, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_High_Value_Report_BY_LOT
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)

            rptDoc.SetParameterValue("ICS_Number", icsnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With ICSGeneratorReport
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
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnICSHighValueBYLOT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICSHighValueBYLOT.Click
        Try

            Dim mydataics As DataTable = myquery.runQuery("SELECT max(ICS_ID) from auto_icsnew")

            Dim id As Integer = mydataics.Rows(0).Item(0) + 1

            Dim icsnumber As String = "ICS" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
            Dim itemid As String = ""



            Dim frmCGSONumber As New CGSO_NUMBER
            With frmCGSONumber
                .paricsnumbergenerated = icsnumber
                .ShowDialog()
            End With

            txtPARICSNumber.Text = icsnumber

            '    total_selected()

            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")


            rpTitle = "Inventory Custodian Slip High Value by LOT"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, NumberSeries, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_High_Value_Report_BY_LOT
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("ICS_Number", icsnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With ICSGeneratorReport
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPARBYLOT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPARBYLOT.Click
        Try

            '    total_selected()
            Dim mydatapar As DataTable = myquery.runQuery("SELECT max(PAR_ID) from auto_parnew")

            Dim id As Integer = mydatapar.Rows(0).Item(0) + 1

            Dim parnumber As String = "PAR" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
            Dim itemid As String = ""



            Dim frmCGSONumber As New CGSO_NUMBER
            With frmCGSONumber
                .paricsnumbergenerated = parnumber
                .ShowDialog()
            End With
            txtPARICSNumber.Text = parnumber


            Dim PARGeneratorReport As New rptViewerPAR
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            rpTitle = "Property Acknowledgement Receipt by LOT"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, NumberSeries, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            ' mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_PAR_ARE_LONG_Report_BY_LOT
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("PAR_Number", parnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With PARGeneratorReport
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
        Catch ex As Exception


        End Try
    End Sub

    Private Sub GunaButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim mydataics As DataTable = myquery.runQuery("SELECT max(ICS_ID) from auto_icsnew")

        Dim id As Integer = mydataics.Rows(0).Item(0) + 1

        Dim icsnumber As String = "ICS" & Now.Year & "" & Now.Month & "" & id.ToString("00000")
        Dim itemid As String = ""




        Dim frmCGSONumber As New CGSO_NUMBER
        With frmCGSONumber
            .paricsnumbergenerated = icsnumber
            .ShowDialog()
        End With


        txtPARICSNumber.Text = icsnumber

        Try

            '    total_selected()

            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet



            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")

            rpTitle = "Inventory Custodian Slip Low Value"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
                ' MsgBox(dgvNewInventory.Rows(i).Cells(1).Value.ToString)
            Next



            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, NumberSeries FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_Low_Value_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)

            rptDoc.SetParameterValue("ICS_Number", icsnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With ICSGeneratorReport
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
        Catch ex As Exception

        End Try
    End Sub

 
    Private Sub btnInventoryTransferForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventoryTransferForm.Click
        With Transfer_Form
            .lblTransfer.Text = "Inventory Transfer Report"
            .btnITRSave.Visible = True
            .btnPTRSave.Visible = False
            .ShowDialog()
        End With
     
    End Sub

    Private Sub btnPropertyTransferForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPropertyTransferForm.Click
        With Transfer_Form
            .lblTransfer.Text = "Property Transfer Report"
            .btnITRSave.Visible = False
            .btnPTRSave.Visible = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnPrintSmallTagForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSmallTagForm.Click


        'Print Small GSO Inventory Tagging'


        Dim dt As New DataTable
        Dim PurchaseRequestGeneratorReport As New rpt_Viewer 'vb viewer
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing

        rpTitle = "Small ID Inventory"

        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Inventory_Label_3
        Try

        rptDoc.SetParameterValue("BARCODE", txtEncoded.Text)
       


        Catch ex As Exception

        End Try
        With PurchaseRequestGeneratorReport
            .blnDataSource = True
            .ReportSource = rptSource
            .ReportPath = crFile
            .ReportTitle = rpTitle
            .crptViewer.ReportSource = rptDoc
            .WindowState = FormWindowState.Maximized
            .crptViewer.Zoom(300)
            .ShowDialog()
            .crptViewer.Refresh()
            .Dispose()


        End With
    End Sub

    Private Sub btnPropertyCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            '    total_selected()

            Dim PropertyCardGenerator As New rptViewerPropertyCard
            Dim crfile As String = Nothing
            Dim rptitle As String = Nothing
            Dim rptsource As String = Nothing
            Dim myds As New DataSet



            rptitle = "Property Card"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'else
                    '    msgbox("please check items to print!")
                    '    exit sub
                End If
                ' msgbox(dgvnewinventory.rows(i).cells(1).value.tostring)
            Next



            mydata = myquery.runQuery("select inv_id, po_number, item_id, items_description, unit, quantity, unit_cost, total_cost, tag_number, serial_number, inventory_category, par_ics_number, accountable_officer, ti.position, location, classification, remarks, ti.condition, date_entry, numberseries, estimated_life from tbl_inventory_items_details ti where po_number = '" & txtPONUMBER.Text & "' and inv_id in (" & sql_string_string & ") and isdeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myds.Tables.Add(mydata)

            mydata = myquery.runQuery("select sub_id, inv_id, po_number, items_description, unit_cost, serial_number, tag_number, accountable_officer, location, remarks, ts.condition, date_entry from tbl_inventory_sub_details ts where inv_id in (select inv_id from tbl_inventory_items_details where inv_id in (" & sql_string_string & ") and isdeleted = 0) and isdeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myds.Tables.Add(mydata)



            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New Property_Card
            rptdoc.SetDataSource(myds)



            rptdoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)


            rptdoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptdoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)
            rptdoc.SetParameterValue("Location", dgvNewInventory.SelectedRows(0).Cells(14).Value)
            rptdoc.SetParameterValue("initial", txtInitial.Text)
            '  rptdoc.SetParameterValue("total_amount", txtTotal_Amount.Text)


            With PropertyCardGenerator
                .blnDataSource = True
                .ReportSource = rptsource
                .ReportPath = crfile
                .ReportTitle = rptitle
                .crptViewer.ReportSource = rptdoc
                .WindowState = FormWindowState.Maximized
                .crptViewer.Zoom(75)
                .ShowDialog()
                .crptViewer.Refresh()
                .Dispose()
            End With
        Catch ex As Exception
        End Try
    End Sub



    Private Sub btnPropertyCardSemiEXP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPropertyCardSemiEXP.Click

        Dim dt As New DataTable
        Dim PropertyCardGenerator As New rptViewerPropertyCard 'vb viewer
        Dim crFile As String = Nothing
        Dim rpTitle As String = Nothing
        Dim rptSource As String = Nothing

        rpTitle = "Property_Card"


        'Dim ds As New DataSet1
        'Dim ms As New System.IO.MemoryStream
        'myPic.Image.Save(ms, myPic.Image.RawFormat)
        'Dim byt() As Byte = ms.ToArray
        'ds.DataTable1.Rows.Add(byt)


        Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptDoc = New Property_Card_Semi_Expendable


        With PropertyCardGenerator
            .blnDataSource = True
            .ReportSource = rptSource
            .ReportPath = crFile
            .ReportTitle = rpTitle
            .crptViewer.ReportSource = rptDoc
            .WindowState = FormWindowState.Maximized
            .crptViewer.Zoom(300)
            .ShowDialog()
            .crptViewer.Refresh()
            .Dispose()

        End With
    End Sub

    Private Sub btnPARrenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPARrenew.Click

        If dgvNewInventory.SelectedRows.Count = 0 Then
            MsgBox("Please select a record first.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtPARICSNumber.Text = dgvNewInventory.SelectedRows(0).Cells(11).Value

        If txtPARICSNumber.Text <> "" Then

            Dim result As MsgBoxResult = MsgBox( _
                "Are you sure you want to process this PARICS NUMBER = " & txtPARICSNumber.Text & "?" & vbCrLf & vbCrLf & _
                "YES = RENEW PAR" & vbCrLf & _
                "NO = REPRINT PAR", _
                MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question, _
                "Confirmation" _
            )

            Select Case result
                Case MsgBoxResult.Yes
                    RENEW_PAR()

                Case MsgBoxResult.No
                    REPRINT_PAR()

                Case MsgBoxResult.Cancel
                    ' Do nothing
            End Select

        Else
            MsgBox("Invalid PARICS Number.", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub printICSHighValueRenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printICSHighValueRenew.Click

        If dgvNewInventory.SelectedRows.Count = 0 Then
            MsgBox("Please select a record first.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtPARICSNumber.Text = dgvNewInventory.SelectedRows(0).Cells(11).Value

        If txtPARICSNumber.Text <> "" Then

            Dim result As MsgBoxResult = MsgBox( _
                "Are you sure you want to process this PARICS NUMBER (HIGH VALUE ICS) = " & txtPARICSNumber.Text & "?" & vbCrLf & vbCrLf & _
                "YES = RENEW HIGH VALUE ICS" & vbCrLf & _
                "NO = REPRINT HIGH VALUE ICS", _
                MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question, _
                "Confirmation" _
            )

            Select Case result
                Case MsgBoxResult.Yes
                    ICS_HIGH_VALUE_RENEW()

                Case MsgBoxResult.No
                    ICS_HIGH_VALUE_REPRINT()

                Case MsgBoxResult.Cancel
                    ' Do nothing
            End Select

        Else
            MsgBox("Invalid PARICS Number.", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Public Sub ICS_HIGH_VALUE_REPRINT()




        Try

            
            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet




            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")


            rpTitle = "Inventory Custodian Slip High Value"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_High_Value_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("ICS_Number", dgvNewInventory.SelectedRows(0).Cells(11).Value)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With ICSGeneratorReport
                .ics = "RENEW"
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
        Catch ex As Exception


        End Try



    End Sub

    Public Sub ICS_HIGH_VALUE_RENEW()




        Try

            Dim icsnumber As String = dgvNewInventory.SelectedRows(0).Cells(11).Value & "-R" & Now.Year


            Dim frmCGSONumber As New CGSO_NUMBER
            With frmCGSONumber
                .paricsnumbergenerated = icsnumber
                .ShowDialog()
            End With

            txtPARICSNumber.Text = icsnumber


            Dim ICSGeneratorReport As New rptViewerICS
            Dim crFile As String = Nothing
            Dim rpTitle As String = Nothing
            Dim rptSource As String = Nothing
            Dim myDs As New DataSet




            '    'rptSource = ("C:\Program Files\City Gov't of Malaybalay\Purchase Request Setup\Crystal reports\crptPurchaseRequest.xml")

            '    crFile = ("Forms\Crystal reports\PAR_ARE Report.rpt")


            rpTitle = "Inventory Custodian Slip High Value"



            Dim sql_string_string As String
            Dim _row = 0
            For i As Integer = 0 To dgvNewInventory.Rows.Count - 1
                If dgvNewInventory.Rows(i).Cells(0).Value = True Then
                    If _row = 0 Then
                        sql_string_string = sql_string_string & dgvNewInventory.Rows(i).Cells(1).Value.ToString
                    ElseIf _row > 0 Then
                        sql_string_string = sql_string_string & "," & "" & dgvNewInventory.Rows(i).Cells(1).Value.ToString & ""
                    End If
                    _row += 1
                    'Else
                    '    MsgBox("Please Check Items to Print!")
                    '    Exit Sub
                End If
            Next

            mydata = myquery.runQuery("SELECT Inv_ID, PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Tag_Number, Serial_Number, Inventory_Category, PAR_ICS_Number, Accountable_Officer, ti.Position, Location, Classification, Remarks, ti.Condition, Date_Entry, Estimated_Life FROM tbl_inventory_items_details ti WHERE PO_Number = '" & txtPONUMBER.Text & "' AND Inv_ID in (" & sql_string_string & ") and isDeleted = 0")
            mydata.TableName = "tbl_inventory_items_details"
            myDs.Tables.Add(mydata)

            mydata = myquery.runQuery("SELECT sub_id, Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE Inv_ID IN (SELECT Inv_ID FROM tbl_inventory_items_details WHERE Inv_ID IN (" & sql_string_string & ") AND isDeleted = 0) and isDeleted = 0")
            mydata.TableName = "tbl_inventory_sub_details"
            myDs.Tables.Add(mydata)



            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptDoc = New Inventory_ICS_High_Value_Report
            rptDoc.SetDataSource(myDs)



            rptDoc.SetParameterValue("Classification", dgvNewInventory.SelectedRows(0).Cells(15).Value)
            rptDoc.SetParameterValue("Supplier", combo_supplier.Text)
            rptDoc.SetParameterValue("Purpose", purpose.Text)
            rptDoc.SetParameterValue("Source_of_Fund", txtSource.Text)
            rptDoc.SetParameterValue("Remarks", dgvNewInventory.SelectedRows(0).Cells(16).Value)

            rptDoc.SetParameterValue("CGSO_Officer", txtcgsoofficer.Text)
            rptDoc.SetParameterValue("CGSO_Position", txtcgsoposition.Text)

            rptDoc.SetParameterValue("Accountable_Officer", dgvNewInventory.SelectedRows(0).Cells(12).Value)
            rptDoc.SetParameterValue("Accountable_Position", dgvNewInventory.SelectedRows(0).Cells(13).Value)

            rptDoc.SetParameterValue("Initial", txtInitial.Text)
            rptDoc.SetParameterValue("ICS_Number", icsnumber)

            rptDoc.SetParameterValue("Date_Entry", Date_Entry.Text)
            rptDoc.SetParameterValue("Total_Amount", txtTotal_Amount.Text)


            With ICSGeneratorReport
                .ics = "RENEW"
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
        Catch ex As Exception


        End Try



    End Sub

   
End Class
