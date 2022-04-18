<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lbFacilities = New System.Windows.Forms.ListBox()
        Me.btnEditUser = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lvUsers = New System.Windows.Forms.ListView()
        Me.FirstName = New System.Windows.Forms.ColumnHeader()
        Me.LastName = New System.Windows.Forms.ColumnHeader()
        Me.Login = New System.Windows.Forms.ColumnHeader()
        Me.Role = New System.Windows.Forms.ColumnHeader()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbFacilities
        '
        Me.lbFacilities.BackColor = System.Drawing.Color.White
        Me.lbFacilities.FormattingEnabled = True
        Me.lbFacilities.ItemHeight = 15
        Me.lbFacilities.Location = New System.Drawing.Point(523, 95)
        Me.lbFacilities.Name = "lbFacilities"
        Me.lbFacilities.Size = New System.Drawing.Size(284, 469)
        Me.lbFacilities.TabIndex = 2
        '
        'btnEditUser
        '
        Me.btnEditUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnEditUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.btnEditUser.Location = New System.Drawing.Point(732, 17)
        Me.btnEditUser.Name = "btnEditUser"
        Me.btnEditUser.Size = New System.Drawing.Size(75, 23)
        Me.btnEditUser.TabIndex = 4
        Me.btnEditUser.Text = "Edit"
        Me.btnEditUser.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.btnAdd.Location = New System.Drawing.Point(651, 17)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(31, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Saturn Users"
        '
        'lvUsers
        '
        Me.lvUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.lvUsers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FirstName, Me.LastName, Me.Login, Me.Role})
        Me.lvUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.lvUsers.FullRowSelect = True
        Me.lvUsers.HideSelection = False
        Me.lvUsers.Location = New System.Drawing.Point(28, 95)
        Me.lvUsers.MultiSelect = False
        Me.lvUsers.Name = "lvUsers"
        Me.lvUsers.Size = New System.Drawing.Size(475, 469)
        Me.lvUsers.TabIndex = 7
        Me.lvUsers.UseCompatibleStateImageBehavior = False
        '
        'FirstName
        '
        Me.FirstName.Width = 110
        '
        'LastName
        '
        Me.LastName.Width = 110
        '
        'Login
        '
        Me.Login.Width = 130
        '
        'Role
        '
        Me.Role.Width = 100
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(524, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 23)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Facilities"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(840, 584)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lvUsers)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnEditUser)
        Me.Controls.Add(Me.lbFacilities)
        Me.Name = "FormMain"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbFacilities As ListBox
    Friend WithEvents btnEditUser As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lvUsers As ListView
    Friend WithEvents FirstName As ColumnHeader
    Friend WithEvents LastName As ColumnHeader
    Friend WithEvents Login As ColumnHeader
    Friend WithEvents Role As ColumnHeader
    Friend WithEvents Label2 As Label
End Class
