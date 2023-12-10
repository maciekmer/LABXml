using XmlSearch.Utils;
using System.Xml;
using XmlSearch.Model;
using System.Text;

namespace XmlSearch.XML
{
    public class SaxXmlAnalyser : IXmlAnalyser
    {
        private readonly XmlReader _reader;
        private Filters _currentScope;
        private readonly Dictionary<string, Filters> _stringToFilters;

        public SaxXmlAnalyser()
        {
            try
            {
                _reader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(FileManager.GetInstance().xml.Content)));
                _reader.MoveToContent();
                if (_reader.NodeType == XmlNodeType.EndElement) _reader.Read();
            }
            catch { throw; }

            _stringToFilters = new Dictionary<string, Filters>
            {
                {"InvoiceId", Filters.InvoiceId},
                {"Branch", Filters.MarketBranch},
                {"City", Filters.City},
                {"CustomerType", Filters.CustomerType},
                {"Sex", Filters.Sex},
                {"ProductLine", Filters.ProductLine},
                {"UnitPrice", Filters.ProductUnitPrice},
                {"Quantity", Filters.ProductQuantity},
                {"Tax", Filters.ProductTax},
                {"Total", Filters.ProductTotal},
                {"Date", Filters.Date},
                {"Payment", Filters.Payment},
                {"CostOfGoods", Filters.ProductCost},
                {"Rating", Filters.Rating}
            };
        }

        private Sale ReadSale()
        {
            var currentSale = new Sale();
            while (!(_reader.NodeType == XmlNodeType.EndElement && _reader.Name.Equals("Sale")))
            {
                if (_reader.NodeType == XmlNodeType.Element)
                {
                    if (_reader.Name == "Sale") currentSale = new Sale();
                    else if (_stringToFilters.ContainsKey(_reader.Name))
                        _currentScope = _stringToFilters[_reader.Name];
                    else if (_reader.Name != "Sales") return null;
                }
                else if (_reader.NodeType == XmlNodeType.Text)
                {
                    string value = _reader.Value;
                    switch (_currentScope)
                    {
                        case Filters.None:
                            break;
                        case Filters.InvoiceId:
                            if (!XmlParser.ParseInvoiceId(value)) return null;
                            currentSale.InvoiceId = value;
                            break;
                        case Filters.MarketBranch:
                            if (!XmlParser.ParseMarketBranch(value)) return null;
                            currentSale.MarketBranch = value;
                            break;
                        case Filters.City:
                            if (!XmlParser.ParseCity(value)) return null;
                            currentSale.City = value;
                            break;
                        case Filters.CustomerType:
                            if (!XmlParser.ParseCustomerType(value)) return null;
                            currentSale.CustomerType = value;
                            break;
                        case Filters.Sex:
                            if (!XmlParser.ParseSex(value)) return null;
                            currentSale.Sex = value;
                            break;
                        case Filters.ProductLine:
                            if (!XmlParser.ParseProductLine(value)) return null;
                            currentSale.ProductLine = value;
                            break;
                        case Filters.ProductUnitPrice:
                            if (!XmlParser.ParsePositiveDouble(value, out double unitPrice)) return null;
                            currentSale.ProductUnitPrice = unitPrice;
                            break;
                        case Filters.ProductQuantity:
                            if (!XmlParser.ParsePositiveInt(value, out int quantity)) return null;
                            currentSale.ProductQuantity = quantity;
                            break;
                        case Filters.ProductTax:
                            if (!XmlParser.ParsePositiveDouble(value, out double tax)) return null;
                            currentSale.ProductTax = tax;
                            break;
                        case Filters.ProductTotal:
                            if (!XmlParser.ParsePositiveDouble(value, out double total)) return null;
                            currentSale.ProductTotal = total;
                            break;
                        case Filters.Date:
                            if (!XmlParser.ParseDate(value, out DateOnly date)) return null;
                            currentSale.Date = date;
                            break;
                        case Filters.Payment:
                            if (!XmlParser.ParsePayment(value)) return null;
                            currentSale.Payment = value;
                            break;
                        case Filters.ProductCost:
                            if (!XmlParser.ParsePositiveDouble(value, out double costOfGoods)) return null;
                            currentSale.ProductCostWithoutTax = costOfGoods;
                            break;
                        case Filters.Rating:
                            if (!XmlParser.ParsePositiveDouble(value, out double rating)) return null;
                            currentSale.Rating = rating;
                            break;
                    }
                }

                _reader.Read();
            }

            return currentSale;
        }

        private List<Sale> GetNonNullSales(Func<Sale, bool> filter)
        {
            List<Sale> sales = new List<Sale>();

            while (!_reader.EOF)
            {
                if (_reader.NodeType == XmlNodeType.Element && _reader.Name.Equals("Sale"))
                {
                    var sale = ReadSale();
                    if (sale != null && filter(sale))
                    {
                        sales.Add(sale);
                    }
                }
                else
                {
                    _reader.Read();
                }
            }
            return sales;
        }

        public List<Sale> GetAllSales()
        {

            return GetNonNullSales(sell => true);
        }

        public List<Sale> GetByCity(string city)
        {
            return GetNonNullSales(sale => sale.City.Equals(city));
        }

        public List<Sale> GetByCustomerType(string customerType)
        {
            return GetNonNullSales(sale => sale.CustomerType == customerType);
        }

        public List<Sale> GetByDate(DateOnly date)
        {
            return GetNonNullSales(sale => sale.Date == date);
        }

        public List<Sale> GetBySex(string sex)
        {
            return GetNonNullSales(sale => sale.Sex == sex);
        }

        public List<Sale> GetByInvoiceId(string invoiceId)
        {
            var list = new List<Sale>
            {
                GetNonNullSales(sale => sale.InvoiceId == invoiceId).First()
            };

            return list;
        }

        public List<Sale> GetByMarketBranch(string marketBranch)
        {
            return GetNonNullSales(sale => sale.MarketBranch == marketBranch);
        }

        public List<Sale> GetByPayment(string payment)
        {
            return GetNonNullSales(sale => sale.Payment == payment);
        }

        public List<Sale> GetByProductCostWithoutTax(double productCostOfGoods)
        {
            return GetNonNullSales(sale => sale.ProductCostWithoutTax == productCostOfGoods);
        }

        public List<Sale> GetByProductLine(string productLine)
        {
            return GetNonNullSales(sale => sale.ProductLine.Contains(productLine));
        }

        public List<Sale> GetByProductQuantity(int productQuantity)
        {
            return GetNonNullSales(sale => sale.ProductQuantity == productQuantity);
        }

        public List<Sale> GetByProductTax(double productTax)
        {
            return GetNonNullSales(sale => sale.ProductTax == productTax);
        }

        public List<Sale> GetByProductTotal(double productTotal)
        {
            return GetNonNullSales(sale => sale.ProductTotal == productTotal);
        }

        public List<Sale> GetByProductUnitPrice(double productUnitPrice)
        {
            return GetNonNullSales(sale => sale.ProductUnitPrice == productUnitPrice);
        }

        public List<Sale> GetByRating(double rating)
        {
            return GetNonNullSales(sale => sale.Rating == rating);
        }
    }
}