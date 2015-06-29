using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Entity.Admin;
using Complex.ICO_AOP;
using Complex.ICO_AOP.Attribute;
using Complex.Repository;

namespace Complex.Logical.Admin.Realization
{
    [ICOConfig("RDepartment")]
    public class RDepartment : RBase<T_Department>, IDepartment
    {
        public RDepartment()
            : base("MySQLServerContext")
        {
        }
        public List<T_Department> GetTreeGrid(int ParentID)
        {

            List<T_Department> quer = GetAllNoCache().Where(p => p.IsDelete == false).ToList();
            List<T_Department> datatreating = new List<T_Department>();
            if (quer != null)
            {
                datatreating = Setchildren(quer, ParentID);
            }
            return datatreating;
        }

        public List<T_Department> Setchildren(List<T_Department> quer, int ParentID)
        {
            var datatreating = new List<T_Department>();

            foreach (var item in quer.Where(p => p.ParentId == ParentID).ToList())
            {
                item.children = Setchildren(quer, item.ID);
                datatreating.Add(item);
            }

            return datatreating;
        }

        public List<T_Department> GetDepartment(int ParentID)
        {

            List<T_Department> quer = GetAllNoCache().Where(p => p.IsDelete == false).ToList();
            List<T_Department> datatreating = new List<T_Department>();
            if (quer != null)
            {
                datatreating = SetGetDepartmentChildren(quer, ParentID);
            }
            foreach (var item in datatreating.Where(p => p.ParentId == ParentID).ToList())
            {
                item.text = item.DepartmentName;
            }
            return datatreating;
        }

        public List<T_Department> SetGetDepartmentChildren(List<T_Department> quer, int ParentID)
        {
            var datatreating = new List<T_Department>();
            foreach (var item in quer.Where(p => p.ParentId == ParentID).ToList())
            {
                item.text = item.DepartmentName;
                item.children = SetGetDepartmentChildren(quer, item.ID);
                datatreating.Add(item);
            }
            return datatreating;
        }


        public string GetDepartmentName(int DepartmentID)
        {
            return GetByKey(DepartmentID).DepartmentName;
        }
    }
        //public List<T_Button> GetPage(string predicate, int page, int page_size, string order, string asc)
        //{
        //  //  return GetAllNoCache().OrderBy(p=>p.ID).Skip(( page - 1 ) * page_size).Take(page_size).ToList();

        //    return SearchSqLFor_Page<T_Button>("ID>1",2,5,"ID","asc"); 
        //}





}
