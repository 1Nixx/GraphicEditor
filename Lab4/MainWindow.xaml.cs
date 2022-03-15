using Core;
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

			for(Tool toolType = Tool.Pen; toolType <= Tool.Circle; toolType++)
				cbTool.Items.Add(toolType.ToString());

			cbTool.SelectedIndex = 0;
		}

		private void cnvPaint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_isLeftDown = true;
			_leftTopPos = Mouse.GetPosition(cnvPaint);
		}

		private void cnvPaint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (_isLeftDown)
			{
				_rightBottomPos = Mouse.GetPosition(cnvPaint);
				DrawFigure();
				_isLeftDown = false;
			}
		}

		private void cbTool_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_selectedTool = GetToolByIndex(cbTool.SelectedIndex);
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			cnvPaint.Children.Clear();
		}

		private void cnvPaint_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_isLeftDown)
				return;

			if (_selectedTool == Tool.Pen)
			{
				_rightBottomPos = _leftTopPos = Mouse.GetPosition(cnvPaint);
				DrawFigure();
			}
				
		}

		private Tool _selectedTool;
		private Point _leftTopPos;
		private Point _rightBottomPos;
		private bool _isLeftDown = false;
		private FigureInfo _figureInfo = new FigureInfo();

		private void DrawFigure()
		{
			DrawControl.Draw(cnvPaint, _leftTopPos, _rightBottomPos, _selectedTool, _figureInfo);
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
			_figureInfo.colorFill =  new SolidColorBrush(clrFill.Color);
		}

		private void clrBorder_ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			_figureInfo.colorBorder = new SolidColorBrush(clrBorder.Color);
		}
	}
}
