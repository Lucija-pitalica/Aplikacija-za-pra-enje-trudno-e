using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikakacijaZaPracenjeTrudnoce
{
    public partial class Dnevnik : Form
    {
        public string spajanje = null;
        public int id_trudnica = 1;
        public Dnevnik(string conString)
        {
            InitializeComponent();
            spajanje = conString;
        }

        public NpgsqlConnection konekcija;
        private NpgsqlCommand naredba;
        private DataTable tablica;
        private string sql = null;
  
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            UnosenjeNovogZapisaUdnevnik();
            Osvjezi();
        }

        private void Osvjezi()
        {
            checkBoxMucnina.Checked = false;
            checkBoxSlabost.Checked = false;
            checkBoxZgaravica.Checked = false;
            checkBoxBol.Checked = false;
            comboBoxBroj.Visible = false;
            comboBoxBroj.Enabled = false;
            labelBroj.Visible = false;
            textBoxPrehrana.Text = "";
            textBoxSimptomi.Text = "";
        }

        private void UnosenjeNovogZapisaUdnevnik()
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            string mucnina = "DA";
            string slabost = "DA";
            string bol_u_zglobovima = "DA";
            string zgaravica = "DA";

            if (checkBoxMucnina.Checked == false) zgaravica = "NE";
            if (checkBoxSlabost.Checked == false) slabost = "NE";
            if (checkBoxBol.Checked == false) bol_u_zglobovima = "NE";
            if (checkBoxZgaravica.Checked == false) zgaravica = "NE";

            DateTime datum;
            DateTime.TryParse(DateTime.Now.ToString(), out datum);
            if ( textBoxPrehrana.Text.Length != 0)
            {
                try
                {
                    NpgsqlCommand naredba = new NpgsqlCommand("insert into dnevnik (mucnina,broj_puta_povracanja,slabost,zgaravica,opis_prehrane,bol_u_zglobovima,ostali_simptomi,fk_trudnica,datum) values(@mucnina,@broj_puta_povracanja,@slabost,@zgaravica,@opis_prehrane,@bol_u_zglobovima,@ostali_simptomi, @fk_trudnica,@dat)", konekcija);

                    naredba.Parameters.Add(new NpgsqlParameter("@mucnina", mucnina));
                    naredba.Parameters.Add(new NpgsqlParameter("@broj_puta_povracanja", int.Parse(comboBoxBroj.SelectedIndex.ToString()) + 1));
                    naredba.Parameters.Add(new NpgsqlParameter("@slabost", slabost));
                    naredba.Parameters.Add(new NpgsqlParameter("@zgaravica", zgaravica));
                    naredba.Parameters.Add(new NpgsqlParameter("@opis_prehrane", textBoxPrehrana.Text));
                    naredba.Parameters.Add(new NpgsqlParameter("@bol_u_zglobovima", bol_u_zglobovima));
                    naredba.Parameters.Add(new NpgsqlParameter("@ostali_simptomi", textBoxSimptomi.Text));
                    naredba.Parameters.Add(new NpgsqlParameter("@fk_trudnica", id_trudnica));
                    naredba.Parameters.Add(new NpgsqlParameter("@dat", datum));
                    naredba.ExecuteNonQuery();
                    konekcija.Close();
                    MessageBox.Show("Uspješno ste unijeli novi zapis u dnevnik.");
                    UcitavanjeProslihZapisaDnevnik();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Molimo Vas, nadopunite potrebne podatke o prehrani.");
            }

        }
        private void prikazCheckBoxaMucnina()
        {
            if(checkBoxMucnina.Checked == true)
            {
                comboBoxBroj.Visible = true;
                comboBoxBroj.Enabled = true;
                labelBroj.Visible = true;
            }
        }

        private void checkBoxMucnina_Click(object sender, EventArgs e)
        {
            if (checkBoxMucnina.Checked == true)
            {
                comboBoxBroj.Visible = true;
                comboBoxBroj.Enabled = true;
                labelBroj.Visible = true;
            }
        }

        private void buttonNoviIzvjestaj_Click(object sender, EventArgs e)
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            DateTime datumPocetka = dateTimePickerPocetni.Value;
            DateTime.TryParse(dateTimePickerPocetni.Value.ToString(), out datumPocetka);
            DateTime datumKraja = dateTimePickerKonacni.Value;
            DateTime.TryParse(dateTimePickerKonacni.Value.ToString(), out datumKraja);
            try
                {
                    NpgsqlCommand naredba = new NpgsqlCommand("insert into izvjesce (datum_pocetka_vodenja, datum_kraja_dnevnika, fk_trudnica) values (@datum_pocetka_vodenja, @datum_kraja_dnevnika, @fk_trudnica)", konekcija);
                    naredba.Parameters.Add(new NpgsqlParameter("@datum_pocetka_vodenja", datumPocetka));
                    naredba.Parameters.Add(new NpgsqlParameter("@datum_kraja_dnevnika", datumKraja));
                    naredba.Parameters.Add(new NpgsqlParameter("@fk_trudnica", id_trudnica));
                    naredba.ExecuteNonQuery();
                    konekcija.Close();
                    MessageBox.Show("Uspješno ste kreirali novo izvješće.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void buttonProsliDnevnik_Click(object sender, EventArgs e)
        {
            UcitavanjeProslihZapisaDnevnik();
        }
         private void UcitavanjeProslihZapisaDnevnik()
        {

            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            dataGridViewZapisiIzvjesca.DataSource = null;

            sql = $"select * from dnevnik where fk_trudnica = '1'";
            naredba = new NpgsqlCommand(sql, konekcija);
            tablica = new DataTable();
            tablica.Load(naredba.ExecuteReader());

            dataGridViewZapisiIzvjesca.DataSource = tablica;
            dataGridViewZapisiIzvjesca.Columns[0].Visible = false;
            dataGridViewZapisiIzvjesca.Columns[9].Visible = false;
            konekcija.Close();
        }
        private void buttonProsliIzvjesce_Click(object sender, EventArgs e)
        {
            konekcija = new NpgsqlConnection(spajanje);
            konekcija.Open();
            dataGridViewZapisiIzvjesca.DataSource = null;

            sql = $"select * from izvjesce where fk_trudnica = {id_trudnica}";
            naredba = new NpgsqlCommand(sql, konekcija);
            tablica = new DataTable();
            tablica.Load(naredba.ExecuteReader());

            dataGridViewZapisiIzvjesca.DataSource = tablica;
            dataGridViewZapisiIzvjesca.Columns[0].Visible = true;
            dataGridViewZapisiIzvjesca.Columns[4].Visible = false;
            konekcija.Close();
        }
    }
}
