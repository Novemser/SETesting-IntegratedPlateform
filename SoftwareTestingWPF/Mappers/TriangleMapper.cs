using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SoftwareTestingWPF.Mappers
{
    class TriangleMapper : CsvClassMap<TriangleMapper>
    {
        public TriangleMapper()
        {
            Map(m => m.Id);
            Map(m => m.A);
            Map(m => m.B);
            Map(m => m.C);
            Map(m => m.Expected);
            Map(m => m.Result);
            Map(m => m.Exception);
            Map(m => m.IsCorrect);
        }
        public string Expected { get; set; }
        public int Id { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public string Result { get; set; }

        public string Exception { get; set; }

        public string IsCorrect { get; set; }
    }
}
