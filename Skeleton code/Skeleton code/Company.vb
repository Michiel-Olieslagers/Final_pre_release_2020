Imports System.Math
Class Company
    Protected Name, Category As String
    Protected Balance, ReputationScore, AvgCostPerMeal, AvgPricePerMeal, DailyCosts, FamilyOutletCost, FastFoodOutletCost, NamedChefOutletCost, FuelCostPerUnit, BaseCostOfDelivery As Single
    Protected Outlets As New ArrayList
    Protected FamilyFoodOutletCapacity, FastFoodOutletCapacity, NamedChefOutletCapacity As Integer
    'NOTE: FormatCurrency(CalculateDeliveryCost.ToString, 2) CONVERTS INTO MONEY FORMAT
    Public Sub New(ByVal Name As String, ByVal Category As String, ByVal Balance As Single, ByVal X As Integer, ByVal Y As Integer, ByVal FuelCostPerUnit As Single, ByVal BaseCostOfDelivery As Single)
        FamilyOutletCost = 1000
        FastFoodOutletCost = 2000
        NamedChefOutletCost = 15000
        FamilyFoodOutletCapacity = 150
        FastFoodOutletCapacity = 200
        NamedChefOutletCapacity = 50
        Me.Name = Name
        Me.Category = Category
        Me.Balance = Balance
        Me.FuelCostPerUnit = FuelCostPerUnit
        Me.BaseCostOfDelivery = BaseCostOfDelivery
        ReputationScore = 100
        DailyCosts = 100
        If Me.Category = "fast food" Then
            AvgCostPerMeal = 5
            AvgPricePerMeal = 10
            ReputationScore += Rnd() * 10 - 8
        ElseIf Me.Category = "family" Then
            AvgCostPerMeal = 12
            AvgPricePerMeal = 14
            ReputationScore += Rnd() * 30 - 5
        Else
            AvgCostPerMeal = 20
            AvgPricePerMeal = 40
            ReputationScore += Rnd() * 50
        End If
        OpenOutlet(X, Y)
    End Sub
    Public Function GetBalance() As Single 'added for power outage event happening
        Return Balance
    End Function
    Public Sub AlterBalance(ByVal Change As Single) 'added for power outage event happening
        Balance -= Change
    End Sub
    Public Function GetName() As String
        Return Name
    End Function
    Public Function GetFuelCostPerUnit() As Integer 'Add a new public function to the company class to get access to the current value of FuelCostPerUnit
        Return FuelCostPerUnit
    End Function
    Public Function GetNumberOfOutlets() As Integer
        Return Outlets.Count
    End Function

    Public Function GetReputationScore() As Single
        Return ReputationScore
    End Function

    Public Sub AlterDailyCosts(ByVal Change As Single)
        Dim oldAmount As Single = DailyCosts
        DailyCosts += Change
        If DailyCosts > 0 Then
            'daily cost unchanged
        Else
            DailyCosts = 0 'idk it this is the right thing but checking that daily cost isn't negative
        End If
    End Sub

    Public Sub AlterAvgCostPerMeal(ByVal Change As Single)
        AvgCostPerMeal += Change
    End Sub

    Public Sub AlterFuelCostPerUnit(ByVal Change As Single)
        FuelCostPerUnit += Change
    End Sub
    Public Function GetAvgCostPerMeal() 'added for merge
        Return AvgCostPerMeal
    End Function
    Public Function GetDailyCosts() 'added for merge
        Return DailyCosts
    End Function
    Public Function getBaseCostOfDelivery() 'added for merge
        Return BaseCostOfDelivery
    End Function
    Public Function getOutlets() ' added for merge
        Return Outlets
    End Function
    Public Function getCategory() 'added for merge
        Return Category
    End Function
    Public Function GetAvgPricePerMeal() 'added for merge
        Return AvgPricePerMeal
    End Function
    Public Function isBankrupt(Balance) 'added for bankrupt
        If Balance <= -10000 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub AlterReputation(ByVal Change As Single)
        ReputationScore += Change
    End Sub

    Public Sub NewDay()
        For Each O In Outlets
            O.NewDay()
        Next
    End Sub

    Public Sub AddVisitToNearestOutlet(ByVal X As Integer, ByVal Y As Integer)
        Dim NearestOutlet As Integer = 0
        Dim NearestOutletDistance, CurrentDistance As Single
        NearestOutletDistance = Sqrt((Outlets(0).GetX() - X) ^ 2 + (Outlets(0).GetY() - Y) ^ 2)
        For Current = 1 To Outlets.Count - 1
            CurrentDistance = Sqrt((Outlets(Current).GetX() - X) ^ 2 + (Outlets(Current).GetY() - Y) ^ 2)
            If CurrentDistance < NearestOutletDistance Then
                NearestOutletDistance = CurrentDistance
                NearestOutlet = Current
            End If
        Next
        Outlets(NearestOutlet).IncrementVisits()
    End Sub
    Public Function processGoBankruptEvent() 'added for bankrupt evenCalculat
        If Balance < -1000 Then
            Console.WriteLine(Name & " has gone bankrupt and is closing")
            Return True
        End If
        Return False
    End Function
    Public Function GetDetails() As String
        Dim Details As String = ""
        Details &= "Name: " & Name & Environment.NewLine & "Type of business: " & Category & Environment.NewLine
        Details &= "Current balance: " & FormatCurrency(Balance.ToString, 2) & Environment.NewLine & "Average cost per meal: " & FormatCurrency(AvgCostPerMeal.ToString, 2) & Environment.NewLine
        Details &= "Average price per meal: " & FormatCurrency(AvgPricePerMeal.ToString, 2) & Environment.NewLine & "Daily costs: " & FormatCurrency(DailyCosts.ToString, 2) & Environment.NewLine
        Details &= "Delivery costs: " & FormatCurrency(CalculateDeliveryCost.ToString, 2) & Environment.NewLine & "Reputation: " & ReputationScore.ToString() & Environment.NewLine & Environment.NewLine
        Details &= "Number of outlets: " & Outlets.Count.ToString() & Environment.NewLine & "Outlets" & Environment.NewLine
        For Current = 1 To Outlets.Count
            Details &= Current.ToString() & ". " & Outlets(Current - 1).GetDetails() & Environment.NewLine
        Next
        Return Details
    End Function

    Public Function ProcessDayEnd() As String
        Dim Details As String = ""
        Dim ProfitLossFromOutlets As Single = 0
        Dim ProfitLossFromThisOutlet As Single = 0
        Dim DeliveryCosts As Single
        If Outlets.Count > 1 Then
            DeliveryCosts = BaseCostOfDelivery + CalculateDeliveryCost()
        Else
            DeliveryCosts = BaseCostOfDelivery
        End If
        Details &= "Daily costs for company: " & DailyCosts.ToString() & Environment.NewLine & "Cost for delivering produce to outlets: " & DeliveryCosts.ToString() & Environment.NewLine
        For Current = 0 To Outlets.Count - 1
            ProfitLossFromThisOutlet = Outlets(Current).CalculateDailyProfitLoss(AvgCostPerMeal, AvgPricePerMeal)
            Details &= "Outlet " & (Current + 1).ToString() & " profit/loss: " & FormatCurrency(ProfitLossFromThisOutlet.ToString, 2) & Environment.NewLine
            ProfitLossFromOutlets += ProfitLossFromThisOutlet
        Next
        Details &= "Previous balance for company: " & FormatCurrency(Balance.ToString, 2) & Environment.NewLine
        Balance += ProfitLossFromOutlets - DailyCosts - DeliveryCosts
        Details &= "New balance for company: " & FormatCurrency(Balance.ToString, 2)
        Return Details
    End Function

    Public Function CloseOutlet(ByVal ID As Integer) As Boolean
        Dim CloseCompany As Boolean = False
        Outlets.RemoveAt(ID)
        If Outlets.Count = 0 Then
            CloseCompany = True
        End If
        Return CloseCompany
    End Function

    Public Sub ExpandOutlet(ByVal ID As Integer)
        Dim Change, Result As Integer
        Console.Write("Enter amount you would like to expand the capacity by: ")
        Change = Console.ReadLine()
        Result = Outlets(ID).AlterCapacity(Change)
        If Result = Change Then
            Console.WriteLine("Capacity adjusted.")
        Else
            Console.WriteLine("Only some of that capacity added, outlet now at maximum capacity.")
        End If
    End Sub

    Public Sub JoinOutlets()
        'find all the outlets of 1

        'get the details of it
        'add to array
        'find all the outlets of 2
        'get the details of it
        'add to array
    End Sub

    Public Sub OpenOutlet(ByVal X As Integer, ByVal Y As Integer)
        Dim Capacity As Integer
        If Category = "fast food" Then
            Balance -= FastFoodOutletCost
            Capacity = FastFoodOutletCapacity
        ElseIf Category = "family" Then
            Balance -= FamilyOutletCost
            Capacity = FamilyFoodOutletCapacity
        Else
            Balance -= NamedChefOutletCost
            Capacity = NamedChefOutletCapacity
        End If
        Dim NewOutlet As New Outlet(X, Y, Capacity)
        Outlets.Add(NewOutlet)
    End Sub
    Private Function GetListOfOutlets() As ArrayList
        Dim Temp As New ArrayList
        For Current = 0 To Outlets.Count - 1
            Temp.Add(Current)
        Next
        Return Temp
    End Function


    Private Function GetDistanceBetweenTwoOutlets(ByVal Outlet1 As Integer, ByVal Outlet2 As Integer) As Single
        Return Sqrt((Outlets(Outlet1).GetX() - Outlets(Outlet2).GetX()) ^ 2 + (Outlets(Outlet1).GetY() - Outlets(Outlet2).GetY()) ^ 2)
    End Function

    Public Function CalculateDeliveryCost() As Single
        Dim ListOfOutlets As ArrayList = GetListOfOutlets()
        Dim TotalDistance As Single = 0
        Dim TotalCost As Single
        For Current = 0 To ListOfOutlets.Count - 2
            TotalDistance += GetDistanceBetweenTwoOutlets(ListOfOutlets(Current), ListOfOutlets(Current + 1))
        Next
        TotalCost = TotalDistance * FuelCostPerUnit
        Return TotalCost
    End Function
End Class