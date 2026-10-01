<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Generate_Category
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.combo_category = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnContinue = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 19)
        Me.Label1.TabIndex = 129
        Me.Label1.Text = "Category:"
        '
        'combo_category
        '
        Me.combo_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_category.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.combo_category.FormattingEnabled = True
        Me.combo_category.Location = New System.Drawing.Point(10, 59)
        Me.combo_category.Name = "combo_category"
        Me.combo_category.Size = New System.Drawing.Size(410, 27)
        Me.combo_category.TabIndex = 130
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnContinue)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.combo_category)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(441, 167)
        Me.GroupBox1.TabIndex = 131
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add Category"
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.White
        Me.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContinue.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.Black
        Me.btnContinue.Location = New System.Drawing.Point(320, 109)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(100, 31)
        Me.btnContinue.TabIndex = 126
        Me.btnContinue.Text = "Save"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'Generate_Category
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(460, 182)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Generate_Category"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate_Category"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents combo_category As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnContinue As System.Windows.Forms.Button
End Class
