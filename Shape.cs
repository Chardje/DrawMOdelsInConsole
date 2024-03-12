using System.Xml.Serialization;

namespace OOPLab2
{
    public class Poligon
    {
        public virtual double Area
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Points.Count; ++i)
                {
                    sum += this[i].X * this[i - 1].Y - this[i - 1].X * this[i].Y;
                }
                return sum;
            }
        }
        public List<Vector> Points;
        public Vector this[int n]
        {
            get
            {
                if (n >= 0 && n < Points.Count)
                {
                    return Points[n];
                }
                else if (n < 0)
                {
                    return Points[Points.Count + n];
                }
                else
                {
                    return Points[n % Points.Count];
                }
            }

            set
            {
                if (n >= 0 && n < Points.Count)
                {
                    Points[n] = value;
                }
                else if (n < 0)
                {
                    Points[Points.Count + n] = value;
                }
                else
                {
                    Points[n % Points.Count] = value;
                }
            }

        }

        internal virtual Vector Center
        {
            get
            {
                Vector sum = Vector.Zero;
                for (int i = 0; i < Points.Count; ++i)
                {
                    sum += this[i];
                }
                return sum / Points.Count;
            }
            
        }

        internal Vector Normal()
        {
            Vector a, b, c;
            a = this[0];
            b = this[1];
            c = this[2];
            Vector ab, bc;
            ab = a - b;
            bc = b - c;
            return Vector.CrossProduct(ab,bc).Norm;
        }

        
        internal Poligon(List<Vector> Points)
        {
            this.Points = Points;
        }
        internal Poligon()
        {            
        }

        //зміна полігону матрицею
        public static Poligon operator *(Matrix m, Poligon p)
        {
            List<Vector> Points=new List<Vector>();
            for (int i = 0;i< p.Points.Count; i++)
            {
                Points.Add(m*p.Points[i]);
            }
            return new Poligon(Points);
        }
    }

    public class RegularShape : Poligon
    {
        double radius;
        internal override Vector Center
        {
            get;            
        }

        public override double Area
        {
            get
            {
                return 0.5 * Points.Count * radius * radius * Math.Sin(Math.PI * 2 / Points.Count);
            }
        }

        public static RegularShape operator *(Matrix m, RegularShape s)
        {
            List<Vector> Points = new List<Vector>();
            for (int i = 0; i < s.Points.Count; i++)
            {
                Points.Add(m * s.Points[i]);
            }
            return new RegularShape(Points,s.radius,s.Center);
        }

        private RegularShape(List<Vector> Points,double R,Vector Center) :base(Points)
        {
            radius=R;
            this.Center = Center;
        }
        //конструктор 2
        public RegularShape(double R, int CountPoints) : this(R, CountPoints, Vector.Zero) { }

        //конструктор
        public RegularShape(double R, int CountPoints, Vector center)
        {
            Center=center;
            if (CountPoints < 2) return;
            Points = new List<Vector>(capacity: CountPoints);
            radius = R;

            //далі заповнюємо ліст координатами
            double angle = Math.PI * 2 / CountPoints;
            double sin, cos;
            for (int i = 0; i < CountPoints; ++i)
            {
                (sin, cos) = Math.SinCos(angle * i);
                Points.Add(new Vector(R * sin, R * cos, 0));
            }
        }
    }
}
