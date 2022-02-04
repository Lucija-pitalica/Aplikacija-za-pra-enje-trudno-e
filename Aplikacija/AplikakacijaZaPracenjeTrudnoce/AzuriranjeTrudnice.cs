using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikakacijaZaPracenjeTrudnoce
{
    public partial class AzuriranjeTrudnice : Form
    {
        public string spajanje = null;
        public int id_trudnica = 0;
        public AzuriranjeTrudnice(string conString, int id_trudnice)
        {
            InitializeComponent();
            spajanje = conString;
            id_trudnica = id_trudnice;
        }
        public NpgsqlConnection konekcija;
        private NpgsqlCommand naredba;
        private string sql = null;
        private void AžuriranjeTrudnice_Load(object sender, EventArgs e)
        {
            ucitavanjePodataka();
        }

        private void buttonAzuriraj_Click(object sender, EventArgs e)
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            if (textBoxAdresaTr.Text.Length != 0 && textBoxImeTr.Text.Length != 0 && textBoxPrezimeTr.Text.Length != 0
                && textBoxBrojMob.Text.Length != 0 && textBoxEmail.Text.Length != 0)
            { 
                sql = @"update trudnica set ime=@ime, prezime=@prezime, adresa=@adresa, broj_mobitela=@broj_mobitela, email=@email, spol_djeteta=@spol_djeteta WHERE id_trudnica = @id_trudnica";
                naredba = new NpgsqlCommand(sql, konekcija);
                naredba.Parameters.AddWithValue("@ime", textBoxImeTr.Text);
                naredba.Parameters.AddWithValue("@prezime", textBoxPrezimeTr.Text);
                naredba.Parameters.AddWithValue("@id_trudnica", id_trudnica);
                naredba.Parameters.AddWithValue("@adresa", textBoxAdresaTr.Text);
                naredba.Parameters.AddWithValue("@broj_mobitela", textBoxBrojMob.Text);
                naredba.Parameters.AddWithValue("@email", textBoxEmail.Text);
                naredba.Parameters.AddWithValue("@spol_djeteta", textBoxSpol.Text);
                naredba.ExecuteNonQuery();
                this.Close();
                konekcija.Close();
            }
            else
            {
                MessageBox.Show("Sva polja nisu popunjena, molimo popunite sva polja!");
            }
        }

        private void ucitavanjePodataka()
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();

            sql = $"select oib, ime, prezime, adresa, email, broj_mobitela, spol_djeteta  from trudnica where id_trudnica = {id_trudnica}";
            naredba = new NpgsqlCommand(sql, konekcija);
            NpgsqlDataReader npgsqlDataReader = naredba.ExecuteReader();
            while (npgsqlDataReader.Read())
            {
                textBoxOIB.Text = npgsqlDataReader["oib"].ToString();
                textBoxImeTr.Text = npgsqlDataReader["ime"].ToString();
                textBoxPrezimeTr.Text = npgsqlDataReader["prezime"].ToString();
                textBoxAdresaTr.Text = npgsqlDataReader["adresa"].ToString();
                textBoxEmail.Text = npgsqlDataReader["email"].ToString();
                textBoxBrojMob.Text = npgsqlDataReader["broj_mobitela"].ToString();
                textBoxSpol.Text = npgsqlDataReader["spol_djeteta"].ToString();
            }
            npgsqlDataReader.Close();
            konekcija.Close();
        }

        private void buttonObrisi_Click_1(object sender, EventArgs e)
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            sql = $"delete from trudnica where id_trudnica = {id_trudnica}";
            naredba = new NpgsqlCommand(sql, konekcija);
            naredba.ExecuteNonQuery();
            konekcija.Close();
            this.Hide();
        }
    }
}
