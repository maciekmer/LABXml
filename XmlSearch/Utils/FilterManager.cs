using XmlSearch.Model;
using XmlSearch.XML;
using CommunityToolkit.Maui.Alerts;
using System.Globalization;
using CommunityToolkit.Maui.Core;

namespace XmlSearch.Utils
{
    public class FilterManager
    {
        private IXmlAnalyser xmlAnalyser;
        private readonly Dictionary<Filters, Func<string, Task<List<Sale>>>> filterHandlers;

        public FilterManager(IXmlAnalyser xmlAnalyser)
        {
            this.xmlAnalyser = xmlAnalyser;
            filterHandlers = new Dictionary<Filters, Func<string, Task<List<Sale>>>>
            {
                { Filters.None, async keyword => HandleNone() },
                    { Filters.InvoiceId, HandleInvoiceId },
                    { Filters.MarketBranch, HandleMarketBranch },
                    { Filters.City, HandleCity },
                    { Filters.CustomerType, HandleCustomerType },
                    { Filters.Sex, HandleSex },
                    { Filters.ProductLine, HandleProductLine },
                    { Filters.ProductUnitPrice, HandleProductUnitPrice },
                    { Filters.ProductQuantity, HandleProductQuantity },
                    { Filters.ProductTax, HandleProductTax },
                    { Filters.ProductTotal, HandleProductTotal },
                    { Filters.Date, HandleDate },
                    { Filters.Payment, HandlePayment },
                    { Filters.ProductCost, HandleProductCost },
                    { Filters.Rating, HandleRating }
            };
        }

        public void UpdateAnalyser(IXmlAnalyser xmlAnalyser)
        {
            if(xmlAnalyser != null && this.xmlAnalyser != xmlAnalyser)
                this.xmlAnalyser = xmlAnalyser;
        }

        public async Task<List<Sale>> HandleFilter(Filters selectedFilter, string keyword)
        {
            if (filterHandlers.ContainsKey(selectedFilter))
            {
                return await filterHandlers[selectedFilter](keyword);
            }
            return null;
        }

        private List<Sale> HandleNone()
        {
            return xmlAnalyser.GetAllSales();
        }

        private async Task<List<Sale>> HandleInvoiceId(string id)
        {
            if (!XmlParser.ParseInvoiceId(id))
            {
                await ShowToastLongDuration("Неправильний формат ID");
                return null;
            }
            return xmlAnalyser.GetByInvoiceId(id);
        }

        private async Task<List<Sale>> HandleMarketBranch(string branchStr)
        {
            if (!XmlParser.ParseMarketBranch(branchStr))
            {
                await ShowToastLongDuration("Такого Branch не існує!");
                return null;
            }
            return xmlAnalyser.GetByMarketBranch(branchStr);
        }

        private async Task<List<Sale>> HandleCity(string city)
        {
            if (!XmlParser.ParseCity(city))
            {
                await ShowToastLongDuration("Такого міста не існує!");
                return null;
            }
            return xmlAnalyser.GetByCity(city);
        }

        private async Task<List<Sale>> HandleCustomerType(string customerType)
        {
            if (!XmlParser.ParseCustomerType(customerType))
            {
                await ShowToastLongDuration("Такого типу клієнта не існує!");
                return null;
            }
            return xmlAnalyser.GetByCustomerType(customerType);
        }

        private async Task<List<Sale>> HandleSex(string sexStr)
        {
            if (!XmlParser.ParseSex(sexStr))
            {
                await ShowToastLongDuration("Такої статі не існує!");
                return null;
            }
            return xmlAnalyser.GetBySex(sexStr);
        }

        private async Task<List<Sale>> HandleProductLine(string productLine)
        {
            if (!XmlParser.ParseProductLine(productLine))
            {
                await ShowToastLongDuration("ProductLine некоректний");
                return null;
            }
            return xmlAnalyser.GetByProductLine(productLine);
        }

        private async Task<List<Sale>> HandleProductUnitPrice(string unitPriceStr)
        {
            if (!XmlParser.ParsePositiveDouble(unitPriceStr, out double parsedUnitPrice))
            {
                await ShowToastLongDuration("Неправильний формат ціни");
                return null;
            }
            return xmlAnalyser.GetByProductUnitPrice(parsedUnitPrice);
        }

        private async Task<List<Sale>> HandleProductQuantity(string quantity)
        {
            if (!XmlParser.ParsePositiveInt(quantity, out int parsedQuantity))
            {
                await ShowToastLongDuration("Неправильний формат кількості");
                return null;
            }
            return xmlAnalyser.GetByProductQuantity(parsedQuantity);
        }

        private async Task<List<Sale>> HandleProductTax(string productTax)
        {
            if (!XmlParser.ParsePositiveDouble(productTax, out double parsedProductTax))
            {
                await ShowToastLongDuration("Неправильний формат податку");
                return null;
            }
            return xmlAnalyser.GetByProductTax(parsedProductTax);
        }

        private async Task<List<Sale>> HandleProductTotal(string productTotal)
        {
            if (!XmlParser.ParsePositiveDouble(productTotal, out double parsedProductTotal))
            {
                await ShowToastLongDuration("Неправильний формат суми");
                return null;
            }
            return xmlAnalyser.GetByProductTotal(parsedProductTotal);
        }

        private async Task<List<Sale>> HandleDate(string date)
        {
            if (!XmlParser.ParseDate(date, out DateOnly parsedDate))
            {
                await ShowToastLongDuration("Формат дати: M/d/yyyy");
                return null;
            }
            return xmlAnalyser.GetByDate(parsedDate);
        }

        private async Task<List<Sale>> HandlePayment(string paymentType)
        {
            if (!XmlParser.ParsePayment(paymentType))
            {
                await ShowToastLongDuration("Такого типу оплати не існує!");
                return null;
            }
            return xmlAnalyser.GetByPayment(paymentType);
        }

        private async Task<List<Sale>> HandleProductCost(string productCost)
        {
            if (!XmlParser.ParsePositiveDouble(productCost, out double parsedProductCost))
            {
                await ShowToastLongDuration("Неправильний формат вартості");
                return null;
            }
            return xmlAnalyser.GetByProductCostWithoutTax(parsedProductCost);
        }

        private async Task<List<Sale>> HandleRating(string rating)
        {
            if (!XmlParser.ParsePositiveDouble(rating, out double parsedRating))
            {
                await ShowToastLongDuration("Неправильний формат рейтингу");
                return null;
            }
            return xmlAnalyser.GetByRating(parsedRating);
        }

        private async Task ShowToastLongDuration(string message)
        {
            await Toast.Make(message, ToastDuration.Long).Show();
        }
    }
}