using QLNV.Models.Entities;

namespace Management.Models.ViewModel
{
    public class WorkViewModel
    {
        public int Id { get; set; }
        public int PerId { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
