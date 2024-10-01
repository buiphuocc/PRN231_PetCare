namespace Infrastructure.ViewModels.OrderDTO;

public class UpdateQuantityRequest
{
    public int orderid { get; set; }
    public int productid { get; set; }
    public int Quantity { get; set; }
}