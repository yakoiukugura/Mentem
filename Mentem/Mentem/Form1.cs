using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mentem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool clicked = false;
        PictureBox[] pics = new PictureBox[16];  // vector pentru imaginile jocului
        PictureBox[] covers = new PictureBox[16];  // vector pentru copertile jocului
        PictureBox firstpic, firstcover;  // variabile pentru a stoca prima imagine selectata
        string path = "Spune-i lui Denis sa te ajute aici";  // calea catre imaginile jocului

        // functia apelata la apasarea butonului de start
        private void start_button_Click(object sender, EventArgs e)
        {
            // dezactiveaza si ascunde elementele din meniu
            menu.Enabled = false;
            menu.Visible = false;

            // dezactiveaza si ascunde titlul jocului
            title.Enabled = false;
            title.Visible = false;

            // dezactiveaza si ascunde butoanele Start si Quit
            start_button.Enabled = false;
            start_button.Visible = false;
            quit_button.Enabled = false;
            quit_button.Visible = false;

            // initializeaza jocul
            pics = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8,
                                      pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16};

            covers = new PictureBox[] { pictureBox17, pictureBox18, pictureBox19, pictureBox20, pictureBox21, pictureBox22, pictureBox23, pictureBox24,
                                        pictureBox25, pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30, pictureBox31, pictureBox32};

            shuffle_cards();  // amesteca cartile
            timer1.Enabled = true;  // porneste cronometrul
        }

        // functia apelata la apasarea butonului de Quit
        private void quit_button_Click(object sender, EventArgs e)
        {
            // afiseaza o fereastra de dialog pentru confirmarea iesirii din joc
            if (MessageBox.Show("Are you sure you want to quit?", "Quit Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        // functia apelata la apasarea butonului de Cancel
        private void cancel_button_Click(object sender, EventArgs e)
        {
            // activeaza si afiseaza elementele din meniu
            menu.Enabled = true;
            menu.Visible = true;

            // activeaza si afiseaza titlul jocului
            title.Text = "Mentem";
            title.Enabled = true;
            title.Visible = true;

            // activeaza si afiseaza butoanele Start si Quit
            start_button.Text = "Start";
            start_button.Enabled = true;
            start_button.Visible = true;
            quit_button.Enabled = true;
            quit_button.Visible = true;

            timer1.Enabled = false;  // opreste cronometrul
        }

        // functia apelata la apasarea butonului de Shuffle
        private void shuffle_button_Click(object sender, EventArgs e)
        {
            shuffle_cards();  // amesteca cartile
        }

        /*  Functia shuffle_cards() se apeleaza cand jucatorul da click pe butonul "Shuffle" si amesteca cartile. 
        Pentru a amesteca, mai intai se reseteaza imaginile si tag-urile tuturor cartilor, se afiseaza copertile si se seteaza 
        variabila clicked la false. Apoi, pentru fiecare imagine, se alege un numar aleatoriu intre 0 si 15 si se pune imaginea 
        curenta pe pozitia respectiva, daca pozitia nu a fost deja ocupata de alta imagine. */
        private void shuffle_cards()
        {
            for (int i = 0; i < 16; i++)
            {
                pics[i].Image = null;
                pics[i].Tag = null;
                covers[i].Visible = true;
                covers[i].Enabled = true;
                clicked = false;
            }
            for (int i = 1; i <= 8; i++)
            {
                Random rand = new Random();
                int nr = rand.Next(0, 16);
                while(pics[nr].Image != null)
                    nr = rand.Next(0, 16);
                pics[nr].Tag = i;
                pics[nr].Image = Image.FromFile(path + "img" + i + ".jpg");
                while (pics[nr].Image != null)
                    nr = rand.Next(0, 16);
                pics[nr].Tag = i;
                pics[nr].Image = Image.FromFile(path + "img" + i + ".jpg");
            }
        }


        /*  Functia pictureBox17_Click() este apelata cand jucatorul da click pe una din copertile de pe ecran. 
        Se determina care coperta a fost apasata, se ascunde si se seteaza variabila clicked la true. Daca aceasta 
        este prima coperta apasata, se retine imaginea si coperta curenta. Daca este a doua coperta apasata, se 
        verifica daca tag-ul imaginii de sub coperta curenta este acelasi cu cel al primei imagini. Daca nu, dupa o 
        scurta asteptare se afiseaza din nou ambele coperti si se reseteaza variabila clicked. Daca da, atunci doar se reseteaza 
        variabila clicked. */
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            picture.Visible = false;
            picture.Enabled = false;
            int i;
            for (i = 0; i < 16; i++)
                if (covers[i] == picture)
                    break;
            if (!clicked)
            {
                firstpic = pics[i];
                firstcover = covers[i];
                clicked = true;
            }
            else if (Convert.ToInt32(firstpic.Tag) != Convert.ToInt32(pics[i].Tag))
            {
                wait(1000);
                covers[i].Visible = true;
                covers[i].Enabled = true;
                firstcover.Visible = true;
                firstcover.Enabled = true;
                clicked = false;
            }
            else
            {
                clicked = false;
            }
        }

        /*  Functia timer1_Tick() este apelata la fiecare interval de timp setat pe timer si verifica daca mai 
        exista vreo coperta vizibila pe ecran. Daca nu exista, se opreste timer-ul si se afiseaza un mesaj de "You Win!". */
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool end = true;
            for (int i = 0; i < 16; i++)
                if (covers[i].Visible == true)
                    end = false;
            if (end)
            {
                timer1.Enabled = false;
                MessageBox.Show("You Win!");
                this.Close();
            }
        }


        /*  Functia wait() face o asteptare sincrona in timpul executiei aplicatiei pentru o anumita perioada de timp
        specificata in milisecunde.Aceasta este utilizata pentru a amana afisarea copertilor in cazul in care
        jucatorul a apasat doua carti diferite.Aceasta asteptare se face intr-un bucla while in care se ruleaza
        continuu metoda Application.DoEvents(), astfel incat aplicatia sa poate continua sa functioneze in timp ce asteapta.
        https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program*/
        public void wait(int milliseconds)
        {
            this.Enabled = false;
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                this.Enabled = true;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
