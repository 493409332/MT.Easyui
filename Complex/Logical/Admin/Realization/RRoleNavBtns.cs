using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Newtonsoft.Json.Linq;
using Complex.Common.Utility.Extensions;
namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RRoleNavBtns")]
    public class RRoleNavBtns : RBase<T_RoleNavBtns>, IRoleNavBtns
    {
        public RRoleNavBtns()
            : base("MySQLServerContext")
        {

        }
       #region IRoleNavButtons 成员
        public List<T_RoleNavBtns> GetRoleNavBtns()
        {
            return GetAllNoCache().Where(p=>p.IsDelete == false).ToList();
        }

        public List<T_RoleNavBtns> GetRoleNavBtnsByRole(int roleID)
        {
            return GetAllNoCache().Where(p => p.RoleID==roleID && p.IsDelete==false).ToList();
        }

        [AOPTransaction]
        public bool setRoleButtons(string Data)
        {
            JObject jobj = JObject.Parse(Data);
            int roleID = jobj["roleId"].ReferenceFromType<JToken, int>();
            var menus = jobj["menus"];

            var buttons = GetAllNoCache<T_Button>().OrderByDescending(p => p.Sortnum).ToList();
            

            var navs = menus.Select(menu => new
            {
                navid = menu["navid"].ReferenceFromType<JToken, int>(),
                btns = buttons.Where(n =>
                        menu["buttons"].Select(m => (string) m).Contains<string>(n.ButtonTag)
                        ).Select(k => k)
            });

            bool ret = true;
            int Erro = 0;
            foreach ( var nav in navs )
            {
                if ( GetAllNoCache().Where(p => p.NavID == nav.navid && p.RoleID == roleID).Count() > 0 )
                {
                    if ( EF.Database.ExecuteSqlCommand("delete from T_RoleNavBtns where NavId=" + nav.navid + " and RoleID=" + roleID)==0 )
                    {
                        Erro++;
                    }
                }

                foreach ( var btn in nav.btns )
                {
                    if ( Add(new T_RoleNavBtns() { RoleID = roleID, NavID = nav.navid, BtnID = btn.ID }) < 1 )
                    {
                        Erro++;
                    }
                }
            }
            if ( Erro != 0 )
            {
                Rollback();
                ret = false;
            }

     
            return ret;
        }
         #endregion


    }
}
