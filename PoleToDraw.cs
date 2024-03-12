using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab2
{
    internal class PoleToDraw
    {
        //поки не використовується повнісю але це на майбутне кольори заповнення малюнку
        public readonly static char[] simbols = { ' ', '.', ':', '-', '*', '=', '$', '%', '#', '@' };

        //висота ширина
        int h, w;

        public Matrix cam;

        //поле у вигляді масив
        private char[,] _data;

        void PutPoint(int x, int y, char color)
        {
            if (x < 0 || x >= w || y < 0 || y >= h) return;
            _data[x, y] = color;
        }
        //конструктор
        public PoleToDraw(int w, int h)
        {
            _data = new char[w, h];
            this.h = h;
            this.w = w;
            cam = Matrix.OrtoganalMatrix(1.5, -1.5, 3, 0, 2, -2);
        }

        public void Draw(Poligon poligon)
        {
            for (int i = 0; i < poligon.Points.Count; i++)
            {
                DrawLine(new Ray(poligon[i], poligon[i + 1]), '@');
            }
            DrawLine(new Ray(poligon[0], poligon[poligon.Points.Count - 1]), '@');

        }

        public void Draw(Model model)
        {
            foreach (Edge e in model.edges)
            {
                DrawLine(e, e.color);
            }
        }

        void DrawLine(Ray ray, char color)
        {

            Vector v0, v1;
            v0 = cam * ray[0];
            v1 = cam * ray[1];

            DrawLine((int)v0.X + w / 2, (int)v0.Y / 4 + h / 2, (int)v1.X + w / 2, (int)v1.Y / 4 + h / 2, color);
        }

        public void DrawLine(int x0, int y0, int x1, int y1, char color)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x1 >= x0 ? 1 : -1;
            int sy = y1 >= y0 ? 1 : -1;

            if (dy <= dx)
            {
                int d = (dy << 1) - dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                PutPoint(x0, y0, color);
                for (int x = x0 + sx, y = y0, i = 1; i <= dx; i++, x += sx)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPoint(x, y, color);
                }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;
                PutPoint(x0, y0, color);
                for (int y = y0 + sy, x = x0, i = 1; i <= dy; i++, y += sy)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPoint(x, y, color);
                }
            }
        }

        // вивести в консоль масив
        public string Export()
        {
            string s = "";
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (_data[x, y] != 0)
                    {
                        s += _data[x, y];
                    }
                    else
                    {
                        s += simbols[0];
                    }
                }
                s += "\n";
            }
            return s;
        }

        //очистити масив
        public void Clear()
        {
            _data = new char[w, h];
        }
    }
}
