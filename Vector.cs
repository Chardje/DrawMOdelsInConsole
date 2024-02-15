using System.Drawing;

namespace OOPLab2
{
    public class Vector : ICloneable
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Vector CrossProduct (Vector v1, Vector v2)
        {
            return new Vector(v1.Y* v2.Z-v2.Y*v1.Z, v1.X * v2.Z - v2.X * v1.Z, v1.X*v2.Y-v2.X*v1.Y);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vector operator *(double s, Vector v)
        {
            return new Vector(s * v.X, s * v.Y, s * v.Z);
        }
        public static Vector operator /(Vector v, double s)
        {
            return new Vector(v.X / s, v.Y / s, v.Z / s);
        }
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        /// <summary>
        /// Length of the vector
        /// </summary>
        public double Length => System.Math.Sqrt(this * this);

        /// <summary>
        /// Normalized vector, collinear to the current
        /// </summary>
        public Vector Norm
        {
            get
            {
                double l = Length;
                return new Vector(X / l, Y / l, Z / l);
            }
        }

        /// <summary>
        /// Scalar Product
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Scalar Product</returns>
        public static double operator *(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
    }
    
    public class Ray
    {
        
        private readonly Vector[] points = new Vector[2];
        
        public Ray(Vector a, Vector b)
        {
            points[0] = a;
            points[1] = b;
        }
        public Vector Direction()
        {
            return points[1]-points[0];
        }
        
        public Vector this[int index]
        {
            get
            {
                return points[index];
            }
        }
        public int Count => 2;
    }
    class Edge : Ray
    {
        ConsoleColor color;
        public Edge(Vector a, Vector b, ConsoleColor color) : base(a, b)
        {
            this.color = color;
        }
        
    }
}