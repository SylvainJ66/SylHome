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

    private bool Equals(CallForFunds other)
    {
        return _id.Equals(other._id) && 
               _condoId.Equals(other._condoId) && 
               _quarterAmount == other._quarterAmount && 
               _quarter == other._quarter;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CallForFunds)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _condoId, _quarterAmount, (int)_quarter);
    }
}