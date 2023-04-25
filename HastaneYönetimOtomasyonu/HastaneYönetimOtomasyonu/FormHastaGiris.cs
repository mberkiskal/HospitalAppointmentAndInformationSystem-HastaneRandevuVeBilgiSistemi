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
    public partial class FormHastaGiris : Form
    {
        public FormHastaGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FormHastaGiris_Load(object sender, EventArgs e)
        {

        }

        private void lnkuyeol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormHastaUyeKayıt fr = new FormHastaUyeKayıt();
            fr.Show();
        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand("Select * from Table_Hastalar where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());
            command.Parameters.AddWithValue("@p1", mskTC.Text);
            command.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = command.ExecuteReader();
            if(dr.Read())
            {
                FormHastaDetay fr = new FormHastaDetay();
                fr.tc=mskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre!\n","Bilgilerinizi Tekrar Kontrol Ediniz!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();

        }
    }
}
