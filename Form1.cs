using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tytul = null;
            string tresc = null;
            string data = null;

            tytul = listView1.SelectedItems[0].SubItems[0].Text;
            tresc = listView1.SelectedItems[0].SubItems[1].Text;
            data = listView1.SelectedItems[0].SubItems[2].Text;

            MessageBox.Show("Dnia: " + data + " notatka: " + tytul + " o tresci: " + tresc);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("Tytul", 70);
            listView1.Columns.Add("Tresc", 140);
            listView1.Columns.Add("Data", 70);
            listView1.Columns.Add("Godzina", 70);
            listView1.Columns.Add("Grupa", 70);
            listView1.Columns.Add("Termin", 70);
            listView1.Columns.Add("Przypomnienie", 70);

            //Add items in the listview
            string[] arr = new string[7];
            ListViewItem itm;

            string FileName = "plik.txt";
            string line, tytul, tresc, data, godzina, grupa, termin, przypomnienie;

            System.IO.StreamReader reader;
            reader = new System.IO.StreamReader(FileName, true);

            while ((line = reader.ReadLine()) != null)
            {
                int i = line.IndexOf('|');
                tytul = line.Substring(0, i);
                arr[0] = tytul;
                string sub = line.Substring(i + 1);

                int a = sub.IndexOf('|');
                tresc = sub.Substring(0, a);
                arr[1] = tresc;
                string sub_1 = sub.Substring(a + 1);

                int b = sub_1.IndexOf('|');
                data = sub_1.Substring(0, b);
                arr[2] = data;
                string sub_2 = sub_1.Substring(b + 1);

                int c = sub_2.IndexOf('|');
                godzina = sub_2.Substring(0, c);
                arr[3] = godzina;
                string sub_3 = sub_2.Substring(c + 1);

                int d = sub_3.IndexOf('|');
                grupa = sub_3.Substring(0, d);
                arr[4] = grupa;
                string sub_4 = sub_3.Substring(d + 1);

                int x = sub_4.IndexOf('|');
                termin = sub_4.Substring(0, x);
                arr[5] = termin;
                string sub_5 = sub_4.Substring(x + 1);

                int y = sub_5.IndexOf('|');
                przypomnienie = sub_5.Substring(0, y);
                arr[6] = przypomnienie;

                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);

            }
        }

    }
}

