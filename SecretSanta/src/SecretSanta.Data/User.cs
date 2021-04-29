namespace SecretSanta.Data
{
    public class User
    {
        public int Id { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("Firstnames")]
        public string FirstName { get; set; } = "";
        [System.Text.Json.Serialization.JsonPropertyName("Lastnames")]
        public string LastName { get; set; } = "";
    }
}
