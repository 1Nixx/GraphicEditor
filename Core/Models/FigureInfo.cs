using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Core
{
	public class FigureInfo : ICloneable, IEditable
	{

		public Point leftTopPos { get; set; }

		public Point rightBottomPos { get; set; }

		public Tool toolType { get; set; }

		public int? borderWidth { get; set; }

		[JsonIgnore]
		public Color colorBorder { get; set; }

		[JsonIgnore]
		public Color colorFill { get; set; }

		[JsonPropertyName("ColorBorder")]
		public uint ColorBorderSerialized
		{
			get
			{
				byte[] numb = new byte[4];
				numb[0] = colorBorder.A; 
				numb[1] = colorBorder.R;
				numb[2] = colorBorder.G;
				numb[3] = colorBorder.B;
				return BitConverter.ToUInt32(numb, 0);
			}
			set
			{
				var arr = BitConverter.GetBytes(value);
				colorBorder = Color.FromArgb(arr[0], arr[1], arr[2], arr[3]);
			}
		}

		[JsonPropertyName("ColorFill")]
		public uint ColorFillSerialized
		{
			get
			{
				byte[] numb = new byte[4];
				numb[0] = colorFill.A;
				numb[1] = colorFill.R;
				numb[2] = colorFill.G;
				numb[3] = colorFill.B;
				return BitConverter.ToUInt32(numb, 0);
			}
			set
			{
				var arr = BitConverter.GetBytes(value);
				colorFill = Color.FromArgb(arr[0], arr[1], arr[2], arr[3]);
			}
		}

		public void ChangeBorderWidth(int newWidth)
		{
			borderWidth = newWidth;
		}

		public void ChangeCenterPos(Point newPos)
		{
			leftTopPos = new Point { X = leftTopPos.X + newPos.X, Y = leftTopPos.Y + newPos.Y };
			rightBottomPos = new Point { X = rightBottomPos.X + newPos.X, Y = rightBottomPos.Y + newPos.Y };
		}

		public void ChangeColorBorder(Color colorBorder)
		{
			this.colorBorder = colorBorder;
		}

		public void ChangeColorFill(Color colorFill)
		{
			this.colorFill = colorFill;
		}

		public void ChangeSizes(int width, int height)
		{
			var leftTop = new Point { X = (leftTopPos.X > rightBottomPos.X ? rightBottomPos.X : leftTopPos.X), Y = leftTopPos.Y > rightBottomPos.Y ? rightBottomPos.Y : leftTopPos.Y };
			leftTopPos = leftTop;
			rightBottomPos = new Point { X = leftTop.X + width, Y = leftTop.Y + height };
		}

		public object Clone()
		{
			return new FigureInfo
			{
				leftTopPos = leftTopPos,
				rightBottomPos = rightBottomPos,
				toolType = toolType,
				borderWidth = borderWidth,
				colorBorder = colorBorder,
				colorFill = colorFill
			};
		}
	}
}
