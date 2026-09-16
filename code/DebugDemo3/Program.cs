using Metalama.Documentation.QuickStart;
using Metalama.Framework;
namespace DebugDemo3
{
    public class Demo
    {
        public static void Main()
        {
            string detailXML = GetCustomerDetailsXML("CUST001");
        }

        [Retry]
        [Log]
        public static string GetCustomerDetailsXML(string customerID)
        {
            //TODO
            return string.Empty;
        }
    }
}