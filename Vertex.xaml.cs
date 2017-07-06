using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GraphEditor
{
    /// <summary>
    /// Interaction logic for Vertex.xaml
    /// </summary>
    public partial class Vertex : PositionableControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty TextProperty;
           
        public static readonly DependencyProperty FillProperty;

        public static readonly DependencyProperty RadiusProperty;

        #endregion

        #region Static Constructor

        static Vertex()
        {
            TextProperty = DependencyProperty.Register("Text", 
                typeof(string), typeof(Vertex), 
                new PropertyMetadata(""));

            FillProperty =
            DependencyProperty.Register("Fill", 
                typeof(SolidColorBrush), typeof(Vertex), 
                new PropertyMetadata(Brushes.Red));

            RadiusProperty =
            DependencyProperty.Register("Radius", 
                typeof(double), typeof(Vertex), 
                new PropertyMetadata(double.NaN));
        }
        
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of Vertex class (represents a vertex of a graph). 
        /// By default this would have Radius of double.NaN and color of Red with no text on it.
        /// This class, as an acnestor of PositionableControl, implements INotifyPropertyChanged.
        /// </summary>
        public Vertex()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Radius of the vertex element.
        /// Also notifies about the Diameter property changed each time it has it's value updated.
        /// </summary>
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set
            {
                SetValue(RadiusProperty, value);
                OnPropertyChanged("Diameter");
            }
        }

        /// <summary>
        /// Returns the Radius doubled. This is a get-only property.
        /// It is automatically updated each time the Radius property is changed.
        /// </summary>
        public double Diameter
        {
            get { return Radius * 2; }
        }

        /// <summary>
        /// Gets or sets the text on the Vertex.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the vertex
        /// </summary>
        public SolidColorBrush Fill
        {
            get { return (SolidColorBrush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        #endregion
    }
}