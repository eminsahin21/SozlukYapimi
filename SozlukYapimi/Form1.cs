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

namespace SozlukYapimi
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "\\vt_sozluk.accdb");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand eklekomutu = new OleDbCommand("insert into ingturkce values('"+textBox1.Text+"','"+textBox2.Text+"')", baglanti);
                eklekomutu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Sözcük veri tabanına eklendi...","Veri tabani işlemleri");
                textBox1.Clear();
                textBox2.Clear();
                
            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message+"Veri tabanı işlemleri");
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand guncelle_komutu = new OleDbCommand("update ingturkce set turkce='"+textBox2.Text+"' where ingilizce='"+textBox1.Text+"'", baglanti);
                guncelle_komutu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Sözcük veri tabnında güncellendi...", "Veri tabani işlemleri");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message + "Veri tabanı işlemleri");
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                OleDbCommand silme_komutu = new OleDbCommand("delete from ingturkce where ingilizce='"+textBox1.Text+"'", baglanti);
                silme_komutu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Sözcük veri tabanından silindi...", "Veri tabani işlemleri");
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message + "Veri tabanı işlemleri");
                baglanti.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                baglanti.Open();
                OleDbCommand aramakomutu = new OleDbCommand("select * from ingturkce where ingilizce like '"+textBox1.Text+"%'", baglanti);
                OleDbDataReader oku = aramakomutu.ExecuteReader();
                while (oku.Read())
                {
                    listBox1.Items.Add(oku["ingilizce"].ToString() + "=" + oku["turkce"].ToString());                 
                }
                baglanti.Close();
            }
            catch (Exception aciklama)
            {

                MessageBox.Show(aciklama.Message + "Veri tabanı işlemleri");
                baglanti.Close();
            }
        }
    }
}
