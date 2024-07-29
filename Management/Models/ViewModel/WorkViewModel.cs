using QLNV.Models.Entities;

namespace Management.Models.ViewModel
{
    public class WorkViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }//id người giao
        public string NoiDung { get; set; }
        public DateOnly NgayXong { get; set; }
        public string TepDinhKemPath { get; set; }
        public int Status { get; set; }
    }
}
