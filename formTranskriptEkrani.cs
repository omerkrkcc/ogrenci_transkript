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
    public partial class formTranskriptEkrani : Form
    {
        public formTranskriptEkrani()
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
                dataGridListele.DataSource = dataTable;
            }
            komut.Dispose();
            sqlErisim.Close();
        }
        private void btnDersListele_Click(object sender, EventArgs e)
        {
            verileriListele("select dersKodu,dersAdi,kredi," + "harfNotu,case when harfNotu='AA' then 4 when harfNotu='BA' then 3.5 when harfNotu='CB' then 2.5 when harfNotu='BB' then 3 when harfNotu='CC' then 2 when harfNotu='DC' then 1.5 when harfNotu='DD' then 1 when harfNotu='FD' then 0.5 when harfNotu='FF' then 0 end puan from ders where harfNotu is not null AND harfNotu!=' ' order by donem");

            double krediVeNot = 0;
            double toplamKredi = 0;
            foreach (DataGridViewRow row in dataGridListele.Rows)
            {
                if (row.Cells[2].Value != null && row.Cells[4].Value != null)
                {
                    double kredim = Convert.ToDouble(row.Cells[2].Value);
                    double not = Convert.ToDouble(row.Cells[4].Value);

                    toplamKredi += kredim;
                    double krediNot = kredim * not;
                    krediVeNot += krediNot;

                }
            }
            double gano = krediVeNot / toplamKredi;
            listViewToplamKredi.View = View.Details;
            listViewToplamKredi.Columns.Add("Kredi", 150);
            ListViewItem kredi = new ListViewItem(toplamKredi.ToString("0.00"));
            listViewToplamKredi.Items.Add(kredi);

            listViewGano.View = View.Details;
            listViewGano.Columns.Add("Gano", 150);
            ListViewItem ortalama = new ListViewItem(gano.ToString("0.00"));
            listViewGano.Items.Add(ortalama);
        }
        private void btnDKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
