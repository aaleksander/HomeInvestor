using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace HomeInvestor.Exts
{
	/// <summary>
	/// Description of ColorExts.
	/// </summary>
	public static class ColorExts
	{
		public static string ToStr(this SolidColorBrush br)
		{
			var r = br.Color.R;
			var g = br.Color.G;
			var b = br.Color.B;
			return string.Format("{0}.{1}.{2}",r, g, b);
		}

		public static SolidColorBrush ToColor(this string str)
		{
			if( str == null )
				return Brushes.White;
			if( str == "" )
				return Brushes.White;
			var pp = str.Split('.');
			var r = byte.Parse(pp[0]);
			var g = byte.Parse(pp[1]);
			var b = byte.Parse(pp[2]);
			return new SolidColorBrush(Color.FromRgb(r, g, b));
		}
	}
	
    public class ColorHelper
    {
        public static List<KeyValuePair<String, Color>> AllColors
        {
        	get{
	            var res = typeof(Colors)
	                .GetProperties()
	                .Where(prop =>
	                    typeof(Color).IsAssignableFrom(prop.PropertyType))
	                .Select(prop =>
	            	 	new KeyValuePair<String, Color>(prop.Name, (Color)prop.GetValue(null))).ToList();
	            
	            //удаляем тусклые, темные и почти одинаковые
	            var no = new List<string>{
	            	"Azure", "AliceBlue", "Beige", "Bisque", "Black", "Blanch*", "Blue*", "Brown", "Chocolate", "Cornsilk",
	            	"Dark*", "DimGray", "Indigo", "Ivory", "Lavender*", "White*", "DeepSkyBlue", "Cyan", "DodgerBlue",
	            	"Fire*", "Floral*", "Forest*", "Fu*", "Gai*", "Gho*", "Green*", "Hon*", "Hot*", "Ind*", "Light*", "L*", "M*",
	            	"Navy", "Old*", "OliveD*", "P*", "R*", "Kh*", "Nav*", "Snow", "T*"
	            };

	            foreach(var p in no)
	            {
	            	if( p.EndsWith("*") )
	            	{
	            		res.RemoveAll(x => x.Key.StartsWith(p.Replace("*", "")));
	            		continue;
	            	}

	            	res.RemoveAll(x => x.Key == p);
	            }

	            return res;
        	}
        }
        
    }
}
