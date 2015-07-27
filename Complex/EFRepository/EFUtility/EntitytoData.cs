
using Complex.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Complex.Entity.Admin;
 
namespace Complex.Repository.Utility
{
    public class EntitytoData : DbContext
    {
        public EntitytoData() : base("MySQLServerContext") { }
        public EntitytoData(string p) : base(p) {
                
            Database.SetInitializer<EntitytoData>(new EntitytoDataInitializer()); 
        }
        public static EntitytoData Init(string connectionstring)
        {
     

            ////1.关闭初始化
            //Database.SetInitializer<EntitytoData>(null);

            ////2.CreateDatabaseIfNotExists 这是Entity Framework的默认初始化策略，没有必要设置它，如果真的需要设置，如下： 
            // Database.SetInitializer(new CreateDatabaseIfNotExists<EntitytoData>());

            ////3.DropCreateDatabaseWhenModelChanges 如果模型发生了改变，则删除并重建数据库。 
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntitytoData>());

            ////4.DropCreateDatabaseAlways 无论模型和数据库匹配与否，都删除并重建数据库。 
            //Database.SetInitializer(new DropCreateDatabaseAlways<EntitytoData>());
             


          // Database.SetInitializer<EntitytoData>(new EntitytoDataInitializer()); 

            return new EntitytoData(connectionstring);
        }
 
        //public DbSet<test2> test2 { get; set; }

        public DbSet<T_User> T_User { get; set; }

        public DbSet<T_Button> T_Button { get; set; }

        public DbSet<T_Navigation> T_Navigation { get; set; }

        public DbSet<T_NavButtons> T_NavButtons { get; set; }

        public DbSet<T_Department> T_Department { get; set; }

        public DbSet<T_Role> T_Role { get; set; }

        public DbSet<T_RoleNavBtns> T_RoleNavBtns { get; set; }

        public DbSet<T_UserRoles> T_UserRoles { get; set; }

      

        protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {

            UserTableChanged(entityEntry);
            return base.ShouldValidateEntity(entityEntry);
        }

