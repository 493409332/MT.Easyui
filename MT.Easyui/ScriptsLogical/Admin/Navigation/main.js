/// <reference path="../../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../../Scripts/avalon/avalon.js" />
/// <reference path="../../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../../Scripts/easyui1.3.3/jquery.easyui.all.js" />
/// <reference path="../../../Scripts/easyui1.3.3/Xiucai.EasyUI.Extensions.js" />
/// <reference path="../../../Scripts/Linqjs/linq.js" />
/// <reference path="../../../Scripts/Linqjs/linq.jquery.js" />


require(["easyui", "jqueryAjax", "easyui_lang_zh_CN", "linq", "linq_jquery"], function ()
{
    $.log("hehe");
    var navgrid;

  

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
           // console.log(1)
            return navgrid.treegrid('getSelected');
        }
    };


    var crud = {
        formUrl: "/ScriptsLogical/Admin/Navigation/NavForm.html?n=" + Math.random(),
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
                        top.$('#GuID').val(row.GuID);
                      
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
                    href: '/ScriptsLogical/Admin/Navigation/SetNavButtons.html?n=' + Math.random(),
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