using System.Xml;
using XmlSearch.Model;
using XmlSearch.Utils;

namespace XmlSearch.XML
{
    public class DomXmlAnalyser : IXmlAnalyser
    {
        private List<Sale> _sales;
        private XmlDocument _document;

        public DomXmlAnalyser()
        {
            _sales = new List<Sale>();
            _document = new XmlDocument();
            
            try{
                _document.LoadXml(FileManager.GetInstance().xml.Content);
                _sales = _document.SelectSingleNode("descendant::Sales")
                    .ChildNodes // get get all Sale nodes (XmlNodeList)
                    .Cast<XmlNode>() // transform to IEnumerable<XmlNode>
                    .Select(XmlNodeToSale).Where(e => e != null).ToList(); // transform to List<Sale>
            } catch {
                throw;
            }
        }

        public List<Sale> GetAllSales(){
            return _sales;
        }

        public List<Sale> GetByInvoiceId(string invoiceId)
        {
            _sales.Clear();
            _sales.Add(_sales.Where(s => s.InvoiceId == invoiceId).First());
            return _sales;
        }

        public List<Sale> GetByMarketBranch(string marketBranch)
        {
            return _sales.Where(s => s.MarketBranch == marketBranch).ToList();
        }

        public List<Sale> GetByCity(string city)
        {
            return _sales.Where(s => s.City == city).ToList();
        }

        public List<Sale> GetByCustomerType(string customerType)
        {
            return _sales.Where(s => s.CustomerType == customerType).ToList();
        }

        public List<Sale> GetBySex(string sex)
        {
            return _sales.Where(s => s.Sex == sex).ToList();
        }

        public List<Sale> GetByProductLine(string productLine)
        {
            return _sales.Where(s => s.ProductLine.Contains(productLine)).ToList();
        }

        public List<Sale> GetByProductUnitPrice(double productUnitPrice)
        {
            return _sales.Where(s => s.ProductUnitPrice == productUnitPrice).ToList();
        }

        public List<Sale> GetByProductQuantity(int productQuanity)
        {
            return _sales.Where(s => s.ProductQuantity == productQuanity).ToList();
        }

        public List<Sale> GetByProductCostWithoutTax(double productCostOfGoods)
        {
            return _sales.Where(s => s.ProductCostWithoutTax == productCostOfGoods).ToList();
        }

        public List<Sale> GetByProductTax(double productTax)
        {
            return _sales.Where(s => s.ProductTax == productTax).ToList();
        }

        public List<Sale> GetByProductTotal(double productTotal)
        {
            return _sales.Where(s => s.ProductTotal == productTotal).ToList();
        }

        public List<Sale> GetByDate(DateOnly date)
        {
            return _sales.Where(s => s.Date == date).ToList();
        }

        public List<Sale> GetByPayment(string payment)
        {
            return _sales.Where(s => s.Payment == payment).ToList();
        }

        public List<Sale> GetByRating(double rating)
        {
            return _sales.Where(s => s.Rating == rating).ToList();
        }

        private Sale XmlNodeToSale(XmlNode saleNode)
        {
            var invoiceId = saleNode.SelectSingleNode("descendant::InvoiceId")?.InnerText;
            var branch = saleNode.SelectSingleNode("descendant::Branch")?.InnerText;
            var city = saleNode.SelectSingleNode("descendant::City")?.InnerText;
            var customerType = saleNode.SelectSingleNode("descendant::CustomerType")?.InnerText;
            var sex = saleNode.SelectSingleNode("descendant::Sex")?.InnerText;
            var productLine = saleNode.SelectSingleNode("descendant::ProductLine")?.InnerText;
            var unitPrice = saleNode.SelectSingleNode("descendant::UnitPrice")?.InnerText;
            var quantity = saleNode.SelectSingleNode("descendant::Quantity")?.InnerText;
            var tax = saleNode.SelectSingleNode("descendant::Tax")?.InnerText;
            var total = saleNode.SelectSingleNode("descendant::Total")?.InnerText;
            var date = saleNode.SelectSingleNode("descendant::Date")?.InnerText;
            var payment = saleNode.SelectSingleNode("descendant::Payment")?.InnerText;
            var costOfGoods = saleNode.SelectSingleNode("descendant::CostOfGoods")?.InnerText;
            var rating = saleNode.SelectSingleNode("descendant::Rating")?.InnerText;

            return SaleFactory.Instance.CreateSale(invoiceId, branch, city, customerType, sex, productLine, unitPrice,
                quantity, tax, total, date, payment, costOfGoods, rating);
        }
    }
}

