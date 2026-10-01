<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Add_PPMP_Items
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
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.combo_unit = New System.Windows.Forms.ComboBox
        Me.txt_Unit_Cost = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.items_details = New System.Windows.Forms.GroupBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ppmp_items_status = New System.Windows.Forms.ToolStripStatusLabel
        Me.items_details.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(12, 147)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(375, 272)
        Me.txtDescription.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Items Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Unit"
        '
        'combo_unit
        '
        Me.combo_unit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_unit.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.combo_unit.FormattingEnabled = True
        Me.combo_unit.Location = New System.Drawing.Point(83, 39)
        Me.combo_unit.Name = "combo_unit"
        Me.combo_unit.Size = New System.Drawing.Size(181, 27)
        Me.combo_unit.TabIndex = 3
        '
        'txt_Unit_Cost
        '
        Me.txt_Unit_Cost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Unit_Cost.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.txt_Unit_Cost.Location = New System.Drawing.Point(83, 80)
        Me.txt_Unit_Cost.Name = "txt_Unit_Cost"
        Me.txt_Unit_Cost.Size = New System.Drawing.Size(181, 27)
        Me.txt_Unit_Cost.TabIndex = 4
        Me.txt_Unit_Cost.Text = "0.00"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label3.Location = New System.Drawing.Point(8, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 19)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Unit Cost"
        '
        'items_details
        '
        Me.items_details.Controls.Add(Me.btnSave)
        Me.items_details.Controls.Add(Me.Label1)
        Me.items_details.Controls.Add(Me.Label3)
        Me.items_details.Controls.Add(Me.txtDescription)
        Me.items_details.Controls.Add(Me.txt_Unit_Cost)
        Me.items_details.Controls.Add(Me.Label2)
        Me.items_details.Controls.Add(Me.combo_unit)
        Me.items_details.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.items_details.Location = New System.Drawing.Point(12, 12)
        Me.items_details.Name = "items_details"
        Me.items_details.Size = New System.Drawing.Size(407, 484)
        Me.items_details.TabIndex = 6
        Me.items_details.TabStop = False
        Me.items_details.Text = "Please Fill up all Details to Add Items"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(12, 425)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(375, 41)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'StatusStrip2
        '
        Me.StatusStrip2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel3, Me.ppmp_items_status})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 499)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(428, 22)
        Me.StatusStrip2.TabIndex = 141
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(42, 17)
        Me.ToolStripStatusLabel3.Text = "Status:"
        '
        'ppmp_items_status
        '
        Me.ppmp_items_status.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ppmp_items_status.ForeColor = System.Drawing.Color.Black
        Me.ppmp_items_status.Name = "ppmp_items_status"
        Me.ppmp_items_status.Size = New System.Drawing.Size(109, 17)
        Me.ppmp_items_status.Text = "ppmp_items_status"
        '
        'Add_PPMP_Items
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(428, 521)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.items_details)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Add_PPMP_Items"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add_PPMP_Items"
        Me.items_details.ResumeLayout(False)
        Me.items_details.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents combo_unit As System.Windows.Forms.ComboBox
    Friend WithEvents txt_Unit_Cost As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents items_details As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ppmp_items_status As System.Windows.Forms.ToolStripStatusLabel
End Class
