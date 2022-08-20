using Com.SylHome.Adapters.Secondary.CallForFunds;
using SylHome.Hexagon.Gateways.Repositories;
using SylHome.Hexagon.Models;

namespace SylHome.Integration;

public class IntegrationTestConfiguration
{
    private readonly DapperCallForFundsRepository _repo;
    private readonly Guid callForFundsId = Guid.Parse("d3c68699-24e2-43a5-bb3d-be522628a9ec");
    private readonly Guid CondoId = Guid.Parse("d6c68699-24e2-43a5-bb3d-be522628a9ec");

    public IntegrationTestConfiguration()
    {
        _repo = new DapperCallForFundsRepository();
    }

    [Fact]
    public void Can_save_a_callforfunds()
    {
        var callForFunds = new CallForFunds(callForFundsId, CondoId, 2500, Quarter.First);

        _repo.Save(callForFunds);
        
        var expected = _repo.ById(callForFundsId);

        Assert.Equal(expected, callForFunds);
    }
}