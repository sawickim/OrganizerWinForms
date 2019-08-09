using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Timer t = new Timer();

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            //i day = DateTime.Now.DayOfWeek;

            label4.Text = DateTime.Now.DayOfWeek.ToString();

            PictureBox imageControl = new PictureBox();
            imageControl.Width = 400;
            imageControl.Height = 400;

            Bitmap image = new Bitmap("organizer-rodzinny02-1024x680.jpg");
            imageControl.Dock = DockStyle.Fill;
            imageControl.Image = (Image)image;

            Controls.Add(imageControl);
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.BackColor = System.Drawing.Color.Orange;
            listView1.ForeColor = System.Drawing.Color.Black;

            //Add column header
            listView1.Columns.Add("Tytul", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Tresc", 140, HorizontalAlignment.Left);
            listView1.Columns.Add("Data", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Godzina", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Grupa", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Termin", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Przypomnienie", 120, HorizontalAlignment.Center);

            //Add items in the listview
            string[] arr = new string[7];
            ListViewItem itm;

            string FileName = "plik.txt";
            string line, tytul, tresc, data, godzina, grupa, termin, przypomnienie;

            System.IO.StreamReader reader;
            reader = new System.IO.StreamReader(FileName, true);

            while ((line = reader.ReadLine()) != null)
            {
                int i = line.IndexOf('|');          //Dodaj tytul do listview 
                tytul = line.Substring(0, i);
                arr[0] = tytul;
                string sub = line.Substring(i + 1);

                int a = sub.IndexOf('|');
                tresc = sub.Substring(0, a);        //dodaj tresc do listview
                arr[1] = tresc;
                string sub_1 = sub.Substring(a + 1);

                int b = sub_1.IndexOf('|');
                data = sub_1.Substring(0, b);       //dodaj date do listview
                arr[2] = data;
                string sub_2 = sub_1.Substring(b + 1);

                int c = sub_2.IndexOf('|');         //dodaj godzine do listview
                godzina = sub_2.Substring(0, c);
                arr[3] = godzina;
                string sub_3 = sub_2.Substring(c + 1);

                int d = sub_3.IndexOf('|');
                grupa = sub_3.Substring(0, d);      //dodaj grupe do listview
                arr[4] = grupa;
                string sub_4 = sub_3.Substring(d + 1);

                int x = sub_4.IndexOf('|');         //dodaj termin do listiview
                termin = sub_4.Substring(0, x);
                arr[5] = termin;
                string sub_5 = sub_4.Substring(x + 1);

                int y = sub_5.IndexOf('|');             //dodaj przypomnienie do listview
                przypomnienie = sub_5.Substring(0, y);
                arr[6] = przypomnienie;

                if (przypomnienie.Contains("/") && przypomnienie.Contains(":"))
                    remember(przypomnienie, tresc, data);       //metoda do wyswietlania komunikatu o bledzie bez wczesniejszego przypomnienia

                if (przypomnienie.Contains("termin"))
                    remember2(data, godzina, tresc);    //metoda wyswietlajaca komunikat o zadaniu do wykonania z wczesniejszym przypomnieniu
                //label1.Text = przypomnienie;

                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
                //listView1.Items.Remove(itm);
            }
            reader.Close();
        }

        private void remember2(string przyp, string zadanie, string wykonanie)    //metoda wyswietlajaca komunikat o zadaniu do wykonania z wczesniejszym przypomnieniu
        {

            int ii = przyp.IndexOf('/');
            string dd = przyp.Substring(0, ii);         //wytnij ze stringa dzien miesiaca
            string sub = przyp.Substring(ii + 1);

            int a = sub.IndexOf('/');
            string MM = sub.Substring(0, a);            //..miesiaca
            string yy = sub.Substring(a + 1);           //..roku

            int c = zadanie.IndexOf(':');
            string h = zadanie.Substring(0, c);         //..godzine
            string m = zadanie.Substring(c + 1);        //.. minute

            if (((DateTime.Now.Year > int.Parse(yy)) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month > int.Parse(MM))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day > int.Parse(dd))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day == int.Parse(dd)) && (DateTime.Now.Hour > int.Parse(m))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day == int.Parse(dd)) && (DateTime.Now.Hour == int.Parse(h)) && (DateTime.Now.Minute > int.Parse(m)))))
                MessageBox.Show("Przypominam że dnia: " + przyp + "\nPominieneś " + wykonanie);

        }

        private void remember(string przyp, string zadanie, string wykonanie)   //metoda do wyswietlania komunikatu o bledzie bez wczesniejszego przypomnienia
        {
            string hash = "/";
            string colon = ":";

            if (przyp.Contains(hash) && przyp.Contains(colon))      //jesli w polu przypomnienie zawarte jest '/'lub ':'
            {
                int ii = przyp.IndexOf('/');
                string dd = przyp.Substring(0, ii);         //wytnij ze stringa dzien
                string sub = przyp.Substring(ii + 1);

                int a = sub.IndexOf('/');
                string MM = sub.Substring(0, a);            // wytnij ze stringa miesiac
                string sub1 = sub.Substring(a + 1);

                int b = sub1.IndexOf(' ');
                string yy = sub1.Substring(0, b);           //wytnij ze stringa rok
                string sub2 = sub1.Substring(b + 1);

                int c = sub2.IndexOf(':');
                string h = sub2.Substring(0, c);            //wytnij ze stringa godzine
                string m = sub2.Substring(c + 1);           //wytnij ze stringa minute


                if (((DateTime.Now.Year > int.Parse(yy)) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month > int.Parse(MM))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day > int.Parse(dd))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day == int.Parse(dd)) && (DateTime.Now.Hour > int.Parse(m))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day == int.Parse(dd)) && (DateTime.Now.Hour == int.Parse(h)) && (DateTime.Now.Minute > int.Parse(m)))))
                    MessageBox.Show("Przypominam że dnia: " + wykonanie + "\nPowinieneś " + zadanie);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)                  //kliknij na listview i zobacz wiecej
        {
            string tytul = null;
            string tresc = null;
            string data = null;

            tytul = listView1.SelectedItems[0].SubItems[0].Text;
            tresc = listView1.SelectedItems[0].SubItems[1].Text;
            data = listView1.SelectedItems[0].SubItems[2].Text;

            MessageBox.Show("Dnia: " + data + "\nNotatka: " + tytul + "\nO tresci: " + tresc);
        }

        private void button2_Click(object sender, EventArgs e)                  //otworz nowe okno dodajace zadania
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)            //metoda odswiez pokazujaca pola ktore przypomnial program
        {
            //Add items in the listview
            string[] arr = new string[7];
            ListViewItem itm;

            string FileName = "plik.txt";
            string line, tytul, tresc, data, godzina, grupa, termin, przypomnienie;

            System.IO.StreamReader reader;
            reader = new System.IO.StreamReader(FileName, false);

            listView1.Items.Clear();

            while ((line = reader.ReadLine()) != null)
            {

                int i = line.IndexOf('|');          //Odswiez tytul 
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
            reader.Close();
        }

        private void button4_Click(object sender, EventArgs e)      //metoda usun przeteminowane pola
        {
            //Add items in the listview
            string[] arr = new string[7];
            ListViewItem itm;

            string FileName = "plik.txt";
            string line, tytul, tresc, data, godzina, grupa, termin, przypomnienie;
            int number = 0;

            System.IO.StreamReader reader;
            reader = new System.IO.StreamReader(FileName, true);

            listView1.Items.Clear();
            List<int> listnumber = new List<int>();

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
                //listView1.Items.Remove(itm);

                if (przypomnienie.Contains("/") && przypomnienie.Contains(":"))
                {

                    int ii = przypomnienie.IndexOf('/');
                    string dd = przypomnienie.Substring(0, ii);
                    string sub_ = przypomnienie.Substring(ii + 1);

                    int aa = sub_.IndexOf('/');
                    string MM = sub_.Substring(0, aa);
                    string sub1 = sub_.Substring(aa + 1);

                    int bb = sub1.IndexOf(' ');
                    string yy = sub1.Substring(0, bb);
                    string sub2 = sub1.Substring(bb + 1);

                    int cc = sub2.IndexOf(':');
                    string h = sub2.Substring(0, cc);
                    string m = sub2.Substring(cc + 1);

                    if (((DateTime.Now.Year > int.Parse(yy)) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month > int.Parse(MM))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day > int.Parse(dd))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day == int.Parse(dd)) && (DateTime.Now.Hour > int.Parse(m))) || ((DateTime.Now.Year == int.Parse(yy)) && (DateTime.Now.Month == int.Parse(MM)) && (DateTime.Now.Day == int.Parse(dd)) && (DateTime.Now.Hour == int.Parse(h)) && (DateTime.Now.Minute > int.Parse(m)))))
                    {
                        listView1.Items.Remove(itm);
                        //objWriter.WriteLine(line);
                        //richTextBox1.Text = line;
                        //var linesList = File.ReadAllLines("plik.txt").ToList();
                        //linesList.RemoveAt(number);
                        //File.WriteAllLines(("plik.txt"), linesList.ToArray());
                        listnumber.Add(number);
                    }
                }

                if (przypomnienie.Contains("termin"))
                {
                    //metoda wyswietlajaca komunikat o zadaniu do wykonania z wczesniejszym przypomnieniu
                    int iii = data.IndexOf('/');
                    string ddd = data.Substring(0, iii);
                    string sub5 = data.Substring(iii + 1);

                    int aaa = sub5.IndexOf('/');
                    string MMM = sub5.Substring(0, aaa);
                    string yyy = sub5.Substring(aaa + 1);

                    int ccc = godzina.IndexOf(':');
                    string hh = godzina.Substring(0, ccc);
                    string mm = godzina.Substring(ccc + 1);

                    //label1.Text = yyy;


                    if (((DateTime.Now.Year > int.Parse(yyy)) || ((DateTime.Now.Year == int.Parse(yyy)) && (DateTime.Now.Month > int.Parse(MMM))) || ((DateTime.Now.Year == int.Parse(yyy)) && (DateTime.Now.Month == int.Parse(MMM)) && (DateTime.Now.Day > int.Parse(ddd))) || ((DateTime.Now.Year == int.Parse(yyy)) && (DateTime.Now.Month == int.Parse(MMM)) && (DateTime.Now.Day == int.Parse(ddd)) && (DateTime.Now.Hour > int.Parse(mm))) || ((DateTime.Now.Year == int.Parse(yyy)) && (DateTime.Now.Month == int.Parse(MMM)) && (DateTime.Now.Day == int.Parse(ddd)) && (DateTime.Now.Hour == int.Parse(hh)) && (DateTime.Now.Minute > int.Parse(mm)))))
                    {
                        listView1.Items.Remove(itm);
                        //obj1Writer.WriteLine(line);
                        //var linesList = File.ReadAllLines("plik.txt").ToList();
                        //linesList.RemoveAt(number);
                        //File.WriteAllLines(("plik.txt"), linesList.ToArray());
                        listnumber.Add(number);
                    }
                }
                number++;
            }
            reader.Close();
            //objWriter.Close();
            //obj1Writer.Close();
            //Cancel_line();          //kasowanie niepotrzebnych danych
            Cancel_line1(listnumber);
        }
        
        private void Cancel_line1(List<int> listnumber) // kasuje linie w pliku 
        {
            var linesList = File.ReadAllLines("plik.txt").ToList();
            foreach (var prime in listnumber.OrderByDescending(r => r).ToList())
            {
                linesList.RemoveAt(prime);
            }
            File.WriteAllLines(("plik.txt"), linesList.ToArray());
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}