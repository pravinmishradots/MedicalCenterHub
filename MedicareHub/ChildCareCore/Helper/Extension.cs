
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ChildCareCore.Helper
{

    public static class Extension
    {

        #region "Dictionary"

        public static void AddOrReplace(this IDictionary<string, object> DICT, string key, object value)
        {
            if (DICT.ContainsKey(key))
                DICT[key] = value;
            else
                DICT.Add(key, value);
        }

        public static dynamic GetObjectOrDefault(this IDictionary<string, object> DICT, string key)
        {
            if (DICT.ContainsKey(key))

                return DICT[key];
            else
                return null;
        }

        public static T GetObjectOrDefault<T>(this IDictionary<string, object> DICT, string key)
        {
            if (DICT.ContainsKey(key))
                return (T)Convert.ChangeType(DICT[key], typeof(T));
            else
                return default(T);
        }

        #endregion "Dictionary"

        #region "Enums"

        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                        .ToDictionary(k => k.Key, k => k.Value);
        }

        public static IDictionary<string, int> EnumToDictionaryWithDescription(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return Enumerable.Range(0, names.Length)
                .Select(i => new { Key = ((Enum)values.GetValue(i)).GetDescription(), Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }

        public static IDictionary<string, string> EnumToDictionaryKeyWithDescription(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return Enumerable.Range(0, names.Length)
                .Select(i => new { Key = ((Enum)values.GetValue(i)).GetDescription(), Value = values.GetValue(i).ToString() })
                .ToDictionary(k => k.Key, k => k.Value.ToString());
        }

        public static DataTable DictionaryToDataTable(this List<Dictionary<string, object>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            result.Columns.AddRange(
                list.First().Select(r => new DataColumn(r.Key)).ToArray()
            );

            list.ForEach(r => result.Rows.Add(r.Select(c => c.Value).ToArray()));

            return result;
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            DescriptionAttribute[] descriptionAttribute = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else if (descriptionAttribute != null && descriptionAttribute.Length > 0)
                return descriptionAttribute[0].Description;
            else
                return value.ToString();
        }

        public static string GetDescription<T>(this string value)
        {
            MemberInfo property = typeof(T).GetProperty(value);

            if (property != null)
            {
                DisplayNameAttribute[] attributes = (DisplayNameAttribute[])property.GetCustomAttributes(typeof(DisplayNameAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].DisplayName;
            }
            return value.ToString();
        }


        public static List<string> GetPropertiesNameOfClass<T>()
        {
            List<string> propertyList = new List<string>();
            foreach (var prop in typeof(T).GetProperties())
            {
                propertyList.Add(prop.Name);
            }
            return propertyList;
        }


        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion "Enums"
    }














    //public static class Extension
    //{

    //    public static void AddOrReplace(this IDictionary<string, object> dictionaryContainer, string key, object value)
    //    {
    //        if (dictionaryContainer.ContainsKey(key))
    //        {
    //            dictionaryContainer[key] = value;
    //        }
    //        else
    //        {
    //            dictionaryContainer.Add(key, value);
    //        }


    //    }
    //}
}
