using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphEditor
{
    /// <summary>
    /// Represents an oriented linesegment in 2D coordinates
    /// </summary>
    public struct Segment
    {
        /// <summary>
        /// Sets the location of the start of Segment
        /// </summary>
        public Point From { get; set; }
        /// <summary>
        /// Sets the location of the end of Segment
        /// </summary>
        public Point To { get; set; }
        /// <summary>
        /// Creates a new GraphEditor.Segment structure that contains two points - start and end.
        /// </summary>
        /// <param name="from">The start point of the segment</param>
        /// <param name="to">The end point of the segment</param>
        public Segment(Point from, Point to)
        {
            From = from;
            To = to;
        }
    }
}
