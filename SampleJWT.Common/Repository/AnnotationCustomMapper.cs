using DapperExtensions.Mapper;
using System;
using System.Linq;

namespace SampleJWT.Repository
{
    public class AnnotationCustomMapper<T>: AutoClassMapper<T> 
        where T : class
    {
        public AnnotationCustomMapper():base()
        {
            Type type = typeof(T);
            var foundAttribute = type.GetCustomAttributes(typeof(Dapper.Contrib.Extensions.TableAttribute), true);
            if(foundAttribute.Any())
            {
                Table(((Dapper.Contrib.Extensions.TableAttribute)foundAttribute.FirstOrDefault()).Name);
            }
            var allproperties = type.GetProperties();
            foreach (var property in allproperties)
            {
                var columnMappings = property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute),true);
                if(columnMappings.Any())
                {
                    //Removing existing mapping(maybe due to AutoMapper)
                    var existingMapping = this.Properties.FirstOrDefault(item => item.PropertyInfo == property);
                    if (existingMapping != null)
                        this.Properties.Remove(existingMapping);
                    Map(property).Column(((System.ComponentModel.DataAnnotations.Schema.ColumnAttribute)columnMappings.FirstOrDefault()).Name);
                }
                
            }                        
        }
    }
}
