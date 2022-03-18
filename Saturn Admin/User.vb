Public Class UserFacility
    Public Property UserID As Integer
    Public Property UserFirstName As String
    Public Property UserLastName As String
    Public Property UserLogin As String
    Public Property UserJobTitle As String
    Public Property UserRole As String
    Public Property DummyVendorId As Integer

    Public Property FacilityId As Integer
    Public Property FacilityName As String


End Class

Public Class User
    Public Property UserID As Integer
    Public Property UserFirstName As String
    Public Property UserLastName As String
    Public Property UserLogin As String
    Public Property UserJobTitle As String
    Public Property UserRole As String
    Public Property DummyVendorId As Integer
    Public Property FacilityNames As New Collection
    Public Property FacilityIds As New Collection
End Class


Public Class Facility
    Public Property FacilityId As Integer
    Public Property FacilityName As String
End Class

Public Class IndexedUserListItem
    Public Property CollectionIndex As Integer
    Public Property UserName As String
End Class

Public Class GlobalVariables
    Public Shared CurrentUser As User
    Public Shared iAddedUserID As Integer
    Public Shared iAddedFacilityID As Integer
    Public Shared ResetUser As Boolean
    Public Shared sEnv As String
    Public Shared FacilityList As New Collection
    Public Shared bUserAdd As Boolean
    Public Shared CurrentUserID As Integer
    Public Shared CurrentFirstName As String
    Public Shared CurrentLastName As String
    Public Shared CurrentRoleDescription As String
    Public Shared CurrentUserRole As Integer
    Public Shared CurrentLogin As String
    Public Shared CurrentDummyVendorID As Integer

    Public Shared Function DQuot(lsConvStr As String) As String
        Dim lsRetStr As String
        lsRetStr = Replace(lsConvStr, "'", "''")
        DQuot = lsRetStr
    End Function


End Class


