Option Strict Off
Imports System.Data.SqlClient



Public Class FormAddUser
    Dim oFacilityIDs As New Collection
    Dim oUsers As New Collection
    Dim oCollRoles As New Collection

    Private Sub FormAddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oConn As SqlConnection

        'oConn = New SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Dim sTestProd As String
        sTestProd = "T"
        If sTestProd = "P" Then
            oConn = New System.Data.SqlClient.SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Else
            oConn = New System.Data.SqlClient.SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")
        End If
        oConn.Open()
        Dim myCmd = oConn.CreateCommand
        Dim sSql As String
        Dim iCnt As Integer

        'lbFacilities.Sorted = True


        sSql = "SELECT users_facilities.facility_id, facilities.facility_name "
        sSql = sSql & "FROM users_facilities, facilities "
        sSql = sSql & "WHERE users_facilities.facility_id = facilities.facility_id "

        myCmd.CommandText = sSql

        Dim oReader = myCmd.ExecuteReader()
        oFacilityIDs.Clear()
        If oReader.HasRows Then
            iCnt = 0
            Do While oReader.Read()
                lbFacilities.Items.Add(oReader.GetString(1))
                oFacilityIDs.Add(oReader.GetInt32(0), iCnt.ToString())
                iCnt = iCnt + 1
            Loop
        End If

        oReader.Close()
        cmbUserRole.DisplayMember = "Text"
        cmbUserRole.ValueMember = "Value"

        sSql = "SELECT user_role, user_role_description  "
        sSql = sSql & "FROM users_roles "


        myCmd = oConn.CreateCommand()
        myCmd.CommandText = sSql
        oReader = myCmd.ExecuteReader()

        If oReader.HasRows() Then
            Do While oReader.Read()
                cmbUserRole.Items.Add(oReader.GetString(1))
                oCollRoles.Add(oReader.GetInt32(0), oReader.GetString(1))
            Loop
        End If



        oReader.Close()
        oConn.Close()


    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        GlobalVariables.ResetUser = True
        GlobalVariables.CurrentFirstName = txtUserFirstName.Text
        GlobalVariables.CurrentLastName = txtUserLastName.Text
        GlobalVariables.CurrentLogin = txtUserLogin.Text
        GlobalVariables.CurrentRoleDescription = cmbUserRole.SelectedIndex

        Dim sSql As String
        Dim iUserID As Integer
        Dim iDummyVendorID As Integer
        Dim iFacilityID As Integer
        Dim iVendorID As Integer
        Dim iUserRole As Integer
        Dim bDataValidated As Boolean
        Dim oConn As SqlConnection

        'oConn = New SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")

        Dim sTestProd As String
        sTestProd = "T"
        If sTestProd = "P" Then
            oConn = New System.Data.SqlClient.SqlConnection("Server=pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Else
            oConn = New System.Data.SqlClient.SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")
        End If

        oConn.Open()
        Dim myCmd = oConn.CreateCommand




        bDataValidated = True
        If Len(txtUserFirstName.Text.ToString()) = 0 Then
            MessageBox.Show("First Name is Requied")
            bDataValidated = False
        End If

        If Len(txtUserLastName.Text.ToString()) = 0 Then
            MessageBox.Show("Last Name is Required")
            bDataValidated = False
        End If

        If Len(txtUserLogin.Text.ToString()) = 0 Then
            MessageBox.Show("User Login is Required")
            bDataValidated = False
        End If

        If lbFacilities.SelectedIndices.Count = 0 Then
            MessageBox.Show("Facility must be Selected")
            bDataValidated = False
        End If

        If Len(cmbUserRole.SelectedIndex) = -1 Then
            MessageBox.Show("User Role Must be Selected")
            bDataValidated = False
        End If

        If bDataValidated Then
            GlobalVariables.ResetUser = True
            'MessageBox.Show(cmbUserRole.SelectedIndex)
            iFacilityID = 0
            iUserRole = oCollRoles(cmbUserRole.SelectedItem)
            sSql = "INSERT INTO users (user_first_name, user_last_name, user_login, user_role) "
            sSql = sSql & "VALUES ('" & txtUserFirstName.Text & "', '" & txtUserLastName.Text & "', '" & txtUserLogin.Text & "', "
            sSql = sSql & iUserRole.ToString() & ")"
            sSql = sSql & "; SELECT @@IDENTITY "
            myCmd.CommandText = sSql
            iUserID = myCmd.ExecuteScalar()
            GlobalVariables.iAddedUserID = iUserID

            'myCmd.CommandText = sSql
            For Each iIndex In lbFacilities.SelectedIndices
                sSql = "INSERT INTO users_facilities (user_id, facility_id) "
                sSql = sSql & "VALUES (" & iUserID.ToString() & ", " & oFacilityIDs(iIndex.ToString()).ToString() & ")"
                myCmd.CommandText = sSql
                myCmd.ExecuteNonQuery()
            Next


            sSql = "INSERT INTO vendors(vendor_name, vendor_dummy) "
            sSql = sSql & "VALUES ('" & txtUserLogin.Text.ToUpper() & "', 'Y' ); SELECT @@IDENTITY "
            myCmd.CommandText = sSql
            iDummyVendorID = myCmd.ExecuteScalar()
            GlobalVariables.CurrentDummyVendorID = iDummyVendorID


            sSql = "UPDATE users "
            sSql = sSql & "SET dummy_vendor_id = " & iDummyVendorID.ToString() & " "
            sSql = sSql & "WHERE user_id = " & iUserID.ToString()
            myCmd.CommandText = sSql
            myCmd.ExecuteNonQuery()


            For Each iIndex In lbFacilities.SelectedIndices
                sSql = "INSERT INTO vendors_facilities (vendor_id, facility_id) "
                sSql = sSql & "VALUES (" & iDummyVendorID.ToString() & ", " & oFacilityIDs(iIndex.ToString()).ToString() & ")"
                myCmd.CommandText = sSql
                myCmd.ExecuteNonQuery()
            Next


        End If
        oConn.Close()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
