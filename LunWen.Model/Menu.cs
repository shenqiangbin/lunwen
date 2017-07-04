using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; }
        public string MenuUrl { get; set; }
        public string MenuName { get; set; }
        public int Sort { get; set; }
        public int Status { get; set; }
    }
}
