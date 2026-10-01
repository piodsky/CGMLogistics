<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CGSO_Main_Form
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
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.Guna2GroupBox5 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.btnNew = New System.Windows.Forms.Button
        Me.GunaButton2 = New Guna.UI.WinForms.GunaButton
        Me.btnEdit = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Guna2GroupBox3 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.dgvInventory = New System.Windows.Forms.DataGridView
        Me.Date_Created = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PO_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PR_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Purpose = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Guna2GroupBox2 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.combo_search = New System.Windows.Forms.ComboBox
        Me.chkadvancesearch = New System.Windows.Forms.CheckBox
        Me.groupadvancesearch = New System.Windows.Forms.GroupBox
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtSearchBox = New System.Windows.Forms.TextBox
        Me.GunaButton1 = New Guna.UI.WinForms.GunaButton
        Me.btnClear = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Guna2GroupBox1.SuspendLayout()
        Me.Guna2GroupBox5.SuspendLayout()
        Me.Guna2GroupBox3.SuspendLayout()
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Guna2GroupBox2.SuspendLayout()
        Me.groupadvancesearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.Controls.Add(Me.Guna2GroupBox5)
        Me.Guna2GroupBox1.Controls.Add(Me.Guna2GroupBox3)
        Me.Guna2GroupBox1.Controls.Add(Me.Guna2GroupBox2)
        Me.Guna2GroupBox1.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox1.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Colonna MT", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(0, -1)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.ShadowDecoration.Parent = Me.Guna2GroupBox1
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(1522, 790)
        Me.Guna2GroupBox1.TabIndex = 229
        Me.Guna2GroupBox1.Text = "Inventory Management"
        Me.Guna2GroupBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox1.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'Guna2GroupBox5
        '
        Me.Guna2GroupBox5.Controls.Add(Me.btnNew)
        Me.Guna2GroupBox5.Controls.Add(Me.GunaButton2)
        Me.Guna2GroupBox5.Controls.Add(Me.btnEdit)
        Me.Guna2GroupBox5.Controls.Add(Me.Button2)
        Me.Guna2GroupBox5.Controls.Add(Me.Label5)
        Me.Guna2GroupBox5.Controls.Add(Me.Label7)
        Me.Guna2GroupBox5.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox5.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox5.Location = New System.Drawing.Point(1002, 47)
        Me.Guna2GroupBox5.Name = "Guna2GroupBox5"
        Me.Guna2GroupBox5.ShadowDecoration.Parent = Me.Guna2GroupBox5
        Me.Guna2GroupBox5.Size = New System.Drawing.Size(325, 181)
        Me.Guna2GroupBox5.TabIndex = 229
        Me.Guna2GroupBox5.Text = "Menu"
        Me.Guna2GroupBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox5.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox5.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.White
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.btnNew.Location = New System.Drawing.Point(19, 49)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(45, 38)
        Me.btnNew.TabIndex = 95
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'GunaButton2
        '
        Me.GunaButton2.AnimationHoverSpeed = 0.07!
        Me.GunaButton2.AnimationSpeed = 0.03!
        Me.GunaButton2.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton2.BaseColor = System.Drawing.Color.White
        Me.GunaButton2.BorderColor = System.Drawing.Color.Black
        Me.GunaButton2.BorderSize = 1
        Me.GunaButton2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton2.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton2.Font = New System.Drawing.Font("Calisto MT", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaButton2.ForeColor = System.Drawing.Color.Black
        Me.GunaButton2.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.GunaButton2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.GunaButton2.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton2.Location = New System.Drawing.Point(1094, 405)
        Me.GunaButton2.Name = "GunaButton2"
        Me.GunaButton2.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.GunaButton2.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton2.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton2.OnHoverImage = Nothing
        Me.GunaButton2.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton2.Radius = 15
        Me.GunaButton2.Size = New System.Drawing.Size(218, 44)
        Me.GunaButton2.TabIndex = 225
        Me.GunaButton2.Text = "SAVE"
        Me.GunaButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.GunaButton2.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.White
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Image = Global.Purchase_Request.My.Resources.Resources.edit_small
        Me.btnEdit.Location = New System.Drawing.Point(82, 49)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(45, 38)
        Me.btnEdit.TabIndex = 96
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Green
        Me.Button2.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(1222, 56)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(90, 30)
        Me.Button2.TabIndex = 21
        Me.Button2.Text = "Add Row"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(89, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 15)
        Me.Label5.TabIndex = 99
        Me.Label5.Text = "View"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label7.ForeColor = System.Drawing.Color.Green
        Me.Label7.Location = New System.Drawing.Point(24, 89)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 15)
        Me.Label7.TabIndex = 98
        Me.Label7.Text = "New"
        '
        'Guna2GroupBox3
        '
        Me.Guna2GroupBox3.Controls.Add(Me.dgvInventory)
        Me.Guna2GroupBox3.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox3.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox3.Location = New System.Drawing.Point(15, 234)
        Me.Guna2GroupBox3.Name = "Guna2GroupBox3"
        Me.Guna2GroupBox3.ShadowDecoration.Parent = Me.Guna2GroupBox3
        Me.Guna2GroupBox3.Size = New System.Drawing.Size(1312, 500)
        Me.Guna2GroupBox3.TabIndex = 228
        Me.Guna2GroupBox3.Text = "CGSO Inventory List"
        Me.Guna2GroupBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox3.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'dgvInventory
        '
        Me.dgvInventory.AllowUserToAddRows = False
        Me.dgvInventory.AllowUserToDeleteRows = False
        Me.dgvInventory.AllowUserToResizeColumns = False
        Me.dgvInventory.AllowUserToResizeRows = False
        Me.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvInventory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvInventory.BackgroundColor = System.Drawing.Color.DarkSeaGreen
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(6)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInventory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInventory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Date_Created, Me.PO_Number, Me.PR_Number, Me.Department, Me.Supplier_Name, Me.Purpose})
        Me.dgvInventory.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInventory.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInventory.Location = New System.Drawing.Point(12, 52)
        Me.dgvInventory.Name = "dgvInventory"
        Me.dgvInventory.ReadOnly = True
        Me.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInventory.Size = New System.Drawing.Size(1284, 436)
        Me.dgvInventory.TabIndex = 20
        '
        'Date_Created
        '
        Me.Date_Created.FillWeight = 70.0!
        Me.Date_Created.HeaderText = "Date Created"
        Me.Date_Created.Name = "Date_Created"
        Me.Date_Created.ReadOnly = True
        '
        'PO_Number
        '
        Me.PO_Number.FillWeight = 80.0!
        Me.PO_Number.HeaderText = "Purchase Order Number"
        Me.PO_Number.Name = "PO_Number"
        Me.PO_Number.ReadOnly = True
        '
        'PR_Number
        '
        Me.PR_Number.FillWeight = 80.0!
        Me.PR_Number.HeaderText = "Purchase Request Number"
        Me.PR_Number.Name = "PR_Number"
        Me.PR_Number.ReadOnly = True
        '
        'Department
        '
        Me.Department.FillWeight = 120.0!
        Me.Department.HeaderText = "Department / Offices / Barangays"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        '
        'Supplier_Name
        '
        Me.Supplier_Name.FillWeight = 120.0!
        Me.Supplier_Name.HeaderText = "Supplier"
        Me.Supplier_Name.Name = "Supplier_Name"
        Me.Supplier_Name.ReadOnly = True
        '
        'Purpose
        '
        Me.Purpose.FillWeight = 200.0!
        Me.Purpose.HeaderText = "Purpose"
        Me.Purpose.Name = "Purpose"
        Me.Purpose.ReadOnly = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewUpdateToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(143, 26)
        '
        'ViewUpdateToolStripMenuItem
        '
        Me.ViewUpdateToolStripMenuItem.Name = "ViewUpdateToolStripMenuItem"
        Me.ViewUpdateToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.ViewUpdateToolStripMenuItem.Text = "View/Update"
        '
        'Guna2GroupBox2
        '
        Me.Guna2GroupBox2.BackColor = System.Drawing.Color.White
        Me.Guna2GroupBox2.Controls.Add(Me.combo_search)
        Me.Guna2GroupBox2.Controls.Add(Me.chkadvancesearch)
        Me.Guna2GroupBox2.Controls.Add(Me.groupadvancesearch)
        Me.Guna2GroupBox2.Controls.Add(Me.txtSearchBox)
        Me.Guna2GroupBox2.Controls.Add(Me.GunaButton1)
        Me.Guna2GroupBox2.Controls.Add(Me.btnClear)
        Me.Guna2GroupBox2.Controls.Add(Me.Button1)
        Me.Guna2GroupBox2.Controls.Add(Me.btnSearch)
        Me.Guna2GroupBox2.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox2.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox2.Location = New System.Drawing.Point(15, 47)
        Me.Guna2GroupBox2.Name = "Guna2GroupBox2"
        Me.Guna2GroupBox2.ShadowDecoration.Parent = Me.Guna2GroupBox2
        Me.Guna2GroupBox2.Size = New System.Drawing.Size(981, 181)
        Me.Guna2GroupBox2.TabIndex = 227
        Me.Guna2GroupBox2.Text = "Searchbox"
        Me.Guna2GroupBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox2.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'combo_search
        '
        Me.combo_search.BackColor = System.Drawing.Color.White
        Me.combo_search.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.combo_search.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.combo_search.ForeColor = System.Drawing.Color.Black
        Me.combo_search.FormattingEnabled = True
        Me.combo_search.Items.AddRange(New Object() {"VIEW ALL", "PO_Number", "PR_Number", "Department", "Supplier"})
        Me.combo_search.Location = New System.Drawing.Point(12, 54)
        Me.combo_search.Name = "combo_search"
        Me.combo_search.Size = New System.Drawing.Size(168, 25)
        Me.combo_search.TabIndex = 226
        '
        'chkadvancesearch
        '
        Me.chkadvancesearch.AutoSize = True
        Me.chkadvancesearch.BackColor = System.Drawing.Color.AntiqueWhite
        Me.chkadvancesearch.Location = New System.Drawing.Point(12, 90)
        Me.chkadvancesearch.Name = "chkadvancesearch"
        Me.chkadvancesearch.Size = New System.Drawing.Size(110, 19)
        Me.chkadvancesearch.TabIndex = 110
        Me.chkadvancesearch.Text = "Advance Search"
        Me.chkadvancesearch.UseVisualStyleBackColor = False
        '
        'groupadvancesearch
        '
        Me.groupadvancesearch.BackColor = System.Drawing.Color.AntiqueWhite
        Me.groupadvancesearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.groupadvancesearch.Controls.Add(Me.datefrom)
        Me.groupadvancesearch.Controls.Add(Me.Label9)
        Me.groupadvancesearch.Controls.Add(Me.dateto)
        Me.groupadvancesearch.Controls.Add(Me.Label10)
        Me.groupadvancesearch.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupadvancesearch.ForeColor = System.Drawing.Color.Black
        Me.groupadvancesearch.Location = New System.Drawing.Point(12, 103)
        Me.groupadvancesearch.Name = "groupadvancesearch"
        Me.groupadvancesearch.Size = New System.Drawing.Size(667, 66)
        Me.groupadvancesearch.TabIndex = 111
        Me.groupadvancesearch.TabStop = False
        '
        'datefrom
        '
        Me.datefrom.Location = New System.Drawing.Point(95, 14)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(268, 22)
        Me.datefrom.TabIndex = 68
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(46, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 15)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "FROM"
        '
        'dateto
        '
        Me.dateto.Location = New System.Drawing.Point(95, 40)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(268, 22)
        Me.dateto.TabIndex = 65
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(54, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(22, 15)
        Me.Label10.TabIndex = 67
        Me.Label10.Text = "TO"
        '
        'txtSearchBox
        '
        Me.txtSearchBox.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchBox.Location = New System.Drawing.Point(186, 56)
        Me.txtSearchBox.Name = "txtSearchBox"
        Me.txtSearchBox.Size = New System.Drawing.Size(277, 23)
        Me.txtSearchBox.TabIndex = 2
        '
        'GunaButton1
        '
        Me.GunaButton1.AnimationHoverSpeed = 0.07!
        Me.GunaButton1.AnimationSpeed = 0.03!
        Me.GunaButton1.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton1.BaseColor = System.Drawing.Color.White
        Me.GunaButton1.BorderColor = System.Drawing.Color.Black
        Me.GunaButton1.BorderSize = 1
        Me.GunaButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton1.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton1.Font = New System.Drawing.Font("Calisto MT", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaButton1.ForeColor = System.Drawing.Color.Black
        Me.GunaButton1.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.GunaButton1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.GunaButton1.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton1.Location = New System.Drawing.Point(1094, 405)
        Me.GunaButton1.Name = "GunaButton1"
        Me.GunaButton1.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.GunaButton1.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton1.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton1.OnHoverImage = Nothing
        Me.GunaButton1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton1.Radius = 15
        Me.GunaButton1.Size = New System.Drawing.Size(218, 44)
        Me.GunaButton1.TabIndex = 225
        Me.GunaButton1.Text = "SAVE"
        Me.GunaButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.GunaButton1.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(581, 52)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(96, 30)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Green
        Me.Button1.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(1222, 56)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 30)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "Add Row"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Blue
        Me.btnSearch.Location = New System.Drawing.Point(479, 52)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(96, 30)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'CGSO_Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1335, 755)
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Name = "CGSO_Main_Form"
        Me.Text = "Inventory Management"
        Me.Guna2GroupBox1.ResumeLayout(False)
        Me.Guna2GroupBox5.ResumeLayout(False)
        Me.Guna2GroupBox5.PerformLayout()
        Me.Guna2GroupBox3.ResumeLayout(False)
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Guna2GroupBox2.ResumeLayout(False)
        Me.Guna2GroupBox2.PerformLayout()
        Me.groupadvancesearch.ResumeLayout(False)
        Me.groupadvancesearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents Guna2GroupBox5 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents GunaButton2 As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Guna2GroupBox3 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents dgvInventory As System.Windows.Forms.DataGridView
    Friend WithEvents Guna2GroupBox2 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents chkadvancesearch As System.Windows.Forms.CheckBox
    Friend WithEvents groupadvancesearch As System.Windows.Forms.GroupBox
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSearchBox As System.Windows.Forms.TextBox
    Friend WithEvents GunaButton1 As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents combo_search As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ViewUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Date_Created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PO_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PR_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Purpose As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
