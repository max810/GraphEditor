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
    public class PositionableControl : UserControl, INotifyPropertyChanged
    {
        #region Dependency Properties

        public static readonly DependencyProperty LeftOffsetProperty;

        public static readonly DependencyProperty TopOffsetProperty;

        #endregion //Dependency Properties

        #region Static Constructor

        static PositionableControl()
        {
            LeftOffsetProperty =
            DependencyProperty.Register("LeftOffset",
                typeof(double), typeof(PositionableControl),
                new PropertyMetadata(double.NaN, new PropertyChangedCallback(OffsetPropertyChangedCallback)));

            TopOffsetProperty =
            DependencyProperty.Register("TopOffset",
                typeof(double), typeof(PositionableControl),
                new PropertyMetadata(double.NaN, new PropertyChangedCallback(OffsetPropertyChangedCallback)));
        }

        #endregion //Static Constructor

        #region Constructor 
        /// <summary>
        /// A base constructor for all inheritors.
        /// </summary>
        public PositionableControl()
        {

        }

        #endregion //Constructor

        #region Properties
        /// <summary>
        /// Gets or sets the LeftOFfset property which represents the distance
        /// from the left border of a grouping element this element is located in (usually Canvas).
        /// </summary>
        public double LeftOffset
        {
            get { return (double)GetValue(LeftOffsetProperty); }
            set { SetValue(LeftOffsetProperty, value); }
        }

        /// <summary>
        /// Gets or sets the TopOffset property which represents the distance
        /// from the upper border of a grouping element this element is located in (usually Canvas).
        /// </summary>
        public double TopOffset
        {
            get { return (double)GetValue(TopOffsetProperty); }
            set { SetValue(TopOffsetProperty, value); }
        }

        /// <summary>
        /// Returns the Center point of the figure, based on offsets and Width/Height properties.
        /// This is a get-only property, it is calculated and it's updates are notified automatically.
        /// </summary>
        public Point CenterPoint
        {
            get
            {
                double x = LeftOffset + Width / 2;
                double y = TopOffset + Height / 2;
                return new Point(x, y);
            }
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
        /// Is called each time when a DependencyProperty is changed.
        /// Notifies about the property change.
        /// </summary>
        /// <param name="d"> DependencyObject which had it's property changed </param>
        /// <param name="e"> Args of the DependencyPropertyChanged event occured </param>
        private static void OffsetPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PositionableControl;
            control.OnPropertyChanged(e.Property.Name);
            control.OnPropertyChanged("CenterPoint");
        }

        /// <summary>
        /// Checks whether there are any subscribers to PropertyChanged event 
        /// and, in such a case, invokes the event.
        /// </summary>
        /// <param name="prop"> Optional parameter - represents the name of the property changed. </param>
        protected void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion //Methods
    }
}
