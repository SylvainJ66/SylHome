using SylHome.Models;

namespace SylHome.Gateways.Repositories;

public interface ICallForFundsRepository
{
    void Save(CallForFunds callForFunds);
    bool HasCallBeenLauched(Quarter currentQuarter);
}