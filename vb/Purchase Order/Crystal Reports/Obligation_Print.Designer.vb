<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Obligation_Print
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtBarcodeNumber = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtTotalAmount = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnSave2 = New System.Windows.Forms.Button
        Me.combo_end_user = New System.Windows.Forms.ComboBox
        Me.txtposition = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtDepartment = New System.Windows.Forms.TextBox
        Me.combo_category = New System.Windows.Forms.ComboBox
        Me.txtBudget = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtSupplierAddress = New System.Windows.Forms.TextBox
        Me.txtSupplierName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.Controls.Add(Me.txtBarcodeNumber)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtTotalAmount)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnSave2)
        Me.GroupBox1.Controls.Add(Me.combo_end_user)
        Me.GroupBox1.Controls.Add(Me.txtposition)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtDepartment)
        Me.GroupBox1.Controls.Add(Me.combo_category)
        Me.GroupBox1.Controls.Add(Me.txtBudget)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSupplierAddress)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(7, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(464, 375)
        Me.GroupBox1.TabIndex = 175
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Please fill up the Details for the Obligation Request"
        '
        'txtBarcodeNumber
        '
        Me.txtBarcodeNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodeNumber.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeNumber.ForeColor = System.Drawing.Color.Black
        Me.txtBarcodeNumber.Location = New System.Drawing.Point(128, 27)
        Me.txtBarcodeNumber.Multiline = True
        Me.txtBarcodeNumber.Name = "txtBarcodeNumber"
        Me.txtBarcodeNumber.Size = New System.Drawing.Size(324, 23)
        Me.txtBarcodeNumber.TabIndex = 136
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(21, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 15)
        Me.Label9.TabIndex = 135
        Me.Label9.Text = "Barcode Number"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAmount.Location = New System.Drawing.Point(127, 285)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(186, 23)
        Me.txtTotalAmount.TabIndex = 134
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(21, 288)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 15)
        Me.Label6.TabIndex = 133
        Me.Label6.Text = "Total Amount"
        '
        'btnSave2
        '
        Me.btnSave2.BackColor = System.Drawing.Color.White
        Me.btnSave2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave2.ForeColor = System.Drawing.Color.Red
        Me.btnSave2.Location = New System.Drawing.Point(181, 323)
        Me.btnSave2.Name = "btnSave2"
        Me.btnSave2.Size = New System.Drawing.Size(132, 28)
        Me.btnSave2.TabIndex = 132
        Me.btnSave2.Text = "Reimbursement Print"
        Me.btnSave2.UseVisualStyleBackColor = False
        '
        'combo_end_user
        '
        Me.combo_end_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_end_user.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_end_user.ForeColor = System.Drawing.Color.Black
        Me.combo_end_user.FormattingEnabled = True
        Me.combo_end_user.Location = New System.Drawing.Point(128, 228)
        Me.combo_end_user.Name = "combo_end_user"
        Me.combo_end_user.Size = New System.Drawing.Size(324, 23)
        Me.combo_end_user.TabIndex = 131
        Me.combo_end_user.Text = "JAY WARREN R. PABILLARAN"
        '
        'txtposition
        '
        Me.txtposition.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtposition.ForeColor = System.Drawing.Color.Black
        Me.txtposition.Location = New System.Drawing.Point(128, 256)
        Me.txtposition.Name = "txtposition"
        Me.txtposition.Size = New System.Drawing.Size(324, 23)
        Me.txtposition.TabIndex = 130
        Me.txtposition.Text = "CITY MAYOR"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(21, 257)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 15)
        Me.Label3.TabIndex = 129
        Me.Label3.Text = "End User Position"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(21, 228)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 15)
        Me.Label1.TabIndex = 127
        Me.Label1.Text = "End User"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(21, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 15)
        Me.Label8.TabIndex = 126
        Me.Label8.Text = "Department"
        '
        'txtDepartment
        '
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.ForeColor = System.Drawing.Color.Black
        Me.txtDepartment.Location = New System.Drawing.Point(128, 138)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(324, 23)
        Me.txtDepartment.TabIndex = 125
        '
        'combo_category
        '
        Me.combo_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_category.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_category.ForeColor = System.Drawing.Color.Black
        Me.combo_category.FormattingEnabled = True
        Me.combo_category.Location = New System.Drawing.Point(128, 168)
        Me.combo_category.Name = "combo_category"
        Me.combo_category.Size = New System.Drawing.Size(324, 23)
        Me.combo_category.TabIndex = 122
        '
        'txtBudget
        '
        Me.txtBudget.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBudget.ForeColor = System.Drawing.Color.Black
        Me.txtBudget.Location = New System.Drawing.Point(128, 198)
        Me.txtBudget.Name = "txtBudget"
        Me.txtBudget.Size = New System.Drawing.Size(324, 23)
        Me.txtBudget.TabIndex = 121
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(21, 199)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 15)
        Me.Label7.TabIndex = 119
        Me.Label7.Text = "City Budget"
        '
        'txtSupplierAddress
        '
        Me.txtSupplierAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierAddress.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierAddress.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierAddress.Location = New System.Drawing.Point(128, 85)
        Me.txtSupplierAddress.Multiline = True
        Me.txtSupplierAddress.Name = "txtSupplierAddress"
        Me.txtSupplierAddress.Size = New System.Drawing.Size(324, 47)
        Me.txtSupplierAddress.TabIndex = 117
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(128, 56)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(324, 23)
        Me.txtSupplierName.TabIndex = 116
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(21, 169)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 63
        Me.Label2.Text = "Category"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(21, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 15)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Supplier Address"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(21, 58)
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
        Me.btnSave.Location = New System.Drawing.Point(319, 323)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(132, 28)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Payment Print"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Obligation_Print
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(477, 391)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Obligation_Print"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Obligation_Print"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents combo_category As System.Windows.Forms.ComboBox
    Friend WithEvents txtBudget As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents combo_end_user As System.Windows.Forms.ComboBox
    Friend WithEvents txtposition As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSave2 As System.Windows.Forms.Button
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBarcodeNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
