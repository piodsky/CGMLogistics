Public Class Inventory_Report_List

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        With Inventory_Reports
            .ShowDialog()
        End With
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        With Inventory__Management_Reports
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        With PRS_Report
            .ShowDialog()
        End With
    End Sub
End Class