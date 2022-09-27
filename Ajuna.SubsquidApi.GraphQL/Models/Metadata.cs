namespace Ajuna.SubsquidApi.GraphQL.Models;

public class Metadata: ModelBase
{
    public string? SpecName { get; set; }
    public int SpecVersion { get; set; }
    public int? BlockHeight { get; set; }
    public string? BlockHash { get; set; }
    public string? Hex { get; set; }
}