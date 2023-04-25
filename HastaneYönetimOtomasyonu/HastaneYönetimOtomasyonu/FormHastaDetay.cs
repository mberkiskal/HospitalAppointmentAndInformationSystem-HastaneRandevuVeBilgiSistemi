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
    public partial class FormHastaDetay : Form
    {
        public FormHastaDetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;


        private void FormHastaDetay_Load(object sender, EventArgs e)
        {
            //AD SOYAD ÇEKME
            lblTC.Text = tc;    
            SqlCommand command = new SqlCommand("Select HastaAd,HastaSoyad from Table_Hastalar where HastaTC=@p1", bgl.baglanti());
            command.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();


            //RANDEVU GEÇMİŞİNİ ÇEKME
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter("Select * from Table_Randevular where HastaTC="+ tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            

            //BRANŞLARI ÇEKME
            SqlCommand command2 = new SqlCommand("Select BransAd from Table_Branslar",bgl.baglanti());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdoktor.Items.Clear();

            SqlCommand command3 = new SqlCommand("Select DoktorAd,DoktorSoyad from Table_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            command3.Parameters.AddWithValue("@p1", cmbbrans.Text);
            SqlDataReader dr3 = command3.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0]+" "+ dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Table_Randevular where RandevuBrans='" +cmbbrans.Text+ "'"+"and RandevuDoktor='" +cmbdoktor.Text+ "'and RandevuDurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkbilgiduzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormBilgiDuzenle fr= new FormBilgiDuzenle();
            fr.TCno = lblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtrandevuid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnrandevual_Click(object sender, EventArgs e)
        {
            if (txtrandevuid.Text == "" || cmbbrans.Text == "" || cmbdoktor.Text == "" || rchsikayet.Text == "")
            {
                MessageBox.Show("Eksik Bilgi Girdiniz!", "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                SqlCommand cmd = new SqlCommand("update Table_Randevular set RandevuDurum=1, HastaTC=@p1, HastaSikayet=@p2 where RandevuId=@p3", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", lblTC.Text);
                cmd.Parameters.AddWithValue("@p2", rchsikayet.Text);
                cmd.Parameters.AddWithValue("p3", txtrandevuid.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Randevu Başarıyla Alındı!", "Randevu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }
    }
}
