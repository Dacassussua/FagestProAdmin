using System;
using System.ComponentModel.DataAnnotations;

namespace FagestProAdmin.Models.Licences
{
    public class KeyManagerViewModel
    {
        public string customerId { get; set; }
        public string ativationKey { get; set; }

        public string obs { get; set; }
        public string status { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createDate { get; set; }
        private int _time { get ; set; }
        
        public int time
        {
            get {
                DataExtraResult(endDate, out int timeResult);
                return timeResult;
            
            }
            set { _time = value; }
        }


        public string statusResult
        {
            get
            {
                GetStatus(status, out string statusResult);
                return statusResult;
            }
        }

        void GetStatus(string status, out string statusResult)
        {
            statusResult = "Espirado";
            if (status.Equals("V"))
                statusResult = "Activo";
            if (status.Contains("C"))
                statusResult = "Cancelado";

        }
        void DataExtraResult(DateTime endDate, out int time)
        {
            time = (endDate.Date - DateTime.Now.Date).Days;
        }
    }


}
