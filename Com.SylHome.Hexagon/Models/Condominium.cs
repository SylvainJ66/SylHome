namespace SylHome.Models;

public class Condominium
{
    public Guid Id { get; }
    private readonly decimal _yearlyBudget;

    public Condominium(Guid id, decimal yearlyBudget)
    {
        Id = id;
        _yearlyBudget = yearlyBudget;
    }

    public decimal QuarterBudget() => Math.Ceiling(_yearlyBudget / 4);

    public Quarter CurrentQuarter(DeterministicDateTime date)
    {
        var quarterLimits = new List<DateTime>
        {
            new DateTime(2022, 1, 1),
            new DateTime(2022, 3, 31),
            new DateTime(2022, 6, 30),
        };

        var quarters = new List<Quarter>
        {
            Quarter.First, Quarter.Second, Quarter.Third
        };

        Quarter currentQuarter = quarters.ElementAt(0);
        for (int i = 0; i < quarterLimits.Count; i++)
        {
            if (date.Now() >= quarterLimits.ElementAt(i))
            {
                currentQuarter = quarters.ElementAt(i);
            }
        }

        return currentQuarter;
    }
}