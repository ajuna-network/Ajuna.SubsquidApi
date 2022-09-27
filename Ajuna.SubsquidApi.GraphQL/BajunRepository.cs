using Ajuna.SubsquidApi.GraphQL.Models;
using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Ajuna.SubsquidApi.GraphQL;

public class BajunRepository
{
    private readonly GraphQLHttpClient _client;

    public BajunRepository(GraphQLHttpClient client)
    {
        _client = client;
    }

    public async Task<List<BalanceTransfer>> GetBalanceTransfers(int limit = 100)
    {
        var settings = new JsonSerializerSettings
            {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        var request = GetEventsByName("Balances.Transfer", limit);

        var graphQLResponse = await _client.SendQueryAsync<dynamic>(request, CancellationToken.None);
        var data = graphQLResponse.Data as JObject;
        
        var payload = GetJArrayValue(data, "events");
 
        var events =
            JsonConvert.DeserializeObject<List<SubsquidEvent>>(payload , settings);
        

        var balanceTransfers = events.Select(x =>  x.Args.ToObject<BalanceTransfer>()).ToList();

        return balanceTransfers;
    }

    public class Container
    {
        public List<SubsquidEvent> Events { get; set; }
    }
    private GraphQLRequest GetEventsByName(string name, int limit = 100)
    {
        // block {
        //     id
        //         hash}
        
        return new GraphQLRequest
        {
            Query = @"query GetEventsByName($name: String!, $limit: Int!) {
            events(limit: $limit, where: {name_eq: $name }) {            
            id
            name
            args
            }
        }",
            Variables = new
            {
                name = name,
                limit = limit
            }
        };
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