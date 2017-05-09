using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using CsvHelper.Configuration;
using Microsoft.Win32;
using SoftwareTestingWPF.Base;
using SoftwareTestingWPF.Mappers;
using SoftwareTestingWPF.Utils;
using SoftwareTestingWPF.View.Canander;

namespace SoftwareTestingWPF
{
    class MainWindowViewModel : PropertyChangedBase
    {
        private string _calanderShowResult;
        public ICommand ParseDateCommand
        {
            get
            {
                return new RelayCommand(ParseDateManually, CanDoParse);
            }
        }

        public ICommand BatchCalculateBabyBreakCommand
        {
            get
            {
                return new RelayCommand(BatchCalculateBabyBreak, CanDoParse);
            }
        }

        void BatchCalculateBabyBreak(object sender)
        {
            var filePath = GetInputCurWindowFilePath();
            if (null == filePath) return;
            InitReportVisuals();
            var dataList = FileUtils<BabyBreakCSV>.ReadDateList(filePath);
            if (null != dataList)
            {
                CountControl.SetTotal(dataList.Count);
                int trueCnt = 0, falseCnt = 0;

                foreach (var babyBreakCsv in dataList)
                {
                    var cnt = GetBabyBreakCount(
                        babyBreakCsv.is_woman,
                        babyBreakCsv.late_marriage,
                        babyBreakCsv.late_birth,
                        babyBreakCsv.less_7_month,
                        babyBreakCsv.dystocia,
                        babyBreakCsv.less_30_day
                    );

                    babyBreakCsv.Result = cnt.ToString();

                    if (cnt == babyBreakCsv.result_day)
                    {
                        trueCnt++;
                        babyBreakCsv.IsCorrect = "True";
                    }
                    else
                    {
                        falseCnt++;
                        babyBreakCsv.IsCorrect = "False";
                    }
                    
                }
                CountControl.SetPassed(trueCnt);
                CountControl.SetFailed(falseCnt);
                //WriteResult(dataList);
                FileUtils<BabyBreakCSV>.WriteResult(dataList, "rBabyBreaks.csv");
                DefactPercentControl.SetValues(trueCnt, falseCnt);
            }
        }

        int GetBabyBreakCount(
            bool female,
            bool lateMar,
            bool lateBreed,
            bool less7,
            bool dystocia,
            bool less30
        )
        {
            int normal = 90;
            int smallBreed = 0;
            int dystociaDay = 15;
            int late = 30;
            int accompany = 7;
            int total = 0;

            if (female)
            {
                total += normal;
                if (less7)
                {
                    return normal;
                }
                if (lateBreed && lateMar)
                {
                    total += late;
                }
                if (dystocia)
                {
                    total += dystociaDay;
                }
            }
            else
            {
                if (lateBreed && lateMar)
                {
                    total += accompany;
                }
            }
            return total;
        }

        void InitReportVisuals()
        {
            CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
            DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow,
                "DefactPercentControl");
        }

