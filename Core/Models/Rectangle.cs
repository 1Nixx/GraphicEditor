using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Rectangle : FigureBase
	{
		public override Shape BuildFigure()
		{
			var shape = new System.Windows.Shapes.Rectangle();
			_shape = shape;

			shape.Width = Math.Abs(MouseLeftDownPos.X - MouseLeftUpPos.X);
			shape.Height = Math.Abs(MouseLeftDownPos.Y - MouseLeftUpPos.Y);

			ConfigureColors();
			return shape;
		}
	}
}
