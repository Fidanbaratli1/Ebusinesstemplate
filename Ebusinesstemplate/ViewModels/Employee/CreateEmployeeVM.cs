using System.ComponentModel.DataAnnotations.Schema;

namespace Ebusinesstemplate.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
