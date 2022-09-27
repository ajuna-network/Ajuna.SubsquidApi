namespace Ajuna.SubsquidApi.GraphQL.Models;

public class Call<T> : ModelBase {
    
    public Call<T> Parent { get; set; }
    public Block<T>? Block { get; set; }
    public Extrinsic<T>? Extrinsic { get; set; }
    public bool? Success  { get; set; }
    public string? Error  { get; set; }
    public string? Origin  { get; set; }
    public string? Name  { get; set; }
    public T Args  { get; set; }
    public int? Pos { get; set; }
}