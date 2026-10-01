Public Class CGSO_NUMBER
    Public paricsnumbergenerated
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Close()
    End Sub

    Private Sub CGSO_NUMBER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PARICS_Number.Text = paricsnumbergenerated
    End Sub


  
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class