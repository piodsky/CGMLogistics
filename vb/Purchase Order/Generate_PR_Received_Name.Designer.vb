<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Generate_PR_Received_Name
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
        Me.combo_pr_received_name = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnContinue = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'combo_pr_received_name
        '
        Me.combo_pr_received_name.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_pr_received_name.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.combo_pr_received_name.FormattingEnabled = True
        Me.combo_pr_received_name.Items.AddRange(New Object() {"IMELDA B. DAJAO", "JOEJEN R. TAGAAN", "JUSTINE HYNMAN C. PEN", "MARIANNE GRACE Y. MONTANO", "MAUREEN JANE P. BAUTISTA", "MICHAEL ANGELO S. CALAOR", "NOVAVILLA D. OLBENAR", "OLIVE CHERYL LYNN O. INTO", "RHODA B. GUZMAN", "TIFFANY C. GALINDO"})
        Me.combo_pr_received_name.Location = New System.Drawing.Point(10, 58)
        Me.combo_pr_received_name.Name = "combo_pr_received_name"
        Me.combo_pr_received_name.Size = New System.Drawing.Size(410, 27)
        Me.combo_pr_received_name.TabIndex = 100
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(6, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(135, 19)
        Me.Label7.TabIndex = 99
        Me.Label7.Text = "PR Received Name:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnContinue)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.combo_pr_received_name)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(441, 167)
        Me.GroupBox1.TabIndex = 101
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add PR Received Name"
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
        'Generate_PR_Received_Name
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(459, 190)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Generate_PR_Received_Name"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate_PR_Received_Name"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents combo_pr_received_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnContinue As System.Windows.Forms.Button
End Class
