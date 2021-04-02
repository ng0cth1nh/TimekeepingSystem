using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityFramework
{
    public class TagInfor
    {
        public int ID { get; set; }
        public String Size { get; set; }
        public String Color { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public int StepID { get; set; }
        public String Step { get; set; }
    }
}
