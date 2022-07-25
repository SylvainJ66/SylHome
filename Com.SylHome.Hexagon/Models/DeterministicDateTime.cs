namespace SylHome.Models;

public class DeterministicDateTime
{
    private DateTime DateOfNow;

    public DateTime Now() => DateOfNow;

    public void SetDateOfNow(DateTime date) => DateOfNow = date;
}