using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphEditor
{
    public static class MathGeometry
    {
        public const double RadianToDegree = 57.295779513;
        public const double DegreeToRadian = 1 / RadianToDegree;
        /// <summary>
        /// Calculates the angle in Radians between the X-axis and the given vector 
        /// </summary>
        /// <param name="v">The given vector parameter</param>
        /// <returns>The angle of type double between the X-axis and the vector</returns>
        public static double AngleOf(Vector v)
        {
            return Vector.AngleBetween(new Vector(1, 0), v) * DegreeToRadian;
        }

        /// <summary>
        /// Calculates the angle in Radians between the the X-axis and the given vector (formed of two points)
        /// </summary>
        /// <param name="from">The start point of the vector</param>
        /// <param name="to">The end point of the vector</param>
        /// <returns>The angle of type double between the X-axis and the vector</returns>
        public static double AngleOf(Point from, Point to)
        {
            return Vector.AngleBetween(new Vector(1, 0), new Vector(to.X - from.X, to.Y - from.Y))
                    * DegreeToRadian;
        }

        /// <summary>
        /// Calculates the angle in Radians between the X-axis and the given vector (presented as a segment) 
        /// </summary>
        /// <param name="s">Segment object representing the vector</param>
        /// <returns>The angle of type double between the X-axis and the vector</returns>
        public static double AngleOf(Segment s)
        {
            return AngleOf(s.From, s.To);
        }
    }
}
