namespace OOPLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Piramid piramid = new Piramid(Height: 75, Base: Matrix.RotateX(-1.57079633) * new RegularShape(50, 5));
            Console.WriteLine(piramid.Volum);
            
            PoleToDraw pole = new PoleToDraw(90, 30);
            
            pole.cam *= Matrix.Move(new Vector(0,0,0))*Matrix.RotateX(-0.3)*Matrix.RotateY(1.5);

            /*for (int i = 0; i < 75; i++)
            {
                pole.Draw(piramid);
                pole.cam *= Matrix.RotateY(0.2)*Matrix.RotateX(0.0);

                //Console.Clear();
                Console.Write(pole.Export());
                pole.Clear();
                //Thread.Sleep(200);
            }*/
            Poligon p ;

            for (int i = 3; i < 10; i++)
            {
                p = new RegularShape(70, i);
                for (int j = 0; j < 10; j++)
                {
                    pole.Draw(p);
                    pole.cam *= Matrix.RotateY(0.314);

                    Console.Clear();
                    Console.Write(pole.Export());
                    pole.Clear();
                    Thread.Sleep(200);
                }

            }
            
        }
    }
}