<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
        Me.lbFacilities = New System.Windows.Forms.ListBox()
        Me.btnEditUser = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lvUsers = New System.Windows.Forms.ListView()
        Me.FirstName = New System.Windows.Forms.ColumnHeader()
        Me.LastName = New System.Windows.Forms.ColumnHeader()
        Me.Login = New System.Windows.Forms.ColumnHeader()
        Me.Role = New System.Windows.Forms.ColumnHeader()
        Me.JobTitle = New System.Windows.Forms.ColumnHeader()
        Me.SuspendLayout()
        '
        'lbFacilities
        '
        Me.lbFacilities.BackColor = System.Drawing.Color.White
        Me.lbFacilities.FormattingEnabled = True
        Me.lbFacilities.ItemHeight = 15
        Me.lbFacilities.Location = New System.Drawing.Point(801, 104)
        Me.lbFacilities.Name = "lbFacilities"
        Me.lbFacilities.Size = New System.Drawing.Size(284, 409)
        Me.lbFacilities.TabIndex = 2
        '
        'btnEditUser
        '
        Me.btnEditUser.Location = New System.Drawing.Point(609, 36)
        Me.btnEditUser.Name = "btnEditUser"
        Me.btnEditUser.Size = New System.Drawing.Size(75, 23)
        Me.btnEditUser.TabIndex = 4
        Me.btnEditUser.Text = "Edit"
        Me.btnEditUser.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(528, 36)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(28, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 32)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Saturn Users"
        '
        'lvUsers
        '
        Me.lvUsers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FirstName, Me.LastName, Me.Login, Me.Role, Me.JobTitle})
        Me.lvUsers.HideSelection = False
        Me.lvUsers.Location = New System.Drawing.Point(28, 104)
        Me.lvUsers.MultiSelect = False
        Me.lvUsers.Name = "lvUsers"
        Me.lvUsers.Size = New System.Drawing.Size(767, 409)
        Me.lvUsers.TabIndex = 7
        Me.lvUsers.UseCompatibleStateImageBehavior = False
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1135, 584)
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
    Friend WithEvents JobTitle As ColumnHeader
End Class
