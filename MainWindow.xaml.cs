using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GraphEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            base.OnMouseDown(e);
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Vertex a = new Vertex
            {
                Fill = Brushes.Blue,
                Radius = 20,
                Text = "A",
                LeftOffset = 50,
                TopOffset = 100
            };
            Canvas.SetLeft(a, 250);
            Vertex b = new Vertex
            {
                Fill = Brushes.Red,
                Radius = 20,
                Text = "B",
                LeftOffset = 200,
                TopOffset = 250
            };

            Vertex c = new Vertex
            {
                Fill = Brushes.PaleVioletRed,
                Radius = 20,
                Text = "C",
                LeftOffset = 275,
                TopOffset = 300
            };

            Vertex d = new Vertex
            {
                Fill = Brushes.Pink,
                Radius = 20,
                Text = "D",
                LeftOffset = 220,
                TopOffset = 300
            };

            Vertex e = new Vertex
            {
                Fill = Brushes.Gray,
                Radius = 20,
                Text = "E",
                LeftOffset = 235, 
                TopOffset = 120
            };


            Edge x = new Edge
            {
                StartVertex = a,
                EndVertex = b,
                IsWeighted = true,
                IsDirected = true
            };


            var col = new ObservableCollection<UIElement>
            {
                x, a, b, c, d, e
            };
            Vertices.ItemsSource = col;

            RNGButton.Click += (s, ev) =>
            {
                Random rng = new Random();
                int i1 = rng.Next(1, col.Count);
                int i2 = rng.Next(1, col.Count);
                if (i1 == i2)
                {
                    i1 = i1 + 1 == col.Count ? 1 : i1 + 1;
                }
                x.StartVertex = col[i1] as Vertex;
                x.EndVertex = col[i2] as Vertex;
            };
        }
    } 
}
