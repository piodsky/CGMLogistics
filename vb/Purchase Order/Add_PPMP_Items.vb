Public Class Add_PPMP_Items
    Inherits Windows.Forms.Form
    Dim itemid As Integer = 0
    Dim myquery As New MyClassLibrary.MyHelper
    Dim myfetcher As New MyClassLibrary.MyFetcher
    Dim mydata As DataTable
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_Unit_Cost.KeyPress

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            'If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            If e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".") Then
            Else

                MsgBox("Please Input Numeric Data Only!")
                e.Handled = True

            End If
        End If

    End Sub

    Private Sub Add_PPMP_Items_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ppmp_items_status.Text = "Add" Then
            combo_unit.ResetText()
            txt_Unit_Cost.ResetText()

            txtDescription.ResetText()


        ElseIf ppmp_items_status.Text = "Edit/Update" Then

            Dim myquery As New MyClassLibrary.MyHelper



            itemid = PPMP_Items.dgvPPMPItemsList.SelectedRows.Item(0).Cells(1).Value

            With myquery
                connect(myquery)
                mydata = myquery.runQuery("SELECT * FROM tbl_ppmp_bac_items WHERE item_id = '" & itemid.ToString & "'")

                With myfetcher
                    .setDatatable(mydata)
                    .getData(combo_unit.Text, "Unit")
                    .getData(txt_Unit_Cost.Text, "Unit_Cost")
                    .getData(txtDescription.Text, "Items_Description")
                End With

            End With
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim myquery As New MyClassLibrary.MyHelper
        For i As Integer = (Me.items_details.Controls.Count - 1) To 0 Step -1
            If Me.items_details.Controls(i).Text.ToString = "" Then
                MsgBox("Please leave no blank in each entry", MsgBoxStyle.Information)
                Me.items_details.Controls(i).Focus()
                Exit Sub
            End If
        Next i

        If ppmp_items_status.Text = "Edit/Update" Then
            itemid = PPMP_Items.dgvPPMPItemsList.SelectedRows.Item(0).Cells(1).Value
        End If


        With myquery
            connect(myquery)
            .setTable("tbl_ppmp_bac_items")
            .addValue(combo_unit.Text.ToString, "Unit")
            .addValue(txt_Unit_Cost.Text.ToString, "Unit_Cost")
            .addValue(txtDescription.Text.ToString, "Items_Description")

            If ppmp_items_status.Text = "Add" Then
                .runInsert()
                MsgBox("Sucessfully Added!")
            ElseIf ppmp_items_status.Text = "Edit/Update" Then
                myquery.runUpdate("Item_Id = '" & itemid.ToString & "'")
                MsgBox("Sucessfully Updated!")
            End If

        End With
        PPMP_Items.form_load_PPMPItemsList()
        Me.Close()
    End Sub


End Class