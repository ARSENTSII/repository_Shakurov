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
using System.Windows.Shapes;
using Microsoft.Win32;
using Task2.Services;

namespace Task2.Views
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private ReportService _service = new ReportService();
        private List<ProjectReportRow> _projectRows;
        private List<EmployeeReportRow> _employeeRows;

        public ReportWindow()
        {
            InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            try
            {
                _projectRows = _service.GetProjectReport();
                _employeeRows = _service.GetEmployeeReport();

                dgProjectReport.ItemsSource = _projectRows;
                dgEmployeeReport.ItemsSource = _employeeRows;
                txtSummary.Text = _service.GetSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка построения отчёта: " + ex.Message);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadReports();
        }


        private void SaveCsv_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV файл (*.csv)|*.csv",
                FileName = "report_" + DateTime.Today.ToString("yyyy-MM-dd") + ".csv"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _service.SaveCsv(dialog.FileName, _projectRows, _employeeRows);
                    MessageBox.Show("Отчёт сохранён");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить: " + ex.Message);
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
