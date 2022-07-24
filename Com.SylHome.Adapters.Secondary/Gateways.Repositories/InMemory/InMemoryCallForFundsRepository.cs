using SylHome.Gateways.Repositories;
using SylHome.Models;

namespace Com.SylHome.Adapters.Secondary.Gateways.Repositories.InMemory;

public class InMemoryCallForFundsRepository : ICallForFundsRepository
{
    private readonly List<CallForFunds> _callsForFunds = new();

    public List<CallForFunds> GetCallsForFunds() => _callsForFunds;

    public void Save(CallForFunds callForFunds) => _callsForFunds.Add(callForFunds);

    public bool HasCallBeenLauched(Quarter currentQuarter) => 
        _callsForFunds.Any(c => c.IsAbout(currentQuarter));
}