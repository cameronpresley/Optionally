using System;

namespace Optionally
{
    public static class Converter
    {
        public static Option<Int32> ToInt(string s)
        {
            return Int32.TryParse(s, out int x) ? Option.Some(x) : Option.No<int>();
        }

        public static Option<DateTime> ToDateTime(string s)
        {
            return DateTime.TryParse(s, out DateTime x) ? Option.Some(x) : Option.No<DateTime>();
        }

        public static Option<decimal> ToDecimal(string s)
        {
            return Decimal.TryParse(s, out decimal x) ? Option.Some(x) : Option.No<decimal>();
        }

        public static Option<double> ToDouble(string s)
        {
            return Double.TryParse(s, out double x) ? Option.Some(x) : Option.No<double>();
        }

        public static Option<bool> ToBool(string s)
        {
            return Boolean.TryParse(s, out bool x) ? Option.Some(x) : Option.No<bool>();
        }
    }
}
