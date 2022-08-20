using SylHome.Hexagon.Gateways.Repositories;
using SylHome.Hexagon.Models;

namespace Com.SylHome.Adapters.Secondary.InMemory.InMemory;

public class InMemoryCallForFundsRepository : ICallForFundsRepository
{
    
    private readonly List<CallForFunds> _callsForFunds = new();

    public List<CallForFunds> GetCallsForFunds() => _callsForFunds;

    public void Save(CallForFunds callForFunds) => _callsForFunds.Add(callForFunds);

    public bool HasCallBeenLauched(Quarter currentQuarter) => 
        _callsForFunds.Any(c => c.IsAbout(currentQuarter));

    public CallForFunds ById(Guid callForFundsId)
    {
        throw new NotImplementedException();
    }

    public void FeedWith(List<CallForFunds> callForFunds)
    {
        _callsForFunds.AddRange(callForFunds);
    }
    
}