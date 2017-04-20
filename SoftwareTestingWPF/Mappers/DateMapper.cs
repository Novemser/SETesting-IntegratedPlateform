using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SoftwareTestingWPF.Mappers
{
    class DateMapper : CsvClassMap<DateMapper>
    {
        public DateMapper()
        {
            Map(m => m.Id);
            Map(m => m.DateStr);
            Map(m => m.Expected);
            Map(m => m.Result);
            Map(m => m.Exception);
        }
        public string Expected { get; set; }
        public string DateStr { get; set; }
        public int Id { get; set; }

        public string Result { get; set; }

        public string Exception { get; set; }
    }
}
