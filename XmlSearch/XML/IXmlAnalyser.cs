using XmlSearch.Model;
namespace XmlSearch.XML
{
	public interface IXmlAnalyser
	{
		public List<Sale> GetAllSales();
		public List<Sale> GetByInvoiceId(string invoiceId);
		public List<Sale> GetByMarketBranch(string marketBranch);
		public List<Sale> GetByCity(string city);
		public List<Sale> GetByCustomerType(string customerType);
		public List<Sale> GetBySex(string sex);
		public List<Sale> GetByProductLine(string productLine);
		public List<Sale> GetByProductUnitPrice(double productUnitPrice);
		public List<Sale> GetByProductQuantity(int productQuanity);
		public List<Sale> GetByProductCostWithoutTax(double productCostOfGoods);
		public List<Sale> GetByProductTax(double productTax);
		public List<Sale> GetByProductTotal(double productTotal);
		public List<Sale> GetByDate(DateOnly date);
		public List<Sale> GetByPayment(string payment);
		public List<Sale> GetByRating(double rating);
	}
}