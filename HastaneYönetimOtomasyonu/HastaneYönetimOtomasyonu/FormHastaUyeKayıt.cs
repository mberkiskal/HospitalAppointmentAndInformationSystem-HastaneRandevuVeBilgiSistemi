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
    public partial class FormHastaUyeKayıt : Form
    {
        public FormHastaUyeKayıt()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FormHastaUyeKayıt_Load(object sender, EventArgs e)
        {

        }

        private void btnkayıtyap_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("insert into Table_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTel,HastaSifre,HastaCinsiyer) values (@p1,@p2,@p3,@p4,@p5,@p6) ", bgl.baglanti());
            com.Parameters.AddWithValue("@p1", txtad.Text);
            com.Parameters.AddWithValue("@p2", txtsoyad.Text);
            com.Parameters.AddWithValue("@p3", mskTC.Text);
            com.Parameters.AddWithValue("@p4", msktelefon.Text);
            com.Parameters.AddWithValue("@p5", txtsifre.Text);
            com.Parameters.AddWithValue("@p6", cmbcinsiyet.Text);
            com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Başarıyla Gerçekleşmiştir!\n Şifreniz:"+ txtsifre.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
