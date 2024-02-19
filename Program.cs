namespace OOPLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Piramid piramid = new Piramid(Height: 75, Base: Matrix.RotateX(-1.57079633) * new RegularShape(50, 8));
            Console.WriteLine(piramid.Volum);
            
            PoleToDraw pole = new PoleToDraw(30, 90);
            
            pole.cam *= Matrix.Move(new Vector(0,0,30));
            //pole.cam *= Matrix.RotateX(-0.5);

            for (int i = 0; i < 75; i++)
            {
                pole.Draw(piramid);
                pole.cam *= Matrix.RotateY(0.2);
                Console.Clear();
                Console.Write(pole.Export());
                pole.Clear();
                Thread.Sleep(200);
            }
            
        }
    }
}