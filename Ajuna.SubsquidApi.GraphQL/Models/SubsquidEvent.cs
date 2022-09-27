using Newtonsoft.Json.Linq;

namespace Ajuna.SubsquidApi.GraphQL.Models;

public class SubsquidEvent<T> : ModelBase
{
    public Block<T>? Block { get; set; }
    public int? IndexInBlock { get; set; }
    public string? Phase { get; set; }
    public string? Name { get; set; }
    public Extrinsic<T> Extrinsic { get; set; }
    public Call<T> Call { get; set; }
    public T Args { get; set; }
    public int? Pos { get; set; }
}