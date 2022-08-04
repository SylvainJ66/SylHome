namespace SylHome.Hexagon.Models;

public class CallForFunds
{
    public Guid Id { get; }
    public Guid CondoId { get; }
    public decimal QuarterAmount { get; }
    public Quarter Quarter { get; }

    public CallForFunds(Guid id, Guid condoId, decimal quarterAmount, Quarter quarter)
    {
        Id = id;
        CondoId = condoId;
        QuarterAmount = quarterAmount;
        Quarter = quarter;
    }

    public bool IsAbout(Quarter currentQuarter)
    {
        return currentQuarter == Quarter;
    }

    private bool Equals(CallForFunds other)
    {
        return Id.Equals(other.Id) && 
               CondoId.Equals(other.CondoId) && 
               QuarterAmount == other.QuarterAmount && 
               Quarter == other.Quarter;
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
        return HashCode.Combine(Id, CondoId, QuarterAmount, (int)Quarter);
    }
}