<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inventory_Reports
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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.rad_ics = New System.Windows.Forms.RadioButton
        Me.rad_are = New System.Windows.Forms.RadioButton
        Me.btnExport = New System.Windows.Forms.Button
        Me.dgvView = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.UltraPrintPreviewDialog1 = New Infragistics.Win.Printing.UltraPrintPreviewDialog(Me.components)
        Me.UltraPrintDocument1 = New Infragistics.Win.Printing.UltraPrintDocument(Me.components)
        Me.UltraGridExcelExporter1 = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.btnGenerate = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCategory = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.radViewAll = New System.Windows.Forms.RadioButton
        Me.redHighValue = New System.Windows.Forms.RadioButton
        Me.radLowValue = New System.Windows.Forms.RadioButton
        Me.groupICS = New System.Windows.Forms.GroupBox
        CType(Me.dgvView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.groupICS.SuspendLayout()
        Me.SuspendLayout()
        '
        'rad_ics
        '
        Me.rad_ics.AutoSize = True
        Me.rad_ics.BackColor = System.Drawing.Color.Transparent
        Me.rad_ics.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rad_ics.ForeColor = System.Drawing.Color.Tomato
        Me.rad_ics.Location = New System.Drawing.Point(22, 17)
        Me.rad_ics.Name = "rad_ics"
        Me.rad_ics.Size = New System.Drawing.Size(261, 27)
        Me.rad_ics.TabIndex = 1
        Me.rad_ics.TabStop = True
        Me.rad_ics.Text = "(ICS) Inventory Custodian Slip "
        Me.rad_ics.UseVisualStyleBackColor = False
        '
        'rad_are
        '
        Me.rad_are.AutoSize = True
        Me.rad_are.BackColor = System.Drawing.Color.Transparent
        Me.rad_are.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rad_are.ForeColor = System.Drawing.Color.DodgerBlue
        Me.rad_are.Location = New System.Drawing.Point(22, 53)
        Me.rad_are.Name = "rad_are"
        Me.rad_are.Size = New System.Drawing.Size(349, 27)
        Me.rad_are.TabIndex = 2
        Me.rad_are.TabStop = True
        Me.rad_are.Text = "(PAR) Property Acknowledgement Receipt"
        Me.rad_are.UseVisualStyleBackColor = False
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.White
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ForeColor = System.Drawing.Color.Green
        Me.btnExport.Location = New System.Drawing.Point(1136, 12)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(118, 34)
        Me.btnExport.TabIndex = 3
        Me.btnExport.Text = "Export thru Excel"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'dgvView
        '
        Me.dgvView.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance2.BackColor = System.Drawing.Color.WhiteSmoke
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackHatchStyle = Infragistics.Win.BackHatchStyle.LightHorizontal
        Me.dgvView.DisplayLayout.Appearance = Appearance2
        Me.dgvView.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Me.dgvView.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.PrefixWithEmptyCell
        Me.dgvView.DisplayLayout.InterBandSpacing = 10
        Me.dgvView.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free
        Me.dgvView.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.dgvView.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.dgvView.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.dgvView.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Etched
        Me.dgvView.DisplayLayout.Override.BorderStyleRowSelector = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.dgvView.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Appearance4.BackColor = System.Drawing.Color.AntiqueWhite
        Appearance4.BackColor2 = System.Drawing.Color.AntiqueWhite
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.GlassBottom37
        Appearance4.BackHatchStyle = Infragistics.Win.BackHatchStyle.None
        Me.dgvView.DisplayLayout.Override.CellAppearance = Appearance4
        Me.dgvView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.dgvView.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn
        Me.dgvView.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange
        Me.dgvView.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo
        Me.dgvView.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains
        Me.dgvView.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Appearance5.BackColor = System.Drawing.Color.BurlyWood
        Appearance5.BackColor2 = System.Drawing.Color.BurlyWood
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal
        Appearance5.FontData.SizeInPoints = 10.0!
        Appearance5.ForeColor = System.Drawing.Color.Black
        Appearance5.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.dgvView.DisplayLayout.Override.HeaderAppearance = Appearance5
        Me.dgvView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance6.BackColor = System.Drawing.Color.Transparent
        Appearance6.ForeColor = System.Drawing.Color.DarkGreen
        Me.dgvView.DisplayLayout.Override.RowSelectorAppearance = Appearance6
        Me.dgvView.DisplayLayout.Override.RowSelectorWidth = 10
        Me.dgvView.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Free
        Me.dgvView.DisplayLayout.Override.RowSizingArea = Infragistics.Win.UltraWinGrid.RowSizingArea.RowBordersOnly
        Me.dgvView.DisplayLayout.Override.RowSpacingAfter = 3
        Me.dgvView.DisplayLayout.Override.RowSpacingBefore = 3
        Appearance7.BackColor = System.Drawing.SystemColors.ActiveCaption
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Appearance7.BackHatchStyle = Infragistics.Win.BackHatchStyle.None
        Appearance7.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.dgvView.DisplayLayout.Override.SelectedRowAppearance = Appearance7
        Me.dgvView.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.dgvView.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None
        Me.dgvView.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.[True]
        Me.dgvView.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvView.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.dgvView.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.dgvView.DisplayLayout.UseFixedHeaders = True
        Me.dgvView.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.dgvView.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvView.Location = New System.Drawing.Point(12, 49)
        Me.dgvView.Name = "dgvView"
        Me.dgvView.Size = New System.Drawing.Size(1243, 414)
        Me.dgvView.TabIndex = 4
        '
        'UltraPrintPreviewDialog1
        '
        Me.UltraPrintPreviewDialog1.Document = Me.UltraPrintDocument1
        Me.UltraPrintPreviewDialog1.Name = "UltraPrintPreviewDialog1"
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.btnGenerate.ForeColor = System.Drawing.Color.Blue
        Me.btnGenerate.Location = New System.Drawing.Point(399, 34)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(96, 33)
        Me.btnGenerate.TabIndex = 5
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.Green
        Me.btnClear.Location = New System.Drawing.Point(297, 34)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(96, 33)
        Me.btnClear.TabIndex = 75
        Me.btnClear.Text = "Clear (View All)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(33, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 15)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "TO"
        '
        'dateto
        '
        Me.dateto.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateto.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.dateto.Location = New System.Drawing.Point(61, 57)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(207, 23)
        Me.dateto.TabIndex = 70
        '
        'datefrom
        '
        Me.datefrom.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datefrom.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.datefrom.Location = New System.Drawing.Point(61, 28)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(208, 23)
        Me.datefrom.TabIndex = 69
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(16, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 15)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "FROM"
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblCategory.Location = New System.Drawing.Point(1981, 160)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(99, 25)
        Me.lblCategory.TabIndex = 76
        Me.lblCategory.Text = "Category"
        Me.lblCategory.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Copperplate Gothic Light", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label4.Location = New System.Drawing.Point(886, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(392, 40)
        Me.Label4.TabIndex = 75
        Me.Label4.Text = "Inventory Reports"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox3.Controls.Add(Me.Label34)
        Me.GroupBox3.Controls.Add(Me.dgvView)
        Me.GroupBox3.Controls.Add(Me.lblStatus)
        Me.GroupBox3.Controls.Add(Me.btnExport)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 210)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1272, 495)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "List of Items"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(1075, 465)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(48, 16)
        Me.Label34.TabIndex = 9
        Me.Label34.Text = "Status:"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(1124, 466)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(84, 16)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "LOADING . . ."
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.btnClear)
        Me.GroupBox4.Controls.Add(Me.dateto)
        Me.GroupBox4.Controls.Add(Me.datefrom)
        Me.GroupBox4.Controls.Add(Me.btnGenerate)
        Me.GroupBox4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(24, 109)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(669, 95)
        Me.GroupBox4.TabIndex = 111
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Advance Search"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox2.Controls.Add(Me.rad_ics)
        Me.GroupBox2.Controls.Add(Me.rad_are)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(24, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(669, 91)
        Me.GroupBox2.TabIndex = 110
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SearchBox"
        '
        'radViewAll
        '
        Me.radViewAll.AutoSize = True
        Me.radViewAll.Location = New System.Drawing.Point(6, 17)
        Me.radViewAll.Name = "radViewAll"
        Me.radViewAll.Size = New System.Drawing.Size(62, 17)
        Me.radViewAll.TabIndex = 3
        Me.radViewAll.TabStop = True
        Me.radViewAll.Text = "View All"
        Me.radViewAll.UseVisualStyleBackColor = True
        '
        'redHighValue
        '
        Me.redHighValue.AutoSize = True
        Me.redHighValue.ForeColor = System.Drawing.Color.Blue
        Me.redHighValue.Location = New System.Drawing.Point(6, 41)
        Me.redHighValue.Name = "redHighValue"
        Me.redHighValue.Size = New System.Drawing.Size(77, 17)
        Me.redHighValue.TabIndex = 4
        Me.redHighValue.TabStop = True
        Me.redHighValue.Text = "High Value"
        Me.redHighValue.UseVisualStyleBackColor = True
        '
        'radLowValue
        '
        Me.radLowValue.AutoSize = True
        Me.radLowValue.ForeColor = System.Drawing.Color.Red
        Me.radLowValue.Location = New System.Drawing.Point(6, 63)
        Me.radLowValue.Name = "radLowValue"
        Me.radLowValue.Size = New System.Drawing.Size(75, 17)
        Me.radLowValue.TabIndex = 5
        Me.radLowValue.TabStop = True
        Me.radLowValue.Text = "Low Value"
        Me.radLowValue.UseVisualStyleBackColor = True
        '
        'groupICS
        '
        Me.groupICS.Controls.Add(Me.radViewAll)
        Me.groupICS.Controls.Add(Me.radLowValue)
        Me.groupICS.Controls.Add(Me.redHighValue)
        Me.groupICS.Location = New System.Drawing.Point(423, 12)
        Me.groupICS.Name = "groupICS"
        Me.groupICS.Size = New System.Drawing.Size(270, 91)
        Me.groupICS.TabIndex = 112
        Me.groupICS.TabStop = False
        '
        'Inventory_Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1306, 712)
        Me.Controls.Add(Me.groupICS)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.Label4)
        Me.Name = "Inventory_Reports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory_Reports"
        CType(Me.dgvView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.groupICS.ResumeLayout(False)
        Me.groupICS.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rad_ics As System.Windows.Forms.RadioButton
    Friend WithEvents rad_are As System.Windows.Forms.RadioButton
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvView As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPrintPreviewDialog1 As Infragistics.Win.Printing.UltraPrintPreviewDialog
    Friend WithEvents UltraPrintDocument1 As Infragistics.Win.Printing.UltraPrintDocument
    Friend WithEvents UltraGridExcelExporter1 As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents radViewAll As System.Windows.Forms.RadioButton
    Friend WithEvents redHighValue As System.Windows.Forms.RadioButton
    Friend WithEvents radLowValue As System.Windows.Forms.RadioButton
    Friend WithEvents groupICS As System.Windows.Forms.GroupBox
End Class
