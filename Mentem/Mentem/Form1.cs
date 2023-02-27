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

            pics[0] = pictureBox1;
            pics[1] = pictureBox2;
            pics[2] = pictureBox3;
            pics[3] = pictureBox4;
            pics[4] = pictureBox5;
            pics[5] = pictureBox6;
            pics[6] = pictureBox7;
            pics[7] = pictureBox8;
            pics[8] = pictureBox9;
            pics[9] = pictureBox10;
            pics[10] = pictureBox11;
            pics[11] = pictureBox12;
            pics[12] = pictureBox13;
            pics[13] = pictureBox14;
            pics[14] = pictureBox15;
            pics[15] = pictureBox16;

            covers[0] = pictureBox17;
            covers[1] = pictureBox18;
            covers[2] = pictureBox19;
            covers[3] = pictureBox20;
            covers[4] = pictureBox21;
            covers[5] = pictureBox22;
            covers[6] = pictureBox23;
            covers[7] = pictureBox24;
            covers[8] = pictureBox25;
            covers[9] = pictureBox26;
            covers[10] = pictureBox27;
            covers[11] = pictureBox28;
            covers[12] = pictureBox29;
            covers[13] = pictureBox30;
            covers[14] = pictureBox31;
            covers[15] = pictureBox32;
        }

        bool clicked = false;
        PictureBox[] pics = new PictureBox[16];
        PictureBox[] covers = new PictureBox[16];
        PictureBox firstpic, firstcover;
        string path = "C:\\Users\\TheBoss\\OneDrive\\Desktop\\Atestate\\Mentem\\Mentem\\Resources\\";

        private void Form1_Load(object sender, EventArgs e)
        {
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
    }
}
