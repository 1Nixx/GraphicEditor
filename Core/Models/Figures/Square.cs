using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Square : Rectangle
	{
		public override Shape BuildFigure()
		{
			var shape = base.BuildFigure();

			shape.Width = shape.Height = Math.Min(shape.Width, shape.Height);

			return shape;
		}
	}
}
