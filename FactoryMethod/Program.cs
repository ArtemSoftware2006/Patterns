public interface IMembership
{
    string Name {get;}
    decimal GetPrice();
}
public record class GymMembership : IMembership
{
    public GymMembership( decimal price)
    {
        Name = "Gym";
        Price = price;
    }
    public decimal Price{ get;private set; } 
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal GetPrice()
    {
        return Price;
    }
}
public record class GymPlusPoolMembership : IMembership
{
    public GymPlusPoolMembership( decimal price)
    {
        Name = "GymPlusPool";
        Price = price;
    }
    public decimal Price{ get;private set; } 
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal GetPrice()
    {
        return Price;
    }
}
public abstract class MembershipFactory
{
    public abstract IMembership GetMembership();
}
public class GymMembershipFactory : MembershipFactory
{
    private readonly decimal _price;
    public GymMembershipFactory(decimal price)
    {
        _price = price;
    }
    public override IMembership GetMembership()
    {
        return new GymMembership(_price);
    }
}
public class GymPlusPoolMembershipFactory : MembershipFactory
{
   private readonly decimal _price;
    public GymPlusPoolMembershipFactory(decimal price)
    {
        _price = price;
    }
    public override IMembership GetMembership()
    {
        return new GymPlusPoolMembership(_price);
    }
}