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
    public partial class LijecnikPocetnaForma : Form
    {
        public string spajanje = null;
        public int id_lijecnik = 1;
        public LijecnikPocetnaForma(string conString)
        {
            InitializeComponent();
            spajanje = conString;
        }
        public NpgsqlConnection konekcija;
        private NpgsqlCommand naredba;
        private DataTable tablica;
        private string sql = null;
        private void LijecnikPocetnaForma_Load(object sender, EventArgs e)
        {
            UcitavanjePodatakaOLijecniku();
            UcitivanjePodatakaOTrudnicama();
        }
        private void UcitavanjePodatakaOKontrolama()
        {
            if (OdabirTrudnica() != 0)
            {
                konekcija = new NpgsqlConnection(spajanje);
                konekcija.Open();
                dataGridViewKontrole.DataSource = null;

                sql = $"select * from kontrola where fk_trudnica = {OdabirTrudnica()}";
                naredba = new NpgsqlCommand(sql, konekcija);
                tablica = new DataTable();
                tablica.Load(naredba.ExecuteReader());

                dataGridViewKontrole.DataSource = tablica;
                dataGridViewKontrole.Columns[0].Visible = false;
                dataGridViewKontrole.Columns[8].Visible = false;
                konekcija.Close();
            }
        }
        private void UcitavanjePodatakaOLijecniku()
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();

            sql = $"select ime, prezime, adresa, broj_telefona from lijecnik where id_lijecnik = {id_lijecnik}";
            naredba = new NpgsqlCommand(sql, konekcija);
            NpgsqlDataReader npgsqlDataReader = naredba.ExecuteReader();
            while (npgsqlDataReader.Read())
            {
                textBoxIme.Text = npgsqlDataReader["ime"].ToString();
                textBoxPrezime.Text = npgsqlDataReader["prezime"].ToString();
                textBoxMob.Text = npgsqlDataReader["broj_telefona"].ToString();
                textBoxAdresa.Text = npgsqlDataReader["adresa"].ToString();
            }
            npgsqlDataReader.Close();
            konekcija.Close();
        }
        private void UcitivanjePodatakaOTrudnicama()
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            dataGridViewTrudnice.DataSource = null;

            sql = $"select * from trudnica where fk_lijecnik = {id_lijecnik}";
            naredba = new NpgsqlCommand(sql, konekcija);
            tablica = new DataTable();
            tablica.Load(naredba.ExecuteReader());
            
            dataGridViewTrudnice.DataSource = tablica;
            dataGridViewTrudnice.Columns[0].Visible = false;
            dataGridViewTrudnice.Columns[1].Visible = false;
            dataGridViewTrudnice.Columns[5].Visible = false;
            dataGridViewTrudnice.Columns[7].Visible = false;
            dataGridViewTrudnice.Columns[10].Visible = false;
            dataGridViewTrudnice.Columns[12].Visible = false;
            konekcija.Close();
        }

        private void buttonUnesi_Click(object sender, EventArgs e)
        {
            int id = OdabirTrudnica();
            if (id != 0)
            {
                UnosTrudnice formaUnosTrudnice = new UnosTrudnice(spajanje);
                formaUnosTrudnice.ShowDialog();
                UcitivanjePodatakaOTrudnicama();
            }
        }

        
         private int OdabirTrudnica()
         {
             string id_trudnica = null;
             int indeks = dataGridViewTrudnice.SelectedRows[0].Index;
             if (indeks != -1)
             {
                 id_trudnica = dataGridViewTrudnice.SelectedRows[0].Cells["id_trudnica"].Value.ToString();
             }
             return int.Parse(id_trudnica); 
         }
        private int OdabirKontrole()
        {
            string id_kontrola = null;
            int indeks = dataGridViewKontrole.SelectedRows[0].Index;
            if (indeks != -1)
            {
                id_kontrola = dataGridViewKontrole.SelectedRows[0].Cells["id_kontrola"].Value.ToString();
            }
            return int.Parse(id_kontrola);
        }

        private void dataGridViewTrudnice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OdabirTrudnica() != 0)
            {
                UcitavanjePodatakaOKontrolama();
                dataGridViewLabNalazi.DataSource = null;
            }
        }

        private void UcitavanjePodatkaONalazima()
        {
                konekcija.Open();
                dataGridViewLabNalazi.DataSource = null;
                 if (OdabirKontrole() != 0)
            {

                    sql = $"select * from laboratorijski_nalazi where fk_kontrola = {OdabirKontrole()}";
                    naredba = new NpgsqlCommand(sql, konekcija);
                    naredba.ExecuteNonQuery();
                    tablica = new DataTable();
                    tablica.Load(naredba.ExecuteReader());
                    dataGridViewLabNalazi.DataSource = tablica;
                    dataGridViewLabNalazi.Columns[0].Visible = false;
                    dataGridViewLabNalazi.Columns[5].Visible = false;
                    konekcija.Close();
             }

        }

        private void dataGridViewKontrole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OdabirKontrole() != 0)
            {
                UcitavanjePodatkaONalazima();
            }
        }

        private void buttonUnesiNalaz_Click(object sender, EventArgs e)
        {
            int id = OdabirKontrole();
            if (id != 0)
            {
                UnosLaboratorijskihNalaza formaLabNalazi = new UnosLaboratorijskihNalaza(spajanje, id);
                formaLabNalazi.ShowDialog();
                UcitavanjePodatakaOKontrolama();
                dataGridViewLabNalazi.DataSource = null;    
            }

        }

        private void buttonNovaTrudnica_Click(object sender, EventArgs e)
        {
            UnosTrudnice formaUnosTrudnice = new UnosTrudnice(spajanje);
            formaUnosTrudnice.ShowDialog();
            UcitivanjePodatakaOTrudnicama();
            dataGridViewKontrole.DataSource = null;
            dataGridViewLabNalazi.DataSource = null;
        }

 

        private void buttonObrisiTrudnicu_Click(object sender, EventArgs e)
        {
            if (OdabirTrudnica() != 0)
            {
                konekcija = new NpgsqlConnection(spajanje);
                konekcija.Open();
                sql = $"delete from trudnica where id_trudnica = {OdabirTrudnica()}";
                naredba = new NpgsqlCommand(sql, konekcija);
                naredba.ExecuteNonQuery();
                konekcija.Close();
                UcitivanjePodatakaOTrudnicama();
                dataGridViewKontrole.DataSource = null;
                dataGridViewLabNalazi.DataSource = null;
            }
        }

        private void buttonAzuriraj_Click(object sender, EventArgs e)
        {
            int id = OdabirTrudnica();
            if (id != 0)
            {
                AzuriranjeTrudnice formaAzuriranje = new AzuriranjeTrudnice(spajanje, id);
                formaAzuriranje.ShowDialog();
                UcitivanjePodatakaOTrudnicama();
                dataGridViewKontrole.DataSource = null;
                dataGridViewLabNalazi.DataSource = null;
            }
        }

        private void buttonObrisiKontrolu_Click(object sender, EventArgs e)
        {
            if (OdabirKontrole() != 0)
            {
                string uzv = Convert.ToString(dataGridViewKontrole.SelectedRows[0].Cells["uzv"].Value);
                if (uzv == "") {
                    konekcija = new NpgsqlConnection(spajanje);
                    konekcija.Open();
                    sql = $"delete from kontrola where id_kontrola = {OdabirKontrole()}";
                    naredba = new NpgsqlCommand(sql, konekcija);
                    naredba.ExecuteNonQuery();
                    konekcija.Close();
                    UcitivanjePodatakaOTrudnicama();
                    dataGridViewKontrole.DataSource = null;
                    dataGridViewLabNalazi.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Za tu kontrolu već postoji podatak o UZV, nije ju moguće izbrisati.");
                }
            }
        }

        private void buttonAzurirajKontrolu_Click(object sender, EventArgs e)
        {
            int id = OdabirKontrole();
            if (id != 0)
            {
                AzuriranjeKontrole formaAzuriranje = new AzuriranjeKontrole(spajanje, id);
                formaAzuriranje.ShowDialog();
                UcitivanjePodatakaOTrudnicama();
                dataGridViewKontrole.DataSource = null;
                dataGridViewLabNalazi.DataSource = null;
            }
        }

        private void buttonUnesiDatumKontrole_Click(object sender, EventArgs e)
        {
            int id = OdabirTrudnica();
            if (id != 0)
            {
                     try
                    {
                        DateTime datum = dateTimePickerKontrola.Value;
                        DateTime.TryParse(dateTimePickerKontrola.Value.ToString(), out datum);
                        if (dateTimePickerKontrola.Value != null)
                        {
                            konekcija = new NpgsqlConnection(spajanje);
                            konekcija.Open();
                            NpgsqlCommand naredba = new NpgsqlCommand("insert into kontrola ( datum_pregleda, fk_trudnica) values( @datum_pregleda, @fk_trudnica )", konekcija);
                            naredba.Parameters.Add(new NpgsqlParameter("@datum_pregleda", datum));
                            naredba.Parameters.Add(new NpgsqlParameter("@fk_trudnica", id));
                            naredba.ExecuteNonQuery();
                            konekcija.Close();
                            MessageBox.Show("Uspješno ste unijeli novu kontrolu.");
                            UcitivanjePodatakaOTrudnicama();
                            dataGridViewKontrole.DataSource = null;
                            dataGridViewLabNalazi.DataSource = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    
}


