namespace OOPLab2
{
    class Model
    {
        public List<Vector> points
        {
            get;
            init;
        }
        public List<Ray> edges
        {
            get;
            init;
        }
        public List<Poligon> poligons
        {
            get;
            init;
        }
        ~Model()
        {
            Console.WriteLine("Model Destroed");
        }
    }
    internal class Piramid:Model
    {
        public List<Vector> points
        {
            get;
            init;
        }
        //висота
        double height;
        // Gлоща основи
        double baseArea;
        public double Volum { get => height * baseArea / 3; }
        public Piramid(double Height, Poligon Base)
        {
            height = Height;
            baseArea = Base.Area;

            points = new List<Vector>();
            points.AddRange(Base.Points);
            points.Add(Base.Centr() + (height* Base.Normal()));

        }
        ~Piramid()
        {
            Console.WriteLine("Piramid is destroyed");
        }
    }
}
