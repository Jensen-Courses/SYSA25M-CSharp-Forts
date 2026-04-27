namespace core.Entities.Orders;

public class PaymentInfo
{
    public required string Owner { get; set; }
    public int LastFourDigits { get; set; }
    public required string CardType { get; set; }
    public int ExpireMonth { get; set; }
    public int ExpireYear { get; set; }
}