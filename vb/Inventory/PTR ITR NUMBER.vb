Public Class PTR_ITR_NUMBER
    Public PTRICSnumbergenerated
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Close()
    End Sub

    Private Sub PTR_ITR_NUMBER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PTRITR_Number.Text = PTRICSnumbergenerated
    End Sub
End Class