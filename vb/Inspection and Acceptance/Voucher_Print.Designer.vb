<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Voucher_Print
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
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.combo_category = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.combo_category_explanation = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtTIN = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDepartment = New System.Windows.Forms.TextBox
        Me.txtMayor = New System.Windows.Forms.TextBox
        Me.txtTreasurer = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSupplierAddress = New System.Windows.Forms.TextBox
        Me.txtSupplierName = New System.Windows.Forms.TextBox
        Me.txtAccountant = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Inspect_Status = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(14, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 15)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Supplier Address"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.Controls.Add(Me.combo_category)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.combo_category_explanation)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTIN)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDepartment)
        Me.GroupBox1.Controls.Add(Me.txtMayor)
        Me.GroupBox1.Controls.Add(Me.txtTreasurer)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSupplierAddress)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.txtAccountant)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(8, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(462, 452)
        Me.GroupBox1.TabIndex = 174
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Please fill up the Details for the Disbursement Voucher"
        '
        'combo_category
        '
        Me.combo_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_category.FormattingEnabled = True
        Me.combo_category.Location = New System.Drawing.Point(122, 167)
        Me.combo_category.Name = "combo_category"
        Me.combo_category.Size = New System.Drawing.Size(324, 23)
        Me.combo_category.TabIndex = 129
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(18, 170)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 15)
        Me.Label9.TabIndex = 128
        Me.Label9.Text = "Category"
        '
        'combo_category_explanation
        '
        Me.combo_category_explanation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.combo_category_explanation.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_category_explanation.ForeColor = System.Drawing.Color.Black
        Me.combo_category_explanation.Location = New System.Drawing.Point(122, 203)
        Me.combo_category_explanation.Multiline = True
        Me.combo_category_explanation.Name = "combo_category_explanation"
        Me.combo_category_explanation.Size = New System.Drawing.Size(324, 101)
        Me.combo_category_explanation.TabIndex = 127
        Me.combo_category_explanation.Text = "Explanation"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(14, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 15)
        Me.Label8.TabIndex = 126
        Me.Label8.Text = "TIN Number"
        '
        'txtTIN
        '
        Me.txtTIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTIN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTIN.ForeColor = System.Drawing.Color.Black
        Me.txtTIN.Location = New System.Drawing.Point(122, 106)
        Me.txtTIN.Name = "txtTIN"
        Me.txtTIN.Size = New System.Drawing.Size(241, 23)
        Me.txtTIN.TabIndex = 125
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(14, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 15)
        Me.Label1.TabIndex = 124
        Me.Label1.Text = "Department Initial"
        '
        'txtDepartment
        '
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.ForeColor = System.Drawing.Color.Black
        Me.txtDepartment.Location = New System.Drawing.Point(122, 134)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(109, 23)
        Me.txtDepartment.TabIndex = 123
        '
        'txtMayor
        '
        Me.txtMayor.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMayor.ForeColor = System.Drawing.Color.Black
        Me.txtMayor.Location = New System.Drawing.Point(122, 368)
        Me.txtMayor.Name = "txtMayor"
        Me.txtMayor.Size = New System.Drawing.Size(241, 23)
        Me.txtMayor.TabIndex = 121
        '
        'txtTreasurer
        '
        Me.txtTreasurer.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTreasurer.ForeColor = System.Drawing.Color.Black
        Me.txtTreasurer.Location = New System.Drawing.Point(122, 339)
        Me.txtTreasurer.Name = "txtTreasurer"
        Me.txtTreasurer.Size = New System.Drawing.Size(241, 23)
        Me.txtTreasurer.TabIndex = 120
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(18, 368)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 15)
        Me.Label7.TabIndex = 119
        Me.Label7.Text = "City Mayor"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(18, 310)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 15)
        Me.Label6.TabIndex = 118
        Me.Label6.Text = "City Accountant"
        '
        'txtSupplierAddress
        '
        Me.txtSupplierAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierAddress.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierAddress.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierAddress.Location = New System.Drawing.Point(122, 55)
        Me.txtSupplierAddress.Multiline = True
        Me.txtSupplierAddress.Name = "txtSupplierAddress"
        Me.txtSupplierAddress.Size = New System.Drawing.Size(324, 45)
        Me.txtSupplierAddress.TabIndex = 117
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(122, 26)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(324, 23)
        Me.txtSupplierName.TabIndex = 116
        '
        'txtAccountant
        '
        Me.txtAccountant.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountant.ForeColor = System.Drawing.Color.Black
        Me.txtAccountant.Location = New System.Drawing.Point(122, 310)
        Me.txtAccountant.Name = "txtAccountant"
        Me.txtAccountant.Size = New System.Drawing.Size(241, 23)
        Me.txtAccountant.TabIndex = 114
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(18, 339)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 15)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "City Treasurer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(18, 205)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 30)
        Me.Label2.TabIndex = 63
        Me.Label2.Text = "Category " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Explanation "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(14, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 15)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Supplier Name"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(314, 404)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(132, 28)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'StatusStrip2
        '
        Me.StatusStrip2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel3, Me.Inspect_Status})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 461)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(477, 22)
        Me.StatusStrip2.TabIndex = 188
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(42, 17)
        Me.ToolStripStatusLabel3.Text = "Status:"
        '
        'Inspect_Status
        '
        Me.Inspect_Status.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Inspect_Status.ForeColor = System.Drawing.Color.Black
        Me.Inspect_Status.Name = "Inspect_Status"
        Me.Inspect_Status.Size = New System.Drawing.Size(82, 17)
        Me.Inspect_Status.Text = "Inspect_Status"
        '
        'Voucher_Print
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(477, 483)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Voucher_Print"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Voucher_Print"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSupplierAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountant As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtMayor As System.Windows.Forms.TextBox
    Friend WithEvents txtTreasurer As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTIN As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Inspect_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents combo_category_explanation As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents combo_category As System.Windows.Forms.ComboBox
End Class
