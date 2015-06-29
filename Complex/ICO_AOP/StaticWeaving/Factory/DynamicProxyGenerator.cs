using MtAop.BaseAttribute;
using MtAop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using Complex.ICO_AOP.Attribute;


namespace MtAop.Factory
{
    public class DynamicProxyGenerator
    {

        const string assemblyname = "MtAop.Dynamic{0}Assembly";

        const string AssemblyFileName = "MtAop.Dynamic{0}Assembly.dll";

        const string ModuleName = "MtAop.Dynamic{0}Module";
        
        const string TypeNameFormat = "MtAop.Dynamic{0}Type";

        private Type _realProxyType;
        private Type _interfaceType;
        private AssemblyBuilder _assemblyBuilder;
        private ModuleBuilder _moduleBuilder;
        private TypeBuilder _typeBuilder;
        private FieldBuilder _realProxyField;

       // private FieldBuilder _dbContextTransaction;
        /// <summary>
        /// 父类类型
        /// </summary>
     //   public Type _baseType=null;
        /// <summary>
        /// 是否开启事务
        /// </summary>
       // bool TransactionEnable;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="realProxyType"></param>
        /// <param name="interfaceType"></param>
        public DynamicProxyGenerator(Type realProxyType, Type interfaceType)
        {
            _realProxyType = realProxyType;
            _interfaceType = interfaceType;

           // TransactionEnable = transactionEnable;
          //  if ( baseType != null )
               // this._baseType = baseType; 
        }
 
        //System.Data.Entity.DbContextTransaction Dt;
        //Dt = EF.Database.BeginTransaction();
        //Dt.Commit();
        //Dt.Rollback();

        public Type GenerateType()
        {
            // 构造程序集
            BuildAssembly();
            // 构造模块
            BuildModule();
            // 构造类型
            BuildType();
            // 构造字段
            BuildField();
            // 构造函数
            BuildConstructor();
            // 构造方法
            BuildMethods();

            Type type = _typeBuilder.CreateType();
            // 将新建的类型保存在硬盘上（如果每次都动态生成，此步骤可省略）
             _assemblyBuilder.Save(string.Format(AssemblyFileName, _realProxyType.Name));
            return type;
        }


        // 构造程序集
        void BuildAssembly()
        {
            // 程序集名字
            AssemblyName assemblyName = new AssemblyName(string.Format(assemblyname, _realProxyType.Name));

            // 在当前的AppDomain中构造程序集
            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly( assemblyName,
                AssemblyBuilderAccess.RunAndSave, System.AppDomain.CurrentDomain.BaseDirectory);
        }
        // 构造模块
        void BuildModule()
        {
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(string.Format(ModuleName, _realProxyType.Name), string.Format(AssemblyFileName, _realProxyType.Name));
        }
        // 构造类型
        void BuildType()
        {
            _typeBuilder = _moduleBuilder.DefineType(string.Format(TypeNameFormat, _realProxyType.Name),
                TypeAttributes.Public | TypeAttributes.Sealed);
            _typeBuilder.AddInterfaceImplementation(_interfaceType);

            //if ( _baseType!=null)
            //    _typeBuilder.SetParent(_baseType);
         
        }
        // 构造字段
        void BuildField()
        {
            _realProxyField = _typeBuilder.DefineField("_realProxy", _realProxyType, FieldAttributes.Private);
            _realProxyField.SetConstant(null);
            //if ( _baseType!=null )
            //{
            //    _dbContextTransaction = _typeBuilder.DefineField("_dbContextTransaction", typeof(System.Data.Entity.DbContextTransaction), FieldAttributes.Private);
            //    _dbContextTransaction.SetConstant(null);
            //}
        }

        // 构造函数
        void BuildConstructor()
        {
            ConstructorBuilder constructorBuilder = _typeBuilder.DefineConstructor(MethodAttributes.Public,
                CallingConventions.HasThis, null);
            ILGenerator generator = constructorBuilder.GetILGenerator();



            generator.Emit(OpCodes.Ldarg_0);

            // _realProxy = new RealProxy();
       
            ConstructorInfo defaultConstructorInfo = _realProxyType.GetConstructor(Type.EmptyTypes);
            generator.Emit(OpCodes.Newobj, defaultConstructorInfo);
            generator.Emit(OpCodes.Stfld, _realProxyField);

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, _typeBuilder.BaseType.GetConstructors()[0]);
            generator.Emit(OpCodes.Nop); generator.Emit(OpCodes.Nop);

