namespace Ajuna.SubsquidApi.GraphQL;

/// <summary>
/// Args Type for Balances.Transfer Event
/// </summary>
public class BalanceTransfer
{
    public string Amount { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}