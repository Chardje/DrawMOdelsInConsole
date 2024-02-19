namespace OOPLab2
{
    class Model
    {
        public List<Vector> points
        {
            get;
            init;
        }
        public List<Edge> edges
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

            edges = new List<Edge>();
            for (int i = 0; i < points.Count-1; i++)
            {
                edges.Add(new Edge(points[i], points[i+1],'='));
                edges.Add(new Edge(points[i], points.Last(), '1'));
            }
            edges.Add(new Edge(points[0], points[points.Count-2], '='));

        }
        ~Piramid()
        {
            Console.WriteLine("Piramid is destroyed");
        }
    }
}
