using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketGame
{
    public partial class Form1 : Form
    {
        Body[] bodies =
          { new Body(1, new Vector2D(0d,0d), new Vector2D(0d,0d), "Ball1")
            , new Body(1, new Vector2D(0,0), new Vector2D(0,0), "Ball2")
        };

        public double zoom = 1;
        public double alfax = 0;
        public double alfay = 0;
        public double force = 100;

        public int points = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            alfax = Math.Cos(Math.PI * Convert.ToDouble(textBox1.Text) / 180);
            alfay = Math.Cos(Math.PI * (90- Convert.ToDouble(textBox1.Text)) / 180);

            label1.Text = alfay.ToString();

            force = Convert.ToDouble(textBox2.Text);

            bodies[0] = new Body(1, new Vector2D(0d, 0d), new Vector2D(force * alfax, force * alfay), "Ball1");
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Body.Gravity(bodies[0]);
            Body.Gravity(bodies[1]);

            bodies[0].Move(0.05);

            radioButton1.Left = Convert.ToInt32(bodies[0].pos.x / zoom + 60 + 10);
            radioButton1.Top = -1* Convert.ToInt32(bodies[0].pos.y / zoom - 430 - 10);


            label1.Text = bodies[0].pos.x.ToString("0.##"); //label a writes the instantaneous x position of the object
            label2.Text = bodies[0].pos.y.ToString("0.##"); //label a writes the instantaneous y position of the object


            label2.Text = (Convert.ToDouble(label2.Text) + 0.01).ToString();


            if(bodies[0].pos.x > 1070)
            { 
               timer1.Enabled = false;           // When the counter stops, points are added to the scoreboard according to the position of the object.

                if (bodies[0].pos.y > -50 && bodies[0].pos.y < 50)
                {
                    points += 10;
                    label7.Text = points.ToString();
                }
                else if (bodies[0].pos.y > 50 && bodies[0].pos.y < 100)
                {
                    points += 5;
                    label7.Text = points.ToString();
                }
                else if(bodies[0].pos.y > 100 && bodies[0].pos.y < 200)
                {
                    points += 5;
                    label7.Text = points.ToString();
                }
                else if (bodies[0].pos.y < -50 && bodies[0].pos.y > -100)
                {
                    points += 5;
                    label7.Text = points.ToString();
                }
                else if (bodies[0].pos.y < -100 && bodies[0].pos.y > -200)
                {
                    points += 2;
                    label7.Text = points.ToString();
                }
                else
                {
                    MessageBox.Show("SHOT FAILED");  //If the object does not reach the target range, the text (shot failed) will appear.
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;      //When the button is pressed, the counter and the object stop.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}

class Body
{
    public double mass; // mass
    public Vector2D pos, vel; // position, velocity
    private Vector2D acc; // acceleration
    public string name; // a naming
    public Body(double mass_, Vector2D pos_, Vector2D vel_, string name_ = "Body")
    {
        acc = new Vector2D(0, 0);
        mass = mass_;
        vel = vel_;
        pos = pos_;
        name = name_;
    }
    public void AddForce(Vector2D F)
    {
        acc += (1d / mass) * F;
    }
    public void Move(double dt)
    {
        pos += vel * dt + .5d * dt * dt * acc;
        vel += acc * dt;
        acc *= 0;
    }

    public static void Gravity(Body b1)
    {
        Vector2D F = new Vector2D(0, -9.87f * b1.mass);
        b1.AddForce(F);
    }
}
class Vector2D
{
    public double x, y;
    public Vector2D(double x_, double y_)
    {
        x = x_;
        y = y_;
    }
    public static Vector2D operator +(Vector2D v1, Vector2D v2)
    {
        return new Vector2D(v1.x + v2.x, v1.y + v2.y);
    }
    public static Vector2D operator -(Vector2D v1, Vector2D v2)
    {
        return new Vector2D(v1.x - v2.x, v1.y - v2.y);
    }
    public static Vector2D operator *(double k, Vector2D v)
    {
        return new Vector2D(v.x * k, v.y * k);
    }
    public static Vector2D operator *(Vector2D v, double k)
    {
        return new Vector2D(v.x * k, v.y * k);
    }
    public override string ToString()
    {
        return "x: " + x + " y: " + y;
    }
}