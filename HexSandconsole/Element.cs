using SFML;
using SFML.Graphics;


namespace Hexsand
{
    public class Element
    {
        public virtual int Id { get; set; } = -1;
        public virtual Color Color { get; set; } = Color.Magenta;
        public virtual bool Ticked { get; set; } = false;


        public Element()
        {
            
        }

        // Parameterized constructor
        public Element(int id, Color color)
        {
            Id = id;
            Color = color;
        }
        public virtual void tick(Vector2 pos, List<List<Element>> simspace) 
        {

        }

    }
    public class Voider :Element 
    {
        
        public Voider() : base(id: 0, color: Color.Black)
        {
            
        }
    }

    public class Rock : Element
    {
        public Rock() : base(id: 1, color: new Color(90, 90, 90))
        {
            
        }
    }
    public class Sand : Element
    {
        public Sand() : base(id: 2, color: new Color(217, 195, 90))
        {
            
        }
        public override void tick(Vector2 pos, List<List<Element>> simspace) 
        {
            if (Ticked==false)
            {
                //Console.WriteLine("update");
                if (simspace[(int)pos.x][(int)pos.y + 1].Id == 0)
                {
                    //Console.WriteLine("update2");
                    simspace[(int)pos.x][(int)pos.y + 1] = this;
                    simspace[(int)pos.x][(int)pos.y] = new Voider();
                }
                Ticked = true;
            }
        }
    }
}