            generator.Emit(OpCodes.Ret);
        }
        /// <summary>
        /// 遍历对象中所有方法
        /// </summary>
        void BuildMethods()
        {
            MethodInfo[] methodInfos = _realProxyType.GetMethods(System.Reflection.BindingFlags.Public | BindingFlags.Instance  | System.Reflection.BindingFlags.DeclaredOnly
);
            MethodInfo[] openbasemethodInfos = _realProxyType.GetMethods(System.Reflection.BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttributes(typeof(OpenBaseAttribute)).Count()>0).ToArray();
          
            //var methodInfosquer = methodInfos.Where(p =>!new string[] { "Equals", "GetHashCode", "GetType", "ToString" }.Contains(p.Name));
           
           MethodInfo[] quer = methodInfos.Concat(openbasemethodInfos).Distinct().ToArray();
           foreach ( MethodInfo methodInfo in quer)
            {
                BuildMethod(methodInfo);
            }
        }
        /// <summary>
        /// 动态生成方法
        /// </summary>
        /// <param name="methodInfo"></param>
        void BuildMethod(MethodInfo methodInfo)
        {

            string methodName = methodInfo.Name;
            
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            Type returnType = methodInfo.ReturnType;

            MethodBuilder methodBuilder = _typeBuilder.DefineMethod(methodName,
                MethodAttributes.Public | MethodAttributes.Virtual,
                returnType, parameterInfos.Select(pi => pi.ParameterType).ToArray());

            var generator = methodBuilder.GetILGenerator();

            Label castPreSuccess = generator.DefineLabel();
            Label castPreSuccess1 = generator.DefineLabel();
            Label castPostSuccess = generator.DefineLabel();
            Label castExSuccess = generator.DefineLabel();
            Label castRetSuccess = generator.DefineLabel();


            #region 事务实例化
         //      ICO_AOPEnableAttribute ia = (ICO_AOPEnableAttribute) iType.GetCustomAttribute(typeof(ICO_AOPEnableAttribute), false);
            AOPTransactionAttribute AOPConfig = (AOPTransactionAttribute) methodInfo.GetCustomAttribute(typeof(AOPTransactionAttribute), false);
            MethodInfo openTransaction = this._realProxyType.GetMethod("OpenTransaction", new Type[] { }, null);
            if ( AOPConfig != null && AOPConfig.TransactionEnable && _realProxyType.BaseType != null && openTransaction!=null ) 
            {
                  generator.Emit(OpCodes.Ldarg_0);
                  generator.Emit(OpCodes.Ldfld, _realProxyField);
                
                  generator.Emit(OpCodes.Callvirt, openTransaction);
 

            }


            //if ( _baseType != null )
            //{
                 
            //    generator.Emit(OpCodes.Ldarg_0);
            //    generator.Emit(OpCodes.Ldstr, "OraString");
            //    MethodInfo oraString = this._baseType.GetMethod("ChangeDatabase", new Type[] { typeof(string) }, null);

            //    generator.Emit(OpCodes.Call, oraString);
               
            //     generator.Emit(OpCodes.Ldarg_0);
            //    generator.Emit(OpCodes.Ldarg_0);
 
            //    generator.Emit(OpCodes.Ldfld, _typeBuilder.BaseType.GetField("EF"));

            //    PropertyInfo efDatabase = typeof(System.Data.Entity.DbContext).GetProperty("Database");

            //    generator.Emit(OpCodes.Callvirt, efDatabase.GetMethod);

            //    MethodInfo beginTransaction = typeof(System.Data.Entity.Database).GetMethod("BeginTransaction", new Type[] { }, null);
            //    generator.Emit(OpCodes.Callvirt, beginTransaction);
            //    generator.Emit(OpCodes.Stfld, _dbContextTransaction);
             
            //}


            #endregion

            #region 实例化 InvokeContext

            Type contextType = typeof(InvokeContext);
            //所有元数据
            var contextLocal = generator.DeclareLocal(contextType);
            generator.Emit(OpCodes.Newobj, contextType.GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, contextLocal);

            //设置方法名
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Ldstr, methodName);
            generator.Emit(OpCodes.Call, contextType.GetMethod("SetMethod", BindingFlags.Public | BindingFlags.Instance));
           
            
            //设置类名
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Ldstr, _realProxyType.FullName);
            generator.Emit(OpCodes.Call, contextType.GetMethod("SetClassName", BindingFlags.Public | BindingFlags.Instance));

            //设置返回类型 
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Ldtoken, methodInfo.ReturnType);
            generator.Emit(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(System.RuntimeTypeHandle) }));
            PropertyInfo ResultType = contextType.GetProperty("ResultType");
            generator.Emit(OpCodes.Callvirt, ResultType.SetMethod);
   
            #endregion




            #region 声明  result 初始化

            LocalBuilder resultLocal = null;

            if (returnType != typeof(void))
            {
                resultLocal = generator.DeclareLocal(returnType);
                if (returnType.IsValueType)
                {
                    generator.Emit(OpCodes.Ldstr, returnType.FullName);
                    generator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetType", new Type[] { typeof(string) }));
                    generator.Emit(OpCodes.Call, typeof(Activator).GetMethod("CreateInstance", new Type[] { typeof(Type) }));
                }
                else
                {
                    generator.Emit(OpCodes.Ldnull);
                }

                generator.Emit(OpCodes.Stloc, resultLocal);
            }

            #endregion

            #region 声明 Exception 初始化

            var exceptionLocal = generator.DeclareLocal(typeof(Exception));
            generator.Emit(OpCodes.Ldnull);
            generator.Emit(OpCodes.Stloc, exceptionLocal);
            #endregion

            #region Invoke PreInvoke 方法执行前

            #region Set parameter to InvkeContext  为参数元数据特性添加参数


            for (int i = 1; i <= parameterInfos.Length; i++)
            {
                generator.Emit(OpCodes.Ldloc, contextLocal);
                generator.Emit(OpCodes.Ldarg, i);
                if (parameterInfos[i - 1].ParameterType.IsValueType)
                {
                    generator.Emit(OpCodes.Box, parameterInfos[i - 1].ParameterType);
                }
#warning out 参数作为指针传送无法中途拦截赋值 必须在方法中实现赋值目前有没有找到有效解决方案
                //if ( parameterInfos[i - 1].ParameterType.Name.Contains("&") )
                //{
                //    generator.Emit(OpCodes.Box, Type.GetType( parameterInfos[i - 1].ParameterType.FullName.Replace("&","")));
                //    generator.Emit(OpCodes.Unbox, Type.GetType(parameterInfos[i - 1].ParameterType.FullName.Replace("&", "")));
                //}
                generator.Emit(OpCodes.Call, contextType.GetMethod("SetParameter", BindingFlags.Public | BindingFlags.Instance));
            }

            #endregion

            /*
             * C# 代码
             * MethodInfo methodInfoLocal = _realProxyField.GetType().GetMethod("methodName");
             * PreAspectAttribute preAspectLocal = 
             *      (PreAspectAttribute)Attribute.GetCustomAttribute(methodInfoLocal, typeof(PreAspectAttribute))
             */
            var methodInfoLocal = generator.DeclareLocal(typeof(System.Reflection.MethodInfo));
            var preAspectLocal = generator.DeclareLocal(typeof(PreAspectAttribute));

           // var boolLocal = generator.DeclareLocal(typeof(bool));
         //   Label castboolSuccess = generator.DefineLabel();


         //   generator.Emit(OpCodes.Ldc_I4_1);
          //  generator.Emit(OpCodes.Stloc, boolLocal);

 

          
            var Typeparmeter = generator.DeclareLocal(typeof(Type[]));

            generator.Emit(OpCodes.Ldc_I4, parameterInfos.Length);
            generator.Emit(OpCodes.Newarr, typeof(Type));
            generator.Emit(OpCodes.Stloc, Typeparmeter);

            for ( int i = 0; i < parameterInfos.Length; i++ )
            {
                generator.Emit(OpCodes.Ldloc, Typeparmeter);
                generator.Emit(OpCodes.Ldc_I4, i);
                //TypeToken typetoken = new TypeToken();
               
                if ( parameterInfos[i].ParameterType.Name.Contains("&") )
                {
                    string typstr = parameterInfos[i].ParameterType.FullName.Replace("&", "");
                    Type typetem = Type.GetType(typstr);
                    generator.Emit(OpCodes.Ldtoken, typetem); 
                }
                else
                {
                    generator.Emit(OpCodes.Ldtoken, parameterInfos[i].ParameterType);
                } 

                generator.Emit(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(System.RuntimeTypeHandle) }));
                if ( parameterInfos[i].ParameterType.Name.Contains("&") )
                {
                    generator.Emit(OpCodes.Callvirt, typeof(System.Type).GetMethod("MakeByRefType"));
                }
                generator.Emit(OpCodes.Stelem_Ref);
            }

             
       

            //相当于this
            generator.Emit(OpCodes.Ldarg_0);
            //构造中实例化的对象
            generator.Emit(OpCodes.Ldfld, _realProxyField);
  

            generator.Emit(OpCodes.Callvirt, typeof(System.Object).GetMethod("GetType", BindingFlags.Public | BindingFlags.Instance));

            generator.Emit(OpCodes.Ldstr, methodName); 
            generator.Emit(OpCodes.Ldloc, Typeparmeter);

            generator.Emit(OpCodes.Callvirt,
                typeof(System.Type).GetMethod("GetMethod", new Type[] { typeof(string),typeof(Type[]) }));
            generator.Emit(OpCodes.Stloc, methodInfoLocal);
            generator.Emit(OpCodes.Ldloc, methodInfoLocal);
            //typeof(PreAspectAttribute)
            generator.Emit(OpCodes.Ldtoken, typeof(PreAspectAttribute));
            generator.Emit(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(System.RuntimeTypeHandle) }));
            //Attribute.GetCustomAttribute(methodInfoLocal, typeof(PreAspectAttribute))
            generator.Emit(OpCodes.Call, typeof(System.Attribute).GetMethod("GetCustomAttribute", new Type[] { typeof(System.Reflection.MethodInfo), typeof(System.Type) }));
            //(PreAspectAttribute)
            generator.Emit(OpCodes.Castclass, typeof(PreAspectAttribute));
            generator.Emit(OpCodes.Stloc, preAspectLocal);

            

            /*  * if (preAspectLocal != null)
           * {
           *      preAspectLocal.Action(contextLocal);
           * }
           *  */
            generator.Emit(OpCodes.Ldloc, preAspectLocal);
            generator.Emit(OpCodes.Ldnull);
            generator.Emit(OpCodes.Ceq);
            generator.Emit(OpCodes.Brtrue_S, castPreSuccess);

            generator.Emit(OpCodes.Ldloc, preAspectLocal);
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Callvirt,
                typeof(AspectAttribute).GetMethod("Action", new Type[]{ typeof(InvokeContext) }));

            generator.Emit(OpCodes.Stloc, contextLocal);


            generator.MarkLabel(castPreSuccess);
          
           // generator.Emit(OpCodes.Ldloc, boolLocal);
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Callvirt, contextType.GetProperty("IsRun").GetMethod);
            generator.Emit(OpCodes.Ldc_I4_1);
            generator.Emit(OpCodes.Ceq);
            generator.Emit(OpCodes.Brtrue_S, castPreSuccess1); 

            if (returnType != typeof(void))
            {
               
                if (returnType.IsValueType)
                {
                    generator.Emit(OpCodes.Ldstr, returnType.FullName);
                    generator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetType", new Type[] { typeof(string) }));
                    generator.Emit(OpCodes.Call, typeof(Activator).GetMethod("CreateInstance", new Type[] { typeof(Type) })); 
                }
                else
                {
                    generator.Emit(OpCodes.Ldnull);
                } 
                generator.Emit(OpCodes.Stloc, resultLocal);
                generator.Emit(OpCodes.Ldloc, resultLocal);
            }

             
             generator.Emit(OpCodes.Ret); 

           generator.MarkLabel(castPreSuccess1);


            #endregion

            #region Begin Exception Block    开始异常捕获
            //Try
            Label exLbl = generator.BeginExceptionBlock();

            #endregion

            #region Invoke  反射原方法

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, _realProxyField);
            for (int i = 1; i <= parameterInfos.Length; i++)
            {
             
                generator.Emit(OpCodes.Ldloc, contextLocal);
                generator.Emit(OpCodes.Callvirt, contextType.GetProperty("Parameters").GetMethod);
                generator.Emit(OpCodes.Ldc_I4, i - 1);
                generator.Emit(OpCodes.Callvirt, typeof(List<object>).GetMethod("get_Item"));
                if ( parameterInfos[i - 1].ParameterType.IsValueType )
                {
                    generator.Emit(OpCodes.Unbox_Any, parameterInfos[i - 1].ParameterType);
                }
              
            }
            Type[] parameterInfostypes = new Type[parameterInfos.Length];

            for ( int i = 0; i < parameterInfos.Length; i++ )
            {
                parameterInfostypes[i] = parameterInfos[i].ParameterType;
            }
           
            generator.Emit(OpCodes.Call, _realProxyType.GetMethod(methodName,BindingFlags.ExactBinding| BindingFlags.Public | BindingFlags.Instance, null, parameterInfostypes, null));
            if (typeof(void) != returnType)
            {
                generator.Emit(OpCodes.Stloc, resultLocal);
            }

            #endregion

            #region Invoke PostInovke 原方法执行完

            #region Set result to InvkeContext 设置返回元数据

            generator.Emit(OpCodes.Ldloc, contextLocal);
            // load parameter
            if (typeof(void) != returnType)
            {
                generator.Emit(OpCodes.Ldloc, resultLocal);
                if (returnType.IsValueType)
                {
                    generator.Emit(OpCodes.Box, returnType);
                }
            }
            else
            {
                generator.Emit(OpCodes.Ldnull);
            }
            generator.Emit(OpCodes.Call, contextType.GetMethod("SetResult", BindingFlags.Public | BindingFlags.Instance));

            #endregion



            var postAspectLocal = generator.DeclareLocal(typeof(PostAspectAttribute));


            //  PostAspectAttribute attribute2 = (PostAspectAttribute) Attribute.GetCustomAttribute(method, typeof(PostAspectAttribute)); 
            generator.Emit(OpCodes.Ldloc, methodInfoLocal);
            generator.Emit(OpCodes.Ldtoken, typeof(PostAspectAttribute));
            generator.Emit(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(System.RuntimeTypeHandle) }));
            generator.Emit(OpCodes.Call, typeof(System.Attribute).GetMethod("GetCustomAttribute", new Type[] { typeof(System.Reflection.MethodInfo), typeof(System.Type) }));
            generator.Emit(OpCodes.Castclass, typeof(PostAspectAttribute));
            generator.Emit(OpCodes.Stloc, postAspectLocal);
            //     if (attribute2 != null)
            //{
            //    attribute2.Action(context);
            //} 
            generator.Emit(OpCodes.Ldloc, postAspectLocal);
            generator.Emit(OpCodes.Ldnull);
            generator.Emit(OpCodes.Ceq);
            generator.Emit(OpCodes.Brtrue_S, castPostSuccess);
            generator.Emit(OpCodes.Ldloc, postAspectLocal);
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Callvirt, typeof(AspectAttribute).GetMethod("Action", new Type[] { typeof(InvokeContext) }));
            generator.Emit(OpCodes.Stloc, contextLocal);

            //获取拦截的返回值


  //            IL_0021:  ldloc.1
  //IL_0022:  callvirt   instance object MtAop.Context.InvokeContext::get_Result()
  //IL_0027:  call       valuetype [mscorlib]System.Decimal [mscorlib]System.Convert::ToDecimal(object)
            MethodInfo convertMethodInfo = null;
            Type convertType = null;
            PropertyInfo Result = contextType.GetProperty("Result");

            if ( methodInfo.ReturnType != typeof(void) )
            {
               


                if ( methodInfo.ReturnType.IsGenericType && methodInfo.ReturnType.IsInterface )
                {
                    convertType = typeof(List<>).MakeGenericType(methodInfo.ReturnType.GetGenericArguments());
                }
                //else if ( ( methodInfo.ReturnType.IsGenericType && !methodInfo.ReturnType.IsInterface ) || methodInfo.ReturnType.IsClass ||)
                else if ( !methodInfo.ReturnType.IsValueType )
                {
                    convertType = methodInfo.ReturnType;
                }
                else
                {
                    convertMethodInfo = typeof(System.Convert).GetMethod("To" + methodInfo.ReturnType.Name, new Type[] { typeof(object) });
                }
                // methodInfo.ReturnType.IsValueType

                generator.Emit(OpCodes.Ldloc, contextLocal);
               
                generator.Emit(OpCodes.Callvirt, Result.GetMethod);


                if ( !methodInfo.ReturnType.IsValueType )
                {
                    generator.Emit(OpCodes.Castclass, convertType);
                }
                else
                {
                    generator.Emit(OpCodes.Call, convertMethodInfo);
                }

                generator.Emit(OpCodes.Stloc, resultLocal);
            }


            generator.MarkLabel(castPostSuccess);
            if ( AOPConfig != null && AOPConfig.TransactionEnable && _realProxyType.BaseType != null && openTransaction != null ) 
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldfld, _realProxyField);
                MethodInfo CommitTransaction = this._realProxyType.GetMethod("Commit", new Type[] { }, null);
                generator.Emit(OpCodes.Callvirt, CommitTransaction);
            }
            #endregion

            #region Catch Block 出现异常处理块

            generator.BeginCatchBlock(typeof(Exception));



            if ( AOPConfig != null && AOPConfig.TransactionEnable && _realProxyType.BaseType != null && openTransaction != null ) 
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldfld, _realProxyField);
                MethodInfo RollbackTransaction = this._realProxyType.GetMethod("Rollback", new Type[] { }, null);
                generator.Emit(OpCodes.Callvirt, RollbackTransaction);
            }

            //if ( _baseType != null )
            //{
            //    generator.Emit(OpCodes.Ldarg_0);  //this
            //    generator.Emit(OpCodes.Ldfld, _dbContextTransaction);
            //    MethodInfo rollbackTransaction = typeof(System.Data.Entity.DbContextTransaction).GetMethod("Rollback", new Type[] { }, null);
            //    generator.Emit(OpCodes.Callvirt, rollbackTransaction);

            //}

            //e = exception1;
            //context.SetError(e); 
            generator.Emit(OpCodes.Stloc, exceptionLocal);
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Ldloc, exceptionLocal);
            generator.Emit(OpCodes.Call, contextType.GetMethod("SetError", BindingFlags.Public | BindingFlags.Instance));


            var exAspectLocal = generator.DeclareLocal(typeof(ExceptionAspectAttribute));

            // PostAspectAttribute attribute3 = (PostAspectAttribute) ((ExceptionAspectAttribute) Attribute.GetCustomAttribute(method, typeof(ExceptionAspectAttribute))); 
            generator.Emit(OpCodes.Ldloc, methodInfoLocal);
            generator.Emit(OpCodes.Ldtoken, typeof(ExceptionAspectAttribute));
            generator.Emit(OpCodes.Call, typeof(System.Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(System.RuntimeTypeHandle) }));
            generator.Emit(OpCodes.Call, typeof(System.Attribute).GetMethod("GetCustomAttribute", new Type[] { typeof(System.Reflection.MethodInfo), typeof(System.Type) }));
            generator.Emit(OpCodes.Castclass, typeof(ExceptionAspectAttribute));
            generator.Emit(OpCodes.Stloc, exAspectLocal);
            //if (attribute3 != null)
            //{
            //    attribute3.Action(context);
            //} 
            generator.Emit(OpCodes.Ldloc, exAspectLocal);
            generator.Emit(OpCodes.Ldnull);
            generator.Emit(OpCodes.Ceq);
            generator.Emit(OpCodes.Brtrue_S, castExSuccess);
            generator.Emit(OpCodes.Ldloc, exAspectLocal);
            generator.Emit(OpCodes.Ldloc, contextLocal);
            generator.Emit(OpCodes.Callvirt, typeof(AspectAttribute).GetMethod("Action", new Type[] { typeof(InvokeContext) }));
            generator.Emit(OpCodes.Stloc, contextLocal);
            //获取拦截的返回值


            if ( methodInfo.ReturnType != typeof(void) )
            {

                generator.Emit(OpCodes.Ldloc, contextLocal);
                // PropertyInfo ResultType = contextType.GetProperty("ResultType");
                generator.Emit(OpCodes.Callvirt, Result.GetMethod);

                if ( !methodInfo.ReturnType.IsValueType )
                {
                    generator.Emit(OpCodes.Castclass, convertType);
                }
                else
                {
                    generator.Emit(OpCodes.Call, convertMethodInfo);
                }

                generator.Emit(OpCodes.Stloc, resultLocal);

            }

            generator.Emit(OpCodes.Leave_S, castRetSuccess); 
            generator.MarkLabel(castExSuccess); 
            generator.Emit(OpCodes.Ldloc, exceptionLocal);
            generator.Emit(OpCodes.Throw);


            #endregion

            #region End Exception Block

            generator.EndExceptionBlock();

            #endregion
            
            generator.MarkLabel(castRetSuccess);
            //if ( _baseType != null )
            //{
            //    generator.Emit(OpCodes.Ldarg_0);
            //    generator.Emit(OpCodes.Ldfld, _dbContextTransaction);
            //    MethodInfo commitTransaction = typeof(System.Data.Entity.DbContextTransaction).GetMethod("Commit", new Type[] { }, null);
            //    generator.Emit(OpCodes.Callvirt, commitTransaction);
            //}

           

            if (typeof(void) != returnType)
            {
                generator.Emit(OpCodes.Ldloc, resultLocal);
            } 
           
 
          
            generator.Emit(OpCodes.Ret);
        }
    }
}
