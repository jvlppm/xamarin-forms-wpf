using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Xamarin.Forms.Platform.WPF
{
    public interface IWPFRenderer
    {
        FrameworkElement Element { get; }
        VisualElement Model { get; set; }
    }
}
