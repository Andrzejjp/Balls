using System;
using System.Drawing;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Balls
{
    class Mover
    {
        static Random rnd = new Random();
        public Vector2 location;
        public Vector2 velocity;
        int formWidth, formHeight;
        Color colour = Color.Black;
        public Vector2 acceleration;
        public Vector2 mouse = new Vector2();
        float topSpeed;
        Random_Ball_Movement frm;
        

        public Mover(int width,int height, Random_Ball_Movement theForm)
        {
            formHeight = height;
            formWidth = width;
            location = new Vector2(rnd.Next(width-150), rnd.Next(height-150));
            velocity = new Vector2(0,0);
            acceleration = new Vector2((float)-0.001, (float)-0.001);
            frm = theForm;
            topSpeed = 10;
        }

        float Mag(Vector2 theVector)
        {
            return (float)Math.Sqrt(theVector.X * theVector.X + theVector.Y * theVector.Y);
        }

        float distance(Vector2 ball,Point mouse)
        {
            Vector2 screensize = new Vector2(formWidth,formHeight);
            Vector2 distance = new Vector2(ball.X-mouse.X,ball.Y-mouse.Y);
            return (Mag(distance)/Mag(screensize));
        }
        float Fix(float multi)
        {
            if (multi < 1 && multi > 0)
            {
                return multi;
            }
            else
            {
                return 0.99f;
            }
            
        }

        public Vector2 Limit(float theLimit, Vector2 theVector)
        {
            if (Mag(theVector) > theLimit)
            {
                theVector.X = theVector.X * theLimit / Mag(theVector);
                theVector.Y = theVector.Y * theLimit / Mag(theVector);
            }
            return theVector;
        }

        public void Update()
        {
            Point cp = frm.PointToClient(Cursor.Position);
            mouse.X = cp.X; mouse.Y = cp.Y;
            Vector2 dir = Vector2.Subtract(mouse, location);
            dir = Vector2.Normalize(dir);
            dir = Vector2.Multiply(0.25f, dir);
            acceleration = dir;
            velocity = Vector2.Add(velocity, acceleration);
            velocity = Limit(topSpeed, velocity);
            location = Vector2.Add(location,velocity);
            float multipicative = 1f-distance(location, cp);
            float mult2 = 1f-(multipicative * multipicative* multipicative * multipicative);
            mult2 = Fix(mult2);
            colour = Color.FromArgb(Convert.ToInt32(255-230*mult2),20, Convert.ToInt32(25 + 230 * mult2));
        }

        public void Display(Graphics e)
        {
            SolidBrush brush = new SolidBrush(colour);
            e.FillEllipse(brush, location.X, location.Y, 15, 15);
        }
    }
}
