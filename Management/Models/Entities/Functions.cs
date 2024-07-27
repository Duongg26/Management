namespace QLNV.Models.Entities
{
    public class Functions
    {
        public int Id { get; set; }
        public int ? IdCn { get; set; }
        public int ? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Status { get; set; }
        public Account? Account { get; set; }
    }
}
