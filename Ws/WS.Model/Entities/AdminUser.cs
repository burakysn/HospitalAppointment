using Infrastructure.Model;

namespace WS.Model.Entities
{
    public class AdminUser : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
