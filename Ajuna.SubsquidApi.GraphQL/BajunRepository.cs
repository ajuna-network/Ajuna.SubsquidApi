using Ajuna.SubsquidApi.GraphQL.Models;
using GraphQL;

namespace Ajuna.SubsquidApi.GraphQL;

public class BajunRepository : SubsquidRepositoryBase
{
    public BajunRepository(string endPointUrl) : base(endPointUrl)
    {
    }

    public Task<List<SubsquidEvent<T>>> GetEventsRequest<T>(GraphQLRequest request)
    {
        return GetMany<SubsquidEvent<T>>(request);
    }

    public Task<List<SubsquidEvent<BalanceTransfer>>> GetBalanceTransferEvents(int limit = 100)
    {
        var request = GetEventsByName("Balances.Transfer", limit);
        return GetEventsRequest<BalanceTransfer>(request);
    }

    public Task<SubsquidEvent<T>> GetEventById<T>(string id)
    {
        var request = GetEventById(id);

        return Get<SubsquidEvent<T>>(request);
    }

    public Task<Block<T>> GetBlockById<T>(string id)
    {
        var request = GetBlockByIdRequest(id);
        return Get<Block<T>>(request);
    }

    #region Requests

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
            },
            OperationName = "events"
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
            },
            OperationName = "blockById"
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

    #endregion
}