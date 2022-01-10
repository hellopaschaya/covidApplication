using System;

namespace WebApplication1.Model
{
    public class CoronaModel
    {
        public int? CovidID { get; set; }
        public DateTime? Date { get; set; }
        public int? New_Amount { get; set; }
        public int? Old_GetWell_Amount { get; set; }
        public int? Died_Amount { get; set; }
        public int? SumOld { get; set; }
        public int? SumNew { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Update_User { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Create_User { get; set; }
        public bool IsDelete { get; set; }

    }
}
