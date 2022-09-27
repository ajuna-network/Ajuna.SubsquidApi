using Ajuna.SubsquidApi.GraphQL;
using GraphQL;

// Instantiate Repository
var repo = new BajunRepository("https://bajun.explorer.subsquid.io/graphql");

// Get Balance Transfer Events
 var s = await repo.GetBalanceTransferEvents();


// Get Event by Id 
// var sss = await repo.GetEventById<BalanceTransfer>("0000312575-000005-f6531");

//Console.ReadLine();

// Get Block by Id 
var block = await repo.GetBlockById<dynamic>("0000312575-f6531");

// Get only Balance Transfer Events

var balanceTransferEvents = block.Events.Where(x => x.Name == "Balances.Transfer").ToList();


var blockRequest = new GraphQLRequest()
{
  Query = @"query MyQuery {
  blocks(limit: 10) {
    hash
    height
    id
    timestamp
    events {
          args
        }
  }
}",
  OperationName = "blocks"
};

// Get top ten blocks
//var blocks = await repo.GetMany<Block<dynamic>>(blockRequest);



//
// var callsRequest = new GraphQLRequest()
//     {
//         Query = @"query MyQuery {
//           calls(limit: 10, orderBy: block_height_DESC, where: {success_eq: true}) {
//             args
//             success
//             name
//             origin        
//           }
//         }",
//         OperationName = "calls"
//     };
//
// var calls = await repo.GetMany<Call<JObject>>(callsRequest);

Console.ReadLine();


// Block that includes Balance Transfer
//"0000312575-f6531"