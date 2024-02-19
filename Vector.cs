using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace OOPLab2
{
    public class Vector : ICloneable
    {
        public static readonly Vector Zero = new Vector(0, 0, 0);

        public double[] cords;
        public double X { get => cords[0]; set => cords[0] = value; }
        public double Y { get => cords[1]; set => cords[1] = value; }
        public double Z { get => cords[2]; set => cords[2] = value; }
        public Vector(double x, double y, double z)
        {
            cords = new double[] { x, y, z, 1, };
        }
        public Vector(double[] cords)
        {
            this.cords = cords;
        }
        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            return new Vector(v1.Y * v2.Z - v2.Y * v1.Z, v1.X * v2.Z - v2.X * v1.Z, v1.X * v2.Y - v2.X * v1.Y);
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
            return points[1] - points[0];
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
        public char color;
        public Edge(Vector a, Vector b, char color) : base(a, b)
        {
            this.color = color;
        }

    }

    public class Matrix
    {
        public static readonly Matrix I = new Matrix(MakeIArray());

        private readonly double[,] cords;

        private Matrix(double[,] cords)
        {
            this.cords = cords;
        }
        /// <summary>
        /// Об'єднання матриць
        /// </summary>
        /// <param name="a">матриця 1</param>
        /// <param name="b">матриця 2</param>
        /// <returns>Множення матриць</returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            double[,] ac = a.cords;
            double[,] bc = b.cords;
            double[,] result = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    double s = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        s += ac[i, k] * bc[k, j];
                    }
                    result[i, j] = s;
                }
            }
            return new Matrix(result);
        }

        /// <summary>
        /// Зміна вектора через матрицю
        /// </summary>
        /// <param name="a">Матриця</param>
        /// <param name="b">Вектор</param>
        /// <returns>Змінений вектор</returns>
        public static Vector operator *(Matrix a, Vector b)
        {
            double[,] ac = a.cords;
            double[] bc = b.cords;
            double[] result = new double[4];
            for (int i = 0; i < 4; i++)
            {
                double s = 0;
                for (int k = 0; k < 4; k++)
                {
                    s += ac[i, k] * bc[k];
                }
                result[i] = s;
            }
            return new Vector(result);
        }
        /// <summary>
        /// Нульова матриця
        /// </summary>
        /// <returns></returns>
        private static double[,] MakeIArray()
        {
            double[,] result = new double[4, 4];
            result[0, 0] = 1;
            result[1, 1] = 1;
            result[2, 2] = 1;
            result[3, 3] = 1;
            return result;
        }
        /// <summary>
        /// Ортоганальна матриця
        /// </summary>
        /// <returns></returns>
        public static Matrix OrtoganalMatrix(double l, double r, double b, double t, double n, double f)
        {
            double[,] result = new double[4, 4];
            result[0, 0] = 2 / (r - l);
            result[1, 1] = 2 / (t - b);
            result[2, 1] = -2 / (f - n);
            result[2, 3] = -(r + l) / (r - l);
            result[3, 0] = -(t + b) / (t - b);
            result[3, 1] = -(f + n) / (f - n);
            return new Matrix(result);
        }
        /// <summary>
        /// Поворот за осью z
        /// </summary>
        /// <param name="angle">кут поворота в радианах</param>
        /// <returns>повертає матрицу за осью z</returns>
        public static Matrix RotateZ(double angle)
        {
            double[,] array = MakeIArray();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            array[0, 0] = cos;
            array[0, 1] = -sin;
            array[1, 0] = sin;
            array[1, 1] = cos;
            return new Matrix(array);
        }
        /// <summary>
        /// Поворот за осью y
        /// </summary>
        /// <param name="angle">кут поворота в радианах</param>
        /// <returns>повертає матрицу за осью у</returns>
        public static Matrix RotateY(double angle)
        {
            double[,] array = MakeIArray();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            array[0, 0] = cos;
            array[0, 2] = sin;
            array[2, 0] = -sin;
            array[2, 2] = cos;
            return new Matrix(array);
        }
        /// <summary>
        /// Поворот за осью х
        /// </summary>
        /// <param name="angle">кут поворота в радианах</param>
        /// <returns>повертає матрицу за осью х</returns>
        public static Matrix RotateX(double angle)
        {
            double[,] array = MakeIArray();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            array[1, 1] = cos;
            array[1, 2] = -sin;
            array[2, 1] = sin;
            array[2, 2] = cos;
            return new Matrix(array);
        }
        /// <summary>
        /// Матриця переміщення
        /// </summary>
        /// <param name="point">місце куди перемістити</param>
        /// <returns>Повертає матрицю Перміщення</returns>
        public static Matrix Move(Vector point)
        {
            double[,] array = MakeIArray();
            array[0, 3] = point.X;
            array[1, 3] = point.Y;
            array[2, 3] = point.Z;
            return new Matrix(array);
        }
    }
}