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
    public partial class FormDoktorPaneli : Form
    {
        public FormDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FormDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Table_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //BRANSLARI AKTARMA
            SqlCommand cmd4 = new SqlCommand("Select BransAd from Table_Branslar", bgl.baglanti());
            SqlDataReader dr = cmd4.ExecuteReader();
            while (dr.Read())
            {
                cmbbrans.Items.Add(dr[0]);
            }
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Table_Doktorlar (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorSifre) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtad.Text);
            cmd.Parameters.AddWithValue("@p2", txtsoyad.Text);
            cmd.Parameters.AddWithValue("@p3", cmbbrans.Text);
            cmd.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd.Parameters.AddWithValue("@p5", txtsifre.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Başarıyla EKlendi!", "İşlem Başarılı", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbbrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtsifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("delete from Table_Doktorlar where DoktorTC=@p1",bgl.baglanti());
            cmd2.Parameters.AddWithValue("@p1", mskTC.Text);
            cmd2.ExecuteNonQuery();
            bgl.baglanti() .Close();
            MessageBox.Show("Kayıt Başarıyla Silindi!","İşlem Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd3 = new SqlCommand("update Table_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p5 where DoktorTC=@p4", bgl.baglanti());
            cmd3.Parameters.AddWithValue("@p1", txtad.Text);
            cmd3.Parameters.AddWithValue("@p2", txtsoyad.Text);
            cmd3.Parameters.AddWithValue("@p3", cmbbrans.Text);
            cmd3.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd3.Parameters.AddWithValue("@p5", txtsifre.Text);
            cmd3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Başarıyla Güncellendi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