        private void UserTableChanged(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Deleted || entityEntry.State == EntityState.Added)
            {
                

            }
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {  
            //去除数据库表复数约定
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            //数据库架构名称 主要对Oracle 影响较大
            modelBuilder.HasDefaultSchema("dbo");
            //Entity Framework中DbContext首次加载OnModelCreating会检查__MigrationHistory表，
            //作为使用Code Frist编程模式，而实际先有数据库时，这种检测就是多余的了，所以需要屏蔽，
            //在EF 4.1之前可以使用在OnModelCreating函数总加入下面语句来屏蔽这种检测：
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>(); 
            //而到4.3之后需要使用，上列语句以被MSDN明确表示过时，所以需要新的方式取代：  
            //Database.SetInitializer<EntitytoData>(null);

            //modelBuilder.Configurations.Add(new Complex.Entity.Mapping.test2map());
            //modelBuilder.Configurations.Add(new T_DepartmentOrDescriptionMap());
            //modelBuilder.Configurations.Add(new T_Contradiction_TypeDepartmentMap());

        }
      
    }


    internal class EntitytoDataInitializer : CreateDatabaseIfNotExists<EntitytoData>
    { 
        protected override void Seed(EntitytoData context)
        {
            //TestCase model= new TestCase { Field1 = 1, Field3 = "a", Field2 = 1.2F, Field4 = "a", Field5 = DateTime.Now };
            //context.TestCase.Add(model);
            context.T_User.Add(new T_User() { UserName = "admin", Password = "E10ADC3949BA59ABBE56E057F20F883E", TrueName="初始化管理员", IsAdmin=true,DepartmentId=0,IsDelete=false,IsDisabled=false});
            context.T_User.Add(new T_User() { UserName = "user1", Password = "E10ADC3949BA59ABBE56E057F20F883E", TrueName = "普通用户1", IsAdmin = false, DepartmentId = 1, IsDelete = false, IsDisabled = false });
            context.T_User.Add(new T_User() { UserName = "user2", Password = "E10ADC3949BA59ABBE56E057F20F883E", TrueName = "普通用户2", IsAdmin = false, DepartmentId = 1, IsDelete = false, IsDisabled = false });
            context.T_User.Add(new T_User() { UserName = "user3", Password = "E10ADC3949BA59ABBE56E057F20F883E", TrueName = "普通用户3", IsAdmin = false, DepartmentId = 1, IsDelete = false, IsDisabled = false });

            context.T_Navigation.Add(new T_Navigation() { ID = 1, NavTitle = "系统设置", iconCls = "icon-set1", Linkurl = "#", iconUrl = "", ParentID = 0, Sortnum = 1, BigImageUrl = null, IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 2, NavTitle = "操作按钮", iconCls = "icon-bricks", Linkurl = "/Admin/Button/Index", iconUrl = "/Content/iconcss/icon/bricks.png", ParentID = 1, Sortnum = 100, BigImageUrl = "/Content/iconcss/icon/32/bricks.png", IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 3, NavTitle = "导航菜单", iconCls = "icon-application_side_tree", Linkurl = "/Admin/Navigation/Index", iconUrl = "/Content/iconcss/icon/application_side_tree.png", ParentID = 1, Sortnum = 99, BigImageUrl = "/Content/iconcss/icon/32/application_side_tree.png", IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 4, NavTitle = "角色管理", iconCls = "icon-group", Linkurl = "/Admin/Role/Index", iconUrl = "/Content/iconcss/icon/group.png", ParentID = 1, Sortnum = 98, BigImageUrl = "/Content/iconcss/icon/32/group.png", IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 5, NavTitle = "用户管理", iconCls = "icon-users", Linkurl = "/Admin/User/Index", iconUrl = "/Content/iconcss/icon/users.png", ParentID = 1, Sortnum = 97, BigImageUrl = "/Content/iconcss/icon/32/group.png", IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 6, NavTitle = "部门管理", iconCls = "icon-chart_organisation", Linkurl = "/Admin/Department/Index", iconUrl = "/Content/iconcss/icon/chart_organisation.png", ParentID = 1, Sortnum = 96, BigImageUrl = "/Content/iconcss/icon/32/chart_organisation.png", IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 7, NavTitle = "个性化设置", iconCls = "icon-wrench_orange", Linkurl = "/Admin/UserConfig/Index", iconUrl = "/Content/iconcss/icon/wrench_orange.png", ParentID = 1, Sortnum = 95, BigImageUrl = "/Content/iconcss/icon/32/palette.png", IsSys = true });

            context.T_Navigation.Add(new T_Navigation() { ID = 8, NavTitle = "操作日志", iconCls = "icon-page_error", Linkurl = "/Admin/AdminLog/Index", iconUrl = "/Content/iconcss/icon/page_error.png", ParentID = 1, Sortnum = 94, BigImageUrl = "/Content/iconcss/icon/32/page_error.png", IsSys = true });

        

            context.T_Navigation.Add(new T_Navigation() { ID = 9, NavTitle = "DEMO", iconCls = "icon-note", Linkurl = "#", iconUrl = "/Content/iconcss/icon/note.png", ParentID = 0, Sortnum = 2, BigImageUrl = "/Content/iconcss/icon/32/note.png", IsSys = false });

            context.T_Button.Add(new T_Button() { ID = 1, ButtonTag = "browser", iconCls = "icon-eye", ButtonText = "浏览", IconUrl = "", Remark = "所有页面必须有此权限方可访问!", Sortnum = 101 });
            context.T_Button.Add(new T_Button() { ID = 2, ButtonTag = "add", iconCls = "icon-add", ButtonText = "添加", IconUrl = "", Remark = "", Sortnum = 100, IsSys = true });
            context.T_Button.Add(new T_Button() { ID = 3, ButtonTag = "delete", iconCls = "icon-delete3", ButtonText = "删除", IconUrl = "", Remark = "", Sortnum = 99, IsSys = true });
            context.T_Button.Add(new T_Button() { ID = 4, ButtonTag = "edit", iconCls = "icon-pencil", ButtonText = "编辑", IconUrl = "", Remark = "", Sortnum =98, IsSys = true });
            context.T_Button.Add(new T_Button() { ID = 5, ButtonTag = "search", iconCls = "icon-search", ButtonText = "查询", IconUrl = "", Remark = "", Sortnum =97, IsSys = true });
            context.T_Button.Add(new T_Button() { ID = 6, ButtonTag = "inport", iconCls = "icon-database_copy", ButtonText = "导入", IconUrl = "", Remark = "", Sortnum = 96 });
            context.T_Button.Add(new T_Button() { ID = 7, ButtonTag = "export", iconCls = "icon-page_excel", ButtonText = "导出", IconUrl = "", Remark = "", Sortnum = 95 });
            context.T_Button.Add(new T_Button() { ID = 8, ButtonTag = "set", iconCls = "icon-wrench_orange", ButtonText = "设置", IconUrl = "", Remark = "", Sortnum = 94 });
            context.T_Button.Add(new T_Button() { ID = 9, ButtonTag = "audit", iconCls = "icon-user_magnify", ButtonText = "审核", IconUrl = "", Remark = "", Sortnum = 93 });
            context.T_Button.Add(new T_Button() { ID = 10, ButtonTag = "upload", iconCls = "icon-folder_up", ButtonText = "上传", IconUrl = "", Remark = "", Sortnum =92 });
            context.T_Button.Add(new T_Button() { ID = 11, ButtonTag = "download", iconCls = "icon-download", ButtonText = "下载", IconUrl = "", Remark = "", Sortnum = 91 });

           
 

            for ( int i = 1; i < 100; i++ )
            {
                context.T_NavButtons.Add(new T_NavButtons() { ButtonId = 1, NavId = i });
            }
            for ( int i = 2; i < 7; i++ )
            {
                context.T_NavButtons.Add(new T_NavButtons() { ButtonId = 2, NavId = i });
                context.T_NavButtons.Add(new T_NavButtons() { ButtonId = 3, NavId = i });
                context.T_NavButtons.Add(new T_NavButtons() { ButtonId = 4, NavId = i });
                if (! new int[]{3,6}.Contains(i) )
                {
                    context.T_NavButtons.Add(new T_NavButtons() { ButtonId = 5, NavId = i });
                }
                
            }


            context.T_Department.Add(new T_Department() {ID=1,DepartmentName="一级部门",Sortnum=0,Remark="初始化一级部门",IsDelete=false});
            context.T_Department.Add(new T_Department() { ID = 2, DepartmentName = "二级部门1", Sortnum = 1, Remark = "二级部门1", IsDelete = false,ParentId=1});
            context.T_Department.Add(new T_Department() { ID = 3, DepartmentName = "二级部门2", Sortnum = 2, Remark = "二级部门2", IsDelete = false, ParentId = 1 });
            context.T_Department.Add(new T_Department() { ID = 4, DepartmentName = "一级部门2", Sortnum = 3, Remark = "一级部门2", IsDelete = false });
            context.T_Department.Add(new T_Department() { ID = 5, DepartmentName = "二级部门3", Sortnum = 4, Remark = "二级部门3", IsDelete = false, ParentId = 4 });
            context.T_Department.Add(new T_Department() { ID = 6, DepartmentName = "二级部门4", Sortnum = 5, Remark = "二级部门4", IsDelete = false, ParentId = 4 });


            context.T_Role.Add(new T_Role() {ID = 1,RoleName = "管理员",Sortnum = 0,Remark = "管理员",IsDefault = 1,IsDelete = false });
            context.T_Role.Add(new T_Role() { ID = 2, RoleName = "普通用户", Sortnum = 1, Remark = "普通用户", IsDefault = 0, IsDelete = false });

            base.Seed(context);
        }
    }
        
}
