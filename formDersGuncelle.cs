using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace ogrenciTranskriptUyglm
{
    public partial class formDersGuncelle : Form
    {
        public formDersGuncelle()
        {
            InitializeComponent();
        }
        public void formuHazırla()
        {
            txtKoduGetir.Text = "";
            txtDersAkts.Text = "";
            txtDersAdi.Text = "";
            txtDersKredi.Text = "";
            txtGuncellenecekKod.Text = "";
            txtHarfNotu.Text = "";
            txtKoduGetir.Text = "";
            txtOgretmenAdi.Text = "";
            txtOgretmenSoyadi.Text = "";
            txtYeniDersKodu.Text = "";
            txtKoduGetir.Focus();
        }
        private void formDersGuncelle_Load(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("SELECT DISTINCT donem FROM DERS ORDER BY donem", sqlErisim);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(komut);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            cmbDonem.ValueMember = "dersKodu";
            cmbDonem.DisplayMember = "donem";
            cmbDonem.DataSource = dataTable;
        }
        NpgsqlConnection sqlErisim = new NpgsqlConnection("Server=localHost;Port=5432;Database=ogrenciTranskriptUygulamasi;User Id=postgres;Password=admin");
        public void verileriListele(string txt)
        {
            NpgsqlCommand komut = new NpgsqlCommand();
            komut.Connection = sqlErisim;
            komut.CommandType = CommandType.Text;
            komut.CommandText = txt;
            sqlErisim.Open();
            NpgsqlDataReader dataReader = komut.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridGuncelle.DataSource = dataTable;
            }
            komut.Dispose();
            sqlErisim.Close();
        }
        private void btnVerileriGetir_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komutGetir = new NpgsqlCommand("select * from DERS WHERE dersKodu = @dersKodu", sqlErisim);
            komutGetir.Parameters.AddWithValue("@dersKodu", txtKoduGetir.Text);
            sqlErisim.Open();
            NpgsqlDataReader dataReader = komutGetir.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridGuncelle.DataSource = dataTable;
                dataGridGuncelle.Refresh();
            }
            komutGetir.Dispose();
            sqlErisim.Close();
        }

        private void btnDersIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnDersGuncelle_Click(object sender, EventArgs e)
        {

            NpgsqlCommand komutGuncelle = new NpgsqlCommand("UPDATE DERS SET dersKodu = @dersKodu, dersAdi = @dersAdi, kredi = @kredi, akts = @akts, ogretimUyesiAdi = @ogretimUyesiAdi, ogretimUyesiSoyadi = @ogretimUyesiSoyadi, donem = @donem, harfNotu = @harfNotu WHERE dersKodu = @guncelDers", sqlErisim);
            komutGuncelle.Parameters.AddWithValue("@dersKodu", txtYeniDersKodu.Text);
            komutGuncelle.Parameters.AddWithValue("@dersAdi", txtDersAdi.Text);
            komutGuncelle.Parameters.AddWithValue("@kredi", Convert.ToDecimal(txtDersKredi.Text));
            komutGuncelle.Parameters.AddWithValue("@akts", Convert.ToInt32(txtDersAkts.Text));
            komutGuncelle.Parameters.AddWithValue("@ogretimUyesiAdi", txtOgretmenAdi.Text);
            komutGuncelle.Parameters.AddWithValue("@ogretimUyesiSoyadi", txtOgretmenSoyadi.Text);
            komutGuncelle.Parameters.AddWithValue("@donem", cmbDonem.Text);
            komutGuncelle.Parameters.AddWithValue("@harfNotu", txtHarfNotu.Text);
            komutGuncelle.Parameters.AddWithValue("@guncelDers", txtGuncellenecekKod.Text);
            sqlErisim.Open();
            NpgsqlDataReader dataReader = komutGuncelle.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridGuncelle.DataSource = dataTable;
                dataGridGuncelle.Refresh();
            }
            komutGuncelle.Dispose();
            sqlErisim.Close();
            verileriListele("select * from ders");
            formuHazırla();
        }
        private void btnDKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDonem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
