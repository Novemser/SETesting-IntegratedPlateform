using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTestingWPF.Mappers
{
    class ProductionProductCSV
    {
        public int Id { get; set; }
        public string 场景 { get; set; }
        public int 销售合同订单量 { get; set; }
        public bool 库存满足销售需求 { get; set; }
        public int 库存量 { get; set; }
        public int 生产库存量 { get; set; }
        public bool 库存满足生产需求 { get; set; }
        public bool 生产质检合格 { get; set; }
        public bool 废品 { get; set; }
        public bool 采购质检合格 { get; set; }
        public bool 货已发完 { get; set; }
        public string 预期结果 { get; set; }
        public string 实际结果 { get; set; }

    }
}
