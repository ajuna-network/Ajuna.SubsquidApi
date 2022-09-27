namespace Ajuna.SubsquidApi.GraphQL.Models;

public  class Extrinsic<T> : ModelBase {
    public Block<T>? Block { get; set; }
    public int? IndexInBlock { get; set; }
    public int? Version { get; set; }
    public string? Signature  { get; set; }
    public bool? Success  { get; set; }
    public string? Error  { get; set; }
    public Call<T> Call { get; set; }
    public int Fee { get; set; }
    public int Tip { get; set; }
    public string? Hash { get; set; }
    public int? Pos { get; set; }
    public List<Call<T>> Calls { get; set; }
}