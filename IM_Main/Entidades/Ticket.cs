using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM_Main.Entidades
{
    public class Ticket
    {
        public Int64 Id{get; set;}
        public string TicketIdPrefix { get; set; }
        public int TicketId{get; set;}
        public int BatchId{get; set;}
        public int OutletId{get; set;}
        public int FruitCount{get; set;}
        public int PackWeightDecigrams{get; set;}
        public DateTime PackCompleteTime{get; set;}
        public DateTime PrintTime{get; set;}
        public int Status{get; set;}
        public int ModifiedByUser{get; set;}
        public int CreatedByUser{get; set;}
        public int IsTransferred{get; set;}
        public int IsMerged{get; set;}
        public int MergedTicketId{get; set;}
        public string PrinterName{get; set;}
        public Int64 Tag{get; set;}
        public string PrintedId{get; set;}
    }
}
