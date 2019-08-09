using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Timer t = new Timer();

        private void Form2_Load(object sender, EventArgs e)
        {

            BackColor = Color.Orange;

            label8.Text = DateTime.Now.DayOfWeek.ToString();

            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            //Form2 form2 = new Form2();
            //form2.Show();

            //form2.Close();

            comboBox1.Items.Add("Moje terminy");
            comboBox1.Items.Add("Inne");

            //label7.Text = DateTime.Now.ToString("dd / MM / yyyy H:mm");
            //label7.Text = DateTime.Now.ToString("H:mm");

          

            

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string separator = "|";
            string separator_1 = "/";
            string separator_2 = ":";

            System.IO.StreamWriter objWriter = new System.IO.StreamWriter(@"plik.txt", true); //open the file for writing.               

            objWriter.Write(textBox1.Text);         //tytul
            objWriter.Write(separator);
            objWriter.Write(textBox2.Text);         //tresc
            objWriter.Write(separator);
            objWriter.Write(numericUpDown1.Text);   //parametry dzien
            objWriter.Write(separator_1);
            objWriter.Write(numericUpDown3.Text);   //parametry miesiac
            objWriter.Write(separator_1);
            objWriter.Write(numericUpDown4.Text);   //parametry rok
            objWriter.Write(separator);
            objWriter.Write(numericUpDown5.Text);   //parametry minuta
            objWriter.Write(separator_2);
            objWriter.Write(numericUpDown6.Text);   //..godzina
            objWriter.Write(separator);
            objWriter.Write(comboBox1.Text);        //Grupa
            objWriter.Write(separator);

            if (radioButton4.Checked == true)       //Rodzaj terminu
                objWriter.Write("Lokalny");
            else if (radioButton5.Checked == true)
                objWriter.Write("Online");

            objWriter.Write(separator);

            if (radioButton1.Checked == true)       //Przypomnienie
                objWriter.Write("Brak przypomnienia");
            else if (radioButton2.Checked == true)
                objWriter.Write("W czasie terminu");
            else if (radioButton3.Checked == true)
            {
                objWriter.Write(numericUpDown8.Text);   //przypomnienie dzien
                objWriter.Write(separator_1);
                objWriter.Write(numericUpDown7.Text);   //przypomnienie miesiac
                objWriter.Write(separator_1);
                objWriter.Write(numericUpDown2.Text);   //przypomnienie rok
                objWriter.Write(" ");
                objWriter.Write(numericUpDown10.Text);   //.. minuta
                objWriter.Write(separator_2);
                objWriter.Write(numericUpDown9.Text);   //..godzina

            }
            objWriter.Write(separator);
            objWriter.WriteLine();
            objWriter.Close(); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void t_Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            string time = "";
            string data = "";

            if (hh < 10)
                time += "0" + hh;
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
                time += "0" + mm;
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
                time += "0" + ss;
            else
            {
                time += ss;
            }

            label1.Text = time;

            int dd = DateTime.Now.Day;
            int MM = DateTime.Now.Month;
            int yy = DateTime.Now.Year;

            data += dd;
            data += " / ";
            data += MM;
            data += " / ";
            data += yy;

            label2.Text = data;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        

    }
}
