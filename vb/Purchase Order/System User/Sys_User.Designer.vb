<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Sys_User
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Sys_User))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt_user = New System.Windows.Forms.TextBox
        Me.txt_password = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txt_confirm = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button_next = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.employee_details = New System.Windows.Forms.GroupBox
        Me.txt_BT = New System.Windows.Forms.ComboBox
        Me.txt_SS = New System.Windows.Forms.ComboBox
        Me.txt_sex = New System.Windows.Forms.ComboBox
        Me.txt_W = New System.Windows.Forms.TextBox
        Me.txt_PA = New System.Windows.Forms.TextBox
        Me.txt_H = New System.Windows.Forms.TextBox
        Me.birthdate = New System.Windows.Forms.DateTimePicker
        Me.txt_RA = New System.Windows.Forms.TextBox
        Me.txt_fname = New System.Windows.Forms.TextBox
        Me.txt_mname = New System.Windows.Forms.TextBox
        Me.txt_lname = New System.Windows.Forms.TextBox
        Me.txtPIID = New System.Windows.Forms.TextBox
        Me.txt_BD = New System.Windows.Forms.TextBox
        Me.piid = New System.Windows.Forms.Label
        Me.txt_BP = New System.Windows.Forms.TextBox
        Me.txtIDNumber = New System.Windows.Forms.TextBox
        Me.txt_email = New System.Windows.Forms.TextBox
        Me.IDnumber = New System.Windows.Forms.Label
        Me.txt_CN = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Remove = New System.Windows.Forms.LinkLabel
        Me.btnSave = New System.Windows.Forms.Button
        Me.Browse = New System.Windows.Forms.LinkLabel
        Me.id_num2 = New System.Windows.Forms.Label
        Me.myPic = New System.Windows.Forms.PictureBox
        Me.ID_num = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Button1 = New System.Windows.Forms.Button
        Me.role_id = New System.Windows.Forms.Label
        Me.Combo_role = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.not_match = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.user_state = New System.Windows.Forms.ToolStripStatusLabel
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.combo_dept = New System.Windows.Forms.ComboBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.employee_details.SuspendLayout()
        CType(Me.myPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(399, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 23)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "User Name :"
        '
        'txt_user
        '
        Me.txt_user.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_user.Location = New System.Drawing.Point(519, 96)
        Me.txt_user.Name = "txt_user"
        Me.txt_user.Size = New System.Drawing.Size(287, 31)
        Me.txt_user.TabIndex = 1
        Me.txt_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_password
        '
        Me.txt_password.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_password.Location = New System.Drawing.Point(519, 135)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txt_password.Size = New System.Drawing.Size(287, 31)
        Me.txt_password.TabIndex = 2
        Me.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(412, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 23)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Password :"
        '
        'txt_confirm
        '
        Me.txt_confirm.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_confirm.Location = New System.Drawing.Point(519, 174)
        Me.txt_confirm.Name = "txt_confirm"
        Me.txt_confirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txt_confirm.Size = New System.Drawing.Size(287, 31)
        Me.txt_confirm.TabIndex = 3
        Me.txt_confirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(346, 177)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 23)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Confirm Password :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(59, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 19)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "First Name :"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Brown
        Me.Button2.Location = New System.Drawing.Point(630, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(104, 43)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Browse"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(50, 218)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(97, 19)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "Cell Number :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(99, 190)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(46, 17)
        Me.Label16.TabIndex = 68
        Me.Label16.Text = "Email :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(3, 363)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(143, 19)
        Me.Label15.TabIndex = 66
        Me.Label15.Text = "Permanent Address :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(2, 282)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(146, 19)
        Me.Label14.TabIndex = 64
        Me.Label14.Text = "Residential Address :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(488, 157)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 19)
        Me.Label13.TabIndex = 62
        Me.Label13.Text = "Blood Type :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(513, 126)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 19)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Weight :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(516, 91)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 19)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "Height :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(484, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 19)
        Me.Label10.TabIndex = 56
        Me.Label10.Text = "Civil Status :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(534, 28)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 19)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Sex :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(62, 152)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 19)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Birth Place :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(65, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 19)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "Birth Date :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(60, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 19)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Last Name :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(43, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 19)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Middle Name :"
        '
        'Button_next
        '
        Me.Button_next.BackColor = System.Drawing.Color.SeaGreen
        Me.Button_next.Enabled = False
        Me.Button_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_next.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button_next.ForeColor = System.Drawing.Color.White
        Me.Button_next.Location = New System.Drawing.Point(758, 568)
        Me.Button_next.Name = "Button_next"
        Me.Button_next.Size = New System.Drawing.Size(134, 45)
        Me.Button_next.TabIndex = 2
        Me.Button_next.Text = "Next"
        Me.Button_next.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(5, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(906, 648)
        Me.TabControl1.TabIndex = 74
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.TabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage1.Controls.Add(Me.btnUpdate)
        Me.TabPage1.Controls.Add(Me.btnAdd)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Controls.Add(Me.employee_details)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.Remove)
        Me.TabPage1.Controls.Add(Me.btnSave)
        Me.TabPage1.Controls.Add(Me.Browse)
        Me.TabPage1.Controls.Add(Me.id_num2)
        Me.TabPage1.Controls.Add(Me.myPic)
        Me.TabPage1.Controls.Add(Me.ID_num)
        Me.TabPage1.Controls.Add(Me.Button_next)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(898, 622)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "User Profile"
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.White
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.ForeColor = System.Drawing.Color.Red
        Me.btnUpdate.Location = New System.Drawing.Point(178, 545)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(173, 68)
        Me.btnUpdate.TabIndex = 115
        Me.btnUpdate.Text = "Update " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Employee"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.White
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.Blue
        Me.btnAdd.Location = New System.Drawing.Point(6, 545)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(166, 68)
        Me.btnAdd.TabIndex = 114
        Me.btnAdd.Text = "Register " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "New Employees"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.Black
        Me.Button3.Location = New System.Drawing.Point(6, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(89, 37)
        Me.Button3.TabIndex = 110
        Me.Button3.Text = "Back"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'employee_details
        '
        Me.employee_details.Controls.Add(Me.Label4)
        Me.employee_details.Controls.Add(Me.Label8)
        Me.employee_details.Controls.Add(Me.Label7)
        Me.employee_details.Controls.Add(Me.txt_BT)
        Me.employee_details.Controls.Add(Me.Label6)
        Me.employee_details.Controls.Add(Me.txt_SS)
        Me.employee_details.Controls.Add(Me.Label16)
        Me.employee_details.Controls.Add(Me.txt_sex)
        Me.employee_details.Controls.Add(Me.Label5)
        Me.employee_details.Controls.Add(Me.Label17)
        Me.employee_details.Controls.Add(Me.txt_W)
        Me.employee_details.Controls.Add(Me.txt_PA)
        Me.employee_details.Controls.Add(Me.txt_H)
        Me.employee_details.Controls.Add(Me.birthdate)
        Me.employee_details.Controls.Add(Me.txt_RA)
        Me.employee_details.Controls.Add(Me.txt_fname)
        Me.employee_details.Controls.Add(Me.txt_mname)
        Me.employee_details.Controls.Add(Me.txt_lname)
        Me.employee_details.Controls.Add(Me.txtPIID)
        Me.employee_details.Controls.Add(Me.txt_BD)
        Me.employee_details.Controls.Add(Me.piid)
        Me.employee_details.Controls.Add(Me.Label13)
        Me.employee_details.Controls.Add(Me.txt_BP)
        Me.employee_details.Controls.Add(Me.Label9)
        Me.employee_details.Controls.Add(Me.Label12)
        Me.employee_details.Controls.Add(Me.txtIDNumber)
        Me.employee_details.Controls.Add(Me.Label10)
        Me.employee_details.Controls.Add(Me.txt_email)
        Me.employee_details.Controls.Add(Me.Label11)
        Me.employee_details.Controls.Add(Me.IDnumber)
        Me.employee_details.Controls.Add(Me.txt_CN)
        Me.employee_details.Controls.Add(Me.Label14)
        Me.employee_details.Controls.Add(Me.Label15)
        Me.employee_details.Location = New System.Drawing.Point(3, 77)
        Me.employee_details.Name = "employee_details"
        Me.employee_details.Size = New System.Drawing.Size(731, 442)
        Me.employee_details.TabIndex = 108
        Me.employee_details.TabStop = False
        Me.employee_details.Text = "Details"
        '
        'txt_BT
        '
        Me.txt_BT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txt_BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txt_BT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BT.FormattingEnabled = True
        Me.txt_BT.Items.AddRange(New Object() {"O+", "O-", "A+", "A-", "B+", "B-", "AB+", "AB-"})
        Me.txt_BT.Location = New System.Drawing.Point(583, 158)
        Me.txt_BT.Name = "txt_BT"
        Me.txt_BT.Size = New System.Drawing.Size(105, 23)
        Me.txt_BT.TabIndex = 105
        '
        'txt_SS
        '
        Me.txt_SS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txt_SS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txt_SS.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_SS.FormattingEnabled = True
        Me.txt_SS.Items.AddRange(New Object() {"Single", "Married", "Widowed", "Divorced", "Separated"})
        Me.txt_SS.Location = New System.Drawing.Point(583, 59)
        Me.txt_SS.Name = "txt_SS"
        Me.txt_SS.Size = New System.Drawing.Size(106, 23)
        Me.txt_SS.TabIndex = 104
        '
        'txt_sex
        '
        Me.txt_sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txt_sex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txt_sex.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_sex.FormattingEnabled = True
        Me.txt_sex.Items.AddRange(New Object() {"Male", "Female"})
        Me.txt_sex.Location = New System.Drawing.Point(583, 28)
        Me.txt_sex.Name = "txt_sex"
        Me.txt_sex.Size = New System.Drawing.Size(106, 23)
        Me.txt_sex.TabIndex = 103
        '
        'txt_W
        '
        Me.txt_W.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_W.ForeColor = System.Drawing.Color.Black
        Me.txt_W.Location = New System.Drawing.Point(583, 124)
        Me.txt_W.Name = "txt_W"
        Me.txt_W.Size = New System.Drawing.Size(106, 23)
        Me.txt_W.TabIndex = 94
        '
        'txt_PA
        '
        Me.txt_PA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PA.ForeColor = System.Drawing.Color.Black
        Me.txt_PA.Location = New System.Drawing.Point(154, 361)
        Me.txt_PA.Multiline = True
        Me.txt_PA.Name = "txt_PA"
        Me.txt_PA.Size = New System.Drawing.Size(326, 66)
        Me.txt_PA.TabIndex = 97
        '
        'txt_H
        '
        Me.txt_H.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_H.ForeColor = System.Drawing.Color.Black
        Me.txt_H.Location = New System.Drawing.Point(583, 90)
        Me.txt_H.Name = "txt_H"
        Me.txt_H.Size = New System.Drawing.Size(106, 23)
        Me.txt_H.TabIndex = 93
        '
        'birthdate
        '
        Me.birthdate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.birthdate.Location = New System.Drawing.Point(154, 124)
        Me.birthdate.Name = "birthdate"
        Me.birthdate.Size = New System.Drawing.Size(276, 23)
        Me.birthdate.TabIndex = 101
        '
        'txt_RA
        '
        Me.txt_RA.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_RA.ForeColor = System.Drawing.Color.Black
        Me.txt_RA.Location = New System.Drawing.Point(154, 280)
        Me.txt_RA.Multiline = True
        Me.txt_RA.Name = "txt_RA"
        Me.txt_RA.Size = New System.Drawing.Size(326, 71)
        Me.txt_RA.TabIndex = 96
        '
        'txt_fname
        '
        Me.txt_fname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_fname.ForeColor = System.Drawing.Color.Black
        Me.txt_fname.Location = New System.Drawing.Point(154, 30)
        Me.txt_fname.Name = "txt_fname"
        Me.txt_fname.Size = New System.Drawing.Size(276, 23)
        Me.txt_fname.TabIndex = 84
        '
        'txt_mname
        '
        Me.txt_mname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_mname.ForeColor = System.Drawing.Color.Black
        Me.txt_mname.Location = New System.Drawing.Point(154, 61)
        Me.txt_mname.Name = "txt_mname"
        Me.txt_mname.Size = New System.Drawing.Size(276, 23)
        Me.txt_mname.TabIndex = 85
        '
        'txt_lname
        '
        Me.txt_lname.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_lname.ForeColor = System.Drawing.Color.Black
        Me.txt_lname.Location = New System.Drawing.Point(154, 92)
        Me.txt_lname.Name = "txt_lname"
        Me.txt_lname.Size = New System.Drawing.Size(276, 23)
        Me.txt_lname.TabIndex = 86
        '
        'txtPIID
        '
        Me.txtPIID.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIID.ForeColor = System.Drawing.Color.Black
        Me.txtPIID.Location = New System.Drawing.Point(583, 372)
        Me.txtPIID.Name = "txtPIID"
        Me.txtPIID.Size = New System.Drawing.Size(134, 23)
        Me.txtPIID.TabIndex = 98
        '
        'txt_BD
        '
        Me.txt_BD.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txt_BD.ForeColor = System.Drawing.Color.Green
        Me.txt_BD.Location = New System.Drawing.Point(154, 124)
        Me.txt_BD.Name = "txt_BD"
        Me.txt_BD.Size = New System.Drawing.Size(276, 25)
        Me.txt_BD.TabIndex = 87
        '
        'piid
        '
        Me.piid.AutoSize = True
        Me.piid.BackColor = System.Drawing.Color.Transparent
        Me.piid.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.piid.ForeColor = System.Drawing.Color.Black
        Me.piid.Location = New System.Drawing.Point(534, 374)
        Me.piid.Name = "piid"
        Me.piid.Size = New System.Drawing.Size(39, 19)
        Me.piid.TabIndex = 82
        Me.piid.Text = "PIID:"
        '
        'txt_BP
        '
        Me.txt_BP.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BP.ForeColor = System.Drawing.Color.Black
        Me.txt_BP.Location = New System.Drawing.Point(154, 155)
        Me.txt_BP.Name = "txt_BP"
        Me.txt_BP.Size = New System.Drawing.Size(276, 23)
        Me.txt_BP.TabIndex = 88
        '
        'txtIDNumber
        '
        Me.txtIDNumber.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDNumber.ForeColor = System.Drawing.Color.Red
        Me.txtIDNumber.Location = New System.Drawing.Point(583, 403)
        Me.txtIDNumber.Name = "txtIDNumber"
        Me.txtIDNumber.Size = New System.Drawing.Size(134, 23)
        Me.txtIDNumber.TabIndex = 99
        '
        'txt_email
        '
        Me.txt_email.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_email.ForeColor = System.Drawing.Color.Black
        Me.txt_email.Location = New System.Drawing.Point(154, 186)
        Me.txt_email.Name = "txt_email"
        Me.txt_email.Size = New System.Drawing.Size(276, 23)
        Me.txt_email.TabIndex = 89
        '
        'IDnumber
        '
        Me.IDnumber.AutoSize = True
        Me.IDnumber.BackColor = System.Drawing.Color.Transparent
        Me.IDnumber.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IDnumber.ForeColor = System.Drawing.Color.Black
        Me.IDnumber.Location = New System.Drawing.Point(491, 405)
        Me.IDnumber.Name = "IDnumber"
        Me.IDnumber.Size = New System.Drawing.Size(82, 19)
        Me.IDnumber.TabIndex = 80
        Me.IDnumber.Text = "ID Number:"
        '
        'txt_CN
        '
        Me.txt_CN.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_CN.ForeColor = System.Drawing.Color.Black
        Me.txt_CN.Location = New System.Drawing.Point(154, 221)
        Me.txt_CN.Name = "txt_CN"
        Me.txt_CN.Size = New System.Drawing.Size(276, 23)
        Me.txt_CN.TabIndex = 90
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(6, 43)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(272, 33)
        Me.Label19.TabIndex = 107
        Me.Label19.Text = "Register New Employee"
        '
        'Remove
        '
        Me.Remove.AutoSize = True
        Me.Remove.Location = New System.Drawing.Point(842, 155)
        Me.Remove.Name = "Remove"
        Me.Remove.Size = New System.Drawing.Size(47, 13)
        Me.Remove.TabIndex = 102
        Me.Remove.TabStop = True
        Me.Remove.Text = "Remove"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(758, 517)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(134, 45)
        Me.btnSave.TabIndex = 100
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Browse
        '
        Me.Browse.AutoSize = True
        Me.Browse.Location = New System.Drawing.Point(740, 155)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(42, 13)
        Me.Browse.TabIndex = 77
        Me.Browse.TabStop = True
        Me.Browse.Text = "Browse"
        '
        'id_num2
        '
        Me.id_num2.AutoSize = True
        Me.id_num2.BackColor = System.Drawing.Color.Transparent
        Me.id_num2.ForeColor = System.Drawing.Color.Black
        Me.id_num2.Location = New System.Drawing.Point(790, 179)
        Me.id_num2.Name = "id_num2"
        Me.id_num2.Size = New System.Drawing.Size(47, 13)
        Me.id_num2.TabIndex = 76
        Me.id_num2.Text = "id_num2"
        Me.id_num2.Visible = False
        '
        'myPic
        '
        Me.myPic.BackColor = System.Drawing.Color.Transparent
        Me.myPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.myPic.Location = New System.Drawing.Point(740, 3)
        Me.myPic.Name = "myPic"
        Me.myPic.Size = New System.Drawing.Size(152, 146)
        Me.myPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.myPic.TabIndex = 75
        Me.myPic.TabStop = False
        '
        'ID_num
        '
        Me.ID_num.AutoSize = True
        Me.ID_num.BackColor = System.Drawing.Color.Transparent
        Me.ID_num.ForeColor = System.Drawing.Color.Black
        Me.ID_num.Location = New System.Drawing.Point(740, 179)
        Me.ID_num.Name = "ID_num"
        Me.ID_num.Size = New System.Drawing.Size(44, 13)
        Me.ID_num.TabIndex = 74
        Me.ID_num.Text = "ID_num"
        Me.ID_num.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.AntiqueWhite
        Me.TabPage2.Controls.Add(Me.Label20)
        Me.TabPage2.Controls.Add(Me.combo_dept)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.role_id)
        Me.TabPage2.Controls.Add(Me.Combo_role)
        Me.TabPage2.Controls.Add(Me.Label18)
        Me.TabPage2.Controls.Add(Me.not_match)
        Me.TabPage2.Controls.Add(Me.CheckBox1)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.txt_user)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.txt_password)
        Me.TabPage2.Controls.Add(Me.txt_confirm)
        Me.TabPage2.Controls.Add(Me.PictureBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(898, 622)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "User Account"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Brown
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(517, 348)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(287, 52)
        Me.Button1.TabIndex = 82
        Me.Button1.Text = "Done"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'role_id
        '
        Me.role_id.AutoSize = True
        Me.role_id.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.role_id.Location = New System.Drawing.Point(812, 213)
        Me.role_id.Name = "role_id"
        Me.role_id.Size = New System.Drawing.Size(63, 23)
        Me.role_id.TabIndex = 81
        Me.role_id.Text = "role_id"
        Me.role_id.Visible = False
        '
        'Combo_role
        '
        Me.Combo_role.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.Combo_role.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Combo_role.FormattingEnabled = True
        Me.Combo_role.Location = New System.Drawing.Point(517, 309)
        Me.Combo_role.Name = "Combo_role"
        Me.Combo_role.Size = New System.Drawing.Size(287, 31)
        Me.Combo_role.TabIndex = 5
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(389, 312)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(114, 23)
        Me.Label18.TabIndex = 79
        Me.Label18.Text = "Access Level :"
        '
        'not_match
        '
        Me.not_match.AutoSize = True
        Me.not_match.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.not_match.ForeColor = System.Drawing.Color.Red
        Me.not_match.Location = New System.Drawing.Point(639, 68)
        Me.not_match.Name = "not_match"
        Me.not_match.Size = New System.Drawing.Size(197, 23)
        Me.not_match.TabIndex = 78
        Me.not_match.Text = "Password did not Match"
        Me.not_match.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(654, 213)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(152, 27)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "Show Password"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(22, 16)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(309, 116)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 75
        Me.PictureBox2.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.user_state})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 656)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(915, 22)
        Me.StatusStrip1.TabIndex = 75
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(79, 17)
        Me.ToolStripStatusLabel1.Text = "Current State:"
        '
        'user_state
        '
        Me.user_state.Name = "user_state"
        Me.user_state.Size = New System.Drawing.Size(59, 17)
        Me.user_state.Text = "user_state"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'combo_dept
        '
        Me.combo_dept.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.combo_dept.Font = New System.Drawing.Font("Calibri", 14.25!)
        Me.combo_dept.FormattingEnabled = True
        Me.combo_dept.Location = New System.Drawing.Point(517, 263)
        Me.combo_dept.Name = "combo_dept"
        Me.combo_dept.Size = New System.Drawing.Size(289, 31)
        Me.combo_dept.TabIndex = 83
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(391, 266)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(112, 23)
        Me.Label20.TabIndex = 84
        Me.Label20.Text = "Department :"
        '
        'Sys_User
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(915, 678)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Sys_User"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sys_User"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.employee_details.ResumeLayout(False)
        Me.employee_details.PerformLayout()
        CType(Me.myPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_user As System.Windows.Forms.TextBox
    Friend WithEvents txt_password As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_confirm As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Button_next As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents myPic As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents not_match As System.Windows.Forms.Label
    Friend WithEvents Combo_role As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents role_id As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents user_state As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Browse As System.Windows.Forms.LinkLabel
    Friend WithEvents id_num2 As System.Windows.Forms.Label
    Friend WithEvents ID_num As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtIDNumber As System.Windows.Forms.TextBox
    Friend WithEvents IDnumber As System.Windows.Forms.Label
    Friend WithEvents txtPIID As System.Windows.Forms.TextBox
    Friend WithEvents piid As System.Windows.Forms.Label
    Friend WithEvents txt_fname As System.Windows.Forms.TextBox
    Friend WithEvents txt_mname As System.Windows.Forms.TextBox
    Friend WithEvents txt_email As System.Windows.Forms.TextBox
    Friend WithEvents txt_CN As System.Windows.Forms.TextBox
    Friend WithEvents txt_BP As System.Windows.Forms.TextBox
    Friend WithEvents txt_BD As System.Windows.Forms.TextBox
    Friend WithEvents txt_lname As System.Windows.Forms.TextBox
    Friend WithEvents txt_W As System.Windows.Forms.TextBox
    Friend WithEvents txt_H As System.Windows.Forms.TextBox
    Friend WithEvents txt_PA As System.Windows.Forms.TextBox
    Friend WithEvents txt_RA As System.Windows.Forms.TextBox
    Friend WithEvents birthdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Remove As System.Windows.Forms.LinkLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txt_sex As System.Windows.Forms.ComboBox
    Friend WithEvents txt_SS As System.Windows.Forms.ComboBox
    Friend WithEvents txt_BT As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents employee_details As System.Windows.Forms.GroupBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents combo_dept As System.Windows.Forms.ComboBox
End Class
