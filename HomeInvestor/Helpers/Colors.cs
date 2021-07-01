using wpf = System.Windows;
using System;
using System.Windows.Media;
using old = System.Windows.Forms;

namespace HomeInvestor.Helpers
{
	/// <summary>
	/// Description of Colors.
	/// </summary>
	public static class Colors
	{
		public static SolidColorBrush GetColor(SolidColorBrush br)
		{
			var dlg = new old.ColorDialog();
	      	dlg.Color = System.Drawing.Color.FromArgb(255, br.Color.R, br.Color.G, br.Color.B);
	      	var res = dlg.ShowDialog();
	      	return new SolidColorBrush(wpf.Media.Color.FromRgb(dlg.Color.R, dlg.Color.G, dlg.Color.B));
		}
	}
}
