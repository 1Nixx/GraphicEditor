using AutoMapper;
using Core.Models;

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
			);

			_mapper = new Mapper(config);
		}

		private static List<FigureInfo> figureList = new List<FigureInfo>();

		public static void Draw(Canvas drawingField, FigureInfo figureInfo)
		{	
			var shapeCreator = _mapper.Map<FigureBase>(figureInfo);

			var figure = shapeCreator.BuildFigure();
			SetFigurePos(shapeCreator.MouseLeftDownPos, shapeCreator.MouseLeftUpPos, figure);

			figureList.Add(figureInfo);

			drawingField.Children.Add(figure);
		}

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
				var buildFigure = shapeCreator.BuildFigure();
				SetFigurePos(shapeCreator.MouseLeftDownPos, shapeCreator.MouseLeftUpPos, buildFigure);

				drawingField.Children.Add(buildFigure);
			}
		}
	
		public static void ClearPaintField(Canvas drawingField)
		{
			figureList.Clear();
			drawingField.Children.Clear();
		}
	}
}
