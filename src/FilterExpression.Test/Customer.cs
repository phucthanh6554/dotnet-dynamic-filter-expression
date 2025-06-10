namespace FilterExpression.Test;

public class Customer
{
    public int Id { get; init; }
    
    public string FirstName { get; init; } = string.Empty;
    
    public string LastName { get; init; } = string.Empty;
    
    public EmailAddress Email { get; init; } = new EmailAddress();
    
    public string PhoneNumber { get; init; } = string.Empty;
    
    public DateTime DateOfBirth { get; init; }
    
    public DeliveryAddress DeliveryAddress { get; init; } = new DeliveryAddress();
}