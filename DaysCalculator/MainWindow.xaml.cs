using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace DaysCalculator
{

    public partial class MainWindow : Window
    {
        private DateTime _vacationDate;
        private List<DateTime> _holidays;

        public MainWindow()
        {
            InitializeComponent();
            _holidays = LoadHolidays();

            _vacationDate = new DateTime(2025, 06, 1);
            datePicker.SelectedDate = _vacationDate;
            UpdateCountdown();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _vacationDate = datePicker.SelectedDate.Value;
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            DateTime now = DateTime.Now;

            if (_vacationDate < now)
            {
                countdownLabel.Content = "Отпуск уже прошел!";
                return;
            }

            TimeSpan remainingTime = _vacationDate - now;
            int remainingDays = GetWorkingDays(_vacationDate, now);

            countdownLabel.Content = $"До лета: {remainingDays} дней";
        }

        private int GetWorkingDays(DateTime endDate, DateTime startDate)
        {
            int workingDays = 0;

            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (!_holidays.Contains(date))
                    {
                        workingDays++;
                    }
                }
            }

            return workingDays;
        }

        private List<DateTime> LoadHolidays()
        {
            List<DateTime> holidays = new List<DateTime>();
            holidays.Add(new DateTime(2024, 1, 1));
            holidays.Add(new DateTime(2024, 5, 1));
            holidays.Add(new DateTime(2024, 12, 25));
            return holidays;
        }
    }
}