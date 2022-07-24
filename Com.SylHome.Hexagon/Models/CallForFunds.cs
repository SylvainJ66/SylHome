namespace SylHome.Models;

public class CallForFunds
{
    private readonly Guid _id;
    private readonly Guid _condoId;
    private readonly decimal _quarterAmount;
    private readonly Quarter _quarter;

    public CallForFunds(Guid id, Guid condoId, decimal quarterAmount, Quarter quarter)
    {
        _id = id;
        _condoId = condoId;
        _quarterAmount = quarterAmount;
        _quarter = quarter;
    }

    public bool IsAbout(Quarter currentQuarter)
    {
        return currentQuarter == _quarter;
    }

    public bool Equal(object o)
    {
        if (o == null) throw new ArgumentNullException(nameof(o));
        if (this == o) return true;
        if (this.GetType() != o.GetType()) return false;
        CallForFunds that = (CallForFunds)o;
        return _id == that._id &&
               _condoId == that._condoId &&
               _quarterAmount == that._quarterAmount &&
               _quarter == that._quarter;
    }
    
}