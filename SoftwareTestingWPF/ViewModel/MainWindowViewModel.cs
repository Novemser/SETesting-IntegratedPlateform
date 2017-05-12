using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.Win32;
using SoftwareTestingWPF.Base;
using SoftwareTestingWPF.Mappers;
using SoftwareTestingWPF.Utils;
using SoftwareTestingWPF.View.Canander;

namespace SoftwareTestingWPF
{
    internal class MainWindowViewModel : PropertyChangedBase
    {
        private readonly double[] discountTable =
        {
            0.01, 0.015, 0.02, 0.025, 0.03
        };

        private double _aLen, _bLen, _cLen;

        private int _BabyBreakDays;
        private string _calanderShowResult;
        private bool _Dystocia;
        private int _inStockProduct;
        private int _inStockReady;

        private bool _IsFemale;
        private bool _isProductDelivered;

        private bool _isProductQualified;
        private bool _isPurchaseQualified;
        private bool _isRubbish;
        private bool _LateBreed;
        private bool _LateMarry;
        private bool _Less30;
        private bool _Less7;
        private int _mainFrame;
        private int _monitor;
        private int _outLet;
        private string _productProductionResult;
        private double _telecomTotal;

        private int _telecomTotalMin;
        private int _telecomTotalTimes;
        private int _totalRequirred;

        private string _triangleShowResult;

        private string total;

        public string ProductProductionResult
        {
            get { return _productProductionResult; }
            set
            {
                _productProductionResult = value;
                NotifyOfPropertyChange(nameof(ProductProductionResult));
            }
        }

        public bool IsProductDelivered
        {
            get { return _isProductDelivered; }
            set
            {
                _isProductDelivered = value;
                NotifyOfPropertyChange(nameof(IsProductDelivered));
            }
        }

        public bool IsPurchaseQualified
        {
            get { return _isPurchaseQualified; }
            set
            {
                _isPurchaseQualified = value;
                NotifyOfPropertyChange(nameof(IsPurchaseQualified));
            }
        }

        public bool IsRubbish
        {
            get { return _isRubbish; }
            set
            {
                _isRubbish = value;
                NotifyOfPropertyChange(nameof(IsRubbish));
            }
        }

        public bool IsProductQualified
        {
            get { return _isProductQualified; }
            set
            {
                _isProductQualified = value;
                NotifyOfPropertyChange(nameof(IsProductQualified));
            }
        }

        public int InStockProduct
        {
            get { return _inStockProduct; }
            set
            {
                _inStockProduct = value;
                NotifyOfPropertyChange(nameof(InStockProduct));
            }
        }

        public int InStockReady
        {
            get { return _inStockReady; }
            set
            {
                _inStockReady = value;
                NotifyOfPropertyChange(nameof(InStockReady));
            }
        }

        public int TotalRequired
        {
            get { return _totalRequirred; }
            set
            {
                _totalRequirred = value;
                NotifyOfPropertyChange(nameof(TotalRequired));
            }
        }

        public ICommand ParseDateCommand
        {
            get { return new RelayCommand(ParseDateManually, CanDoParse); }
        }

        public ICommand BatchCalculateBabyBreakCommand
        {
            get { return new RelayCommand(BatchCalculateBabyBreak, CanDoParse); }
        }

        public ICommand BatchCalculateProductCommand
        {
            get { return new RelayCommand(BatchCalculateProduct, CanDoParse); }
        }