        string GetInputCurWindowFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }

            return null;
        }

        public ICommand BatchCalculateComputerCommand
        {
            get
            {
                return new RelayCommand(BatchCalculate, CanDo);
            }
        }

        public ICommand CalculateTotalSaleCommand
        {
            get { return new RelayCommand(CalculateTotal, CanDo); }
        }

        public ICommand ParseTriangleCommand
        {
            get { return new RelayCommand(ParseTriangle, CanDo); }
        }

        public ICommand BatchDeterTri
        {
            get { return new RelayCommand(BatchDeterTriFun, CanDo); }
        }

        public ICommand BatchTelecomCalFunCommand
        {
            get { return new RelayCommand(BatchTelecomCalFun, CanDo); }
        }

        public ICommand ParseTelecomCommand
        {
            get { return new RelayCommand(ParseTelecom, CanDo); }
        }

        public void ParseTelecom(object sender)
        {
            double[] res = CalculateTelecomFee(TelecomTotalMin, TelecomTotalTimes);
            TelecomTotal = res[1];
        }

        public void BatchDeterTriFun(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
                DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow,
                    "DefactPercentControl");

                var dataList = FileUtils<TriangleMapper>.ReadDateList(fileDialog.FileName);
                if (null != dataList)
                {
                    CountControl.SetTotal(dataList.Count);
                    int trueCnt = 0, falseCnt = 0;

                    foreach (var csvMapper in dataList)
                    {
                        var parseResult = GeneralParser.ParseTriangle(csvMapper.A, csvMapper.B, csvMapper.C, GetParseTriangleStr);
                        csvMapper.Result = parseResult;

                        if (parseResult.Equals("不能构成三角形."))
                        {
                            csvMapper.Exception = "不能构成三角形.";
                        }

                        if (parseResult.Equals(csvMapper.Expected))
                        {
                            csvMapper.IsCorrect = "true";
                            trueCnt++;

                        }
                        else
                        {
                            csvMapper.IsCorrect = "false";
                            falseCnt++;

                        }


                    }
                    CountControl.SetPassed(trueCnt);
                    CountControl.SetFailed(falseCnt);
                    //WriteResult(dataList);
                    FileUtils<TriangleMapper>.WriteResult(dataList, "r3.csv");
                    DefactPercentControl.SetValues(trueCnt, falseCnt);
                }
            }
        }

        private int _telecomTotalMin;
        private int _telecomTotalTimes;
        private double _telecomTotal;

        public int TelecomTotalMin
        {
            get { return _telecomTotalMin; }
            set
            {
                _telecomTotalMin = value;
                NotifyOfPropertyChange(nameof(TelecomTotalMin));
            }
        }

        public int TelecomTotalTimes
        {
            get { return _telecomTotalTimes; }
            set
            {
                _telecomTotalTimes = value;
                NotifyOfPropertyChange(nameof(TelecomTotalTimes));
            }
        }

        public double TelecomTotal
        {
            get { return _telecomTotal; }
            set
            {
                _telecomTotal = value;
                NotifyOfPropertyChange(nameof(TelecomTotal));
            }
        }

        public void BatchTelecomCalFun(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
                DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow,
                    "DefactPercentControl");
                var dataList = FileUtils<TelecomMapper>.ReadDateList(fileDialog.FileName);
                if (null != dataList)
                {
                    CountControl.SetTotal(dataList.Count);
                    int trueCnt = 0, falseCnt = 0;

                    foreach (var csvMapper in dataList)
                    {
                        var parseResult = CalculateTelecomFee(csvMapper.minutes, csvMapper.times);
                        csvMapper.ResultDiscount = parseResult[0].ToString();
                        csvMapper.ResultFee = parseResult[1].ToString();
                        if (Math.Abs(double.Parse(csvMapper.ExpectedDiscount) - parseResult[0]) < 1e10 - 3 && Math.Abs(double.Parse(csvMapper.ExpectedFee) - parseResult[1]) < 1e10 - 3)
                        {
                            csvMapper.IsCorrect = "True";
                            trueCnt++;
                        }
                        else
                        {
                            csvMapper.IsCorrect = "False";
                            falseCnt++;
                        }
                    }
                    CountControl.SetPassed(trueCnt);
                    CountControl.SetFailed(falseCnt);
                    //WriteResult(dataList);
                    FileUtils<TelecomMapper>.WriteResult(dataList, "rTelecom.csv");
                    DefactPercentControl.SetValues(trueCnt, falseCnt);
                }
            }
        }

        private double _aLen, _bLen, _cLen;

        public double ALen
        {
            get { return _aLen; }
            set
            {
                _aLen = value;
                NotifyOfPropertyChange(nameof(ALen));
            }
        }
        public double BLen
        {
            get { return _bLen; }
            set
            {
                _bLen = value;
                NotifyOfPropertyChange(nameof(BLen));
            }
        }
        public double CLen
        {
            get { return _cLen; }
            set
            {
                _cLen = value;
                NotifyOfPropertyChange(nameof(CLen));
            }
        }

        public void ParseTriangle(object sender)
        {
            double a, b, c;
            a = ALen;
            b = BLen;
            c = CLen;
            TriangleShowResult = GetParseTriangleStr(a, b, c);
        }

        private string _triangleShowResult;
        public string TriangleShowResult
        {
            get { return _triangleShowResult; }
            set
            {
                _triangleShowResult = value;
                NotifyOfPropertyChange(nameof(TriangleShowResult));
            }
        }

        private string GetParseTriangleStr(double a, double b, double c)
        {
            string res = "";
            if (a > 0 && b > 0 && c > 0)
            {

                if (a + b > c && a + c > b && b + c > a)
                {
                    if (a * a + b * b == c * c || a * a + c * c == b * b
                            || b * b + c * c == a * a)
                    {
                        if (a == b || a == c || b == c)
                        {
                            res = "为等腰直角三角形.";
                        }
                        else
                        {
                            res = "一般直角三角形.";
                        }
                    }
                    else if (a == b && b == c && a == c)
                    {
                        res = "为等边三角形.";

                    }
                    else if ((a == b && a != c) || (a == c && a != b)
                          || (b == c && a != c))
                    {
                        res = "为等腰三角形.";
                    }
                    else
                    {
                        res = "为一般三角形.";
                    }
                }
                else
                {
                    res = "不能构成三角形.";
                }

            }
            else
            {
                res = "不能构成三角形.";
            }
            return res;
        }

        public bool CanDo()
        {
            return true;
        }

        private void CalculateTotal(object sender)
        {
            string[] res = GetSumStr(OutLet, MainFrame, Monitor);
            if (res[0].Equals("-1"))
            {
                TotalSaleVal = res[1];
            }
            else
            {
                TotalSaleVal = res[0];
            }
        }
        private string[] GetSumStr(int OutLet, int MainFrame, int Monitor)
        {
            StringBuilder sb = new StringBuilder();
            string[] result = new string[2];
            if (OutLet < 1)
            {
                sb.Append("外设数量<1  ");
            }
            if (OutLet > 90)
            {
                sb.Append("外设数量>90  ");
            }
            if (MainFrame < 1)
            {
                sb.Append("主机数量<1  ");
            }
            if (MainFrame > 70)
            {
                sb.Append("主机数量>70  ");
            }
            if (Monitor < 1)
            {
                sb.Append("显示器数量<1  ");
            }
            if (Monitor > 80)
            {
                sb.Append("显示器数量>80  ");
            }
            if (sb.Length > 2)
            {
                result[0] = (-1).ToString();
                result[1] = sb.ToString();
                return result;
            }

            long sum = 25 * OutLet + 45 * MainFrame + 30 * Monitor;
            result[0] = sum + "$";
            return result;
        }

        public ICommand BatchDateCommand
        {
            get { return new RelayCommand(DoBatchParse, CanDoBatch); }
        }

        private bool CanDoBatch()
        {
            return true;
        }

        public MainWindowViewModel()
        {

        }
        public CaseCountControl CountControl { get; set; }
        public DefactPercentControl DefactPercentControl { get; set; }

        private void BatchCalculate(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                var dataList = FileUtils<ComputerMapper>.ReadDateList(fileDialog.FileName);
                if (null != dataList)
                {
                    int passed, failed;
                    passed = failed = 0;
                    CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
                    DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow, "DefactPercentControl");

                    foreach (var csvMapper in dataList)
                    {
                        string[] strRes = GetSumStr(csvMapper.OutLet, csvMapper.MainFrame, csvMapper.Monitor);
                        if (strRes[0].Equals(csvMapper.Expected))
                        {
                            csvMapper.IsCorrect = "true";
                            passed++;
                        }
                        else
                        {
                            csvMapper.IsCorrect = "false";
                            failed++;
                        }

                        csvMapper.Result = strRes[0];
                        csvMapper.Exception = strRes[1];
                    }

                    FileUtils<ComputerMapper>.WriteResult(dataList, "r2.csv");

                    CountControl.SetPassed(passed);
                    CountControl.SetFailed(failed);
                    CountControl.SetTotal(passed + failed);
                    DefactPercentControl.SetValues(passed, failed);
                }
            }
        }

        void DoBatchParse(object parameter)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {

                CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
                DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow, "DefactPercentControl");

                var dataList = FileUtils<DateMapper>.ReadDateList(fileDialog.FileName);
                if (null != dataList)
                {
                    CountControl.SetTotal(dataList.Count);
                    int trueCnt = 0, falseCnt = 0;

                    foreach (var csvMapper in dataList)
                    {
                        var parseResult = GeneralParser.ParseDate(csvMapper.DateStr);
                        if (parseResult[0].Equals(true))
                        {
                            csvMapper.Result = parseResult[1] as string;
                            trueCnt++;
                        }
                        else
                        {
                            csvMapper.Result = "Parse Error";
                            csvMapper.Exception = parseResult[1] as string;
                            falseCnt++;
                        }
                    }
                    CountControl.SetPassed(trueCnt);
                    CountControl.SetFailed(falseCnt);
                    //WriteResult(dataList);
                    FileUtils<DateMapper>.WriteResult(dataList, "r1.csv");
                    DefactPercentControl.SetValues(trueCnt, falseCnt);
                }
            }
        }

        private bool CanDoParse()
        {
            return true;
        }

        public string CalanderShowResult
        {
            get { return _calanderShowResult; }
            set
            {
                _calanderShowResult = value;
                NotifyOfPropertyChange(nameof(CalanderShowResult));
            }
        }
        private int _outLet;
        private int _mainFrame;
        private int _monitor;
        private string total;

        public int OutLet
        {
            get { return _outLet; }
            set
            {
                _outLet = value;
                NotifyOfPropertyChange(nameof(OutLet));
            }
        }

        public int MainFrame
        {
            get { return _mainFrame; }
            set
            {
                _mainFrame = value;
                NotifyOfPropertyChange(nameof(MainFrame));
            }
        }

        public int Monitor
        {
            get { return _monitor; }
            set
            {
                _monitor = value;
                NotifyOfPropertyChange(nameof(Monitor));
            }
        }

        public string TotalSaleVal
        {
            get { return total; }
            set
            {
                total = value;
                NotifyOfPropertyChange(nameof(TotalSaleVal));
            }
        }

        private double[] discountTable = {
            0.01, 0.015, 0.02, 0.025, 0.03
        };

        private double[] CalculateTelecomFee(int callMiniute, int dueTimes)
        {
            double[] result = new double[2];
            result[1] = 25;
            double preMin = 0.15;

            if (callMiniute < 0 || dueTimes < 0 || callMiniute > 31 * 24 * 60 || dueTimes > 11)
            {
                result[0] = -1;
                result[1] = -1;
                return result;
            }

            if (callMiniute <= 60)
            {
                //if (dueTimes <= 1)
                //{
                //    result[0] = discountTable[0];
                //    result[1] += callMiniute * preMin * (1 - discountTable[0]);
                //}
                //else
                //{
                //    result[0] = 0;
                //    result[1] += callMiniute * preMin;
                //}
                CalcTelecom(callMiniute, preMin, 1, dueTimes, 0, result);
            }
            else if (callMiniute <= 120)
            {
                CalcTelecom(callMiniute, preMin, 2, dueTimes, 1, result);
            }
            else if (callMiniute <= 180)
            {
                CalcTelecom(callMiniute, preMin, 3, dueTimes, 2, result);
            }
            else if (callMiniute <= 300)
            {
                CalcTelecom(callMiniute, preMin, 3, dueTimes, 3, result);
            }
            else
            {
                CalcTelecom(callMiniute, preMin, 6, dueTimes, 4, result);
            }

            return result;
        }

        private void CalcTelecom(int callMiniute, double perMin, int dueLimit, int dueTime, int discountIndex, double[] result)
        {
            if (dueTime <= dueLimit)
            {
                result[0] = discountTable[discountIndex];
                result[1] += callMiniute * perMin * (1 - discountTable[discountIndex]);
            }
            else
            {
                result[0] = 0;
                result[1] += callMiniute * perMin;
            }
        }

        void ParseDateManually(object parameter)
        {
            TextBox textBox = parameter as TextBox;
            string resultText = "";
            if (null == textBox)
            {
                return;
            }

            try
            {
                DateTime time = DateTime.Parse(textBox.Text);
                time = time.AddDays(1);
                resultText = time.ToLongDateString();
                Console.WriteLine(resultText);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resultText = "输入格式错误:" + e.Message;
            }
            finally
            {
                CalanderShowResult = resultText;
            }
        }
    }
}
