using Newtonsoft.Json.Linq;

namespace Ajuna.SubsquidApi.GraphQL.Models;

public class SubsquidEvent : ModelBase
{
    public Block? Block { get; set; }
    public int? IndexInBlock { get; set; }
    public string? Phase { get; set; }
    public string? Name { get; set; }
    public Extrinsic Extrinsic { get; set; }
    public Call Call { get; set; }
    public JObject Args { get; set; }
    public int? Pos { get; set; }
}