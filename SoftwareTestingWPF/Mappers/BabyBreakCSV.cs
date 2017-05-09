using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SoftwareTestingWPF.Mappers
{
    class BabyBreakCSV
    {
        public bool is_woman { get; set; }
        public bool late_marriage { get; set; }
        public bool late_birth { get; set; }
        public bool less_7_month { get; set; }
        public bool dystocia { get; set; }
        public bool less_30_day { get; set; }
        public int result_day { get; set; }

        public string IsCorrect { get; set; }

        public string Exception { get; set; }

        public string Result { get; set; }

        public int Id { get; set; }
    }
}
