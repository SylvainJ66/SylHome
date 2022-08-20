using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using SylHome.Hexagon.Gateways.Repositories;
using SylHome.Hexagon.Models;

namespace Com.SylHome.Adapters.Secondary.CallForFunds;

public class DapperCallForFundsRepository : ICallForFundsRepository
{
    private readonly IConfiguration _config;
    private string _connectionId;

    public DapperCallForFundsRepository(IConfiguration config, string connectionId)
    {
        _config = config;
        _connectionId = connectionId;
    }

    public void Save(global::SylHome.Hexagon.Models.CallForFunds callForFunds)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_connectionId));
        connection.Insert(
            new DapperCallForFunds{
                Id = callForFunds.Id.ToString(), 
                CondoId = callForFunds.CondoId.ToString(), 
                QuarterAmount = callForFunds.QuarterAmount, 
                Quarter = (int)callForFunds.Quarter });
    }

    public bool HasCallBeenLauched(Quarter currentQuarter)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_connectionId));
        return false;
    }

    public global::SylHome.Hexagon.Models.CallForFunds ById(Guid callForFundsId)
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(_connectionId));
        
        IEnumerable<DapperCallForFunds> dapperCallForFunds = connection.Query<DapperCallForFunds>(
            "select * from CallForFunds where Id = @id", 
            new { id = callForFundsId.ToString() });
        
        var config = new MapperConfiguration(cfg => 
            cfg.CreateMap<DapperCallForFunds, global::SylHome.Hexagon.Models.CallForFunds>());
        
        var mapper = config.CreateMapper();
        return mapper.Map<global::SylHome.Hexagon.Models.CallForFunds>(dapperCallForFunds.FirstOrDefault());
    }
}