using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfApp
{
    public static class Coordinate
    {
        /// <summary>
        /// Очередь фигур
        /// </summary>
        private static Queue<MyFigure> figures = new();


        /// <summary>
        /// Добавить в очередь первый элемент
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="widht">Размер ребра основания фигуры</param>
        /// <returns></returns>
        public static void AddToQueue(double x, double y, double widht)
        {
            figures.Enqueue(new MyFigure(x, y, widht, 0));
        }
        /// <summary>
        /// Очистка очереди
        /// </summary>
        public static void Clear()
        {
            figures.Clear();
        }
        /// <summary>
        /// Возвращает фигуру координаты
        /// </summary>
        public static PointCollection Get()
        {
            if (figures.Count == 0)
                return null;
            MyFigure f = figures.Dequeue();
            return GetPoints(f);

        }
        /// <summary>
        /// Рассчёт координат для фигуры
        /// </summary>
        /// <param name="f">Фигура</param>
        /// <returns>Координаты вершин</returns>
        private static PointCollection GetPoints(MyFigure f)
        {
            Point point = new(f.X, f.Y);
            PointCollection position = new()
            {
                PointRotate(f.Alfa, new Point(f.X, f.Y - f.Width), point),
                PointRotate(f.Alfa, new Point(f.X + f.Width / 2, f.Y - 1.5 * f.Width), point),
                PointRotate(f.Alfa, new Point(f.X + f.Width, f.Y - f.Width), point),
                PointRotate(f.Alfa, new Point(f.X + f.Width, f.Y), point),
                PointRotate(f.Alfa, new Point(f.X, f.Y), point),
                PointRotate(f.Alfa, new Point(f.X, f.Y - f.Width), point),
                PointRotate(f.Alfa, new Point(f.X + f.Width, f.Y - f.Width), point)
            };
            double width = f.Width / Math.Sqrt(2);
            figures.Enqueue(new MyFigure(position[0].X, position[0].Y, width, f.Alfa - 45));
            figures.Enqueue(new MyFigure(position[1].X, position[1].Y, width, f.Alfa + 45));
            position.Freeze();
            return position;
        }
        /// <summary>
        /// Поворот точки относительно другой точки
        /// </summary>
        /// <param name="a">Угол поворота</param>
        /// <param name="p">какие координаты поворачиваем</param>
        /// <param name="p0">относительно каких</param>
        /// <returns></returns>
        private static Point PointRotate(double a, Point p, Point p0)
        {
            double alfa = a * Math.PI / 180;
            double x = p.X - p0.X;
            double y = p.Y - p0.Y;
            double X = x * Math.Cos(alfa) - y * Math.Sin(alfa) + p0.X;
            double Y = x * Math.Sin(alfa) + y * Math.Cos(alfa) + p0.Y;
            return new Point(X, Y);
        }
    }
}
