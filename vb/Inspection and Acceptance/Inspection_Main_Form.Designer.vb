<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inspection_Main_Form
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgvInspect = New System.Windows.Forms.DataGridView
        Me.PO_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PR_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TradeName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Address = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Purpose = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Date_Created = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Inspect = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Inspected = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InspectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewInspectedItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ForProgressBillingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.combo_search = New System.Windows.Forms.ComboBox
        Me.txtSearchBox = New System.Windows.Forms.TextBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.groupadvancesearch = New System.Windows.Forms.GroupBox
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.city_budget = New System.Windows.Forms.Label
        Me.city_accountant = New System.Windows.Forms.Label
        Me.city_treasurer = New System.Windows.Forms.Label
        Me.city_mayor = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.dgvPO_ITEMLIST = New System.Windows.Forms.DataGridView
        Me.Item_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Units = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Item_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Total_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkadvancesearch = New System.Windows.Forms.CheckBox
        CType(Me.dgvInspect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.groupadvancesearch.SuspendLayout()
        CType(Me.dgvPO_ITEMLIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Location = New System.Drawing.Point(14, 626)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1341, 12)
        Me.Panel3.TabIndex = 109
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Location = New System.Drawing.Point(1345, 96)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 542)
        Me.Panel1.TabIndex = 108
        '
        'dgvInspect
        '
        Me.dgvInspect.AllowUserToAddRows = False
        Me.dgvInspect.AllowUserToDeleteRows = False
        Me.dgvInspect.AllowUserToResizeColumns = False
        Me.dgvInspect.AllowUserToResizeRows = False
        Me.dgvInspect.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvInspect.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvInspect.BackgroundColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInspect.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInspect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInspect.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PO_Number, Me.PR_Number, Me.Department, Me.TradeName, Me.Supplier_Address, Me.Purpose, Me.Date_Created, Me.Inspect, Me.Inspected})
        Me.dgvInspect.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInspect.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInspect.Location = New System.Drawing.Point(29, 234)
        Me.dgvInspect.Name = "dgvInspect"
        Me.dgvInspect.ReadOnly = True
        Me.dgvInspect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInspect.Size = New System.Drawing.Size(1020, 374)
        Me.dgvInspect.TabIndex = 6
        '
        'PO_Number
        '
        Me.PO_Number.FillWeight = 50.0!
        Me.PO_Number.HeaderText = "Purchase Order Number"
        Me.PO_Number.Name = "PO_Number"
        Me.PO_Number.ReadOnly = True
        Me.PO_Number.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'PR_Number
        '
        Me.PR_Number.FillWeight = 50.0!
        Me.PR_Number.HeaderText = "Purchase Request Number"
        Me.PR_Number.Name = "PR_Number"
        Me.PR_Number.ReadOnly = True
        '
        'Department
        '
        Me.Department.FillWeight = 80.0!
        Me.Department.HeaderText = "Office"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        '
        'TradeName
        '
        Me.TradeName.FillWeight = 80.0!
        Me.TradeName.HeaderText = "Supplier Name"
        Me.TradeName.Name = "TradeName"
        Me.TradeName.ReadOnly = True
        '
        'Supplier_Address
        '
        Me.Supplier_Address.FillWeight = 60.0!
        Me.Supplier_Address.HeaderText = "Supplier Address"
        Me.Supplier_Address.Name = "Supplier_Address"
        Me.Supplier_Address.ReadOnly = True
        '
        'Purpose
        '
        Me.Purpose.FillWeight = 80.0!
        Me.Purpose.HeaderText = "Purpose"
        Me.Purpose.Name = "Purpose"
        Me.Purpose.ReadOnly = True
        '
        'Date_Created
        '
        Me.Date_Created.FillWeight = 50.0!
        Me.Date_Created.HeaderText = "Date Created"
        Me.Date_Created.Name = "Date_Created"
        Me.Date_Created.ReadOnly = True
        '
        'Inspect
        '
        Me.Inspect.FillWeight = 20.0!
        Me.Inspect.HeaderText = "Inspect"
        Me.Inspect.Name = "Inspect"
        Me.Inspect.ReadOnly = True
        Me.Inspect.Visible = False
        '
        'Inspected
        '
        Me.Inspected.FillWeight = 35.0!
        Me.Inspected.HeaderText = "Inspected"
        Me.Inspected.Name = "Inspected"
        Me.Inspected.ReadOnly = True
        Me.Inspected.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Inspected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InspectToolStripMenuItem, Me.ViewInspectedItemsToolStripMenuItem, Me.ForProgressBillingToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(191, 70)
        '
        'InspectToolStripMenuItem
        '
        Me.InspectToolStripMenuItem.Name = "InspectToolStripMenuItem"
        Me.InspectToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.InspectToolStripMenuItem.Text = "Inspect Supplier"
        '
        'ViewInspectedItemsToolStripMenuItem
        '
        Me.ViewInspectedItemsToolStripMenuItem.Name = "ViewInspectedItemsToolStripMenuItem"
        Me.ViewInspectedItemsToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ViewInspectedItemsToolStripMenuItem.Text = "View Inspected Item/s"
        '
        'ForProgressBillingToolStripMenuItem
        '
        Me.ForProgressBillingToolStripMenuItem.Name = "ForProgressBillingToolStripMenuItem"
        Me.ForProgressBillingToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ForProgressBillingToolStripMenuItem.Text = "For Progress Billing"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox2.Controls.Add(Me.combo_search)
        Me.GroupBox2.Controls.Add(Me.txtSearchBox)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(29, 68)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(667, 60)
        Me.GroupBox2.TabIndex = 111
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SearchBox"
        '
        'combo_search
        '
        Me.combo_search.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combo_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_search.FormattingEnabled = True
        Me.combo_search.Items.AddRange(New Object() {"PO_Number", "PR_Number", "Department", "Supplier_Name", "Supplier_address"})
        Me.combo_search.Location = New System.Drawing.Point(6, 22)
        Me.combo_search.Name = "combo_search"
        Me.combo_search.Size = New System.Drawing.Size(133, 23)
        Me.combo_search.TabIndex = 1
        '
        'txtSearchBox
        '
        Me.txtSearchBox.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchBox.Location = New System.Drawing.Point(145, 22)
        Me.txtSearchBox.Name = "txtSearchBox"
        Me.txtSearchBox.Size = New System.Drawing.Size(238, 23)
        Me.txtSearchBox.TabIndex = 2
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(526, 19)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(106, 29)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Blue
        Me.btnSearch.Location = New System.Drawing.Point(414, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(106, 29)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Copperplate Gothic Bold", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.IndianRed
        Me.Label1.Location = New System.Drawing.Point(371, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(613, 43)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "- Inspection and Acceptance"
        '
        'groupadvancesearch
        '
        Me.groupadvancesearch.BackColor = System.Drawing.Color.AntiqueWhite
        Me.groupadvancesearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.groupadvancesearch.Controls.Add(Me.datefrom)
        Me.groupadvancesearch.Controls.Add(Me.Label8)
        Me.groupadvancesearch.Controls.Add(Me.dateto)
        Me.groupadvancesearch.Controls.Add(Me.Label9)
        Me.groupadvancesearch.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.groupadvancesearch.ForeColor = System.Drawing.Color.Black
        Me.groupadvancesearch.Location = New System.Drawing.Point(29, 144)
        Me.groupadvancesearch.Name = "groupadvancesearch"
        Me.groupadvancesearch.Size = New System.Drawing.Size(667, 84)
        Me.groupadvancesearch.TabIndex = 112
        Me.groupadvancesearch.TabStop = False
        '
        'datefrom
        '
        Me.datefrom.Location = New System.Drawing.Point(95, 22)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(268, 23)
        Me.datefrom.TabIndex = 68
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(46, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 15)
        Me.Label8.TabIndex = 66
        Me.Label8.Text = "FROM"
        '
        'dateto
        '
        Me.dateto.Location = New System.Drawing.Point(95, 48)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(268, 23)
        Me.dateto.TabIndex = 65
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(54, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 15)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "TO"
        '
        'city_budget
        '
        Me.city_budget.AutoSize = True
        Me.city_budget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_budget.Location = New System.Drawing.Point(755, 208)
        Me.city_budget.Name = "city_budget"
        Me.city_budget.Size = New System.Drawing.Size(81, 15)
        Me.city_budget.TabIndex = 177
        Me.city_budget.Text = "CITY BUDGET"
        Me.city_budget.Visible = False
        '
        'city_accountant
        '
        Me.city_accountant.AutoSize = True
        Me.city_accountant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_accountant.Location = New System.Drawing.Point(755, 187)
        Me.city_accountant.Name = "city_accountant"
        Me.city_accountant.Size = New System.Drawing.Size(110, 15)
        Me.city_accountant.TabIndex = 176
        Me.city_accountant.Text = "CITY ACCOUNTANT"
        Me.city_accountant.Visible = False
        '
        'city_treasurer
        '
        Me.city_treasurer.AutoSize = True
        Me.city_treasurer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_treasurer.Location = New System.Drawing.Point(755, 166)
        Me.city_treasurer.Name = "city_treasurer"
        Me.city_treasurer.Size = New System.Drawing.Size(103, 15)
        Me.city_treasurer.TabIndex = 175
        Me.city_treasurer.Text = "CITY TREASURER"
        Me.city_treasurer.Visible = False
        '
        'city_mayor
        '
        Me.city_mayor.AutoSize = True
        Me.city_mayor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_mayor.Location = New System.Drawing.Point(755, 144)
        Me.city_mayor.Name = "city_mayor"
        Me.city_mayor.Size = New System.Drawing.Size(75, 15)
        Me.city_mayor.TabIndex = 174
        Me.city_mayor.Text = "CITY MAYOR"
        Me.city_mayor.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.IndianRed
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Location = New System.Drawing.Point(12, 234)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(10, 404)
        Me.Panel5.TabIndex = 178
        '
        'dgvPO_ITEMLIST
        '
        Me.dgvPO_ITEMLIST.AllowUserToAddRows = False
        Me.dgvPO_ITEMLIST.AllowUserToDeleteRows = False
        Me.dgvPO_ITEMLIST.AllowUserToResizeColumns = False
        Me.dgvPO_ITEMLIST.AllowUserToResizeRows = False
        Me.dgvPO_ITEMLIST.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPO_ITEMLIST.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvPO_ITEMLIST.BackgroundColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO_ITEMLIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPO_ITEMLIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPO_ITEMLIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Item_ID, Me.Units, Me.Item_Description, Me.Quantity, Me.Unit_Cost, Me.Total_Cost})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO_ITEMLIST.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPO_ITEMLIST.Location = New System.Drawing.Point(1055, 96)
        Me.dgvPO_ITEMLIST.Name = "dgvPO_ITEMLIST"
        Me.dgvPO_ITEMLIST.ReadOnly = True
        Me.dgvPO_ITEMLIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPO_ITEMLIST.Size = New System.Drawing.Size(284, 511)
        Me.dgvPO_ITEMLIST.TabIndex = 7
        '
        'Item_ID
        '
        Me.Item_ID.FillWeight = 15.0!
        Me.Item_ID.HeaderText = "ID"
        Me.Item_ID.Name = "Item_ID"
        Me.Item_ID.ReadOnly = True
        Me.Item_ID.Visible = False
        '
        'Units
        '
        Me.Units.FillWeight = 40.0!
        Me.Units.HeaderText = "Units"
        Me.Units.Name = "Units"
        Me.Units.ReadOnly = True
        Me.Units.Visible = False
        '
        'Item_Description
        '
        Me.Item_Description.HeaderText = "Items Description"
        Me.Item_Description.Name = "Item_Description"
        Me.Item_Description.ReadOnly = True
        Me.Item_Description.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Quantity
        '
        Me.Quantity.FillWeight = 20.0!
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.ReadOnly = True
        Me.Quantity.Visible = False
        '
        'Unit_Cost
        '
        Me.Unit_Cost.FillWeight = 40.0!
        Me.Unit_Cost.HeaderText = "Unit Cost"
        Me.Unit_Cost.Name = "Unit_Cost"
        Me.Unit_Cost.ReadOnly = True
        Me.Unit_Cost.Visible = False
        '
        'Total_Cost
        '
        Me.Total_Cost.FillWeight = 40.0!
        Me.Total_Cost.HeaderText = "Total Cost"
        Me.Total_Cost.Name = "Total_Cost"
        Me.Total_Cost.ReadOnly = True
        Me.Total_Cost.Visible = False
        '
        'chkadvancesearch
        '
        Me.chkadvancesearch.AutoSize = True
        Me.chkadvancesearch.Location = New System.Drawing.Point(30, 132)
        Me.chkadvancesearch.Name = "chkadvancesearch"
        Me.chkadvancesearch.Size = New System.Drawing.Size(106, 17)
        Me.chkadvancesearch.TabIndex = 4
        Me.chkadvancesearch.Text = "Advance Search"
        Me.chkadvancesearch.UseVisualStyleBackColor = True
        '
        'Inspection_Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1364, 645)
        Me.Controls.Add(Me.chkadvancesearch)
        Me.Controls.Add(Me.dgvPO_ITEMLIST)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.city_budget)
        Me.Controls.Add(Me.city_accountant)
        Me.Controls.Add(Me.city_treasurer)
        Me.Controls.Add(Me.city_mayor)
        Me.Controls.Add(Me.groupadvancesearch)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.dgvInspect)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Inspection_Main_Form"
        Me.Text = "Inspection_Main_Form"
        CType(Me.dgvInspect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.groupadvancesearch.ResumeLayout(False)
        Me.groupadvancesearch.PerformLayout()
        CType(Me.dgvPO_ITEMLIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgvInspect As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents combo_search As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents groupadvancesearch As System.Windows.Forms.GroupBox
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents InspectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewInspectedItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents city_budget As System.Windows.Forms.Label
    Friend WithEvents city_accountant As System.Windows.Forms.Label
    Friend WithEvents city_treasurer As System.Windows.Forms.Label
    Friend WithEvents city_mayor As System.Windows.Forms.Label
    Friend WithEvents ForProgressBillingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dgvPO_ITEMLIST As System.Windows.Forms.DataGridView
    Friend WithEvents Item_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Item_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PO_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PR_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TradeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Purpose As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Inspect As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Inspected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkadvancesearch As System.Windows.Forms.CheckBox
End Class