        public ICommand BatchCalculateComputerCommand
        {
            get { return new RelayCommand(BatchCalculate, CanDo); }
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

        public ICommand CalculateBabyBreakCommand
        {
            get { return new RelayCommand(CalculateBabyBreak, CanDo); }
        }

        public ICommand CalculateProductProductionCommand
        {
            get { return new RelayCommand(CalculateProductProduction, CanDo); }
        }

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

        public string TriangleShowResult
        {
            get { return _triangleShowResult; }
            set
            {
                _triangleShowResult = value;
                NotifyOfPropertyChange(nameof(TriangleShowResult));
            }
        }

        public ICommand BatchDateCommand
        {
            get { return new RelayCommand(DoBatchParse, CanDoBatch); }
        }

        public CaseCountControl CountControl { get; set; }
        public DefactPercentControl DefactPercentControl { get; set; }

        public string CalanderShowResult
        {
            get { return _calanderShowResult; }
            set
            {
                _calanderShowResult = value;
                NotifyOfPropertyChange(nameof(CalanderShowResult));
            }
        }

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

        public bool IsFemale
        {
            get { return _IsFemale; }
            set
            {
                _IsFemale = value;
                NotifyOfPropertyChange(nameof(IsFemale));
            }
        }

        public bool LateMarry
        {
            get { return _LateMarry; }
            set
            {
                _LateMarry = value;
                NotifyOfPropertyChange(nameof(LateMarry));
            }
        }

        public bool LateBreed
        {
            get { return _LateBreed; }
            set
            {
                _LateBreed = value;
                NotifyOfPropertyChange(nameof(LateBreed));
            }
        }

        public bool Less7
        {
            get { return _Less7; }
            set
            {
                _Less7 = value;
                NotifyOfPropertyChange(nameof(Less7));
            }
        }

        public bool Dystocia
        {
            get { return _Dystocia; }
            set
            {
                _Dystocia = value;
                NotifyOfPropertyChange(nameof(Dystocia));
            }
        }

        public bool Less30
        {
            get { return _Less30; }
            set
            {
                _Less30 = value;
                NotifyOfPropertyChange(nameof(Less30));
            }
        }

        public int BabyBreakDays
        {
            get { return _BabyBreakDays; }
            set
            {
                _BabyBreakDays = value;
                NotifyOfPropertyChange(nameof(BabyBreakDays));
            }
        }

        private void CalculateProductProduction(object e)
        {
            ProductProductionResult = GetProductProductionResult(
                TotalRequired,
                InStockReady,
                InStockProduct,
                IsProductQualified,
                IsRubbish,
                IsPurchaseQualified,
                IsProductDelivered
            );
        }

        private void CalculateBabyBreak(object e)
        {
            BabyBreakDays = GetBabyBreakCount(
                IsFemale,
                LateMarry,
                LateBreed,
                Less7,
                Dystocia,
                Less30
            );
        }

        private void BatchCalculateProduct(object e)
        {
            var filePath = GetInputCurWindowFilePath();
            if (null == filePath) return;
            InitReportVisuals();
            var dataList = FileUtils<ProductionProductCSV>.ReadDateList(filePath);
            if (null != dataList)
            {
                CountControl.SetTotal(dataList.Count);
                int trueCnt = 0, falseCnt = 0;

                foreach (var productCSV in dataList)
                {
                    productCSV.实际结果 = GetProductProductionResult(
                        productCSV.销售合同订单量,
                        productCSV.库存量,
                        productCSV.生产库存量,
                        productCSV.生产质检合格,
                        productCSV.废品,
                        productCSV.采购质检合格,
                        productCSV.货已发完
                    );
                    if (productCSV.实际结果.Equals(productCSV.预期结果))
                        trueCnt++;
                    else
                        falseCnt++;
                }
                CountControl.SetPassed(trueCnt);
                CountControl.SetFailed(falseCnt);

                FileUtils<ProductionProductCSV>.WriteResult(dataList, "rProductProduction.csv");
                DefactPercentControl.SetValues(trueCnt, falseCnt);
            }
        }

        private string GetProductProductionResult(
            int 需求,
            int 可用库存,
            int 生产库存量,
            bool 生产质检合格,
            bool 废品,
            bool 采购质检合格,
            bool 货已发完
        )
        {
            var result = "";
            if (可用库存 >= 需求)
            {
                result = 货已发完 ? "合同结案" : "发货直到合同结案";
            }
            else
            {
                //result += "执行主生产计划 ";

                if (生产库存量 >= Math.Abs(需求 - 可用库存))
                {
                    if (生产质检合格)
                    {
                        if (货已发完)
                            result += "合同结案";
                        else
                            result += "发货直到合同结案";
                    }
                    else
                    {
                        if (废品)
                            result += "重新计划需求 ";
                        else
                            result += "重新生产 ";
                        if (货已发完)
                            result += "合同结案";
                        else
                            result += "发货直到合同结案";
                    }
                }
                else
                {
                    // 需要采购了
                    if (采购质检合格)
                        result += "采购入库 开始生产 ";
                    else
                        result += "重新采购 直至采购质检合格 采购入库 开始生产 ";


                    if (生产质检合格)
                    {
                        if (货已发完)
                            result += "合同结案";
                        else
                            result += "发货直到合同结案";
                    }
                    else
                    {
                        if (废品)
                            result += "重新计划需求 ";
                        else
                            result += "重新生产 ";
                        if (货已发完)
                            result += "合同结案";
                        else
                            result += "发货直到合同结案";
                    }
                }
            }

            return result;
        }

        private void BatchCalculateBabyBreak(object sender)
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

        private int GetBabyBreakCount(
            bool female,
            bool lateMar,
            bool lateBreed,
            bool less7,
            bool dystocia,
            bool less30
        )
        {
            var normal = 90;
            var smallBreed = 0;
            var dystociaDay = 15;
            var late = 30;
            var accompany = 7;
            var total = 0;

            if (female)
            {
                total += normal;
                if (less7)
                    return normal;
                if (lateBreed && lateMar)
                    total += late;
                if (dystocia)
                    total += dystociaDay;
            }
            else
            {
                if (lateBreed && lateMar)
                    total += accompany;
            }
            return total;
        }

        private void InitReportVisuals()
        {
            CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
            DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow,
                "DefactPercentControl");
        }

