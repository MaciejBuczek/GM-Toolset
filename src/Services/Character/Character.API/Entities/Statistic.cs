namespace Character.API.Entities
{
    public record Statistic
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}