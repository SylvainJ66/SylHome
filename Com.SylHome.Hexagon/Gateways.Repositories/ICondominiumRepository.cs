using SylHome.Models;

namespace SylHome.Gateways.Repositories;

public interface ICondominiumRepository
{
    public Condominium ById(Guid condoId);
}