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

namespace ChessGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //public void CreateTiles()
        //{
        //    int rows = 8;
        //    int columns = 8;

        //    PlayingArea.Rows = rows;
        //    PlayingArea.Columns = columns;

        //    for (int y = 0; y < rows; y++)
        //    {

        //        for (int x = 0; x < columns; x++)
        //        {
        //            //Declare Label and Cell
        //            Label SpaceLabel = new Label();
        //            Coordinate c = new Coordinate(columns, rows);

        //            //Poulate Grid with labels
        //            Grid.SetRow(SpaceLabel, y);
        //            Grid.SetColumn(SpaceLabel, x);

        //            //Label attributes
        //            SpaceLabel.Name = "Test";
        //            SpaceLabel.BorderBrush = Brushes.Black;
        //            SpaceLabel.BorderThickness = new Thickness(1);

        //            PlayingArea.Children.Add(SpaceLabel);
        //            SpaceLabel.Content = $"{x},{y}";

        //            if ((x + y) % 2 == 0)
        //            {
        //                SpaceLabel.Background = Brushes.Wheat;
        //            }
        //            else
        //            {
        //                SpaceLabel.Background = Brushes.Green;
        //            }

        //        }
        //    }

        //}
    }
}
