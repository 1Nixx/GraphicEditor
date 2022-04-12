using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Models
{
	internal interface ISelectable
	{
		bool IsSelected(Point pointPos);
		void SetSelection();
		void RemoveSelection();
	}
}
