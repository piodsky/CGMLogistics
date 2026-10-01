<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Add_Purchase_Order
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtTotalItems = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSubTotal = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.AddRow = New System.Windows.Forms.Button
        Me.btnUse = New System.Windows.Forms.Button
        Me.dgvPO = New System.Windows.Forms.DataGridView
        Me.Item_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Items_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Total_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NumberSeries = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddRowNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.po_details = New System.Windows.Forms.GroupBox
        Me.combo_category = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.combo_pr_received_name = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.supplier_id = New System.Windows.Forms.Label
        Me.sec_id = New System.Windows.Forms.Label
        Me.div_id = New System.Windows.Forms.Label
        Me.dep_id = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txt_pr_number = New System.Windows.Forms.TextBox
        Me.combo_supplier_address = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.combo_supplier = New System.Windows.Forms.ComboBox
        Me.combo_div = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.po_date = New System.Windows.Forms.DateTimePicker
        Me.combo_section = New System.Windows.Forms.ComboBox
        Me.combo_dept = New System.Windows.Forms.ComboBox
        Me.purpose = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblsave = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.PO_Status = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtPONUMBER = New System.Windows.Forms.Label
        Me.lblpo = New System.Windows.Forms.Label
        Me.remarks = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.supplier_date_received = New System.Windows.Forms.TextBox
        Me.coa_date_received = New System.Windows.Forms.TextBox
        Me.approved_po_date = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Ref_Number = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.BAC_Resolution_Number = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.po_details.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtTotalItems)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtSubTotal)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.AddRow)
        Me.GroupBox1.Controls.Add(Me.btnUse)
        Me.GroupBox1.Controls.Add(Me.dgvPO)
        Me.GroupBox1.Location = New System.Drawing.Point(352, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(978, 545)
        Me.GroupBox1.TabIndex = 82
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Selected Item/s"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(21, 503)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(148, 15)
        Me.Label16.TabIndex = 148
        Me.Label16.Text = "NAAY NAKA HIDE SA UBOS!"
        Me.Label16.Visible = False
        '
        'txtTotalItems
        '
        Me.txtTotalItems.AutoSize = True
        Me.txtTotalItems.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalItems.ForeColor = System.Drawing.Color.Black
        Me.txtTotalItems.Location = New System.Drawing.Point(720, 499)
        Me.txtTotalItems.Name = "txtTotalItems"
        Me.txtTotalItems.Size = New System.Drawing.Size(20, 23)
        Me.txtTotalItems.TabIndex = 157
        Me.txtTotalItems.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(758, 500)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 19)
        Me.Label8.TabIndex = 156
        Me.Label8.Text = "Total Cost:"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.Color.Green
        Me.txtSubTotal.Location = New System.Drawing.Point(842, 497)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(122, 27)
        Me.txtSubTotal.TabIndex = 155
        Me.txtSubTotal.Text = "0.00"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(646, 503)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 15)
        Me.Label10.TabIndex = 154
        Me.Label10.Text = "Total Items:"
        '
        'AddRow
        '
        Me.AddRow.BackColor = System.Drawing.Color.White
        Me.AddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddRow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddRow.ForeColor = System.Drawing.Color.Green
        Me.AddRow.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.AddRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddRow.Location = New System.Drawing.Point(874, 16)
        Me.AddRow.Name = "AddRow"
        Me.AddRow.Size = New System.Drawing.Size(90, 30)
        Me.AddRow.TabIndex = 21
        Me.AddRow.Text = "Add Row"
        Me.AddRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddRow.UseVisualStyleBackColor = False
        '
        'btnUse
        '
        Me.btnUse.BackColor = System.Drawing.Color.White
        Me.btnUse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUse.Image = Global.Purchase_Request.My.Resources.Resources.Datbase
        Me.btnUse.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnUse.Location = New System.Drawing.Point(6, 23)
        Me.btnUse.Name = "btnUse"
        Me.btnUse.Size = New System.Drawing.Size(94, 23)
        Me.btnUse.TabIndex = 17
        Me.btnUse.Text = "Select Items"
        Me.btnUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUse.UseVisualStyleBackColor = False
        '
        'dgvPO
        '
        Me.dgvPO.AllowUserToAddRows = False
        Me.dgvPO.AllowUserToDeleteRows = False
        Me.dgvPO.AllowUserToResizeColumns = False
        Me.dgvPO.AllowUserToResizeRows = False
        Me.dgvPO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvPO.BackgroundColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Item_ID, Me.Unit, Me.Items_Description, Me.Quantity, Me.DataGridViewTextBoxColumn6, Me.Total_Cost, Me.NumberSeries})
        Me.dgvPO.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPO.Location = New System.Drawing.Point(6, 50)
        Me.dgvPO.Name = "dgvPO"
        Me.dgvPO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvPO.Size = New System.Drawing.Size(958, 441)
        Me.dgvPO.TabIndex = 19
        '
        'Item_ID
        '
        Me.Item_ID.FillWeight = 15.0!
        Me.Item_ID.HeaderText = "ID"
        Me.Item_ID.Name = "Item_ID"
        Me.Item_ID.Visible = False
        '
        'Unit
        '
        Me.Unit.FillWeight = 40.0!
        Me.Unit.HeaderText = "Unit"
        Me.Unit.Name = "Unit"
        '
        'Items_Description
        '
        Me.Items_Description.FillWeight = 120.0!
        Me.Items_Description.HeaderText = "Item Description"
        Me.Items_Description.Name = "Items_Description"
        Me.Items_Description.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Quantity
        '
        Me.Quantity.FillWeight = 40.0!
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.FillWeight = 60.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "Unit_Cost"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'Total_Cost
        '
        Me.Total_Cost.FillWeight = 60.0!
        Me.Total_Cost.HeaderText = "Total Cost"
        Me.Total_Cost.Name = "Total_Cost"
        '
        'NumberSeries
        '
        Me.NumberSeries.FillWeight = 20.0!
        Me.NumberSeries.HeaderText = "#"
        Me.NumberSeries.Name = "NumberSeries"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveItemsToolStripMenuItem, Me.AddRowNumberToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(170, 48)
        '
        'RemoveItemsToolStripMenuItem
        '
        Me.RemoveItemsToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.RemoveItemsToolStripMenuItem.Name = "RemoveItemsToolStripMenuItem"
        Me.RemoveItemsToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.RemoveItemsToolStripMenuItem.Text = "Remove Item/s"
        '
        'AddRowNumberToolStripMenuItem
        '
        Me.AddRowNumberToolStripMenuItem.Name = "AddRowNumberToolStripMenuItem"
        Me.AddRowNumberToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.AddRowNumberToolStripMenuItem.Text = "Add Row Number"
        '
        'po_details
        '
        Me.po_details.BackColor = System.Drawing.Color.AntiqueWhite
        Me.po_details.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.po_details.Controls.Add(Me.combo_category)
        Me.po_details.Controls.Add(Me.Label19)
        Me.po_details.Controls.Add(Me.Label18)
        Me.po_details.Controls.Add(Me.combo_pr_received_name)
        Me.po_details.Controls.Add(Me.Ref_Number)
        Me.po_details.Controls.Add(Me.Label7)
        Me.po_details.Controls.Add(Me.Label20)
        Me.po_details.Controls.Add(Me.supplier_id)
        Me.po_details.Controls.Add(Me.BAC_Resolution_Number)
        Me.po_details.Controls.Add(Me.sec_id)
        Me.po_details.Controls.Add(Me.div_id)
        Me.po_details.Controls.Add(Me.dep_id)
        Me.po_details.Controls.Add(Me.Label13)
        Me.po_details.Controls.Add(Me.txt_pr_number)
        Me.po_details.Controls.Add(Me.combo_supplier_address)
        Me.po_details.Controls.Add(Me.Label12)
        Me.po_details.Controls.Add(Me.Label11)
        Me.po_details.Controls.Add(Me.combo_supplier)
        Me.po_details.Controls.Add(Me.combo_div)
        Me.po_details.Controls.Add(Me.Label2)
        Me.po_details.Controls.Add(Me.po_date)
        Me.po_details.Controls.Add(Me.combo_section)
        Me.po_details.Controls.Add(Me.combo_dept)
        Me.po_details.Controls.Add(Me.purpose)
        Me.po_details.Controls.Add(Me.Label6)
        Me.po_details.Controls.Add(Me.Label5)
        Me.po_details.Controls.Add(Me.Label4)
        Me.po_details.Controls.Add(Me.Label3)
        Me.po_details.ForeColor = System.Drawing.Color.Black
        Me.po_details.Location = New System.Drawing.Point(9, 3)
        Me.po_details.Name = "po_details"
        Me.po_details.Size = New System.Drawing.Size(327, 654)
        Me.po_details.TabIndex = 78
        Me.po_details.TabStop = False
        Me.po_details.Text = "Purchase Order Details"
        '
        'combo_category
        '
        Me.combo_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_category.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_category.FormattingEnabled = True
        Me.combo_category.Location = New System.Drawing.Point(9, 120)
        Me.combo_category.Name = "combo_category"
        Me.combo_category.Size = New System.Drawing.Size(298, 23)
        Me.combo_category.TabIndex = 100
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(7, 102)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 15)
        Me.Label18.TabIndex = 99
        Me.Label18.Text = "Category:"
        '
        'combo_pr_received_name
        '
        Me.combo_pr_received_name.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_pr_received_name.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_pr_received_name.FormattingEnabled = True
        Me.combo_pr_received_name.Location = New System.Drawing.Point(8, 76)
        Me.combo_pr_received_name.Name = "combo_pr_received_name"
        Me.combo_pr_received_name.Size = New System.Drawing.Size(298, 23)
        Me.combo_pr_received_name.TabIndex = 98
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(5, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 15)
        Me.Label7.TabIndex = 97
        Me.Label7.Text = "PR Received Name:"
        '
        'supplier_id
        '
        Me.supplier_id.AutoSize = True
        Me.supplier_id.Location = New System.Drawing.Point(101, 371)
        Me.supplier_id.Name = "supplier_id"
        Me.supplier_id.Size = New System.Drawing.Size(57, 13)
        Me.supplier_id.TabIndex = 96
        Me.supplier_id.Text = "supplier_id"
        Me.supplier_id.Visible = False
        '
        'sec_id
        '
        Me.sec_id.AutoSize = True
        Me.sec_id.Location = New System.Drawing.Point(59, 239)
        Me.sec_id.Name = "sec_id"
        Me.sec_id.Size = New System.Drawing.Size(38, 13)
        Me.sec_id.TabIndex = 95
        Me.sec_id.Text = "sec_id"
        Me.sec_id.Visible = False
        '
        'div_id
        '
        Me.div_id.AutoSize = True
        Me.div_id.Location = New System.Drawing.Point(70, 195)
        Me.div_id.Name = "div_id"
        Me.div_id.Size = New System.Drawing.Size(35, 13)
        Me.div_id.TabIndex = 94
        Me.div_id.Text = "div_id"
        Me.div_id.Visible = False
        '
        'dep_id
        '
        Me.dep_id.AutoSize = True
        Me.dep_id.Location = New System.Drawing.Point(88, 147)
        Me.dep_id.Name = "dep_id"
        Me.dep_id.Size = New System.Drawing.Size(39, 13)
        Me.dep_id.TabIndex = 93
        Me.dep_id.Text = "dep_id"
        Me.dep_id.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(8, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(150, 15)
        Me.Label13.TabIndex = 92
        Me.Label13.Text = "Purchase Request Number"
        '
        'txt_pr_number
        '
        Me.txt_pr_number.BackColor = System.Drawing.Color.White
        Me.txt_pr_number.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.txt_pr_number.ForeColor = System.Drawing.Color.Blue
        Me.txt_pr_number.Location = New System.Drawing.Point(9, 34)
        Me.txt_pr_number.Name = "txt_pr_number"
        Me.txt_pr_number.Size = New System.Drawing.Size(298, 23)
        Me.txt_pr_number.TabIndex = 1
        '
        'combo_supplier_address
        '
        Me.combo_supplier_address.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_supplier_address.Location = New System.Drawing.Point(10, 431)
        Me.combo_supplier_address.Multiline = True
        Me.combo_supplier_address.Name = "combo_supplier_address"
        Me.combo_supplier_address.Size = New System.Drawing.Size(298, 53)
        Me.combo_supplier_address.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(7, 413)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(103, 15)
        Me.Label12.TabIndex = 89
        Me.Label12.Text = "Supplier Address:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(7, 370)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 15)
        Me.Label11.TabIndex = 88
        Me.Label11.Text = "Supplier Name:"
        '
        'combo_supplier
        '
        Me.combo_supplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_supplier.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_supplier.FormattingEnabled = True
        Me.combo_supplier.Location = New System.Drawing.Point(10, 388)
        Me.combo_supplier.Name = "combo_supplier"
        Me.combo_supplier.Size = New System.Drawing.Size(298, 23)
        Me.combo_supplier.TabIndex = 9
        '
        'combo_div
        '
        Me.combo_div.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_div.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_div.FormattingEnabled = True
        Me.combo_div.Location = New System.Drawing.Point(10, 213)
        Me.combo_div.Name = "combo_div"
        Me.combo_div.Size = New System.Drawing.Size(298, 23)
        Me.combo_div.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 193)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 15)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Division:"
        '
        'po_date
        '
        Me.po_date.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.po_date.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.po_date.Location = New System.Drawing.Point(9, 504)
        Me.po_date.Name = "po_date"
        Me.po_date.Size = New System.Drawing.Size(298, 23)
        Me.po_date.TabIndex = 13
        '
        'combo_section
        '
        Me.combo_section.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_section.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_section.FormattingEnabled = True
        Me.combo_section.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.combo_section.Location = New System.Drawing.Point(9, 257)
        Me.combo_section.Name = "combo_section"
        Me.combo_section.Size = New System.Drawing.Size(298, 23)
        Me.combo_section.TabIndex = 7
        '
        'combo_dept
        '
        Me.combo_dept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_dept.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.combo_dept.FormattingEnabled = True
        Me.combo_dept.Location = New System.Drawing.Point(11, 165)
        Me.combo_dept.Name = "combo_dept"
        Me.combo_dept.Size = New System.Drawing.Size(298, 23)
        Me.combo_dept.TabIndex = 3
        '
        'purpose
        '
        Me.purpose.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.purpose.Location = New System.Drawing.Point(9, 545)
        Me.purpose.Multiline = True
        Me.purpose.Name = "purpose"
        Me.purpose.Size = New System.Drawing.Size(300, 102)
        Me.purpose.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(7, 527)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 15)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Purpose/Remarks:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 488)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Date:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(8, 239)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 15)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Section:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 147)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Department:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Copperplate Gothic Light", 27.75!)
        Me.Label1.ForeColor = System.Drawing.Color.IndianRed
        Me.Label1.Location = New System.Drawing.Point(572, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(351, 42)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Purchase Order"
        '
        'lblsave
        '
        Me.lblsave.AutoSize = True
        Me.lblsave.BackColor = System.Drawing.Color.AntiqueWhite
        Me.lblsave.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsave.ForeColor = System.Drawing.Color.Blue
        Me.lblsave.Location = New System.Drawing.Point(28, 66)
        Me.lblsave.Name = "lblsave"
        Me.lblsave.Size = New System.Drawing.Size(37, 18)
        Me.lblsave.TabIndex = 69
        Me.lblsave.Text = "Save"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Controls.Add(Me.lblsave)
        Me.GroupBox2.Location = New System.Drawing.Point(1063, 15)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(267, 91)
        Me.GroupBox2.TabIndex = 86
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Menu"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnSave.Location = New System.Drawing.Point(17, 19)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 44)
        Me.btnSave.TabIndex = 23
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'StatusStrip2
        '
        Me.StatusStrip2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel3, Me.PO_Status})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 660)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(1340, 22)
        Me.StatusStrip2.TabIndex = 140
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(42, 17)
        Me.ToolStripStatusLabel3.Text = "Status:"
        '
        'PO_Status
        '
        Me.PO_Status.BackColor = System.Drawing.Color.AntiqueWhite
        Me.PO_Status.ForeColor = System.Drawing.Color.Black
        Me.PO_Status.Name = "PO_Status"
        Me.PO_Status.Size = New System.Drawing.Size(75, 17)
        Me.PO_Status.Text = "ppmp_status"
        '
        'txtPONUMBER
        '
        Me.txtPONUMBER.AutoSize = True
        Me.txtPONUMBER.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtPONUMBER.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txtPONUMBER.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONUMBER.ForeColor = System.Drawing.Color.Green
        Me.txtPONUMBER.Location = New System.Drawing.Point(436, 75)
        Me.txtPONUMBER.Name = "txtPONUMBER"
        Me.txtPONUMBER.Size = New System.Drawing.Size(114, 28)
        Me.txtPONUMBER.TabIndex = 141
        Me.txtPONUMBER.Text = "PO Number"
        '
        'lblpo
        '
        Me.lblpo.AutoSize = True
        Me.lblpo.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpo.Location = New System.Drawing.Point(358, 81)
        Me.lblpo.Name = "lblpo"
        Me.lblpo.Size = New System.Drawing.Size(72, 15)
        Me.lblpo.TabIndex = 142
        Me.lblpo.Text = "PO Number:"
        '
        'remarks
        '
        Me.remarks.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.remarks.Location = New System.Drawing.Point(803, 690)
        Me.remarks.Multiline = True
        Me.remarks.Name = "remarks"
        Me.remarks.Size = New System.Drawing.Size(300, 54)
        Me.remarks.TabIndex = 145
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(6, 693)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(135, 15)
        Me.Label9.TabIndex = 99
        Me.Label9.Text = "Date Received Supplier:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(29, 724)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 15)
        Me.Label14.TabIndex = 146
        Me.Label14.Text = "Date COA Received:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(740, 696)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 15)
        Me.Label15.TabIndex = 147
        Me.Label15.Text = "Remarks:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(386, 696)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 15)
        Me.Label17.TabIndex = 149
        Me.Label17.Text = "Date Approved PO:"
        '
        'supplier_date_received
        '
        Me.supplier_date_received.BackColor = System.Drawing.Color.White
        Me.supplier_date_received.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.supplier_date_received.ForeColor = System.Drawing.Color.Blue
        Me.supplier_date_received.Location = New System.Drawing.Point(147, 690)
        Me.supplier_date_received.Name = "supplier_date_received"
        Me.supplier_date_received.Size = New System.Drawing.Size(214, 23)
        Me.supplier_date_received.TabIndex = 99
        '
        'coa_date_received
        '
        Me.coa_date_received.BackColor = System.Drawing.Color.White
        Me.coa_date_received.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.coa_date_received.ForeColor = System.Drawing.Color.Blue
        Me.coa_date_received.Location = New System.Drawing.Point(147, 721)
        Me.coa_date_received.Name = "coa_date_received"
        Me.coa_date_received.Size = New System.Drawing.Size(214, 23)
        Me.coa_date_received.TabIndex = 152
        '
        'approved_po_date
        '
        Me.approved_po_date.BackColor = System.Drawing.Color.White
        Me.approved_po_date.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.approved_po_date.ForeColor = System.Drawing.Color.Blue
        Me.approved_po_date.Location = New System.Drawing.Point(501, 692)
        Me.approved_po_date.Name = "approved_po_date"
        Me.approved_po_date.Size = New System.Drawing.Size(214, 23)
        Me.approved_po_date.TabIndex = 153
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(8, 283)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 15)
        Me.Label19.TabIndex = 157
        Me.Label19.Text = "Reference #:"
        '
        'Ref_Number
        '
        Me.Ref_Number.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.Ref_Number.ForeColor = System.Drawing.Color.ForestGreen
        Me.Ref_Number.Location = New System.Drawing.Point(9, 301)
        Me.Ref_Number.Name = "Ref_Number"
        Me.Ref_Number.Size = New System.Drawing.Size(298, 23)
        Me.Ref_Number.TabIndex = 155
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(8, 326)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(69, 15)
        Me.Label20.TabIndex = 154
        Me.Label20.Text = "BAC Reso #:"
        '
        'BAC_Resolution_Number
        '
        Me.BAC_Resolution_Number.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.BAC_Resolution_Number.ForeColor = System.Drawing.Color.ForestGreen
        Me.BAC_Resolution_Number.Location = New System.Drawing.Point(9, 344)
        Me.BAC_Resolution_Number.Name = "BAC_Resolution_Number"
        Me.BAC_Resolution_Number.Size = New System.Drawing.Size(298, 23)
        Me.BAC_Resolution_Number.TabIndex = 156
        '
        'Add_Purchase_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1340, 682)
        Me.Controls.Add(Me.approved_po_date)
        Me.Controls.Add(Me.coa_date_received)
        Me.Controls.Add(Me.supplier_date_received)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.remarks)
        Me.Controls.Add(Me.lblpo)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.txtPONUMBER)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.po_details)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Add_Purchase_Order"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add_Purchase_Order"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.po_details.ResumeLayout(False)
        Me.po_details.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents po_details As System.Windows.Forms.GroupBox
    Friend WithEvents combo_div As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents po_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents combo_section As System.Windows.Forms.ComboBox
    Friend WithEvents combo_dept As System.Windows.Forms.ComboBox
    Friend WithEvents purpose As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblsave As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PO_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnUse As System.Windows.Forms.Button
    Friend WithEvents AddRow As System.Windows.Forms.Button
    Friend WithEvents txtTotalItems As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents combo_supplier As System.Windows.Forms.ComboBox
    Friend WithEvents combo_supplier_address As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_pr_number As System.Windows.Forms.TextBox
    Friend WithEvents dep_id As System.Windows.Forms.Label
    Friend WithEvents div_id As System.Windows.Forms.Label
    Friend WithEvents supplier_id As System.Windows.Forms.Label
    Friend WithEvents sec_id As System.Windows.Forms.Label
    Friend WithEvents txtPONUMBER As System.Windows.Forms.Label
    Friend WithEvents lblpo As System.Windows.Forms.Label
    Friend WithEvents combo_pr_received_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents supplier_date_received As System.Windows.Forms.TextBox
    Friend WithEvents coa_date_received As System.Windows.Forms.TextBox
    Friend WithEvents approved_po_date As System.Windows.Forms.TextBox
    Friend WithEvents combo_category As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents AddRowNumberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvPO As System.Windows.Forms.DataGridView
    Friend WithEvents Item_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Items_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumberSeries As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ref_Number As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents BAC_Resolution_Number As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class
