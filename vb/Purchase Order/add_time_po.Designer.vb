<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class add_time_po
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
        Me.Label19 = New System.Windows.Forms.Label
        Me.Ref_Number = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.BAC_Resolution_Number = New System.Windows.Forms.TextBox
        Me.btnPrintPublicBiddingbyLot = New System.Windows.Forms.Button
        Me.btnPrintPublicBidding = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.combo_payment = New System.Windows.Forms.ComboBox
        Me.btnPrintbyLot = New System.Windows.Forms.Button
        Me.combo_incharge = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtNOTE = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.date_printed = New System.Windows.Forms.TextBox
        Me.txtInitial = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.combo_PLACE = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.combo_days = New System.Windows.Forms.ComboBox
        Me.combo_MODE = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Ref_Number)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.BAC_Resolution_Number)
        Me.GroupBox1.Controls.Add(Me.btnPrintPublicBiddingbyLot)
        Me.GroupBox1.Controls.Add(Me.btnPrintPublicBidding)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.combo_payment)
        Me.GroupBox1.Controls.Add(Me.btnPrintbyLot)
        Me.GroupBox1.Controls.Add(Me.combo_incharge)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtNOTE)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.date_printed)
        Me.GroupBox1.Controls.Add(Me.txtInitial)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.combo_PLACE)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.combo_days)
        Me.GroupBox1.Controls.Add(Me.combo_MODE)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(5, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(482, 475)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Please select number of days for items to be delivered"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(63, 89)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 15)
        Me.Label19.TabIndex = 161
        Me.Label19.Text = "Reference #:"
        '
        'Ref_Number
        '
        Me.Ref_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Ref_Number.Enabled = False
        Me.Ref_Number.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.Ref_Number.ForeColor = System.Drawing.Color.ForestGreen
        Me.Ref_Number.Location = New System.Drawing.Point(141, 87)
        Me.Ref_Number.Name = "Ref_Number"
        Me.Ref_Number.Size = New System.Drawing.Size(266, 23)
        Me.Ref_Number.TabIndex = 159
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(63, 124)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(69, 15)
        Me.Label20.TabIndex = 158
        Me.Label20.Text = "BAC Reso #:"
        '
        'BAC_Resolution_Number
        '
        Me.BAC_Resolution_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BAC_Resolution_Number.Enabled = False
        Me.BAC_Resolution_Number.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Italic)
        Me.BAC_Resolution_Number.ForeColor = System.Drawing.Color.ForestGreen
        Me.BAC_Resolution_Number.Location = New System.Drawing.Point(141, 118)
        Me.BAC_Resolution_Number.Name = "BAC_Resolution_Number"
        Me.BAC_Resolution_Number.Size = New System.Drawing.Size(266, 23)
        Me.BAC_Resolution_Number.TabIndex = 160
        '
        'btnPrintPublicBiddingbyLot
        '
        Me.btnPrintPublicBiddingbyLot.BackColor = System.Drawing.Color.White
        Me.btnPrintPublicBiddingbyLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintPublicBiddingbyLot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintPublicBiddingbyLot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrintPublicBiddingbyLot.Location = New System.Drawing.Point(17, 397)
        Me.btnPrintPublicBiddingbyLot.Name = "btnPrintPublicBiddingbyLot"
        Me.btnPrintPublicBiddingbyLot.Size = New System.Drawing.Size(149, 28)
        Me.btnPrintPublicBiddingbyLot.TabIndex = 128
        Me.btnPrintPublicBiddingbyLot.Text = "Print by Lot (Public Bidding)"
        Me.btnPrintPublicBiddingbyLot.UseVisualStyleBackColor = False
        '
        'btnPrintPublicBidding
        '
        Me.btnPrintPublicBidding.BackColor = System.Drawing.Color.White
        Me.btnPrintPublicBidding.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintPublicBidding.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintPublicBidding.ForeColor = System.Drawing.Color.Black
        Me.btnPrintPublicBidding.Location = New System.Drawing.Point(17, 431)
        Me.btnPrintPublicBidding.Name = "btnPrintPublicBidding"
        Me.btnPrintPublicBidding.Size = New System.Drawing.Size(149, 28)
        Me.btnPrintPublicBidding.TabIndex = 127
        Me.btnPrintPublicBidding.Text = "Print (Public Bidding)"
        Me.btnPrintPublicBidding.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(323, 387)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 126
        Me.Label10.Text = "Payment Term"
        '
        'combo_payment
        '
        Me.combo_payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_payment.FormattingEnabled = True
        Me.combo_payment.Items.AddRange(New Object() {"0", "5", "10", "15", "20", "25", "30", "10%", "20%", "30%", "40%", "50%", "Cash Basis", "As Needed", "Immediately", "Progress Billing", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Daily", "Weekly", "Monthly", "Yearly"})
        Me.combo_payment.Location = New System.Drawing.Point(214, 383)
        Me.combo_payment.Name = "combo_payment"
        Me.combo_payment.Size = New System.Drawing.Size(103, 23)
        Me.combo_payment.TabIndex = 125
        Me.combo_payment.Text = "30"
        '
        'btnPrintbyLot
        '
        Me.btnPrintbyLot.BackColor = System.Drawing.Color.White
        Me.btnPrintbyLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintbyLot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintbyLot.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrintbyLot.Location = New System.Drawing.Point(200, 431)
        Me.btnPrintbyLot.Name = "btnPrintbyLot"
        Me.btnPrintbyLot.Size = New System.Drawing.Size(132, 28)
        Me.btnPrintbyLot.TabIndex = 124
        Me.btnPrintbyLot.Text = "Print by Lot"
        Me.btnPrintbyLot.UseVisualStyleBackColor = False
        '
        'combo_incharge
        '
        Me.combo_incharge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_incharge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_incharge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.combo_incharge.FormattingEnabled = True
        Me.combo_incharge.Items.AddRange(New Object() {"JAY WARREN R. PABILLARAN", "ESTELITO R. MARABE, CPA"})
        Me.combo_incharge.Location = New System.Drawing.Point(142, 216)
        Me.combo_incharge.Name = "combo_incharge"
        Me.combo_incharge.Size = New System.Drawing.Size(316, 21)
        Me.combo_incharge.TabIndex = 123
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(72, 218)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 15)
        Me.Label9.TabIndex = 122
        Me.Label9.Text = "In Charge:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(94, 246)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 15)
        Me.Label8.TabIndex = 121
        Me.Label8.Text = "NOTE:"
        '
        'txtNOTE
        '
        Me.txtNOTE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNOTE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNOTE.ForeColor = System.Drawing.Color.Black
        Me.txtNOTE.Location = New System.Drawing.Point(143, 244)
        Me.txtNOTE.Multiline = True
        Me.txtNOTE.Name = "txtNOTE"
        Me.txtNOTE.Size = New System.Drawing.Size(316, 97)
        Me.txtNOTE.TabIndex = 72
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(97, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 15)
        Me.Label6.TabIndex = 119
        Me.Label6.Text = "Date:"
        '
        'date_printed
        '
        Me.date_printed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.date_printed.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.date_printed.ForeColor = System.Drawing.Color.Black
        Me.date_printed.Location = New System.Drawing.Point(142, 154)
        Me.date_printed.Name = "date_printed"
        Me.date_printed.Size = New System.Drawing.Size(265, 23)
        Me.date_printed.TabIndex = 68
        '
        'txtInitial
        '
        Me.txtInitial.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInitial.ForeColor = System.Drawing.Color.Black
        Me.txtInitial.Location = New System.Drawing.Point(142, 187)
        Me.txtInitial.Name = "txtInitial"
        Me.txtInitial.Size = New System.Drawing.Size(132, 20)
        Me.txtInitial.TabIndex = 70
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(42, 192)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 15)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "Initial Signature:"
        '
        'combo_PLACE
        '
        Me.combo_PLACE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_PLACE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_PLACE.ForeColor = System.Drawing.Color.Chocolate
        Me.combo_PLACE.FormattingEnabled = True
        Me.combo_PLACE.Items.AddRange(New Object() {"CGSO WAREHOUSE", "JOB SITE", "PICK UP"})
        Me.combo_PLACE.Location = New System.Drawing.Point(142, 57)
        Me.combo_PLACE.Name = "combo_PLACE"
        Me.combo_PLACE.Size = New System.Drawing.Size(316, 21)
        Me.combo_PLACE.TabIndex = 62
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(38, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 15)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Place of Delivery:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(323, 358)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Working Days"
        '
        'combo_days
        '
        Me.combo_days.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_days.FormattingEnabled = True
        Me.combo_days.Items.AddRange(New Object() {"0", "5", "10", "15", "20", "25", "30", "45", "As Needed", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Daily", "Weekly", "Monthly", "Yearly"})
        Me.combo_days.Location = New System.Drawing.Point(214, 354)
        Me.combo_days.Name = "combo_days"
        Me.combo_days.Size = New System.Drawing.Size(103, 23)
        Me.combo_days.TabIndex = 74
        Me.combo_days.Text = "15"
        '
        'combo_MODE
        '
        Me.combo_MODE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_MODE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_MODE.ForeColor = System.Drawing.Color.Chocolate
        Me.combo_MODE.FormattingEnabled = True
        Me.combo_MODE.Items.AddRange(New Object() {"SVP", "Public Bidding/Competitive Bidding (Sec. 10)", "Limited Source Bidding (Sec. 49)", "Direct Contracting (Sec. 50)", "Repeat Order (Sec. 51)", "Shopping (Sec. 52)", "Negotiated (Sec. 53.1 Two Failed Biddings)", "Negotiated (Sec. 53.2 Emergency Cases)", "Negotiated (Sec. 53.3 Take-over contracts)", "Negotiated (Sec. 53.4 Adjacent or Contiguous)", "Negotiated (Sec. 53.5 Agency to Agency)", "Negotiated (Sec. 53.6 Scientific, Scholarly)", "Negotiated (Sec. 53.7 Highly Technical Consultants)", "Negotiated (Sec. 53.8 Defense Cooperation Agreement)", "Negotiated (Sec. 53.9 Small Value Procurement)", "Negotiated (Sec. 53.10 Lease of Real Property and Venue)", "Negotiated (Sec. 53.11 NGO Participation)", "Negotiated (Sec. 53.12 Community Participation)", "Negotiated (Sec. 53.13 UN Agencies, Internation Organizations)", "Negotiated (Sec. 53.14 Direct Retail Purchase of POL)"})
        Me.combo_MODE.Location = New System.Drawing.Point(142, 26)
        Me.combo_MODE.Name = "combo_MODE"
        Me.combo_MODE.Size = New System.Drawing.Size(316, 21)
        Me.combo_MODE.TabIndex = 60
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(14, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(125, 15)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Mode of Procurement:"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(338, 431)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(132, 28)
        Me.btnSave.TabIndex = 76
        Me.btnSave.Text = "Print"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'add_time_po
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(489, 496)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "add_time_po"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Purchase Order"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents combo_days As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents combo_PLACE As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents combo_MODE As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtInitial As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNOTE As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents date_printed As System.Windows.Forms.TextBox
    Friend WithEvents combo_incharge As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnPrintbyLot As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents combo_payment As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintPublicBidding As System.Windows.Forms.Button
    Friend WithEvents btnPrintPublicBiddingbyLot As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Ref_Number As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents BAC_Resolution_Number As System.Windows.Forms.TextBox
End Class
