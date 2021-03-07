using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Data
{
    public static class Recursos
    {
        public const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;
    }

    public class Converter<T>
    {
        public static List<T> ConvertDataSetToList(IDataReader data)
        {
            List<T> lista = new List<T>();
            if (data != null)
            {
                while (data.Read())
                {
                    T itemClass = ConvertReaderToObject(data);

                    lista.Add(itemClass);
                }
            }
            data.Close();
            return lista;
        }

        public static T ConvertReaderToObject(IDataReader data)
        {

            try
            {
                T itemClass = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] properties = itemClass.GetType().GetProperties((Recursos.flags));

                for (int i = 0; i < data.FieldCount; i++)
                {
                    string currentName = data.GetName(i);
                    PropertyInfo currentProperty = properties.FirstOrDefault(
                        x => currentName.ToLower().Equals(x.Name.ToLower()));

                    if (currentProperty != null)
                    {
                        if (data[currentName] != null && !System.DBNull.Value.Equals(data[currentName]))
                        {

                            currentProperty.SetValue(itemClass, data[currentName], null);
                        }
                    }
                }
                return itemClass;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
