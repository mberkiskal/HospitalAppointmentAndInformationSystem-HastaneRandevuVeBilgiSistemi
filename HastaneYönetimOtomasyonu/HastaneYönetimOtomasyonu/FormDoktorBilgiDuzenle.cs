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
    public partial class FormDoktorBilgiDuzenle : Form
    {
        public FormDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TCNO;

        private void FormDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCNO;
            SqlCommand cmd = new SqlCommand("select * from Table_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                cmbbrans.Text = dr[3].ToString();
                txtsifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
            
        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("update Table_Doktorlar set DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorSifre=@p4 where DoktorTC=@p5",bgl.baglanti());
            cmd2.Parameters.AddWithValue("@p1",txtad.Text);
            cmd2.Parameters.AddWithValue("@p2", txtsoyad.Text);
            cmd2.Parameters.AddWithValue("@p3", cmbbrans.Text);
            cmd2.Parameters.AddWithValue("@p4", txtsifre.Text);
            cmd2.Parameters.AddWithValue("@p5", mskTC.Text);
            cmd2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Başarıyla Güncellendi!");
        }
    }
}
