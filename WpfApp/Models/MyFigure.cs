namespace WpfApp
{
    /// <summary>
    /// Фигура
    /// </summary>
    public class MyFigure
    {
        /// <summary>
        /// Начальная координата по X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Начальная координата по Y
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Длина ребра
        /// </summary>
        public double Width { get; set; }
        
        /// <summary>
        /// Угол поворота
        /// </summary>
        public double Alfa { get; set; }
        
        /// <summary>
        /// Конструктор создания новой фигуры
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Начальный размер</param>
        /// <param name="alfa">Угол наклона</param>
        public MyFigure(double x, double y, double width, double alfa)
        {
            X = x;
            Y = y;
            Width = width;
            Alfa = alfa;
        }
    }
}
