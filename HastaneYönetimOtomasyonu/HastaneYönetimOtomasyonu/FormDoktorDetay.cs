using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneYönetimOtomasyonu
{
    public partial class FormDoktorDetay : Form
    {
        public FormDoktorDetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TCNumarasi;
        private void FormDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCNumarasi;


            //DOKTOR AD SOYAD ÇEKME

            SqlCommand cmd = new SqlCommand("select DoktorAd, DoktorSoyad from Table_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0]+" " + dr[1];
            }
            bgl.baglanti().Close();



            //RANDEVU ÇEKME
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Table_Randevular where RandevuDoktor='"+lbladsoyad.Text+"'",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            FormDoktorBilgiDuzenle fr = new FormDoktorBilgiDuzenle();
            fr.TCNO=lblTC.Text;
            fr.Show();
        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            FormDuyurular fr = new FormDuyurular();
            fr.Show();
        }

        private void btncikisyap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchsikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
