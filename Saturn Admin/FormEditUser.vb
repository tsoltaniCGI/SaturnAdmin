Option Strict Off
Imports System.Data.SqlClient

Public Class FormEditUser
    Dim oFacilityIDs As New Collection
    Dim oUsers As New Collection
    Dim oVendIDs As New Collection

    Private Sub FormEditUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oCurrentUser As User
        Dim oCurrentFacility As Facility
        Dim sTestProd As String
        Dim oConn As SqlConnection
        sTestProd = "P"
        If sTestProd = "P" Then
            oConn = New SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Else
            oConn = New SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")

        End If

        oConn.Open()

        Dim myCmd = oConn.CreateCommand
        Dim sSql As String
        Dim iCnt As Integer
        Dim iMax As Integer


        sSql = "SELECT users.user_id, users_facilities.facility_id, facilities.facility_name"
        sSql = sSql & "FROM users, users_facilities, facilities"
        sSql = sSql & "WHERE users.user_id = users_facilities.user_id"
        sSql = sSql & "AND users_facilities.facility_id = facilities.facility_id"



        myCmd.CommandText = sSql

        Dim oReader = myCmd.ExecuteReader()

        If oReader.HasRows Then
            iCnt = 0
            Do While oReader.Read()
                lbFacilities.Items.Add(oReader.GetString(2))
                oFacilityIDs.Add(oReader.GetInt32(1), iCnt.ToString())
                iCnt = iCnt + 1
            Loop

        End If



        sSql = "SELECT user_role_id, user_role_description  "
        sSql = sSql & "FROM roles "


        myCmd = oConn.CreateCommand()
        myCmd.CommandText = sSql
        oReader = myCmd.ExecuteReader()

        If oReader.HasRows() Then
            Do While oReader.Read()
                If oReader.GetInt32(0) <> 0 Then
                    cmbUserRole.Items.Add(oReader.GetString(1))
                Else
                    cmbUserRole.Items.Add("")
                End If
            Loop
        End If

        oReader.Close()

        oConn.Close()

        oCurrentUser = GlobalVariables.CurrentUser
        txtFirstName.Text = oCurrentUser.UserFirstName
        txtLastName.Text = oCurrentUser.UserLastName
        txtLogin.Text = oCurrentUser.UserLogin
        cmbUserRole.Text = oCurrentUser.UserRole



        For Each oCurrentFacility In oCurrentUser.FacilityNames
            iCnt = 0
            iMax = lbFacilities.Items.Count - 1
            Do While iCnt <= iMax
                If oFacilityIDs(iCnt.ToString()) = oCurrentFacility.FacilityId Then
                    lbFacilities.SelectedIndices.Add(iCnt)
                End If
                iCnt = iCnt + 1
            Loop

        Next

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFacilities.SelectedIndexChanged

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim sSql As String
        Dim bDataValidated As Boolean
        Dim oCurrentUser As User
        Dim sCollIndex As String
        Dim oConn As SqlConnection

        oCurrentUser = GlobalVariables.CurrentUser
        GlobalVariables.iAddedUserID = 0
        GlobalVariables.ResetUser = True

        Dim sTestProd As String
        sTestProd = GlobalVariables.sEnv
        If sTestProd = "P" Then
            oConn = New SqlConnection("Server =pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Else
            oConn = New SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")
        End If

        oConn.Open()
        Dim myCmd = oConn.CreateCommand
        bDataValidated = True
        If Len(txtFirstName.Text.ToString()) = 0 Then
            MessageBox.Show("First Name Is Requied")
            bDataValidated = False
        End If


        If Len(txtLastName.Text.ToString()) = 0 Then
            MessageBox.Show("Last Name Is Requied")
            bDataValidated = False
        End If

        If Len(txtLogin.Text.ToString()) = 0 Then
            MessageBox.Show("User Login Is Requied")
            bDataValidated = False
        End If

        If lbFacilities.SelectedIndices.Count() = 0 Then
            MessageBox.Show("Facility Must Be Selected")
            bDataValidated = False
        End If

        If Len(cmbUserRole.SelectedIndex) = -1 Then
            MessageBox.Show("User Role Must be Selected")
            bDataValidated = False
        End If

        If bDataValidated Then
            sSql = "UPDATE users"
            sSql = sSql & "SET user_first_name = '" & GlobalVariables.DQuot(txtFirstName.Text.ToString()) & "', "
            sSql = sSql & "user_last_name = '" & GlobalVariables.DQuot(txtLastName.Text.ToString()) & "', "
            sSql = sSql & "user_login = '" & GlobalVariables.DQuot(txtLogin.Text.ToString()) & "', "
            sSql = sSql & "user_job_title = '" & GlobalVariables.DQuot(txtJobTitle.ToString()) & "', "
            sSql = sSql & "user_role_description = '" & GlobalVariables.DQuot(cmbUserRole.ToString()) & "', "
            sSql = sSql & "WHERE user_id = " & GlobalVariables.CurrentUser.UserID.ToString()

            myCmd.CommandText = sSql

            myCmd.ExecuteNonQuery()

            GlobalVariables.CurrentUser.UserFirstName = txtFirstName.Text.ToString()
            GlobalVariables.CurrentUser.UserLastName = txtLastName.Text.ToString()
            GlobalVariables.CurrentUser.UserLogin = txtLogin.Text.ToString()
            GlobalVariables.CurrentUser.UserJobTitle = txtJobTitle.Text.ToString()
            GlobalVariables.CurrentUser.UserRole = cmbUserRole.SelectedItem.ToString()

            GlobalVariables.CurrentUser.FacilityNames.Clear()
            For Each iIndex In lbFacilities.SelectedIndices
                sCollIndex = oFacilityIDs(iIndex.ToString()).ToString()
                sSql = "INSERT INTO users_facilities (user_id, facility_id) "
                sSql = sSql & "VALUES (" & GlobalVariables.CurrentUser.UserID.ToString() & ", " & oFacilityIDs(iIndex.ToString()).ToString() & ")"
                myCmd.CommandText = sSql
                myCmd.ExecuteNonQuery()
                GlobalVariables.CurrentUser.FacilityNames.Add(GlobalVariables.FacilityList(sCollIndex))
            Next


            For Each iIndex In lbFacilities.SelectedIndices
                sSql = "INSERT INTO vendors_facilities (dummy_vendor_id, facility_id) "
                sSql = sSql & "VALUES (" & GlobalVariables.CurrentDummyVendorID & ", " & oFacilityIDs(iIndex.ToString()).ToString() & ")"
                myCmd.CommandText = sSql
                myCmd.ExecuteNonQuery()
            Next

            Me.Close()

        End If



    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class