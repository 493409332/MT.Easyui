/// <reference path="../../Scripts/Linqjs/linq.jquery.js" />
/// <reference path="../../Scripts/Linqjs/linq.js" />
/// <reference path="../../Scripts/Linqjs/linq.jquery.js" />

/// <reference path="../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../Scripts/avalon/avalon.js" />
/// <reference path="../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../Scripts/easyui1.3.3/jquery.easyui.all.js" />
/// <reference path="../../Scripts/Linqjs/linq.js" />


require(["avalon", "easyui", "easyui_lang_zh_CN", "jqueryAjax", "XiucaiEasyUIExtensions", "jqueryvalidate", "linq_jquery", "Search"], function ()
{
    $.log("hehe");
    var _roleGrid;
    var formHTML = '<form id=xiucaiForm><table cellpadding=5 cellspacing=0 width=100% align="center" class="grid" border=0>'
                + '<tr><td align="right">角色名称：</td>'
                + '<td align="left"><input name=rolename type="text"  required="true" style="width:150px" class="txt03" id="txtrolename" /></td></tr><tr>'
                  + '<tr><td>所属部门：</td><td><input type="text" required="true"  name="DepartmentId" style="width: 200px" id="txt_department" /></td></tr>'
                + '<td align="right">排序：</td><td align="left"><input name=sortnum type="text" value=0 style="width:100px"  id="txtsortnum" /></td></tr>' 
                +'<tr><td align="right">备注</td>'
                + '<td align="left"><textarea rows="3" name=remark style="width:250px;height:50px;" class="txt03" id="txtremark" /></td></tr>'
                + '</table><input id="ID" type="hidden" name="ID" /></form>';

   
    $(function () {
        $('#a_set').linkbutton({ text: '分配权限' }).click(authorize.run);
        $('#a_add').click(crud.add);
        $('#a_edit').click(crud.edit);
        $('#a_delete').click(crud.del);

        $('#a_search').click(function ()
        {
            search.go('roleGrid');
        });
        var size = { width: $(window).width(), height: $(window).height() };
        mylayout.init(size);
        deptree.init();

        $(window).resize(function ()
        {
            size = { width: $(window).width(), height: $(window).height() };
            mylayout.resize(size);
        });

        autoResize({ dataGrid: '#roleGrid', gridType: 'datagrid', callback: mygrid.databind, height:51, width:235 });
        //$('#a_dataset').click(authorize.setDeptments);
    });


    var data = {
        "total": 4,
        "rows": [
            {
                "ID": 2,
                "RoleName": "管理员",
                "Sortnum": 2,
                "Remark": "工程师",
                "isDefault": 0
            },
            {
                "ID": 3,
                "RoleName": "功城队",
                "Sortnum": 1,
                "Remark": "6666",
                "isDefault": 0
            },
            {
                "ID": 17,
                "RoleName": "测试人员",
                "Sortnum": 1,
                "Remark": "1",
                "isDefault": 0
            },
            {
                "ID": 20,
                "RoleName": "普能用户",
                "Sortnum": 3,
                "Remark": "",
                "isDefault": 0
            }
        ]
    };

    var mygrid = {
        databind: function (size)
        {
          
            var listdep=new Array();

            listdepfuc(deptree.data());
           
          
            function listdepfuc(deptreedata)
            {
                $.each(deptreedata, function ()
                { 
                    if (avalon.type(this.children)=="array" && this.children!=null)
                    {
                        listdepfuc(this.children);
                         
                    }
                   
                    listdep.push(this);  
                });
            }

            _roleGrid = $('#roleGrid').datagrid({
                url: "GetPage",
                width: size.width,
                height: size.height,
                //title: '角色列表',
                toolbar: '#toolbar',
                iconCls: 'icon icon-list',
                nowrap: false, //折行
                rownumbers: true, //行号
                striped: true, //隔行变色
                idField: 'ID',//主键
                singleSelect: true, //单选
                columns: [[
                    { title: 'ID', field: 'ID', width: 100, sortable: true},
                    { title: '角色名称', field: 'RoleName', width: 160, sortable: true },
                    {
                        title: '部门名称', field: 'DepartmentId', width: 160, formatter: function (val, row)
                        {
                            var quer = Enumerable.from(listdep).where(function (x)
                            { 
                                return x.id == val;
                            }).select(function (x) { return x.text; }).firstOrDefault();
                         
                          if (quer!=null)
                          {
                              return quer;
                          } else
                          {
                              return val;
                          } 

                        }
                    },
                    { title: '排序', field: 'Sortnum', width: 80, sortable: true },
                    { title: '默认', field: 'IsDefault', width: 80,hidden:true},
                    { title: '备注', field: 'Remark', width: 280 }
                ]],
              
                pagination: true,
                pageSize: 10,
                pageList: [20, 40, 50]
            });
        },
        selected: function () {
            return _roleGrid.datagrid('getSelected');
        },
        reload: function () {
            _roleGrid.datagrid('clearSelections').datagrid('reload', { filter: '' });
        }
    };

    var mylayout = {
        init: function (size)
        {
            $('#layout').width(size.width - 18).height(size.height - 18).layout();
            var center = $('#layout').layout('panel', 'center');
            center.panel({
                onResize: function (w, h)
                {
                    $('#userGrid').datagrid('resize', { width: w - 6, height: h - 36 });
                }
            });
        },
        resize: function (size)
        {
            mylayout.init(size);
            $('#layout').layout('resize');
        }
    };

    var deptree = {
        init: function ()
        {
            $.ajaxSetup({
                async: false
            });


            $('#deptree').tree({
                lines: true,
                url: "/Department/GetAllReplace",
                animate: true,
                onLoadSuccess: function (node, data)
                {
                    $('body').data('depData', data);
                    $.ajaxSetup({
                        async: true
                    });
                },
                onClick: function (node)
                {
                    
                    var depId = node.id;
                     
                    var children = $('#deptree').tree('getChildren', node.target);
                    // alert(children.length);
                    var arr = [];
                    arr.push(depId);
                    for (var i = 0; i < children.length; i++)
                    {
                        arr.push(children[i].id);
                    }

                    var strDepIds = arr.join(',');
                    var filterObj = { "groupOp": "AND", "rules": [{ "field": "DepartmentId", "op": "in", "data": strDepIds }] };
                    $('#roleGrid').datagrid('load', { filter: JSON.stringify(filterObj) });
                }
            });
        },
        data: function ()
        {
            var d = JSON.stringify($('body').data('depData'));
            d = '[{"id":0,"text":"请选择部门"},' + d.substr(1);
           
            return JSON.parse(d);
        }

    };

    var crud = {
        initData: function (depid)
        {
      
            var _depid = depid || 0;
            
            top.$('#txt_department').combotree({
                data: deptree.data(),
                valueField: 'id',
                textField: 'text',
                value: _depid,
                onSelect: function (record)
                {
                    console.log(record)
                    
                }
            });
            
        },

        initValidate: function () {
            top.$('#xiucaiForm').validate();
        },
        add: function ()
        {
            
            var addDialog = top.$.hDialog({
                content: formHTML,
                title: '添加角色',
                iconCls: 'icon-add',
                width: 450,
                height: 260,
                submit: function () {
                    if (top.$('#xiucaiForm').validate().form()) {
                       
                        $.ajaxjson("Add", top.$('#xiucaiForm').serializeArray(), function (d)
                        {
                            if (d.Success) {
                                msg.ok(d.Message);
                                addDialog.dialog('close');
                                mygrid.reload();
                            } else {
                                MessageOrRedirect(d);
                            }
                        });
                    }
                }
            });
            top.$('#txtsortnum').numberspinner({ min: 0, max: 999 });
            crud.initData();

        },
        edit: function () {
            var row = mygrid.selected();

            if (!row) {
                msg.warning('请选择修改的角色。');
                return false;
            }
           
            var editWin = top.$.hDialog({
                content: formHTML,
                title: '编辑角色',
                iconCls: 'icon-add',
                width: 450,
                height: 260,
                submit: function () {
                    if (top.$('#xiucaiForm').validate().form()) {
                        var data = top.$('#xiucaiForm').serializeArray();
                        $.ajaxjson("Update", data, function (d) {
                            if (d.Success) {
                                msg.ok(d.Message);
                                editWin.dialog('close');
                                mygrid.reload();
                            } else {
                                MessageOrRedirect(d);
                            }
                        });
                    }
                }
            });
            //Load Data
            top.$('#ID').val(row.ID);
            top.$('#txtrolename').val(row.RoleName);
            top.$('#txtremark').val(row.Remark);
            top.$('#txtsortnum').numberspinner({ min: 0, max: 999 }).numberspinner('setValue', row.Sortnum);
        
            crud.initData(row.DepartmentId);
            //top.$('#txtIsDefault').attr('checked', row.IsDefault == 1);
       
            return false;
        },
        del: function () {
            var row = mygrid.selected();
            if (!row) {
                msg.warning('请选择要删除的角色。');
                return false;
            }

            if (confirm('确认要删除选中的数据吗?')) {
                $.ajaxjson("Delete", { ID: row.ID }, function (d) {
                    if (d.Success) {
                        msg.ok(d.Message);
                        mygrid.reload();
                    } else {
                        MessageOrRedirect(d);
                    }
                });
            }
            return false;
        }
    };

    var lastIndex = 0;
    var btns;
    var nb;
    var IDArry = new Array();  
    var authorize = {
        run: function () {
            IDArry = [];
            lastIndex = 0;

            $.ajax({
                type: "GET",
                async: false,
                url: "BuildNavBtnsColumns",
                success: function (data) {
                    btns = data;
                    //btns = eval("(" + data + ")");
                }
            });

            var role = mygrid.selected();
            if (!role) {
                msg.warning('请选择一个角色。');
                return false;
            }
            var ad = top.$.hDialog({
                max: true, title: '分配权限',
                content: '<div style="padding:2px;overflow:hidden;"><table id="nb"></table></div>',
                toolbar: [
                    { text: '全选', iconCls: 'icon-checkbox_yes', handler: function () { authorize.btnchecked(true); } },
                    { text: '取消全选', iconCls: 'icon-checkbox_no', handler: function () { authorize.btnchecked(false); } },
                    '-',
                    { text: '编辑全部', iconCls: 'icon-pencil', handler: function () { authorize.apply('beginEdit'); } },
                    { text: '取消编辑', iconCls: 'icon-pencil_delete', handler: function () { authorize.apply('cancelEdit'); } },
                    '-',
                    { text: '应用', iconCls: 'icon-disk_multiple', handler: function () { authorize.apply('endEdit'); } }
                ],
                submit: function ()
                {
                  
                    var data = authorize.getChanges(role);
                 //   $.log("XXXXXXXXXXXXXXXXXXXXXXXXXXX")
                    if (data.menus.length>0) {
                        $.ajaxtext("setRoleButtons", { Data:JSON.stringify( data) }, function (d)
                        { 

                            if (d.Success)
                            {
                                msg.ok(d.Message);
                                ad.dialog('close');
                              
                            } else
                            {
                                msg.warning(d.Message);
                            }
                        });
                    }else
                    {
                        top.$.messager.alert("提示","您未做任何修改！");
                    }
                }
            });
          
             nb = top.$('#nb').treegrid({
                title: '导航菜单',
                url: '/Role/GetNavigation',
                queryParams: { "roleID": role.ID },
                height: ad.dialog('options').height - 115,
                idField: 'ID',
                treeField: 'NavTitle',
                iconCls: 'icon-nav',
                nowrap: false,
                rownumbers: true,
                animate: true,
                collapsible: false,
                frozenColumns: [[{ title: '菜单名称', field: 'NavTitle', width: 200 }]],
                columns: [authorize.allBtns()],
                onClickRow: function (row)
                {
                   
                    if (lastIndex != row.ID) {
                        nb.treegrid('endEdit', lastIndex); 
                        //  IDArry 
                        if (lastIndex != 0)
                        { 
                            if (Enumerable.from(IDArry).where(function (p) { return p == lastIndex }).count() == 0)
                            {
                                IDArry.push(lastIndex)
                            }
                        }
                    }
                    authorize.apply('beginEdit', row.ID);
                    lastIndex = row.ID;
                    
                },
                onContextMenu: function (e, row) {
                    authorize.rowCmenu(e, row);
                }
            });
            return false;
        },
        rowCmenu: function (e, row) { //row 右键菜单
            var createRowMenu = function () {
                var rmenu = top.$('<div id="rmenu" style="width:100px;"></div>').appendTo('body');
                var menus = [{ title: '编辑并全选', iconCls: '' }, { title: '编辑', iconCls: 'icon-edit' }, '-',
                    { title: '全选', iconCls: '' }, { title: '取消全选', iconCls: '' }, '-',
                   { title: '取消编辑', iconCls: '' }, { title: '应用', iconCls: 'icon-ok' }];
                for (var i = 0; i < menus.length; i++) {
                    if (menus[i].title)
                        top.$('<div iconCls="' + menus[i].iconCls + '"/>').html(menus[i].title).appendTo(rmenu);
                    else {
                        top.$('<div class="menu-sep"></div>').appendTo(rmenu);
                    }
                }
            };

            e.preventDefault();
            if (top.$('#rmenu').length == 0) { createRowMenu(); }

            top.$('#nb').treegrid('select', row.ID);
            if (lastIndex != row.ID) { nb.treegrid('endEdit', lastIndex); }
            lastIndex = row.ID;

            top.$('#rmenu').menu({
                onClick: function (item) {
                    switch (item.text) {
                        case '全选': authorize.btnchecked(true); break;
                        case '取消全选': authorize.btnchecked(false); break;
                        case '编辑': authorize.apply('beginEdit', row.ID); break;
                        case '编辑并全选':
                            authorize.apply('beginEdit', row.ID);
                            authorize.btnchecked(true);
                            break;
                        case '取消编辑': authorize.apply('cancelEdit', row.ID); break;
                        case '应用': authorize.apply('endEdit', row.ID); break;
                        default:
                            break;
                    }
                }
            }).menu('show', { left: e.pageX, top: e.pageY });
        },
        allBtns: function () {
         
            window.authorize = authorize;
            Enumerable.from(btns).forEach("o=>o.formatter=function(v,d,i){ return  window.authorize.formatter(v,d,i,o.field);}");
            return btns;
        },
        formatter: function (v, d, i, field)
        {//按钮初始化
              
            //console.log(d)
            //console.log(i)
            //console.log(field)
            if (v)
            {
                if (v == '√')
                    return '<font color=\"#39CB00\"><b>' + v + '</b></font>';
                else
                    return v;
            }
            else
            { 
                var hasbut = Enumerable.from(d.OwnedBut).where(function (p)
                {
                    return p == field;
                }).count() > 0;

                return hasbut ? "<font color=\"#39CB00\"><b>√</b></font>" : "x";
            }
        },
        findCtrl: function (g, fieldname, ID) {
            return g.treegrid('getEditor', { id: ID, field: fieldname }).target;
        },
        btnchecked: function (flag) {

            var rows = top.$('#nb').treegrid('getSelections');
            if (rows) {
                $.each(rows, function (i, n) {
                    var editors = top.$('#nb').treegrid('getEditors', n.ID);
                    $.each(editors, function () {
                        if (!$(this.target).is(":hidden"))
                            $(this.target).attr('checked', flag);

                    });
                });
            } else {
                msg.warning('请选择菜单。');
            }
        },
        apply: function (action, ID)
        {
             
            if (!ID)
                top.$('#nb').treegrid('selectAll');

            var rows = top.$('#nb').treegrid('getSelections');

          
            $.each(rows, function (i, n)
            {
               
                top.$('#nb').treegrid(action, this.ID);
                if (action == 'beginEdit')
                {
                    var editors = top.$('#nb').treegrid('getEditors', n.ID);
                    Enumerable.from(editors).forEach(function (b)
                    {
                        Enumerable.from(btns).forEach(function (x, z)
                        { 
                            var hasbtn = Enumerable.from(n.Buttons).where(function (p) { return p == x.field }).count() > 0;
                         //   var ownedbtn = Enumerable.from(n.OwnedBut).where(function (p) { return p == x.field }).count() > 0; 
                            if (!hasbtn && b.field == x.field)
                            {
                                $(b.target).remove();
                            }
                            else if (b.field == x.field)
                            { 
                                if (b.oldHtml != "x")
                                {
                                    $(b.target).attr("checked", true);
                                } else
                                {
                                    $(b.target).attr("checked", false);
                                }
                              
                            }
                        });
                    });
                }
            });

            if (action != "beginEdit")
            {
                top.$('#nb').treegrid('clearSelections');
                if (lastIndex != 0)
                {
                    if (Enumerable.from(IDArry).where(function (p) { return p == lastIndex }).count() == 0)
                    {
                        IDArry.push(lastIndex)
                    }
                }
            }
        },
        getChanges: function (role)
        {
          
            var rows = top.$('#nb').treegrid('getChildren');

            var o = { roleId: role.ID, menus: [] };

            Enumerable.from(rows).forEach(function (x) {
                var n = { navid: x.ID, buttons: [] }; 
             
                n.buttons = Enumerable.from(x).where('t=>t.value=="√"').select('$.key').toArray();
                if (n.buttons.length != 0 || Enumerable.from(IDArry).where(function (p) { return p == x.ID }).count() > 0)
                {
                    o.menus.push(n);
                }
                

            });
            return  o;
        },
        setDeptments: function () { //设置部门权限
            var role = mygrid.selected();
            var dp = new DataPermission(role, actionUrl);
            dp.show();
        }
    };


    function createParam(action, ID) {
        var o = {};
        var query = top.$('#xiucaiForm').serializeArray();
        query = convertArray(query);
        o.jsonEntity = JSON.stringify(query);
        o.action = action;
        o.ID = ID;
        return "json=" + JSON.stringify(o);
    }

    
});


