using Com.SylHome.Adapters.Secondary.Gateways.Repositories.InMemory;
using SylHome.Models;
using SylHome.UseCases;

namespace SylHome.Unit.Hexagon.UseCases;

public class LauchCallForFundsCommandHandlerShould
{
    private readonly InMemoryCallForFundsRepository _callForFundsRepository = new();
    private readonly InMemoryCondominiumRepository _condominiumRepository = new();
    private readonly LaunchCallForFundsCommandHandler _launchCallForFundsCommandHandler = new();

    private Guid _nexCallForFundId = Guid.Parse("510a9d8a-a6cb-453b-8652-2ce74a56b42c");
    private Guid _condominiumId = Guid.Parse("13425653-df51-49a5-a95b-dcf1b921b452");
    
    [Fact]
    public void Launched_call_for_funds_for_a_given_amount()
    {
        
    }

    private void LaunchCallForFunds(Guid nextCallForFundId)
    {
        _launchCallForFundsCommandHandler.Handle(nextCallForFundId, _condominiumId);
    }

    private void AsserCallsForFunds(List<CallForFundsCriterion> criteria)
    {
        var callsForFunds = new List<CallForFunds>();

        foreach (var criterion in criteria)
        {
            callsForFunds.Add(new CallForFunds(
                criterion.Id,
                _condominiumId,
                (decimal)criterion.Amount,
                criterion.Quarter));
        }
        
        Assert.Equal(Enumerable.SequenceEqual(_callForFundsRepository.GetCallsForFunds().OrderBy(x => x), 
            callsForFunds.OrderBy(y => y)));
    }
}

public class CallForFundsCriterion
{
    public Guid Id { get; }
    public int Amount { get; }
    public Quarter Quarter { get; }

    public CallForFundsCriterion(Guid id, int amount, Quarter quarter)
    {
        Id = id;
        Amount = amount;
        Quarter = quarter;
    }
}

