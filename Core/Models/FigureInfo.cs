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
	public class FigureInfo : ICloneable
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
