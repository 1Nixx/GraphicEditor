using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Ellipse : FigureBase
	{
		public override Shape BuildFigure()
		{
			Shape shape = new System.Windows.Shapes.Ellipse();
			_shape = shape;

			shape.Width = Math.Abs(MouseLeftDownPos.X - MouseLeftUpPos.X);
			shape.Height = Math.Abs(MouseLeftDownPos.Y - MouseLeftUpPos.Y);

			ConfigureColors();

			return shape;
		}
	}
}
