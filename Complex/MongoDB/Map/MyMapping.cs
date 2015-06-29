using Complex.Mongodb.Entity;
using MongoDB.Configuration.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Mongodb.Map
{
    //个性映射
    public class MyMapping
    {
        //映射方法
        public void Mapping(MappingStoreBuilder mapping)
        {


          


            mapping.Map<Log>();
            
            mapping.DefaultProfile(p=>p.SubClassesAre(t=>t.IsSubclassOf(typeof(Log))));
            //mapping.Map<SubClass>();
            //mapping.DefaultProfile(profile =>
            //{
            //    profile.SubClassesAre(t => t.IsSubclassOf(typeof(MyClass)));
            //});
        }
    }
     
}
