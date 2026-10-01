<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PO_Main_Form
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dgvPO_LIST = New System.Windows.Forms.DataGridView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddPRReceivedNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddDateReceivedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddCoaDateReceivedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddApprovedPODateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddRemarksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.dgvPO_ITEMLIST = New System.Windows.Forms.DataGridView
        Me.Item_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Units = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Item_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Total_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnPrintObligationRequest = New System.Windows.Forms.Button
        Me.btnPrintPO = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnEdit = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnDel = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.combo_search = New System.Windows.Forms.ComboBox
        Me.txtSearchBox = New System.Windows.Forms.TextBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.city_mayor = New System.Windows.Forms.Label
        Me.groupadvancesearch = New System.Windows.Forms.GroupBox
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.chkadvancesearch = New System.Windows.Forms.CheckBox
        Me.city_treasurer = New System.Windows.Forms.Label
        Me.city_accountant = New System.Windows.Forms.Label
        Me.city_budget = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rad_date_received_supplier = New System.Windows.Forms.RadioButton
        Me.rad_View_ALL = New System.Windows.Forms.RadioButton
        Me.rad_Date_COA_Received = New System.Windows.Forms.RadioButton
        Me.rad_Date_Approved_PO = New System.Windows.Forms.RadioButton
        Me.group_admin = New System.Windows.Forms.GroupBox
        Me.rad_or = New System.Windows.Forms.RadioButton
        Me.rad_Blank_All = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtSubTotal = New System.Windows.Forms.TextBox
        Me.groupadminControl = New System.Windows.Forms.GroupBox
        Me.btnUNLOCK = New Guna.UI.WinForms.GunaButton
        Me.btnLOCK = New Guna.UI.WinForms.GunaButton
        Me.PO_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PR_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Barcode_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TradeName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Address = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Purpose = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Date_Created = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PR_Received_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Date_Received_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.COA_Date_Received = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Date_Approved_PO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvPO_LIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.dgvPO_ITEMLIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.groupadvancesearch.SuspendLayout()
        Me.group_admin.SuspendLayout()
        Me.groupadminControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvPO_LIST
        '
        Me.dgvPO_LIST.AllowUserToAddRows = False
        Me.dgvPO_LIST.AllowUserToDeleteRows = False
        Me.dgvPO_LIST.AllowUserToResizeColumns = False
        Me.dgvPO_LIST.AllowUserToResizeRows = False
        Me.dgvPO_LIST.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPO_LIST.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvPO_LIST.BackgroundColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO_LIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPO_LIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPO_LIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PO_Number, Me.PR_Number, Me.Barcode_Number, Me.Department, Me.TradeName, Me.Supplier_Address, Me.Purpose, Me.Category, Me.Date_Created, Me.PR_Received_Name, Me.Date_Received_Supplier, Me.COA_Date_Received, Me.Date_Approved_PO, Me.Remarks})
        Me.dgvPO_LIST.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO_LIST.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPO_LIST.Location = New System.Drawing.Point(13, 221)
        Me.dgvPO_LIST.Name = "dgvPO_LIST"
        Me.dgvPO_LIST.ReadOnly = True
        Me.dgvPO_LIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPO_LIST.Size = New System.Drawing.Size(1043, 444)
        Me.dgvPO_LIST.TabIndex = 11
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewToolStripMenuItem, Me.AddCategoryToolStripMenuItem, Me.AddPRReceivedNameToolStripMenuItem, Me.AddDateReceivedToolStripMenuItem, Me.AddCoaDateReceivedToolStripMenuItem, Me.AddApprovedPODateToolStripMenuItem, Me.AddRemarksToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(220, 158)
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ViewToolStripMenuItem.Text = "View/Edit"
        '
        'AddCategoryToolStripMenuItem
        '
        Me.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem"
        Me.AddCategoryToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AddCategoryToolStripMenuItem.Text = "Add Category"
        '
        'AddPRReceivedNameToolStripMenuItem
        '
        Me.AddPRReceivedNameToolStripMenuItem.Name = "AddPRReceivedNameToolStripMenuItem"
        Me.AddPRReceivedNameToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AddPRReceivedNameToolStripMenuItem.Text = "Add PR Received Name"
        '
        'AddDateReceivedToolStripMenuItem
        '
        Me.AddDateReceivedToolStripMenuItem.Name = "AddDateReceivedToolStripMenuItem"
        Me.AddDateReceivedToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AddDateReceivedToolStripMenuItem.Text = "Add Date Received Supplier"
        '
        'AddCoaDateReceivedToolStripMenuItem
        '
        Me.AddCoaDateReceivedToolStripMenuItem.Name = "AddCoaDateReceivedToolStripMenuItem"
        Me.AddCoaDateReceivedToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AddCoaDateReceivedToolStripMenuItem.Text = "Add COA Date Received"
        '
        'AddApprovedPODateToolStripMenuItem
        '
        Me.AddApprovedPODateToolStripMenuItem.Name = "AddApprovedPODateToolStripMenuItem"
        Me.AddApprovedPODateToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AddApprovedPODateToolStripMenuItem.Text = "Add Approved PO Date"
        '
        'AddRemarksToolStripMenuItem
        '
        Me.AddRemarksToolStripMenuItem.Name = "AddRemarksToolStripMenuItem"
        Me.AddRemarksToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AddRemarksToolStripMenuItem.Text = "Add Remarks"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Copperplate Gothic Bold", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.IndianRed
        Me.Label1.Location = New System.Drawing.Point(482, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(331, 37)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "- Purchase Order"
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
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO_ITEMLIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPO_ITEMLIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPO_ITEMLIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Item_ID, Me.Units, Me.Item_Description, Me.Quantity, Me.Unit_Cost, Me.Total_Cost})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO_ITEMLIST.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPO_ITEMLIST.Location = New System.Drawing.Point(1062, 154)
        Me.dgvPO_ITEMLIST.Name = "dgvPO_ITEMLIST"
        Me.dgvPO_ITEMLIST.ReadOnly = True
        Me.dgvPO_ITEMLIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPO_ITEMLIST.Size = New System.Drawing.Size(229, 467)
        Me.dgvPO_ITEMLIST.TabIndex = 12
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
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Controls.Add(Me.btnPrintObligationRequest)
        Me.GroupBox6.Controls.Add(Me.btnPrintPO)
        Me.GroupBox6.Controls.Add(Me.btnNew)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.btnEdit)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.btnDel)
        Me.GroupBox6.Controls.Add(Me.Label4)
        Me.GroupBox6.Location = New System.Drawing.Point(872, 13)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(434, 119)
        Me.GroupBox6.TabIndex = 100
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Menu"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label10.ForeColor = System.Drawing.Color.Green
        Me.Label10.Location = New System.Drawing.Point(317, 71)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 30)
        Me.Label10.TabIndex = 98
        Me.Label10.Text = "           Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Obligation Request"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Green
        Me.Label2.Location = New System.Drawing.Point(218, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 30)
        Me.Label2.TabIndex = 96
        Me.Label2.Text = "        Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Purchase Order"
        '
        'btnPrintObligationRequest
        '
        Me.btnPrintObligationRequest.BackColor = System.Drawing.Color.White
        Me.btnPrintObligationRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintObligationRequest.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintObligationRequest.ForeColor = System.Drawing.Color.Black
        Me.btnPrintObligationRequest.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPrintObligationRequest.Location = New System.Drawing.Point(337, 14)
        Me.btnPrintObligationRequest.Name = "btnPrintObligationRequest"
        Me.btnPrintObligationRequest.Size = New System.Drawing.Size(59, 54)
        Me.btnPrintObligationRequest.TabIndex = 10
        Me.btnPrintObligationRequest.UseVisualStyleBackColor = False
        '
        'btnPrintPO
        '
        Me.btnPrintPO.BackColor = System.Drawing.Color.White
        Me.btnPrintPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintPO.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintPO.ForeColor = System.Drawing.Color.Black
        Me.btnPrintPO.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPrintPO.Location = New System.Drawing.Point(231, 15)
        Me.btnPrintPO.Name = "btnPrintPO"
        Me.btnPrintPO.Size = New System.Drawing.Size(60, 54)
        Me.btnPrintPO.TabIndex = 9
        Me.btnPrintPO.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.White
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.btnNew.Location = New System.Drawing.Point(6, 23)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(45, 38)
        Me.btnNew.TabIndex = 6
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(137, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 15)
        Me.Label6.TabIndex = 94
        Me.Label6.Text = "Delete"
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.White
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Image = Global.Purchase_Request.My.Resources.Resources.edit_small
        Me.btnEdit.Location = New System.Drawing.Point(69, 23)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(45, 38)
        Me.btnEdit.TabIndex = 7
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(63, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 15)
        Me.Label5.TabIndex = 93
        Me.Label5.Text = "View/Edit"
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.White
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDel.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.Black
        Me.btnDel.Image = Global.Purchase_Request.My.Resources.Resources.delete
        Me.btnDel.Location = New System.Drawing.Point(135, 23)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(45, 38)
        Me.btnDel.TabIndex = 8
        Me.btnDel.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Green
        Me.Label4.Location = New System.Drawing.Point(11, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 15)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "New"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox2.Controls.Add(Me.combo_search)
        Me.GroupBox2.Controls.Add(Me.txtSearchBox)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(12, 39)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(669, 60)
        Me.GroupBox2.TabIndex = 101
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SearchBox"
        '
        'combo_search
        '
        Me.combo_search.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combo_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_search.FormattingEnabled = True
        Me.combo_search.Items.AddRange(New Object() {"PR_Number", "PO_Number", "Department", "Supplier_Name", "Supplier_Address", "Date_Created"})
        Me.combo_search.Location = New System.Drawing.Point(6, 22)
        Me.combo_search.Name = "combo_search"
        Me.combo_search.Size = New System.Drawing.Size(133, 22)
        Me.combo_search.TabIndex = 1
        '
        'txtSearchBox
        '
        Me.txtSearchBox.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchBox.Location = New System.Drawing.Point(145, 22)
        Me.txtSearchBox.Name = "txtSearchBox"
        Me.txtSearchBox.Size = New System.Drawing.Size(277, 23)
        Me.txtSearchBox.TabIndex = 2
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(540, 18)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(96, 30)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Blue
        Me.btnSearch.Location = New System.Drawing.Point(438, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(96, 30)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label3.Font = New System.Drawing.Font("Copperplate Gothic Bold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.IndianRed
        Me.Label3.Location = New System.Drawing.Point(1059, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 15)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "ITEMS LIST/s"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label7.Font = New System.Drawing.Font("Copperplate Gothic Bold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.IndianRed
        Me.Label7.Location = New System.Drawing.Point(12, 203)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(182, 15)
        Me.Label7.TabIndex = 103
        Me.Label7.Text = "PURCHASE ORDER LIST"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Location = New System.Drawing.Point(1296, 154)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 530)
        Me.Panel1.TabIndex = 105
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Location = New System.Drawing.Point(2, 671)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1304, 13)
        Me.Panel4.TabIndex = 107
        '
        'city_mayor
        '
        Me.city_mayor.AutoSize = True
        Me.city_mayor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_mayor.Location = New System.Drawing.Point(687, 48)
        Me.city_mayor.Name = "city_mayor"
        Me.city_mayor.Size = New System.Drawing.Size(75, 15)
        Me.city_mayor.TabIndex = 108
        Me.city_mayor.Text = "CITY MAYOR"
        Me.city_mayor.Visible = False
        '
        'groupadvancesearch
        '
        Me.groupadvancesearch.BackColor = System.Drawing.Color.AntiqueWhite
        Me.groupadvancesearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.groupadvancesearch.Controls.Add(Me.datefrom)
        Me.groupadvancesearch.Controls.Add(Me.Label8)
        Me.groupadvancesearch.Controls.Add(Me.dateto)
        Me.groupadvancesearch.Controls.Add(Me.Label9)
        Me.groupadvancesearch.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupadvancesearch.ForeColor = System.Drawing.Color.Black
        Me.groupadvancesearch.Location = New System.Drawing.Point(14, 132)
        Me.groupadvancesearch.Name = "groupadvancesearch"
        Me.groupadvancesearch.Size = New System.Drawing.Size(667, 66)
        Me.groupadvancesearch.TabIndex = 109
        Me.groupadvancesearch.TabStop = False
        '
        'datefrom
        '
        Me.datefrom.Location = New System.Drawing.Point(95, 14)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(268, 22)
        Me.datefrom.TabIndex = 68
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(46, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 15)
        Me.Label8.TabIndex = 66
        Me.Label8.Text = "FROM"
        '
        'dateto
        '
        Me.dateto.Location = New System.Drawing.Point(95, 40)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(268, 22)
        Me.dateto.TabIndex = 65
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(54, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 15)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "TO"
        '
        'chkadvancesearch
        '
        Me.chkadvancesearch.AutoSize = True
        Me.chkadvancesearch.Location = New System.Drawing.Point(18, 114)
        Me.chkadvancesearch.Name = "chkadvancesearch"
        Me.chkadvancesearch.Size = New System.Drawing.Size(106, 17)
        Me.chkadvancesearch.TabIndex = 4
        Me.chkadvancesearch.Text = "Advance Search"
        Me.chkadvancesearch.UseVisualStyleBackColor = True
        '
        'city_treasurer
        '
        Me.city_treasurer.AutoSize = True
        Me.city_treasurer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_treasurer.Location = New System.Drawing.Point(687, 70)
        Me.city_treasurer.Name = "city_treasurer"
        Me.city_treasurer.Size = New System.Drawing.Size(103, 15)
        Me.city_treasurer.TabIndex = 110
        Me.city_treasurer.Text = "CITY TREASURER"
        Me.city_treasurer.Visible = False
        '
        'city_accountant
        '
        Me.city_accountant.AutoSize = True
        Me.city_accountant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_accountant.Location = New System.Drawing.Point(687, 91)
        Me.city_accountant.Name = "city_accountant"
        Me.city_accountant.Size = New System.Drawing.Size(110, 15)
        Me.city_accountant.TabIndex = 111
        Me.city_accountant.Text = "CITY ACCOUNTANT"
        Me.city_accountant.Visible = False
        '
        'city_budget
        '
        Me.city_budget.AutoSize = True
        Me.city_budget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.city_budget.Location = New System.Drawing.Point(687, 112)
        Me.city_budget.Name = "city_budget"
        Me.city_budget.Size = New System.Drawing.Size(81, 15)
        Me.city_budget.TabIndex = 112
        Me.city_budget.Text = "CITY BUDGET"
        Me.city_budget.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.IndianRed
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Location = New System.Drawing.Point(2, 219)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(10, 464)
        Me.Panel5.TabIndex = 113
        '
        'rad_date_received_supplier
        '
        Me.rad_date_received_supplier.AutoSize = True
        Me.rad_date_received_supplier.Location = New System.Drawing.Point(6, 16)
        Me.rad_date_received_supplier.Name = "rad_date_received_supplier"
        Me.rad_date_received_supplier.Size = New System.Drawing.Size(138, 17)
        Me.rad_date_received_supplier.TabIndex = 114
        Me.rad_date_received_supplier.TabStop = True
        Me.rad_date_received_supplier.Text = "Date Received Supplier"
        Me.rad_date_received_supplier.UseVisualStyleBackColor = True
        '
        'rad_View_ALL
        '
        Me.rad_View_ALL.AutoSize = True
        Me.rad_View_ALL.Location = New System.Drawing.Point(492, 15)
        Me.rad_View_ALL.Name = "rad_View_ALL"
        Me.rad_View_ALL.Size = New System.Drawing.Size(62, 17)
        Me.rad_View_ALL.TabIndex = 115
        Me.rad_View_ALL.TabStop = True
        Me.rad_View_ALL.Text = "View All"
        Me.rad_View_ALL.UseVisualStyleBackColor = True
        '
        'rad_Date_COA_Received
        '
        Me.rad_Date_COA_Received.AutoSize = True
        Me.rad_Date_COA_Received.Location = New System.Drawing.Point(144, 17)
        Me.rad_Date_COA_Received.Name = "rad_Date_COA_Received"
        Me.rad_Date_COA_Received.Size = New System.Drawing.Size(128, 17)
        Me.rad_Date_COA_Received.TabIndex = 116
        Me.rad_Date_COA_Received.TabStop = True
        Me.rad_Date_COA_Received.Text = "Date_COA_Received"
        Me.rad_Date_COA_Received.UseVisualStyleBackColor = True
        '
        'rad_Date_Approved_PO
        '
        Me.rad_Date_Approved_PO.AutoSize = True
        Me.rad_Date_Approved_PO.Location = New System.Drawing.Point(273, 16)
        Me.rad_Date_Approved_PO.Name = "rad_Date_Approved_PO"
        Me.rad_Date_Approved_PO.Size = New System.Drawing.Size(121, 17)
        Me.rad_Date_Approved_PO.TabIndex = 117
        Me.rad_Date_Approved_PO.TabStop = True
        Me.rad_Date_Approved_PO.Text = "Date_Approved_PO"
        Me.rad_Date_Approved_PO.UseVisualStyleBackColor = True
        '
        'group_admin
        '
        Me.group_admin.Controls.Add(Me.rad_or)
        Me.group_admin.Controls.Add(Me.rad_Blank_All)
        Me.group_admin.Controls.Add(Me.rad_date_received_supplier)
        Me.group_admin.Controls.Add(Me.rad_View_ALL)
        Me.group_admin.Controls.Add(Me.rad_Date_Approved_PO)
        Me.group_admin.Controls.Add(Me.rad_Date_COA_Received)
        Me.group_admin.ForeColor = System.Drawing.Color.IndianRed
        Me.group_admin.Location = New System.Drawing.Point(121, 98)
        Me.group_admin.Name = "group_admin"
        Me.group_admin.Size = New System.Drawing.Size(560, 40)
        Me.group_admin.TabIndex = 118
        Me.group_admin.TabStop = False
        Me.group_admin.Text = "Admin Search"
        '
        'rad_or
        '
        Me.rad_or.AutoSize = True
        Me.rad_or.Location = New System.Drawing.Point(450, 15)
        Me.rad_or.Name = "rad_or"
        Me.rad_or.Size = New System.Drawing.Size(36, 17)
        Me.rad_or.TabIndex = 119
        Me.rad_or.TabStop = True
        Me.rad_or.Text = "Or"
        Me.rad_or.UseVisualStyleBackColor = True
        '
        'rad_Blank_All
        '
        Me.rad_Blank_All.AutoSize = True
        Me.rad_Blank_All.Location = New System.Drawing.Point(400, 16)
        Me.rad_Blank_All.Name = "rad_Blank_All"
        Me.rad_Blank_All.Size = New System.Drawing.Size(44, 17)
        Me.rad_Blank_All.TabIndex = 118
        Me.rad_Blank_All.TabStop = True
        Me.rad_Blank_All.Text = "And"
        Me.rad_Blank_All.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(1068, 636)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 15)
        Me.Label11.TabIndex = 119
        Me.Label11.Text = "Total Cost:"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.Color.Green
        Me.txtSubTotal.Location = New System.Drawing.Point(1138, 630)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(145, 27)
        Me.txtSubTotal.TabIndex = 156
        Me.txtSubTotal.Text = "0.00"
        '
        'groupadminControl
        '
        Me.groupadminControl.BackColor = System.Drawing.Color.BurlyWood
        Me.groupadminControl.Controls.Add(Me.btnUNLOCK)
        Me.groupadminControl.Controls.Add(Me.btnLOCK)
        Me.groupadminControl.Location = New System.Drawing.Point(897, 172)
        Me.groupadminControl.Name = "groupadminControl"
        Me.groupadminControl.Size = New System.Drawing.Size(159, 44)
        Me.groupadminControl.TabIndex = 157
        Me.groupadminControl.TabStop = False
        Me.groupadminControl.Text = "CONTROLS"
        '
        'btnUNLOCK
        '
        Me.btnUNLOCK.AnimationHoverSpeed = 0.07!
        Me.btnUNLOCK.AnimationSpeed = 0.03!
        Me.btnUNLOCK.BackColor = System.Drawing.Color.Transparent
        Me.btnUNLOCK.BaseColor = System.Drawing.Color.White
        Me.btnUNLOCK.BorderColor = System.Drawing.Color.Black
        Me.btnUNLOCK.BorderSize = 1
        Me.btnUNLOCK.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnUNLOCK.FocusedColor = System.Drawing.Color.Empty
        Me.btnUNLOCK.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnUNLOCK.ForeColor = System.Drawing.Color.Black
        Me.btnUNLOCK.Image = Nothing
        Me.btnUNLOCK.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnUNLOCK.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnUNLOCK.Location = New System.Drawing.Point(81, 19)
        Me.btnUNLOCK.Name = "btnUNLOCK"
        Me.btnUNLOCK.OnHoverBaseColor = System.Drawing.Color.LightGreen
        Me.btnUNLOCK.OnHoverBorderColor = System.Drawing.Color.LightGreen
        Me.btnUNLOCK.OnHoverForeColor = System.Drawing.Color.White
        Me.btnUNLOCK.OnHoverImage = Nothing
        Me.btnUNLOCK.OnPressedColor = System.Drawing.Color.Black
        Me.btnUNLOCK.Radius = 15
        Me.btnUNLOCK.Size = New System.Drawing.Size(64, 21)
        Me.btnUNLOCK.TabIndex = 234
        Me.btnUNLOCK.Text = "UNLOCK"
        Me.btnUNLOCK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnUNLOCK.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnLOCK
        '
        Me.btnLOCK.AnimationHoverSpeed = 0.07!
        Me.btnLOCK.AnimationSpeed = 0.03!
        Me.btnLOCK.BackColor = System.Drawing.Color.Transparent
        Me.btnLOCK.BaseColor = System.Drawing.Color.White
        Me.btnLOCK.BorderColor = System.Drawing.Color.Black
        Me.btnLOCK.BorderSize = 1
        Me.btnLOCK.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnLOCK.FocusedColor = System.Drawing.Color.Empty
        Me.btnLOCK.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnLOCK.ForeColor = System.Drawing.Color.Black
        Me.btnLOCK.Image = Nothing
        Me.btnLOCK.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnLOCK.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnLOCK.Location = New System.Drawing.Point(6, 19)
        Me.btnLOCK.Name = "btnLOCK"
        Me.btnLOCK.OnHoverBaseColor = System.Drawing.Color.Firebrick
        Me.btnLOCK.OnHoverBorderColor = System.Drawing.Color.Firebrick
        Me.btnLOCK.OnHoverForeColor = System.Drawing.Color.White
        Me.btnLOCK.OnHoverImage = Nothing
        Me.btnLOCK.OnPressedColor = System.Drawing.Color.Black
        Me.btnLOCK.Radius = 15
        Me.btnLOCK.Size = New System.Drawing.Size(69, 21)
        Me.btnLOCK.TabIndex = 233
        Me.btnLOCK.Text = "LOCK"
        Me.btnLOCK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnLOCK.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'PO_Number
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        Me.PO_Number.DefaultCellStyle = DataGridViewCellStyle2
        Me.PO_Number.FillWeight = 110.0!
        Me.PO_Number.HeaderText = "Purchase Order Number"
        Me.PO_Number.Name = "PO_Number"
        Me.PO_Number.ReadOnly = True
        '
        'PR_Number
        '
        Me.PR_Number.FillWeight = 70.0!
        Me.PR_Number.HeaderText = "Purchase Request Number"
        Me.PR_Number.Name = "PR_Number"
        Me.PR_Number.ReadOnly = True
        '
        'Barcode_Number
        '
        Me.Barcode_Number.FillWeight = 70.0!
        Me.Barcode_Number.HeaderText = "Barcode Number"
        Me.Barcode_Number.Name = "Barcode_Number"
        Me.Barcode_Number.ReadOnly = True
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
        Me.TradeName.FillWeight = 70.0!
        Me.TradeName.HeaderText = "Supplier Name"
        Me.TradeName.Name = "TradeName"
        Me.TradeName.ReadOnly = True
        '
        'Supplier_Address
        '
        Me.Supplier_Address.FillWeight = 70.0!
        Me.Supplier_Address.HeaderText = "Supplier Address"
        Me.Supplier_Address.Name = "Supplier_Address"
        Me.Supplier_Address.ReadOnly = True
        '
        'Purpose
        '
        Me.Purpose.FillWeight = 110.0!
        Me.Purpose.HeaderText = "Purpose"
        Me.Purpose.Name = "Purpose"
        Me.Purpose.ReadOnly = True
        '
        'Category
        '
        Me.Category.FillWeight = 70.0!
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        '
        'Date_Created
        '
        Me.Date_Created.FillWeight = 70.0!
        Me.Date_Created.HeaderText = "Date Created"
        Me.Date_Created.Name = "Date_Created"
        Me.Date_Created.ReadOnly = True
        '
        'PR_Received_Name
        '
        Me.PR_Received_Name.FillWeight = 70.0!
        Me.PR_Received_Name.HeaderText = "PR Received Name"
        Me.PR_Received_Name.Name = "PR_Received_Name"
        Me.PR_Received_Name.ReadOnly = True
        '
        'Date_Received_Supplier
        '
        Me.Date_Received_Supplier.FillWeight = 70.0!
        Me.Date_Received_Supplier.HeaderText = "Date Received Supplier"
        Me.Date_Received_Supplier.Name = "Date_Received_Supplier"
        Me.Date_Received_Supplier.ReadOnly = True
        '
        'COA_Date_Received
        '
        Me.COA_Date_Received.FillWeight = 70.0!
        Me.COA_Date_Received.HeaderText = "COA Date Received"
        Me.COA_Date_Received.Name = "COA_Date_Received"
        Me.COA_Date_Received.ReadOnly = True
        '
        'Date_Approved_PO
        '
        Me.Date_Approved_PO.FillWeight = 70.0!
        Me.Date_Approved_PO.HeaderText = "Approved PO Date"
        Me.Date_Approved_PO.Name = "Date_Approved_PO"
        Me.Date_Approved_PO.ReadOnly = True
        '
        'Remarks
        '
        Me.Remarks.FillWeight = 70.0!
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.ReadOnly = True
        '
        'PO_Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1313, 696)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.group_admin)
        Me.Controls.Add(Me.chkadvancesearch)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.groupadminControl)
        Me.Controls.Add(Me.groupadvancesearch)
        Me.Controls.Add(Me.city_budget)
        Me.Controls.Add(Me.city_accountant)
        Me.Controls.Add(Me.city_treasurer)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.city_mayor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dgvPO_LIST)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvPO_ITEMLIST)
        Me.Name = "PO_Main_Form"
        Me.Text = "Purchase Order"
        CType(Me.dgvPO_LIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.dgvPO_ITEMLIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.groupadvancesearch.ResumeLayout(False)
        Me.groupadvancesearch.PerformLayout()
        Me.group_admin.ResumeLayout(False)
        Me.group_admin.PerformLayout()
        Me.groupadminControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvPO_LIST As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvPO_ITEMLIST As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPrintPO As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents combo_search As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchBox As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents city_mayor As System.Windows.Forms.Label
    Friend WithEvents groupadvancesearch As System.Windows.Forms.GroupBox
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents city_treasurer As System.Windows.Forms.Label
    Friend WithEvents city_accountant As System.Windows.Forms.Label
    Friend WithEvents city_budget As System.Windows.Forms.Label
    Friend WithEvents btnPrintObligationRequest As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents AddDateReceivedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddCoaDateReceivedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddRemarksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Item_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Item_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkadvancesearch As System.Windows.Forms.CheckBox
    Friend WithEvents AddPRReceivedNameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddApprovedPODateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddCategoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rad_date_received_supplier As System.Windows.Forms.RadioButton
    Friend WithEvents rad_View_ALL As System.Windows.Forms.RadioButton
    Friend WithEvents rad_Date_COA_Received As System.Windows.Forms.RadioButton
    Friend WithEvents rad_Date_Approved_PO As System.Windows.Forms.RadioButton
    Friend WithEvents group_admin As System.Windows.Forms.GroupBox
    Friend WithEvents rad_Blank_All As System.Windows.Forms.RadioButton
    Friend WithEvents rad_or As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents groupadminControl As System.Windows.Forms.GroupBox
    Friend WithEvents btnUNLOCK As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnLOCK As Guna.UI.WinForms.GunaButton
    Friend WithEvents PO_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PR_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TradeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Purpose As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PR_Received_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Received_Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COA_Date_Received As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Approved_PO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
