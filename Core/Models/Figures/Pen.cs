using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Pen : FigureBase, ISelectable
	{
		public override Shape BuildFigure()
		{
			var shape = new System.Windows.Shapes.Ellipse();
			shape.Height = shape.Width = borderWidth.GetValueOrDefault(5);

			if (!(colorFill is null))
				shape.Fill = colorFill;
			else
				shape.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));

			if (_isSelected)
				shape.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));


			return shape;
		}

		public bool IsSelected(Point pointPos)
		{
			if (pointPos.X >= MouseLeftDownPos.X && pointPos.Y >= MouseLeftDownPos.Y && pointPos.X <= MouseLeftDownPos.X + borderWidth && pointPos.Y <= MouseLeftDownPos.Y + borderWidth)
				return true;
			return false;
		}

		public void RemoveSelection()
		{
			throw new NotImplementedException();
		}

		public void SetSelection()
		{
			_isSelected = true;
		}
	}
}
