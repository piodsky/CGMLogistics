Public Class PO_Choices

    Private Sub btnGF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGF.Click
        With Add_Purchase_Order
            .PO_Status.Text = "Add"

            .category = "PO"
            .ShowDialog()
            .Focus()
        End With

        Me.Close()
    End Sub

    Private Sub btnTF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTF.Click
        With Add_Purchase_Order
            .PO_Status.Text = "Add"
         
            .category = "TF"
            .ShowDialog()
            .Focus()
        End With

        Me.Close()
    End Sub

    Private Sub btnSEF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSEF.Click
        With Add_Purchase_Order
            .PO_Status.Text = "Add"
          
            .category = "SEF"
            .ShowDialog()
            .Focus()
        End With
        
        Me.Close()
    End Sub

    Private Sub PO_Choices_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class