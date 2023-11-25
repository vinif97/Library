using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Helpers.Extensions
{
    public static class ObjectExtension
    {
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public static void UpdatePropertiesIfNotNull(this object objectToUpdate, object objectDto, string methodPrefix)
        {
            var propsToChange = objectDto.GetType().GetProperties().Where(a => a.GetValue(objectDto) != null);
            var methods = objectToUpdate.GetType().GetMethods();

            foreach (var prop in propsToChange)
            {
                var method = methods.FirstOrDefault(a => a.Name == methodPrefix + prop.Name);

                object? value = objectDto.GetType().GetProperty(prop.Name)!.GetValue(objectDto);

                if (method is not null && value is not null)
                {
                    object[] parmeters = new object[] { value };
                    method.Invoke(objectToUpdate, parmeters);
                }
            }
        }
    }
}
