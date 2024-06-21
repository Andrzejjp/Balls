using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Collections.Specialized;

namespace Balls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Vector2 location = new Vector2();
        Vector2 velocity = new Vector2();

        private void Form1_Load(object sender, EventArgs e)
        {
            location.X = this.Width / 2;
            location.Y = this.Height / 2;
            velocity.X = 4;
            velocity.Y = 1;

            this.DoubleBuffered = true;
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            location = location + velocity;
            if (location.X<0 || location.X>this.Width-130)
            {
                velocity.X = velocity.X * -1;
            }
            if (location.Y<0 || location.Y > this.Height - 160)
            {
                velocity.Y = velocity.Y * -1;
            }
            e.Graphics.FillEllipse(Brushes.BlueViolet, location.X, location.Y, 120, 120);
        }
    }
}
