using Newtonsoft.Json.Linq;

namespace BankConsole;

public static class ValidJson
{
    public static bool IsValid(string value)
    {
        try
        {
            var obj = JObject.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsExistCommand(JObject jObject, string obj)
    {
        try
        {
            string str = jObject[obj].ToString();
            return !string.IsNullOrEmpty(str);
        }
        catch
        {
            return false;
        }
    }
}