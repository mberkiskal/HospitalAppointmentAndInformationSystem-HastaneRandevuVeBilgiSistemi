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
    public partial class FormDoktorGiris : Form
    {
        public FormDoktorGiris()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Table_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2 ",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTC.Text);
            cmd.Parameters.AddWithValue("@p2", txtsifre.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                FormDoktorDetay fr = new FormDoktorDetay();
                fr.TCNumarasi=mskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre. Lütfen Bilgilerinizi tekrar kontrol ediniz!","Giriş Hatası",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
