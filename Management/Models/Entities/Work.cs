using QLNV.Models.Entities;

namespace Management.Models.Entities
{
    public class Work
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }   //Id người giao
        public DateOnly NgayGiao { get; set; }
        public DateOnly NgayXong { get; set; }
        public string NoiDung { get; set; }
        public int Status { get; set; }
        public string TepDinhKemPath { get; set; }

        public Work()
        {
            NgayGiao = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}