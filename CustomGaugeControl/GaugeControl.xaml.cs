using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomGaugeControl
{
    /// <summary>
    /// Interaction logic for GaugeControl.xaml
    /// </summary>
    public partial class GaugeControl : UserControl , INotifyPropertyChanged
    {
        public static readonly DependencyProperty MinProperty;
        public static readonly DependencyProperty MaxProperty;
        public static readonly DependencyProperty AngleProperty;
        public static readonly DependencyProperty ValueProperty;
        static GaugeControl()
        {
            MaxProperty = DependencyProperty.Register(
            "Min", typeof(double), typeof(GaugeControl), new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                    FrameworkPropertyMetadataOptions.AffectsRender));
                    
            MaxProperty = DependencyProperty.Register(
            "Max", typeof(double), typeof(GaugeControl), new FrameworkPropertyMetadata(
                    100.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                    FrameworkPropertyMetadataOptions.AffectsRender));
            AngleProperty = DependencyProperty.Register(
            "Angle", typeof(double), typeof(GaugeControl), new FrameworkPropertyMetadata(
                    33.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                    FrameworkPropertyMetadataOptions.AffectsRender));
            ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(GaugeControl), new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                    FrameworkPropertyMetadataOptions.AffectsRender));
        }
    
        public double Min
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }
        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }
        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);
                Angle = (Value >= Max)
                    ? 326.0
                    : 293.0 * (Value / Max) + 33.0;
            }
        }


        public GaugeControl()
        {
            InitializeComponent();
            this.DataContext = this;
            DrawMetricScale();
        }
        private void DrawMetricScale()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Black);
            double
                minangle = 260.0,
                _angle = minangle,
                maxAngle = 550.0;
            int nLine = 0;
            while (_angle < maxAngle)
            {
                double longline = nLine++ % 5 == 0 ? 1 : 0;
                RotateTransform trace = new RotateTransform(_angle, 27, 27);
                Line line = new Line()
                {
                    X1 = 11,
                    Y1 = 11,
                    X2 = 12 + longline,
                    Y2 = 12 + longline,
                    StrokeThickness = 0.25
                };
                line.Stroke = solidColorBrush;
                line.RenderTransform = trace;
                grid.Children.Add(line);
                _angle += 3.85;
            }
        }


        #region ObservableObject
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion ObservableObject
    }
}
