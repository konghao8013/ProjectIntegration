using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

public static class StringExtend
{
    public static string Format(this string format, params string[] args)
    {
        return string.Format(format, args);
    }
    static JavaScriptSerializer Json = new JavaScriptSerializer();
    public static string Serialize(this Object value)
    {
        return Json.Serialize(value);
    }
    public static T Deserialize<T>(this string value) where T : new()
    {
        T t = default(T);
        try
        {
            t = Json.Deserialize<T>(value);
        }
        catch (Exception)
        {

            t = new T();
        }
        return t;
    }
}
