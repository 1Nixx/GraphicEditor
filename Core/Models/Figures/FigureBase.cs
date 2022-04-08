using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Core.Models
{
	internal abstract class FigureBase
	{
		public Point MouseLeftDownPos { get; set; }

		public Point MouseLeftUpPos { get; set; }

		public Brush colorBorder { get; set; }
		public Brush colorFill { get; set; }
		public int? borderWidth { get; set; }

		protected Shape _shape;

		public abstract Shape BuildFigure();

		protected virtual void ConfigureColors()
		{
			if (_shape is null)
				throw new ArgumentException();

			if (!(colorFill is null))
				_shape.Fill = colorFill;
			else
				_shape.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));

			if (!(colorBorder is null))
				_shape.Stroke = colorBorder;
			else
				_shape.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));

			_shape.StrokeThickness = borderWidth.GetValueOrDefault(2);
		}
	}
}
