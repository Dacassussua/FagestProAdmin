using System.ComponentModel.DataAnnotations;

namespace FagestProAdmin.Models.Customer
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage ="Digite o Id da Máquina")]
        public string customerId { get; set; }
        [Required(ErrorMessage ="Digite o nome da empresa")]
        public string customerName { get; set; }
        [Required(ErrorMessage = "Digite o NIF da empresa")]
        public string customerNIF { get; set; }
        [Required(ErrorMessage = "Digite o Nome do responsável empresa")]
        public string customerResponsavel { get; set; }
        public string customerContact { get; set; }
        public string customerEmail { get; set; }
        public string status { get; set; }
        public int counter { get; set; }
    }
}
