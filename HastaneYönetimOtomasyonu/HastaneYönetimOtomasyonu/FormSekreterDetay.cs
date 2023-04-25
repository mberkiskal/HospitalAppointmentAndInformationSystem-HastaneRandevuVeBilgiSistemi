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
    public partial class FormSekreterDetay : Form
    {
        public FormSekreterDetay()
        {
            InitializeComponent();
        }


        public string TCnumara;

        sqlbaglantisi bgl=new sqlbaglantisi();
        
        private void FormSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCnumara;

            //AD SOYAD ÇEKME
            SqlCommand cmd1 = new SqlCommand("Select SekreterAdSoyad from Table_Sekreterler where SekreterTc=@p1",bgl.baglanti());
            cmd1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                lbladsoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();



            //BRANŞLARI ÇEKME

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Table_Branslar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;


            //DOKTORLARI LİSTEYE AKTARMA

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+' '+DoktorSoyad) as 'Doktor Ad-Soyad',DoktorBrans from Table_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //BRANŞI COMBOBOX AKTARMA

            SqlCommand cmd2 = new SqlCommand("Select BransAd from Table_Branslar", bgl.baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            
        }

        private void btnrandevukayıt_Click(object sender, EventArgs e)
        {
            if (msktarih.Text!="" || msksaat.Text!="" || cmbbrans.Text!=""||cmbdoktor.Text!="")
            {
                MessageBox.Show("Eksik Bilgi Girdiniz!", "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                SqlCommand komutkaydet = new SqlCommand("insert into Table_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@r1, @r2, @r3, @r4)", bgl.baglanti());
                komutkaydet.Parameters.AddWithValue("@r1", msktarih.Text);
                komutkaydet.Parameters.AddWithValue("@r2", msksaat.Text);
                komutkaydet.Parameters.AddWithValue("@r3", cmbbrans.Text);
                komutkaydet.Parameters.AddWithValue("@r4", cmbdoktor.Text);
                komutkaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Randevu Başarıyla Oluşturuldu!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdoktor.Items.Clear();
            SqlCommand cmd3 = new SqlCommand("Select DoktorAd, DoktorSoyad from Table_Doktorlar where DoktorBrans=@p1 ", bgl.baglanti());
            cmd3.Parameters.AddWithValue("@p1", cmbbrans.Text);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0]+" "+dr3[1]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {   
            if(rchduyuru.Text != "")
            {
                SqlCommand cmd = new SqlCommand("insert into Table_Duyurular (Duyuru) values (@d1) ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@d1", rchduyuru.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru Başarıyla Oluşturuldu!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bu alan boş geçilemez!");
            }
           
        }

        private void btndoktorpanel_Click(object sender, EventArgs e)
        {
            FormDoktorPaneli fr = new FormDoktorPaneli();
            fr.Show();
        }

        private void btnbranspanel_Click(object sender, EventArgs e)
        {
            FormBrans formBrans = new FormBrans();
            formBrans.Show();
        }

        private void btnrandevuliste_Click(object sender, EventArgs e)
        {
            FormRandevuListesi fr= new FormRandevuListesi();
            fr.Show();
        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            FormDuyurular fr= new FormDuyurular();
            fr.Show();
        }

       

    }
}
