Class Outlet
    Protected VisitsToday, XCoord, YCoord, Capacity, MaxCapacity As Integer
    Protected DailyCosts As Single

    Public Sub New(ByVal XCoord As Integer, ByVal YCoord As Integer, ByVal MaxCapacityBase As Integer)
        Me.XCoord = XCoord
        Me.YCoord = YCoord
        Capacity = Int(MaxCapacityBase * 0.6)
        MaxCapacity = MaxCapacityBase + Int(Rnd() * 50) - Int(Rnd() * 50)
        DailyCosts = MaxCapacityBase * 0.2 + Capacity * 0.5 + 100
        NewDay()
    End Sub
    Public Sub New(ByVal MaxX As Integer, ByVal MaxY As Integer, ByVal MaxCapacityBase As Integer, ByVal isRandom As Boolean)
        Me.XCoord = CInt(Math.Ceiling(Rnd() * MaxX))
        Me.YCoord = CInt(Math.Ceiling(Rnd() * MaxY))
        Capacity = Int(MaxCapacityBase * 0.6)
        MaxCapacity = MaxCapacityBase + Int(Rnd() * 50) - Int(Rnd() * 50)
        DailyCosts = MaxCapacityBase * 0.2 + Capacity * 0.5 + 100
        NewDay()
    End Sub

    Public Function GetCapacity() As Integer
        Return Capacity
    End Function

    Public Function GetX() As Integer
        Return XCoord
    End Function

    Public Function GetY() As Integer
        Return YCoord
    End Function
    Public Function ExtendCapacity(ByVal perimeter As Integer) As Integer 'added to extend capacity of outlet
        MaxCapacity = MaxCapacity * perimeter
        Return MaxCapacity
    End Function
    Public Sub AlterDailyCost(ByVal Amount As Single) 'changed
        Dim oldAmount As Single = DailyCosts
        DailyCosts += Amount
        If DailyCosts > 0 Then
            'daily cost unchanged
        Else
            DailyCosts = 0 'idk it this is the right thing but checking that daily cost isn't negative
        End If
    End Sub

    Public Function AlterCapacity(ByVal Change As Integer) As Integer
        Dim OldCapacity As Integer = Capacity
        Capacity += Change
        If Capacity > MaxCapacity Then 'returns no. people it will allow
            Capacity = MaxCapacity
            Return MaxCapacity - OldCapacity
        ElseIf Capacity < 0 Then 'error checks
            Capacity = 0
        End If
        DailyCosts = MaxCapacity * 0.2 + Capacity * 0.5 + 100 'changes cost depending on capacity
        Return Change
    End Function

    Public Sub IncrementVisits()
        VisitsToday += 1
    End Sub

    Public Sub NewDay()
        VisitsToday = 0
    End Sub

    Public Function CalculateDailyProfitLoss(ByVal AvgCostPerMeal As Single, ByVal AvgPricePerMeal As Single) As Single
        Return (AvgPricePerMeal - AvgCostPerMeal) * VisitsToday - DailyCosts
    End Function

    Public Function GetDetails() As String
        Dim Details As String
        Details = "Coordinates: (" & XCoord.ToString() & ", " & YCoord.ToString() & ")     Capacity: " & Capacity.ToString() & "      Maximum Capacity: "
        Details &= MaxCapacity.ToString() & "      Daily Costs: " & DailyCosts.ToString() & "      Visits today: " & VisitsToday.ToString()
        Return Details
    End Function
End Class