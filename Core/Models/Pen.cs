using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Pen : FigureBase
	{
		public override Shape BuildFigure()
		{
			var shape = new System.Windows.Shapes.Ellipse();
			shape.Height = shape.Width = borderWidth.GetValueOrDefault(5);

			if (!(colorFill is null))
				shape.Fill = colorFill;
			else
				shape.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));

			return shape;
		}
	}
}
