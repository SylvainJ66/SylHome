using SylHome.Gateways.Repositories;
using SylHome.Models;

namespace SylHome.UseCases;

public class LaunchCallForFundsCommandHandler
{
    private readonly ICallForFundsRepository _callForFundsRepository;
    private readonly ICondominiumRepository _condominiumRepository;
    private readonly DeterministicDateTime _date;

    public LaunchCallForFundsCommandHandler(
        ICallForFundsRepository callForFundsRepository, 
        ICondominiumRepository condominiumRepository, 
        DeterministicDateTime date)
    {
        _callForFundsRepository = callForFundsRepository;
        _condominiumRepository = condominiumRepository;
        _date = date;
    }

    public void Handle(Guid callForFundsId, Guid condominiumId)
    {
        Condominium condominium = _condominiumRepository.ById(condominiumId);

        if (_callForFundsRepository.HasCallBeenLauched(condominium.CurrentQuarter(_date)))
        {
            throw new Exception("Can't launch that call for funds because already leaunched !");
        }
        
        _callForFundsRepository.Save(new CallForFunds(
            callForFundsId, 
            condominiumId, 
            condominium.QuarterBudget(), 
            condominium.CurrentQuarter(_date)));
    }
    
}