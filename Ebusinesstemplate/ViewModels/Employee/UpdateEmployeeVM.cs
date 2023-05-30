using System.ComponentModel.DataAnnotations.Schema;

namespace Ebusinesstemplate.ViewModels.Employee
{
    public class UpdateEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionId { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
