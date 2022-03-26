

using Hangfire.Annotations;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp
{
    public class Command : INotifyPropertyChanged
    {
        #region Variables
        /// <summary>
        /// Коллекция элементов
        /// </summary>
        public ObservableCollection<Polyline> CollectionFigure { get; } = new();
        private double myWidth = 600;
        /// <summary>
        /// Ширина холста
        /// </summary>
        public double MyWidth { get => myWidth; set => SetProperty(ref myWidth, value); }

        private double myHeight = 500;
        /// <summary>
        /// Высота холста
        /// </summary>
        public double MyHeight { get => myHeight; set => SetProperty(ref myHeight, value); }
        /// <summary>
        /// Размер основания фигуры
        /// </summary>
        public double Size { get; set; } = 100;

        /// <summary>
        /// Можно ли рисовать, если да true
        /// </summary>
        public bool CanDraw { get; set; }
        #endregion

        #region Constructor
        public Command()
        {
            DrawDelegateCommand = new DelegateCommand(DrawTreeAsync);
            StopDelegateCommand = new DelegateCommand(Stop);
            ClearDelegateCommand = new DelegateCommand(Clear);
        }
        #endregion
        int parent = 0, count_child = 2;
        #region Methods
        /// <summary>
        /// Распределение времени прорисовки фигур
        /// </summary>
        /// <param name="size">Размер основания фигуры</param>
        /// <param name="height">Высота холста</param>
        /// <param name="width">ширина холств</param>
        private void Draw(double size, double height, double width)
        {
            if (CollectionFigure.Count == 0)
                Coordinate.AddToQueue((width - size) / 2, height - 10, size);

            while (CanDraw)
            {
                if (++parent == count_child)
                {
                    count_child *= 2;
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(100);
                }
                DrawObject();
            }
        }
        /// <summary>
        /// Добавление фигур на холст
        /// </summary>
        void DrawObject()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (CanDraw)
                {
                    Polyline p = new()
                    {
                        Points = Coordinate.Get(),
                        Stroke = Brushes.White,
                        StrokeThickness = 2
                    };
                    CollectionFigure.Add(p);
                    return;
                }
                parent--;
            });
        }
        #endregion

        #region Delegate Commands
        public DelegateCommand DrawDelegateCommand { get; set; }
        public DelegateCommand StopDelegateCommand { get; set; }
        public DelegateCommand ClearDelegateCommand { get; set; }
        #endregion

        #region Delegate Command Functions
        /// <summary>
        /// Рисование на холсте
        /// </summary>
        private async void DrawTreeAsync()
        {
            CanDraw = true;
            await Task.Run(() => Draw(Size, MyHeight, MyWidth));
        }
        /// <summary>
        /// Очистка холста. Остановка рисования.
        /// </summary>
        private void Clear()
        {
            CanDraw = false;
            Coordinate.Clear();
            CollectionFigure.Clear();
        }
        /// <summary>
        /// Пауза для рисования
        /// </summary>
        private void Stop()
        {
            CanDraw = false;
        }
        #endregion

        #region Property Changed Logic
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
        #endregion
    }
}

