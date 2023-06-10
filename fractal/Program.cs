using System;
using System.Drawing;
using System.Windows.Forms;

public class KochCurveFractal : Form
{
    int depth = 3;

    Button increaseButton;
    Button decreaseButton;

    public KochCurveFractal()
    {
        this.Size = new Size(800, 600);

        increaseButton = new Button();
        increaseButton.Text = "Increase";
        increaseButton.BackColor= Color.Green;
        increaseButton.Location = new Point(10, 20);
        increaseButton.Size = new Size(80, 40);
        increaseButton.Click += new EventHandler(IncreaseDepth);

        decreaseButton = new Button();
        decreaseButton.Text = "Decrease";
        decreaseButton.BackColor= Color.Red;
        decreaseButton.Location = new Point(100, 20);
        decreaseButton.Size = new Size(80, 40);
        decreaseButton.Click += new EventHandler(DecreaseDepth);

        this.Controls.Add(increaseButton);
        this.Controls.Add(decreaseButton);

        this.Paint += new PaintEventHandler(DrawKochCurve);
    }

    private void DrawKochCurve(object sender,PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        Pen pen = new Pen(Color.Black);
        pen.Width = 1;

        PointF p1 = new PointF(50, Height / 2);
        PointF p2 = new PointF(Width - 50, Height / 2);
        DrawFractalKochCurve(graphics, pen, p1, p2, depth);
    }

    private void DrawFractalKochCurve(Graphics graphics, Pen pen, PointF p1, PointF p2, int depth)
    {
        if (depth == 0)
        {
            graphics.DrawLine(pen, p1, p2);
        }
        else
        {
            PointF p3 = new PointF(p1.X + (p2.X - p1.X) / 3, p1.Y + (p2.Y - p1.Y) / 3);
            PointF p4 = new PointF(p1.X + (p2.X - p1.X) * 2 / 3, p1.Y + (p2.Y - p1.Y) * 2 / 3);
            PointF p5 = new PointF((float)(p3.X + (p4.X - p3.X) * Math.Cos(Math.PI / 3) + (p4.Y - p3.Y) * Math.Sin(Math.PI / 3)),
                                    (float)(p3.Y - (p4.X - p3.X) * Math.Sin(Math.PI / 3) + (p4.Y - p3.Y) * Math.Cos(Math.PI / 3)));

            DrawFractalKochCurve(graphics, pen, p1, p3, depth - 1);
            DrawFractalKochCurve(graphics, pen, p3, p5, depth - 1);
            DrawFractalKochCurve(graphics, pen, p5, p4, depth - 1);
            DrawFractalKochCurve(graphics, pen, p4, p2, depth - 1);
        }
    }

    private void IncreaseDepth(object sender, EventArgs e)
    {
        depth++;
        this.Invalidate(); 
    }

    private void DecreaseDepth(object sender, EventArgs e)
    {
        if (depth > 0)
        {
            depth--;
            this.Invalidate(); 
        }
    }

    public static void Main()
    {
        Application.Run(new KochCurveFractal());
    }
}
