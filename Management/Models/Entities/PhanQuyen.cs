namespace QLNV.Models.Entities
{
    public class PhanQuyen
    {
        public int Id { get; set; }
       
     
        public int AccountId { get; set; }
        public int ? IdCn { get; set; }
        public int? IsAdd { get; set; }
        public int? IsDelete { get; set; }
        public int? IsEdit { get; set; }
        public int? IsRead { get; set; }
        public bool? Status { get; set; }    }
}
