public class AddShieldGA : GameAction
{
    public int Amount { get; }
    public Shield Target { get; }

    public AddShieldGA(int amount, Shield target)
    {
        Amount = amount;
        Target = target;
    }
}