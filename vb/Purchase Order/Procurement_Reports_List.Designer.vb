<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Procurement_Reports_List
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
        Me.btnGenerate = New System.Windows.Forms.Button
        Me.btnIAR = New System.Windows.Forms.Button
        Me.btnPOIAR = New System.Windows.Forms.Button
        Me.btnPOIARItems = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.ForeColor = System.Drawing.Color.Black
        Me.btnGenerate.Location = New System.Drawing.Point(43, 52)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(368, 61)
        Me.btnGenerate.TabIndex = 6
        Me.btnGenerate.Text = "Purchase Order Reports"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnIAR
        '
        Me.btnIAR.BackColor = System.Drawing.Color.White
        Me.btnIAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIAR.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIAR.ForeColor = System.Drawing.Color.Black
        Me.btnIAR.Location = New System.Drawing.Point(43, 128)
        Me.btnIAR.Name = "btnIAR"
        Me.btnIAR.Size = New System.Drawing.Size(368, 61)
        Me.btnIAR.TabIndex = 7
        Me.btnIAR.Text = "Inspection and Acceptance Reports"
        Me.btnIAR.UseVisualStyleBackColor = False
        '
        'btnPOIAR
        '
        Me.btnPOIAR.BackColor = System.Drawing.Color.White
        Me.btnPOIAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPOIAR.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPOIAR.ForeColor = System.Drawing.Color.Black
        Me.btnPOIAR.Location = New System.Drawing.Point(43, 205)
        Me.btnPOIAR.Name = "btnPOIAR"
        Me.btnPOIAR.Size = New System.Drawing.Size(368, 61)
        Me.btnPOIAR.TabIndex = 8
        Me.btnPOIAR.Text = "POIAR Reports"
        Me.btnPOIAR.UseVisualStyleBackColor = False
        '
        'btnPOIARItems
        '
        Me.btnPOIARItems.BackColor = System.Drawing.Color.White
        Me.btnPOIARItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPOIARItems.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPOIARItems.ForeColor = System.Drawing.Color.Black
        Me.btnPOIARItems.Location = New System.Drawing.Point(43, 281)
        Me.btnPOIARItems.Name = "btnPOIARItems"
        Me.btnPOIARItems.Size = New System.Drawing.Size(368, 61)
        Me.btnPOIARItems.TabIndex = 9
        Me.btnPOIARItems.Text = "PO Items Reports"
        Me.btnPOIARItems.UseVisualStyleBackColor = False
        '
        'Procurement_Reports_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(466, 489)
        Me.Controls.Add(Me.btnPOIARItems)
        Me.Controls.Add(Me.btnPOIAR)
        Me.Controls.Add(Me.btnIAR)
        Me.Controls.Add(Me.btnGenerate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Procurement_Reports_List"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PO_Reports_List"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents btnIAR As System.Windows.Forms.Button
    Friend WithEvents btnPOIAR As System.Windows.Forms.Button
    Friend WithEvents btnPOIARItems As System.Windows.Forms.Button
End Class
