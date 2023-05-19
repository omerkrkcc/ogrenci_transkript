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
    public partial class formDersEkle : Form
    {
        public formDersEkle()
        {
            InitializeComponent();
        }
        NpgsqlConnection sqlErisim = new NpgsqlConnection("Server=localHost;Port=5432;Database=ogrenciTranskriptUygulamasi;User Id=postgres;Password=admin");
        public void formuHazırla()
        {
            txtDersKodu.Text = "";
            txtDersAdi.Text = "";
            txtDersAkts.Text = "";
            txtDersKredi.Text = "";
            txtHarfNotu.Text = "";
            txtOgretmenAdi.Text = "";
            txtOgretmenSoyadi.Text = "";
            txtDersKodu.Focus();
        }
        private void formDersEkle_Load(object sender, EventArgs e)
        {
            NpgsqlCommand komut = new NpgsqlCommand("SELECT DISTINCT donem FROM DERS ORDER BY donem", sqlErisim);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(komut);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            cmbDonem.ValueMember = "dersKodu";
            cmbDonem.DisplayMember = "donem";
            cmbDonem.DataSource = dataTable;
        }
        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
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
                dataGridEkle.DataSource = dataTable;
            }
            komut.Dispose();
            sqlErisim.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            sqlErisim.Open();
            NpgsqlCommand komutEkle = new NpgsqlCommand();
            komutEkle.Connection = sqlErisim;

            komutEkle.Parameters.AddWithValue("@dersKodu", txtDersKodu.Text);
            komutEkle.Parameters.AddWithValue("@dersAdi", txtDersAdi.Text);
            komutEkle.Parameters.AddWithValue("@kredi", Convert.ToDecimal(txtDersKredi.Text));
            komutEkle.Parameters.AddWithValue("@akts", Convert.ToInt32(txtDersAkts.Text));
            komutEkle.Parameters.AddWithValue("@ogretimUyesiAdi", txtOgretmenAdi.Text);
            komutEkle.Parameters.AddWithValue("@ogretimUyesiSoyadi", txtOgretmenSoyadi.Text);
            komutEkle.Parameters.AddWithValue("@donem", cmbDonem.Text);
            komutEkle.Parameters.AddWithValue("@harfNotu", txtHarfNotu.Text);

            komutEkle.CommandType = CommandType.Text;
            komutEkle.CommandText = "insert into DERS (dersKodu,dersAdi,kredi,akts,ogretimUyesiAdi,ogretimUyesiSoyadi,donem,harfNotu) values (@dersKodu,@dersAdi,@kredi,@akts,@ogretimUyesiAdi,@ogretimUyesiSoyadi,@donem,@harfNotu)";
            NpgsqlDataReader dataReader = komutEkle.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridEkle.DataSource = dataTable;
            }
            komutEkle.Dispose();
            sqlErisim.Close();
            verileriListele("select * from DERS");
            formuHazırla();
        }
        private void btnDKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
