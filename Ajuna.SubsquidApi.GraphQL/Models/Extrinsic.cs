namespace Ajuna.SubsquidApi.GraphQL.Models;

public  class Extrinsic : ModelBase {
    public Block? Block { get; set; }
    public int? IndexInBlock { get; set; }
    public int? Version { get; set; }
    public string? Signature  { get; set; }
    public bool? Success  { get; set; }
    public string? Error  { get; set; }
    public Call Call { get; set; }
    public int Fee { get; set; }
    public int Tip { get; set; }
    public string? Hash { get; set; }
    public int? Pos { get; set; }
    public List<Call> Calls { get; set; }
}