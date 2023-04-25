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
    public partial class FormBilgiDuzenle : Form
    {
        public FormBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno;
        sqlbaglantisi bgl = new sqlbaglantisi();


        private void FormBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text= TCno;  
            SqlCommand cmd = new SqlCommand("Select * from Table_Hastalar where HastaTC=@p1",bgl.baglanti()); 
            cmd.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text= dr[1].ToString();
                txtsoyad.Text= dr[2].ToString();
                msktelefon.Text= dr[4].ToString();
                txtsifre.Text= dr[5].ToString();
                cmbcinsiyet.Text= dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Update Table_Hastalar SET HastaAd=@p1, HastaSoyad=@p2, HastaTel=@p3, HastaSifre=@p4, HastaCinsiyer=@p5 where HastaTC=@p6", bgl.baglanti());
            cmd2.Parameters.AddWithValue("@p1",txtad.Text);
            cmd2.Parameters.AddWithValue("@p2",txtsoyad.Text);
            cmd2.Parameters.AddWithValue("@p3",msktelefon.Text);
            cmd2.Parameters.AddWithValue("@p4",txtsifre.Text);
            cmd2.Parameters.AddWithValue("@p5",cmbcinsiyet.Text);
            cmd2.Parameters.AddWithValue("@p6", mskTC.Text);

            cmd2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Başarıyla Güncellendi!","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
