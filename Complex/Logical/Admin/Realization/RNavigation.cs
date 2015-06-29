using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP.Attribute;
using Complex.Repository;

namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RNavigation")]
    public class RNavigation : RBase<T_Navigation> , INavigation
    {
        public RNavigation()
            : base("MySQLServerContext")
        { 
        } 
 
        public List<T_Navigation> GetTreeGrid(int ParentID)
        {

            List<T_Navigation> quer = GetAllNoCache().Where(p=>p.IsDelete==false).OrderByDescending(p=>p.Sortnum).ToList();
            List<T_Navigation> datatreating = new List<T_Navigation>();
            if ( quer != null )
            {
                datatreating = Setchildren(quer, ParentID);
            }
            return datatreating;
        }

        public List<T_Navigation> Setchildren(List<T_Navigation> quer, int ParentID)
        {
            var datatreating = new List<T_Navigation>();

            foreach ( var item in quer.Where(p => p.ParentID == ParentID).OrderByDescending(p => p.Sortnum).ToList() )
            {
                item.children = Setchildren(quer, item.ID);
                datatreating.Add(item);
            }

            return datatreating;
        }

        public object GetNavigation()
        { 
 
            var quer = GetAllNoCache().Where(p => p.IsDelete == false&&p.IsVisible==true).OrderByDescending(p => p.Sortnum).Select(p => new TemNavigation() { id = p.ID, state = "open", iconCls= p.iconCls, text = p.NavTitle, attributes = new TemNavigationAttr { url = p.Linkurl, sortnum = p.Sortnum, parentid = p.ParentID, iconUrl = p.iconUrl, BigImageUrl = p.BigImageUrl } }).ToList(); 
            object  datatreating = new object();
            if ( quer != null )
            {
                datatreating = Setchildren(quer, 0);
            }
            return datatreating;
        }
        public List<TemNavigation> Setchildren(List<TemNavigation> quer, int ParentID)
        {
            var datatreating = new List<TemNavigation>();

            foreach ( var item in quer.Where(p => p.attributes.parentid == ParentID).OrderByDescending(p => p.attributes.sortnum).ToList() )
            {
                item.children = Setchildren(quer, item.id);
                datatreating.Add(item);
            }  
            return datatreating;
        }

        public object GetNavigation(List<T_Navigation> list)
        { 
            var quer = list.Where(p => p.IsDelete == false && p.IsVisible == true).OrderByDescending(p => p.Sortnum).Select(p => new TemNavigation() { id = p.ID, state = "open", iconCls = p.iconCls, text = p.NavTitle, attributes = new TemNavigationAttr { url = p.Linkurl, sortnum = p.Sortnum, parentid = p.ParentID, iconUrl = p.iconUrl, BigImageUrl = p.BigImageUrl } }).ToList();
            object datatreating = new object();
            if ( quer != null )
            {
                datatreating = Setchildren(quer, 0);
            }
            return datatreating;
        }
        
    }
    public class TemNavigation
    {
        public int id { get; set; }

        public string state { get; set; }

        public string text { get; set; }

        public TemNavigationAttr attributes { get; set; } 

        public List<TemNavigation> children { get; set; }

        public string iconCls { get; set; }
    }
    public class TemNavigationAttr
    {

        public string url { get; set; }

        public int sortnum { get; set; }

        public int? parentid { get; set; }

        public string iconUrl { get; set; }

        public string BigImageUrl { get; set; }
    }
}
