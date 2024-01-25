using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexsand
{
    public partial class Form1 
    {
        private Timer timer;
        public int x = 100;
        public List<List<Element>> simspace = new List<List<Element>>();
        public Vector2 simsize = new Vector2(25,25);
        //public Brush brush = new SolidBrush(Color.Red);
        public int particlesize = 12;
        public Form1()
        {
            InitializeComponent();
            for (int y = 0; y < simsize.x; y++)
            {
                simspace.Add(new List<Element>());
                for (int x = 0; x < simsize.y; x++) 
                {
                    simspace[simspace.Count - 1].Add(new Void());
                }
            }
            simspace[10][10] = new Sand();
            for (int y = 0;y < simsize.x; y++) 
            {
                simspace[y][simspace.Count-1] = new Rock();
            }


        }
        private void Updatesim()
        {
            for (int y = 0; y < simspace.Count; y++)
            {
                for (int x = 0; x < simspace[y].Count; x++)
                {

                    simspace[y][x].tick(new Vector2(y, x), simspace);
                }
            }
            foreach (var item in simspace)
            {
                foreach (var item1 in item)
                {
                    item1.Ticked = false;
                }
            }
        }
        private Point[] hexp(Vector2 center, int radius)
        {
            Point[] hexagonPoints = new Point[6];

            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3.0 * i;
                int x = (int)(center.x + radius * Math.Cos(angle));
                int y = (int)(center.y + radius * Math.Sin(angle));
                hexagonPoints[i] = new Point(x, y);
            }

            return hexagonPoints;
        }





        public void ReDraw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int hexRadius = particlesize;

            int hexWidth = (int)(Math.Sqrt(3) * hexRadius);
            int hexHeight = 2 * hexRadius;
            Brush brush0 = new SolidBrush(Color.Black);
            g.FillRectangle(brush0, 0, 0, 1000, 1000);
            for (int y = 0; y < simspace.Count; y++)
            {
                for (int x = 0; x < simspace[y].Count; x++)
                {
                    if (simspace[x][y].Id != 0)
                    {
                        Brush brush = new SolidBrush(simspace[x][y].Color);

                        int offsetX = x % 2 == 1 ? hexWidth / 2 : 0;

                        
                        Point[] hexagonPoints = hexp(new Vector2(x * hexWidth / 1.12f, y * hexWidth + offsetX), hexRadius);

                        g.FillPolygon(brush, hexagonPoints);
                    }

                }
            }
        }
    }
}
