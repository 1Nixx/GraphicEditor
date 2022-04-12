using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Core
{
	internal interface IEditable
	{
		void ChangeColorFill(Color colorFill);
		void ChangeColorBorder(Color colorBorder);
		void ChangeBorderWidth(int newWidth);

		void ChangeCenterPos(Point newPos);
		void ChangeSizes(int width, int height);

	}
}
