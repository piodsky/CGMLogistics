
Public Class CGSO_PO_Select
    Inherits Windows.Forms.Form
    Dim myquery As New MyClassLibrary.MyHelper
    Dim mydata, mydata1 As DataTable
    Dim mydata_ics, mydata_par As DataTable

    Dim mydata_name, mydata_department, mydata_classification As DataTable
    Public scAutoComplete As New AutoCompleteStringCollection
    Public scAutoComplete1 As New AutoCompleteStringCollection
    Public scAutoComplete2 As New AutoCompleteStringCollection
    Public scAutoComplete3 As New AutoCompleteStringCollection
    Private Sub CGSO_PO_Select_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        combo_search.Text = "PO_Number"
    End Sub

    Public Sub form_load_purchase_order_list()
        dgvPO.Rows.Clear()


        With myquery
            connect(myquery)

            mydata = .runQuery("SELECT PO_Number, PR_Number, Department, Supplier_Name, Category, Date_Created FROM tbl_purchase_order WHERE isDeleted = 0")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvPO
                    .Rows.Add()
                    With .Rows(count)

                        dgvPO.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                        dgvPO.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                        dgvPO.Item(2, count).Value = mydata.Rows(count).Item("Department").ToString
                        dgvPO.Item(3, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                        dgvPO.Item(4, count).Value = mydata.Rows(count).Item("Category").ToString
                        dgvPO.Item(5, count).Value = mydata.Rows(count).Item("Date_Created").ToString



                    End With

                End With

            Next



        End With


    End Sub



    Public Sub max_parnew()
        'Dim mydata As DataTable

        '' Generate ICS NUMBER'
        'Dim paricsnumber As Integer
        'Dim year_in_digits = Year(Now)
        'Dim month_in_digits = Month(Now)

        'With myquery
        '    connect(myquery)
        '    mydata = .runQuery("SELECT max(PAR_ICS_Number) as paricsnumber FROM tbl_inventory_items_details")
        'End With

        'For Each mydatarow In mydata.Rows
        '    With mydatarow
        '        paricsnumber = CInt(.Item("paricsnumber"))
        '    End With
        'Next

        '' Increment the paricsnumber by 1 '


        'For i As Integer = 0 To CGSO_Inventory.dgvInventory.Rows.Count - 1
        '    paricsnumber += 1
        '    CGSO_Inventory.dgvInventory.Rows.Item(i).Cells(10).Value = "ICS" & year_in_digits & month_in_digits & paricsnumber.ToString("0000")
        'Next

    End Sub
  
    Private Sub btnUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUse.Click



        CGSO_Inventory.dgvNewInventory.Rows.Clear()

        Dim paricsnumber As Integer
        Dim year_in_digits = Year(Now)
        Dim month_in_digits = Month(Now)
        Dim unitCost As Decimal

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT tp.`PO_Number`, tp.`PR_Number`, tp.`Department`, tp.`Division`, tp.`Section`, tp.`Supplier_Name`, tp.`Supplier_Address`, tp.`Date_Received_Supplier`, tp.`PR_Received_Name`, tp.`Category`, tp.`Date_Received_Supplier`, tp.`Date_COA_Received`, tp.`Date_Approved_PO`, tp.`Remarks`, tp.`Date_PO`, tp.`Purpose`, tp.`Requested_by`, tpd.`Item_ID`, tpd.`Items_Description`, tpd.`Unit`, tpd.`Quantity`, tpd.`Unit_Cost`, tpd.`Total_Cost`, tpd.`NumberSeries` FROM tbl_purchase_order tp INNER JOIN tbl_purchase_order_items_details tpd ON tp.PO_Number = tpd.PO_Number WHERE tp.isDeleted = 0 AND tpd.isDeleted = 0 AND tp.PO_Number = '" & dgvPO.SelectedRows.Item(0).Cells(0).Value & "'")
            mydata_par = .runQuery("SELECT max(inv_ID) as paricsnumber FROM tbl_inventory_items_details")

            paricsnumber = CInt(mydata_par.Rows(0).Item("paricsnumber"))

            For count As Integer = 0 To mydata.Rows.Count - 1
                With CGSO_Inventory.dgvNewInventory
                    .Rows.Add()
                    With .Rows(count)

                        CGSO_Inventory.dgvNewInventory.Item(2, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        CGSO_Inventory.dgvNewInventory.Item(3, count).Value = mydata.Rows(count).Item("Unit").ToString
                        CGSO_Inventory.dgvNewInventory.Item(4, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        CGSO_Inventory.dgvNewInventory.Item(5, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        CGSO_Inventory.dgvNewInventory.Item(6, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        CGSO_Inventory.dgvNewInventory.Item(7, count).Value = mydata.Rows(count).Item("Total_Cost").ToString




                        CGSO_Inventory.txtPONUMBER.Text = mydata.Rows(count).Item("PO_Number").ToString
                        CGSO_Inventory.txtPRNUMBER.Text = mydata.Rows(count).Item("PR_Number").ToString

                        CGSO_Inventory.combo_category.Text = mydata.Rows(count).Item("Category").ToString
                        CGSO_Inventory.txtDepartment.Text = mydata.Rows(count).Item("Department").ToString

                        CGSO_Inventory.combo_supplier.Text = mydata.Rows(count).Item("Supplier_Name").ToString
                        CGSO_Inventory.combo_supplier_address.Text = mydata.Rows(count).Item("Supplier_Address").ToString
                        CGSO_Inventory.po_date.Text = mydata.Rows(count).Item("Date_PO").ToString
                        CGSO_Inventory.purpose.Text = mydata.Rows(count).Item("Purpose").ToString


                        If Not IsDBNull(mydata.Rows(count).Item("Unit_Cost")) Then

                            unitCost = CDec(mydata.Rows(count).Item("Unit_Cost"))

                            If unitCost <= 4999.99 Then
                                ' ...
                                ' CGSO_Inventory.dgvNewInventory.Item(11, count).Value = "ICS" & year_in_digits & month_in_digits & paricsnumber.ToString("00000")
                                ' ...
                                CGSO_Inventory.dgvNewInventory.Item(10, count).Value = "ICS LOW VALUE"
                            ElseIf unitCost >= 5000.0 And unitCost < 49000.0 Then
                                ' ...
                                ' CGSO_Inventory.dgvNewInventory.Item(11, count).Value = "ICS" & year_in_digits & month_in_digits & paricsnumber.ToString("00000")
                                ' ...
                                CGSO_Inventory.dgvNewInventory.Item(10, count).Value = "ICS HIGH VALUE"
                            ElseIf unitCost >= 49001.01 Then
                                ' ...
                                ' CGSO_Inventory.dgvNewInventory.Item(11, count).Value = "PAR" & year_in_digits & month_in_digits & paricsnumber.ToString("00000")
                                ' ...
                                CGSO_Inventory.dgvNewInventory.Item(10, count).Value = "PAR"
                            End If

                            paricsnumber += 1
                        Else
                        End If
                    End With
                End With

            Next

            Try



            Catch ex As Exception

            End Try
        End With

  
     

        For x As Integer = 0 To CGSO_Inventory.dgvNewInventory.Rows.Count
            CGSO_Inventory.txtTotalItems.Text = x.ToString
        Next

        sumx()



    
        Me.Close()

    End Sub

    Public Sub sumx()
        'Total sum of total of the Items'
        Try

            'If dgvPO.Rows(i).Cells(3).Value = "" And dgvPO.Rows(i).Cells(4).Value = "" Then
            '    total += dgvPO.Rows(i).Cells(5).Value
            'Else


            Dim total As Decimal = 0
            For i As Decimal = 0 To CGSO_Inventory.dgvNewInventory.RowCount - 1

                total += CGSO_Inventory.dgvNewInventory.Rows(i).Cells(7).Value




            Next
            CGSO_Inventory.txtSubTotal.Text = Decimal.Parse(total).ToString("N2")
        Catch ex As Exception

        End Try





    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgvPO.Rows.Clear()


        With myquery
            connect(myquery)

            mydata = .runQuery("SELECT PO_Number, PR_Number, Department, Supplier_Name, Category, Date_Created FROM tbl_purchase_order WHERE isDeleted = 0 and " & combo_search.Text.ToString & " LIKE '%" & txtSearchBox.Text.ToString & "%'")
            For count As Integer = 0 To mydata.Rows.Count - 1
                With dgvPO
                    .Rows.Add()
                    With .Rows(count)

                        dgvPO.Item(0, count).Value = mydata.Rows(count).Item("PO_Number").ToString
                        dgvPO.Item(1, count).Value = mydata.Rows(count).Item("PR_Number").ToString
                        dgvPO.Item(2, count).Value = mydata.Rows(count).Item("Department").ToString
                        dgvPO.Item(3, count).Value = mydata.Rows(count).Item("Supplier_Name").ToString
                        dgvPO.Item(4, count).Value = mydata.Rows(count).Item("Category").ToString
                        dgvPO.Item(5, count).Value = mydata.Rows(count).Item("Date_Created").ToString



                    End With

                End With

            Next



        End With
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        form_load_purchase_order_list()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub combo_search_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_search.SelectedIndexChanged
        If combo_search.Text = "VIEW ALL" Then
            txtSearchBox.Visible = False
            form_load_purchase_order_list()
        Else
            txtSearchBox.Visible = True
        End If
    End Sub


    Private Sub dgvPO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPO.CellDoubleClick
        CGSO_Inventory.dgvNewInventory.Rows.Clear()

        Dim paricsnumber As Integer
        Dim year_in_digits = Year(Now)
        Dim month_in_digits = Month(Now)
        Dim unitCost As Decimal

        With myquery
            connect(myquery)
            mydata = .runQuery("SELECT tp.`PO_Number`, tp.`PR_Number`, tp.`Department`, tp.`Division`, tp.`Section`, tp.`Supplier_Name`, tp.`Supplier_Address`, tp.`Date_Received_Supplier`, tp.`PR_Received_Name`, tp.`Category`, tp.`Date_Received_Supplier`, tp.`Date_COA_Received`, tp.`Date_Approved_PO`, tp.`Remarks`, tp.`Date_PO`, tp.`Purpose`, tp.`Requested_by`, tpd.`Item_ID`, tpd.`Items_Description`, tpd.`Unit`, tpd.`Quantity`, tpd.`Unit_Cost`, tpd.`Total_Cost`, tpd.`NumberSeries` FROM tbl_purchase_order tp INNER JOIN tbl_purchase_order_items_details tpd ON tp.PO_Number = tpd.PO_Number WHERE tp.isDeleted = 0 AND tpd.isDeleted = 0 AND tp.PO_Number = '" & dgvPO.SelectedRows.Item(0).Cells(0).Value & "'")
            mydata_par = .runQuery("SELECT max(inv_ID) as paricsnumber FROM tbl_inventory_items_details")

            paricsnumber = CInt(mydata_par.Rows(0).Item("paricsnumber"))

            For count As Integer = 0 To mydata.Rows.Count - 1
                With CGSO_Inventory.dgvNewInventory
                    .Rows.Add()
                    With .Rows(count)

                        CGSO_Inventory.dgvNewInventory.Item(2, count).Value = mydata.Rows(count).Item("Item_ID").ToString
                        CGSO_Inventory.dgvNewInventory.Item(3, count).Value = mydata.Rows(count).Item("Unit").ToString
                        CGSO_Inventory.dgvNewInventory.Item(4, count).Value = mydata.Rows(count).Item("Items_Description").ToString
                        CGSO_Inventory.dgvNewInventory.Item(5, count).Value = mydata.Rows(count).Item("Quantity").ToString
                        CGSO_Inventory.dgvNewInventory.Item(6, count).Value = mydata.Rows(count).Item("Unit_Cost").ToString
                        CGSO_Inventory.dgvNewInventory.Item(7, count).Value = mydata.Rows(count).Item("Total_Cost").ToString




                        CGSO_Inventory.txtPONUMBER.Text = mydata.Rows(count).Item("PO_Number").ToString
                        CGSO_Inventory.txtPRNUMBER.Text = mydata.Rows(count).Item("PR_Number").ToString

                        CGSO_Inventory.combo_category.Text = mydata.Rows(count).Item("Category").ToString
                        CGSO_Inventory.txtDepartment.Text = mydata.Rows(count).Item("Department").ToString

                        CGSO_Inventory.combo_supplier.Text = mydata.Rows(count).Item("Supplier_Name").ToString
                        CGSO_Inventory.combo_supplier_address.Text = mydata.Rows(count).Item("Supplier_Address").ToString
                        CGSO_Inventory.po_date.Text = mydata.Rows(count).Item("Date_PO").ToString
                        CGSO_Inventory.purpose.Text = mydata.Rows(count).Item("Purpose").ToString


                        If Not IsDBNull(mydata.Rows(count).Item("Unit_Cost")) Then

                            unitCost = CDec(mydata.Rows(count).Item("Unit_Cost"))

                            If unitCost <= 4999.99 Then
                                ' ...
                                ' CGSO_Inventory.dgvNewInventory.Item(11, count).Value = "ICS" & year_in_digits & month_in_digits & paricsnumber.ToString("00000")
                                ' ...
                                CGSO_Inventory.dgvNewInventory.Item(10, count).Value = "ICS LOW VALUE"
                            ElseIf unitCost >= 5000.0 And unitCost < 49000.0 Then
                                ' ...
                                ' CGSO_Inventory.dgvNewInventory.Item(11, count).Value = "ICS" & year_in_digits & month_in_digits & paricsnumber.ToString("00000")
                                ' ...
                                CGSO_Inventory.dgvNewInventory.Item(10, count).Value = "ICS HIGH VALUE"
                            ElseIf unitCost >= 49001.01 Then
                                ' ...
                                ' CGSO_Inventory.dgvNewInventory.Item(11, count).Value = "PAR" & year_in_digits & month_in_digits & paricsnumber.ToString("00000")
                                ' ...
                                CGSO_Inventory.dgvNewInventory.Item(10, count).Value = "PAR"
                            End If

                            paricsnumber += 1
                        Else
                        End If
                    End With
                End With

            Next

            Try



            Catch ex As Exception

            End Try
        End With




        For x As Integer = 0 To CGSO_Inventory.dgvNewInventory.Rows.Count
            CGSO_Inventory.txtTotalItems.Text = x.ToString
        Next

        sumx()




        Me.Close()
    End Sub
End Class