namespace Character.API.OpenApi.Requests
{
    public static class DefaultRequestProvider
    {
        public static OpenApiRequestBody CreateCharacterRequest()
        {
            return new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    [MediaTypeNames.Application.Json] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.Schema,
                                Id = nameof(CreateCharacterRequest)
                            }
                        },
                        Example = new OpenApiString(JsonSerializer.Serialize(new CreateCharacterRequest(
                            UserId: Guid.NewGuid(),
                            SchemaId: Guid.NewGuid(),
                            Name: "Glimbo the Goblin",
                            Description: "He is just a wibble gobwin that doesn't drop good items. Pleas don't swing your sword at him, he will die in just one attack",
                            Statistics:
                            [
                                new Statistic
                                {
                                    Name = "Hit points",
                                    Value = "3"
                                },
                                new Statistic
                                {
                                    Name = "Size",
                                    Value = "Wibble"
                                }
                            ])))
                    }
                }
            };
        }
    }
}
