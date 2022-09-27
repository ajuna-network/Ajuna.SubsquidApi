namespace Ajuna.SubsquidApi.GraphQL.Models;

public class Block : ModelBase
{
    public int? Height { get; set; }
    public string? Hash { get; set; }
    public string? ParentHash { get; set; }
    public long? TimeStamp { get; set; }    
    public Metadata? Spec { get; set; }
    public string? Validator { get; set; }

    public List<SubsquidEvent> Events { get; set; }
    public List<Call> Calls { get; set; }
    public List<Extrinsic> Extrinsics { get; set; }

}