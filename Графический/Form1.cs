using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Графический
{
    public partial class Form1 : Form
    {

        int x1, y1;
        int nachx, nachy;
        int zaliv = 0;
        string tip = "линия";
        Bitmap pic;
        Bitmap pic1;
        ColorDialog col = new ColorDialog();
        Pen p = new Pen(Color.Black);
        SolidBrush zal = new SolidBrush(Color.Black);
        Graphics g;
        Graphics g1;
        string path;

        public Form1()
        {
            InitializeComponent();
            pic = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            pic1 = new Bitmap(pictureBox1.Width,pictureBox1.Height);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e) //сохранить как
        {
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.DefaultExt = "*.jpg";
            saveFileDialog1.Filter = "JPG Files|*.jpg";

            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName.Length > 0)
            {
                pic.Save(saveFileDialog1.FileName);
                path = saveFileDialog1.FileName;
            }          
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) //открыть
        {           
            openFileDialog1 = new OpenFileDialog();

            openFileDialog1.DefaultExt = "*.jpg";
  
            if(openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pic1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);              
                pic = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                //pictureBox1.Image = pic;
                path = null;
            }          
        }
  
        private void button6_Click(object sender, EventArgs e) //цвет кисти
        {
            if (col.ShowDialog() == DialogResult.OK)
            {
                p.Color = col.Color;
                zal.Color = col.Color;
                button6.BackColor = col.Color;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //размер
        {
            if (textBox1.Text.Length > 0)
            {
                p.Width = Convert.ToInt32(textBox1.Text);
                p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            }
        }

        private void button3_Click(object sender, EventArgs e) //круг
        {
            tip = "эллипс";
        }

        private void button5_Click(object sender, EventArgs e) //линия
        {
            tip = "линия";
        }

        private void button4_Click(object sender, EventArgs e) //прямоугольник
        {
            tip = "прямоугольник";
        }

        /*public void Zalivka(int x3, int y3) // функция заливки
        {
            if (x3 >= pictureBox1.Width - 1) return;
            if (x3 < 1) return;
            if (y3 >= pictureBox1.Height - 1) return;
            if (y3 < 1) return;
           
            g.DrawLine(new Pen(zal.Color), x3, y3, x3, y3 + 0.5f);
            Bitmap pic1 = (Bitmap)pictureBox1.Image;

            if (pic3.GetPixel(x3 + 1, y3) != button6.BackColor && pic3.GetPixel(x3 + 1, y3) != button1.BackColor)
            {
                Zalivka(x3 + 1, y3);
            }
            if (pic.GetPixel(x3 - 1, y3) != button6.BackColor && pic.GetPixel(x3 - 1, y3) != button1.BackColor)
            {
                Zalivka(x3 - 1, y3);
            }
            if (pic.GetPixel(x3, y3 + 1) != button6.BackColor && pic.GetPixel(x3, y3 + 1) != button1.BackColor)
            {
                Zalivka(x3, y3 + 1);
            }
            if (pic.GetPixel(x3, y3 - 1) != button6.BackColor && pic.GetPixel(x3, y3 - 1) != button1.BackColor)
            {
                Zalivka(x3, y3 - 1);
            }
        }*/

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) //отрисовка фигур на основной слой
        {
            if (zaliv == 0)
            {
                if (tip == "прямоугольник")
                {
                    int x, y;
                    x = nachx;
                    y = nachy;
                    if (x > e.X)
                    {
                        x = e.X;
                    }
                    if (y > e.Y)
                    {
                        y = e.Y;
                    }
                    g.DrawRectangle(p, x, y, Math.Abs(e.X - nachx), Math.Abs(e.Y - nachy));
                }

                if (tip == "эллипс")
                {
                    g.DrawEllipse(p, nachx, nachy, e.X - nachx, e.Y - nachy);
                }
            }
            else
            {
                if (tip == "прямоугольник")
                {
                    int x, y;
                    x = nachx;
                    y = nachy;
                    if (x > e.X)
                    {
                        x = e.X;
                    }
                    if (y > e.Y)
                    {
                        y = e.Y;
                    }
                    g.FillRectangle(zal, x, y, Math.Abs(e.X - nachx), Math.Abs(e.Y - nachy));
                }

                if (tip == "эллипс")
                {
                    g.FillEllipse(zal, nachx, nachy, e.X - nachx, e.Y - nachy);
                }
            }

            g1.DrawImage(pic, 0, 0);
            pictureBox1.Image = pic1;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) //новый
        {
            pictureBox1.Image = null;
            pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pic1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            path = null;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton check = sender as RadioButton;

            if(check.Checked)
            {
                zaliv = 1;
            }

            else
            {
                zaliv = 0;
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
   
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) // сохранить
        {
            if (path != null)
            {
                pic.Save(path);
            }
            else
            {
                saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.DefaultExt = "*.jpg";
                saveFileDialog1.Filter = "RTF Files|*.jpg";

                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName.Length > 0)
                {
                    pic.Save(saveFileDialog1.FileName);
                    path = saveFileDialog1.FileName;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            nachx = e.X;
            nachy = e.Y;

            if (e.Button == MouseButtons.Right)
            {
                //Zalivka(e.X, e.Y);
            }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //рисование
        {

            if (e.Button == MouseButtons.Left)
            {
                g = Graphics.FromImage(pic);
                g1 = Graphics.FromImage(pic1);

                if (tip == "линия")
                {
                    g.DrawLine(p, x1, y1, e.X, e.Y);                    
                }

                if (zaliv == 0)
                {
                    if (tip == "эллипс")
                    {
                        g1.Clear(Color.White);
                        g1.DrawEllipse(p, nachx, nachy, e.X - nachx, e.Y - nachy);
                    }

                    if (tip == "прямоугольник")
                    {
                        g1.Clear(Color.White);
                        int x, y;
                        x = nachx;
                        y = nachy;
                        if (x > e.X)
                        {
                            x = e.X;
                        }
                        if (y > e.Y)
                        {
                            y = e.Y;
                        }
                        g1.DrawRectangle(p, x, y, Math.Abs(e.X - nachx), Math.Abs(e.Y - nachy));
                    }
                }
                else
                {
                    if (tip == "эллипс")
                    {
                        g1.Clear(Color.White);
                        g1.FillEllipse(zal, nachx, nachy, e.X - nachx, e.Y - nachy);
                    }

                    if (tip == "прямоугольник")
                    {
                        g1.Clear(Color.White);
                        int x, y;
                        x = nachx;
                        y = nachy;
                        if (x > e.X)
                        {
                            x = e.X;
                        }
                        if (y > e.Y)
                        {
                            y = e.Y;
                        }
                        g1.FillRectangle(zal, x, y, Math.Abs(e.X - nachx), Math.Abs(e.Y - nachy));
                    }
                }
                g1.DrawImage(pic, 0, 0);
                pictureBox1.Image = pic1;

            }

            x1 = e.X;
            y1 = e.Y;
        }
    }
}
