using QLNV.Models.Entities;

namespace Management.Models.ViewModel
{
    public class WorkViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }//id người giaopublic string DsNguoiLam {  get; set; }
        public string TenCV { get; set; }
        public string NoiDung { get; set; }
        public DateOnly NgayXong { get; set; }
        public string DsTep { get; set; }
        public int Status { get; set; }
        public string DSNguoiLam { get; set; }

    }
}
