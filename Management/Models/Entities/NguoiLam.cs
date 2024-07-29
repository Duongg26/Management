namespace Management.Models.Entities
{
    public class NguoiLam
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public Work Work { get; set; }
        public int IdNguoiLam { get; set; }
        public int Status { get; set; }
    }
}
