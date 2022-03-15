using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Core
{
	public static class DrawControl
	{
		private static Dictionary<Tool, FigureBase> tools = new Dictionary<Tool, FigureBase>()
		{
			{Tool.Rectangle, new Models.Rectangle() },
			{Tool.Line, new Models.Line() },
			{Tool.Ellipse, new Models.Ellipse() },
			{Tool.Pen, new Models.Pen() },
			{Tool.Square, new Models.Square() },
			{Tool.Circle, new Models.Circle() }
		};

		public static void Draw(Canvas drawingField, Point leftTopPos, Point rightBottomPos, Tool toolType, FigureInfo figureInfo)
		{
			var shapeCreator = tools[toolType];

			shapeCreator.MouseLeftDownPos = leftTopPos;
			shapeCreator.MouseLeftUpPos = rightBottomPos;

			if (!(figureInfo is null))
			{
				shapeCreator.colorFill = figureInfo.colorFill;
				shapeCreator.colorBorder = figureInfo.colorBorder;
				shapeCreator.borderWidth = figureInfo.borderWidth;
			}

			var figure = shapeCreator.BuildFigure();
			SetFigurePos(leftTopPos, rightBottomPos, figure);

			drawingField.Children.Add(figure);
		}

		private static void SetFigurePos(Point leftTopPos, Point rightBottomPos, Shape figure)
		{
			if (figure is System.Windows.Shapes.Line)
				return;
			Canvas.SetLeft(figure, leftTopPos.X > rightBottomPos.X ? rightBottomPos.X : leftTopPos.X);
			Canvas.SetTop(figure, leftTopPos.Y > rightBottomPos.Y ? rightBottomPos.Y : leftTopPos.Y);
		}
	}
}
