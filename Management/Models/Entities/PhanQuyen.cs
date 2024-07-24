namespace QLNV.Models.Entities
{
    public class PhanQuyen
    {
        public int Id { get; set; }
        public Functions ? Functions { get; set; }
        public Account? Account { get; set; }
        public int AccountId { get; set; }
        public int FunctionsId { get; set; }
        public int? IsAdd { get; set; }
        public int? IsDelete { get; set; }
        public int? IsEdit { get; set; }
        public int? IsUpdate { get; set; }
        public bool? Status { get; set; }
    }
}
