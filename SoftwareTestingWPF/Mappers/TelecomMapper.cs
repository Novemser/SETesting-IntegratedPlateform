using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SoftwareTestingWPF.Mappers
{
    class TelecomMapper : CsvClassMap<TelecomMapper>
    {

        public TelecomMapper()
        {
            Map(m => m.minutes);
            Map(m => m.times);
            Map(m => m.ExpectedDiscount);
            Map(m => m.ExpectedFee);
            Map(m => m.ResultDiscount);
            Map(m => m.ResultFee);
            Map(m => m.Exception);
            Map(m => m.IsCorrect);
            Map(m => m.Id);
        }

        public string IsCorrect { get; set; }

        public string Exception { get; set; }

        public string ResultFee { get; set; }
        public string ResultDiscount { get; set; }

        public string ExpectedFee { get; set; }

        public string ExpectedDiscount { get; set; }

        public int minutes { get; set; }

        public int times { get; set; }

        public int Id { get; set; }
    }
}
