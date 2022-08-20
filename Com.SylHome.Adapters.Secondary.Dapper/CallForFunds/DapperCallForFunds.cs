using Dapper.Contrib.Extensions;

namespace Com.SylHome.Adapters.Secondary.CallForFunds;

[Table("callforfunds")]
public class DapperCallForFunds
{
    [Key]
    public string Id { get; set; }
    public string CondoId { get; set; }
    public decimal QuarterAmount { get; set; }
    public int Quarter { get; set; }
}