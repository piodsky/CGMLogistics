Public Class Procurement_Reports_List

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        With PO_Reports
            .ShowDialog()
        End With
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIAR.Click
        With Inspection_Reports
            .ShowDialog()
        End With
    End Sub

    Private Sub btnPOIAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOIAR.Click
        With POIAR_Reports
            .ShowDialog()
        End With
    End Sub

    Private Sub btnPOIARItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOIARItems.Click
        With PO_Items
            .ShowDialog()
        End With
    End Sub
End Class