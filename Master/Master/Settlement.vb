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
        Dim done As Boolean
        Do
            done = True
            X = Int(Rnd() * XSize)
            Y = Int(Rnd() * YSize)
            For Each H In Households
                If H.GetX() = X And H.GetY() = Y Then
                    done = False
                End If
            Next
        Loop Until done = True
    End Sub

    Protected Sub CreateHouseholds()
        For Count = 0 To StartNoOfHouseholds - 1
            AddHousehold()
        Next
    End Sub

    Public Sub AddHousehold()
        Dim X, Y As Integer
        GetRandomLocation(X, Y)
        Dim count As Integer = 0
        While (count < Households.Count)
            If (Households(count).getX() = X And Households(count).getY() = Y) Then
                GetRandomLocation(X, Y)
                count = 0
            Else
                count += 1
            End If
        End While
        Dim Temp As New Household(X, Y)
        Households.Add(Temp)
    End Sub

    Public Sub DisplayHouseholds()
        Console.WriteLine(Environment.NewLine & "**********************************")
        Console.WriteLine("*** Details of all households: ***")
        Console.WriteLine("**********************************" & Environment.NewLine)
        Console.WriteLine("Size of settlement: " & XSize & " , " & YSize & Environment.NewLine)
        For Each H In Households
            Console.WriteLine(H.GetDetails())
        Next
        Console.WriteLine()
    End Sub

    Public Function FindOutIfHouseholdEatsOut(ByVal HouseholdNo As Integer, ByRef X As Integer, ByRef Y As Integer) As Boolean
        Dim EatOutRNo As Single = Rnd()
        X = Households(HouseholdNo).GetX()
        Y = Households(HouseholdNo).GetY()
        If EatOutRNo < Households(HouseholdNo).GetChanceEatOut() Then
            Return True
        Else
            Return False
        End If
    End Function
End Class