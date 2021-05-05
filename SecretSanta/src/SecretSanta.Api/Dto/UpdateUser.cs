namespace SecretSanta.Api.Dto
{
    //DTO
    //Domain Transfer Object
    public class UpdateUser
    {
        public int? Id { get; set;}
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}