 
/// <reference path="../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../Scripts/avalon/avalon.js" />
/// <reference path="../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../Scripts/easyui1.3.3/jquery.easyui.all.js" />
/// <reference path="../../Scripts/easyui1.3.3/Xiucai.EasyUI.Extensions.js" />
/// <reference path="../../Scripts/Linqjs/linq.js" />
/// <reference path="../../Scripts/Linqjs/linq.jquery.js" />


require(["easyui", "jqueryAjax", "easyui_lang_zh_CN", "linq", "linq_jquery"], function ()
{
    $.log("hehe");
    var navgrid;

    var data=[
    {
        "ID": 1,
        "NavTitle": "系统设置",
        "Linkurl": "#",
        "Sortnum": 0,
        "iconCls": "icon-set1",
        "iconUrl": "",
        "IsVisible": true,
        "ParentID": 0,
        "NavTag": "SysSetting",
        "BigImageUrl": "/css/icon/32/bricks.png",
        "children": [
            {
                "ID": 2,
                "NavTitle": "操作按钮",
                "Linkurl": "sys/ButtonList.aspx",
                "Sortnum": 1,
                "iconCls": "icon-bricks",
                "iconUrl": "/css/icon/bricks.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Buttons",
                "BigImageUrl": "/css/icon/32/bricks.png",
                "children": [
                    {
                        "ID": 30,
                        "NavTitle": "添加",
                        "Linkurl": "2222",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "iconUrl": "/css/icon/16/add.png",
                        "IsVisible": true,
                        "ParentID": 2,
                        "NavTag": "addButton",
                        "BigImageUrl": "/css/icon/32/add.png",
                        "children": [],
                        "Buttons": []
                    },
                    {
                        "ID": 32,
                        "NavTitle": "修改",
                        "Linkurl": "/",
                        "Sortnum": 1,
                        "iconCls": "icon-edit",
                        "iconUrl": "/css/icon/16/edit.gif",
                        "IsVisible": true,
                        "ParentID": 2,
                        "NavTag": "按钮修改",
                        "BigImageUrl": "/css/icon/32/page_edit.png",
                        "children": [],
                        "Buttons": []
                    },
                    {
                        "ID": 33,
                        "NavTitle": "删除",
                        "Linkurl": "/",
                        "Sortnum": 3,
                        "iconCls": "icon-delete",
                        "iconUrl": "/css/icon/16/delete.png",
                        "IsVisible": true,
                        "ParentID": 2,
                        "NavTag": "按钮删除",
                        "BigImageUrl": "/css/icon/32/cross.png",
                        "children": [],
                        "Buttons": []
                    }
                ],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    },
                    {
                        "ID": 4,
                        "ButtonText": "查询",
                        "Sortnum": 4,
                        "iconCls": "icon-search",
                        "IconUrl": null,
                        "ButtonTag": "search",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_search\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-search\" title=\"查询\">查询</a>"
                    },
                    {
                        "ID": 19,
                        "ButtonText": "导出",
                        "Sortnum": 6,
                        "iconCls": "icon-page_excel",
                        "IconUrl": null,
                        "ButtonTag": "export",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_export\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-page_excel\" title=\"导出\">导出</a>"
                    }
                ]
            },
            {
                "ID": 10,
                "NavTitle": "导航菜单",
                "Linkurl": "sys/NavigationList.aspx",
                "Sortnum": 2,
                "iconCls": "icon-application_side_tree",
                "iconUrl": "/css/icon/application_side_tree.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Navigation",
                "BigImageUrl": "/css/icon/32/sitemap_color.png",
                "children": [],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    }
                ]
            },
            {
                "ID": 11,
                "NavTitle": "角色管理",
                "Linkurl": "sys/RoleList.aspx",
                "Sortnum": 3,
                "iconCls": "icon-group",
                "iconUrl": "/css/icon/group.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Role",
                "BigImageUrl": "/css/icon/32/group.png",
                "children": [],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    },
                    {
                        "ID": 20,
                        "ButtonText": "设置",
                        "Sortnum": 7,
                        "iconCls": "icon-wrench_orange",
                        "IconUrl": null,
                        "ButtonTag": "set",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_set\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-wrench_orange\" title=\"设置\">设置</a>"
                    },
                    {
                        "ID": 29,
                        "ButtonText": "数据权限",
                        "Sortnum": 15,
                        "iconCls": "icon-lightning",
                        "IconUrl": null,
                        "ButtonTag": "dataset",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_dataset\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-lightning\" title=\"数据权限\">数据权限</a>"
                    }
                ]
            },
            {
                "ID": 12,
                "NavTitle": "用户管理",
                "Linkurl": "sys/Users.aspx",
                "Sortnum": 4,
                "iconCls": "icon-users",
                "iconUrl": "/css/icon/users.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Users",
                "BigImageUrl": "/css/icon/32/group.png",
                "children": [],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    },
                    {
                        "ID": 4,
                        "ButtonText": "查询",
                        "Sortnum": 4,
                        "iconCls": "icon-search",
                        "IconUrl": null,
                        "ButtonTag": "search",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_search\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-search\" title=\"查询\">查询</a>"
                    },
                    {
                        "ID": 29,
                        "ButtonText": "数据权限",
                        "Sortnum": 15,
                        "iconCls": "icon-lightning",
                        "IconUrl": null,
                        "ButtonTag": "dataset",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_dataset\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-lightning\" title=\"数据权限\">数据权限</a>"
                    }
                ]
            },
            {
                "ID": 13,
                "NavTitle": "部门管理",
                "Linkurl": "sys/Departments.aspx",
                "Sortnum": 5,
                "iconCls": "icon-chart_organisation",
                "iconUrl": "/css/icon/chart_organisation.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Department",
                "BigImageUrl": "/css/icon/32/chart_organisation.png",
                "children": [],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    }
                ]
            },
            {
                "ID": 14,
                "NavTitle": "数据字典",
                "Linkurl": "sys/datadic.aspx",
                "Sortnum": 6,
                "iconCls": "icon-book_open_mark",
                "iconUrl": "/css/icon/book_open_mark.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Dic",
                "BigImageUrl": "/css/icon/32/book_addresses.png",
                "children": [],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    },
                    {
                        "ID": 26,
                        "ButtonText": "刷新",
                        "Sortnum": 10,
                        "iconCls": "icon-arrow_refresh",
                        "IconUrl": null,
                        "ButtonTag": "refresh",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_refresh\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-arrow_refresh\" title=\"刷新\">刷新</a>"
                    }
                ]
            },
            {
                "ID": 15,
                "NavTitle": "操作日志",
                "Linkurl": "sys/logs.aspx",
                "Sortnum": 7,
                "iconCls": "icon-page_error",
                "iconUrl": "/css/icon/page_error.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Log",
                "BigImageUrl": "/css/icon/32/page_lightning.png",
                "children": [],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 4,
                        "ButtonText": "查询",
                        "Sortnum": 4,
                        "iconCls": "icon-search",
                        "IconUrl": null,
                        "ButtonTag": "search",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_search\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-search\" title=\"查询\">查询</a>"
                    },
                    {
                        "ID": 3,
                        "ButtonText": "删除",
                        "Sortnum": 3,
                        "iconCls": "icon-delete3",
                        "IconUrl": null,
                        "ButtonTag": "delete",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_delete\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-delete3\" title=\"删除\">删除</a>"
                    }
                ]
            },
            {
                "ID": 16,
                "NavTitle": "个性化设置",
                "Linkurl": "sys/SysConfig.aspx",
                "Sortnum": 8,
                "iconCls": "icon-wrench_orange",
                "iconUrl": "/css/icon/wrench_orange.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "Profile",
                "BigImageUrl": "/css/icon/32/palette.png",
                "children": [
                    {
                        "ID": 31,
                        "NavTitle": "个性化",
                        "Linkurl": "sys/sysconfig.aspx",
                        "Sortnum": 1,
                        "iconCls": "icon-lightbulb",
                        "iconUrl": "/css/icon/16/lightbulb.png",
                        "IsVisible": true,
                        "ParentID": 16,
                        "NavTag": "个性化",
                        "BigImageUrl": "/css/icon/32/palette.png",
                        "children": [],
                        "Buttons": []
                    }
                ],
                "Buttons": [
                    {
                        "ID": 18,
                        "ButtonText": "浏览",
                        "Sortnum": 0,
                        "iconCls": "icon-eye",
                        "IconUrl": null,
                        "ButtonTag": "browser",
                        "Remark": "所有页面必须有此权限方可访问!",
                        "ButtonHtml": "<a id=\"a_browser\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-eye\" title=\"浏览\">浏览</a>"
                    },
                    {
                        "ID": 1,
                        "ButtonText": "添加",
                        "Sortnum": 1,
                        "iconCls": "icon-add",
                        "IconUrl": null,
                        "ButtonTag": "add",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_add\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-add\" title=\"添加\">添加</a>"
                    },
                    {
                        "ID": 10,
                        "ButtonText": "编辑",
                        "Sortnum": 2,
                        "iconCls": "icon-pencil",
                        "IconUrl": null,
                        "ButtonTag": "edit",
                        "Remark": "",
                        "ButtonHtml": "<a id=\"a_edit\" style=\"float:left\" href=\"javascript:;\" plain=\"true\" class=\"easyui-linkbutton\" icon=\"icon-pencil\" title=\"编辑\">编辑</a>"
                    }
                ]
            },
            {
                "ID": 17,
                "NavTitle": "数据库",
                "Linkurl": "sys/databackup.aspx",
                "Sortnum": 9,
                "iconCls": "icon-database_yellow",
                "iconUrl": "/css/icon/database_yellow.png",
                "IsVisible": true,
                "ParentID": 1,
                "NavTag": "DataBase",
                "BigImageUrl": "/css/icon/32/database_gear.png",
                "children": [] 
            }
        ] 
    },
    {
        "ID": 34,
        "NavTitle": "DEMO",
        "Linkurl": "#",
        "Sortnum": 1,
        "iconCls": "icon-note",
        "iconUrl": "",
        "IsVisible": true,
        "ParentID": 0,
        "NavTag": "DEMO",
        "BigImageUrl": "/Content/iconcss/icon/32/note.png",
        "children": [
            {
                "ID": 35,
                "NavTitle": "数据权限",
                "Linkurl": "demo/datatest.aspx",
                "Sortnum": 1,
                "iconCls": "icon-note",
                "iconUrl": "",
                "IsVisible": true,
                "ParentID": 34,
                "NavTag": "数据权限",
                "BigImageUrl": "/css/icon/32/note.png",
                "children": []
                
            }
        ] 
    }
    ];

    var grid = {
        databind: function (winsize)
        {
            navgrid = $('#navGrid').treegrid({
                toolbar: '#toolbar',
                width: winsize.width,
                height: winsize.height,
                url: "GetAll",
               // data:data,
                idField: 'ID',
                treeField: 'NavTitle',
                title: '导航菜单',
                iconCls: 'icon-nav',
                nowrap: false,
                rownumbers: true,
                animate: true,
                collapsible: false,
                frozenColumns: [[
                    { title: 'ID', field: 'ID', width: 50 },
                    { title: '菜单名称', field: 'NavTitle', width: 200 }
                ]],
                columns: [[
                    { title: '图标', field: 'iconCls', width: 180 },
                    { title: '标记', field: 'NavTag', width: 100 },
                    { title: '链接地址', field: 'Linkurl', width: 360 },
                    {
                        title: '是否显示', field: 'IsVisible', width: 80, align: 'center', formatter: function (v, d, i)
                        {
                            return '<img src="/images/' + (v ? "checkmark.gif" : "checknomark.gif") + '" />';
                        }
                    },
                    {
                        title: '是否为系统菜单', field: 'IsSys', width: 80, align: 'center', formatter: function (v, d, i)
                        {
                            return '<img src="/images/' + (v ? "checkmark.gif" : "checknomark.gif") + '" />';
                        }
                    },
                    { title: '排序', field: 'Sortnum', width: 80, align: 'center' }
                ]]
            });
        },
        reload: function ()
        {
            navgrid.treegrid('reload');
        },
        selected: function ()
        {
            console.log(1)
            return navgrid.treegrid('getSelected');
        }
    };


    var crud = {
        formUrl: "/ScriptsLogical/Navigation/NavForm.html?n=" + Math.random(),
        bindCtrl: function (navId)
        {

            var treeData = navgrid.treegrid('getData');
            treeData = JSON.stringify(treeData).replace(/ID/g, 'id').replace(/NavTitle/g, 'text');
            treeData = '[{"id":0,"selected":true,"text":"根节点"},' + treeData.substr(1, treeData.length - 1);

            top.$('#txt_parentid').combotree({
                data: JSON.parse(treeData),
                valueField: 'id',
                textField: 'text',
                panelWidth: '180',
                editable: false,
                lines: true,
                onSelect: function (item)
                {
                    var nodeId = top.$('#txt_parentid').combotree('getValue');
                    if (item.id == navId)
                    {
                        top.$('#txt_parentid').combotree('setValue', nodeId);
                        alert('上级菜单不能与当前菜单相同!');
                    }
                }
            }).combotree('setValue', 0);
            showIcon(); //选取图标
            top.$('#txt_orderid').numberspinner();
            top.$('#uiform').validate({
                //此处加入验证
            });
        },
        add: function ()
        {
            var addDialog = top.$.hDialog({
                href: crud.formUrl, title: '添加菜单', iconCls: 'icon-add', width: 500, height: 400,
                onLoad: function ()
                {
                    crud.bindCtrl();
                    var row = grid.selected();
                    if (row)
                    {
                        top.$('#txt_parentid').combotree('setValue', row.ID);
                    }

                },
                submit: function ()
                {
                    if (top.$('#uiform').validate().form())
                    {
                       
                        $.ajaxjson("Add", top.$('#uiform').serializeArray(), function (d)
                        {
                            if (d.Success)
                            {
                                msg.ok(d.Message);
                                addDialog.dialog('close');
                                grid.reload();
                            } else
                            {
                                msg.warning(d.Message);
                            }
                        });
                    }
                }
            }); 

        },
 
        edit: function ()
        {
            var row = grid.selected();
            if (row)
            {
                var editDailog = top.$.hDialog({
                    href: crud.formUrl, title: '编辑菜单', iconCls: 'icon-edit', width: 500, height: 400,
                    onLoad: function ()
                    {
                        crud.bindCtrl(row.ID);
                        
                        top.$('#ID').val(row.ID);
                        top.$('#txt_ptag').val(row.NavTag);
                        top.$('#txt_title').val(row.NavTitle);
                        top.$('#txt_url').val(row.Linkurl);
                        top.$('#txt_iconcls').val(row.iconCls);
                        top.$('#smallIcon').attr('class', "icon " + row.iconCls);
                        top.$('#txt_parentid').combotree('setValue', row.ParentID);
                        top.$('#chkvisible').attr('checked', row.IsVisible);
                        top.$('#txt_orderid').numberspinner('setValue', row.Sortnum);
                        top.$('#txt_iconurl').val(row.iconUrl);
                        top.$('#txt_bigimgurl').val(row.BigImageUrl);
                        top.$('#imgBig').attr('src', row.BigImageUrl);
                    },
                    submit: function ()
                    {
                        if (top.$('#uiform').validate().form())
                        {
                         //   var query = createParam('edit', row.ID);
                            $.ajaxjson("Update", top.$('#uiform').serializeArray(), function (d)
                            { 
                                if (d.Success)
                                {
                                    msg.ok(d.Message);
                                    editDailog.dialog('close');
                                    grid.reload();
                                } else
                                {
                                    MessageOrRedirect(d.Message);
                                }
                            });
                        }
                    }
                });


            } else
            {
                msg.warning('请选择要修改菜单!');
                return false;
            }
            return false;
        },
        del: function ()
        {
            var row = grid.selected();
            if (row != null)
            {
                var nodes = [row.ID];
               // getChildNodes(row.ID, nodes);
             //   var query = createParam("delete", row.ID, nodes.join(','));
                var state=true;
                top.$.messager.confirm("提示", '确认要删除选中的菜单吗？\r\n注意：将连同子菜单一同删除。',
                function(event){
                
                    if (event)
                    {
                        $.ajaxjson("Delete", { ID: row.ID }, function (d)
                        {
                            if (d.Success)
                            {
                                msg.ok(d.Message);
                                grid.reload();
                            } else
                            {
                                MessageOrRedirect(d.Message);
                            }
                        });
                         
                    } else
                    {
                        state = false;
                    }

                } );
               
                return state;
                
                    
            }
            else
            {
                msg.warning('请选择要删除的菜单!');
                return false;
            }
            return false;
        },
        setButtons: function ()
        { //给菜单分配按钮
            var row = grid.selected();
            var btngrid;
            if (row)
            {
                var setDialog = top.$.hDialog({
                    title: '菜单名称：' + row.NavTitle,
                    width: 440, height: 400, iconCls: 'icon-cog', cache: false,
                    href: '/ScriptsLogical/Navigation/SetNavButtons.html?n=' + Math.random(),
                    onLoad: function ()
                    {
                        btngrid = top.$('#left_btns').datagrid({
                            title: '所有按钮',
                            url: "/Admin/Navigation/GetButtonsAllList",
                            nowrap: false, //折行
                            fit: true,
                            rownumbers: true, //行号
                            striped: true, //隔行变色
                            idField: 'ID',//主键
                            singleSelect: true, //单选
                            frozenColumns: [[]],
                            columns: [[
                                {
                                    title: '图标', field: 'iconCls', width: 38, formatter: function (v, d, i)
                                    {
                                        return '<span class="icon ' + v + '">&nbsp;</span>';
                                    }, align: 'center'
                                },
                                { title: '按钮名称', field: 'ButtonText', width: 80, align: 'center' },
                                { title: '备注', field: 'Remark', width: 180, hidden: true }
                            ]],
                            onDblClickRow: function (rowIndex, rowData)
                            {
                                var currRows = top.$('#right_btns').datagrid('getRows');
                                var hasBtns = Enumerable.from(currRows).where("x=>x.ID==" + rowData.ID).select("$").toArray();
                                if (hasBtns.length > 0)
                                    return false;
                                else
                                {
                                    top.$('#right_btns').datagrid('appendRow', rowData);
                                }
                            },
                            onLoadSuccess: function (data)
                            {

                                //var arr = Enumerable.from(row.Buttons).select("$.ID").toArray();
                                //url: "/Admin/Navigation/GetNavButtons",
                                //queryParams: { NavID: row.ID },
                             

                                top.$('#right_btns').datagrid({
                                    title: '已选按钮',
                                    nowrap: false, //折行
                                    fit: true,
                                    rownumbers: true, //行号
                                    striped: true, //隔行变色
                                    idField: 'ID',//主键
                                    singleSelect: true, //单选
                                    frozenColumns: [[]],
                                    columns: [[
                                        {
                                            title: '图标', field: 'iconCls', width: 38, formatter: function (v, d, i)
                                            {
                                                return '<span class="icon ' + v + '">&nbsp;</span>';
                                            }, align: 'center'
                                        },
                                        { title: '按钮名称', field: 'ButtonText', width: 80, align: 'center' },
                                        { title: '备注', field: 'Remark', width: 180, hidden: true }
                                    ]],
                                    onDblClickRow: function (rowIndex, rowData)
                                    {
                                        top.$('#right_btns').datagrid('deleteRow', rowIndex);
                                    }
                                });
                                $.ajaxjson("/Admin/Navigation/GetNavButtons", { NavID: row.ID }, function (data1)
                                {
                                    var arr = Enumerable.from(data1).select("$.ButtonId").toArray();
                                   
                                    if (arr.length > 0)
                                    { 
                                        $.each(data.rows, function (i, n)
                                        { 
                                            if ($.inArray(n.ID, arr) > -1)
                                                top.$('#right_btns').datagrid('appendRow', n);
                                        });
                                    }
                                });

                               

                                //绑定移动按钮事件
                                //top.$('#btnUp').click(function () { moveGridRow.Up(top.$('#right_btns')); });
                                //top.$('#btnDown').click(function () { moveGridRow.Down(top.$('#right_btns')); });
                                top.$('#btnRight').click(function () { moveGridRow.Insert(top.$('#left_btns'), top.$('#right_btns')); });
                                top.$('#btnLeft').click(function () { moveGridRow.Remove(top.$('#right_btns')); });
                            }
                        });


                    },
                    submit: function ()
                    {
                        var btns = top.$('#right_btns').datagrid('getRows');

                        if (btns.length > 0)
                        {
                            var permissionids = Enumerable.from(btns).select("$.ID").toArray().join(",");
                            $.ajaxtext("/Admin/Navigation/setButtons", { NavID: row.ID, btns: permissionids }, function (d)
                            { 
                                if (d.Success)
                                {
                                    msg.ok(d.Message);
                                    setDialog.dialog('close');
                                    grid.reload();
                                } else
                                {
                                    msg.warning(d.Message);
                                }
                            });
                        } else
                        {
                            alert('请选择按钮啊，亲！');
                        }
                    }
                });


            }
            else
                msg.warning('请选择导航菜单');
        }
    };
 
    var moveGridRow = {
        //Up: function (jq)
        //{
        //    var rowindex = jq.datagrid('getSelectedIndex');
        //    if (rowindex > -1)
        //    {
        //        var rows = jq.datagrid('getRows');
        //        var newRowIndex = rowindex - 1;
        //        if (newRowIndex < 0)
        //            newRowIndex = 0;

        //        var targetRow = rows[newRowIndex];
        //        var currentRow = rows[rowindex];

        //        rows[newRowIndex] = currentRow;
        //        rows[rowindex] = targetRow;

        //        jq.datagrid('loadData', rows);
        //        jq.datagrid('selectRow', newRowIndex);

        //    } else
        //        alert('亲，都到顶啦，在点就可以见到天宫1号啦！');
        //},
        //Down: function (jq)
        //{
        //    var rowindex = jq.datagrid('getSelectedIndex');
        //    if (rowindex > -1)
        //    {
        //        var rows = jq.datagrid('getRows');
        //        if (rowindex < rows.length - 1)
        //        {
        //            var newRowIndex = rowindex + 1;

        //            var targetRow = rows[newRowIndex];
        //            var currentRow = rows[rowindex];

        //            rows[newRowIndex] = currentRow;
        //            rows[rowindex] = targetRow;

        //            jq.datagrid('loadData', rows);
        //            jq.datagrid('selectRow', newRowIndex);

        //        } else
        //            alert('亲，到底啦，在点就罢工啦！');
        //    }
        //},
        Insert: function (ljq, rjq)
        {
            var rows = ljq.datagrid('getSelected');
            if (rows)
            {
                var currRows = rjq.datagrid('getRows');
                var hasBtns = Enumerable.from(currRows).where("x=>x.ID==" + rows.ID).select("$").toArray();
                if (hasBtns.length > 0)
                    return false;
                else
                {
                    rjq.datagrid('appendRow', rows);
                }
            } else
            {
                alert('请选择按钮。');
                return false;
            }
        },
        Remove: function (jq)
        {
            var rowindex = jq.datagrid('getSelectedIndex');
            if (rowindex > -1)
                jq.datagrid('deleteRow', rowindex);
            return false;
        }
    }
    var showIcon = function ()
    { 
         top.$('#selecticon').click(function ()
         {
             var iconDialog = top.$.hDialog({
                 iconCls: 'icon-application_view_icons',
                 href: '/Content/iconcss/iconlist.htm?v=' + Math.random(),
                 title: '选取图标', width: 800, height: 600, showBtns: false,
                 onLoad: function ()
                 {
                     top.$('#iconlist li').attr('style', 'float:left;border:1px solid #fff;margin:2px;width:16px;cursor:pointer').click(function ()
                     {
                         var iconCls = top.$(this).find('span').attr('class').replace('icon ', '');
                         top.$('#txt_iconcls').val(iconCls);
                         top.$('#txt_iconurl').val(top.$(this).attr('title'));
                         top.$('#smallIcon').attr('class', "icon " + iconCls);

                         iconDialog.dialog('close');
                     }).hover(function ()
                     {
                         top.$(this).css({ 'border': '1px solid red' });
                     }, function ()
                     {
                         top.$(this).css({ 'border': '1px solid #fff' });
                     });


                     //top.$('#btnicon').click(function() {
                     //    $.get(actionURL, 'action=buildIcon', function(d) {
                     //        top.$.hDialog({
                     //            content: '<textarea style="width:100%;height:100%;">' + d + '</textarea>',
                     //            max: true
                     //        });
                     //    });

                     //});
                 }
             });
         });

         top.$('#selectBigIcon').click(function ()
         {
             var iconDialog = top.$.hDialog({
                 iconCls: 'icon-application_view_icons',
                 href: '/Content/iconcss/icon32list.html?v=' + Math.random(),
                 title: '选取图标',
                 width: 800,
                 height: 600,
                 showBtns: false,
                 onLoad: function ()
                 {
                     top.$('#icon32list li').css({ 'float': 'left', 'width': '32px', 'margin': '2px', 'border': '1px solid #fff' }).hover(function ()
                     {
                         top.$(this).css({ 'border': '1px solid red' });
                     }, function ()
                     {
                         top.$(this).css({ 'border': '1px solid #fff' });
                     }).click(function ()
                     {
                         top.$('#txt_bigimgurl').val($(this).attr('title'));
                         top.$('#imgBig').attr('src', $(this).attr('title'));
                         iconDialog.dialog('close');
                     });
                 }
             });
         });
    };



    autoResize({ dataGrid: '#navGrid', gridType: 'treegrid', callback: grid.databind, height: 20, width: 8 });
    $('#a_add').click(crud.add);
    $('#a_setbtn').click(crud.setButtons);
    $('#a_edit').click(crud.edit);
    $('#a_delete').click(crud.del);
});