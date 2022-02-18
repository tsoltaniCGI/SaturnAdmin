Option Strict Off
Imports System.Data.SqlClient

Public Class FormMain

    Inherits System.Windows.Forms.Form
    Dim oCollUsers As New Collection

    Dim oConn As New SqlConnection

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim sTestProd As String
        sTestProd = "P"
        If sTestProd = "P" Then
            oConn = New System.Data.SqlClient.SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Else
            oConn = New System.Data.SqlClient.SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")
        End If

        Dim mycmd = oConn.CreateCommand
        Dim oReader As SqlDataReader
        Dim sSql As String
        Dim iUserId As Integer
        Dim iNum As Integer
        Dim oCollUserFacility As New Collection
        Dim iMax As Integer
        Dim iCnt As Integer
        Dim iFacilityId As Integer




        sSql = "SELECT users.user_id, ISNULL(user_first_name, '') AS 'First Name', ISNULL(user_last_name, '') AS 'Last Name', "
        sSql = sSql & "ISNULL(user_login, '') AS 'Login', ISNULL(user_role_description, '') AS 'Role', "
        sSql = sSql & "ISNULL(user_job_title, ' ') As 'Job Title', ISNULL(dummy_vendor_id, '') AS 'Dummy Vendor ID', users_facilities.facility_id, ISNULL(facilities.facility_name, '') AS 'Facility' "
        sSql = sSql & "From users, users_facilities, facilities, users_roles "
        sSql = sSql & "WHERE users.user_id = users_facilities.user_id "
        sSql = sSql & "AND users_facilities.facility_id = facilities.facility_id "
        sSql = sSql & "AND users_roles.user_role = users.user_role "
        sSql = sSql & "ORDER BY user_id, facility_id"

        mycmd.CommandText = sSql

        oConn.Open()

        oReader = mycmd.ExecuteReader()





        If oReader.HasRows Then
            iNum = 1
            Do While oReader.Read()
                Dim oUSRFAC As New UserFacility
                oUSRFAC.UserID = oReader.GetInt32(0)
                oUSRFAC.UserFirstName = oReader.GetString(1)
                oUSRFAC.UserLastName = oReader.GetString(2)
                oUSRFAC.UserLogin = oReader.GetString(3)
                oUSRFAC.UserRole = oReader.GetString(4)
                oUSRFAC.UserJobTitle = oReader.GetString(5)
                oUSRFAC.DummyVendorId = oReader.GetInt32(6)
                oUSRFAC.FacilityId = oReader.GetInt32(7)
                oUSRFAC.FacilityName = oReader.GetString(8)

                'oCollUserFacility.Add(oUSRFAC, oUSRFAC.UserID.ToString() & oUSRFAC.FacilityId.ToString() & iNum.ToString())
                oCollUserFacility.Add(oUSRFAC)
                iNum = iNum + 1
            Loop
        End If

        oReader.Close()
        iMax = oCollUserFacility.Count
        iCnt = 1

        iUserId = -1

        Do While iCnt <= iMax
            If oCollUserFacility(iCnt).UserId <> iUserId Then
                iUserId = oCollUserFacility(iCnt).UserId
                'lbUsers.Items.Add(oCollUserFacility(iCnt).UserFristName)

                Dim oUser As New User
                oUser.UserFirstName = oCollUserFacility(iCnt).UserFirstName
                oUser.UserLastName = oCollUserFacility(iCnt).UserLastName
                oUser.UserLogin = oCollUserFacility(iCnt).UserLogin
                oUser.UserRole = oCollUserFacility(iCnt).UserRole
                oUser.UserJobTitle = oCollUserFacility(iCnt).UserJobTitle
                oUser.DummyVendorId = oCollUserFacility(iCnt).DummyVendorId


                'iFacilityId = -1
                Do While iUserId = oCollUserFacility(iCnt).UserId
                    'If iFacilityId <> oCollUserFacility(iCnt).FacilityId Then
                    '    iFacilityId = oCollUserFacility(iCnt).FacilityId

                    'Dim oFacility As New Facility()
                    '    oFacility.FacilityName = oCollUserFacility(iCnt).FacilityName
                    '    oFacility.FacilityId = iFacilityId
                    '    oUser.FacilityName.Add(oFacility)
                    'End If
                    oUser.FacilityNames.Add(oCollUserFacility(iCnt).FacilityName)
                    oUser.FacilityIds.Add(oCollUserFacility(iCnt).FacilityId)
                    iCnt = iCnt + 1

                    If iCnt >= iMax Then
                        Exit Do
                    End If
                Loop
                oCollUsers.Add(oUser)

            End If
        Loop

        lbFacilities.Sorted = True

        lvUsers.View = View.Details
        lvUsers.Items.Clear()

        'lbUsers.Sorted = True
        'lbUsers.ValueMember = "CollectionIndex"
        'lbUsers.DisplayMember = "UserFirstName, UserLastName"

        For Each oCurUser In oCollUsers
            Dim oLVU As New ListViewItem
            oLVU.SubItems(0).Text = oCurUser.UserFirstName
            oLVU.SubItems.Add(oCurUser.UserLastName)
            oLVU.SubItems.Add(oCurUser.UserLogin)
            oLVU.SubItems.Add(oCurUser.UserRole)
            oLVU.SubItems.Add(oCurUser.UserJobTitle)
            lvUsers.Items.Add(oLVU)
        Next

        lvUsers.Columns(0).Text = "First Name"
        lvUsers.Columns(1).Text = "Last Name"
        lvUsers.Columns(2).Text = "Login"
        lvUsers.Columns(3).Text = "Role"
        lvUsers.Columns(3).Text = "Job Title"

    End Sub




    'Private Sub dgvUsers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellContentClick

    'End Sub

    Private Sub lvUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvUsers.SelectedIndexChanged
        Dim iCnt As Integer
        Dim iMax As Integer
        'Dim oSelItem As IndexedUserListItem = lvUsers.SelectedItems

        If lvUsers.SelectedIndices.Count >= 1 Then
            iMax = oCollUsers(lvUsers.SelectedIndices(0) + 1).FacilityNames.Count
            'iMax = oCollUsers(lvUsers.SelectedItems(0).Index + 1).FacilityNames.Count
            'iMax = oCollUsers(lvUsers.FocusedItem.Index + 1).FacilityNames.Count

            iCnt = 1
            lbFacilities.Items.Clear()
            Do While iCnt <= iMax
                lbFacilities.Items.Add(oCollUsers(lvUsers.SelectedIndices(0) + 1).FacilityNames(iCnt))
                iCnt = iCnt + 1
            Loop
        End If

    End Sub
    Private Sub RebuildPage()
        Form1_Load(Me, EventArgs.Empty)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim iIndex As Integer
        Dim sSql As String
        Dim sCurName As String


        Me.TopMost = False
        Dim frmAddUser = New FormAddUser
        Me.TopMost = False
        GlobalVariables.ResetUser = False
        frmAddUser.ShowDialog()

        Me.TopMost = True

        lvUsers.Items.Clear()

        If GlobalVariables.ResetUser Then
            '    Dim sTestProd As String
            '    sTestProd = GlobalVariables.sEnv
            '    If sTestProd = "P" Then
            '        oConn = New SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
            '    Else
            '        oConn = New SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")
            '    End If

            '    Dim myCmd = oConn.CreateCommand
            '    sSql = "INSERT INTO users"
            '    sSql = sSql & "(user_first_name, user_last_name, user_login, user_role_description) "
            '    sSql = sSql & "VALUES ('" & GlobalVariables.CurrentFirstName & "', " & GlobalVariables.CurrentLastName & "', "
            '    sSql = sSql & "'" & GlobalVariables.CurrentLogin & "', " & GlobalVariables.CurrentRoleDescription & " ') "
            '    'sSql = sSql & iUserId.ToString() & " ') "


            '    myCmd.CommandText = sSql
            '    oConn.Open()





            RebuildPage()
            'sCurName = oCollUsers(GlobalVariables.iAddedUserID.ToString()).UserFisrtName
            'sCurName = sCurName & " "
            'sCurName = sCurName & oCollUsers(GlobalVariables.iAddedUserID.ToString()).UserLastName

            'iIndex = lvUsers.FindItemWithText("sCurName")
            'If iIndex > -1 Then
            '    lvUsers.SelectedIndices = iIndex
            'Else
            '    lvUsers.FocusedItem.Index = 0
            'End If
        End If




    End Sub

    'Private Sub RebuildPage()
    '    Throw New NotImplementedException()
    'End Sub





    'Private Sub pbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    '    If myDataGridView.SelectedRows.Count > 0 Then
    '        'you may want to add a confirmation message, and if the user confirms delete
    '        myDataGridView.Rows.Remove(myDataGridView.SelectedRows(0))
    '    Else
    '        MessageBox.Show("Select 1 row before you hit Delete")
    '    End If

    'End Sub

    '    If dgv(11, dgv.CurrentRow.Index).Selected = True Then
    '    dgv.Rows.RemoveAt(dgv.CurrentRow.Index)
    'Else
    '    Exit Sub
    '    End If
End Class
