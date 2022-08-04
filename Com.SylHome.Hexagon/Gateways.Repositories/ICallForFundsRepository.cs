using SylHome.Hexagon.Models;

namespace SylHome.Hexagon.Gateways.Repositories;

public interface ICallForFundsRepository
{
    void Save(CallForFunds callForFunds);
    bool HasCallBeenLauched(Quarter currentQuarter);
    CallForFunds ById(Guid callForFundsId);
}