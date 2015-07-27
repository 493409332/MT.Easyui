using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Complex.Common.Enumspace;
using Complex.Entity.Admin;
using Complex.Logical.Admin.Realization;
using Complex.Mongodb.Entity;
using Complex.Mongodb.Utility;
using MongoDB;
using MtAop.Context;

namespace Complex.Logical.Admin.AopAttribute
{
    public class LogAttribute : T_UserInfoCacheAttribute
    {
        public override InvokeContext Action(InvokeContext context)
        {
            context = base.Action(context);
            if ( context.MethodName == "Add" || context.MethodName == "Edit" || context.MethodName == "Remove" || context.MethodName == "TrueRemove" || context.MethodName == "SetUseConfigrByKey" || context.MethodName == "AddUserTo" || context.MethodName == "setRoleButtons" || context.MethodName == "setButtons" )
            { 
                var Session = HttpContext.Current.Session;

                T_UserInfo userinfo = new RUserInfo().GetUserInfoBySession();
                
                Document Dmodel = new Document();
                Log logmodel = new Log();

                OperationType optype = new OperationType();
                
                List<string> liststr = userinfo.T_Rolels.Select(p => p.RoleName).ToList();
                string Rolelist = string.Join("|", liststr);
                var quer = context.Parameters.FirstOrDefault();
                switch ( context.MethodName )
                {
                    case "Add":
                    case "Edit":
                        optype = OperationType.Insert;
                        if ( context.MethodName == "Edit" )
                        {
                            optype = OperationType.Update;
                        }
                        if ( quer != null )
                        {
                            foreach ( var item in quer.GetType().GetProperties() )
                            {
                                var value = item.GetValue(quer, null);
                                string strval = value != null ? value.ToString() : "";
                                Dmodel.Add(item.Name, strval);
                            }
                        }
                        logmodel = new Log() { Model = Dmodel, MethodName=context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist };

                        break;
                    case "Remove":
                        optype = OperationType.Delete;
                        Dmodel.Add("ID", context.Parameters[0] != null ? context.Parameters[0].ToString() : "");
                        logmodel = new Log() { Model = Dmodel, MethodName = context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist };

                        break;
                    case "TrueRemove":
                        optype = OperationType.TrueDelete;
                        Dmodel.Add("ID", context.Parameters[0] != null ? context.Parameters[0].ToString() : "");
                        logmodel = new Log() { Model = Dmodel, MethodName = context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist };

                        break;
                    case "SetUseConfigrByKey":
                        optype = OperationType.UseConfigrByKey;

                        Dmodel.Add("ID", context.Parameters[0] != null ? context.Parameters[0].ToString() : "");
                        Dmodel.Add("ConfigJson", context.Parameters[1] != null ? context.Parameters[1].ToString() : "");
                        logmodel = new Log() { Model = Dmodel, MethodName = context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist };
                        break;
                    case "AddUserTo":
                        optype = OperationType.UserRolesUpdate;

                        Dmodel.Add("ID", context.Parameters[0] != null ? context.Parameters[0].ToString() : "");
                        try
                        {
                            int[] roleids = (int[]) context.Parameters[1];
                            Dmodel.Add("roleids", roleids.Length > 1 ? string.Join(",", roleids) : roleids[0].ToString());
                        }
                        catch ( Exception )
                        { 
                            Dmodel.Add("roleids",  "日志存储异常");
                        }
                       
                        logmodel = new Log() { Model = Dmodel, MethodName = context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist }; 
                        break;
                    case "setRoleButtons":
                        optype = OperationType.RoleNavButtons;

                     //   Dmodel.Add("ID", context.Parameters[0] != null ? context.Parameters[0].ToString() : ""); 
                        Dmodel.Add("Data", context.Parameters[0] != null ? context.Parameters[0].ToString() : "");

                        logmodel = new Log() { Model = Dmodel, MethodName = context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist };
                        break;
                    case "setButtons":
                        optype = OperationType.NavButtons;

                        Dmodel.Add("ID", context.Parameters[0] != null ? context.Parameters[0].ToString() : "");
                        try
                        {
                            int[] roleids = (int[]) context.Parameters[1];
                            Dmodel.Add("btns", roleids.Length > 1 ? string.Join(",", roleids) : roleids[0].ToString());
                        }
                        catch ( Exception )
                        {
                            Dmodel.Add("btns", "日志存储异常");
                        }
                        logmodel = new Log() { Model = Dmodel, MethodName = context.MethodName, RunClassName = context.ClassFullName.ToString(), OperationTime = DateTime.Now, OperationType = optype, UserID = userinfo.T_User.ID, SaveChangesint = context.Result.ToString(), UserName = userinfo.T_User.TrueName != null ? userinfo.T_User.TrueName.Trim() : userinfo.T_User.UserName.Trim(), PurviewName = Rolelist };
                        break;

                }
                using ( MongoDBUtility db = new MongoDBUtility() )
                {
                    db.GetIMongoCollection<Log>().Insert(logmodel);
                }
            }
           
            return context;
        }
    }
}
