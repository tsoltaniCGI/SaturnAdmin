Option Strict Off
Imports System.Data.SqlClient

Public Class FormEditUser
    Dim oFacilityIDs As New Collection
    Dim oUsers As New Collection
    Dim oVendIDs As New Collection
    Dim oCollRoles As New Collection

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

        Me.lblFirstName.Text = GlobalVariables.CurrentUser.UserFirstName
        Me.lblLastName.Text = GlobalVariables.CurrentUser.UserLastName
        Me.lblRole.Text = GlobalVariables.CurrentUser.UserRole


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


        'oCurrentUser = GlobalVariables.CurrentUser
        'txtFirstName.Text = oCurrentUser.UserFirstName
        'txtLastName.Text = oCurrentUser.UserLastName
        'txtLogin.Text = oCurrentUser.UserLogin
        'cmbUserRole.Text = oCurrentUser.UserRole



        'For Each oCurrentFacility In oCurrentUser.FacilityNames
        '    iCnt = 0
        '    iMax = lbFacilities.Items.Count - 1
        '    Do While iCnt <= iMax
        '        If oFacilityIDs(iCnt.ToString()) = oCurrentFacility.FacilityId Then
        '            lbFacilities.SelectedIndices.Add(iCnt)
        '        End If
        '        iCnt = iCnt + 1
        '    Loop

        'Next

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFacilities.SelectedIndexChanged

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        GlobalVariables.ResetUser = True
        Dim sSql As String
        Dim bDataValidated As Boolean
        Dim oCurrentUser As User
        Dim sCollIndex As String
        Dim oConn As SqlConnection
        Dim iDummyVendorID As Integer
        Dim oTrans As SqlTransaction
        Dim iUserRole As Integer
        Dim iVendorID As Integer
        Dim oReader As SqlDataReader

        oCurrentUser = GlobalVariables.CurrentUser
        GlobalVariables.iAddedUserID = 0
        GlobalVariables.ResetUser = True

        Dim sTestProd As String
        sTestProd = "P"
        'sTestProd = GlobalVariables.sEnv
        If sTestProd = "P" Then
            oConn = New SqlConnection("Server =pdx-sql14;Database=SATURN_PROD;UID=saturndba;PWD=saturndba")
        Else
            oConn = New SqlConnection("Server=pdx-sql16;Database=SATURN_DEV;UID=saturndba;PWD=saturndba")
        End If

        oConn.Open()

        'oTrans = oConn.BeginTransaction()

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


        'sSql = "SELECT vendor_id "
        'sSql = sSql & "FROM vendors "
        'sSql = sSql & "WHERE vendor_name = " & GlobalVariables.CurrentUser.UserLogin
        'myCmd.CommandText = sSql
        ''Conn.Open()
        'oReader = myCmd.ExecuteReader()


        'iVendorID = 0
        'If oReader.HasRows Then
        '    Do While oReader.Read()
        '        iVendorID = oReader.GetInt32(0)
        '    Loop
        'End If
        'oReader.Close()
        'Conn.Close()

        'myCmd.CommandText = sSql
        'iVendorID = myCmd.ExecuteScalar()
        'GlobalVariables.CurrentDummyVendorID = iVendorID


        If bDataValidated Then
            GlobalVariables.ResetUser = True
            iUserRole = oCollRoles(cmbUserRole.SelectedItem)
            sSql = "UPDATE users "
            sSql = sSql & "Set user_first_name = '" & GlobalVariables.DQuot(txtFirstName.Text.ToString()) & "', "
            sSql = sSql & "user_last_name = '" & GlobalVariables.DQuot(txtLastName.Text.ToString()) & "', "
            sSql = sSql & "user_login = '" & GlobalVariables.DQuot(txtLogin.Text.ToString()) & "', "
            sSql = sSql & "user_role = '" & iUserRole.ToString() & "' "
            'sSql = sSql & "user_role = '" & GlobalVariables.DQuot(cmbUserRole.ToString()) & "' "
            sSql = sSql & "WHERE user_id = " & GlobalVariables.CurrentUser.UserID.ToString()

            myCmd.CommandText = sSql

            myCmd.ExecuteNonQuery()

            GlobalVariables.CurrentUser.UserFirstName = txtFirstName.Text.ToString()
            GlobalVariables.CurrentUser.UserLastName = txtLastName.Text.ToString()
            GlobalVariables.CurrentUser.UserLogin = txtLogin.Text.ToString()
            GlobalVariables.CurrentUser.UserRole = cmbUserRole.SelectedItem.ToString()

            'oCurrentUser = GlobalVariables.CurrentUser

            'oTrans = oConn.BeginTransaction()
            'myCmd.Connection = oConn
            'myCmd.Transaction = oTrans


            sSql = "DELETE FROM users_facilities "
            sSql = sSql & "WHERE user_id = " & GlobalVariables.CurrentUser.UserID.ToString()
            myCmd.CommandText = sSql
            myCmd.ExecuteNonQuery()

            sSql = "DELETE FROM vendors_facilities "
            sSql = sSql & "WHERE vendor_id = " & GlobalVariables.CurrentDummyVendorID.ToString()
            myCmd.CommandText = sSql
            myCmd.ExecuteNonQuery()


            GlobalVariables.CurrentUser.FacilityNames.Clear()
            For Each iIndex In lbFacilities.SelectedIndices
                sCollIndex = oFacilityIDs(iIndex.ToString()).ToString()
                sSql = "INSERT INTO users_facilities (user_id, facility_id) "
                sSql = sSql & "VALUES (" & GlobalVariables.CurrentUser.UserID.ToString() & ", " & oFacilityIDs(iIndex.ToString()).ToString() & ")"
                myCmd.CommandText = sSql
                myCmd.ExecuteNonQuery()
                'GlobalVariables.CurrentUser.FacilityNames.Add(GlobalVariables.FacilityList(sCollIndex))
            Next



            For Each iIndex In lbFacilities.SelectedIndices
                sSql = "INSERT INTO vendors_facilities (vendor_id, facility_id) "
                sSql = sSql & "VALUES (" & GlobalVariables.CurrentDummyVendorID.ToString() & ", " & oFacilityIDs(iIndex.ToString()).ToString() & ")"
                myCmd.CommandText = sSql
                myCmd.ExecuteNonQuery()
                'DummyVendorID = myCmd.ExecuteScalar()
                'lobalVariables.CurrentDummyVendorID = iDummyVendorID
            Next

            sSql = "UPDATE vendors "
            sSql = sSql & "SET vendor_name = '" & txtLogin.Text.ToUpper() & "' "
            sSql = sSql & "WHERE vendor_id = " & GlobalVariables.CurrentDummyVendorID.ToString()
            myCmd.CommandText = sSql
            myCmd.ExecuteNonQuery()

            'oTrans.Commit()
            'Me.Close()

            If MessageBox.Show("Name: " & GlobalVariables.CurrentUser.UserFirstName & " " & GlobalVariables.CurrentUser.UserLastName & vbCrLf & "Login : " & GlobalVariables.CurrentUser.UserLogin & vbCrLf & "Role: " & GlobalVariables.CurrentUser.UserRole & vbCrLf & vbCrLf & " Do you approve?", " ", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Me.Close()
            End If



        End If



    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub lblLastName_Click(sender As Object, e As EventArgs) Handles lblLastName.Click

    End Sub

    Dim a As String
    Dim b As String


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Len(txtFirstName.Text.ToString()) >= 1 Then
            Dim FirstLetter As String
            FirstLetter = txtFirstName.Text.Substring(0, 1)
            a = FirstLetter.ToString()
            b = txtLastName.Text.ToString()

            txtLogin.Text = a + b
        End If
    End Sub

    Private Sub txtFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub txtLogin_TextChanged(sender As Object, e As EventArgs) Handles txtLogin.TextChanged

    End Sub
End Class