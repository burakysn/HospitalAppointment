using Infrastructure.Model;

namespace WS.Model.Entities
{
    public class Patient : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Int64 TcNo { get; set; }
        public Int64 PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
