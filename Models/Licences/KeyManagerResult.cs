using System;

namespace FagestProAdmin.Models.Licences
{
    public class KeyManagerResult
    {
        public string customerId { get; set; }
        public string status { get; set; }
        public string ativationKey { get; set; }
        public string obs { get; set; }
        public string customerName { get; set; }
        public DateTime createDate { get; set; }
        public DateTime endDate { get; set; }


        private int _time { get; set; }
        private bool SilentMode { get; set; } = false;
        public int time
        {
            get
            {
                DataExtraResult(endDate, out int timeResult);
                return timeResult;

            }
            set { _time = value; }
        }

        void DataExtraResult(DateTime endDate, out int time)
        {
            time = (endDate.Date - DateTime.Now.Date).Days;
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
    }
}
