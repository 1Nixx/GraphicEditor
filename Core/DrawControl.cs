using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

		private static Mapper _mapper { get; }

		static DrawControl()
		{
			var config = new MapperConfiguration(cfg =>
				cfg.CreateMap<FigureInfo, FigureBase>()
				.ConstructUsing((figInf, context) => tools[figInf.toolType])
				.ForMember(x => x.MouseLeftDownPos, s => s.MapFrom(x => x.leftTopPos))
				.ForMember(x => x.MouseLeftUpPos, s => s.MapFrom(x => x.rightBottomPos))
				.ForMember(x => x.colorBorder, s => s.MapFrom(fInf => new SolidColorBrush(fInf.colorBorder)))
				.ForMember(x => x.colorFill, s => s.MapFrom(fInf => new SolidColorBrush(fInf.colorFill)))
				.ForMember(x => x.borderWidth, s => s.MapFrom(x => x.borderWidth))
				.ForMember(x => x._isSelected, s => s.MapFrom(fInf => false))
			);

			_mapper = new Mapper(config);
		}

		private static List<FigureInfo> figureList = new List<FigureInfo>();

		public static void Draw(Canvas drawingField, FigureInfo figureInfo, bool isNew)
		{	
			var shapeCreator = _mapper.Map<FigureBase>(figureInfo);

			var figure = shapeCreator.BuildFigure();
			SetFigurePos(shapeCreator.MouseLeftDownPos, shapeCreator.MouseLeftUpPos, figure);

			if (!isNew && figureList.Count != 0)
				figureList.RemoveAt(figureList.Count - 1);

			figureList.Add(figureInfo);

			LoadShapesToCanvas(drawingField);
			//drawingField.Children.Add(figure);

		}

		private static FigureInfo _selectedFigure;

		public static bool SelectFigure(Canvas drawingField, Point pressPos)
		{
			for (int i = figureList.Count - 1; i >= 0; i--)
			{
				var shapeCreator = _mapper.Map<FigureBase>(figureList[i]);
				if (shapeCreator is ISelectable)
				{
					if ((shapeCreator as ISelectable).IsSelected(pressPos))
					{
						_selectedFigure = figureList[i];
						return true;
					}
				}
			}
			return false;
		}

		public static void RemoveSelection()
		{
			_selectedFigure = null;
		}

		#region Change selection figure

		public static void ChangeColorFill(Color newColor)
		{
			_selectedFigure?.ChangeColorFill(newColor);
		}

		public static void ChangeColorBorder(Color newColor)
		{
			_selectedFigure?.ChangeColorBorder(newColor);
		}

		public static void ChangeBorderWidth(int newWidth)
		{
			_selectedFigure?.ChangeBorderWidth(newWidth);
		}

		public static void ChangeFigurePos(Point downPos, Point upPos)
		{
			var vector = new Point { X = upPos.X - downPos.X, Y = upPos.Y - downPos.Y };
			_selectedFigure?.ChangeCenterPos(vector);
		}

		public static void ChandeSizes(int newWidth, int newHeight)
		{
			_selectedFigure?.ChangeSizes(newWidth, newHeight);
		}
		
		public static (int width, int height) GetFigureSizes()
		{
			return ((int)Math.Abs(_selectedFigure.leftTopPos.X - _selectedFigure.rightBottomPos.X), (int)Math.Abs(_selectedFigure.leftTopPos.Y - _selectedFigure.rightBottomPos.Y));
		}

		#endregion

		private static void SetFigurePos(Point leftTopPos, Point rightBottomPos, Shape figure)
		{
			if (figure is System.Windows.Shapes.Line)
				return;
			Canvas.SetLeft(figure, leftTopPos.X > rightBottomPos.X ? rightBottomPos.X : leftTopPos.X);
			Canvas.SetTop(figure, leftTopPos.Y > rightBottomPos.Y ? rightBottomPos.Y : leftTopPos.Y);
		}

		public static void SaveToFile(string fileName)
		{
			var figSerialize = JsonSerializer.Serialize<List<FigureInfo>>(figureList);

			using (var file = new StreamWriter(fileName))
			{
				file.Write(figSerialize);
			}
		}

		public static void RestoreFromFile(string fileName)
		{
			string resString;
			using (var file = new StreamReader(fileName))
			{
				resString = file.ReadToEnd();
			}
			figureList.Clear();
			figureList = JsonSerializer.Deserialize<List<FigureInfo>>(resString);
		}

		public static void LoadShapesToCanvas(Canvas drawingField)
		{
			drawingField.Children.Clear();
			foreach (var figure in figureList)
			{
				var shapeCreator = _mapper.Map<FigureBase>(figure);
				if (figure == _selectedFigure)
					(shapeCreator as ISelectable).SetSelection();
				
				var buildFigure = shapeCreator.BuildFigure();
				SetFigurePos(shapeCreator.MouseLeftDownPos, shapeCreator.MouseLeftUpPos, buildFigure);

				drawingField.Children.Add(buildFigure);
			}
		}
	
		public static void ClearPaintField(Canvas drawingField)
		{
			figureList.Clear();
			_selectedFigure = null;
			drawingField.Children.Clear();
		}
	}
}
