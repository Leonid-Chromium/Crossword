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
        public uint abcise { get; set; } = 1;
        public uint ordinate { get; set; } = 1;


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
            init("Определение", "Значение", new Coordinate(2, 2), true);
            init("Влияние неопределённости на процесс достижение поставленных целей", "Риск", new Coordinate(1, 1), true);
            init("Неправомерное получение информации с использованием технического средства, осуществляющего обнаружение, приём и обработку информативных сигналов", "Перехват", new Coordinate(20, 20), false);
            init("Определение", "Значение1", new Coordinate(3, 3), true);
            init("Определение", "Значение2", new Coordinate(4, 4), true);
            init("Определение", "Значение3", new Coordinate(5, 5), true);
            init("Определение", "Значение4", new Coordinate(6, 6), true);
            init("Определение", "Значение5", new Coordinate(7, 7), false);

            //testDataTableFunction(dataTable);

            Trace.WriteLine("");
            Trace.WriteLine("Общая информация");
            Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
            Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));

            DTtoTrace(dataTable);
            myDataGrid.ItemsSource = dataTable.DefaultView;
        }

        public void DTtoTrace(DataTable dataTable)
        {
            Trace.WriteLine("");
            Trace.WriteLine("Общая информация");
            Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
            Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Trace.Write("|");
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Trace.Write(String.Format("{0,3}",dataTable.Rows[i].ItemArray[j].ToString()));
                    Trace.Write("|");
                }
                Trace.WriteLine("");
            }

            for(int i = 0; i<dataTable.Columns.Count; i++)
            {
                Trace.WriteLine(String.Format(dataTable.Columns[i].ColumnName + " " + dataTable.Columns[i].DataType));
            }
        }

        public void testDataTableFunction(DataTable dataTable)
        {
            dataTable.Columns.Add("Дата");
            dataTable.Columns.Add("Время");
            dataTable.Columns.Add("Тип события");
            dataTable.Columns.Add("Описание");
            for (int i = 0; i < 10; i++)
            {
                dataTable.Rows.Add("t", "e", "s", "t");
            }
            dataTable.Rows[18].ItemArray[5] = 2;
        }

        public void init(string description, string value, Coordinate startCoordinate, bool vector)
        {
            // true = vertical

            Trace.WriteLine(String.Format("description = " + description));
            Trace.WriteLine(String.Format("value = " + value));
            Trace.WriteLine(String.Format("start ord = " + startCoordinate.ordinate));
            Trace.WriteLine(String.Format("start abc = " + startCoordinate.abcise));
            Trace.WriteLine(String.Format("Vector = " + vector));

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

            formatDTFunction(startCoordinate.abcise, startCoordinate.ordinate, value, vector);
            fillDT(startCoordinate.abcise, startCoordinate.ordinate, value, vector);

            Trace.WriteLine(String.Format("x = " + dataTable.Columns.Count));
            Trace.WriteLine(String.Format("y = " + dataTable.Rows.Count));
        }

        public void formatDTFunction(uint x, uint y, string value, bool vector)
        {
            if (vector)
                y = (y + (uint)value.Length) - 1;
            else
                x = (x + (uint)value.Length) - 1;

            while (x > dataTable.Columns.Count)
                dataTable.Columns.Add(Convert.ToString(dataTable.Columns.Count), typeof(string));

            while (y > dataTable.Rows.Count)
                dataTable.Rows.Add(/*Convert.ToString(dataTable.Rows.Count)*/);
        }

        public void fillDT(uint x, uint y, string value, bool vector)
        {
            int newX = Convert.ToInt32(x) - 1;
            int newY = Convert.ToInt32(y) - 1;
            Trace.WriteLine(String.Format("Длина слова = " + value.Length));
            if (vector)
            {
                for (int j = 0; j < value.Length; j++)
                {
                    try
                    {
                        Trace.Write(value[j]);
                        dataTable.Rows[j + newY].SetField(dataTable.Columns[newX], value[j]);
                        Trace.WriteLine(dataTable.Rows[j + newY].ItemArray[x]);
                    }
                    catch(Exception ex)
                    {
                        Trace.WriteLine("");
                        Trace.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                for (int i = 0; i<value.Length; i++)
                {
                    try
                    {
                        Trace.Write(value[i]);
                        dataTable.Rows[newY].SetField(dataTable.Columns[i + newX], value[i]);
                        Trace.WriteLine(dataTable.Rows[newY].ItemArray[x + i]);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine("");
                        Trace.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