        private string GetInputCurWindowFilePath()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
                return fileDialog.FileName;

            return null;
        }

        public void ParseTelecom(object sender)
        {
            var res = CalculateTelecomFee(TelecomTotalMin, TelecomTotalTimes);
            TelecomTotal = res[1];
        }

        public void BatchDeterTriFun(object sender)
        {
            var fileDialog = new OpenFileDialog();
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
                        var parseResult = GeneralParser.ParseTriangle(csvMapper.A, csvMapper.B, csvMapper.C,
                            GetParseTriangleStr);
                        csvMapper.Result = parseResult;

                        if (parseResult.Equals("不能构成三角形."))
                            csvMapper.Exception = "不能构成三角形.";

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
                    FileUtils<TriangleMapper>.WriteResult(dataList, "rTriangle.csv");
                    DefactPercentControl.SetValues(trueCnt, falseCnt);
                }
            }
        }

        public void BatchTelecomCalFun(object sender)
        {
            var fileDialog = new OpenFileDialog();
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
                        if (Math.Abs(double.Parse(csvMapper.ExpectedDiscount) - parseResult[0]) < 1e10 - 3 &&
                            Math.Abs(double.Parse(csvMapper.ExpectedFee) - parseResult[1]) < 1e10 - 3)
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

        public void ParseTriangle(object sender)
        {
            double a, b, c;
            a = ALen;
            b = BLen;
            c = CLen;
            TriangleShowResult = GetParseTriangleStr(a, b, c);
        }

        private string GetParseTriangleStr(double a, double b, double c)
        {
            var res = "";
            if (a > 0 && b > 0 && c > 0)
                if (a + b > c && a + c > b && b + c > a)
                    if (a * a + b * b == c * c || a * a + c * c == b * b
                        || b * b + c * c == a * a)
                        if (a == b || a == c || b == c)
                            res = "为等腰直角三角形.";
                        else
                            res = "一般直角三角形.";
                    else if (a == b && b == c && a == c)
                        res = "为等边三角形.";
                    else if (a == b && a != c || a == c && a != b
                             || b == c && a != c)
                        res = "为等腰三角形.";
                    else
                        res = "为一般三角形.";
                else
                    res = "不能构成三角形.";
            else
                res = "不能构成三角形.";
            return res;
        }

        public bool CanDo()
        {
            return true;
        }

        private void CalculateTotal(object sender)
        {
            var res = GetSumStr(OutLet, MainFrame, Monitor);
            if (res[0].Equals("-1"))
                TotalSaleVal = res[1];
            else
                TotalSaleVal = res[0];
        }

        private string[] GetSumStr(int OutLet, int MainFrame, int Monitor)
        {
            var sb = new StringBuilder();
            var result = new string[2];
            if (OutLet < 1)
                sb.Append("外设数量<1  ");
            if (OutLet > 90)
                sb.Append("外设数量>90  ");
            if (MainFrame < 1)
                sb.Append("主机数量<1  ");
            if (MainFrame > 70)
                sb.Append("主机数量>70  ");
            if (Monitor < 1)
                sb.Append("显示器数量<1  ");
            if (Monitor > 80)
                sb.Append("显示器数量>80  ");
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

        private bool CanDoBatch()
        {
            return true;
        }

        private void BatchCalculate(object sender)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                var dataList = FileUtils<ComputerMapper>.ReadDateList(fileDialog.FileName);
                if (null != dataList)
                {
                    int passed, failed;
                    passed = failed = 0;
                    CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow,
                        "CaseCountControl");
                    DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow,
                        "DefactPercentControl");

                    foreach (var csvMapper in dataList)
                    {
                        var strRes = GetSumStr(csvMapper.OutLet, csvMapper.MainFrame, csvMapper.Monitor);
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

                    FileUtils<ComputerMapper>.WriteResult(dataList, "rComputerAccessories.csv");

                    CountControl.SetPassed(passed);
                    CountControl.SetFailed(failed);
                    CountControl.SetTotal(passed + failed);
                    DefactPercentControl.SetValues(passed, failed);
                }
            }
        }

        private void DoBatchParse(object parameter)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "csv file|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                CountControl = UIHelper.FindChild<CaseCountControl>(Application.Current.MainWindow, "CaseCountControl");
                DefactPercentControl = UIHelper.FindChild<DefactPercentControl>(Application.Current.MainWindow,
                    "DefactPercentControl");

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
                        }
                        else
                        {
                            csvMapper.Result = "Parse Error";
                            csvMapper.Exception = parseResult[1] as string;
                        }

                        if (csvMapper.Result.Equals(csvMapper.Expected))
                        {
                            trueCnt++;
                        }
                        else
                        {
                            falseCnt++;
                        }
                    }
                    CountControl.SetPassed(trueCnt);
                    CountControl.SetFailed(falseCnt);
                    //WriteResult(dataList);
                    FileUtils<DateMapper>.WriteResult(dataList, "rNextDate.csv");
                    DefactPercentControl.SetValues(trueCnt, falseCnt);
                }
            }
        }

        private bool CanDoParse()
        {
            return true;
        }

        private double[] CalculateTelecomFee(int callMiniute, int dueTimes)
        {
            var result = new double[2];
            result[1] = 25;
            var preMin = 0.15;

            if (callMiniute < 0 || dueTimes < 0 || callMiniute > 31 * 24 * 60 || dueTimes > 11)
            {
                result[0] = -1;
                result[1] = -1;
                return result;
            }

            if (callMiniute <= 60)
                CalcTelecom(callMiniute, preMin, 1, dueTimes, 0, result);
            else if (callMiniute <= 120)
                CalcTelecom(callMiniute, preMin, 2, dueTimes, 1, result);
            else if (callMiniute <= 180)
                CalcTelecom(callMiniute, preMin, 3, dueTimes, 2, result);
            else if (callMiniute <= 300)
                CalcTelecom(callMiniute, preMin, 3, dueTimes, 3, result);
            else
                CalcTelecom(callMiniute, preMin, 6, dueTimes, 4, result);

            return result;
        }

        private void CalcTelecom(int callMiniute, double perMin, int dueLimit, int dueTime, int discountIndex,
            double[] result)
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

        private void ParseDateManually(object parameter)
        {
            var textBox = parameter as TextBox;
            var resultText = "";
            if (null == textBox)
                return;

            try
            {
                var time = DateTime.Parse(textBox.Text);
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