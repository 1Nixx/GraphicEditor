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
	internal class Ellipse : FigureBase, ISelectable
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

		public bool IsSelected(Point pointPos)
		{
			var XStart = GetXStartPos();
			var YStart = GetYStartPos();

			var signToWidth = MouseLeftUpPos.X >= XStart ? +1 : -1;
			var signToHeight = MouseLeftUpPos.Y >= YStart ? +1 : -1;

			int height = (int)Math.Abs(MouseLeftDownPos.Y - MouseLeftUpPos.Y);
			int width = (int)Math.Abs(MouseLeftDownPos.X - MouseLeftUpPos.X);

			var realPointPos = new Point(pointPos.X - Math.Min(XStart, XStart + signToWidth * width) - (double)width / 2, pointPos.Y - Math.Min(YStart, YStart + signToHeight * height) - (double)height / 2);

			var result = Math.Pow(realPointPos.X, 2) / Math.Pow((double)width / 2, 2) + Math.Pow(realPointPos.Y, 2) / Math.Pow((double)height / 2, 2) <= 1;
			return result;
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
