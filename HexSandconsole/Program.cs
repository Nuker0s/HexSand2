using Hexsand;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Hexsandconsole
{
    public class Program
    {
        public int x = 100;
        public List<List<Element>> simspace = new List<List<Element>>();
        public Vector2 simsize = new Vector2(25, 25);
        public int particlesize = 12;
        

        #region Inits
        public static void Main(string[] args)
        {
            Console.WriteLine("debuggg");
            // Create an instance of the Program class
            Program myProgram = new Program();

            // Call the instance method to run the program
            myProgram.Initialize();
            
            // Add SFML window setup and execution code here

        }
        public void Initialize()
        {
            var window = new RenderWindow(new VideoMode(800, 800), "SFML Window");
            for (int y = 0; y < simsize.x; y++)
            {
                simspace.Add(new List<Element>());
                for (int x = 0; x < simsize.y; x++)
                {
                    simspace[simspace.Count - 1].Add(new Voider());
                }
            }
            simspace[3][3] = new Sand();
            for (int y = 0; y < simsize.x; y++)
            {
                simspace[y][simspace.Count - 1] = new Rock();
            }

            while (window.IsOpen)
            {
                Update(window);
            }

        }
        #endregion
        #region events
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
            
        }
        #endregion
 
        public void Update(RenderWindow window) 
        {
            Simulate();
            Draw(window);
            Thread.Sleep(500);
            
        }
        public void Simulate() 
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

        public void Draw(RenderWindow window)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);

            int hexRadius = particlesize;

            int hexWidth = (int)(Math.Sqrt(3) * hexRadius);

            for (int y = 0; y < simspace.Count; y++)
            {
                for (int x = 0; x < simspace[y].Count; x++)
                {
                    if (simspace[x][y].Id != 0)
                    {
                        Color fillColor = simspace[x][y].Color;

                        int offsetX = x % 2 == 1 ? hexWidth / 2 : 0;

                        Vector2f[] hexagonPoints = hexp(new Vector2f(x * hexWidth / 1.12f, y * hexWidth + offsetX), hexRadius);


                        VertexArray hexagon = new VertexArray(PrimitiveType.TriangleFan);

                        hexagon.Append(new Vertex(new Vector2f(hexagonPoints[0].X, hexagonPoints[0].Y), fillColor));

                        for (int i = 1; i < 6; i++)
                        {
                            hexagon.Append(new Vertex(new Vector2f(hexagonPoints[i].X, hexagonPoints[i].Y), fillColor));
                        }

                        window.Draw(hexagon);
                    }
                }
            }

            window.Display();
        }
        private Vector2f[] hexp(Vector2f center, int radius)
        {
            Vector2f[] hexagonPoints = new Vector2f[6];

            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3.0 * i;
                float x = (float)(center.X + radius * Math.Cos(angle));
                float y = (float)(center.Y + radius * Math.Sin(angle));
                hexagonPoints[i] = new Vector2f(x, y);
            }

            return hexagonPoints;
        }



    }
}
