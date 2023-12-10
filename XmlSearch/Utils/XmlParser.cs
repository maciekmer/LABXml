using System.Globalization;
using System.Text.RegularExpressions;
using XmlSearch.Model;

namespace XmlSearch.Utils
{
    public static class XmlParser
    {

        public static bool ParseInvoiceId(string id)
        {
            return new Regex(@"^\d{3}-\d{2}-\d{4}$").IsMatch(id);
        }

        public static bool ParseCity(string city)
        {
            return !string.IsNullOrWhiteSpace(city);
        }

        public static bool ParseProductLine(string productLine)
        {
            return !string.IsNullOrWhiteSpace(productLine);
        }

        public static bool ParsePositiveDouble(string valueStr, out double result)
        {
            return double.TryParse(valueStr, out result) && result >= 0;
        }

        public static bool ParsePositiveInt(string valueStr, out int result)
        {
            return int.TryParse(valueStr, out result) && result >= 0;
        }

        public static bool ParseDate(string date, out DateOnly outDate)
        {
            return DateOnly.TryParseExact(date, "M/d/yyyy", null, DateTimeStyles.None, out outDate);
        }

        public static bool ParsePayment(string payment)
        {
            return !string.IsNullOrWhiteSpace(payment);
        }

        public static bool ParseSex(string sex)
        {
            return !string.IsNullOrWhiteSpace(sex);
        }

        public static bool ParseMarketBranch(string branch)
        {
            return !string.IsNullOrWhiteSpace(branch);
        }

        public static bool ParseCustomerType(string branch)
        {
            return !string.IsNullOrWhiteSpace(branch);
        }

    }
}

