
using System.Text;
using System.Xml.Linq;
using XmlSearch.Model;
using XmlSearch.Utils;

namespace XmlSearch.XML
{
    public class LinqXmlAnalyser : IXmlAnalyser
    {
        private readonly XElement _root;
        public LinqXmlAnalyser()
        {
            try
            {
                _root = XElement.Load(new MemoryStream(Encoding.UTF8.GetBytes(FileManager.GetInstance().xml.Content)));
            }
            catch { throw; }
        }

        public List<Sale> GetAllSales()
        {
            return _root.Elements().Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByInvoiceId(string invoiceId)
        {
            var list = new List<Sale>(1)
            {
                _root.Elements().Where(e =>
            {
                var value = e.Element("InvoiceId").Value;

                return XmlParser.ParseInvoiceId(value) && invoiceId.Equals(value);
            }
            ).Select(CreateSaleFromXElement).First()
            };
            return list;
        }

        public List<Sale> GetByMarketBranch(string marketBranch)
        {
            return _root.Elements().Where(e =>
            {
                var branch = e.Element("Branch").Value;
                return XmlParser.ParseMarketBranch(branch) && branch.Equals(marketBranch);
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByCity(string city)
        {
            return _root.Elements().Where(e =>
            {
                var value = e.Element("City").Value;
                return XmlParser.ParseCity(value) && city.Equals(value);
            })
                .Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByCustomerType(string customer)
        {
            return _root.Elements().Where(e =>
            {
                var value = e.Element("CustomerType").Value;
                return XmlParser.ParseCustomerType(value) && value.Equals(customer);
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetBySex(string sex)
        {
            return _root.Elements().Where(e =>
            {
                var value = e.Element("Sex").Value;
                return XmlParser.ParseSex(value) && value.Equals(sex);
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByProductLine(string productLine)
        {
            return _root.Elements().Where(e =>
            {
                var value = e.Element("ProductLine").Value;
                return XmlParser.ParseProductLine(value) && value.Contains(productLine);
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByProductUnitPrice(double unitPrice)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParsePositiveDouble(e.Element("UnitPrice").Value, out double parsedUnitPrice);
                return parsedUnitPrice == unitPrice;
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByProductQuantity(int quantity)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParsePositiveInt(e.Element("Quantity").Value, out int parsedQuantity);
                return parsedQuantity == quantity;
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByProductCostWithoutTax(double costOfGoods)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParsePositiveDouble(e.Element("CostOfGoods").Value, out double parsedCostOfGoods);
                return parsedCostOfGoods == costOfGoods;
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByProductTax(double tax)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParsePositiveDouble(e.Element("Tax").Value, out double parsedTax);
                return parsedTax == tax;
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByProductTotal(double total)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParsePositiveDouble(e.Element("Total").Value, out double parsedTotal);
                return parsedTotal == total;
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByDate(DateOnly date)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParseDate(e.Element("Date").Value, out var parsedDate);
                return parsedDate == date;
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByPayment(string payment)
        {
            return _root.Elements().Where(e =>
            {
                var value = e.Element("Payment").Value;
                return XmlParser.ParsePayment(value) && value.Equals(payment);
            }).Select(CreateSaleFromXElement).ToList();
        }

        public List<Sale> GetByRating(double rating)
        {
            return _root.Elements().Where(e =>
            {
                XmlParser.ParsePositiveDouble(e.Element("Rating").Value, out double parsedRating);
                return parsedRating == rating;
            }).Select(CreateSaleFromXElement).ToList();
        }

        private Sale CreateSaleFromXElement(XElement saleElement)
        {
            try
            {
                var invoiceId = saleElement.Element("InvoiceId").Value;
                var branch = saleElement.Element("Branch").Value;
                var city = saleElement.Element("City").Value;
                var customerType = saleElement.Element("CustomerType").Value;
                var sex = saleElement.Element("Sex").Value;
                var productLine = saleElement.Element("ProductLine").Value;
                var unitPrice = saleElement.Element("UnitPrice").Value;
                var quantity = saleElement.Element("Quantity").Value;
                var tax = saleElement.Element("Tax").Value;
                var total = saleElement.Element("Total").Value;
                var date = saleElement.Element("Date").Value;
                var payment = saleElement.Element("Payment").Value;
                var costOfGoods = saleElement.Element("CostOfGoods").Value;
                var rating = saleElement.Element("Rating").Value;

                return SaleFactory.Instance.CreateSale(invoiceId, branch, city, customerType, sex, productLine, unitPrice,
                quantity, tax, total, date, payment, costOfGoods, rating);
            }
            catch { throw; }
        }
    }
}

