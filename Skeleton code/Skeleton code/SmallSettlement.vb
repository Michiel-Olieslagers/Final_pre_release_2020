Class SmallSettlement 'added for small class settlement
    Inherits Settlement

    Public Sub New(ByVal ReduceXBy As Integer, ByVal ReduceYBy As Integer, ByVal ReduceHousingBy As Integer)
        Me.XSize = 1000 - ReduceXBy
        Me.YSize = 1000 - ReduceYBy
        Me.StartNoOfHouseholds = 250 - ReduceHousingBy
        For count = 1 To (StartNoOfHouseholds + 1)
            Me.AddHousehold()
        Next

    End Sub
End Class