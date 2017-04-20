﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SoftwareTestingWPF.Mappers
{
    class ComputerMapper : CsvClassMap<ComputerMapper>
    {
        public ComputerMapper()
        {
            Map(m => m.Id);
            Map(m => m.OutLet);
            Map(m => m.MainFrame);
            Map(m => m.Monitor);
            Map(m => m.Expected);
            Map(m => m.Result);
            Map(m => m.Exception);
            Map(m => m.IsCorrect);
        }
        public string Expected { get; set; }
        public int Id { get; set; }
        public int OutLet { get; set; }
        public int MainFrame { get; set; }
        public int Monitor { get; set; }
        public string Result { get; set; }

        public string Exception { get; set; }

        public string IsCorrect { get; set; }
    }
}
