using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Complex.Common.Cache;
using Complex.EFRepository.Repository;
using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Complex.Common.Utility.Extensions;
using Complex.Logical.Admin.CacheManag;
namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RUserInfo")]
    public class RUserInfo : EFRepositoryBaseGeneric, IUserInfo
    {
        public RUserInfo()
            : base("MySQLServerContext")
        {
        }
        public T_UserInfo GetUserInfo(T_User User)
        {
            T_UserInfo model;
            model = CacheManagement.Instance.Get<T_UserInfo>("T_UserInfo", User.ID); ;
            if ( model == null )
            {
                model = new T_UserInfo();
                List<int> t_userrolesidls = GetAllNoCache<T_UserRoles>().Where(p => p.UserID == User.ID).Select(p => p.RoleID).ToList();

                List<T_RoleNavBtns> t_rolenavbtnsls = GetAllNoCache<T_RoleNavBtns>().Where(p => t_userrolesidls.Contains(p.RoleID)).ToList();

                List<int> navidls = t_rolenavbtnsls.Select(p => p.NavID).Distinct().ToList();

                var allbut = GetAllNoCache<T_Button>().ToList();
                model.T_User = User;
                List<T_Navigation> T_Navigationls = new List<T_Navigation>();
                foreach ( var item in navidls )
                {
                    var nav = GetAllNoCache<T_Navigation>().Where(p => p.ID == item && p.IsDelete != true && p.IsVisible == true).FirstOrDefault();
                    if ( nav != null )
                    {
                        var btnidls = t_rolenavbtnsls.Where(p => p.NavID == item && p.IsDelete != true).Select(p => p.BtnID);
                        // var navbtnsls = GetAllNoCache<T_NavButtons>().Where(p => p.NavId == item && p.IsDelete != true).Select(p => p.ButtonId);

                        nav.ButtonHtmlList = allbut.Where(p => btnidls.Contains(p.ID) && p.IsDelete != true && p.ButtonTag != "browser").Select(p => p.ButtonHtml).ToList();
                        // nav.AllButtonHtmlList = allbut.Where(p => navbtnsls.Contains(p.ID) && p.IsDelete != true).Select(p => p.ButtonHtml).ToList();
                    }
                    T_Navigationls.Add(nav);
                }

                List<T_Navigation> AllT_Navigationls = GetAllNoCache<T_Navigation>().Where(p => p.IsDelete != true && p.IsVisible == true).ToList().Select(
                (p) =>
                {
                    var navbtnsls = GetAllNoCache<T_NavButtons>().Where(z => z.NavId == p.ID && z.IsDelete != true).Select(z => z.ButtonId);
                    T_Navigation navmodel = p;
                    navmodel.AllButtonHtmlList = allbut.Where(z => navbtnsls.Contains(z.ID) && z.IsDelete != true && z.ButtonTag != "browser").Select(z => z.ButtonHtml).ToList();
                    return navmodel;
                    //return new T_Navigation { ID = p.ID, AllButtonHtmlList = allbut.Where(z => navbtnsls.Contains(z.ID) && z.IsDelete != true).Select(z => z.ButtonHtml).ToList(), Sortnum=p.Sortnum , ParentID=p.ParentID , iconUrl =p.iconUrl , Linkurl =p.Linkurl , iconCls =p.iconCls , NavTitle=p.NavTitle , BigImageUrl=p.BigImageUrl , ButtonHtmlList =p.ButtonHtmlList, IsDelete=p.IsDelete ,IsSys=p.IsSys , IsVisible=p.IsVisible , NavTag=p.NavTag , OwnedBut=p.OwnedBut , children=p.children  };
                }).ToList();
                model.AllT_Navigationls = AllT_Navigationls;

                model.T_Navigationls = T_Navigationls;
                //    List<int> btnsidls = t_rolenavbtnsls.Select(p => p.BtnID).ToList();
                 
                CacheManagement.Instance.Add("T_UserInfo", User.ID, model);
            }
            return model;
        }
        public T_UserInfo GetUserInfoByID(int ID)
        {
            T_UserInfo model;
            model = CacheManagement.Instance.Get<T_UserInfo>("T_UserInfo", ID); 
            if ( model!=null )
            {
                return GetUserInfo(model.T_User);
            }
            return GetUserInfo(GetAllNoCache<T_User>().Where(p => p.ID == ID).FirstOrDefault());
        }
        public T_UserInfo GetUserInfoBySession()
        {
            var Session = HttpContext.Current.Session;
            var Cookies = HttpContext.Current.Request.Cookies;
            if ( Session["userinfo"] is T_UserInfo )
            {
                return (T_UserInfo) Session["userinfo"];
            }
            if ( Cookies["user"] != null && Session["userinfo"] == null )
            {
                Session["userinfo"] = GetUserInfoByID(Cookies["user"].Values["userid"].ReferenceFromType<string, int>());
            }
            return null; 
        }

      //  HttpContext.Current.Session[SessionUserIdKey]
    }
}
