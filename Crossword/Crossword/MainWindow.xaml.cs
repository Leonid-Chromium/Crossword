using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace Crossword
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public class Coordinate
    {
        public uint abcise { get; set; }
        public uint ordinate { get; set; }


        public Coordinate(uint x, uint y)
        {
            this.abcise = x;
            this.ordinate = y;
        }
    }

    public partial class MainWindow : Window
    {
        public DataTable dataTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            init("Влияние неопределённости на процесс достижение поставленных целей", "Риск", new Coordinate(1, 1), true);
            init("Неправомерное получение информации с использованием технического средства, осуществляющего обнаружение, приём и обработку информативных сигналов", "Перехват", new Coordinate(1, 1), true);
            init("Определение", "Значениеkj", new Coordinate(1, 10), true);
            init("Определение", "Значение2", new Coordinate(1, 4), false);
            init("Определение", "Значение3", new Coordinate(4, 1), true);
            init("Определение", "Значение4", new Coordinate(1, 1), true);
            init("Определение", "Значение5", new Coordinate(1, 1), true);


            dataTable.Columns.Add("Дата");
            dataTable.Columns.Add("Время");
            dataTable.Columns.Add("Тип события");
            dataTable.Columns.Add("Описание");
            for (int i = 0; i < 10; i++)
            {
                dataTable.Rows.Add("t", "e", "s", "t");
            }

            Trace.WriteLine("");
            Trace.WriteLine("общая информация");
            Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
            Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));
            
            for(int i = 0; i<dataTable.Rows.Count; i++)
            {
                for(int j = 0; j<dataTable.Columns.Count; j++)
                {
                    Trace.Write(dataTable.Rows[i].ItemArray[j].ToString());
                    Trace.Write("|");
                }
                Trace.WriteLine("");
            }

            myDataGrid.DataContext = myDataGrid;
        }

        public void init(string description, string value, Coordinate startCoordinate, bool vector)
        {
            // true = vertical

            //Trace.WriteLine(String.Format("description = " + description));
            //Trace.WriteLine(String.Format("value = " + value));
            //Trace.WriteLine(String.Format("start ord = " + startCoordinate.ordinate));
            //Trace.WriteLine(String.Format("start abc = " + startCoordinate.abcise));
            //Trace.WriteLine(String.Format("Vector = " + vector));

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            TextBlock descriptionTB = new TextBlock();
            TextBox valueTB = new TextBox();
            descriptionTB.Width = 200;
            descriptionTB.Text = description;
            descriptionTB.TextWrapping = TextWrapping.Wrap;
            stackPanel.Children.Add(descriptionTB);
            valueTB.Text = value;
            stackPanel.Children.Add(valueTB);
            stackPanel.Margin = new Thickness(20, 10, 10, 0);

            if (vector)
            {
                VerticalST.Children.Add(stackPanel);
            }
            else
            {
                HorizontalST.Children.Add(stackPanel);
            }

            DTFun(startCoordinate.abcise, startCoordinate.ordinate, value, vector);

            Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
            Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));
        }

        public void DTFun(uint x, uint y, string value, bool vector)
        {
            if (vector)
                y = (y + (uint)value.Length) - 1;
            else
                x = (x + (uint)value.Length) - 1;

            while (x > dataTable.Columns.Count)
                dataTable.Columns.Add(Convert.ToString(dataTable.Columns.Count), typeof(string));

            while (y > dataTable.Rows.Count)
                dataTable.Rows.Add(Convert.ToString(dataTable.Rows.Count));
        }
    }
}
