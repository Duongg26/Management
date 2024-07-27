using QLNV.Models.Entities;

namespace Management.Models.Entities
{
    public class Work
    {
        public int Id { get; set; }
        public int PerId {  get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public DateTime DateWork { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public Work()
        {
            DateWork = DateTime.Now.Date;
        }
    }
}