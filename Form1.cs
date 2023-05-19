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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                dataGridMenu.DataSource = dataTable;
            }
            komut.Dispose();
            sqlErisim.Close();
        }
        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            formDersEkle ekle = new formDersEkle();
            this.Hide();
            ekle.ShowDialog();
            this.Show();
        }
        private void btnDersSil_Click(object sender, EventArgs e)
        {
            formDersSil sil = new formDersSil();
            this.Hide();
            sil.ShowDialog();
            this.Show();
        }
        private void btnDersGuncelle_Click(object sender, EventArgs e)
        {
            formDersGuncelle guncelle = new formDersGuncelle();
            this.Hide();
            guncelle.ShowDialog();
            this.Show();
        }
        private void btnDersListele_Click(object sender, EventArgs e)
        {
            // Bu kısımda puan kısmı kasıtlı olarak gösterilmemiştir . Transkript Ekranı kısmında puan kısmı harf notlarına göre hesaplandı ve orda harfnotuyle beraber 
            // puanları göstermem daha mantıklı geldi.
            verileriListele("select * from ders order by donem");
        }
        private void btnTranskript_Click(object sender, EventArgs e)
        {
            formTranskriptEkrani transkript = new formTranskriptEkrani();
            this.Hide();
            transkript.ShowDialog();
            this.Show();
        }
        private void btnDKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
