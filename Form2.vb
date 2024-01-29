Imports MySql.Data.MySqlClient
Public Class Form2
    Dim strKon As String = "server=localhost; uid=root; database=praktikum3"
    Dim kon As New MySqlConnection(strKon)
    Dim perintah As New MySqlCommand
    Dim mda As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim biayaservis As Double

    Sub tampilData(ByVal sql As String)
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = sql
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "data")
        DataGridView1.DataSource = ds.Tables("data")
        kon.Close()
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        DateTimePicker1.Value = Now
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        Label8.Text = "0"
        TextBox2.Text = ""
        Label9.Text = "0"

    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from pelanggan"
        dt.Load(perintah.ExecuteReader)
        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "nama"
        ComboBox1.ValueMember = "idpel"
        kon.Close()

        Call tampilData("select noservis, tglservis, nama, jenisservis, biayaservis, biayapart, total from servis join pelanggan on servis.idpel = pelanggan.idpel")

        ComboBox2.Items.Add("SERVIS CVT")
        ComboBox2.Items.Add("SERVIS BODY")
        ComboBox2.Items.Add("SERVIS RINGAN")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tanggal, idpel As String
        tanggal = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        idpel = ComboBox1.SelectedValue.ToString
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "insert into servis values('" & TextBox1.Text & "', '" & tanggal & "', '" & idpel & "', '" & ComboBox2.Text & "', '" & Label8.Text & "', '" & TextBox2.Text & "', '" & Label9.Text & "')"
        perintah.ExecuteNonQuery()
        bersih()
        kon.Close()

        Call tampilData("select noservis, tglservis, nama, jenisservis, biayaservis, biayapart, total from servis join pelanggan on servis.idpel = pelanggan.idpel")

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Dim Biaya As Double
        Dim BiayaPart As Double
        Dim total As Double

        Biaya = Val(Label8.Text)
        BiayaPart = Val(TextBox2.Text)

        total = (Biaya + BiayaPart)
        Label9.Text = total
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "SERVIS CVT" Then
            Label8.Text = 35000
        Else
            If ComboBox2.Text = "SERVIS BODY" Then
                Label8.Text = 40000
            Else
                If ComboBox2.Text = "SERVIS RINGAN" Then
                    Label8.Text = 400000
                End If
                Label8.Text = 40000
            End If
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

        Call tampilData("select noservis, tglservis, nama, jenisservis,  biayaservis, biayapart, total from servis join pelanggan on servis.idpel = pelanggan.idpel where nama like '%" & TextBox3.Text & "%'")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub
End Class