// See https://aka.ms/new-console-template for more information
using Ajuna.SubsquidApi.GraphQL;
using Ajuna.SubsquidApi.GraphQL.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;



var settings = new JsonSerializerSettings
    {ContractResolver = new CamelCasePropertyNamesContractResolver()};

var serializer = new NewtonsoftJsonSerializer(settings);
var graphQLClient = new GraphQLHttpClient("https://bajun.explorer.subsquid.io/graphql", serializer);


var repo = new BajunRepository(graphQLClient);

var s = await repo.GetBalanceTransfers();




var bajunRequest = new GraphQLRequest
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
        id = "0000000000-35a06"
    }
};


var graphQLResponse = await graphQLClient.SendQueryAsync<dynamic>(bajunRequest, CancellationToken.None);
var ss = graphQLResponse.Data as JObject;

var hssh = GetJArrayValue(ss, "blockById");
var json = JsonConvert.DeserializeObject<Block>(hssh, settings);


//
//
// Console.WriteLine(ss.ToString());

//
// var graphQLResponse = await graphQLClient.SendQueryAsync<ResponseType>(bajunRequest);
//
// var personName = graphQLResponse.Data.Person.Name;

//Console.WriteLine(personName);

Console.ReadLine();


static string GetJArrayValue(JObject yourJArray, string key)
{
    foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
        if (key == keyValuePair.Key)
            return keyValuePair.Value.ToString();

    return null;
}
