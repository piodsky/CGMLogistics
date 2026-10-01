<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Transfer_Form
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
        Me.Label6 = New System.Windows.Forms.Label
        Me.po_date = New Guna.UI2.WinForms.Guna2DateTimePicker
        Me.txtAccountableTO = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.combo_position = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFundCluster = New Guna.UI.WinForms.GunaTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnPTRSave = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtAccountableFROM = New Guna.UI.WinForms.GunaTextBox
        Me.combo_entity_departments = New System.Windows.Forms.ComboBox
        Me.lblTransfer = New System.Windows.Forms.Label
        Me.btnITRSave = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.approved_by = New System.Windows.Forms.TextBox
        Me.issued_designation = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.issued_by = New System.Windows.Forms.TextBox
        Me.approved_designation = New System.Windows.Forms.TextBox
        Me.txtReasonforTransfer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(17, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 15)
        Me.Label6.TabIndex = 223
        Me.Label6.Text = "Date :"
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
        Me.po_date.Location = New System.Drawing.Point(20, 175)
        Me.po_date.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.po_date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.po_date.Name = "po_date"
        Me.po_date.ShadowDecoration.Parent = Me.po_date
        Me.po_date.Size = New System.Drawing.Size(292, 26)
        Me.po_date.TabIndex = 224
        Me.po_date.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.po_date.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.po_date.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        Me.po_date.Value = New Date(2023, 4, 25, 9, 42, 23, 73)
        '
        'txtAccountableTO
        '
        Me.txtAccountableTO.BackColor = System.Drawing.Color.White
        Me.txtAccountableTO.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.txtAccountableTO.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountableTO.ForeColor = System.Drawing.Color.Black
        Me.txtAccountableTO.FormattingEnabled = True
        Me.txtAccountableTO.Location = New System.Drawing.Point(26, 298)
        Me.txtAccountableTO.Name = "txtAccountableTO"
        Me.txtAccountableTO.Size = New System.Drawing.Size(288, 23)
        Me.txtAccountableTO.TabIndex = 229
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label15.ForeColor = System.Drawing.Color.Green
        Me.Label15.Location = New System.Drawing.Point(19, 279)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(253, 15)
        Me.Label15.TabIndex = 228
        Me.Label15.Text = "To Accountable Officer/Agency/Fund Cluster :"
        '
        'combo_position
        '
        Me.combo_position.BackColor = System.Drawing.Color.White
        Me.combo_position.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.combo_position.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_position.ForeColor = System.Drawing.Color.Black
        Me.combo_position.FormattingEnabled = True
        Me.combo_position.Location = New System.Drawing.Point(26, 343)
        Me.combo_position.Name = "combo_position"
        Me.combo_position.Size = New System.Drawing.Size(288, 23)
        Me.combo_position.TabIndex = 231
        Me.combo_position.Text = "BARANGAY TREASURER"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(19, 324)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 230
        Me.Label2.Text = "Position"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(19, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 15)
        Me.Label3.TabIndex = 232
        Me.Label3.Text = "Fund Cluster :"
        '
        'txtFundCluster
        '
        Me.txtFundCluster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtFundCluster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtFundCluster.BackColor = System.Drawing.Color.Transparent
        Me.txtFundCluster.BaseColor = System.Drawing.Color.White
        Me.txtFundCluster.BorderColor = System.Drawing.Color.Black
        Me.txtFundCluster.BorderSize = 1
        Me.txtFundCluster.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFundCluster.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFundCluster.FocusedBaseColor = System.Drawing.Color.White
        Me.txtFundCluster.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFundCluster.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtFundCluster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFundCluster.Location = New System.Drawing.Point(20, 126)
        Me.txtFundCluster.Name = "txtFundCluster"
        Me.txtFundCluster.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtFundCluster.Radius = 10
        Me.txtFundCluster.SelectedText = ""
        Me.txtFundCluster.Size = New System.Drawing.Size(292, 26)
        Me.txtFundCluster.TabIndex = 233
        Me.txtFundCluster.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(17, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 15)
        Me.Label4.TabIndex = 234
        Me.Label4.Text = "Entity Name :"
        '
        'btnPTRSave
        '
        Me.btnPTRSave.BackColor = System.Drawing.Color.White
        Me.btnPTRSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPTRSave.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPTRSave.ForeColor = System.Drawing.Color.Black
        Me.btnPTRSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnPTRSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPTRSave.Location = New System.Drawing.Point(195, 486)
        Me.btnPTRSave.Name = "btnPTRSave"
        Me.btnPTRSave.Size = New System.Drawing.Size(90, 30)
        Me.btnPTRSave.TabIndex = 236
        Me.btnPTRSave.Text = "PTR Save"
        Me.btnPTRSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPTRSave.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtReasonforTransfer)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtAccountableFROM)
        Me.GroupBox1.Controls.Add(Me.combo_entity_departments)
        Me.GroupBox1.Controls.Add(Me.lblTransfer)
        Me.GroupBox1.Controls.Add(Me.btnITRSave)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.combo_position)
        Me.GroupBox1.Controls.Add(Me.txtAccountableTO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnPTRSave)
        Me.GroupBox1.Controls.Add(Me.po_date)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtFundCluster)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(387, 522)
        Me.GroupBox1.TabIndex = 237
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transfer Details"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(19, 232)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(269, 15)
        Me.Label10.TabIndex = 240
        Me.Label10.Text = "From Accountable Officer/Agency/Fund Cluster :"
        '
        'txtAccountableFROM
        '
        Me.txtAccountableFROM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtAccountableFROM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtAccountableFROM.BackColor = System.Drawing.Color.Transparent
        Me.txtAccountableFROM.BaseColor = System.Drawing.Color.White
        Me.txtAccountableFROM.BorderColor = System.Drawing.Color.Black
        Me.txtAccountableFROM.BorderSize = 1
        Me.txtAccountableFROM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAccountableFROM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccountableFROM.FocusedBaseColor = System.Drawing.Color.White
        Me.txtAccountableFROM.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAccountableFROM.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtAccountableFROM.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountableFROM.Location = New System.Drawing.Point(20, 250)
        Me.txtAccountableFROM.Name = "txtAccountableFROM"
        Me.txtAccountableFROM.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtAccountableFROM.Radius = 10
        Me.txtAccountableFROM.SelectedText = ""
        Me.txtAccountableFROM.Size = New System.Drawing.Size(292, 26)
        Me.txtAccountableFROM.TabIndex = 241
        Me.txtAccountableFROM.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'combo_entity_departments
        '
        Me.combo_entity_departments.BackColor = System.Drawing.Color.White
        Me.combo_entity_departments.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.combo_entity_departments.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_entity_departments.ForeColor = System.Drawing.Color.Black
        Me.combo_entity_departments.FormattingEnabled = True
        Me.combo_entity_departments.Location = New System.Drawing.Point(20, 79)
        Me.combo_entity_departments.Name = "combo_entity_departments"
        Me.combo_entity_departments.Size = New System.Drawing.Size(288, 23)
        Me.combo_entity_departments.TabIndex = 239
        '
        'lblTransfer
        '
        Me.lblTransfer.AutoSize = True
        Me.lblTransfer.BackColor = System.Drawing.Color.Transparent
        Me.lblTransfer.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransfer.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblTransfer.Location = New System.Drawing.Point(20, 27)
        Me.lblTransfer.Name = "lblTransfer"
        Me.lblTransfer.Size = New System.Drawing.Size(143, 24)
        Me.lblTransfer.TabIndex = 238
        Me.lblTransfer.Text = "Transfer Form"
        '
        'btnITRSave
        '
        Me.btnITRSave.BackColor = System.Drawing.Color.White
        Me.btnITRSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnITRSave.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnITRSave.ForeColor = System.Drawing.Color.Black
        Me.btnITRSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnITRSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnITRSave.Location = New System.Drawing.Point(291, 486)
        Me.btnITRSave.Name = "btnITRSave"
        Me.btnITRSave.Size = New System.Drawing.Size(90, 30)
        Me.btnITRSave.TabIndex = 237
        Me.btnITRSave.Text = "ITR Save"
        Me.btnITRSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnITRSave.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.approved_by)
        Me.GroupBox2.Controls.Add(Me.issued_designation)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.issued_by)
        Me.GroupBox2.Controls.Add(Me.approved_designation)
        Me.GroupBox2.Location = New System.Drawing.Point(399, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(328, 234)
        Me.GroupBox2.TabIndex = 267
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Green
        Me.Label8.Location = New System.Drawing.Point(7, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 18)
        Me.Label8.TabIndex = 256
        Me.Label8.Text = "Print Details"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(13, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 15)
        Me.Label5.TabIndex = 250
        Me.Label5.Text = "Approved by:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(14, 119)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 15)
        Me.Label13.TabIndex = 263
        Me.Label13.Text = "Designation:"
        '
        'approved_by
        '
        Me.approved_by.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.approved_by.ForeColor = System.Drawing.Color.Black
        Me.approved_by.Location = New System.Drawing.Point(95, 29)
        Me.approved_by.Name = "approved_by"
        Me.approved_by.Size = New System.Drawing.Size(227, 22)
        Me.approved_by.TabIndex = 251
        Me.approved_by.Text = "DIOSCORA R. NIERE, CPA"
        '
        'issued_designation
        '
        Me.issued_designation.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.issued_designation.ForeColor = System.Drawing.Color.Black
        Me.issued_designation.Location = New System.Drawing.Point(96, 116)
        Me.issued_designation.Name = "issued_designation"
        Me.issued_designation.Size = New System.Drawing.Size(226, 22)
        Me.issued_designation.TabIndex = 262
        Me.issued_designation.Text = "Supply Officer III/ Warehouse In-Charge"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(30, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 15)
        Me.Label7.TabIndex = 252
        Me.Label7.Text = "Issued by:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(15, 57)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 15)
        Me.Label12.TabIndex = 261
        Me.Label12.Text = "Designation:"
        '
        'issued_by
        '
        Me.issued_by.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.issued_by.ForeColor = System.Drawing.Color.Black
        Me.issued_by.Location = New System.Drawing.Point(96, 91)
        Me.issued_by.Name = "issued_by"
        Me.issued_by.Size = New System.Drawing.Size(226, 22)
        Me.issued_by.TabIndex = 253
        Me.issued_by.Text = "GIOVANNI M. TULANG"
        '
        'approved_designation
        '
        Me.approved_designation.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.approved_designation.ForeColor = System.Drawing.Color.Black
        Me.approved_designation.Location = New System.Drawing.Point(95, 54)
        Me.approved_designation.Name = "approved_designation"
        Me.approved_designation.Size = New System.Drawing.Size(227, 22)
        Me.approved_designation.TabIndex = 260
        Me.approved_designation.Text = "CEEDMO/CGSO Designate"
        '
        'txtReasonforTransfer
        '
        Me.txtReasonforTransfer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReasonforTransfer.Location = New System.Drawing.Point(24, 387)
        Me.txtReasonforTransfer.Multiline = True
        Me.txtReasonforTransfer.Name = "txtReasonforTransfer"
        Me.txtReasonforTransfer.Size = New System.Drawing.Size(290, 93)
        Me.txtReasonforTransfer.TabIndex = 264
        Me.txtReasonforTransfer.Text = "For Booking to Barangay Accounting to specific Account titles."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(19, 369)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 15)
        Me.Label1.TabIndex = 265
        Me.Label1.Text = "Reason for Transfer"
        '
        'Transfer_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(733, 546)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Transfer_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer_Form"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents po_date As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents txtAccountableTO As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents combo_position As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFundCluster As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPTRSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnITRSave As System.Windows.Forms.Button
    Friend WithEvents lblTransfer As System.Windows.Forms.Label
    Friend WithEvents combo_entity_departments As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents approved_by As System.Windows.Forms.TextBox
    Friend WithEvents issued_designation As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents issued_by As System.Windows.Forms.TextBox
    Friend WithEvents approved_designation As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAccountableFROM As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtReasonforTransfer As System.Windows.Forms.TextBox
End Class
