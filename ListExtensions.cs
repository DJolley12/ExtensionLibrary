using System.Data;
using System.Reflection;

namespace ExtensionLibrary;
public static class ListExtensions
{
    public static DataTable ToDataTable<T>(this List<T> list)
    {
        var properties = typeof(T).GetProperties();
        var dt = Convert<T>(properties);
        foreach (var obj in list)
        {
            dt.Rows.Add(Convert<T>(dt, obj!, properties));
        }
        return dt;
    }

    private static DataTable Convert<T>(IEnumerable<PropertyInfo> properties)
    {
        var dt = new DataTable();
        foreach (var property in properties)
        {
            dt.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType)!);
        }
        return dt;
    }

    private static DataRow Convert<T>(DataTable dt, object obj, IEnumerable<PropertyInfo> properties)
    {
        var row = dt.NewRow();
        foreach (var property in properties)
        {
            row[property.Name] = property.GetValue(obj);
        }

        return row;
    }
}
