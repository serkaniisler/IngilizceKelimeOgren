using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace IngilizceKelimeOgren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Serkan\\Desktop\\dbSozluk.accdb");

        Random rand = new Random();
        int sure = 90;
        int kelime = 0;


        void Getir()
        {
            int sayi;
            sayi = rand.Next(1, 2490);
            lblCevap.Text = sayi.ToString();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * from sozluk where id=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                txtIngilizce.Text = reader[1].ToString();
                lblCevap.Text = reader[2].ToString();
                lblCevap.Text = lblCevap.Text.ToLower();
                

            }
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Getir();
            txtTurkce.Focus();
            timer1.Start();
        }

        private void txtTurkce_TextChanged(object sender, EventArgs e)
        {
            if (txtTurkce.Text == lblCevap.Text)
            {
                kelime++;
                lblKelime.Text= kelime.ToString();
                Getir();
                txtTurkce.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblSure.Text = sure.ToString();
            if (sure == 0)
            {
                txtTurkce.Enabled = false;
                txtIngilizce.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
