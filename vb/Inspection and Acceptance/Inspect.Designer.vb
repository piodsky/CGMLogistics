<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inspect_Form
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inspect_Form))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.combo_inspect4 = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtRemarks = New Guna.UI.WinForms.GunaTextBox
        Me.txtBarcode = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.date_bac_received = New Guna.UI2.WinForms.Guna2DateTimePicker
        Me.panelProgressbilling = New System.Windows.Forms.Panel
        Me.lblBilled = New System.Windows.Forms.Label
        Me.lblDateTo = New System.Windows.Forms.Label
        Me.lblDateFrom = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.combo_progressbill = New System.Windows.Forms.ComboBox
        Me.lblProgressBilling = New System.Windows.Forms.Label
        Me.combo_inspect3 = New System.Windows.Forms.ComboBox
        Me.received5 = New System.Windows.Forms.ComboBox
        Me.received4 = New System.Windows.Forms.ComboBox
        Me.received3 = New System.Windows.Forms.ComboBox
        Me.received2 = New System.Windows.Forms.ComboBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.lblEdit = New System.Windows.Forms.Label
        Me.btnEdit = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnPrintDisbursement = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblsave = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnPrintInspection = New System.Windows.Forms.Button
        Me.txtTotalItems = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSubTotal = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.PO_Number = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.dgvInspect_ITEMLIST = New System.Windows.Forms.DataGridView
        Me.Item_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Units = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Item_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Unit_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Total_Cost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.accept = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.combo_inspect2 = New System.Windows.Forms.ComboBox
        Me.combo_inspect1 = New System.Windows.Forms.ComboBox
        Me.received1 = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Inspect_Status = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1.SuspendLayout()
        Me.panelProgressbilling.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.dgvInspect_ITEMLIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox1.Controls.Add(Me.combo_inspect4)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.txtBarcode)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.date_bac_received)
        Me.GroupBox1.Controls.Add(Me.panelProgressbilling)
        Me.GroupBox1.Controls.Add(Me.lblProgressBilling)
        Me.GroupBox1.Controls.Add(Me.combo_inspect3)
        Me.GroupBox1.Controls.Add(Me.received5)
        Me.GroupBox1.Controls.Add(Me.received4)
        Me.GroupBox1.Controls.Add(Me.received3)
        Me.GroupBox1.Controls.Add(Me.received2)
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.txtTotalItems)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtSubTotal)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.PO_Number)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.dgvInspect_ITEMLIST)
        Me.GroupBox1.Controls.Add(Me.accept)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.combo_inspect2)
        Me.GroupBox1.Controls.Add(Me.combo_inspect1)
        Me.GroupBox1.Controls.Add(Me.received1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1334, 673)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Inspection Details"
        '
        'combo_inspect4
        '
        Me.combo_inspect4.AllowDrop = True
        Me.combo_inspect4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.combo_inspect4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_inspect4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_inspect4.FormattingEnabled = True
        Me.combo_inspect4.Location = New System.Drawing.Point(96, 585)
        Me.combo_inspect4.Name = "combo_inspect4"
        Me.combo_inspect4.Size = New System.Drawing.Size(287, 23)
        Me.combo_inspect4.TabIndex = 230
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(16, 250)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(57, 15)
        Me.Label18.TabIndex = 229
        Me.Label18.Text = "Remarks:"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtRemarks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtRemarks.BackColor = System.Drawing.Color.Transparent
        Me.txtRemarks.BaseColor = System.Drawing.Color.White
        Me.txtRemarks.BorderColor = System.Drawing.Color.Black
        Me.txtRemarks.BorderSize = 1
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.FocusedBaseColor = System.Drawing.Color.White
        Me.txtRemarks.FocusedBorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemarks.FocusedForeColor = System.Drawing.SystemColors.ControlText
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtRemarks.Location = New System.Drawing.Point(43, 267)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtRemarks.Radius = 10
        Me.txtRemarks.SelectedText = ""
        Me.txtRemarks.Size = New System.Drawing.Size(286, 73)
        Me.txtRemarks.TabIndex = 228
        Me.txtRemarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'txtBarcode
        '
        Me.txtBarcode.BackColor = System.Drawing.Color.White
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.ForeColor = System.Drawing.Color.Red
        Me.txtBarcode.Location = New System.Drawing.Point(43, 225)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(287, 23)
        Me.txtBarcode.TabIndex = 226
        Me.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(16, 207)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(61, 15)
        Me.Label17.TabIndex = 225
        Me.Label17.Text = "BARCODE:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 160)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(141, 15)
        Me.Label5.TabIndex = 224
        Me.Label5.Text = "Date Received From BAC:"
        '
        'date_bac_received
        '
        Me.date_bac_received.BackColor = System.Drawing.Color.Transparent
        Me.date_bac_received.BorderRadius = 10
        Me.date_bac_received.BorderThickness = 1
        Me.date_bac_received.CheckedState.Parent = Me.date_bac_received
        Me.date_bac_received.FillColor = System.Drawing.Color.White
        Me.date_bac_received.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.date_bac_received.ForeColor = System.Drawing.Color.Black
        Me.date_bac_received.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.date_bac_received.HoverState.Parent = Me.date_bac_received
        Me.date_bac_received.Location = New System.Drawing.Point(43, 178)
        Me.date_bac_received.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.date_bac_received.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.date_bac_received.Name = "date_bac_received"
        Me.date_bac_received.ShadowDecoration.Parent = Me.date_bac_received
        Me.date_bac_received.Size = New System.Drawing.Size(287, 26)
        Me.date_bac_received.TabIndex = 223
        Me.date_bac_received.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.date_bac_received.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
        Me.date_bac_received.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.None
        Me.date_bac_received.Value = New Date(2023, 4, 25, 9, 42, 23, 73)
        '
        'panelProgressbilling
        '
        Me.panelProgressbilling.Controls.Add(Me.lblBilled)
        Me.panelProgressbilling.Controls.Add(Me.lblDateTo)
        Me.panelProgressbilling.Controls.Add(Me.lblDateFrom)
        Me.panelProgressbilling.Controls.Add(Me.Label16)
        Me.panelProgressbilling.Controls.Add(Me.Label12)
        Me.panelProgressbilling.Controls.Add(Me.Button1)
        Me.panelProgressbilling.Controls.Add(Me.datefrom)
        Me.panelProgressbilling.Controls.Add(Me.Label14)
        Me.panelProgressbilling.Controls.Add(Me.dateto)
        Me.panelProgressbilling.Controls.Add(Me.Label15)
        Me.panelProgressbilling.Controls.Add(Me.Label13)
        Me.panelProgressbilling.Controls.Add(Me.combo_progressbill)
        Me.panelProgressbilling.Location = New System.Drawing.Point(365, 90)
        Me.panelProgressbilling.Name = "panelProgressbilling"
        Me.panelProgressbilling.Size = New System.Drawing.Size(562, 110)
        Me.panelProgressbilling.TabIndex = 172
        '
        'lblBilled
        '
        Me.lblBilled.AutoSize = True
        Me.lblBilled.Font = New System.Drawing.Font("Calibri", 15.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBilled.Location = New System.Drawing.Point(427, 24)
        Me.lblBilled.Name = "lblBilled"
        Me.lblBilled.Size = New System.Drawing.Size(129, 24)
        Me.lblBilled.TabIndex = 173
        Me.lblBilled.Text = "Not Yet Billed!"
        '
        'lblDateTo
        '
        Me.lblDateTo.AutoSize = True
        Me.lblDateTo.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTo.ForeColor = System.Drawing.Color.Blue
        Me.lblDateTo.Location = New System.Drawing.Point(285, 85)
        Me.lblDateTo.Name = "lblDateTo"
        Me.lblDateTo.Size = New System.Drawing.Size(71, 19)
        Me.lblDateTo.TabIndex = 181
        Me.lblDateTo.Text = "lblDateTo"
        '
        'lblDateFrom
        '
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateFrom.ForeColor = System.Drawing.Color.Blue
        Me.lblDateFrom.Location = New System.Drawing.Point(285, 57)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(89, 19)
        Me.lblDateFrom.TabIndex = 180
        Me.lblDateFrom.Text = "lblDateFrom"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(356, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(0, 13)
        Me.Label16.TabIndex = 179
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(3, 5)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(265, 13)
        Me.Label12.TabIndex = 173
        Me.Label12.Text = "Please Select Progress Billing Menu to show the Items "
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Button1.Location = New System.Drawing.Point(6, 25)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 20)
        Me.Button1.TabIndex = 173
        Me.Button1.UseVisualStyleBackColor = False
        '
        'datefrom
        '
        Me.datefrom.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datefrom.Location = New System.Drawing.Point(133, 55)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(134, 23)
        Me.datefrom.TabIndex = 178
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(87, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 15)
        Me.Label14.TabIndex = 176
        Me.Label14.Text = "FROM"
        '
        'dateto
        '
        Me.dateto.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateto.Location = New System.Drawing.Point(133, 86)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(134, 23)
        Me.dateto.TabIndex = 175
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(93, 89)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(22, 15)
        Me.Label15.TabIndex = 177
        Me.Label15.Text = "TO"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(29, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(102, 16)
        Me.Label13.TabIndex = 174
        Me.Label13.Text = "Progress Billing"
        '
        'combo_progressbill
        '
        Me.combo_progressbill.AllowDrop = True
        Me.combo_progressbill.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.combo_progressbill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_progressbill.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_progressbill.FormattingEnabled = True
        Me.combo_progressbill.Items.AddRange(New Object() {"Progress Billing - 1", "Progress Billing - 2", "Progress Billing - 3", "Progress Billing - 4", "Progress Billing - 5", "Progress Billing - 6", "Progress Billing - 7", "Progress Billing - 8", "Progress Billing - 9", "Progress Billing - 10"})
        Me.combo_progressbill.Location = New System.Drawing.Point(133, 23)
        Me.combo_progressbill.Name = "combo_progressbill"
        Me.combo_progressbill.Size = New System.Drawing.Size(287, 27)
        Me.combo_progressbill.TabIndex = 173
        '
        'lblProgressBilling
        '
        Me.lblProgressBilling.AutoSize = True
        Me.lblProgressBilling.BackColor = System.Drawing.Color.AntiqueWhite
        Me.lblProgressBilling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProgressBilling.Font = New System.Drawing.Font("Copperplate Gothic Light", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgressBilling.ForeColor = System.Drawing.Color.Blue
        Me.lblProgressBilling.Location = New System.Drawing.Point(584, 28)
        Me.lblProgressBilling.Name = "lblProgressBilling"
        Me.lblProgressBilling.Size = New System.Drawing.Size(263, 24)
        Me.lblProgressBilling.TabIndex = 170
        Me.lblProgressBilling.Text = "- for Progress Billing"
        '
        'combo_inspect3
        '
        Me.combo_inspect3.AllowDrop = True
        Me.combo_inspect3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.combo_inspect3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_inspect3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_inspect3.FormattingEnabled = True
        Me.combo_inspect3.Location = New System.Drawing.Point(96, 558)
        Me.combo_inspect3.Name = "combo_inspect3"
        Me.combo_inspect3.Size = New System.Drawing.Size(287, 23)
        Me.combo_inspect3.TabIndex = 169
        '
        'received5
        '
        Me.received5.AllowDrop = True
        Me.received5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.received5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.received5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.received5.FormattingEnabled = True
        Me.received5.Location = New System.Drawing.Point(97, 471)
        Me.received5.Name = "received5"
        Me.received5.Size = New System.Drawing.Size(287, 23)
        Me.received5.TabIndex = 168
        '
        'received4
        '
        Me.received4.AllowDrop = True
        Me.received4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.received4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.received4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.received4.FormattingEnabled = True
        Me.received4.Location = New System.Drawing.Point(97, 442)
        Me.received4.Name = "received4"
        Me.received4.Size = New System.Drawing.Size(287, 23)
        Me.received4.TabIndex = 167
        '
        'received3
        '
        Me.received3.AllowDrop = True
        Me.received3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.received3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.received3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.received3.FormattingEnabled = True
        Me.received3.Location = New System.Drawing.Point(97, 413)
        Me.received3.Name = "received3"
        Me.received3.Size = New System.Drawing.Size(287, 23)
        Me.received3.TabIndex = 166
        '
        'received2
        '
        Me.received2.AllowDrop = True
        Me.received2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.received2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.received2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.received2.FormattingEnabled = True
        Me.received2.Location = New System.Drawing.Point(97, 384)
        Me.received2.Name = "received2"
        Me.received2.Size = New System.Drawing.Size(287, 23)
        Me.received2.TabIndex = 165
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Location = New System.Drawing.Point(11, 657)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1317, 10)
        Me.Panel3.TabIndex = 164
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Location = New System.Drawing.Point(1318, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(11, 644)
        Me.Panel1.TabIndex = 163
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblEdit)
        Me.GroupBox6.Controls.Add(Me.btnEdit)
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Controls.Add(Me.btnPrintDisbursement)
        Me.GroupBox6.Controls.Add(Me.btnSave)
        Me.GroupBox6.Controls.Add(Me.lblsave)
        Me.GroupBox6.Controls.Add(Me.Label1)
        Me.GroupBox6.Controls.Add(Me.btnPrintInspection)
        Me.GroupBox6.Location = New System.Drawing.Point(933, 16)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(379, 150)
        Me.GroupBox6.TabIndex = 162
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Menu"
        '
        'lblEdit
        '
        Me.lblEdit.AutoSize = True
        Me.lblEdit.BackColor = System.Drawing.Color.AntiqueWhite
        Me.lblEdit.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEdit.ForeColor = System.Drawing.Color.Red
        Me.lblEdit.Location = New System.Drawing.Point(114, 71)
        Me.lblEdit.Name = "lblEdit"
        Me.lblEdit.Size = New System.Drawing.Size(32, 18)
        Me.lblEdit.TabIndex = 173
        Me.lblEdit.Text = "Edit"
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.White
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Image = Global.Purchase_Request.My.Resources.Resources.edit_small
        Me.btnEdit.Location = New System.Drawing.Point(99, 24)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(68, 44)
        Me.btnEdit.TabIndex = 172
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label11.ForeColor = System.Drawing.Color.Green
        Me.Label11.Location = New System.Drawing.Point(278, 76)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(98, 54)
        Me.Label11.TabIndex = 171
        Me.Label11.Text = "      Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Disbursement " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    Voucher"
        '
        'btnPrintDisbursement
        '
        Me.btnPrintDisbursement.BackColor = System.Drawing.Color.White
        Me.btnPrintDisbursement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintDisbursement.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintDisbursement.ForeColor = System.Drawing.Color.Black
        Me.btnPrintDisbursement.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPrintDisbursement.Location = New System.Drawing.Point(281, 19)
        Me.btnPrintDisbursement.Name = "btnPrintDisbursement"
        Me.btnPrintDisbursement.Size = New System.Drawing.Size(72, 54)
        Me.btnPrintDisbursement.TabIndex = 170
        Me.btnPrintDisbursement.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.Purchase_Request.My.Resources.Resources.save_small
        Me.btnSave.Location = New System.Drawing.Point(15, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 44)
        Me.btnSave.TabIndex = 97
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblsave
        '
        Me.lblsave.AutoSize = True
        Me.lblsave.BackColor = System.Drawing.Color.AntiqueWhite
        Me.lblsave.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsave.ForeColor = System.Drawing.Color.Blue
        Me.lblsave.Location = New System.Drawing.Point(26, 71)
        Me.lblsave.Name = "lblsave"
        Me.lblsave.Size = New System.Drawing.Size(37, 18)
        Me.lblsave.TabIndex = 98
        Me.lblsave.Text = "Save"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Green
        Me.Label1.Location = New System.Drawing.Point(171, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 38)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "      Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  Inspection" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnPrintInspection
        '
        Me.btnPrintInspection.BackColor = System.Drawing.Color.White
        Me.btnPrintInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintInspection.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintInspection.ForeColor = System.Drawing.Color.Black
        Me.btnPrintInspection.Image = Global.Purchase_Request.My.Resources.Resources.printer_1_icon
        Me.btnPrintInspection.Location = New System.Drawing.Point(183, 19)
        Me.btnPrintInspection.Name = "btnPrintInspection"
        Me.btnPrintInspection.Size = New System.Drawing.Size(72, 54)
        Me.btnPrintInspection.TabIndex = 95
        Me.btnPrintInspection.UseVisualStyleBackColor = False
        '
        'txtTotalItems
        '
        Me.txtTotalItems.AutoSize = True
        Me.txtTotalItems.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalItems.Location = New System.Drawing.Point(1049, 593)
        Me.txtTotalItems.Name = "txtTotalItems"
        Me.txtTotalItems.Size = New System.Drawing.Size(20, 23)
        Me.txtTotalItems.TabIndex = 161
        Me.txtTotalItems.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1094, 593)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 19)
        Me.Label8.TabIndex = 160
        Me.Label8.Text = "Total Cost:"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.Color.Green
        Me.txtSubTotal.Location = New System.Drawing.Point(1178, 590)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(122, 27)
        Me.txtSubTotal.TabIndex = 159
        Me.txtSubTotal.Text = "0.00"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(975, 597)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 15)
        Me.Label10.TabIndex = 158
        Me.Label10.Text = "Total Items:"
        '
        'PO_Number
        '
        Me.PO_Number.AutoSize = True
        Me.PO_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PO_Number.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PO_Number.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PO_Number.ForeColor = System.Drawing.Color.ForestGreen
        Me.PO_Number.Location = New System.Drawing.Point(45, 82)
        Me.PO_Number.Name = "PO_Number"
        Me.PO_Number.Size = New System.Drawing.Size(220, 28)
        Me.PO_Number.TabIndex = 144
        Me.PO_Number.Text = "Purchase Order Number"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(6, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(142, 15)
        Me.Label9.TabIndex = 143
        Me.Label9.Text = "Purchase Order Number:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Font = New System.Drawing.Font("Copperplate Gothic Light", 27.75!)
        Me.Label7.ForeColor = System.Drawing.Color.IndianRed
        Me.Label7.Location = New System.Drawing.Point(8, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(564, 42)
        Me.Label7.TabIndex = 101
        Me.Label7.Text = "Inspection and Acceptance"
        '
        'dgvInspect_ITEMLIST
        '
        Me.dgvInspect_ITEMLIST.AllowUserToAddRows = False
        Me.dgvInspect_ITEMLIST.AllowUserToDeleteRows = False
        Me.dgvInspect_ITEMLIST.AllowUserToResizeColumns = False
        Me.dgvInspect_ITEMLIST.AllowUserToResizeRows = False
        Me.dgvInspect_ITEMLIST.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvInspect_ITEMLIST.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgvInspect_ITEMLIST.BackgroundColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInspect_ITEMLIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInspect_ITEMLIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInspect_ITEMLIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Item_ID, Me.Units, Me.Quantity, Me.Item_Description, Me.Unit_Cost, Me.Total_Cost})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(5)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AntiqueWhite
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInspect_ITEMLIST.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInspect_ITEMLIST.Location = New System.Drawing.Point(402, 203)
        Me.dgvInspect_ITEMLIST.Name = "dgvInspect_ITEMLIST"
        Me.dgvInspect_ITEMLIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInspect_ITEMLIST.Size = New System.Drawing.Size(898, 381)
        Me.dgvInspect_ITEMLIST.TabIndex = 83
        '
        'Item_ID
        '
        Me.Item_ID.FillWeight = 15.0!
        Me.Item_ID.HeaderText = "ID"
        Me.Item_ID.Name = "Item_ID"
        Me.Item_ID.Visible = False
        '
        'Units
        '
        Me.Units.FillWeight = 40.0!
        Me.Units.HeaderText = "Units"
        Me.Units.Name = "Units"
        '
        'Quantity
        '
        Me.Quantity.FillWeight = 20.0!
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        '
        'Item_Description
        '
        Me.Item_Description.HeaderText = "Items Description"
        Me.Item_Description.Name = "Item_Description"
        Me.Item_Description.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Unit_Cost
        '
        Me.Unit_Cost.FillWeight = 40.0!
        Me.Unit_Cost.HeaderText = "Unit Cost"
        Me.Unit_Cost.Name = "Unit_Cost"
        '
        'Total_Cost
        '
        Me.Total_Cost.FillWeight = 40.0!
        Me.Total_Cost.HeaderText = "Total Cost"
        Me.Total_Cost.Name = "Total_Cost"
        '
        'accept
        '
        Me.accept.AllowDrop = True
        Me.accept.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.accept.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.accept.FormattingEnabled = True
        Me.accept.Location = New System.Drawing.Point(96, 622)
        Me.accept.Name = "accept"
        Me.accept.Size = New System.Drawing.Size(287, 23)
        Me.accept.TabIndex = 65
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 625)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 15)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Accepted By:"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BackColor = System.Drawing.Color.White
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Red
        Me.txtInvoiceNo.Location = New System.Drawing.Point(43, 134)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(287, 23)
        Me.txtInvoiceNo.TabIndex = 63
        Me.txtInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 15)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Delivery No."
        '
        'combo_inspect2
        '
        Me.combo_inspect2.AllowDrop = True
        Me.combo_inspect2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.combo_inspect2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_inspect2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_inspect2.FormattingEnabled = True
        Me.combo_inspect2.Location = New System.Drawing.Point(96, 531)
        Me.combo_inspect2.Name = "combo_inspect2"
        Me.combo_inspect2.Size = New System.Drawing.Size(287, 23)
        Me.combo_inspect2.TabIndex = 59
        '
        'combo_inspect1
        '
        Me.combo_inspect1.AllowDrop = True
        Me.combo_inspect1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.combo_inspect1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.combo_inspect1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combo_inspect1.FormattingEnabled = True
        Me.combo_inspect1.Location = New System.Drawing.Point(96, 504)
        Me.combo_inspect1.Name = "combo_inspect1"
        Me.combo_inspect1.Size = New System.Drawing.Size(287, 23)
        Me.combo_inspect1.TabIndex = 58
        '
        'received1
        '
        Me.received1.AllowDrop = True
        Me.received1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.received1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.received1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.received1.FormattingEnabled = True
        Me.received1.Location = New System.Drawing.Point(97, 355)
        Me.received1.Name = "received1"
        Me.received1.Size = New System.Drawing.Size(287, 23)
        Me.received1.TabIndex = 57
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 503)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 15)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Inspected By:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 355)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 15)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "End User:"
        '
        'StatusStrip2
        '
        Me.StatusStrip2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel3, Me.Inspect_Status})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 681)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(1358, 22)
        Me.StatusStrip2.TabIndex = 163
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
        'Inspect_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1358, 703)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Inspect_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inspection"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.panelProgressbilling.ResumeLayout(False)
        Me.panelProgressbilling.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.dgvInspect_ITEMLIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents combo_inspect1 As System.Windows.Forms.ComboBox
    Friend WithEvents received1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents combo_inspect2 As System.Windows.Forms.ComboBox
    Friend WithEvents accept As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dgvInspect_ITEMLIST As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PO_Number As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTotalItems As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Inspect_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnPrintInspection As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblsave As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents combo_inspect3 As System.Windows.Forms.ComboBox
    Friend WithEvents received5 As System.Windows.Forms.ComboBox
    Friend WithEvents received4 As System.Windows.Forms.ComboBox
    Friend WithEvents received3 As System.Windows.Forms.ComboBox
    Friend WithEvents received2 As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintDisbursement As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblProgressBilling As System.Windows.Forms.Label
    Friend WithEvents panelProgressbilling As System.Windows.Forms.Panel
    Friend WithEvents combo_progressbill As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblDateTo As System.Windows.Forms.Label
    Friend WithEvents lblDateFrom As System.Windows.Forms.Label
    Friend WithEvents lblBilled As System.Windows.Forms.Label
    Friend WithEvents lblEdit As System.Windows.Forms.Label
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Item_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Item_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_Cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents date_bac_received As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As Guna.UI.WinForms.GunaTextBox
    Friend WithEvents combo_inspect4 As System.Windows.Forms.ComboBox
End Class
