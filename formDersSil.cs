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
    public partial class formDersSil : Form
    {
        public formDersSil()
        {
            InitializeComponent();
        }
        NpgsqlConnection sqlErisim = new NpgsqlConnection("Server=localHost;Port=5432;Database=ogrenciTranskriptUygulamasi;User Id=postgres;Password=admin");
        public void formuHazırla()
        {
            txtVerileriGetir.Text = "";
            txtVerileriGetir.Focus();
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
                dataGridSil.DataSource = dataTable;
            }
            komut.Dispose();
            sqlErisim.Close();
        }
        private void btnVerileriGetir_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komutGetir = new NpgsqlCommand("select * from DERS WHERE dersKodu = @dersKodu", sqlErisim);
            komutGetir.Parameters.AddWithValue("@dersKodu", txtVerileriGetir.Text);
            sqlErisim.Open();
            NpgsqlDataReader dataReader = komutGetir.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridSil.DataSource = dataTable;
                dataGridSil.Refresh();
            }
            komutGetir.Dispose();
            sqlErisim.Close();
        }
        private void btnDersSil_Click(object sender, EventArgs e)
        {
            NpgsqlCommand komutSil = new NpgsqlCommand("DELETE FROM DERS WHERE dersKodu = @dersKodu", sqlErisim);
            komutSil.Parameters.AddWithValue("@dersKodu", txtDersSil.Text);
            sqlErisim.Open();
            NpgsqlDataReader dataReader = komutSil.ExecuteReader();
            if (dataReader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGridSil.DataSource = dataTable;
            }
            komutSil.Dispose();
            sqlErisim.Close();
            verileriListele("select * from DERS");
            formuHazırla();
        }
        private void btnSilmeIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void btnDKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
