using Complex.Mongodb.Map;
using MongoDB;
using MongoDB.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Mongodb.Utility
{
    /// <summary>
    /// MongoDB公用类
    /// </summary>
    public class MongoDBUtility : IDisposable
    {
        //mongodb-csharp Mongo核心类
        private Mongo mongo;
        //mongodb-csharp  IMongoDatabase 数据库接口
        private IMongoDatabase simple;
        //类似EF DbSet 直接操作数据库对应的实体类对应的表
        public IMongoCollection<T> GetIMongoCollection<T>() where T : class
        {
            return simple.GetCollection<T>();
        }
        //无参数构造函数继承下面构造
        public MongoDBUtility()
            : this(ConfigurationManager.ConnectionStrings["MongoContext"].ConnectionString, ConfigurationManager.ConnectionStrings["MongoDatabase"].ConnectionString)
        {

        }
        //构造
        public MongoDBUtility(string ConnString, string databaseName)
        {
            //根据 MongoConfiguration 实例化一个 mongodb-csharp Mongo核心类
            mongo = new Mongo(InitMongoBuilder(ConnString));
            //打开数据库连接
            mongo.Connect();
            try
            {
                //根据指定字符串获取mongodb对应的数据库
                simple = mongo.GetDatabase(databaseName);
            }
            catch (Exception e)
            {
                //异常 释放当前对象
                Dispose();
                throw e;
            }
        }
        /// <summary>
        /// IDisposabl 接口的实现 用于释放资源
        /// </summary>
        public void Dispose()
        {
            mongo.Disconnect();
            if (mongo != null)
            {
                mongo.Dispose();
            }
        }
        //  为Mongo 提供 MongoConfiguration
        public MongoConfiguration InitMongoBuilder(string ConnString)
        {
            var config = new MongoConfigurationBuilder();
            //加入连接字符串
            config.ConnectionString(ConnString);
            //映射公共类
            BaseMapping mapping = new BaseMapping();
            //向映射公共类注册自己定义映射
            mapping.AddMapping(new MyMapping().Mapping);
            //传入公共类的执行方法
            config.Mapping(mapping.ForeachMapping);
            return config.BuildConfiguration();

        }


        //public IMongoCollection<MyClass> categories
        //{
        //    get {
        //        return simple.GetCollection<MyClass>();
        //    }
        //}

    }
}
