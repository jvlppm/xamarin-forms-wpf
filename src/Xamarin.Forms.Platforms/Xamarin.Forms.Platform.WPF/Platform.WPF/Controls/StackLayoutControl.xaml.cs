using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFGrid = System.Windows.Controls.Grid;
using WPFGridLength = System.Windows.GridLength;
using WPFGridUnitType = System.Windows.GridUnitType;
using WPFRowDefinition = System.Windows.Controls.RowDefinition;
using WPFColumnDefinition = System.Windows.Controls.ColumnDefinition;
using Xamarin.Forms.Platform.WPF.Converters;

namespace Xamarin.Forms.Platform.WPF.Controls
{
    /// <summary>
    /// Interaction logic for CustomizableItemsControl.xaml
    /// </summary>
    public partial class StackLayoutControl : UserControl
    {
        WPFGrid _grid;
        Orientation _orientation;

        LayoutOptionsToLengthConverter _converter;

        public IEnumerable<View> ItemsSource
        {
            get { return (IEnumerable<View>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<View>), typeof(StackLayoutControl), new PropertyMetadata(null));

        public StackLayoutControl()
        {
            InitializeComponent();
        }

        public void SetOrientation(Orientation orientation)
        {
            _orientation = orientation;
            UpdateGrid();
        }

        void UpdateGrid()
        {
            if (_grid == null)
                return;

            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();

            if (_converter == null)
                _converter = new LayoutOptionsToLengthConverter();

            if (_orientation == Orientation.Vertical)
            {
                for (int i = 0; i < ItemsControl.Items.Count; i++)
                {
                    var element = _grid.Children.OfType<UIElement>().Skip(i).First();
                    WPFGrid.SetRow(element, i);
                    WPFGrid.SetColumn(element, 0);

                    var row = new WPFRowDefinition { Height = new WPFGridLength(0, WPFGridUnitType.Auto) };
                    _grid.RowDefinitions.Add(row);
                    var binding = new System.Windows.Data.Binding(View.VerticalOptionsProperty.PropertyName)
                    {
                        Source = ItemsSource.Skip(i).FirstOrDefault(),
                        Converter = _converter
                    };
                    row.SetBinding(WPFRowDefinition.HeightProperty, binding);
                }
            }
            else
            {
                for (int i = 0; i < ItemsControl.Items.Count; i++)
                {
                    var element = _grid.Children.OfType<UIElement>().Skip(i).First();
                    WPFGrid.SetRow(element, 0);
                    WPFGrid.SetColumn(element, i);

                    var col = new WPFColumnDefinition { Width = new WPFGridLength(0, WPFGridUnitType.Auto) };
                    _grid.ColumnDefinitions.Add(col);
                    var binding = new System.Windows.Data.Binding(View.HorizontalOptionsProperty.PropertyName)
                    {
                        Source = ItemsSource.Skip(i).FirstOrDefault(),
                        Converter = _converter
                    };
                    col.SetBinding(WPFColumnDefinition.WidthProperty, binding);
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _grid = sender as WPFGrid;
            UpdateGrid();
        }
    }
}
