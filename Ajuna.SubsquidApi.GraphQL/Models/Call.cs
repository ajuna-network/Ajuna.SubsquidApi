namespace Ajuna.SubsquidApi.GraphQL.Models;

public class Call : ModelBase {
    
    public Call Parent { get; set; }
    public Block? Block { get; set; }
    public Extrinsic? Extrinsic { get; set; }
    public bool? Success  { get; set; }
    public string? Error  { get; set; }
    public string? Origin  { get; set; }
    public string? Name  { get; set; }
    public string?  Args  { get; set; }
    public int? Pos { get; set; }
}