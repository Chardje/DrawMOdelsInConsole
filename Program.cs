namespace OOPLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Piramid piramid = new Piramid(Height: 75, Base: new RegularShape(50, 5));
            Console.WriteLine(piramid.Volum);
            /*
            Vector v = new Vector(-2, 10, 6);
            Vector v1 = Matrix.OrtoganalMatrix(-5, 5, -5, 5, -5, 5)*v;
            Console.WriteLine(v1.X+" "+ v1.Y + " "+ v1.Z + " ");
            */
            PoleToDraw pole = new PoleToDraw(30, 120);
            //pole.DrawLine(new Ray(piramid.points[0], piramid.points[1]), '@');
             pole.cam *= Matrix.Move(new Vector(0,0,30));
            pole.cam *= Matrix.RotateX(-0.5);

            for (int i = 0; i < 75; i++)
            {

                pole.Draw(piramid);
                pole.cam *= Matrix.RotateZ(0.2);
                pole.Export();
                pole.Cear();
                Thread.Sleep(200);
                //Console.Clear();
            }
            
        }
    }
}