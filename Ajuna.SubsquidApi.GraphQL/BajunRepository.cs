using Ajuna.SubsquidApi.GraphQL.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ajuna.SubsquidApi.GraphQL;

public class BajunRepository
{
    private readonly GraphQLHttpClient _client;
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public BajunRepository(string endPointUrl)
    {
        _jsonSerializerSettings = new JsonSerializerSettings
            {ContractResolver = new CamelCasePropertyNamesContractResolver()};
        _client = new GraphQLHttpClient(endPointUrl,new NewtonsoftJsonSerializer(_jsonSerializerSettings));
    }
    
    public Task<List<SubsquidEvent<T>>> GetEventsRequest<T>(GraphQLRequest request)
    {
        return GetMany<SubsquidEvent<T>>(request);
    }

    public  Task<List<SubsquidEvent<T>>> GetBalanceTransferEvents<T>(int limit = 100)
    {
        var request = GetEventsByName("Balances.Transfer", limit);
        return GetEventsRequest<T>(request);
    }
    
    public  Task<SubsquidEvent<T>> GetEventById<T>(string id)
    {
        var request = GetEventById(id);

        return Get<SubsquidEvent<T>>(request);
    }

    public Task<Block<T>> GetBlockById<T>(string id)
    {
        var request = GetBlockByIdRequest(id);
        return Get<Block<T>>(request);
    }
    
    public async Task<TSquidType> Get<TSquidType>(GraphQLRequest request) 
        where  TSquidType : ModelBase 
    {
        var graphQLResponse = await _client.SendQueryAsync<dynamic>(request, CancellationToken.None);
        var data = graphQLResponse.Data as JObject;
        
        var payload = GetJArrayValue(data, request.OperationName);
        var @event =
            JsonConvert.DeserializeObject<TSquidType>(payload , _jsonSerializerSettings);
        
        return @event;
    }
    
    public async Task<List<TSquidType>> GetMany<TSquidType>(GraphQLRequest request)
        where  TSquidType : ModelBase 
    {
        var graphQlResponse = await _client.SendQueryAsync<dynamic>(request, CancellationToken.None);
        var data = graphQlResponse.Data as JObject;
        
        var payload = GetJArrayValue(data, request.OperationName);
 
        var events =
            JsonConvert.DeserializeObject<List<TSquidType>>(payload , _jsonSerializerSettings);
        
        return events;
    }
    
    

    private GraphQLRequest GetEventsByName(string name, int limit = 100)
    {
        return new GraphQLRequest
        {
            Query = @"query GetEventsByName($name: String!, $limit: Int!) {
            events(limit: $limit, where: {name_eq: $name }) {            
            id
            name
            args
           block {
                id
                }
                }
                }",
            Variables = new
            {
                name = name,
                limit = limit
            }, OperationName = "events"
        };
    }

    
    
    private GraphQLRequest GetBlockById(string id)
    {
        
         return new GraphQLRequest
        {
            Query = @"query GetBlockById($id: String!)  {
                   blockById(id: $id) {
                      id
                      height
                      hash
                      extrinsicsRoot
                    }
                }",
            // OperationName = "PersonAndFilms",
            Variables = new
            {
                id = id
            },OperationName = "blockById"
        };
        
    }
    
    private GraphQLRequest GetEventById(string id)
    {
        return new GraphQLRequest
        {
            Query = @"query GetEventById($id: String!) {
              eventById(id: $id) {
                        id
                        name
                        args
                    }
                }",
           Variables = new
            {
                id = id,
            },
           OperationName = "eventById"
        };
    }

    private GraphQLRequest GetBlockByIdRequest(string id)
    {
        var blockByIdRequest = new GraphQLRequest()
        {
            Query = @"query MyQuery($id: String!) {
              blockById(id: $id) {
                id
              events {
                  name
                  args
                }
              }
            }", 
            OperationName = "blockById",
            Variables = new
            {
                id = id
            }
        };
        return blockByIdRequest;
    }



    private string GetJArrayValue(JObject yourJArray, string key)
    {
        foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
            if (key == keyValuePair.Key)
                return keyValuePair.Value.ToString();

        return null;
    }
}

public class BalanceTransfer
{
    public string Amount { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}