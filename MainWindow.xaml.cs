using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace matrixPoluektov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            this.MatrixSize = Enumerable.Range(1, 12).ToArray();
            this.DataContext = this;
            


        }

        public IList MatrixSize { get; private set; }
        public object Matrix { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var size = (int)e.AddedItems[0];
            this.UpdateMatrix(size);
        }

        void UpdateMatrix(int size)
        {
            var dt = new DataTable();

            Random random = new Random();

            

            for (var i = 0; i < size; i++)
                dt.Columns.Add(new DataColumn("c" + i, typeof(string)));
            for (var i = 0; i < size; i++)
            {
                var r = dt.NewRow();
                for (var c = 0; c < size; c++)
                    r[c] = random.Next(-100, 100).ToString();
                dt.Rows.Add(r);
            }
            
            int sumOfPositiveNumbers = 0;
            int sumOfCurRow = 0;
            string text = "";

            for (var i = 0; i < size; i++)
            {
                var r = dt.NewRow();
                for (var j = 0; j < size; j++)
                {
                    int number = Convert.ToInt32(dt.Rows[j][i]);
                    if (number < 0)
                    {
                        isAllPositive = false;
                        sumOfCurRow = 0;
                        break;
                    }

                    sumOfCurRow += number;

                    if (j == size - 1)
                    {
                        text += $"sum of positive column №{i+1} = {sumOfCurRow}\n";
                        sumOfPositiveNumbers += sumOfCurRow;
                        sumOfCurRow = 0;
                        
                    }
                }
            }
            this.myTextBox.Text = $"{text}sum of all positive columns: {sumOfPositiveNumbers}";
            this.Matrix = dt.DefaultView;
            PropertyChanged(this, new PropertyChangedEventArgs("Matrix"));
        }
    }
}
