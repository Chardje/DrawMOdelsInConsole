namespace OOPLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Piramid piramid = new Piramid(Height: 10, Base: new RegularShape(1, 3));
            Console.WriteLine(piramid.Volum);
        }
    }
}