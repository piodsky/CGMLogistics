<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CGSO_PO_Select
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
        Me.Guna2GroupBox2 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.GunaButton1 = New Guna.UI.WinForms.GunaButton
        Me.txtSearchBox = New Guna.UI.WinForms.GunaTextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.combo_search = New System.Windows.Forms.ComboBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnCancel = New Guna.UI.WinForms.GunaButton
        Me.btnUse = New Guna.UI.WinForms.GunaButton
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox
        Me.btnSave = New Guna.UI.WinForms.GunaButton
        Me.AddRow = New System.Windows.Forms.Button
        Me.dgvPO = New System.Windows.Forms.DataGridView
        Me.PO_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PR_Number = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supplier_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Date_Created = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Guna2GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Guna2GroupBox1.SuspendLayout()
        CType(Me.dgvPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2GroupBox2
        '
        Me.Guna2GroupBox2.Controls.Add(Me.GunaButton1)
        Me.Guna2GroupBox2.Controls.Add(Me.txtSearchBox)
        Me.Guna2GroupBox2.Controls.Add(Me.Button1)
        Me.Guna2GroupBox2.Controls.Add(Me.combo_search)
        Me.Guna2GroupBox2.Controls.Add(Me.btnClear)
        Me.Guna2GroupBox2.Controls.Add(Me.btnSearch)
        Me.Guna2GroupBox2.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox2.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox2.Location = New System.Drawing.Point(11, 10)
        Me.Guna2GroupBox2.Name = "Guna2GroupBox2"
        Me.Guna2GroupBox2.ShadowDecoration.Parent = Me.Guna2GroupBox2
        Me.Guna2GroupBox2.Size = New System.Drawing.Size(1077, 112)
        Me.Guna2GroupBox2.TabIndex = 228
        Me.Guna2GroupBox2.Text = "Searchbox"
        Me.Guna2GroupBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Guna2GroupBox2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.Guna2GroupBox2.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
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
        'txtSearchBox
        '
        Me.txtSearchBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtSearchBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtSearchBox.BackColor = System.Drawing.Color.Transparent
        Me.txtSearchBox.BaseColor = System.Drawing.Color.White
        Me.txtSearchBox.BorderColor = System.Drawing.Color.Silver
        Me.txtSearchBox.BorderSize = 1
        Me.txtSearchBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearchBox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchBox.FocusedBaseColor = System.Drawing.Color.White
        Me.txtSearchBox.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearchBox.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSearchBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSearchBox.Location = New System.Drawing.Point(199, 64)
        Me.txtSearchBox.Name = "txtSearchBox"
        Me.txtSearchBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSearchBox.Radius = 10
        Me.txtSearchBox.SelectedText = ""
        Me.txtSearchBox.Size = New System.Drawing.Size(277, 26)
        Me.txtSearchBox.TabIndex = 215
        Me.txtSearchBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
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
        'combo_search
        '
        Me.combo_search.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combo_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_search.FormattingEnabled = True
        Me.combo_search.Items.AddRange(New Object() {"PO_Number", "PR_Number", "Department", "Supplier_Name", "Supplier_Address", "Date_Created"})
        Me.combo_search.Location = New System.Drawing.Point(15, 64)
        Me.combo_search.Name = "combo_search"
        Me.combo_search.Size = New System.Drawing.Size(168, 23)
        Me.combo_search.TabIndex = 24
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(594, 64)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(96, 30)
        Me.btnClear.TabIndex = 25
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.Blue
        Me.btnSearch.Location = New System.Drawing.Point(492, 64)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(96, 30)
        Me.btnSearch.TabIndex = 23
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnUse)
        Me.GroupBox1.Controls.Add(Me.Guna2GroupBox1)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(11, 132)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1077, 524)
        Me.GroupBox1.TabIndex = 227
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Please Select the Purchase Order you want to Use"
        '
        'btnCancel
        '
        Me.btnCancel.AnimationHoverSpeed = 0.07!
        Me.btnCancel.AnimationSpeed = 0.03!
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.BaseColor = System.Drawing.Color.White
        Me.btnCancel.BorderColor = System.Drawing.Color.Black
        Me.btnCancel.BorderSize = 1
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCancel.FocusedColor = System.Drawing.Color.Empty
        Me.btnCancel.Font = New System.Drawing.Font("Calisto MT", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Red
        Me.btnCancel.Image = Nothing
        Me.btnCancel.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnCancel.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnCancel.Location = New System.Drawing.Point(951, 477)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnCancel.OnHoverForeColor = System.Drawing.Color.White
        Me.btnCancel.OnHoverImage = Nothing
        Me.btnCancel.OnPressedColor = System.Drawing.Color.Black
        Me.btnCancel.Radius = 15
        Me.btnCancel.Size = New System.Drawing.Size(120, 33)
        Me.btnCancel.TabIndex = 227
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnCancel.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'btnUse
        '
        Me.btnUse.AnimationHoverSpeed = 0.07!
        Me.btnUse.AnimationSpeed = 0.03!
        Me.btnUse.BackColor = System.Drawing.Color.Transparent
        Me.btnUse.BaseColor = System.Drawing.Color.White
        Me.btnUse.BorderColor = System.Drawing.Color.Black
        Me.btnUse.BorderSize = 1
        Me.btnUse.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnUse.FocusedColor = System.Drawing.Color.Empty
        Me.btnUse.Font = New System.Drawing.Font("Calisto MT", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUse.ForeColor = System.Drawing.Color.Black
        Me.btnUse.Image = Global.Purchase_Request.My.Resources.Resources.Datbase
        Me.btnUse.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnUse.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnUse.Location = New System.Drawing.Point(825, 477)
        Me.btnUse.Name = "btnUse"
        Me.btnUse.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.btnUse.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnUse.OnHoverForeColor = System.Drawing.Color.White
        Me.btnUse.OnHoverImage = Nothing
        Me.btnUse.OnPressedColor = System.Drawing.Color.Black
        Me.btnUse.Radius = 15
        Me.btnUse.Size = New System.Drawing.Size(120, 33)
        Me.btnUse.TabIndex = 226
        Me.btnUse.Text = "Use"
        Me.btnUse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnUse.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.Controls.Add(Me.btnSave)
        Me.Guna2GroupBox1.Controls.Add(Me.AddRow)
        Me.Guna2GroupBox1.Controls.Add(Me.dgvPO)
        Me.Guna2GroupBox1.CustomBorderColor = System.Drawing.Color.MediumSeaGreen
        Me.Guna2GroupBox1.FillColor = System.Drawing.Color.AntiqueWhite
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(6, 22)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.ShadowDecoration.Parent = Me.Guna2GroupBox1
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(1065, 449)
        Me.Guna2GroupBox1.TabIndex = 151
        Me.Guna2GroupBox1.Text = "Purchase Order List"
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
        Me.btnSave.Font = New System.Drawing.Font("Calisto MT", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnSave.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnSave.Location = New System.Drawing.Point(1094, 405)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.OnHoverBaseColor = System.Drawing.Color.DodgerBlue
        Me.btnSave.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnSave.OnHoverForeColor = System.Drawing.Color.White
        Me.btnSave.OnHoverImage = Nothing
        Me.btnSave.OnPressedColor = System.Drawing.Color.Black
        Me.btnSave.Radius = 15
        Me.btnSave.Size = New System.Drawing.Size(218, 44)
        Me.btnSave.TabIndex = 225
        Me.btnSave.Text = "SAVE"
        Me.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.btnSave.TextRenderingHint = Guna.UI.WinForms.DrawingTextRenderingHint.SystemDefault
        '
        'AddRow
        '
        Me.AddRow.BackColor = System.Drawing.Color.White
        Me.AddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddRow.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddRow.ForeColor = System.Drawing.Color.Green
        Me.AddRow.Image = Global.Purchase_Request.My.Resources.Resources.add_small
        Me.AddRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddRow.Location = New System.Drawing.Point(1222, 56)
        Me.AddRow.Name = "AddRow"
        Me.AddRow.Size = New System.Drawing.Size(90, 30)
        Me.AddRow.TabIndex = 21
        Me.AddRow.Text = "Add Row"
        Me.AddRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddRow.UseVisualStyleBackColor = False
        '
        'dgvPO
        '
        Me.dgvPO.AllowUserToAddRows = False
        Me.dgvPO.AllowUserToDeleteRows = False
        Me.dgvPO.AllowUserToResizeColumns = False
        Me.dgvPO.AllowUserToResizeRows = False
        Me.dgvPO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvPO.BackgroundColor = System.Drawing.Color.DarkSeaGreen
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PO_Number, Me.PR_Number, Me.Department, Me.Supplier_Name, Me.Category, Me.Date_Created})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPO.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPO.Location = New System.Drawing.Point(15, 47)
        Me.dgvPO.Name = "dgvPO"
        Me.dgvPO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPO.Size = New System.Drawing.Size(1037, 387)
        Me.dgvPO.TabIndex = 20
        '
        'PO_Number
        '
        Me.PO_Number.HeaderText = "PO Number"
        Me.PO_Number.Name = "PO_Number"
        '
        'PR_Number
        '
        Me.PR_Number.HeaderText = "PR Number"
        Me.PR_Number.Name = "PR_Number"
        '
        'Department
        '
        Me.Department.FillWeight = 120.0!
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        '
        'Supplier_Name
        '
        Me.Supplier_Name.FillWeight = 120.0!
        Me.Supplier_Name.HeaderText = "Supplier Name"
        Me.Supplier_Name.Name = "Supplier_Name"
        '
        'Category
        '
        Me.Category.FillWeight = 120.0!
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        '
        'Date_Created
        '
        Me.Date_Created.HeaderText = "Date Created"
        Me.Date_Created.Name = "Date_Created"
        '
        'CGSO_PO_Select
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1102, 665)
        Me.Controls.Add(Me.Guna2GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "CGSO_PO_Select"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CGSO_PO_Select"
        Me.Guna2GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Guna2GroupBox1.ResumeLayout(False)
        CType(Me.dgvPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2GroupBox2 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents GunaButton1 As Guna.UI.WinForms.GunaButton
    Friend WithEvents txtSearchBox As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents combo_search As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnUse As Guna.UI.WinForms.GunaButton
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents btnSave As Guna.UI.WinForms.GunaButton
    Friend WithEvents AddRow As System.Windows.Forms.Button
    Friend WithEvents dgvPO As System.Windows.Forms.DataGridView
    Friend WithEvents PO_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PR_Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Created As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
