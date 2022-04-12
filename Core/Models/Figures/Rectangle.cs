using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Core.Models
{
	internal class Rectangle : FigureBase, ISelectable
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

		public bool IsSelected(Point pointPos)
		{
			var XStart = GetXStartPos();
			var YStart = GetYStartPos();

			var signToWidth = MouseLeftUpPos.X >= XStart ? +1 : -1;
			var signToHeight = MouseLeftUpPos.Y >= YStart ? +1 : -1;

			
			int height = (int)Math.Abs(MouseLeftDownPos.Y - MouseLeftUpPos.Y);
			int width = (int)Math.Abs(MouseLeftDownPos.X - MouseLeftUpPos.X);

			if (pointPos.X >= Math.Min(XStart, XStart + signToWidth * width) && pointPos.X <= Math.Max(XStart, XStart + signToWidth * width) &&
				pointPos.Y >= Math.Min(YStart, YStart + signToHeight * height) && pointPos.Y <= Math.Max(YStart, YStart + signToHeight * height))
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
