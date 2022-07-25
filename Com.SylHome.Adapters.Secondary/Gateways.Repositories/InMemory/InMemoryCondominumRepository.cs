using SylHome.Gateways.Repositories;
using SylHome.Models;

namespace Com.SylHome.Adapters.Secondary.Gateways.Repositories.InMemory;

public class InMemoryCondominiumRepository : ICondominiumRepository
{
    private readonly List<Condominium> _condominiums = new();
    
    public Condominium ById(Guid condoId) => 
        _condominiums.First(c => c.Id == condoId);

    public void FeedWith(List<Condominium> condominiums) => _condominiums.AddRange(condominiums);
}