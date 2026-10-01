<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Choose_User
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Choose_User))
        Me.dgvUser = New System.Windows.Forms.DataGridView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AssignToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnUse = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.id_number2 = New System.Windows.Forms.Label
        Me.id_number1 = New System.Windows.Forms.Label
        Me.txts_name = New System.Windows.Forms.TextBox
        Me.txtSName = New System.Windows.Forms.Label
        Me.txtf_name = New System.Windows.Forms.TextBox
        Me.txtFName = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.dgvUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvUser
        '
        Me.dgvUser.AllowUserToAddRows = False
        Me.dgvUser.AllowUserToDeleteRows = False
        Me.dgvUser.AllowUserToResizeRows = False
        Me.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUser.BackgroundColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUser.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUser.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUser.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvUser.EnableHeadersVisualStyles = False
        Me.dgvUser.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvUser.Location = New System.Drawing.Point(6, 19)
        Me.dgvUser.MultiSelect = False
        Me.dgvUser.Name = "dgvUser"
        Me.dgvUser.ReadOnly = True
        Me.dgvUser.RowHeadersVisible = False
        Me.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUser.Size = New System.Drawing.Size(767, 357)
        Me.dgvUser.TabIndex = 100
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UseToolStripMenuItem, Me.AssignToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(110, 48)
        '
        'UseToolStripMenuItem
        '
        Me.UseToolStripMenuItem.Name = "UseToolStripMenuItem"
        Me.UseToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.UseToolStripMenuItem.Text = "Use"
        '
        'AssignToolStripMenuItem
        '
        Me.AssignToolStripMenuItem.Name = "AssignToolStripMenuItem"
        Me.AssignToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.AssignToolStripMenuItem.Text = "Assign"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.btnUse)
        Me.GroupBox1.Controls.Add(Me.dgvUser)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(5, 116)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(782, 430)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User List"
        '
        'btnUse
        '
        Me.btnUse.BackColor = System.Drawing.Color.Brown
        Me.btnUse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUse.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUse.ForeColor = System.Drawing.Color.White
        Me.btnUse.Location = New System.Drawing.Point(654, 382)
        Me.btnUse.Name = "btnUse"
        Me.btnUse.Size = New System.Drawing.Size(119, 35)
        Me.btnUse.TabIndex = 40
        Me.btnUse.Text = "Use"
        Me.btnUse.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.Red
        Me.Button3.Location = New System.Drawing.Point(440, 61)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(135, 36)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Clear"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Blue
        Me.Button2.Location = New System.Drawing.Point(440, 22)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(135, 34)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Search/Filter"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'id_number2
        '
        Me.id_number2.AutoSize = True
        Me.id_number2.Location = New System.Drawing.Point(700, 32)
        Me.id_number2.Name = "id_number2"
        Me.id_number2.Size = New System.Drawing.Size(62, 13)
        Me.id_number2.TabIndex = 7
        Me.id_number2.Text = "id_number2"
        Me.id_number2.Visible = False
        '
        'id_number1
        '
        Me.id_number1.AutoSize = True
        Me.id_number1.Location = New System.Drawing.Point(700, 9)
        Me.id_number1.Name = "id_number1"
        Me.id_number1.Size = New System.Drawing.Size(62, 13)
        Me.id_number1.TabIndex = 6
        Me.id_number1.Text = "id_number1"
        Me.id_number1.Visible = False
        '
        'txts_name
        '
        Me.txts_name.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txts_name.Location = New System.Drawing.Point(122, 77)
        Me.txts_name.Name = "txts_name"
        Me.txts_name.Size = New System.Drawing.Size(227, 23)
        Me.txts_name.TabIndex = 5
        '
        'txtSName
        '
        Me.txtSName.AutoSize = True
        Me.txtSName.BackColor = System.Drawing.Color.Transparent
        Me.txtSName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSName.Location = New System.Drawing.Point(36, 77)
        Me.txtSName.Name = "txtSName"
        Me.txtSName.Size = New System.Drawing.Size(62, 15)
        Me.txtSName.TabIndex = 4
        Me.txtSName.Text = "SurName :"
        '
        'txtf_name
        '
        Me.txtf_name.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtf_name.Location = New System.Drawing.Point(122, 51)
        Me.txtf_name.Name = "txtf_name"
        Me.txtf_name.Size = New System.Drawing.Size(227, 23)
        Me.txtf_name.TabIndex = 3
        '
        'txtFName
        '
        Me.txtFName.AutoSize = True
        Me.txtFName.BackColor = System.Drawing.Color.Transparent
        Me.txtFName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFName.Location = New System.Drawing.Point(36, 51)
        Me.txtFName.Name = "txtFName"
        Me.txtFName.Size = New System.Drawing.Size(72, 15)
        Me.txtFName.TabIndex = 2
        Me.txtFName.Text = "First Name :"
        '
        'txtID
        '
        Me.txtID.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtID.Location = New System.Drawing.Point(122, 25)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(227, 23)
        Me.txtID.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(36, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID Number :"
        '
        'Choose_User
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(794, 558)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.id_number2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.id_number1)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txts_name)
        Me.Controls.Add(Me.txtFName)
        Me.Controls.Add(Me.txtSName)
        Me.Controls.Add(Me.txtf_name)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Choose_User"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Choose User"
        CType(Me.dgvUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvUser As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txts_name As System.Windows.Forms.TextBox
    Friend WithEvents txtSName As System.Windows.Forms.Label
    Friend WithEvents txtf_name As System.Windows.Forms.TextBox
    Friend WithEvents txtFName As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents id_number2 As System.Windows.Forms.Label
    Friend WithEvents id_number1 As System.Windows.Forms.Label
    Friend WithEvents btnUse As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents AssignToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
