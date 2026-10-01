<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Generate_Time_Add_COA
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
        Me.coa_date = New System.Windows.Forms.DateTimePicker
        Me.btnContinue = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 19)
        Me.Label1.TabIndex = 122
        Me.Label1.Text = "COA Date Received"
        '
        'coa_date
        '
        Me.coa_date.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.coa_date.Location = New System.Drawing.Point(52, 59)
        Me.coa_date.Name = "coa_date"
        Me.coa_date.Size = New System.Drawing.Size(313, 27)
        Me.coa_date.TabIndex = 121
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.White
        Me.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContinue.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.Black
        Me.btnContinue.Location = New System.Drawing.Point(265, 110)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(100, 31)
        Me.btnContinue.TabIndex = 120
        Me.btnContinue.Text = "Save"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnContinue)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.coa_date)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(382, 158)
        Me.GroupBox1.TabIndex = 123
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add COA Date Received"
        '
        'Generate_Time_Add_COA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(402, 179)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Generate_Time_Add_COA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate_Time_Add_COA"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents coa_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
