<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PO_Choices
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
        Me.btnGF = New System.Windows.Forms.Button
        Me.btnTF = New System.Windows.Forms.Button
        Me.btnSEF = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnGF
        '
        Me.btnGF.BackColor = System.Drawing.Color.White
        Me.btnGF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGF.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGF.ForeColor = System.Drawing.Color.Green
        Me.btnGF.Location = New System.Drawing.Point(62, 176)
        Me.btnGF.Name = "btnGF"
        Me.btnGF.Size = New System.Drawing.Size(220, 105)
        Me.btnGF.TabIndex = 0
        Me.btnGF.Text = "General Fund (GF)"
        Me.btnGF.UseVisualStyleBackColor = False
        '
        'btnTF
        '
        Me.btnTF.BackColor = System.Drawing.Color.White
        Me.btnTF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTF.Font = New System.Drawing.Font("Calibri", 15.75!)
        Me.btnTF.ForeColor = System.Drawing.Color.Blue
        Me.btnTF.Location = New System.Drawing.Point(326, 176)
        Me.btnTF.Name = "btnTF"
        Me.btnTF.Size = New System.Drawing.Size(220, 105)
        Me.btnTF.TabIndex = 1
        Me.btnTF.Text = "Trust Fund (TF)"
        Me.btnTF.UseVisualStyleBackColor = False
        '
        'btnSEF
        '
        Me.btnSEF.BackColor = System.Drawing.Color.White
        Me.btnSEF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSEF.Font = New System.Drawing.Font("Calibri", 15.75!)
        Me.btnSEF.ForeColor = System.Drawing.Color.Tomato
        Me.btnSEF.Location = New System.Drawing.Point(591, 176)
        Me.btnSEF.Name = "btnSEF"
        Me.btnSEF.Size = New System.Drawing.Size(220, 105)
        Me.btnSEF.TabIndex = 2
        Me.btnSEF.Text = "Special Education Fund (SEF)"
        Me.btnSEF.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Copperplate Gothic Light", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.IndianRed
        Me.Label1.Location = New System.Drawing.Point(221, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(458, 54)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Purchase Order"
        '
        'PO_Choices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(876, 419)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSEF)
        Me.Controls.Add(Me.btnTF)
        Me.Controls.Add(Me.btnGF)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PO_Choices"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PO_Choices"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGF As System.Windows.Forms.Button
    Friend WithEvents btnTF As System.Windows.Forms.Button
    Friend WithEvents btnSEF As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
