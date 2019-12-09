Class Household
    Protected ChanceEatOutPerDay As Single
    Protected XCoord, YCoord, ID As Integer
    Protected Shared NextID As Integer = 1

    Public Sub New(ByVal X As Integer, ByVal Y As Integer)
        XCoord = X
        YCoord = Y
        ChanceEatOutPerDay = Rnd()
        ID = NextID
        NextID += 1
    End Sub

    Public Function GetDetails() As String
        Dim Details As String
        Details = "ID: " & ID.ToString()
        For x = 0 To 10 - Len(ID.ToString())
            Details &= " "
        Next
        Details &= "Coordinates: (" & XCoord.ToString() & ", " & YCoord.ToString() & ")"
        for x = 0 to 14 - (Len(XCoord.ToString()) + Len(YCoord.ToString()))
            Details &= " "
        Next
        Details &= "Eat out probability: " & ChanceEatOutPerDay.ToString()
        Return Details
    End Function

    Public Function GetChanceEatOut() As Single
        Return ChanceEatOutPerDay
    End Function

    Public Function GetX() As Integer
        Return XCoord
    End Function

    Public Function GetY() As Integer
        Return YCoord
    End Function
End Class