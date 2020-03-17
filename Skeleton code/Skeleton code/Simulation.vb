Class Simulation
    Protected SimulationSettlement As Settlement
    Protected NoOfCompanies As Integer
    Protected FuelCostPerUnit, BaseCostForDelivery As Single
    Protected Companies As New ArrayList
    Protected Households As New ArrayList
    Private brexit As Boolean = False
    'NOTE: FormatCurrency(CalculateDeliveryCost.ToString, 2) CONVERTS INTO CURRENCY FORMAT
    Public Sub New()
        FuelCostPerUnit = 0.0098
        BaseCostForDelivery = 100
        Dim Choice As String
        Console.Write("Enter L for a large settlement, S for a small settlement, anything else for a normal size settlement: ") 'add option for small settlement
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
        ElseIf Choice = "S" Then 'added else if for small settlement
            Dim reducexby As Integer = 1001
            Dim reduceyby As Integer = 1001
            Dim reducehousingby As Integer = 251
            While reducexby >= 1000 Or reduceyby >= 1000 Or reducehousingby >= 250
                Console.WriteLine("X & Y cannot be reduced by more than 999 and Housing cannot be reduced by more than 249.")
                Console.WriteLine("Please enter how much you would like to reduce the X size by: ")
                reducexby = Console.ReadLine()
                Console.WriteLine("Please enter how much you would like to reduce the Y size by: ")
                reduceyby = Console.ReadLine()
                Console.WriteLine("Please enter how much you would like to reduce the number of houses by: ")
                reducehousingby = Console.ReadLine()
            End While
            SimulationSettlement = New SmallSettlement(reducexby, reduceyby, reducehousingby)
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
        Console.WriteLine("5. Remove a company") 'added
        Console.WriteLine("6. Advance to next day")
        Console.WriteLine("7. Change reputation") 'added to change rep of a company
        Console.WriteLine("8. Process multiple days") 'added to process multiple days
        Console.WriteLine("9. Merge companies") 'added to process multiple days
        Console.WriteLine("Q. Quit")
        Console.Write(Environment.NewLine & "Enter your choice: ")
    End Sub
    Public Sub ChangeReputation() 'added to change rep of a company
        Dim companyName As String
        Dim index As Integer
        Dim rate As Char
        Console.Write("Enter company name: ")
        companyName = Console.ReadLine()
        index = GetIndexOfCompany(companyName)
        Console.WriteLine("How do you rate the company - good(g) or bad(b)?")
        rate = Console.ReadLine()
        If (rate = "g") Then
            Companies(index).AlterReputation(3)
        Else
            Companies(index).AlterReputation(-3)
        End If
    End Sub
    Public Sub MergeCompanies() 'added to merge 2 companies (!!!need help with how to store all the outlets!!!!)
        Console.WriteLine("\n******* Merge Companies *********\n")
        For i = 0 To Companies.Count - 1
            Console.WriteLine(i + 1 & " " & Companies(i).GetName())
        Next
        Dim compOne As String
        Dim compTwo As String
        Console.WriteLine("Enter number of company")
        compOne = Console.ReadLine()
        Dim companyStoreOne As Company
        companyStoreOne = Companies(compOne - 1)
        Console.WriteLine()
        For i = 0 To Companies.Count - 1
            If (i <> compOne) Then
                Console.WriteLine(i + 1 & " " & Companies(i).GetName())
            End If
        Next
        Console.WriteLine("Enter number of company")
        compTwo = Console.ReadLine()
        Dim companyStoreTwo As Company
        companyStoreTwo = Companies(compTwo - 1)
        If (compOne > compTwo) Then
            Companies.RemoveAt(compOne)
            Companies.RemoveAt(compTwo)
        Else
            Companies.RemoveAt(compTwo)
            Companies.RemoveAt(compOne)
        End If
        Dim mergeCompName As String
        Console.WriteLine("Enter a new name: ")
        mergeCompName = Console.ReadLine()
        Dim comBal As Single = companyStoreOne.GetBalance() + companyStoreTwo.GetBalance()
        Dim comRep As Single
        If (companyStoreOne.GetReputationScore() > companyStoreTwo.GetReputationScore()) Then
            comRep = companyStoreOne.GetReputationScore()
        Else
            comRep = companyStoreTwo.GetReputationScore()
        End If
        Dim comAvgCost As Single = (companyStoreOne.GetAvgCostPerMeal() + companyStoreTwo.GetAvgCostPerMeal()) / 2
        Dim comAvgPrice As Single = (companyStoreOne.GetAvgPricePerMeal() + companyStoreTwo.GetAvgPricePerMeal()) / 2
        Dim comDaily As Single = (companyStoreOne.GetDailyCosts() + companyStoreTwo.GetDailyCosts()) / 2
        Dim comFuel As Single = (companyStoreOne.GetFuelCostPerUnit() + companyStoreTwo.GetFuelCostPerUnit()) / 2
        Dim comBase As Single = (companyStoreOne.getBaseCostOfDelivery() + companyStoreTwo.getBaseCostOfDelivery()) / 2
        'how do you add together outlets
        Dim compOutletsOne As ArrayList = companyStoreOne.getOutlets()
        Dim compOutletsTwo As ArrayList = companyStoreTwo.getOutlets()
        Dim newCom = New Company(mergeCompName, companyStoreOne.getCategory(), comBal, compOutletsOne(0).GetX(), compOutletsOne(0).GetY(), comFuel, comBase)
        For i = 1 To companyStoreOne.getOutlets.Count - 1
            newCom.OpenOutlet(compOutletsOne(i).GetX(), compOutletsOne(i).GetY())
        Next
        For i = 0 To companyStoreTwo.getOutlets.Count - 1
            newCom.OpenOutlet(compOutletsTwo(i).GetX(), compOutletsTwo(i).GetY())
        Next
        Companies.Add(newCom)
    End Sub
    Private Sub DisplayCompaniesAtDayEnd()
        Dim Details As String
        Dim BankRupt As Boolean
        Dim Index(10) As Integer
        ' Index(0) = 0
        Dim count As Integer = 0
        Console.WriteLine(Environment.NewLine & "**********************")
        Console.WriteLine("***** Companies: *****")
        Console.WriteLine("**********************" & Environment.NewLine)
        For Each C In Companies
            Console.WriteLine(C.GetName())
            Console.WriteLine()
            Details = C.ProcessDayEnd()
            Console.WriteLine(Details & Environment.NewLine)
            BankRupt = C.processGoBankruptEvent()
            If BankRupt = True Then
                Index(count) = GetIndexOfCompany(C.GetName())
                count = count + 1
            End If
        Next
        For x = 0 To count - 1
            Dim IndexA As Integer
            IndexA = Index(x)
            Companies.RemoveAt(IndexA)
        Next
    End Sub
    Public Function processGoBankruptEvent() 'added for bankrupt evenCalculat
        Dim goneBankrupt = False
        For i = 0 To Companies.Count - 1
            If Companies(i).isBankrupt(Companies(i).getBalance) Then
                Console.WriteLine(Companies(i).getName & " has gone bankrupt and is closing")
                Companies.Remove(i)
                i = i - 1
                goneBankrupt = True
            End If
        Next
        Return goneBankrupt
    End Function

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
        If UpOrDown = 1 Then 'altered and added 'Add in a separate if statement to change the value of the upordown to 0 if the current fuelcost change is going to result in a negative fuelcostchange
            If Me.Companies(CompanyNo).GetFuelCostPerUnit() < FuelCostChange Then
                UpOrDown = 0
            End If
        End If
        If UpOrDown = 0 Then 'altered and added 'Add in a separate if statement to change the value of the upordown to 0 if the current fuelcost change is going to result in a negative fuelcostchange
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
                Console.WriteLine("The daily costs for " & Companies(CompanyNo).GetName() & " have gone up by " & FormatCurrency(AmountOfChange.ToString, 2))
            Else
                Console.WriteLine("The daily costs for " & Companies(CompanyNo).GetName() & " have gone down by " & FormatCurrency(AmountOfChange.ToString, 2))
                AmountOfChange *= -1
            End If
            Companies(CompanyNo).AlterDailyCosts(AmountOfChange)
        Else
            AmountOfChange = Int((Rnd() * 9) + 1) / 10
            If UpOrDown = 0 Then
                Console.WriteLine("The average cost of a meal for " & Companies(CompanyNo).GetName() & " has gone up by " & FormatCurrency(AmountOfChange.ToString, 2))
            Else
                Console.WriteLine("The average cost of a meal for " & Companies(CompanyNo).GetName() & " has gone down by " & FormatCurrency(AmountOfChange.ToString, 2))
                AmountOfChange *= -1
            End If
            Companies(CompanyNo).AlterAvgCostPerMeal(AmountOfChange)
        End If
    End Sub

    Private Sub DisplayEventsAtDayEnd()
        Dim hasEvent As Boolean = False 'added for bankrupty
        Console.WriteLine(Environment.NewLine & "***********************")
        Console.WriteLine("*****   Events:   *****")
        Console.WriteLine("***********************" & Environment.NewLine)
        Dim EventRanNo As Single
        EventRanNo = Rnd()
        If EventRanNo < 0.25 Then
            EventRanNo = Rnd()
            If EventRanNo < 0.25 Then
                ProcessAddHouseholdsEvent()
                hasEvent = True
            End If
            EventRanNo = Rnd()
            If EventRanNo < 0.5 Then
                ProcessCostOfFuelChangeEvent()
                hasEvent = True
            End If
            EventRanNo = Rnd()
            If EventRanNo < 0.5 Then
                ProcessReputationChangeEvent()
                hasEvent = True
            End If
            EventRanNo = Rnd()
            If EventRanNo >= 0.5 Then
                ProcessCostChangeEvent()
                hasEvent = True
            End If
            EventRanNo = Rnd() 'added for power outage event happening
            If EventRanNo < 0.1 Then 'added for power outage event happening
                ProcessPowerOutage()
                hasEvent = True
            End If
            EventRanNo = Rnd() 'added for end of the world event happening
            If EventRanNo < 0.1 Then 'added for end of the world event happening
                Dropworld()
            End If
            'If (processGoBankruptEvent() = True) Then 'added for bankrupty !!check!!
            '    hasEvent = True
            'End If
            EventRanNo = Rnd() 'added for result of brexit destroying the population
            If EventRanNo < 0.1 Then 'added for result of brexit destroying the population
                ProcessBrexit()
            End If
            If (hasEvent = False) Then 'added for bankrupty !!check!!
                Console.WriteLine("No events.")
            End If
        Else
            Console.WriteLine("No events.")
        End If
    End Sub
    Sub Dropworld() 'added for end of the world event happening
        Console.WriteLine("Darkrai used black hole eclipse. A quadrillion quadrillion joules of energy was released in two seconds. The world is gone")
        Stop
    End Sub
    Protected Sub ProcessBrexit() 'added cuz brexit
        If (brexit = False) Then
            For Each H In Households
                Console.WriteLine(H.ChangeChanceEatOut(0.3))
            Next
            Console.WriteLine("Brexit has reduced how often households will eat out")
            brexit = True
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
        Dim checkName As Boolean = True 'Added this variable as a condition for your while loop to stop you from continuing with a name already in use
        Dim Balance, X, Y As Integer
        Dim CompanyName, TypeOfCompany As String
        Dim name As String 'Use the name variable as a holder for the name you're checking your chosen name against in the ArrayList
        While checkName = True 'Add in a while loop to check through the ArrayList of company names to check if the chosen name has already been used
            Console.Write("Enter a name for the company: ")
            CompanyName = Console.ReadLine()
            checkName = False
            For i = 0 To Me.Companies.Count - 1
                name = Me.Companies(i).GetName()
                If CompanyName = name Then
                    Console.WriteLine("Company name is already in use")
                    checkName = True
                End If
            Next
        End While
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
    Public Sub RemoveCompany() 'added to remove a company
        Dim CompanyToRemove As String
        Dim Index As String
        Console.WriteLine("Enter the name of the company you would like to remove: ")
        CompanyToRemove = Console.ReadLine()
        Try
            Index = GetIndexOfCompany(CompanyToRemove)
            Companies.RemoveAt(Index)
            Console.WriteLine()
            Console.WriteLine(CompanyToRemove & " removed!")
            Console.WriteLine()
        Catch
            Console.WriteLine()
            Console.WriteLine("Company: " & CompanyToRemove & " doesn't exist")
            Console.WriteLine()
        End Try
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
        Dim Choice As String
        Dim OutletIndex, X, Y As Integer
        Dim CloseCompany As Boolean
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
        End If
        Console.WriteLine()
    End Sub
    Public Sub ProcessPowerOutage() 'added for power outage event happening
        Randomize()
        Dim noOfDays As Integer = Int((Rnd() * 7) + 1)
        Dim moneyLost As Integer = Int((Rnd() * 1000) + 1)
        Dim totalLoss As Integer
        Dim CompanyNo As Integer = Int(Rnd() * Companies.Count)
        Console.WriteLine(Environment.NewLine & Companies(CompanyNo).GetName() & " is experiencing a powercut:")
        Console.WriteLine(Environment.NewLine & Companies(CompanyNo).GetName() & ". Balance: £" & Companies(CompanyNo).getBalance())
        For i = 1 To noOfDays
            Companies(CompanyNo).alterbalance(moneyLost)
            totalLoss += moneyLost
            Console.WriteLine(Environment.NewLine & "Hour: " & i)
            Console.WriteLine(Companies(CompanyNo).GetName() & " has lost £" & totalLoss & " in " & i & " hours.")
            Console.WriteLine(Companies(CompanyNo).GetName() & ". Balance: £" & Companies(CompanyNo).getBalance())
        Next
        Console.WriteLine(Environment.NewLine & Companies(CompanyNo).GetName() & " has lost £" & totalLoss & " in total." & Environment.NewLine)
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
    Public Sub ProcessMultipleDays() 'added to process multiple days in advance
        Dim noOfDays As Integer
        Try
            Console.Write("How many days do you want to advance? ")
            noOfDays = Console.ReadLine
            For i = 1 To noOfDays
                ProcessDayEnd()
            Next
        Catch ex As Exception
            Console.WriteLine("")
            Console.WriteLine("Invalid Input")
            Console.WriteLine("")
        End Try
    End Sub
    Public Sub Run()
        Dim daysCount As Integer = 0 'Add new variable that will be the counter of days
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
                Case "5" 'added
                    RemoveCompany()
                Case "6"
                    ProcessDayEnd()
                    daysCount += 1 'Added: every time we advance 1 day, the counter increments by 1.
                    Console.WriteLine("Day: " & daysCount)
                Case "7" 'added to change rep of company
                    ChangeReputation()
                Case "8" 'added to process multiple days at once
                    ProcessMultipleDays()
                Case "9" 'added to process multiple days at once
                    MergeCompanies()
                Case "Q"
                    Console.WriteLine("Simulation finished, press Enter to close.")
                    Console.ReadLine()
            End Select
        End While
    End Sub
End Class