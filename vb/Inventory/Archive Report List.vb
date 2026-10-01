Public Class Archive_Report_List

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        With Archive
            .ShowDialog()
        End With
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        With New_Archieve
            .ShowDialog()
        End With
    End Sub
End Class