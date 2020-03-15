Class Settlement
    Protected StartNoOfHouseholds, XSize, YSize As Integer
    Protected Households As New ArrayList

    Public Sub New()
        XSize = 1000
        YSize = 1000
        StartNoOfHouseholds = 250
        CreateHouseholds()
    End Sub

    Public Function GetNumberOfHouseholds() As Integer
        Return Households.Count
    End Function

    Public Function GetXSize() As Integer
        Return XSize
    End Function

    Public Function GetYSize() As Integer
        Return YSize
    End Function

    Public Sub GetRandomLocation(ByRef X As Integer, ByRef Y As Integer)
        X = Int(Rnd() * XSize)
        Y = Int(Rnd() * YSize)
    End Sub

    Protected Sub CreateHouseholds()
        For Count = 0 To StartNoOfHouseholds - 1
            AddHousehold()
        Next
    End Sub

    Public Sub AddHousehold()
        Dim X, Y As Integer
        GetRandomLocation(X, Y)
        Dim Temp As New Household(X, Y)
        Households.Add(Temp)
    End Sub

    Public Sub DisplayHouseholds()
        Console.WriteLine(Environment.NewLine & "**********************************")
        Console.WriteLine("*** Details of all households: ***")
        Console.WriteLine("**********************************" & Environment.NewLine)
        For Each H In Households
            Console.WriteLine(H.GetDetails())
        Next
        Console.WriteLine()
    End Sub


    Public Function FindOutIfHouseholdEatsOut(ByVal HouseholdNo As Integer, ByRef X As Integer, ByRef Y As Integer, ByVal days As Integer) As Boolean 'added days paramter
        Dim EatOutRNo As Single = Rnd()
        Dim change As Single
        Select Case (change Mod 7) 'added select statement so that probability of eating out is lower on mon but higher on fri, sat and sun
            Case 0 : change = 0.8
            Case 4 : change = 1.2
            Case 5 : change = 1.2
            Case 6 : change = 1.2
        End Select

        X = Households(HouseholdNo).GetX()
        Y = Households(HouseholdNo).GetY()
        If EatOutRNo < Households(HouseholdNo).GetChanceEatOut() * change Then ' added multiply by change to take into account the change in probability eating on mon / fri / sat / sat
            Return True
        Else
            Return False
        End If
    End Function
End Class