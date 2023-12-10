using System.Globalization;
using XmlSearch.Utils;

namespace XmlSearch.Model;

public class SaleFactory
{
	private static SaleFactory _instance = null;

	public static SaleFactory Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SaleFactory();
			}

			return _instance;
		}
	}

	public Sale CreateSale(string invoiceIdStr, string branchStr, string cityStr, string customerTypeStr, string sexStr, string productLineStr, string unitPriceStr, string quantityStr, string taxStr, string totalStr, string dateStr, string paymentStr, string costOfGoodsStr, string ratingStr)
	{

		var sale = new Sale();
		bool isValid = true;

		isValid &= XmlParser.ParseInvoiceId(invoiceIdStr);
		sale.InvoiceId = invoiceIdStr;

		isValid &= XmlParser.ParseMarketBranch(branchStr);
		sale.MarketBranch = branchStr;

        isValid &= XmlParser.ParseCity(cityStr);
        sale.City = cityStr;

		isValid &= XmlParser.ParseCustomerType(customerTypeStr);
		sale.CustomerType = customerTypeStr;

		isValid &= XmlParser.ParseSex(sexStr);
		sale.Sex = sexStr;

		isValid &= XmlParser.ParseProductLine(productLineStr);
		sale.ProductLine = productLineStr;

		isValid &= XmlParser.ParsePositiveDouble(unitPriceStr, out double unitPrice);
		sale.ProductUnitPrice = unitPrice;

		isValid &= XmlParser.ParsePositiveInt(quantityStr, out int quantity);
		sale.ProductQuantity = quantity;

		isValid &= XmlParser.ParsePositiveDouble(taxStr, out double tax);
		sale.ProductTax = tax;

		isValid &= XmlParser.ParsePositiveDouble(totalStr, out double total);
		sale.ProductTotal = total;

		isValid &= DateOnly.TryParseExact(dateStr, "M/d/yyyy", null, DateTimeStyles.None, out var date);
		sale.Date = date;

		isValid &= XmlParser.ParsePayment(paymentStr);
		sale.Payment = paymentStr;

		isValid &= XmlParser.ParsePositiveDouble(costOfGoodsStr, out double costOfGoods);
		sale.ProductCostWithoutTax = costOfGoods;

		isValid &= XmlParser.ParsePositiveDouble(ratingStr, out double rating);
		sale.Rating = rating;

		return isValid ? sale : null;
	}
}
