namespace XmlSearch.Model;

public class Sale
{
    public string InvoiceId { get; set; }
    public string MarketBranch { get; set; }
    public string City { get; set; }
    public string CustomerType { get; set; }
    public string Sex { get; set; }
    public string ProductLine { get; set; }
    public double ProductUnitPrice { get; set; }
    public int ProductQuantity { get; set; }
    public double ProductCostWithoutTax { get; set; }
    public double ProductTax { get; set; }
    public double ProductTotal { get; set; }
    public DateOnly Date { get ; set; }
    public string Payment { get; set; }
    public double Rating { get; set; }
}