using Infrastructure.Model;

namespace WS.Model.Dtos.User
{
    public class PatientPutDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Int64 TcNo { get; set; }
        public Int64 PhoneNumber { get; set; }
    }
}
