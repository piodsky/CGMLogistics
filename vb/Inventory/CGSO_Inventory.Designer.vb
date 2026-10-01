<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CGSO_Inventory
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
        Me.GunaGroupBox1 = New Guna.UI.WinForms.GunaGroupBox
        Me.groupprint = New Guna.UI2.WinForms.Guna2GroupBox
        Me.printICSHighValueRenew = New Guna.UI.WinForms.GunaButton
        Me.btnPARrenew = New Guna.UI.WinForms.GunaButton
        Me.btnPropertyCardSemiEXP = New Guna.UI.WinForms.GunaButton
        Me.btnPrintSmallTagForm = New Guna.UI.WinForms.GunaButton
        Me.btnInventoryTransferForm = New Guna.UI.WinForms.GunaButton
        Me.btnPropertyTransferForm = New Guna.UI.WinForms.GunaButton
        Me.btnICSHighValueBYLOT = New Guna.UI.WinForms.GunaButton
        Me.btnPARBYLOT = New Guna.UI.WinForms.GunaButton
        Me.btnICSLowValueBYLOT = New Guna.UI.WinForms.GunaButton
        Me.btnICSLowValue = New Guna.UI.WinForms.GunaButton
        Me.btnPrintTagForm = New Guna.UI.WinForms.GunaButton
        Me.btnPAR = New Guna.UI.WinForms.GunaButton
        Me.btnICSHighValue = New Guna.UI.WinForms.GunaButton
        Me.GunaButton2 = New Guna.UI.WinForms.GunaButton
        Me.Button2 = New System.Windows.Forms.Button
        Me.warehouse_details = New Guna.UI2.WinForms.Guna2GroupBox
        Me.txtSource = New System.Windows.Forms.ComboBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.combo_mode = New System.Windows.Forms.ComboBox
        Me.combo_supplier = New System.Windows.Forms.ComboBox
        Me.combo_category = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.po_date = New Guna.UI2.WinForms.Guna2DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtInitial = New Guna.UI.WinForms.GunaTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.purpose = New Guna.UI.WinForms.GunaTextBox
        Me.combo_supplier_address = New Guna.UI.WinForms.GunaTextBox
        Me.txtPONUMBER = New Guna.UI.WinForms.GunaTextBox
        Me.txtPRNUMBER = New Guna.UI.WinForms.GunaTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtDepartment = New Guna.UI.WinForms.GunaTextBox
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtPTRITRNumber = New Guna.UI.WinForms.GunaTextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtTotal_Amount = New Guna.UI.WinForms.GunaTextBox
        Me.chkPrintAll = New System.Windows.Forms.CheckBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPARICSNumber = New Guna.UI.WinForms.GunaTextBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DuplicateRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddSubDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtEncoded = New System.Windows.Forms.TextBox
        Me.lblbarcode = New System.Windows.Forms.Label
        Me.txtBarcode = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtcgsoposition = New Guna.UI.WinForms.GunaTextBox
        Me.txtcgsoofficer = New Guna.UI.WinForms.GunaTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnImport = New System.Windows.Forms.Button
        Me.Date_Entry = New Guna.UI2.WinForms.Guna2DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnEdit = New Guna.UI.WinForms.GunaButton
        Me.btnDelete = New Guna.UI.WinForms.GunaButton
        Me.btnSelectPO = New System.Windows.Forms.Button
        Me.txtTotalItems = New Guna.UI.WinForms.GunaTextBox
        Me.btnSave = New Guna.UI.WinForms.GunaButton
        Me.txtSubTotal = New Guna.UI.WinForms.GunaTextBox
        Me.AddRow = New System.Windows.Forms.Button
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.inventory_state = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.dgvNewInventory = New System.Windows.Forms.DataGridView
        Me.print = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Inv_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Item_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Items_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Total_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Serial_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Tag_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Inventory_Category = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PAR_ICS_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Accountable_Officer = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Position = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Location = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Classification = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Condition = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.NumberSeries = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PTR_ITR_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Estimated_Life = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GunaGroupBox1.SuspendLayout()
        Me.groupprint.SuspendLayout()
        Me.warehouse_details.SuspendLayout()
        Me.Guna2GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgvNewInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GunaGroupBox1
        '
        Me.GunaGroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GunaGroupBox1.BaseColor = System.Drawing.Color.AntiqueWhite
        Me.GunaGroupBox1.BorderColor = System.Drawing.Color.Black
        Me.GunaGroupBox1.Controls.Add(Me.groupprint)
        Me.GunaGroupBox1.Controls.Add(Me.warehouse_details)
        Me.GunaGroupBox1.Controls.Add(Me.Guna2GroupBox1)
        Me.GunaGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GunaGroupBox1.Font = New System.Drawing.Font("Colonna MT", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaGroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GunaGroupBox1.LineColor = System.Drawing.Color.MediumSeaGreen
        Me.GunaGroupBox1.Location = New System.Drawing.Point(-1, 0)
        Me.GunaGroupBox1.Name = "GunaGroupBox1"
        Me.GunaGroupBox1.Size = New System.Drawing.Size(1520, 796)
        Me.GunaGroupBox1.TabIndex = 23
        Me.GunaGroupBox1.Text = "Inventory Management "
        Me.GunaGroupBox1.TextLocation = New System.Drawing.Point(10, 8)
        Me.GunaGroupBox1.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'groupprint
        '
        Me.groupprint.Controls.Add(Me.printICSHighValueRenew)
        Me.groupprint.Controls.Add(Me.btnPARrenew)
        Me.groupprint.Controls.Add(Me.btnPropertyCardSemiEXP)
        Me.groupprint.Controls.Add(Me.btnPrintSmallTagForm)
        Me.groupprint.Controls.Add(Me.btnInventoryTransferForm)
        Me.groupprint.Controls.Add(Me.btnPropertyTransferForm)
        Me.groupprint.Controls.Add(Me.btnICSHighValueBYLOT)
        Me.groupprint.Controls.Add(Me.btnPARBYLOT)
        Me.groupprint.Controls.Add(Me.btnICSLowValueBYLOT)
        Me.groupprint.Controls.Add(Me.btnICSLowValue)
        Me.groupprint.Controls.Add(Me.btnPrintTagForm)
        Me.groupprint.Controls.Add(Me.btnPAR)
        Me.groupprint.Controls.Add(Me.btnICSHighValue)
        Me.groupprint.Controls.Add(Me.GunaButton2)
        Me.groupprint.Controls.Add(Me.Button2)
        Me.groupprint.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.groupprint.FillColor = System.Drawing.Color.AntiqueWhite
        Me.groupprint.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.groupprint.ForeColor = System.Drawing.Color.Black
        Me.groupprint.Location = New System.Drawing.Point(900, 34)
        Me.groupprint.Name = "groupprint"
        Me.groupprint.ShadowDecoration.Parent = Me.groupprint
        Me.groupprint.Size = New System.Drawing.Size(441, 225)
        Me.groupprint.TabIndex = 230
        Me.groupprint.Text = "Print"
        Me.groupprint.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.groupprint.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.groupprint.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'printICSHighValueRenew
        '
        Me.printICSHighValueRenew.AnimationHoverSpeed = 0.07!
        Me.printICSHighValueRenew.AnimationSpeed = 0.03!
        Me.printICSHighValueRenew.BackColor = System.Drawing.Color.Transparent
        Me.printICSHighValueRenew.BaseColor = System.Drawing.Color.White
        Me.printICSHighValueRenew.BorderColor = System.Drawing.Color.Black
        Me.printICSHighValueRenew.BorderSize = 1
        Me.printICSHighValueRenew.DialogResult = System.Windows.Forms.DialogResult.None
        Me.printICSHighValueRenew.FocusedColor = System.Drawing.Color.Empty
        Me.printICSHighValueRenew.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.printICSHighValueRenew.ForeColor = System.Drawing.Color.Red
        Me.printICSHighValueRenew.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.printICSHighValueRenew.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.printICSHighValueRenew.ImageSize = New System.Drawing.Size(20, 20)
        Me.printICSHighValueRenew.Location = New System.Drawing.Point(227, 194)
        Me.printICSHighValueRenew.Name = "printICSHighValueRenew"
        Me.printICSHighValueRenew.OnHoverBaseColor = System.Drawing.Color.CornflowerBlue
        Me.printICSHighValueRenew.OnHoverBorderColor = System.Drawing.Color.CornflowerBlue
        Me.printICSHighValueRenew.OnHoverForeColor = System.Drawing.Color.White
        Me.printICSHighValueRenew.OnHoverImage = Nothing
        Me.printICSHighValueRenew.OnPressedColor = System.Drawing.Color.Black
        Me.printICSHighValueRenew.Radius = 15
        Me.printICSHighValueRenew.Size = New System.Drawing.Size(202, 25)
        Me.printICSHighValueRenew.TabIndex = 246
        Me.printICSHighValueRenew.Text = "Print ICS High Value - Renew"
        Me.printICSHighValueRenew.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.printICSHighValueRenew.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPARrenew
        '
        Me.btnPARrenew.AnimationHoverSpeed = 0.07!
        Me.btnPARrenew.AnimationSpeed = 0.03!
        Me.btnPARrenew.BackColor = System.Drawing.Color.Transparent
        Me.btnPARrenew.BaseColor = System.Drawing.Color.White
        Me.btnPARrenew.BorderColor = System.Drawing.Color.Black
        Me.btnPARrenew.BorderSize = 1
        Me.btnPARrenew.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPARrenew.FocusedColor = System.Drawing.Color.Empty
        Me.btnPARrenew.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPARrenew.ForeColor = System.Drawing.Color.Red
        Me.btnPARrenew.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPARrenew.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPARrenew.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPARrenew.Location = New System.Drawing.Point(227, 168)
        Me.btnPARrenew.Name = "btnPARrenew"
        Me.btnPARrenew.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnPARrenew.OnHoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnPARrenew.OnHoverForeColor = System.Drawing.Color.White
        Me.btnPARrenew.OnHoverImage = Nothing
        Me.btnPARrenew.OnPressedColor = System.Drawing.Color.Black
        Me.btnPARrenew.Radius = 15
        Me.btnPARrenew.Size = New System.Drawing.Size(201, 25)
        Me.btnPARrenew.TabIndex = 245
        Me.btnPARrenew.Text = "Print PAR - Renew"
        Me.btnPARrenew.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPARrenew.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPropertyCardSemiEXP
        '
        Me.btnPropertyCardSemiEXP.AnimationHoverSpeed = 0.07!
        Me.btnPropertyCardSemiEXP.AnimationSpeed = 0.03!
        Me.btnPropertyCardSemiEXP.BackColor = System.Drawing.Color.Transparent
        Me.btnPropertyCardSemiEXP.BaseColor = System.Drawing.Color.White
        Me.btnPropertyCardSemiEXP.BorderColor = System.Drawing.Color.Black
        Me.btnPropertyCardSemiEXP.BorderSize = 1
        Me.btnPropertyCardSemiEXP.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPropertyCardSemiEXP.FocusedColor = System.Drawing.Color.Empty
        Me.btnPropertyCardSemiEXP.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPropertyCardSemiEXP.ForeColor = System.Drawing.Color.Black
        Me.btnPropertyCardSemiEXP.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPropertyCardSemiEXP.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPropertyCardSemiEXP.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPropertyCardSemiEXP.Location = New System.Drawing.Point(9, 198)
        Me.btnPropertyCardSemiEXP.Name = "btnPropertyCardSemiEXP"
        Me.btnPropertyCardSemiEXP.OnHoverBaseColor = System.Drawing.Color.Salmon
        Me.btnPropertyCardSemiEXP.OnHoverBorderColor = System.Drawing.Color.Salmon
        Me.btnPropertyCardSemiEXP.OnHoverForeColor = System.Drawing.Color.White
        Me.btnPropertyCardSemiEXP.OnHoverImage = Nothing
        Me.btnPropertyCardSemiEXP.OnPressedColor = System.Drawing.Color.Black
        Me.btnPropertyCardSemiEXP.Radius = 15
        Me.btnPropertyCardSemiEXP.Size = New System.Drawing.Size(201, 25)
        Me.btnPropertyCardSemiEXP.TabIndex = 244
        Me.btnPropertyCardSemiEXP.Text = "Print Property Card Semi EXP"
        Me.btnPropertyCardSemiEXP.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPropertyCardSemiEXP.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPrintSmallTagForm
        '
        Me.btnPrintSmallTagForm.AnimationHoverSpeed = 0.07!
        Me.btnPrintSmallTagForm.AnimationSpeed = 0.03!
        Me.btnPrintSmallTagForm.BackColor = System.Drawing.Color.Transparent
        Me.btnPrintSmallTagForm.BaseColor = System.Drawing.Color.White
        Me.btnPrintSmallTagForm.BorderColor = System.Drawing.Color.Black
        Me.btnPrintSmallTagForm.BorderSize = 1
        Me.btnPrintSmallTagForm.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPrintSmallTagForm.FocusedColor = System.Drawing.Color.Empty
        Me.btnPrintSmallTagForm.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPrintSmallTagForm.ForeColor = System.Drawing.Color.Black
        Me.btnPrintSmallTagForm.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPrintSmallTagForm.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPrintSmallTagForm.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPrintSmallTagForm.Location = New System.Drawing.Point(8, 145)
        Me.btnPrintSmallTagForm.Name = "btnPrintSmallTagForm"
        Me.btnPrintSmallTagForm.OnHoverBaseColor = System.Drawing.Color.Salmon
        Me.btnPrintSmallTagForm.OnHoverBorderColor = System.Drawing.Color.Salmon
        Me.btnPrintSmallTagForm.OnHoverForeColor = System.Drawing.Color.White
        Me.btnPrintSmallTagForm.OnHoverImage = Nothing
        Me.btnPrintSmallTagForm.OnPressedColor = System.Drawing.Color.Black
        Me.btnPrintSmallTagForm.Radius = 15
        Me.btnPrintSmallTagForm.Size = New System.Drawing.Size(201, 25)
        Me.btnPrintSmallTagForm.TabIndex = 242
        Me.btnPrintSmallTagForm.Text = "Print SMALL Tag Form"
        Me.btnPrintSmallTagForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPrintSmallTagForm.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnInventoryTransferForm
        '
        Me.btnInventoryTransferForm.AnimationHoverSpeed = 0.07!
        Me.btnInventoryTransferForm.AnimationSpeed = 0.03!
        Me.btnInventoryTransferForm.BackColor = System.Drawing.Color.Transparent
        Me.btnInventoryTransferForm.BaseColor = System.Drawing.Color.White
        Me.btnInventoryTransferForm.BorderColor = System.Drawing.Color.Black
        Me.btnInventoryTransferForm.BorderSize = 1
        Me.btnInventoryTransferForm.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnInventoryTransferForm.FocusedColor = System.Drawing.Color.Empty
        Me.btnInventoryTransferForm.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnInventoryTransferForm.ForeColor = System.Drawing.Color.Black
        Me.btnInventoryTransferForm.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnInventoryTransferForm.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnInventoryTransferForm.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnInventoryTransferForm.Location = New System.Drawing.Point(227, 99)
        Me.btnInventoryTransferForm.Name = "btnInventoryTransferForm"
        Me.btnInventoryTransferForm.OnHoverBaseColor = System.Drawing.Color.LavenderBlush
        Me.btnInventoryTransferForm.OnHoverBorderColor = System.Drawing.Color.LavenderBlush
        Me.btnInventoryTransferForm.OnHoverForeColor = System.Drawing.Color.Black
        Me.btnInventoryTransferForm.OnHoverImage = Nothing
        Me.btnInventoryTransferForm.OnPressedColor = System.Drawing.Color.Black
        Me.btnInventoryTransferForm.Radius = 15
        Me.btnInventoryTransferForm.Size = New System.Drawing.Size(201, 25)
        Me.btnInventoryTransferForm.TabIndex = 241
        Me.btnInventoryTransferForm.Text = "Print Inventory Transfer Form"
        Me.btnInventoryTransferForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnInventoryTransferForm.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPropertyTransferForm
        '
        Me.btnPropertyTransferForm.AnimationHoverSpeed = 0.07!
        Me.btnPropertyTransferForm.AnimationSpeed = 0.03!
        Me.btnPropertyTransferForm.BackColor = System.Drawing.Color.Transparent
        Me.btnPropertyTransferForm.BaseColor = System.Drawing.Color.White
        Me.btnPropertyTransferForm.BorderColor = System.Drawing.Color.Black
        Me.btnPropertyTransferForm.BorderSize = 1
        Me.btnPropertyTransferForm.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPropertyTransferForm.FocusedColor = System.Drawing.Color.Empty
        Me.btnPropertyTransferForm.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPropertyTransferForm.ForeColor = System.Drawing.Color.Black
        Me.btnPropertyTransferForm.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPropertyTransferForm.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPropertyTransferForm.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPropertyTransferForm.Location = New System.Drawing.Point(227, 126)
        Me.btnPropertyTransferForm.Name = "btnPropertyTransferForm"
        Me.btnPropertyTransferForm.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnPropertyTransferForm.OnHoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnPropertyTransferForm.OnHoverForeColor = System.Drawing.Color.Black
        Me.btnPropertyTransferForm.OnHoverImage = Nothing
        Me.btnPropertyTransferForm.OnPressedColor = System.Drawing.Color.Black
        Me.btnPropertyTransferForm.Radius = 15
        Me.btnPropertyTransferForm.Size = New System.Drawing.Size(201, 25)
        Me.btnPropertyTransferForm.TabIndex = 240
        Me.btnPropertyTransferForm.Text = "Print Property Transfer Form"
        Me.btnPropertyTransferForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPropertyTransferForm.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnICSHighValueBYLOT
        '
        Me.btnICSHighValueBYLOT.AnimationHoverSpeed = 0.07!
        Me.btnICSHighValueBYLOT.AnimationSpeed = 0.03!
        Me.btnICSHighValueBYLOT.BackColor = System.Drawing.Color.Transparent
        Me.btnICSHighValueBYLOT.BaseColor = System.Drawing.Color.White
        Me.btnICSHighValueBYLOT.BorderColor = System.Drawing.Color.Black
        Me.btnICSHighValueBYLOT.BorderSize = 1
        Me.btnICSHighValueBYLOT.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnICSHighValueBYLOT.FocusedColor = System.Drawing.Color.Empty
        Me.btnICSHighValueBYLOT.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnICSHighValueBYLOT.ForeColor = System.Drawing.Color.Black
        Me.btnICSHighValueBYLOT.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnICSHighValueBYLOT.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSHighValueBYLOT.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnICSHighValueBYLOT.Location = New System.Drawing.Point(8, 119)
        Me.btnICSHighValueBYLOT.Name = "btnICSHighValueBYLOT"
        Me.btnICSHighValueBYLOT.OnHoverBaseColor = System.Drawing.Color.CornflowerBlue
        Me.btnICSHighValueBYLOT.OnHoverBorderColor = System.Drawing.Color.CornflowerBlue
        Me.btnICSHighValueBYLOT.OnHoverForeColor = System.Drawing.Color.White
        Me.btnICSHighValueBYLOT.OnHoverImage = Nothing
        Me.btnICSHighValueBYLOT.OnPressedColor = System.Drawing.Color.Black
        Me.btnICSHighValueBYLOT.Radius = 15
        Me.btnICSHighValueBYLOT.Size = New System.Drawing.Size(201, 25)
        Me.btnICSHighValueBYLOT.TabIndex = 236
        Me.btnICSHighValueBYLOT.Text = "Print ICS High Value by LOT"
        Me.btnICSHighValueBYLOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSHighValueBYLOT.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPARBYLOT
        '
        Me.btnPARBYLOT.AnimationHoverSpeed = 0.07!
        Me.btnPARBYLOT.AnimationSpeed = 0.03!
        Me.btnPARBYLOT.BackColor = System.Drawing.Color.Transparent
        Me.btnPARBYLOT.BaseColor = System.Drawing.Color.White
        Me.btnPARBYLOT.BorderColor = System.Drawing.Color.Black
        Me.btnPARBYLOT.BorderSize = 1
        Me.btnPARBYLOT.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPARBYLOT.FocusedColor = System.Drawing.Color.Empty
        Me.btnPARBYLOT.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPARBYLOT.ForeColor = System.Drawing.Color.Black
        Me.btnPARBYLOT.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPARBYLOT.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPARBYLOT.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPARBYLOT.Location = New System.Drawing.Point(227, 70)
        Me.btnPARBYLOT.Name = "btnPARBYLOT"
        Me.btnPARBYLOT.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnPARBYLOT.OnHoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnPARBYLOT.OnHoverForeColor = System.Drawing.Color.White
        Me.btnPARBYLOT.OnHoverImage = Nothing
        Me.btnPARBYLOT.OnPressedColor = System.Drawing.Color.Black
        Me.btnPARBYLOT.Radius = 15
        Me.btnPARBYLOT.Size = New System.Drawing.Size(201, 25)
        Me.btnPARBYLOT.TabIndex = 235
        Me.btnPARBYLOT.Text = "Print PAR by LOT"
        Me.btnPARBYLOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPARBYLOT.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnICSLowValueBYLOT
        '
        Me.btnICSLowValueBYLOT.AnimationHoverSpeed = 0.07!
        Me.btnICSLowValueBYLOT.AnimationSpeed = 0.03!
        Me.btnICSLowValueBYLOT.BackColor = System.Drawing.Color.Transparent
        Me.btnICSLowValueBYLOT.BaseColor = System.Drawing.Color.White
        Me.btnICSLowValueBYLOT.BorderColor = System.Drawing.Color.Black
        Me.btnICSLowValueBYLOT.BorderSize = 1
        Me.btnICSLowValueBYLOT.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnICSLowValueBYLOT.FocusedColor = System.Drawing.Color.Empty
        Me.btnICSLowValueBYLOT.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnICSLowValueBYLOT.ForeColor = System.Drawing.Color.Black
        Me.btnICSLowValueBYLOT.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnICSLowValueBYLOT.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSLowValueBYLOT.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnICSLowValueBYLOT.Location = New System.Drawing.Point(8, 67)
        Me.btnICSLowValueBYLOT.Name = "btnICSLowValueBYLOT"
        Me.btnICSLowValueBYLOT.OnHoverBaseColor = System.Drawing.Color.Turquoise
        Me.btnICSLowValueBYLOT.OnHoverBorderColor = System.Drawing.Color.Turquoise
        Me.btnICSLowValueBYLOT.OnHoverForeColor = System.Drawing.Color.White
        Me.btnICSLowValueBYLOT.OnHoverImage = Nothing
        Me.btnICSLowValueBYLOT.OnPressedColor = System.Drawing.Color.Black
        Me.btnICSLowValueBYLOT.Radius = 15
        Me.btnICSLowValueBYLOT.Size = New System.Drawing.Size(201, 25)
        Me.btnICSLowValueBYLOT.TabIndex = 234
        Me.btnICSLowValueBYLOT.Text = "Print ICS Low Value by LOT"
        Me.btnICSLowValueBYLOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSLowValueBYLOT.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnICSLowValue
        '
        Me.btnICSLowValue.AnimationHoverSpeed = 0.07!
        Me.btnICSLowValue.AnimationSpeed = 0.03!
        Me.btnICSLowValue.BackColor = System.Drawing.Color.Transparent
        Me.btnICSLowValue.BaseColor = System.Drawing.Color.White
        Me.btnICSLowValue.BorderColor = System.Drawing.Color.Black
        Me.btnICSLowValue.BorderSize = 1
        Me.btnICSLowValue.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnICSLowValue.FocusedColor = System.Drawing.Color.Empty
        Me.btnICSLowValue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICSLowValue.ForeColor = System.Drawing.Color.Black
        Me.btnICSLowValue.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnICSLowValue.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSLowValue.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnICSLowValue.Location = New System.Drawing.Point(8, 41)
        Me.btnICSLowValue.Name = "btnICSLowValue"
        Me.btnICSLowValue.OnHoverBaseColor = System.Drawing.Color.Turquoise
        Me.btnICSLowValue.OnHoverBorderColor = System.Drawing.Color.Turquoise
        Me.btnICSLowValue.OnHoverForeColor = System.Drawing.Color.White
        Me.btnICSLowValue.OnHoverImage = Nothing
        Me.btnICSLowValue.OnPressedColor = System.Drawing.Color.Black
        Me.btnICSLowValue.Radius = 15
        Me.btnICSLowValue.Size = New System.Drawing.Size(201, 25)
        Me.btnICSLowValue.TabIndex = 232
        Me.btnICSLowValue.Text = "Print ICS Low Value"
        Me.btnICSLowValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSLowValue.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPrintTagForm
        '
        Me.btnPrintTagForm.AnimationHoverSpeed = 0.07!
        Me.btnPrintTagForm.AnimationSpeed = 0.03!
        Me.btnPrintTagForm.BackColor = System.Drawing.Color.Transparent
        Me.btnPrintTagForm.BaseColor = System.Drawing.Color.White
        Me.btnPrintTagForm.BorderColor = System.Drawing.Color.Black
        Me.btnPrintTagForm.BorderSize = 1
        Me.btnPrintTagForm.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPrintTagForm.FocusedColor = System.Drawing.Color.Empty
        Me.btnPrintTagForm.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPrintTagForm.ForeColor = System.Drawing.Color.Black
        Me.btnPrintTagForm.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPrintTagForm.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPrintTagForm.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPrintTagForm.Location = New System.Drawing.Point(9, 172)
        Me.btnPrintTagForm.Name = "btnPrintTagForm"
        Me.btnPrintTagForm.OnHoverBaseColor = System.Drawing.Color.Salmon
        Me.btnPrintTagForm.OnHoverBorderColor = System.Drawing.Color.Salmon
        Me.btnPrintTagForm.OnHoverForeColor = System.Drawing.Color.White
        Me.btnPrintTagForm.OnHoverImage = Nothing
        Me.btnPrintTagForm.OnPressedColor = System.Drawing.Color.Black
        Me.btnPrintTagForm.Radius = 15
        Me.btnPrintTagForm.Size = New System.Drawing.Size(201, 25)
        Me.btnPrintTagForm.TabIndex = 230
        Me.btnPrintTagForm.Text = "Print BIG Tag Form"
        Me.btnPrintTagForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPrintTagForm.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnPAR
        '
        Me.btnPAR.AnimationHoverSpeed = 0.07!
        Me.btnPAR.AnimationSpeed = 0.03!
        Me.btnPAR.BackColor = System.Drawing.Color.Transparent
        Me.btnPAR.BaseColor = System.Drawing.Color.White
        Me.btnPAR.BorderColor = System.Drawing.Color.Black
        Me.btnPAR.BorderSize = 1
        Me.btnPAR.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnPAR.FocusedColor = System.Drawing.Color.Empty
        Me.btnPAR.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPAR.ForeColor = System.Drawing.Color.Black
        Me.btnPAR.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPAR.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPAR.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnPAR.Location = New System.Drawing.Point(227, 42)
        Me.btnPAR.Name = "btnPAR"
        Me.btnPAR.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnPAR.OnHoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.btnPAR.OnHoverForeColor = System.Drawing.Color.White
        Me.btnPAR.OnHoverImage = Nothing
        Me.btnPAR.OnPressedColor = System.Drawing.Color.Black
        Me.btnPAR.Radius = 15
        Me.btnPAR.Size = New System.Drawing.Size(201, 25)
        Me.btnPAR.TabIndex = 229
        Me.btnPAR.Text = "Print PAR"
        Me.btnPAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPAR.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnICSHighValue
        '
        Me.btnICSHighValue.AnimationHoverSpeed = 0.07!
        Me.btnICSHighValue.AnimationSpeed = 0.03!
        Me.btnICSHighValue.BackColor = System.Drawing.Color.Transparent
        Me.btnICSHighValue.BaseColor = System.Drawing.Color.White
        Me.btnICSHighValue.BorderColor = System.Drawing.Color.Black
        Me.btnICSHighValue.BorderSize = 1
        Me.btnICSHighValue.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnICSHighValue.FocusedColor = System.Drawing.Color.Empty
        Me.btnICSHighValue.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnICSHighValue.ForeColor = System.Drawing.Color.Black
        Me.btnICSHighValue.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnICSHighValue.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSHighValue.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnICSHighValue.Location = New System.Drawing.Point(8, 93)
        Me.btnICSHighValue.Name = "btnICSHighValue"
        Me.btnICSHighValue.OnHoverBaseColor = System.Drawing.Color.CornflowerBlue
        Me.btnICSHighValue.OnHoverBorderColor = System.Drawing.Color.CornflowerBlue
        Me.btnICSHighValue.OnHoverForeColor = System.Drawing.Color.White
        Me.btnICSHighValue.OnHoverImage = Nothing
        Me.btnICSHighValue.OnPressedColor = System.Drawing.Color.Black
        Me.btnICSHighValue.Radius = 15
        Me.btnICSHighValue.Size = New System.Drawing.Size(201, 25)
        Me.btnICSHighValue.TabIndex = 228
        Me.btnICSHighValue.Text = "Print ICS High Value"
        Me.btnICSHighValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnICSHighValue.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
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
        'warehouse_details
        '
        Me.warehouse_details.BackColor = System.Drawing.Color.AntiqueWhite
        Me.warehouse_details.Controls.Add(Me.txtSource)
        Me.warehouse_details.Controls.Add(Me.Label20)
        Me.warehouse_details.Controls.Add(Me.combo_mode)
        Me.warehouse_details.Controls.Add(Me.combo_supplier)
        Me.warehouse_details.Controls.Add(Me.combo_category)
        Me.warehouse_details.Controls.Add(Me.Label6)
        Me.warehouse_details.Controls.Add(Me.po_date)
        Me.warehouse_details.Controls.Add(Me.Label1)
        Me.warehouse_details.Controls.Add(Me.Label8)
        Me.warehouse_details.Controls.Add(Me.Label14)
        Me.warehouse_details.Controls.Add(Me.txtInitial)
        Me.warehouse_details.Controls.Add(Me.Label9)
        Me.warehouse_details.Controls.Add(Me.Label15)
        Me.warehouse_details.Controls.Add(Me.Label13)
        Me.warehouse_details.Controls.Add(Me.Label16)
        Me.warehouse_details.Controls.Add(Me.purpose)
        Me.warehouse_details.Controls.Add(Me.combo_supplier_address)
        Me.warehouse_details.Controls.Add(Me.txtPONUMBER)
        Me.warehouse_details.Controls.Add(Me.txtPRNUMBER)
        Me.warehouse_details.Controls.Add(Me.Label12)
        Me.warehouse_details.Controls.Add(Me.Label10)
        Me.warehouse_details.Controls.Add(Me.txtDepartment)
        Me.warehouse_details.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.warehouse_details.FillColor = System.Drawing.Color.AntiqueWhite
        Me.warehouse_details.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.warehouse_details.ForeColor = System.Drawing.Color.Black
        Me.warehouse_details.Location = New System.Drawing.Point(11, 34)
        Me.warehouse_details.Name = "warehouse_details"
        Me.warehouse_details.ShadowDecoration.Parent = Me.warehouse_details
        Me.warehouse_details.Size = New System.Drawing.Size(883, 225)
        Me.warehouse_details.TabIndex = 149
        Me.warehouse_details.Text = "Purchase Order Details"
        Me.warehouse_details.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.warehouse_details.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.warehouse_details.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'txtSource
        '
        Me.txtSource.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSource.FormattingEnabled = True
        Me.txtSource.Items.AddRange(New Object() {"General Fund", "Trust Fund", "Special Education Fund"})
        Me.txtSource.Location = New System.Drawing.Point(10, 196)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(251, 23)
        Me.txtSource.TabIndex = 256
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label20.Location = New System.Drawing.Point(9, 178)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(91, 15)
        Me.Label20.TabIndex = 255
        Me.Label20.Text = "Source of Fund:"
        '
        'combo_mode
        '
        Me.combo_mode.BackColor = System.Drawing.Color.White
        Me.combo_mode.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.combo_mode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.combo_mode.ForeColor = System.Drawing.Color.Black
        Me.combo_mode.FormattingEnabled = True
        Me.combo_mode.Location = New System.Drawing.Point(271, 154)
        Me.combo_mode.Name = "combo_mode"
        Me.combo_mode.Size = New System.Drawing.Size(219, 21)
        Me.combo_mode.TabIndex = 227
        '
        'combo_supplier
        '
        Me.combo_supplier.BackColor = System.Drawing.Color.White
        Me.combo_supplier.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.combo_supplier.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.combo_supplier.ForeColor = System.Drawing.Color.Black
        Me.combo_supplier.FormattingEnabled = True
        Me.combo_supplier.Location = New System.Drawing.Point(10, 151)
        Me.combo_supplier.Name = "combo_supplier"
        Me.combo_supplier.Size = New System.Drawing.Size(251, 21)
        Me.combo_supplier.TabIndex = 226
        '
        'combo_category
        '
        Me.combo_category.BackColor = System.Drawing.Color.White
        Me.combo_category.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.combo_category.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.combo_category.ForeColor = System.Drawing.Color.Black
        Me.combo_category.FormattingEnabled = True
        Me.combo_category.Location = New System.Drawing.Point(12, 110)
        Me.combo_category.Name = "combo_category"
        Me.combo_category.Size = New System.Drawing.Size(251, 21)
        Me.combo_category.TabIndex = 225
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(496, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 15)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "PO Date"
        '
        'po_date
        '
        Me.po_date.BackColor = System.Drawing.Color.Transparent
        Me.po_date.BorderRadius = 10
        Me.po_date.BorderThickness = 1
        Me.po_date.CheckedState.Parent = Me.po_date
        Me.po_date.FillColor = System.Drawing.Color.White
        Me.po_date.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.po_date.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.po_date.HoverState.Parent = Me.po_date
        Me.po_date.Location = New System.Drawing.Point(499, 61)
        Me.po_date.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.po_date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.po_date.Name = "po_date"
        Me.po_date.ShadowDecoration.Parent = Me.po_date
        Me.po_date.Size = New System.Drawing.Size(198, 26)
        Me.po_date.TabIndex = 222
        Me.po_date.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.po_date.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.po_date.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        Me.po_date.Value = New Date(2023, 4, 25, 9, 42, 23, 73)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(9, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 15)
        Me.Label1.TabIndex = 115
        Me.Label1.Text = "Department / Office:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(707, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 15)
        Me.Label8.TabIndex = 236
        Me.Label8.Text = "Initial"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(264, 178)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 15)
        Me.Label14.TabIndex = 106
        Me.Label14.Text = "Supplier Address:"
        '
        'txtInitial
        '
        Me.txtInitial.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtInitial.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtInitial.BackColor = System.Drawing.Color.Transparent
        Me.txtInitial.BaseColor = System.Drawing.Color.White
        Me.txtInitial.BorderColor = System.Drawing.Color.Black
        Me.txtInitial.BorderSize = 1
        Me.txtInitial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInitial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInitial.FocusedBaseColor = System.Drawing.Color.White
        Me.txtInitial.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtInitial.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtInitial.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInitial.Location = New System.Drawing.Point(710, 62)
        Me.txtInitial.Name = "txtInitial"
        Me.txtInitial.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtInitial.Radius = 10
        Me.txtInitial.SelectedText = ""
        Me.txtInitial.Size = New System.Drawing.Size(84, 26)
        Me.txtInitial.TabIndex = 235
        Me.txtInitial.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(500, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 15)
        Me.Label9.TabIndex = 102
        Me.Label9.Text = "Purpose / Remarks"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(264, 135)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(128, 15)
        Me.Label15.TabIndex = 107
        Me.Label15.Text = "Mode of Procurement:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(7, 134)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 15)
        Me.Label13.TabIndex = 105
        Me.Label13.Text = "Supplier Name:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(7, 91)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(58, 15)
        Me.Label16.TabIndex = 108
        Me.Label16.Text = "Category:"
        '
        'purpose
        '
        Me.purpose.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.purpose.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.purpose.BackColor = System.Drawing.Color.Transparent
        Me.purpose.BaseColor = System.Drawing.Color.White
        Me.purpose.BorderColor = System.Drawing.Color.Black
        Me.purpose.BorderSize = 1
        Me.purpose.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.purpose.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.purpose.FocusedBaseColor = System.Drawing.Color.White
        Me.purpose.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.purpose.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.purpose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.purpose.Location = New System.Drawing.Point(499, 110)
        Me.purpose.Multiline = True
        Me.purpose.Name = "purpose"
        Me.purpose.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.purpose.Radius = 10
        Me.purpose.SelectedText = ""
        Me.purpose.Size = New System.Drawing.Size(302, 107)
        Me.purpose.TabIndex = 221
        Me.purpose.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'combo_supplier_address
        '
        Me.combo_supplier_address.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.combo_supplier_address.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.combo_supplier_address.BackColor = System.Drawing.Color.Transparent
        Me.combo_supplier_address.BaseColor = System.Drawing.Color.White
        Me.combo_supplier_address.BorderColor = System.Drawing.Color.Black
        Me.combo_supplier_address.BorderSize = 1
        Me.combo_supplier_address.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.combo_supplier_address.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.combo_supplier_address.FocusedBaseColor = System.Drawing.Color.White
        Me.combo_supplier_address.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.combo_supplier_address.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.combo_supplier_address.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_supplier_address.Location = New System.Drawing.Point(271, 195)
        Me.combo_supplier_address.Name = "combo_supplier_address"
        Me.combo_supplier_address.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.combo_supplier_address.Radius = 10
        Me.combo_supplier_address.SelectedText = ""
        Me.combo_supplier_address.Size = New System.Drawing.Size(219, 26)
        Me.combo_supplier_address.TabIndex = 220
        Me.combo_supplier_address.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txtPONUMBER
        '
        Me.txtPONUMBER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtPONUMBER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtPONUMBER.BackColor = System.Drawing.Color.Transparent
        Me.txtPONUMBER.BaseColor = System.Drawing.Color.White
        Me.txtPONUMBER.BorderColor = System.Drawing.Color.Black
        Me.txtPONUMBER.BorderSize = 1
        Me.txtPONUMBER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPONUMBER.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONUMBER.FocusedBaseColor = System.Drawing.Color.White
        Me.txtPONUMBER.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPONUMBER.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPONUMBER.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONUMBER.ForeColor = System.Drawing.Color.Green
        Me.txtPONUMBER.Location = New System.Drawing.Point(271, 60)
        Me.txtPONUMBER.Name = "txtPONUMBER"
        Me.txtPONUMBER.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPONUMBER.Radius = 10
        Me.txtPONUMBER.SelectedText = ""
        Me.txtPONUMBER.Size = New System.Drawing.Size(219, 26)
        Me.txtPONUMBER.TabIndex = 217
        Me.txtPONUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txtPRNUMBER
        '
        Me.txtPRNUMBER.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtPRNUMBER.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtPRNUMBER.BackColor = System.Drawing.Color.Transparent
        Me.txtPRNUMBER.BaseColor = System.Drawing.Color.White
        Me.txtPRNUMBER.BorderColor = System.Drawing.Color.Black
        Me.txtPRNUMBER.BorderSize = 1
        Me.txtPRNUMBER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPRNUMBER.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPRNUMBER.FocusedBaseColor = System.Drawing.Color.White
        Me.txtPRNUMBER.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPRNUMBER.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPRNUMBER.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPRNUMBER.ForeColor = System.Drawing.Color.Blue
        Me.txtPRNUMBER.Location = New System.Drawing.Point(271, 105)
        Me.txtPRNUMBER.Name = "txtPRNUMBER"
        Me.txtPRNUMBER.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPRNUMBER.Radius = 10
        Me.txtPRNUMBER.SelectedText = ""
        Me.txtPRNUMBER.Size = New System.Drawing.Size(219, 26)
        Me.txtPRNUMBER.TabIndex = 216
        Me.txtPRNUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label12.ForeColor = System.Drawing.Color.Green
        Me.Label12.Location = New System.Drawing.Point(268, 41)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(142, 15)
        Me.Label12.TabIndex = 101
        Me.Label12.Text = "Purchase Order Number:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(264, 87)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(153, 15)
        Me.Label10.TabIndex = 99
        Me.Label10.Text = "Purchase Request Number:"
        '
        'txtDepartment
        '
        Me.txtDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtDepartment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtDepartment.BackColor = System.Drawing.Color.Transparent
        Me.txtDepartment.BaseColor = System.Drawing.Color.White
        Me.txtDepartment.BorderColor = System.Drawing.Color.Black
        Me.txtDepartment.BorderSize = 1
        Me.txtDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDepartment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepartment.FocusedBaseColor = System.Drawing.Color.White
        Me.txtDepartment.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDepartment.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtDepartment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.Location = New System.Drawing.Point(10, 61)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtDepartment.Radius = 10
        Me.txtDepartment.SelectedText = ""
        Me.txtDepartment.Size = New System.Drawing.Size(253, 26)
        Me.txtDepartment.TabIndex = 214
        Me.txtDepartment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox1.Controls.Add(Me.dgvNewInventory)
        Me.Guna2GroupBox1.Controls.Add(Me.Label18)
        Me.Guna2GroupBox1.Controls.Add(Me.txtPTRITRNumber)
        Me.Guna2GroupBox1.Controls.Add(Me.Label17)
        Me.Guna2GroupBox1.Controls.Add(Me.txtTotal_Amount)
        Me.Guna2GroupBox1.Controls.Add(Me.chkPrintAll)
        Me.Guna2GroupBox1.Controls.Add(Me.Label11)
        Me.Guna2GroupBox1.Controls.Add(Me.txtPARICSNumber)
        Me.Guna2GroupBox1.Controls.Add(Me.txtEncoded)
        Me.Guna2GroupBox1.Controls.Add(Me.lblbarcode)
        Me.Guna2GroupBox1.Controls.Add(Me.txtBarcode)
        Me.Guna2GroupBox1.Controls.Add(Me.Label7)
        Me.Guna2GroupBox1.Controls.Add(Me.Label5)
        Me.Guna2GroupBox1.Controls.Add(Me.txtcgsoposition)
        Me.Guna2GroupBox1.Controls.Add(Me.txtcgsoofficer)
        Me.Guna2GroupBox1.Controls.Add(Me.Label2)
        Me.Guna2GroupBox1.Controls.Add(Me.btnImport)
        Me.Guna2GroupBox1.Controls.Add(Me.Date_Entry)
        Me.Guna2GroupBox1.Controls.Add(Me.Label3)
        Me.Guna2GroupBox1.Controls.Add(Me.Label4)
        Me.Guna2GroupBox1.Controls.Add(Me.btnEdit)
        Me.Guna2GroupBox1.Controls.Add(Me.btnDelete)
        Me.Guna2GroupBox1.Controls.Add(Me.btnSelectPO)
        Me.Guna2GroupBox1.Controls.Add(Me.txtTotalItems)
        Me.Guna2GroupBox1.Controls.Add(Me.btnSave)
        Me.Guna2GroupBox1.Controls.Add(Me.txtSubTotal)
        Me.Guna2GroupBox1.Controls.Add(Me.AddRow)
        Me.Guna2GroupBox1.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox1.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(11, 265)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.ShadowDecoration.Parent = Me.Guna2GroupBox1
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(1330, 446)
        Me.Guna2GroupBox1.TabIndex = 150
        Me.Guna2GroupBox1.Text = "Purchase Order Items"
        Me.Guna2GroupBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox1.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(1024, 81)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(93, 15)
        Me.Label18.TabIndex = 247
        Me.Label18.Text = "PTR ITR Number"
        Me.Label18.Visible = False
        '
        'txtPTRITRNumber
        '
        Me.txtPTRITRNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtPTRITRNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtPTRITRNumber.BackColor = System.Drawing.Color.Transparent
        Me.txtPTRITRNumber.BaseColor = System.Drawing.Color.White
        Me.txtPTRITRNumber.BorderColor = System.Drawing.Color.Black
        Me.txtPTRITRNumber.BorderSize = 1
        Me.txtPTRITRNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPTRITRNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPTRITRNumber.FocusedBaseColor = System.Drawing.Color.White
        Me.txtPTRITRNumber.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPTRITRNumber.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPTRITRNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPTRITRNumber.Location = New System.Drawing.Point(970, 55)
        Me.txtPTRITRNumber.Name = "txtPTRITRNumber"
        Me.txtPTRITRNumber.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPTRITRNumber.Radius = 10
        Me.txtPTRITRNumber.SelectedText = ""
        Me.txtPTRITRNumber.Size = New System.Drawing.Size(196, 26)
        Me.txtPTRITRNumber.TabIndex = 246
        Me.txtPTRITRNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPTRITRNumber.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(229, 75)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 15)
        Me.Label17.TabIndex = 245
        Me.Label17.Text = "Total Amount:"
        '
        'txtTotal_Amount
        '
        Me.txtTotal_Amount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtTotal_Amount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtTotal_Amount.BackColor = System.Drawing.Color.Transparent
        Me.txtTotal_Amount.BaseColor = System.Drawing.Color.White
        Me.txtTotal_Amount.BorderColor = System.Drawing.Color.Black
        Me.txtTotal_Amount.BorderSize = 1
        Me.txtTotal_Amount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal_Amount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal_Amount.FocusedBaseColor = System.Drawing.Color.White
        Me.txtTotal_Amount.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTotal_Amount.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotal_Amount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTotal_Amount.ForeColor = System.Drawing.Color.Black
        Me.txtTotal_Amount.Location = New System.Drawing.Point(312, 71)
        Me.txtTotal_Amount.Name = "txtTotal_Amount"
        Me.txtTotal_Amount.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtTotal_Amount.Radius = 10
        Me.txtTotal_Amount.SelectedText = ""
        Me.txtTotal_Amount.Size = New System.Drawing.Size(219, 26)
        Me.txtTotal_Amount.TabIndex = 244
        Me.txtTotal_Amount.Text = "0.00"
        Me.txtTotal_Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'chkPrintAll
        '
        Me.chkPrintAll.AutoSize = True
        Me.chkPrintAll.BackColor = System.Drawing.Color.AntiqueWhite
        Me.chkPrintAll.ForeColor = System.Drawing.Color.Red
        Me.chkPrintAll.Location = New System.Drawing.Point(12, 82)
        Me.chkPrintAll.Name = "chkPrintAll"
        Me.chkPrintAll.Size = New System.Drawing.Size(68, 19)
        Me.chkPrintAll.TabIndex = 243
        Me.chkPrintAll.Text = "Print All"
        Me.chkPrintAll.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(803, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 15)
        Me.Label11.TabIndex = 242
        Me.Label11.Text = "PAR ICS Number"
        Me.Label11.Visible = False
        '
        'txtPARICSNumber
        '
        Me.txtPARICSNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtPARICSNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtPARICSNumber.BackColor = System.Drawing.Color.Transparent
        Me.txtPARICSNumber.BaseColor = System.Drawing.Color.White
        Me.txtPARICSNumber.BorderColor = System.Drawing.Color.Black
        Me.txtPARICSNumber.BorderSize = 1
        Me.txtPARICSNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPARICSNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPARICSNumber.FocusedBaseColor = System.Drawing.Color.White
        Me.txtPARICSNumber.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPARICSNumber.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPARICSNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPARICSNumber.Location = New System.Drawing.Point(749, 54)
        Me.txtPARICSNumber.Name = "txtPARICSNumber"
        Me.txtPARICSNumber.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPARICSNumber.Radius = 10
        Me.txtPARICSNumber.SelectedText = ""
        Me.txtPARICSNumber.Size = New System.Drawing.Size(196, 26)
        Me.txtPARICSNumber.TabIndex = 241
        Me.txtPARICSNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPARICSNumber.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem, Me.DuplicateRowToolStripMenuItem, Me.AddSubDetailsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(160, 70)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove Items/s"
        '
        'DuplicateRowToolStripMenuItem
        '
        Me.DuplicateRowToolStripMenuItem.Name = "DuplicateRowToolStripMenuItem"
        Me.DuplicateRowToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.DuplicateRowToolStripMenuItem.Text = "Duplicate Row"
        '
        'AddSubDetailsToolStripMenuItem
        '
        Me.AddSubDetailsToolStripMenuItem.Name = "AddSubDetailsToolStripMenuItem"
        Me.AddSubDetailsToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.AddSubDetailsToolStripMenuItem.Text = "Add Sub Details"
        '
        'txtEncoded
        '
        Me.txtEncoded.Location = New System.Drawing.Point(643, 52)
        Me.txtEncoded.Name = "txtEncoded"
        Me.txtEncoded.Size = New System.Drawing.Size(98, 23)
        Me.txtEncoded.TabIndex = 239
        Me.txtEncoded.Visible = False
        '
        'lblbarcode
        '
        Me.lblbarcode.AutoSize = True
        Me.lblbarcode.BackColor = System.Drawing.Color.Transparent
        Me.lblbarcode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbarcode.ForeColor = System.Drawing.Color.Black
        Me.lblbarcode.Location = New System.Drawing.Point(414, 448)
        Me.lblbarcode.Name = "lblbarcode"
        Me.lblbarcode.Size = New System.Drawing.Size(59, 15)
        Me.lblbarcode.TabIndex = 238
        Me.lblbarcode.Text = "BARCODE"
        '
        'txtBarcode
        '
        Me.txtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(336, 391)
        Me.txtBarcode.Multiline = True
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(227, 44)
        Me.txtBarcode.TabIndex = 237
        Me.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtBarcode.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(594, 420)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 15)
        Me.Label7.TabIndex = 234
        Me.Label7.Text = "Position"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(569, 392)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 15)
        Me.Label5.TabIndex = 233
        Me.Label5.Text = "CGSO Officer"
        '
        'txtcgsoposition
        '
        Me.txtcgsoposition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtcgsoposition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtcgsoposition.BackColor = System.Drawing.Color.Transparent
        Me.txtcgsoposition.BaseColor = System.Drawing.Color.White
        Me.txtcgsoposition.BorderColor = System.Drawing.Color.Black
        Me.txtcgsoposition.BorderSize = 1
        Me.txtcgsoposition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcgsoposition.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcgsoposition.FocusedBaseColor = System.Drawing.Color.White
        Me.txtcgsoposition.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcgsoposition.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtcgsoposition.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcgsoposition.ForeColor = System.Drawing.Color.Black
        Me.txtcgsoposition.Location = New System.Drawing.Point(652, 418)
        Me.txtcgsoposition.Name = "txtcgsoposition"
        Me.txtcgsoposition.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtcgsoposition.Radius = 10
        Me.txtcgsoposition.SelectedText = ""
        Me.txtcgsoposition.Size = New System.Drawing.Size(195, 26)
        Me.txtcgsoposition.TabIndex = 232
        Me.txtcgsoposition.Text = "CGSO OFFICER"
        Me.txtcgsoposition.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txtcgsoofficer
        '
        Me.txtcgsoofficer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtcgsoofficer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtcgsoofficer.BackColor = System.Drawing.Color.Transparent
        Me.txtcgsoofficer.BaseColor = System.Drawing.Color.White
        Me.txtcgsoofficer.BorderColor = System.Drawing.Color.Black
        Me.txtcgsoofficer.BorderSize = 1
        Me.txtcgsoofficer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcgsoofficer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcgsoofficer.FocusedBaseColor = System.Drawing.Color.White
        Me.txtcgsoofficer.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcgsoofficer.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtcgsoofficer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcgsoofficer.Location = New System.Drawing.Point(652, 386)
        Me.txtcgsoofficer.Name = "txtcgsoofficer"
        Me.txtcgsoofficer.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtcgsoofficer.Radius = 10
        Me.txtcgsoofficer.SelectedText = ""
        Me.txtcgsoofficer.Size = New System.Drawing.Size(195, 26)
        Me.txtcgsoofficer.TabIndex = 231
        Me.txtcgsoofficer.Text = "CGSO OFFICER"
        Me.txtcgsoofficer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(243, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 15)
        Me.Label2.TabIndex = 228
        Me.Label2.Text = "Date Entry:"
        '
        'btnImport
        '
        Me.btnImport.BackColor = System.Drawing.Color.White
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.Image = Global.Purchase_Request.My.Resources.Resources.Ok
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnImport.Location = New System.Drawing.Point(563, 49)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(74, 30)
        Me.btnImport.TabIndex = 230
        Me.btnImport.Text = "Import"
        Me.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImport.UseVisualStyleBackColor = False
        Me.btnImport.Visible = False
        '
        'Date_Entry
        '
        Me.Date_Entry.BackColor = System.Drawing.Color.Transparent
        Me.Date_Entry.BorderRadius = 10
        Me.Date_Entry.BorderThickness = 1
        Me.Date_Entry.CheckedState.Parent = Me.Date_Entry
        Me.Date_Entry.FillColor = System.Drawing.Color.White
        Me.Date_Entry.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.Date_Entry.HoverState.Parent = Me.Date_Entry
        Me.Date_Entry.Location = New System.Drawing.Point(312, 42)
        Me.Date_Entry.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.Date_Entry.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Date_Entry.Name = "Date_Entry"
        Me.Date_Entry.ShadowDecoration.Parent = Me.Date_Entry
        Me.Date_Entry.Size = New System.Drawing.Size(219, 26)
        Me.Date_Entry.TabIndex = 229
        Me.Date_Entry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Date_Entry.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Date_Entry.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        Me.Date_Entry.Value = New Date(2023, 4, 25, 9, 42, 23, 73)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(853, 392)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 228
        Me.Label3.Text = "Total Items"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(859, 424)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 15)
        Me.Label4.TabIndex = 229
        Me.Label4.Text = "Total Cost"
        '
        'btnEdit
        '
        Me.btnEdit.AnimationHoverSpeed = 0.07!
        Me.btnEdit.AnimationSpeed = 0.03!
        Me.btnEdit.BackColor = System.Drawing.Color.Transparent
        Me.btnEdit.BaseColor = System.Drawing.Color.White
        Me.btnEdit.BorderColor = System.Drawing.Color.Black
        Me.btnEdit.BorderSize = 1
        Me.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnEdit.FocusedColor = System.Drawing.Color.Empty
        Me.btnEdit.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Image = Nothing
        Me.btnEdit.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnEdit.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnEdit.Location = New System.Drawing.Point(170, 396)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.OnHoverBaseColor = System.Drawing.Color.CornflowerBlue
        Me.btnEdit.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnEdit.OnHoverForeColor = System.Drawing.Color.White
        Me.btnEdit.OnHoverImage = Nothing
        Me.btnEdit.OnPressedColor = System.Drawing.Color.Black
        Me.btnEdit.Radius = 15
        Me.btnEdit.Size = New System.Drawing.Size(152, 33)
        Me.btnEdit.TabIndex = 227
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnEdit.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnDelete
        '
        Me.btnDelete.AnimationHoverSpeed = 0.07!
        Me.btnDelete.AnimationSpeed = 0.03!
        Me.btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnDelete.BaseColor = System.Drawing.Color.White
        Me.btnDelete.BorderColor = System.Drawing.Color.Black
        Me.btnDelete.BorderSize = 1
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDelete.FocusedColor = System.Drawing.Color.Empty
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.Black
        Me.btnDelete.Image = Nothing
        Me.btnDelete.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnDelete.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnDelete.Location = New System.Drawing.Point(12, 396)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.OnHoverBaseColor = System.Drawing.Color.Red
        Me.btnDelete.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnDelete.OnHoverForeColor = System.Drawing.Color.White
        Me.btnDelete.OnHoverImage = Nothing
        Me.btnDelete.OnPressedColor = System.Drawing.Color.Black
        Me.btnDelete.Radius = 15
        Me.btnDelete.Size = New System.Drawing.Size(152, 33)
        Me.btnDelete.TabIndex = 226
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnDelete.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnSelectPO
        '
        Me.btnSelectPO.BackColor = System.Drawing.Color.White
        Me.btnSelectPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectPO.Image = Global.Purchase_Request.My.Resources.Resources.Datbase
        Me.btnSelectPO.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnSelectPO.Location = New System.Drawing.Point(12, 46)
        Me.btnSelectPO.Name = "btnSelectPO"
        Me.btnSelectPO.Size = New System.Drawing.Size(211, 30)
        Me.btnSelectPO.TabIndex = 17
        Me.btnSelectPO.Text = "Select Purchase Order or Press F3"
        Me.btnSelectPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectPO.UseVisualStyleBackColor = False
        '
        'txtTotalItems
        '
        Me.txtTotalItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtTotalItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtTotalItems.BackColor = System.Drawing.Color.Transparent
        Me.txtTotalItems.BaseColor = System.Drawing.Color.White
        Me.txtTotalItems.BorderColor = System.Drawing.Color.Black
        Me.txtTotalItems.BorderSize = 1
        Me.txtTotalItems.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotalItems.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalItems.FocusedBaseColor = System.Drawing.Color.White
        Me.txtTotalItems.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTotalItems.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotalItems.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtTotalItems.Location = New System.Drawing.Point(926, 386)
        Me.txtTotalItems.Name = "txtTotalItems"
        Me.txtTotalItems.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtTotalItems.Radius = 10
        Me.txtTotalItems.SelectedText = ""
        Me.txtTotalItems.Size = New System.Drawing.Size(81, 26)
        Me.txtTotalItems.TabIndex = 223
        Me.txtTotalItems.Text = "0"
        Me.txtTotalItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnSave
        '
        Me.btnSave.AnimationHoverSpeed = 0.07!
        Me.btnSave.AnimationSpeed = 0.03!
        Me.btnSave.BackColor = System.Drawing.Color.Transparent
        Me.btnSave.BaseColor = System.Drawing.Color.White
        Me.btnSave.BorderColor = System.Drawing.Color.Black
        Me.btnSave.BorderSize = 1
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.FocusedColor = System.Drawing.Color.Empty
        Me.btnSave.Font = New System.Drawing.Font("Calisto MT", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnSave.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnSave.Location = New System.Drawing.Point(1106, 392)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.btnSave.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnSave.OnHoverForeColor = System.Drawing.Color.White
        Me.btnSave.OnHoverImage = Nothing
        Me.btnSave.OnPressedColor = System.Drawing.Color.Black
        Me.btnSave.Radius = 15
        Me.btnSave.Size = New System.Drawing.Size(215, 44)
        Me.btnSave.TabIndex = 225
        Me.btnSave.Text = "SAVE"
        Me.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnSave.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtSubTotal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtSubTotal.BackColor = System.Drawing.Color.Transparent
        Me.txtSubTotal.BaseColor = System.Drawing.Color.White
        Me.txtSubTotal.BorderColor = System.Drawing.Color.Black
        Me.txtSubTotal.BorderSize = 1
        Me.txtSubTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.FocusedBaseColor = System.Drawing.Color.White
        Me.txtSubTotal.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSubTotal.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSubTotal.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtSubTotal.ForeColor = System.Drawing.Color.Black
        Me.txtSubTotal.Location = New System.Drawing.Point(926, 418)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSubTotal.Radius = 10
        Me.txtSubTotal.SelectedText = ""
        Me.txtSubTotal.Size = New System.Drawing.Size(159, 26)
        Me.txtSubTotal.TabIndex = 224
        Me.txtSubTotal.Text = "0.00"
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'AddRow
        '
        Me.AddRow.BackColor = System.Drawing.Color.White
        Me.AddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddRow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddRow.ForeColor = System.Drawing.Color.Green
        Me.AddRow.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.AddRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddRow.Location = New System.Drawing.Point(1228, 66)
        Me.AddRow.Name = "AddRow"
        Me.AddRow.Size = New System.Drawing.Size(90, 30)
        Me.AddRow.TabIndex = 21
        Me.AddRow.Text = "Add Row"
        Me.AddRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddRow.UseVisualStyleBackColor = False
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.ForeColor = System.Drawing.Color.Black
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(42, 17)
        Me.ToolStripStatusLabel1.Text = "Status:"
        '
        'inventory_state
        '
        Me.inventory_state.ForeColor = System.Drawing.Color.Black
        Me.inventory_state.Name = "inventory_state"
        Me.inventory_state.Size = New System.Drawing.Size(87, 17)
        Me.inventory_state.Text = "inventory_state"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.StatusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.inventory_state})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 713)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1347, 22)
        Me.StatusStrip1.TabIndex = 24
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'dgvNewInventory
        '
        Me.dgvNewInventory.AllowUserToAddRows = False
        Me.dgvNewInventory.AllowUserToDeleteRows = False
        Me.dgvNewInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvNewInventory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvNewInventory.BackgroundColor = System.Drawing.Color.DarkSeaGreen
        Me.dgvNewInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNewInventory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.print, Me.Inv_ID, Me.Item_ID, Me.Unit, Me.Items_Description, Me.Quantity, Me.Unit_Cost, Me.Total_Cost, Me.Serial_Number, Me.Tag_Number, Me.Inventory_Category, Me.PAR_ICS_Number, Me.Accountable_Officer, Me.Position, Me.Location, Me.Classification, Me.Remarks, Me.Condition, Me.NumberSeries, Me.PTR_ITR_Number, Me.Estimated_Life})
        Me.dgvNewInventory.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(3)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNewInventory.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvNewInventory.Location = New System.Drawing.Point(10, 102)
        Me.dgvNewInventory.Name = "dgvNewInventory"
        Me.dgvNewInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNewInventory.Size = New System.Drawing.Size(1308, 280)
        Me.dgvNewInventory.TabIndex = 248
        '
        'print
        '
        Me.print.FillWeight = 40.0!
        Me.print.HeaderText = ""
        Me.print.Name = "print"
        Me.print.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.print.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.print.Width = 25
        '
        'Inv_ID
        '
        Me.Inv_ID.HeaderText = "Inv ID"
        Me.Inv_ID.Name = "Inv_ID"
        Me.Inv_ID.Visible = False
        Me.Inv_ID.Width = 68
        '
        'Item_ID
        '
        Me.Item_ID.HeaderText = "Item ID"
        Me.Item_ID.Name = "Item_ID"
        Me.Item_ID.Visible = False
        Me.Item_ID.Width = 76
        '
        'Unit
        '
        Me.Unit.FillWeight = 50.0!
        Me.Unit.HeaderText = "Unit"
        Me.Unit.Name = "Unit"
        Me.Unit.Width = 60
        '
        'Items_Description
        '
        Me.Items_Description.FillWeight = 200.0!
        Me.Items_Description.HeaderText = "Item Description"
        Me.Items_Description.Name = "Items_Description"
        Me.Items_Description.Width = 115
        '
        'Quantity
        '
        Me.Quantity.FillWeight = 40.0!
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.Width = 84
        '
        'Unit_Cost
        '
        Me.Unit_Cost.FillWeight = 70.0!
        Me.Unit_Cost.HeaderText = "Unit Cost"
        Me.Unit_Cost.Name = "Unit_Cost"
        Me.Unit_Cost.Width = 81
        '
        'Total_Cost
        '
        Me.Total_Cost.FillWeight = 70.0!
        Me.Total_Cost.HeaderText = "Total Cost"
        Me.Total_Cost.Name = "Total_Cost"
        Me.Total_Cost.Width = 84
        '
        'Serial_Number
        '
        Me.Serial_Number.FillWeight = 70.0!
        Me.Serial_Number.HeaderText = "Serial Number"
        Me.Serial_Number.Name = "Serial_Number"
        Me.Serial_Number.Width = 104
        '
        'Tag_Number
        '
        Me.Tag_Number.FillWeight = 70.0!
        Me.Tag_Number.HeaderText = "Tag Number"
        Me.Tag_Number.Name = "Tag_Number"
        Me.Tag_Number.Width = 95
        '
        'Inventory_Category
        '
        Me.Inventory_Category.HeaderText = "Inventory Category"
        Me.Inventory_Category.Name = "Inventory_Category"
        Me.Inventory_Category.Width = 128
        '
        'PAR_ICS_Number
        '
        Me.PAR_ICS_Number.FillWeight = 80.0!
        Me.PAR_ICS_Number.HeaderText = "PAR / ICS Number"
        Me.PAR_ICS_Number.Name = "PAR_ICS_Number"
        Me.PAR_ICS_Number.Width = 123
        '
        'Accountable_Officer
        '
        Me.Accountable_Officer.FillWeight = 120.0!
        Me.Accountable_Officer.HeaderText = "Accountable Officer"
        Me.Accountable_Officer.Name = "Accountable_Officer"
        Me.Accountable_Officer.Width = 132
        '
        'Position
        '
        Me.Position.FillWeight = 80.0!
        Me.Position.HeaderText = "Position"
        Me.Position.Name = "Position"
        Me.Position.Width = 81
        '
        'Location
        '
        Me.Location.HeaderText = "Location"
        Me.Location.Name = "Location"
        Me.Location.Width = 84
        '
        'Classification
        '
        Me.Classification.HeaderText = "Classification"
        Me.Classification.Name = "Classification"
        Me.Classification.Width = 108
        '
        'Remarks
        '
        Me.Remarks.FillWeight = 130.0!
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.Width = 83
        '
        'Condition
        '
        Me.Condition.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Condition.HeaderText = "Status / Condition"
        Me.Condition.Items.AddRange(New Object() {"IN GOOD CONDITION", "NEEDING REPAIR", "UNSERVICEABLE", "OBSOLETE", "NO LONGER NEEDED", "NOT USED SINCE PURCHASE", "STOLEN", "DAMAGE", "LOST"})
        Me.Condition.Name = "Condition"
        Me.Condition.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Condition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Condition.Width = 123
        '
        'NumberSeries
        '
        Me.NumberSeries.HeaderText = "#"
        Me.NumberSeries.Name = "NumberSeries"
        Me.NumberSeries.Width = 45
        '
        'PTR_ITR_Number
        '
        Me.PTR_ITR_Number.HeaderText = "PTR ITR Number"
        Me.PTR_ITR_Number.Name = "PTR_ITR_Number"
        Me.PTR_ITR_Number.Width = 114
        '
        'Estimated_Life
        '
        Me.Estimated_Life.HeaderText = "Est. Life"
        Me.Estimated_Life.Name = "Estimated_Life"
        Me.Estimated_Life.Width = 73
        '
        'CGSO_Inventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1347, 735)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GunaGroupBox1)
        Me.Name = "CGSO_Inventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.GunaGroupBox1.ResumeLayout(False)
        Me.groupprint.ResumeLayout(False)
        Me.warehouse_details.ResumeLayout(False)
        Me.warehouse_details.PerformLayout()
        Me.Guna2GroupBox1.ResumeLayout(False)
        Me.Guna2GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgvNewInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GunaGroupBox1 As Guna.UI.WinForms.GunaGroupBox
    Friend WithEvents warehouse_details As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents combo_mode As System.Windows.Forms.ComboBox
    Friend WithEvents combo_supplier As System.Windows.Forms.ComboBox
    Friend WithEvents combo_category As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents txtTotalItems As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents po_date As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents purpose As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents combo_supplier_address As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents txtPONUMBER As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents txtPRNUMBER As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents btnSelectPO As System.Windows.Forms.Button
    Friend WithEvents btnSave As Guna.UI.WinForms.GunaButton
    Friend WithEvents AddRow As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DuplicateRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnEdit As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnDelete As Guna.UI.WinForms.GunaButton
    Friend WithEvents groupprint As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents GunaButton2 As Guna.UI.WinForms.GunaButton
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnICSHighValue As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnPAR As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnPrintTagForm As Guna.UI.WinForms.GunaButton
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents inventory_state As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Date_Entry As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents btnICSLowValue As Guna.UI.WinForms.GunaButton
    Friend WithEvents AddSubDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtcgsoposition As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents txtcgsoofficer As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtInitial As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents lblbarcode As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents txtEncoded As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPARICSNumber As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents chkPrintAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnICSLowValueBYLOT As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnICSHighValueBYLOT As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnPARBYLOT As Guna.UI.WinForms.GunaButton
    Friend WithEvents txtTotal_Amount As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnInventoryTransferForm As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnPropertyTransferForm As Guna.UI.WinForms.GunaButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtPTRITRNumber As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents btnPrintSmallTagForm As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnPropertyCardSemiEXP As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnPARrenew As Guna.UI.WinForms.GunaButton
    Friend WithEvents printICSHighValueRenew As Guna.UI.WinForms.GunaButton
    Friend WithEvents dgvNewInventory As System.Windows.Forms.DataGridView
    Friend WithEvents print As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Inv_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Item_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Items_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Serial_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tag_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Inventory_Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PAR_ICS_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Accountable_Officer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Position As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Location As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Classification As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Condition As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents NumberSeries As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PTR_ITR_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Estimated_Life As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
