'Skeleton Program code for the AQA A Level Paper 1 Summer 2020 examination
'this code should be used in conjunction with the Preliminary Material
'written by the AQA Programmer Team
'developed in the Visual Studio Community Edition programming environment

Imports System.Math

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
        Details = ID.ToString() & "     Coordinates: (" & XCoord.ToString() & ", " & YCoord.ToString() & ")     Eat out probability: " & ChanceEatOutPerDay.ToString()
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

Class LargeSettlement
    Inherits Settlement
    Public Sub New(ByVal ExtraXSize As Integer, ByVal ExtraYSize As Integer, ByVal ExtraHouseholds As Integer)
        MyBase.New()
        Me.XSize += ExtraXSize
        Me.YSize += ExtraYSize
        Me.StartNoOfHouseholds += ExtraHouseholds
        For Count = 1 To ExtraHouseholds
            AddHousehold()
        Next
    End Sub
End Class

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

    Public Function GetCapacity() As Integer
        Return Capacity
    End Function

    Public Function GetX() As Integer
        Return XCoord
    End Function

    Public Function GetY() As Integer
        Return YCoord
    End Function

    Public Sub AlterDailyCost(ByVal Amount As Single)
        DailyCosts += Amount
    End Sub

    Public Function AlterCapacity(ByVal Change As Integer) As Integer
        Dim OldCapacity As Integer = Capacity
        Capacity += Change
        If Capacity > MaxCapacity Then
            Capacity = MaxCapacity
            Return MaxCapacity - OldCapacity
        ElseIf Capacity < 0 Then
            Capacity = 0
        End If
        DailyCosts = MaxCapacity * 0.2 + Capacity * 0.5 + 100
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

Class Company
    Protected Name, Category As String
    Protected Balance, ReputationScore, AvgCostPerMeal, AvgPricePerMeal, DailyCosts, FamilyOutletCost, FastFoodOutletCost, NamedChefOutletCost, FuelCostPerUnit, BaseCostOfDelivery As Single
    Protected Outlets As New ArrayList
    Protected FamilyFoodOutletCapacity, FastFoodOutletCapacity, NamedChefOutletCapacity As Integer

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

    Public Function GetName() As String
        Return Name
    End Function

    Public Function GetNumberOfOutlets() As Integer
        Return Outlets.Count
    End Function

    Public Function GetReputationScore() As Single
        Return ReputationScore
    End Function

    Public Sub AlterDailyCosts(ByVal Change As Single)
        DailyCosts += Change
    End Sub

    Public Sub AlterAvgCostPerMeal(ByVal Change As Single)
        AvgCostPerMeal += Change
    End Sub

    Public Sub AlterFuelCostPerUnit(ByVal Change As Single)
        FuelCostPerUnit += Change
    End Sub

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

    Public Function GetDetails() As String
        Dim Details As String = ""
        Details &= "Name: " & Name & Environment.NewLine & "Type of business: " & Category & Environment.NewLine
        Details &= "Current balance: " & Balance.ToString() & Environment.NewLine & "Average cost per meal: " & AvgCostPerMeal.ToString() & Environment.NewLine
        Details &= "Average price per meal: " & AvgPricePerMeal.ToString() & Environment.NewLine & "Daily costs: " & DailyCosts.ToString() & Environment.NewLine
        Details &= "Delivery costs: " & CalculateDeliveryCost().ToString() & Environment.NewLine & "Reputation: " & ReputationScore.ToString() & Environment.NewLine & Environment.NewLine
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
            Details &= "Outlet " & (Current + 1).ToString() & " profit/loss: " & ProfitLossFromThisOutlet.ToString() & Environment.NewLine
            ProfitLossFromOutlets += ProfitLossFromThisOutlet
        Next
        Details &= "Previous balance for company: " & Balance.ToString() & Environment.NewLine
        Balance += ProfitLossFromOutlets - DailyCosts - DeliveryCosts
        Details &= "New balance for company: " & Balance.ToString()
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
        Dim NewOutlet As New Outlet(5, 10, Capacity)
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

