using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Line : FigureBase
	{
		public override Shape BuildFigure()
		{
			var shape = new System.Windows.Shapes.Line();
			_shape = shape;

			shape.X1 = MouseLeftDownPos.X;
			shape.Y1 = MouseLeftDownPos.Y;
			shape.X2 = MouseLeftUpPos.X;
			shape.Y2 = MouseLeftUpPos.Y;

			ConfigureColors();
			return shape;
		}
	}
}
