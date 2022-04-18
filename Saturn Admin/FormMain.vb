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



        sSql = "Select USERS.USER_ID, coalesce(USER_FIRST_NAME, '') [First Name], coalesce(USER_LAST_NAME, '') [Last Name], "
        sSql = sSql & "coalesce(USER_LOGIN, '') [Login], coalesce(USER_ROLE_DESCRIPTION, '') [Role], coalesce(USER_JOB_TITLE, ' ') [Job Title], "
        sSql = sSql & "coalesce(DUMMY_VENDOR_ID, '') [Dummy Vendor ID], USERS_FACILITIES.FACILITY_ID, coalesce(FACILITIES.FACILITY_NAME, '') [Facility] "
        sSql = sSql & "From USERS Left outer join USERS_ROLES On USERS_ROLES.USER_ROLE = USERS.USER_ROLE "
        sSql = sSql & "Join USERS_FACILITIES On USERS.USER_ID = USERS_FACILITIES.USER_ID "
        sSql = sSql & "Join FACILITIES On USERS_FACILITIES.FACILITY_ID = FACILITIES.FACILITY_ID "
        sSql = sSql & "order by USER_FIRST_NAME, FACILITY_ID "

        mycmd.CommandText = sSql
        oConn.Open()
        oReader = mycmd.ExecuteReader()

        oCollUserFacility.Clear()

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
                oUser.UserID = oCollUserFacility(iCnt).UserID
                oUser.UserFirstName = oCollUserFacility(iCnt).UserFirstName
                oUser.UserLastName = oCollUserFacility(iCnt).UserLastName
                oUser.UserLogin = oCollUserFacility(iCnt).UserLogin
                oUser.UserRole = oCollUserFacility(iCnt).UserRole
                oUser.UserJobTitle = oCollUserFacility(iCnt).UserJobTitle
                oUser.DummyVendorId = oCollUserFacility(iCnt).DummyVendorId


                'iFacilityId = -1
                Do While iUserId = oCollUserFacility(iCnt).UserId
                    oUser.FacilityNames.Add(oCollUserFacility(iCnt).FacilityName)
                    oUser.FacilityIds.Add(oCollUserFacility(iCnt).FacilityId)
                    iCnt = iCnt + 1

                    If iCnt >= iMax Then
                        Exit Do
                    End If
                Loop
                oCollUsers.Add(oUser)
                If iCnt >= iMax Then
                    Exit Do
                End If
            End If
        Loop

        'GlobalVariables.CurrentUser = oCollUsers(lvUsers.SelectedIndices(0) + 1)

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


    End Sub


    Private Sub lvUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvUsers.SelectedIndexChanged
        Dim iCnt As Integer
        Dim iMax As Integer
        'Dim oSelItem As IndexedUserListItem = lvUsers.SelectedItems

        If lvUsers.SelectedIndices.Count >= 1 Then
            iMax = oCollUsers(lvUsers.SelectedIndices(0) + 1).FacilityNames.Count
            GlobalVariables.CurrentUser = oCollUsers(lvUsers.SelectedIndices(0) + 1)
            GlobalVariables.CurrentDummyVendorID = GlobalVariables.CurrentUser.DummyVendorId
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
        Dim oNewUser As New User



        Me.TopMost = False
        Dim frmAddUser = New FormAddUser
        Me.TopMost = False
        GlobalVariables.ResetUser = False
        frmAddUser.ShowDialog()

        'Me.TopMost = True

        lvUsers.Items.Clear()

        If GlobalVariables.ResetUser Then

            RebuildPage()
            lvUsers.Sort()

        End If




    End Sub

    Private Sub btnEditUser_Click(sender As Object, e As EventArgs) Handles btnEditUser.Click
        Dim iCurIndex As Integer
        Dim iCnt As Integer
        Dim iMax As Integer
        If lvUsers.SelectedItems.Count < 1 Then
            MessageBox.Show("No User is Selected")
        Else

            Me.TopMost = False
            Dim frmEditUser = New FormEditUser
            Me.TopMost = False
            GlobalVariables.ResetUser = False
            frmEditUser.ShowDialog()

            'Me.TopMost = True

            lvUsers.Items.Clear()

            If GlobalVariables.ResetUser Then

                RebuildPage()


            End If
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
