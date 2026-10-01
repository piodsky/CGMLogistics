<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CGSO_Inventory_Sub_Details
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Date_Entry = New Guna.UI2.WinForms.Guna2DateTimePicker
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.btnSave = New Guna.UI.WinForms.GunaButton
        Me.dgvInventory = New System.Windows.Forms.DataGridView
        Me.Items_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Serial_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Tag_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Accountable_Officer = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Location = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Condition = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AddRow = New System.Windows.Forms.Button
        Me.txtDescription = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPONumber = New Guna.UI.WinForms.GunaTextBox
        Me.txtUnit_Cost = New Guna.UI.WinForms.GunaTextBox
        Me.txtDesc = New Guna.UI.WinForms.GunaTextBox
        Me.Guna2GroupBox1.SuspendLayout()
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(439, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 15)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "Unit Cost"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(225, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 15)
        Me.Label2.TabIndex = 230
        Me.Label2.Text = "Date Entry"
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
        Me.Date_Entry.Location = New System.Drawing.Point(228, 23)
        Me.Date_Entry.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.Date_Entry.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Date_Entry.Name = "Date_Entry"
        Me.Date_Entry.ShadowDecoration.Parent = Me.Date_Entry
        Me.Date_Entry.Size = New System.Drawing.Size(193, 26)
        Me.Date_Entry.TabIndex = 231
        Me.Date_Entry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Date_Entry.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Date_Entry.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        Me.Date_Entry.Value = New Date(2023, 4, 25, 9, 42, 23, 73)
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox1.Controls.Add(Me.btnSave)
        Me.Guna2GroupBox1.Controls.Add(Me.dgvInventory)
        Me.Guna2GroupBox1.Controls.Add(Me.AddRow)
        Me.Guna2GroupBox1.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox1.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(10, 161)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.ShadowDecoration.Parent = Me.Guna2GroupBox1
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(1087, 462)
        Me.Guna2GroupBox1.TabIndex = 232
        Me.Guna2GroupBox1.Text = "Purchase Order Items"
        Me.Guna2GroupBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox1.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
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
        Me.btnSave.Font = New System.Drawing.Font("Calisto MT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnSave.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnSave.Location = New System.Drawing.Point(921, 419)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.btnSave.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnSave.OnHoverForeColor = System.Drawing.Color.White
        Me.btnSave.OnHoverImage = Nothing
        Me.btnSave.OnPressedColor = System.Drawing.Color.Black
        Me.btnSave.Radius = 15
        Me.btnSave.Size = New System.Drawing.Size(161, 33)
        Me.btnSave.TabIndex = 225
        Me.btnSave.Text = "SAVE"
        Me.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnSave.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'dgvInventory
        '
        Me.dgvInventory.AllowUserToAddRows = False
        Me.dgvInventory.AllowUserToDeleteRows = False
        Me.dgvInventory.AllowUserToResizeColumns = False
        Me.dgvInventory.AllowUserToResizeRows = False
        Me.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvInventory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvInventory.BackgroundColor = System.Drawing.Color.DarkSeaGreen
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInventory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInventory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Items_Description, Me.Unit_Cost, Me.Serial_Number, Me.Tag_Number, Me.Accountable_Officer, Me.Location, Me.Remarks, Me.Condition})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInventory.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInventory.Location = New System.Drawing.Point(7, 76)
        Me.dgvInventory.Name = "dgvInventory"
        Me.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInventory.Size = New System.Drawing.Size(1075, 337)
        Me.dgvInventory.TabIndex = 19
        '
        'Items_Description
        '
        Me.Items_Description.FillWeight = 200.0!
        Me.Items_Description.HeaderText = "Item Description"
        Me.Items_Description.Name = "Items_Description"
        Me.Items_Description.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Items_Description.Width = 119
        '
        'Unit_Cost
        '
        Me.Unit_Cost.FillWeight = 60.0!
        Me.Unit_Cost.HeaderText = "Unit Cost"
        Me.Unit_Cost.Name = "Unit_Cost"
        Me.Unit_Cost.Width = 85
        '
        'Serial_Number
        '
        Me.Serial_Number.HeaderText = "Serial Number"
        Me.Serial_Number.Name = "Serial_Number"
        Me.Serial_Number.Width = 108
        '
        'Tag_Number
        '
        Me.Tag_Number.HeaderText = "Tag Number"
        Me.Tag_Number.Name = "Tag_Number"
        Me.Tag_Number.Width = 99
        '
        'Accountable_Officer
        '
        Me.Accountable_Officer.FillWeight = 120.0!
        Me.Accountable_Officer.HeaderText = "Accountable Officer"
        Me.Accountable_Officer.Name = "Accountable_Officer"
        Me.Accountable_Officer.Width = 136
        '
        'Location
        '
        Me.Location.FillWeight = 120.0!
        Me.Location.HeaderText = "Location"
        Me.Location.Name = "Location"
        Me.Location.Width = 88
        '
        'Remarks
        '
        Me.Remarks.FillWeight = 200.0!
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.Width = 87
        '
        'Condition
        '
        Me.Condition.HeaderText = "Condition"
        Me.Condition.Name = "Condition"
        Me.Condition.Width = 95
        '
        'AddRow
        '
        Me.AddRow.BackColor = System.Drawing.Color.White
        Me.AddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddRow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddRow.ForeColor = System.Drawing.Color.Green
        Me.AddRow.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.AddRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddRow.Location = New System.Drawing.Point(989, 44)
        Me.AddRow.Name = "AddRow"
        Me.AddRow.Size = New System.Drawing.Size(90, 30)
        Me.AddRow.TabIndex = 21
        Me.AddRow.Text = "Add Row"
        Me.AddRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddRow.UseVisualStyleBackColor = False
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = True
        Me.txtDescription.BackColor = System.Drawing.Color.Transparent
        Me.txtDescription.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(13, 55)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(131, 15)
        Me.txtDescription.TabIndex = 78
        Me.txtDescription.Text = "Description of the Item"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(14, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 15)
        Me.Label11.TabIndex = 81
        Me.Label11.Text = "PO Number"
        '
        'txtPONumber
        '
        Me.txtPONumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtPONumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtPONumber.BackColor = System.Drawing.Color.Transparent
        Me.txtPONumber.BaseColor = System.Drawing.Color.White
        Me.txtPONumber.BorderColor = System.Drawing.Color.Black
        Me.txtPONumber.BorderSize = 1
        Me.txtPONumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPONumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONumber.FocusedBaseColor = System.Drawing.Color.White
        Me.txtPONumber.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPONumber.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPONumber.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPONumber.Location = New System.Drawing.Point(17, 23)
        Me.txtPONumber.Name = "txtPONumber"
        Me.txtPONumber.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPONumber.Radius = 10
        Me.txtPONumber.SelectedText = ""
        Me.txtPONumber.Size = New System.Drawing.Size(197, 26)
        Me.txtPONumber.TabIndex = 233
        Me.txtPONumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txtUnit_Cost
        '
        Me.txtUnit_Cost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtUnit_Cost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtUnit_Cost.BackColor = System.Drawing.Color.Transparent
        Me.txtUnit_Cost.BaseColor = System.Drawing.Color.White
        Me.txtUnit_Cost.BorderColor = System.Drawing.Color.Black
        Me.txtUnit_Cost.BorderSize = 1
        Me.txtUnit_Cost.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUnit_Cost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit_Cost.FocusedBaseColor = System.Drawing.Color.White
        Me.txtUnit_Cost.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnit_Cost.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtUnit_Cost.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtUnit_Cost.Location = New System.Drawing.Point(442, 23)
        Me.txtUnit_Cost.Name = "txtUnit_Cost"
        Me.txtUnit_Cost.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUnit_Cost.Radius = 10
        Me.txtUnit_Cost.SelectedText = ""
        Me.txtUnit_Cost.Size = New System.Drawing.Size(144, 26)
        Me.txtUnit_Cost.TabIndex = 235
        Me.txtUnit_Cost.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txtDesc
        '
        Me.txtDesc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtDesc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtDesc.BackColor = System.Drawing.Color.Transparent
        Me.txtDesc.BaseColor = System.Drawing.Color.White
        Me.txtDesc.BorderColor = System.Drawing.Color.Black
        Me.txtDesc.BorderSize = 1
        Me.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesc.FocusedBaseColor = System.Drawing.Color.White
        Me.txtDesc.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDesc.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtDesc.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtDesc.Location = New System.Drawing.Point(16, 73)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtDesc.Radius = 10
        Me.txtDesc.SelectedText = ""
        Me.txtDesc.Size = New System.Drawing.Size(569, 79)
        Me.txtDesc.TabIndex = 236
        Me.txtDesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'CGSO_Inventory_Sub_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1104, 635)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtUnit_Cost)
        Me.Controls.Add(Me.txtPONumber)
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Date_Entry)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtDescription)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CGSO_Inventory_Sub_Details"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CGSO Inventory Sub Details"
        Me.Guna2GroupBox1.ResumeLayout(False)
        CType(Me.dgvInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Date_Entry As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents btnSave As Guna.UI.WinForms.GunaButton
    Friend WithEvents dgvInventory As System.Windows.Forms.DataGridView
    Friend WithEvents AddRow As System.Windows.Forms.Button
    Friend WithEvents txtDescription As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPONumber As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents txtUnit_Cost As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents txtDesc As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Items_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Serial_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tag_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Accountable_Officer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Location As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Condition As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
