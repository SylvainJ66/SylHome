using Com.SylHome.Adapters.Secondary.Gateways.Repositories.InMemory;
using SylHome.Models;
using SylHome.UseCases;

namespace SylHome.Unit.Hexagon.UseCases;

public class LauchCallForFundsCommandHandlerShould
{
    private readonly InMemoryCallForFundsRepository _callForFundsRepository = new();
    private readonly InMemoryCondominiumRepository _condominiumRepository = new();
    private readonly DeterministicDateTime _date = new();
    private readonly LaunchCallForFundsCommandHandler _launchCallForFundsCommandHandler;

    private readonly Guid _nexCallForFundId = Guid.Parse("510a9d8a-a6cb-453b-8652-2ce74a56b42c");
    private readonly Guid _condominiumId = Guid.Parse("13425653-df51-49a5-a95b-dcf1b921b452");

    public LauchCallForFundsCommandHandlerShould()
    {
        _launchCallForFundsCommandHandler = new( _callForFundsRepository, _condominiumRepository, _date);
    }

    #region Condo_amount_changing

    [Fact]
    public void Launched_call_for_funds_for_a_given_amount()
    {
        SetDateNow(new DateTime(2022, 1, 1));
        InitCondominium(10000);
        LaunchCallForFunds(_nexCallForFundId);
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            _nexCallForFundId, 2500, Quarter.First)});
    }
    
    [Fact]
    public void Launched_call_for_funds_for_a_another_amount()
    {
        SetDateNow(new DateTime(2022, 1, 1));
        InitCondominium(12000);
        LaunchCallForFunds(_nexCallForFundId);
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            _nexCallForFundId, 3000, Quarter.First)});
    }

    #endregion

    #region Quarter_changing

    [Fact]
    public void Launched_call_for_funds_for_a_given_quarter()
    {
        SetDateNow(new DateTime(2022, 1, 1));
        InitCondominium(10000);
        LaunchCallForFunds(_nexCallForFundId);
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            _nexCallForFundId, 2500, Quarter.First)});
    }

    [Fact]
    public void Launched_call_for_funds_for_another_quarter()
    {
        SetDateNow(new DateTime(2022, 4, 1));
        InitCondominium(10000);
        LaunchCallForFunds(_nexCallForFundId);
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            _nexCallForFundId, 2500, Quarter.Second)});
    }
    
    [Fact]
    public void Launched_call_for_funds_for_third_quarter()
    {
        SetDateNow(new DateTime(2022, 7, 1));
        InitCondominium(10000);
        LaunchCallForFunds(_nexCallForFundId);
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            _nexCallForFundId, 2500, Quarter.Third)});
    }
    #endregion

    [Fact]
    public void Prevent_lauching_a_given_call_twice()
    {
        SetDateNow(new DateTime(2022, 1, 1));
        InitCondominium(10400);
        _callForFundsRepository.FeedWith(new List<CallForFunds>
        {
            new CallForFunds(
                Guid.Parse("d7c68699-24e2-43a5-bb3d-be522628a1ec"),
                _condominiumId,
                2300,
                Quarter.First)
        });
        Assert.Throws<Exception>(() => LaunchCallForFunds(_nexCallForFundId));
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            Guid.Parse("d7c68699-24e2-43a5-bb3d-be522628a1ec"), 2300, Quarter.First)});
    }

    [Fact]
    public void Launch_the_next_call_for_funds_after_a_previous_one()
    {
        SetDateNow(new DateTime(2022, 1, 1));
        InitCondominium(10400);
        _callForFundsRepository.FeedWith(new List<CallForFunds>
        {
            new CallForFunds(
                Guid.Parse("d7c68699-24e2-43a5-bb3d-be522628a1ec"),
                _condominiumId,
                2300,
                Quarter.First)
        });
        SetDateNow(new DateTime(2022, 4, 1));
        LaunchCallForFunds(_nexCallForFundId);
        AsserCallsForFunds(new List<CallForFundsCriterion>{ new CallForFundsCriterion(
            Guid.Parse("d7c68699-24e2-43a5-bb3d-be522628a1ec"), 2300, Quarter.First),
            new CallForFundsCriterion(
                Guid.Parse("510a9d8a-a6cb-453b-8652-2ce74a56b42c"), 2600, Quarter.Second)
        });
    }
    
    private void InitCondominium(decimal yearlyAmount) 
    {
        _condominiumRepository.FeedWith(new List<Condominium>{ new(
            _condominiumId,
            yearlyAmount
        )});
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

        Assert.Equal(
            _callForFundsRepository.GetCallsForFunds(), 
            callsForFunds);
    }

    private void SetDateNow(DateTime date) => _date.SetDateOfNow(date);
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

