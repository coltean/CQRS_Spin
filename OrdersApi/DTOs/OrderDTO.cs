namespace OrdersApi.DTOs
{
    public record OrderDTO(int Id, string FirstName, string LastName, string Status, DateTime CreatedAt, decimal TotalCost)
    {
    }
}
