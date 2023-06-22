using System;

namespace FagestProAdmin.Models.Licences
{
    public class NewKeyManagerViewModel
    {
        public int time { get; set; }
        public int companyCode { get; set; } = 010;
        public string customerId { get; set; }
        public DateTime endDate { get; set; }
        public string obs { get; set; }
    }
}
