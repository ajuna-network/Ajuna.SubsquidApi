// See https://aka.ms/new-console-template for more information

using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

Console.WriteLine("Hello, World!");


var graphQLClient = new GraphQLHttpClient("https://bajun.explorer.subsquid.io/graphql", new NewtonsoftJsonSerializer());

var bajunRequest = new GraphQLRequest {
    Query =@"query GetBlockById($id: String!)  {
   blockById(id: $id) {
      id
      height
      hash
      extrinsicsRoot
    }
}",
    // OperationName = "PersonAndFilms",
    Variables = new {
        id = "0000000000-35a06"
    }
};


var graphQLResponse = await graphQLClient.SendQueryAsync<dynamic>(bajunRequest, CancellationToken.None);
 var ss = graphQLResponse.Data as JObject;

var hssh = GetJArrayValue(ss, "@hash");


Console.WriteLine();

//
// var graphQLResponse = await graphQLClient.SendQueryAsync<ResponseType>(bajunRequest);
//
// var personName = graphQLResponse.Data.Person.Name;

//Console.WriteLine(personName);

Console.ReadLine();


static string GetJArrayValue(JObject yourJArray, string key)
{
    foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
    {
        if (key == keyValuePair.Key)
        {
            return keyValuePair.Value.ToString();
        }
    }

    return null;
}





public class ResponseType 
{
    public PersonType Person { get; set; }
}

public class PersonType 
{
    public string Name { get; set; }
    public FilmConnectionType FilmConnection { get; set; }    
}

public class FilmConnectionType {
    public List<FilmContentType> Films { get; set; }    
}

public class FilmContentType {
    public string Title { get; set; }
}

