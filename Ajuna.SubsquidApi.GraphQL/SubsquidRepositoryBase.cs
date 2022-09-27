using Ajuna.SubsquidApi.GraphQL.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ajuna.SubsquidApi.GraphQL;

public abstract class SubsquidRepositoryBase
{
    protected readonly GraphQLHttpClient Client;

    protected readonly JsonSerializerSettings JsonSerializerSettings;

    public SubsquidRepositoryBase(string endPointUrl)
    {
        JsonSerializerSettings = new JsonSerializerSettings
            {ContractResolver = new CamelCasePropertyNamesContractResolver()};
        Client = new GraphQLHttpClient(endPointUrl,new NewtonsoftJsonSerializer(JsonSerializerSettings));
    }
    
    /// <summary>
    /// Generic Method for getting entities of the basic Subsquid types
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="TSquidType"></typeparam>
    /// <returns></returns>
    public async Task<TSquidType> Get<TSquidType>(GraphQLRequest request) 
        where  TSquidType : ModelBase 
    {
        var graphQLResponse = await Client.SendQueryAsync<dynamic>(request, CancellationToken.None);
        var data = graphQLResponse.Data as JObject;
        
        var payload = GetJArrayValue(data, request.OperationName);
        var @event =
            JsonConvert.DeserializeObject<TSquidType>(payload , JsonSerializerSettings);
        
        return @event;
    }
    
    /// <summary>
    /// Generic Method for getting entities of the basic Subsquid types
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="TSquidType"></typeparam>
    /// <returns></returns>
    public async Task<List<TSquidType>> GetMany<TSquidType>(GraphQLRequest request)
        where  TSquidType : ModelBase 
    {
        var graphQlResponse = await Client.SendQueryAsync<dynamic>(request, CancellationToken.None);
        var data = graphQlResponse.Data as JObject;
        
        var payload = GetJArrayValue(data, request.OperationName);
 
        var events =
            JsonConvert.DeserializeObject<List<TSquidType>>(payload , JsonSerializerSettings);
        
        return events;
    }
    
    private string GetJArrayValue(JObject yourJArray, string key)
    {
        foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
            if (key == keyValuePair.Key)
                return keyValuePair.Value.ToString();

        return null;
    }

}