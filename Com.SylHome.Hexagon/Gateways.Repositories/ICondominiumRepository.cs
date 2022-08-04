using SylHome.Hexagon.Models;

namespace SylHome.Hexagon.Gateways.Repositories;

public interface ICondominiumRepository
{
    public Condominium ById(Guid condoId);
}