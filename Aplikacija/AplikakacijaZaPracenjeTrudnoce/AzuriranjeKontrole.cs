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
    public partial class AzuriranjeKontrole : Form
    {
        public string spajanje = null;
        public int id_kontrola = 0;
        public AzuriranjeKontrole(string conString, int id_kontrole)
        {
            InitializeComponent();
            spajanje = conString;
            id_kontrola = id_kontrole;
        }
        public NpgsqlConnection konekcija;
        private NpgsqlCommand naredba;
        private string sql = null;

        private void buttonAzuriraj_Click(object sender, EventArgs e)
        { 
            if (textBoxKCS.Text != null && textBoxDT.Text != null && textBoxUZV.Text != null && textBoxST.Text != null &&
                textBoxTT.Text != null)
            {
                if (textBoxUZV.Text != null)
                {
                    konekcija = new NpgsqlConnection(spajanje);
                    konekcija.Open();
                    NpgsqlCommand naredba = new NpgsqlCommand("update kontrola SET uzv =@uzv, tt =@tt, sistolicki_tlak =@sistolicki_tlak, dijastolicki_tlak =@dijastolicki_tlak, kcs =@kcs, ostali_pregledi =@ostali_pregledi WHERE id_kontrola = @id_kontrola", konekcija);
                    naredba.Parameters.Add(new NpgsqlParameter("@uzv", textBoxUZV.Text));
                    naredba.Parameters.Add(new NpgsqlParameter("@tt", int.Parse(textBoxTT.Text)));
                    naredba.Parameters.Add(new NpgsqlParameter("@sistolicki_tlak", int.Parse(textBoxST.Text)));
                    naredba.Parameters.Add(new NpgsqlParameter("@dijastolicki_tlak", int.Parse(textBoxDT.Text)));
                    naredba.Parameters.Add(new NpgsqlParameter("@kcs", int.Parse(textBoxKCS.Text)));
                    naredba.Parameters.Add(new NpgsqlParameter("@ostali_pregledi", textBoxOstali.Text));
                    naredba.Parameters.Add(new NpgsqlParameter("@id_kontrola", id_kontrola));
                    naredba.ExecuteNonQuery();
                    konekcija.Close();
                    this.Close();
                    MessageBox.Show("Uspješno ste ažurirali kontrolu.");
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Sva polja nisu popunjena, molimo popunite sva polja!");
            }
        }

        private void AzuriranjeKontrole_Load(object sender, EventArgs e)
        {
            UcitavanjePodatkaOKontroli();
        }
        private void UcitavanjePodatkaOKontroli()
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();

            sql = $"select uzv, tt, sistolicki_tlak, dijastolicki_tlak, kcs, ostali_pregledi from kontrola where id_kontrola = {id_kontrola}";
            naredba = new NpgsqlCommand(sql, konekcija);
            NpgsqlDataReader npgsqlDataReader = naredba.ExecuteReader();
            while (npgsqlDataReader.Read())
            {
                textBoxDT.Text = npgsqlDataReader["dijastolicki_tlak"].ToString();
                textBoxKCS.Text = npgsqlDataReader["kcs"].ToString();
                textBoxOstali.Text = npgsqlDataReader["ostali_pregledi"].ToString();
                textBoxST.Text = npgsqlDataReader["sistolicki_tlak"].ToString();
                textBoxTT.Text = npgsqlDataReader["tt"].ToString();
                textBoxUZV.Text = npgsqlDataReader["uzv"].ToString();
            }
            npgsqlDataReader.Close();
            konekcija.Close();
        }
    }
}
