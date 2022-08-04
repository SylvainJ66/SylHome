using Dapper.Contrib.Extensions;
using SylHome.Hexagon.Models;

namespace Com.SylHome.Adapters.Secondary.Gateways.Repositories.Dapper.CallForFundsNameSpace;

[Table("callforfunds")]
public class DapperCallForFunds
{
    [Key]
    public string Id { get; set; }
    public string CondoId { get; set; }
    public decimal QuarterAmount { get; set; }
    public int Quarter { get; set; }
}