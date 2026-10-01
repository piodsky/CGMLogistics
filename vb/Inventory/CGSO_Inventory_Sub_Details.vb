Public Class CGSO_Inventory_Sub_Details
    Inherits Windows.Forms.Form

    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata, mydata_name, mydata_department, mydata_classification As DataTable
    Private scAutoComplete1 As New AutoCompleteStringCollection
    Private scAutoComplete2 As New AutoCompleteStringCollection

  
    Private Sub CGSO_Inventory_Sub_Details_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Date_Entry.Format = DateTimePickerFormat.Custom
        Date_Entry.CustomFormat = "yyyy-MM-dd HH:mm:ss"



        connect(myquery)
        sourceload()
        txtPONumber.Text = CGSO_Inventory.txtPONUMBER.Text
        txtUnit_Cost.Text = CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(6).Value
        txtDesc.Text = CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(4).Value

        txtPONumber.Enabled = False
        txtUnit_Cost.Enabled = False
        txtDesc.Enabled = False

        form_load_subinventory()


        If dgvInventory.Rows.Count > 0 Then

            btnSave.Text = "Update"
        Else
            btnSave.Text = "Save"
        End If

    End Sub

    Public Sub sourceload()
        Dim yearnow As Integer = Year(Now)
        mydata_name = myquery.runQuery("SELECT CONCAT(firstname,' ',LEFT(middlename, 1),'. ',surname)AS `NAME` FROM hrmis.`tblpersonalinformation` WHERE CONCAT(firstname,' ',LEFT(middlename, 1),'. ',surname) IS NOT NULL ORDER BY `Name`")
        mydata_department = myquery.runQuery("SELECT Department FROM ref_department")


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

    

    End Sub


    Public Sub form_load_subinventory()
        dgvInventory.Rows.Clear()


        With myquery
            connect(myquery)

            mydata = .runQuery("SELECT Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number, Accountable_Officer, Location, Remarks, ts.Condition, Date_Entry FROM tbl_inventory_sub_details ts WHERE isDeleted = 0 and Inv_ID = '" & CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(1).Value & "'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvInventory
                    .Rows.Add()
                    With .Rows(count)

                        dgvInventory.Item(0, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        dgvInventory.Item(1, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        dgvInventory.Item(2, count).Value = mydata.Rows(count).Item("Serial_Number").ToString
                        dgvInventory.Item(3, count).Value = mydata.Rows(count).Item("Tag_Number").ToString
                        dgvInventory.Item(4, count).Value = mydata.Rows(count).Item("Accountable_Officer").ToString
                        dgvInventory.Item(5, count).Value = mydata.Rows(count).Item("Location").ToString
                        dgvInventory.Item(6, count).Value = mydata.Rows(count).Item("Remarks").ToString
                        dgvInventory.Item(7, count).Value = mydata.Rows(count).Item("Condition").ToString

            
                    End With

                End With

            Next

        End With

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If dgvInventory.Rows.Count > 0 Then
            update_subinventory()
        Else
            save_subinventory()
        End If

    End Sub

    Public Sub save_subinventory()



        For i As Integer = 0 To dgvInventory.Rows.Count - 1
            With myquery
                connect(myquery)
                .setTable("tbl_inventory_sub_details")

                'datagrid'

                .addValue(CGSO_Inventory.txtPONUMBER.Text.ToString, "PO_Number")
                .addValue(CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(1).Value, "Inv_ID")

                If dgvInventory.Item(0, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(0, i).Value, "Items_Description")
                End If
                If dgvInventory.Item(1, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(1, i).Value, "Unit_Cost")
                End If
                If dgvInventory.Item(2, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(2, i).Value, "Serial_Number")
                End If
                If dgvInventory.Item(3, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(3, i).Value, "Tag_Number")
                End If
                If dgvInventory.Item(4, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(4, i).Value, "Accountable_Officer")
                End If
                If dgvInventory.Item(5, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(5, i).Value, "Location")
                End If
                If dgvInventory.Item(6, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(6, i).Value, "Remarks")
                End If
                If dgvInventory.Item(7, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(7, i).Value, "Condition")
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


    Public Sub update_subinventory()


        With myquery
            connect(myquery)

            .setTable("tbl_inventory_sub_details")
            .addValue("1", "isDeleted")
            .runUpdate("Inv_ID = '" & CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(1).Value & "'")

        End With


        For i As Integer = 0 To dgvInventory.Rows.Count - 1
            With myquery
                connect(myquery)
                .setTable("tbl_inventory_sub_details")

                'datagrid'

                .addValue(CGSO_Inventory.txtPONUMBER.Text.ToString, "PO_Number")
                .addValue(CGSO_Inventory.dgvNewInventory.SelectedRows(0).Cells(1).Value, "Inv_ID")

                If dgvInventory.Item(0, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(0, i).Value, "Items_Description")
                End If
                If dgvInventory.Item(1, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(1, i).Value, "Unit_Cost")
                End If
                If dgvInventory.Item(2, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(2, i).Value, "Serial_Number")
                End If
                If dgvInventory.Item(3, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(3, i).Value, "Tag_Number")
                End If
                If dgvInventory.Item(4, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(4, i).Value, "Accountable_Officer")
                End If
                If dgvInventory.Item(5, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(5, i).Value, "Location")
                End If
                If dgvInventory.Item(6, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(6, i).Value, "Remarks")
                End If
                If dgvInventory.Item(7, i).Value <> Nothing Then
                    .addValue(dgvInventory.Item(7, i).Value, "Condition")
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

    Private Sub AddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRow.Click
        If dgvInventory.Rows.Count > 0 Then
            Dim i As Integer
            i = dgvInventory.SelectedCells.Item(0).RowIndex + 1
            dgvInventory.Rows.Insert(i)
        Else
            dgvInventory.Rows.Add()

        End If
    End Sub

    Private Sub dgvInventory_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvInventory.EditingControlShowing
        If sender Is Nothing OrElse e Is Nothing Then
            Return
        End If


        'AutoSuggest in Datagrid'
        If dgvInventory.Rows.Count > 0 Then

            Try
                '  If ics_state.Text = "View" Then
                'sourceload()
                Select Case dgvInventory.Columns(dgvInventory.CurrentCell.ColumnIndex).Name
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
    End Sub
End Class