using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace THSchedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient client = new HttpClient();
        private TimetableData timetableData;
        private readonly DispatcherTimer updateTimer = new DispatcherTimer();
        private DateTimeOffset utcNow => DateTimeOffset.UtcNow;
        private bool minified = true;


        public MainWindow()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => UpdateTimer_Tick(null, null)));
            updateTimer.Interval = TimeSpan.FromSeconds(60);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            var day = (utcNow.DayOfWeek == 0) ? 7 : (int)utcNow.DayOfWeek;
            var hour = utcNow.Hour;
            if (minified) ShowDayColumn(day);

            var json = await client.GetStringAsync("https://thishabbo.com/api/events/timetable");
            ClearEvents();

            timetableData = JsonConvert.DeserializeObject<TimetableData>(json);
            foreach (var evt in timetableData.Timetable)
            {
                var eventLabel = new Label
                {
                    Content = evt.Event?.Name ?? evt.Name,
                    ToolTip = new ToolTip()
                    {
                        Content = $"{evt.Event?.Name ?? evt.Name}\r\n{evt.User.Habbo}\r\nTH: {evt.User.Nickname}"
                    },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ForceCursor = true,

                    Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200))
                };

                if (evt.Day == day && evt.Hour == hour)
                {
                    eventLabel.FontWeight = FontWeight.FromOpenTypeWeight(600);
                    eventLabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }

                Grid.SetColumn(eventLabel, evt.Day);
                Grid.SetRow(eventLabel, evt.Hour + 1);
                grdTimetable.Children.Add(eventLabel);
            }
        }

        private void ClearEvents()
        {
            List<UIElement> elements = new List<UIElement>();
            foreach (object child in grdTimetable.Children)
            {
                if (child.GetType() == typeof(Label))
                {
                    if (((Label)child).ForceCursor)
                    {
                        elements.Add((UIElement)child);
                    }
                }
            }

            foreach (var el in elements)
            {
                grdTimetable.Children.Remove(el);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            if (minified) ShowAllColumns();
            else
            {
                var day = (utcNow.DayOfWeek == 0) ? 7 : (int)utcNow.DayOfWeek;
                ShowDayColumn(day);
            }

            minified = !minified;
        }

        private GridLength hiddenLength = new GridLength(0);
        private GridLength shownLength = new GridLength(1, GridUnitType.Star);

        private void ShowDayColumn(int day)
        {
            grdTimetable.RowDefinitions.First().Height = hiddenLength;
            var columns = grdTimetable.ColumnDefinitions.Skip(1);
            var shown = columns.ElementAt(day - 1);

            foreach (var col in columns)
                if (col != shown)
                    col.Width = hiddenLength;

            Width = 240;
            Height = 650;
        }

        private void ShowAllColumns()
        {
            grdTimetable.RowDefinitions.First().Height = shownLength;
            var columns = grdTimetable.ColumnDefinitions.Skip(1);

            foreach (var col in columns)
                col.Width = shownLength;

            Width = 1000;
            Height = 700;
        }
    }
}