Class Simulation
    Protected SimulationSettlement As Settlement
    Protected NoOfCompanies As Integer
    Protected FuelCostPerUnit, BaseCostForDelivery As Single
    Protected Companies As New ArrayList

    Public Sub New()
        FuelCostPerUnit = 0.0098
        BaseCostForDelivery = 100
        Dim Choice As String
        Console.Write("Enter L for a large settlement, anything else for a normal size settlement: ")
        Choice = Console.ReadLine()
        If Choice = "L" Then
            Dim ExtraX, ExtraY, ExtraHouseholds As Integer
            Console.Write("Enter additional amount to add to X size of settlement: ")
            ExtraX = Console.ReadLine()
            Console.Write("Enter additional amount to add to Y size of settlement: ")
            ExtraY = Console.ReadLine()
            Console.Write("Enter additional number of households to add to settlement: ")
            ExtraHouseholds = Console.ReadLine()
            SimulationSettlement = New LargeSettlement(ExtraX, ExtraY, ExtraHouseholds)
        Else
            SimulationSettlement = New Settlement()
        End If
        Console.Write("Enter D for default companies, anything else to add your own start companies: ")
        Choice = Console.ReadLine()
        If Choice = "D" Then
            NoOfCompanies = 3
            Dim Company1 As New Company("AQA Burgers", "fast food", 100000, 200, 203, FuelCostPerUnit, BaseCostForDelivery)
            Companies.Add(Company1)
            Companies(0).OpenOutlet(300, 987)
            Companies(0).OpenOutlet(500, 500)
            Companies(0).OpenOutlet(305, 303)
            Companies(0).OpenOutlet(874, 456)
            Companies(0).OpenOutlet(23, 408)
            Companies(0).OpenOutlet(412, 318)
            Dim Company2 As New Company("Ben Thor Cuisine", "named chef", 100400, 390, 800, FuelCostPerUnit, BaseCostForDelivery)
            Companies.Add(Company2)
            Dim Company3 As New Company("Paltry Poultry", "fast food", 25000, 800, 390, FuelCostPerUnit, BaseCostForDelivery)
            Companies.Add(Company3)
            Companies(2).OpenOutlet(400, 390)
            Companies(2).OpenOutlet(820, 370)
            Companies(2).OpenOutlet(800, 600)
        Else
            Console.Write("Enter number of companies that exist at start of simulation: ")
            NoOfCompanies = Console.ReadLine()
            For Count = 1 To NoOfCompanies
                AddCompany()
            Next
        End If
    End Sub

    Public Sub DisplayMenu()
        Console.WriteLine(Environment.NewLine & "*********************************")
        Console.WriteLine("**********    MENU     **********")
        Console.WriteLine("*********************************")
        Console.WriteLine("1. Display details of households")
        Console.WriteLine("2. Display details of companies")
        Console.WriteLine("3. Modify company")
        Console.WriteLine("4. Add new company")
        Console.WriteLine("6. Advance to next day")
        Console.WriteLine("Q. Quit")
        Console.Write(Environment.NewLine & "Enter your choice: ")
    End Sub

    Private Sub DisplayCompaniesAtDayEnd()
        Dim Details As String
        Console.WriteLine(Environment.NewLine & "**********************")
        Console.WriteLine("***** Companies: *****")
        Console.WriteLine("**********************" & Environment.NewLine)
        For Each C In Companies
            Console.WriteLine(C.GetName())
            Console.WriteLine()
            Details = C.ProcessDayEnd()
            Console.WriteLine(Details & Environment.NewLine)
        Next
    End Sub

    Private Sub ProcessAddHouseholdsEvent()
        Dim NoOfNewHouseholds As Integer = Int(Rnd() * 4) + 1
        For Count = 1 To NoOfNewHouseholds
            SimulationSettlement.AddHousehold()
        Next
        Console.WriteLine(NoOfNewHouseholds.ToString() & " new households have been added to the settlement.")
    End Sub

    Private Sub ProcessCostOfFuelChangeEvent()
        Dim FuelCostChange As Single = (Int(Rnd() * 9) + 1) / 10
        Dim UpOrDown As Integer = Int(Rnd() * 2)
        Dim CompanyNo As Integer = Int(Rnd() * Companies.Count)
        If UpOrDown = 0 Then
            Console.WriteLine("The cost of fuel has gone up by " & FuelCostChange.ToString() & " for " & Companies(CompanyNo).GetName())
        Else
            Console.WriteLine("The cost of fuel has gone down by " & FuelCostChange.ToString() & " for " & Companies(CompanyNo).GetName())
            FuelCostChange *= -1
        End If
        Companies(CompanyNo).AlterFuelCostPerUnit(FuelCostChange)
    End Sub

    Private Sub ProcessReputationChangeEvent()
        Dim ReputationChange As Single = (Int(Rnd() * 9) + 1) / 10
        Dim UpOrDown As Integer = Int(Rnd() * 2)
        Dim CompanyNo As Integer = Int(Rnd() * Companies.Count)
        If UpOrDown = 0 Then
            Console.WriteLine("The reputation of " & Companies(CompanyNo).GetName() & " has gone up by " & ReputationChange.ToString())
        Else
            Console.WriteLine("The reputation of " & Companies(CompanyNo).GetName() & " has gone down by " & ReputationChange.ToString())
            ReputationChange *= -1
        End If
        Companies(CompanyNo).AlterReputation(ReputationChange)
    End Sub

    Private Sub ProcessCostChangeEvent()
        Dim CostToChange As Integer = Int(Rnd() * 2)
        Dim UpOrDown As Integer = Int(Rnd() * 2)
        Dim CompanyNo As Integer = Int(Rnd() * Companies.Count)
        Dim AmountOfChange As Single
        If CostToChange = 0 Then
            AmountOfChange = (Int(Rnd() * 19) + 1) / 10
            If UpOrDown = 0 Then
                Console.WriteLine("The daily costs for " & Companies(CompanyNo).GetName() & " have gone up by " & AmountOfChange.ToString())
            Else
                Console.WriteLine("The daily costs for " & Companies(CompanyNo).GetName() & " have gone down by " & AmountOfChange.ToString())
                AmountOfChange *= -1
            End If
            Companies(CompanyNo).AlterDailyCosts(AmountOfChange)
        Else
            AmountOfChange = Int((Rnd() * 9) + 1) / 10
            If UpOrDown = 0 Then
                Console.WriteLine("The average cost of a meal for " & Companies(CompanyNo).GetName() & " has gone up by " & AmountOfChange.ToString())
            Else
                Console.WriteLine("The average cost of a meal for " & Companies(CompanyNo).GetName() & " has gone down by " & AmountOfChange.ToString())
                AmountOfChange *= -1
            End If
            Companies(CompanyNo).AlterAvgCostPerMeal(AmountOfChange)
        End If
    End Sub

    Private Sub DisplayEventsAtDayEnd()
        Console.WriteLine(Environment.NewLine & "***********************")
        Console.WriteLine("*****   Events:   *****")
        Console.WriteLine("***********************" & Environment.NewLine)
        Dim EventRanNo As Single
        EventRanNo = Rnd()
        If EventRanNo < 0.25 Then
            EventRanNo = Rnd()
            If EventRanNo < 0.25 Then
                ProcessAddHouseholdsEvent()
            End If
            EventRanNo = Rnd()
            If EventRanNo < 0.5 Then
                ProcessCostOfFuelChangeEvent()
            End If
            EventRanNo = Rnd()
            If EventRanNo < 0.5 Then
                ProcessReputationChangeEvent()
            End If
            EventRanNo = Rnd()
            If EventRanNo >= 0.5 Then
                ProcessCostChangeEvent()
            End If
        Else
            Console.WriteLine("No events.")
        End If
    End Sub

    Public Sub ProcessDayEnd()
        Dim TotalReputation As Single = 0
        Dim Reputations As New ArrayList
        Dim CompanyRNo, Current, LoopMax, X, Y As Integer
        For Each C In Companies
            C.NewDay()
            TotalReputation += C.GetReputationScore()
            Reputations.Add(TotalReputation)
        Next
        LoopMax = SimulationSettlement.GetNumberOfHouseholds() - 1
        For Counter = 0 To LoopMax
            If SimulationSettlement.FindOutIfHouseholdEatsOut(Counter, X, Y) Then
                CompanyRNo = Int(Rnd() * (Int(TotalReputation) + 1))
                Current = 0
                While Current < Reputations.Count
                    If CompanyRNo < Reputations(Current) Then
                        Companies(Current).AddVisitToNearestOutlet(X, Y)
                        Exit While
                    End If
                    Current += 1
                End While
            End If
        Next
        DisplayCompaniesAtDayEnd()
        DisplayEventsAtDayEnd()
    End Sub

    Public Sub AddCompany()
        Dim Balance, X, Y As Integer
        Dim CompanyName, TypeOfCompany As String
        Console.Write("Enter a name for the company: ")
        CompanyName = Console.ReadLine()
        Console.Write("Enter the starting balance for the company: ")
        Balance = Console.ReadLine()
        Do
            Console.Write("Enter 1 for a fast food company, 2 for a family company or 3 for a named chef company: ")
            TypeOfCompany = Console.ReadLine()
        Loop Until TypeOfCompany = "1" Or TypeOfCompany = "2" Or TypeOfCompany = "3"
        If TypeOfCompany = "1" Then
            TypeOfCompany = "fast food"
        ElseIf TypeOfCompany = "2" Then
            TypeOfCompany = "family"
        Else
            TypeOfCompany = "named chef"
        End If
        SimulationSettlement.GetRandomLocation(X, Y)
        Dim NewCompany As New Company(CompanyName, TypeOfCompany, Balance, X, Y, FuelCostPerUnit, BaseCostForDelivery)
        Companies.Add(NewCompany)
    End Sub

    Public Function GetIndexOfCompany(ByVal CompanyName As String) As Integer
        Dim Index As Integer = -1
        For Current = 0 To Companies.Count - 1
            If Companies(Current).GetName().ToLower() = CompanyName.ToLower() Then
                Return Current
            End If
        Next
        Return Index
    End Function

    Public Sub ModifyCompany(ByVal Index As Integer)
        Dim Choice As String = ""
        Dim OutletIndex, X, Y As Integer
        Dim CloseCompany As Boolean
        While Choice <> "C" And Choice <> "1" And Choice <> "2" And Choice <> "3"
            Console.WriteLine(Environment.NewLine & "*********************************")
            Console.WriteLine("*******  MODIFY COMPANY   *******")
            Console.WriteLine("*********************************")
            Console.WriteLine("1. Open new outlet")
            Console.WriteLine("2. Close outlet")
            Console.WriteLine("3. Expand outlet")
            Console.Write(Environment.NewLine & "Enter your choice: ")
            Choice = Console.ReadLine()
            Console.WriteLine()
            If Choice = "2" Or Choice = "3" Then
                Console.Write("Enter ID of outlet: ")
                OutletIndex = Console.ReadLine()
                If OutletIndex > 0 And OutletIndex <= Companies(Index).GetNumberOfOutlets() Then
                    If Choice = "2" Then
                        CloseCompany = Companies(Index).CloseOutlet(OutletIndex - 1)
                        If CloseCompany Then
                            Console.WriteLine("That company has now closed down as it has no outlets.")
                            Companies.RemoveAt(Index)
                        End If
                    Else
                        Companies(Index).ExpandOutlet(OutletIndex - 1)
                    End If
                Else
                    Console.WriteLine("Invalid outlet ID.")
                End If
            ElseIf Choice = "1" Then
                Console.Write("Enter X coordinate for new outlet: ")
                X = Console.ReadLine()
                Console.Write("Enter Y coordinate for new outlet: ")
                Y = Console.ReadLine()
                If X >= 0 And X < SimulationSettlement.GetXSize() And Y >= 0 And Y < SimulationSettlement.GetYSize() Then
                    Companies(Index).OpenOutlet(X, Y)
                Else
                    Console.WriteLine("Invalid coordinates.")
                End If
            ElseIf Choice = "C" Then
                Console.WriteLine("Operation Cancelled")
            End If
        End While
        Console.WriteLine()
    End Sub

    Public Sub DisplayCompanies()
        Console.WriteLine(Environment.NewLine & "*********************************")
        Console.WriteLine("*** Details of all companies: ***")
        Console.WriteLine("*********************************" & Environment.NewLine)
        For Each C In Companies
            Console.WriteLine(C.GetDetails() & Environment.NewLine)
        Next
        Console.WriteLine()
    End Sub

    Public Sub Run()
        Dim Choice As String = ""
        Dim Index As Integer
        While Choice <> "Q"
            DisplayMenu()
            Choice = Console.ReadLine()
            Select Case Choice
                Case "1"
                    SimulationSettlement.DisplayHouseholds()
                Case "2"
                    DisplayCompanies()
                Case "3"
                    Dim CompanyName As String
                    Do
                        Console.Write("Enter company name: ")
                        CompanyName = Console.ReadLine()
                        Index = GetIndexOfCompany(CompanyName)
                    Loop Until Index <> -1
                    ModifyCompany(Index)
                Case "4"
                    AddCompany()
                Case "6"
                    ProcessDayEnd()
                Case "Q"
                    Console.WriteLine("Simulation finished, press Enter to close.")
                    Console.ReadLine()
            End Select
        End While
    End Sub
End Class

Module Module1
    Sub Main()
        Randomize()
        Dim ThisSim As New Simulation
        ThisSim.Run()
    End Sub
End Module