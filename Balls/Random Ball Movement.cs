using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Balls
{
    public partial class Random_Ball_Movement : Form
    {
        Mover mover;
        List<Mover> movers = new List<Mover>();
        bool repell = false;
        bool spawnballs = false;
        int ballCount;
        public Random_Ball_Movement()
        {
            InitializeComponent();
        }

        private void Random_Ball_Movement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            this.Paint += Random_Ball_Movement_Paint;
            for (int i = 0; i < 999; i++)
            {
                mover = new Mover(this.Width, this.Height, this);
                movers.Add(mover);
            }
            foreach (Mover ball in movers)
            {
                ballCount++;
            }
        }

        private void Random_Ball_Movement_Paint(object sender, PaintEventArgs e)
        {
            //mover.Update();
            //mover.Display(e.Graphics);
            foreach (Mover ball in movers)
            {
                ball.Update();
                ball.Display(e.Graphics);
            }
        }

        
        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            float screenMag = Convert.ToInt32(Math.Sqrt(this.Width*this.Width+this.Height*this.Height)/3);
            Vector2 normalisedVelocity;
            float inverseConversion;
            float vectorX;
            float vectorY;
            label1.Text = $"Balls: {ballCount}";
            if (repell)
            {
                foreach (Mover ball in movers)
                {
                    vectorX = ball.mouse.X - ball.location.X;
                    vectorY = ball.mouse.Y - ball.location.Y;
                    if(vectorX < 1 && vectorX > -1)
                    {
                        vectorX = 1f;
                    }
                    if (vectorY < 1 && vectorY > -1)
                    {
                        vectorY = 1f;
                    }
                    normalisedVelocity = new Vector2(vectorX / screenMag, vectorY / screenMag);
                    inverseConversion = (screenMag / Convert.ToInt32(Math.Sqrt(vectorX*vectorX+vectorY*vectorY))-1f);
                    ball.velocity += Vector2.Multiply(-(inverseConversion* inverseConversion), normalisedVelocity);
                }
            }
            if (spawnballs)
            {
                mover = new Mover(this.Width, this.Height, this);
                movers.Add(mover);
                ballCount++;
            }
        }

        private void Random_Ball_Movement_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Random_Ball_Movement_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                repell = true;
            }
            if(e.KeyCode == Keys.B && spawnballs == false)
            {
                spawnballs = true;
            }
            else if(e.KeyCode == Keys.B)
            {
                spawnballs = false;
            }
        }

        private void Random_Ball_Movement_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                repell = false;
            }
        }
    }
}
