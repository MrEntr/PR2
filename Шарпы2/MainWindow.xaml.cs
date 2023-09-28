using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Шарпы2
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Timer t = new Timer(1000);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime ci = DateTime.Now;
            this.Dispatcher.Invoke(new Action(() => this.Label1.Content = ci.ToLongTimeString()));
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get reference.
            var calendar = sender as Calendar;

            if (calendar.SelectedDate.HasValue)
            {

                var arDays = this.lst.Where(x => calendar.SelectedDate.Value.Date == x.Date).Cast<Day>();
                foreach (var oneDay in arDays)
                {
                    int iCount = 0;
                  /*  foreach (TextBox tb in this.StEdits.Children.Cast<TextBox>())
                        tb.Text = oneDay.ListTasks[iCount++].ToDo;*/

                }


                // ... See if a date is selected.
                if (calendar.SelectedDate.HasValue)
                {
                    // ... Display SelectedDate in Title.
                    DateTime date = calendar.SelectedDate.Value;
                    this.Title = date.ToShortDateString();

                }
            }
        }

        public class Task
        {
            public string ToDo { get; set; }
            public DateTime Time { get; set; }
            public Task() { }

        }

        
        public class Day
        {
            public List<Task> ListTasks { set; get; }
            public DateTime Date { get; set; }
            public Day() { }

        }

        List<Day> lst = new List<Day>();


        void Serialize()
        {
            try
            {
                using (FileStream fs = new FileStream("1.xml", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Day>));
                    xml.Serialize(fs, lst);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Deserialize()
        {
            try
            {
                using (FileStream fs = new FileStream("1.xml", FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Day>));
                    this.lst = (List<Day>)xml.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

