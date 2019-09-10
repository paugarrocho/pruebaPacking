using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_Main.Entidades
{
    public class TicketDatos
    {
        public Int64 Id { get; set; }
        public int BatchId { get; set; }
        public int PackWeightDecigrams { get; set; }
        public DateTime PrintTimN { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
