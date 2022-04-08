using Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			for (Tool toolType = Tool.Pen; toolType <= Tool.Circle; toolType++)
				cbTool.Items.Add(toolType.ToString());

			cbTool.SelectedIndex = 0;
		}

		private void cnvPaint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_isLeftDown = true;
			_figureInfo.leftTopPos = Mouse.GetPosition(cnvPaint);
		}

		private void cnvPaint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (_isLeftDown)
			{
				_figureInfo.rightBottomPos = Mouse.GetPosition(cnvPaint);
				DrawFigure();
				_isLeftDown = false;
			}
		}

		private void cbTool_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_figureInfo.toolType = GetToolByIndex(cbTool.SelectedIndex);
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			DrawControl.ClearPaintField(cnvPaint);
		}

		private void cnvPaint_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_isLeftDown)
				return;

			if (_figureInfo.toolType == Tool.Pen)
			{
				_figureInfo.rightBottomPos = _figureInfo.leftTopPos = Mouse.GetPosition(cnvPaint);
				DrawFigure();
			}
				
		}

		private bool _isLeftDown = false;
		private FigureInfo _figureInfo = new FigureInfo();

		private void DrawFigure()
		{
			DrawControl.Draw(cnvPaint, _figureInfo.Clone() as FigureInfo);
		}
		private Tool GetToolByIndex(int index)
		{
			for (Tool toolType = Tool.Pen; toolType <= Tool.Circle; toolType++)
			{
				if ((int)toolType == index)
				{
					return toolType;
				}
			}
			return Tool.None;
		}

		private void slWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (!(lbSize2 is null))
				lbSize2.Content = ((int)e.NewValue).ToString() + "px";
			_figureInfo.borderWidth = (int)e.NewValue;
		}

		private void clrFill_ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			_figureInfo.colorFill =  clrFill.Color;
		}

		private void clrBorder_ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			_figureInfo.colorBorder = clrBorder.Color;
		}

		private void btnSaveField_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog openFileDialog = new SaveFileDialog();
			if (openFileDialog.ShowDialog() == true)
				DrawControl.SaveToFile(openFileDialog.FileName);
		}

		private void btnOpenField_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog saveFileDialog = new OpenFileDialog();
			if ((saveFileDialog.ShowDialog()) == true)
			{
				DrawControl.RestoreFromFile(saveFileDialog.FileName);
				DrawControl.LoadShapesToCanvas(cnvPaint);
			}
		}
	}
}
