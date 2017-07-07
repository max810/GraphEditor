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

namespace GraphEditor
{
    /// <summary>
    /// Interaction logic for Edge.xaml
    /// </summary>
    public partial class Edge : UserControl, INotifyPropertyChanged, IGraphElement
    {
        #region Data

        private Vertex startVertex;

        private Vertex endVertex;

        #endregion //Data

        #region Dependency Properties

        public static readonly DependencyProperty IsDirectedProperty;

        public static readonly DependencyProperty IsWeightedProperty;

        public static readonly DependencyProperty WeightBoxHeightProperty;

        public static readonly DependencyProperty WeightBoxWidthProperty;

        #endregion //Dependency Properties

        #region Static Constructor

        static Edge()
        {
            IsDirectedProperty =
                DependencyProperty.Register("IsDirected",
                typeof(bool), typeof(Edge),
                new PropertyMetadata(false));

            IsWeightedProperty =
                DependencyProperty.Register("IsWeighted",
                typeof(bool), typeof(Edge),
                new PropertyMetadata(false));

            WeightBoxHeightProperty =
            DependencyProperty.Register("WeightBoxHeight", 
                typeof(double), typeof(Edge),
                new PropertyMetadata(20.0d));

            WeightBoxWidthProperty =
            DependencyProperty.Register("WeightBoxWidth", 
                typeof(double), typeof(Edge), 
                new PropertyMetadata(40.0d));
        }

        #endregion //Static Constructor

        #region Constructor

        /// <summary>
        /// Initializes a new instance of Edge.
        /// </summary>
        public Edge()
        {
            InitializeComponent();
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Gets or sets whether the current Edge is directed or undirected.
        /// </summary>
        public bool IsDirected
        {
            get { return (bool)GetValue(IsDirectedProperty); }
            set
            {
                SetValue(IsDirectedProperty, value);
                OnPropertyChanged("DirectionVisibility");
            }
        }

        /// <summary>
        /// Gets or sets whether the current Edge is weighted or unweighted.
        /// </summary>
        public bool IsWeighted
        {
            get { return (bool)GetValue(IsWeightedProperty); }
            set
            {
                SetValue(IsWeightedProperty, value);
                OnPropertyChanged("WeightVisibility");
            }
        }

        /// <summary>
        /// Gets or sets the width of the box with text.
        /// </summary>
        public double WeightBoxWidth
        {
            get { return (double)GetValue(WeightBoxWidthProperty); }
            set
            {
                SetValue(WeightBoxWidthProperty, value);
                OnPropertyChanged("TextTopLeftCorner");
            }
        }

        /// <summary>
        /// Gets or sets the height of the box with text.
        /// </summary>
        public double WeightBoxHeight
        {
            get { return (double)GetValue(WeightBoxHeightProperty); }
            set
            {
                SetValue(WeightBoxHeightProperty, value);
                OnPropertyChanged("TextTopLeftCorner");
            }
        }

        /// <summary>
        /// Gets or sets the Vertex, from which the edge goes to the EndVertex.  
        /// </summary>
        public Vertex StartVertex
        {
            get { return startVertex; }
            set
            {
                if (value != null)
                {
                    value.PropertyChanged -= VertexCoordsChangedEventHandler;
                    value.PropertyChanged += VertexCoordsChangedEventHandler;
                }
                startVertex = value;
                VertexCoordsChangedEventHandler(value, new PropertyChangedEventArgs("StartVertex"));
            }
        }

        /// <summary>
        /// Gets or sets the Vertex, that this Edge connects the StartVertex to.
        /// </summary>
        public Vertex EndVertex
        {
            get { return endVertex; }
            set
            {
                if (value != null)
                {
                    value.PropertyChanged -= VertexCoordsChangedEventHandler;
                    value.PropertyChanged += VertexCoordsChangedEventHandler;
                }
                endVertex = value;
                VertexCoordsChangedEventHandler(value, new PropertyChangedEventArgs("EndVertex"));
            }
        }

        /// <summary>
        /// Returns a Segment of pair of points, in which the line, connecting two vertices, touches them.
        /// </summary>
        public Segment LinePoints
        {
            get
            {
                if (StartVertex == null || EndVertex == null)
                    return new Segment();
                double x1 = StartVertex.CenterPoint.X;
                double y1 = StartVertex.CenterPoint.Y;
                double x2 = EndVertex.CenterPoint.X;
                double y2 = EndVertex.CenterPoint.Y;
                double R1 = StartVertex.Radius;
                double R2 = StartVertex.Radius;
                double angle = MathGeometry.AngleOf(StartVertex.CenterPoint, EndVertex.CenterPoint);
                return new Segment
                {
                    From = new Point(x1 + R1 * Math.Cos(angle), y1 + R1 * Math.Sin(angle)),
                    To = new Point(x2 - R2 * Math.Cos(angle), y2 - R2 * Math.Sin(angle))
                };
            }
        }

        /// <summary>
        /// Returns the top-left corner point, which the coordinates of text box are bound to.
        /// </summary>
        public Thickness TextTopLeftCorner
        {
            get { return new Thickness(arrow.Center.X - WeightBoxWidth / 2, arrow.Center.Y - WeightBoxHeight / 2, 0, 0); }
        }

        public Visibility DirectionVisibility
        {
            get { return IsDirected ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility WeightVisibility
        {
            get { return IsWeighted ? Visibility.Visible : Visibility.Collapsed; }
        }

        #endregion //Properties 

        #region Events

        /// <summary>
        /// Occurs when a property value changes. (from INotifyPropertyChanged interface).
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion //Events

        #region Methods

        /// <summary>
        /// Checks whether there are any subscribers to PropertyChanged event 
        /// and, in such a case, invokes the event.
        /// </summary>
        /// <param name="prop"> Optional parameter - represents the name of the property changed. </param>
        protected void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Notifies all of the subscribers of the "LinePoints" property of this class 
        /// each time the Start-or-End-Vertex's property is changed.
        /// Because the "LinePoints" property isn't a DependencyProperty, that's why it's subscribers must be
        /// notified manually each time the vertex changes it's coordinates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VertexCoordsChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                OnPropertyChanged(e.PropertyName);
                OnPropertyChanged("LinePoints");
                OnPropertyChanged("TextTopLeftCorner");
            }
        }

        #endregion //Methods
    }
}
