using MongoDB.Configuration.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Mongodb.Map
{
    //映射公共类
    public class BaseMapping
    {
        //泛型委托 参数为 MappingStoreBuilder 类型
        List<Action<MappingStoreBuilder>> ActionList = new List<Action<MappingStoreBuilder>>();
        //遍历执行已经注册的映射方法
        public void ForeachMapping(MappingStoreBuilder obj)
        {
            foreach (var item in ActionList)
            {
                item(obj);
            }
        }
        //注册映射方法
        public void AddMapping(Action<MappingStoreBuilder> actionmap)
        {
            ActionList.Add(actionmap);
        }
    }
}
