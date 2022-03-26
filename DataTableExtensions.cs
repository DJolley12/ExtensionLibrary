using System.Data;
using System.Reflection;

namespace ExtensionLibrary;
public static class DataTableExtensions
{
    public static List<T> ToList<T>(DataTable dt) where T : new()
    {
        List<T> returnList = new List<T>();
        var properties = typeof(T).GetProperties();
        foreach (DataRow row in dt.Rows)
        {
            var obj = Convert<T>(properties, row);
            returnList.Add(obj);
        }
        return returnList;
    }

    public static T ToObj<T>(DataTable dt) where T : new() => 
        Convert<T>(typeof(T).GetProperties(), dt.Rows[0]);

    private static T Convert<T>(IEnumerable<PropertyInfo> properties, DataRow row) where T : new()
    {
        T obj = new T();
        foreach (PropertyInfo property in properties)
        {
            if (row[property.Name] is DBNull)
            {
                property.SetValue(obj, null);
            }
            else
            {
                property.SetValue(obj, row[property.Name]);
            }
        }
        return obj;
    }
}
