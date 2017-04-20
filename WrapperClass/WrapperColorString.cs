using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Wrapper
{
    public class WrapperColorString
    {
        public static Color GetColorFromString(string c)
        {
            /***/
            if (c == "Gray") return Color.Gray;
            else if (c == "White") return Color.White;
            else if (c == "Black") return Color.Black;
            else if (c == "Red") return Color.Red;
            else if (c == "Green") return Color.Green;
            else if (c == "Blue") return Color.Blue;
            else if (c == "Cyan") return Color.Cyan;
            else if (c == "Magenta") return Color.Magenta;
            else if (c == "Yellow") return Color.Yellow;
            else if (c == "Brown") return Color.Brown;
            else if (c == "Lime") return Color.Lime;
            else if (c == "Orange") return Color.Orange;
            else if (c == "DodgerBlue") return Color.DodgerBlue;
            else
                return Color.SkyBlue;
        }
        public static string GetStringFromColor(Color c)
        {
            /***/
            if (c == Color.Gray) return "Gray";
            else if (c == Color.White) return "White";
            else if (c == Color.Black) return "Black";
            else if (c == Color.Red) return "Red";
            else if (c == Color.Green) return "Green";
            else if (c == Color.Blue) return "Blue";
            else if (c == Color.Cyan) return "Cyan";
            else if (c == Color.Magenta) return "Magenta";
            else if (c == Color.Yellow) return "Yellow";
            else if (c == Color.Brown) return "Brown";
            else if (c == Color.Lime) return "Lime";
            else if (c == Color.Orange) return "Orange";
            else if (c == Color.DodgerBlue) return "DodgerBlue";
            else
                return "SkyBlue";
        }
        
    }
}
