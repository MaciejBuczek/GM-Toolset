namespace Character.API.Entities
{
    public record Character
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SchemaId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required IEnumerable<Statistic> Statistics { get; set; }
    }
}