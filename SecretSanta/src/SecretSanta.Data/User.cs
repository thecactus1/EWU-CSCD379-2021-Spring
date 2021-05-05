namespace SecretSanta.Data
{
    public class User
    {
        public int Id { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("Firstname")]
        public string FirstName { get; set; } = "";
        [System.Text.Json.Serialization.JsonPropertyName("Lastname")]
        public string LastName { get; set; } = "";
    }
}
