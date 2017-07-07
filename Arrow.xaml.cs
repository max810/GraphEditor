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
    /// Interaction logic for Arrow.xaml
    /// </summary>
    public partial class Arrow : UserControl, INotifyPropertyChanged
    {

        #region Dependency Properties

        public static readonly DependencyProperty StartProperty;

        public static readonly DependencyProperty EndProperty;

        public static readonly DependencyProperty ArrowHeadLengthProperty;

        public static readonly DependencyProperty ArrowHeadAngleProperty;

        public static readonly DependencyProperty ArrowHeadVisibilityProperty;

        #endregion

        #region Static Constructor

        static Arrow()
        {
            StartProperty = DependencyProperty.Register("Start", 
                typeof(Point), typeof(Arrow),
                new PropertyMetadata(default(Point), new PropertyChangedCallback(ArrowPropertyChangedCallback)));

            EndProperty = DependencyProperty.Register("End", 
                typeof(Point), typeof(Arrow),
                new PropertyMetadata(default(Point), new PropertyChangedCallback(ArrowPropertyChangedCallback)));

            ArrowHeadLengthProperty = DependencyProperty.Register("ArrowHeadLength", 
                typeof(double), typeof(Arrow),
                new PropertyMetadata(25.0d, new PropertyChangedCallback(ArrowPropertyChangedCallback)));

            ArrowHeadAngleProperty = DependencyProperty.Register("ArrowHeadAngle", 
                typeof(double), typeof(Arrow), 
                new PropertyMetadata(45.0d, new PropertyChangedCallback(ArrowPropertyChangedCallback)));

            ArrowHeadVisibilityProperty = DependencyProperty.Register("ArrowHeadVisibility",
                typeof(Visibility), typeof(Arrow),
                new PropertyMetadata(Visibility.Visible));
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of Arrow.  
        /// </summary>
        public Arrow()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the angle of the Arrow compared to normal Vector on X-axis (between this and (1,0) vector).
        /// </summary>
        private double LineAngle
        {
            get
            {
                return MathGeometry.AngleOf(Start, End);
            }
        }

        /// <summary>
        /// Gets or sets the start point of the arrow.
        /// </summary>
        public Point Start
        {
            get { return (Point)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        /// <summary>
        /// Gets or sets the end point of the arrow.
        /// </summary>
        public Point End
        {
            get { return (Point)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of arrow's head.
        /// </summary>
        public double ArrowHeadLength
        {
            get { return (double)GetValue(ArrowHeadLengthProperty); }
            set { SetValue(ArrowHeadLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the angle between the arrowhead's left and right segments (in degrees).
        /// </summary>
        public double ArrowHeadAngle
        {
            get { return (double)GetValue(ArrowHeadAngleProperty); }
            set { SetValue(ArrowHeadAngleProperty, value); }
        }

        /// <summary>
        /// Returns the ArrowHeadAngle in Radians
        /// </summary>
        private double ArrowHeadAngleRad
        {
            get { return ArrowHeadAngle * MathGeometry.DegreeToRadian; }
        }

        /// <summary>
        /// Gets or sets the visibility of the arrowhead
        /// </summary>
        public Visibility ArrowHeadVisibility
        {
            get { return (Visibility)GetValue(ArrowHeadVisibilityProperty); }
            set { SetValue(ArrowHeadVisibilityProperty, value); }
        }

        /// <summary>
        /// Returns the center of the arrow
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point
                {
                    X = (Start.X + End.X) / 2,
                    Y = (Start.Y + End.Y) / 2
                };
            }
        }

        /// <summary>
        /// Returns the central point of the arrowhead's foundation. 
        /// Perpendiculars to the arrow line, drawn through this point, 
        /// end up in ArrowHeadLeft-and-ArrowHeadRight points.
        /// </summary>
        public Point ArrowHeadBasePoint
        {
            get
            {
                return new Point
                {
                    X = End.X - ArrowHeadLength * Math.Cos(LineAngle),
                    Y = End.Y - ArrowHeadLength * Math.Sin(LineAngle)
                };
            }
        }

        /// <summary>
        /// If we assume that the segments of the arrowhead start in it's pointer, this would be
        /// the "end" point of the arrowhead's Right (look from "behind the arrow") segment.
        /// It is considered Right according to the screen coordinates system. 
        /// If drawn in usual Cartesian coordinates, this would have been Left point.
        /// </summary>
        public Point ArrowHeadRightPoint
        {
            get
            {
                return new Point
                {
                    X = ArrowHeadBasePoint.X - ArrowHeadLength * Math.Sin(LineAngle) * Math.Tan(ArrowHeadAngleRad / 2),
                    Y = ArrowHeadBasePoint.Y + ArrowHeadLength * Math.Cos(LineAngle) * Math.Tan(ArrowHeadAngleRad / 2)
                };
            }
        }

        /// <summary>
        /// If we assume that the segments of the arrowhead start in it's pointer, this would be
        /// the "end" point of the arrowhead's Left (look from "behind the arrow") segment.
        /// It is considered Left according to the screen coordinates system. 
        /// If drawn in usual Cartesian coordinates, this would have been Right point.
        /// </summary>
        public Point ArrowHeadLeftPoint
        {
            get
            {
                return new Point
                {
                    X = ArrowHeadBasePoint.X + ArrowHeadLength * Math.Sin(LineAngle) * Math.Tan(ArrowHeadAngleRad / 2),
                    Y = ArrowHeadBasePoint.Y - ArrowHeadLength * Math.Cos(LineAngle) * Math.Tan(ArrowHeadAngleRad / 2)
                };
            }
        }

        /// <summary>
        /// Returns a collection of three points, which form the arrowhead - "left", "top", "right".
        /// </summary>
        public PointCollection Points
        {
            get { return new PointCollection { ArrowHeadLeftPoint, End, ArrowHeadRightPoint }; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property value changes. (from INotifyPropertyChanged interface).
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Checks whether there are any subscribers to PropertyChanged event 
        /// and, in such a case, invokes the event.
        /// </summary>
        /// <param name="prop"> Optional parameter - represents the name of the property changed. </param>
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        /// <summary>
        /// Is called each time a DependencyProperty changes it's value.
        /// Automatically updates all the other properties, which are not DependencyProperties,
        /// thus bindings to them need to be updated manually.
        /// </summary>
        /// <param name="d"> DependencyObject which had it's property changed </param>
        /// <param name="e"> Args of the DependencyPropertyChanged event occured </param>
        private static void ArrowPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Arrow;
            control.OnPropertyChanged(e.Property.Name);
            control.OnPropertyChanged("Points");
            control.OnPropertyChanged("Center");
            control.OnPropertyChanged("ArrowHeadBasePoint");
            control.OnPropertyChanged("LineAngle");
            control.OnPropertyChanged("ArrowHeadRightPoint");
            control.OnPropertyChanged("ArrowHeadLeftPoint");
        }

        #endregion
    }
}
