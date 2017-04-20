using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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
using LiveCharts;
using LiveCharts.Wpf;
using SoftwareTestingWPF.Annotations;

namespace SoftwareTestingWPF.View.Canander
{
    /// <summary>
    /// CaseCountControl.xaml 的交互逻辑
    /// </summary>
    public partial class CaseCountControl : INotifyPropertyChanged
    {
        public CaseCountControl()
        {
            InitializeComponent();
            //_values = new ChartValues<double>();
            //_values.Add(0);
            _totalValues = new ChartValues<double>();
            _passedValues = new ChartValues<double>();
            _failedValues = new ChartValues<double>();

            Brush b1 = new SolidColorBrush(Color.FromRgb(02,0xa8,0xf3));
            Brush b2 = new SolidColorBrush(Color.FromRgb(65,0xc1,78));
            Brush b3 = new SolidColorBrush(Color.FromRgb(0xF4, 0x43, 0x36));

            SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "测试用例总数量",
                    Values = _totalValues,
                    Fill = b1
                },
                new RowSeries
                {
                    Title = "通过用例数量",
                    Values = _passedValues,
                    Fill = b2
                },
                new RowSeries
                {
                    Title = "未通过用例数量",
                    Values = _failedValues,
                    Fill = b3
                }
            };

            DataContext = this;
        }

        private ChartValues<double> _totalValues;
        private ChartValues<double> _passedValues;
        private ChartValues<double> _failedValues;
        private ChartValues<double> _values;

        public void SetPassed(double val)
        {
            while (_passedValues.Count > 0)
            {
                _passedValues.RemoveAt(0);
            }

            _passedValues.Add(val);
        }
        public void SetFailed(double val)
        {
            while (_failedValues.Count > 0)
            {
                _failedValues.RemoveAt(0);
            }

            _failedValues.Add(val);
        }
        public void SetTotal(double val)
        {
            while (_totalValues.Count > 0)
            {
                _totalValues.RemoveAt(0);
            }

            _totalValues.Add(val);
        }

        public ChartValues<double> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
