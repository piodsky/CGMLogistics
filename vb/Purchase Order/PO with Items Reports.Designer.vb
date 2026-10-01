<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PO_Items
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.btnGenerate = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.dgvView = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.lblStatus = New System.Windows.Forms.Label
        Me.btnExport = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.UltraPrintDocument1 = New Infragistics.Win.Printing.UltraPrintDocument(Me.components)
        Me.UltraPrintPreviewDialog1 = New Infragistics.Win.Printing.UltraPrintPreviewDialog(Me.components)
        Me.UltraGridExcelExporter1 = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Copperplate Gothic Light", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.IndianRed
        Me.Label2.Location = New System.Drawing.Point(937, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(321, 37)
        Me.Label2.TabIndex = 124
        Me.Label2.Text = "PO Items Reports"
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
        Me.GroupBox4.Location = New System.Drawing.Point(10, 85)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(669, 95)
        Me.GroupBox4.TabIndex = 123
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Advance Search"
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
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(424, 34)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(96, 33)
        Me.btnClear.TabIndex = 75
        Me.btnClear.Text = "Clear (View All)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'dateto
        '
        Me.dateto.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateto.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.dateto.Location = New System.Drawing.Point(61, 57)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(223, 23)
        Me.dateto.TabIndex = 70
        '
        'datefrom
        '
        Me.datefrom.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datefrom.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.datefrom.Location = New System.Drawing.Point(61, 28)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(223, 23)
        Me.datefrom.TabIndex = 69
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.White
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.btnGenerate.ForeColor = System.Drawing.Color.Blue
        Me.btnGenerate.Location = New System.Drawing.Point(322, 34)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(96, 33)
        Me.btnGenerate.TabIndex = 5
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.AntiqueWhite
        Me.GroupBox3.Controls.Add(Me.Label34)
        Me.GroupBox3.Controls.Add(Me.dgvView)
        Me.GroupBox3.Controls.Add(Me.lblStatus)
        Me.GroupBox3.Location = New System.Drawing.Point(10, 186)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1254, 495)
        Me.GroupBox3.TabIndex = 122
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
        'dgvView
        '
        Me.dgvView.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance2.BackColor = System.Drawing.Color.WhiteSmoke
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackHatchStyle = Infragistics.Win.BackHatchStyle.LightHorizontal
        Me.dgvView.DisplayLayout.Appearance = Appearance2
        Me.dgvView.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
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
        Me.dgvView.Location = New System.Drawing.Point(6, 19)
        Me.dgvView.Name = "dgvView"
        Me.dgvView.Size = New System.Drawing.Size(1243, 444)
        Me.dgvView.TabIndex = 4
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
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.White
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ForeColor = System.Drawing.Color.Green
        Me.btnExport.Location = New System.Drawing.Point(1140, 148)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(118, 34)
        Me.btnExport.TabIndex = 121
        Me.btnExport.Text = "Export thru Excel"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'UltraPrintPreviewDialog1
        '
        Me.UltraPrintPreviewDialog1.Document = Me.UltraPrintDocument1
        Me.UltraPrintPreviewDialog1.Name = "UltraPrintPreviewDialog1"
        '
        'PO_Items
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1274, 688)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnExport)
        Me.Name = "PO_Items"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PO Items"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgvView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents dgvView As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents UltraPrintDocument1 As Infragistics.Win.Printing.UltraPrintDocument
    Friend WithEvents UltraPrintPreviewDialog1 As Infragistics.Win.Printing.UltraPrintPreviewDialog
    Friend WithEvents UltraGridExcelExporter1 As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
End Class
