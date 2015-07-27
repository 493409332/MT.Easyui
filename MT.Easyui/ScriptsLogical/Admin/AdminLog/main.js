/// <reference path="../../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../../Scripts/avalon/avalon.js" />
/// <reference path="../../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../../Scripts/easyui1.3.3/jquery.easyui.all.js" />
/// <reference path="../../../Scripts/easyui1.3.3/Xiucai.EasyUI.Extensions.js" />
/// <reference path="../../../Scripts/Linqjs/linq.js" />
/// <reference path="../../../Scripts/Linqjs/linq.jquery.js" />

require(["Conver", "easyui", "jqueryAjax", "easyui_lang_zh_CN", "datagrid_detailview", "Search"], function (Conver)
{
   


    var grid = {
        bind: function (winSize)
        {
            $('#LogGrid').datagrid({
                url: "GetPage",
                toolbar: '#toolbar',
                title: "系统操作日志",
                iconCls: 'icon icon-list',
                width: winSize.width,
                height: winSize.height,
                nowrap: false, //折行
                rownumbers: true, //行号
                striped: true, //隔行变色
                idField: 'GuID',//主键
                singleSelect: true, //单选
                fitColumns: true,
                frozenColumns: [[]],
                columns: [[
                    { title: 'GuID', field: 'GuID', width: 10, sortable: true, hidden: true },
                    //{
                    //    title: '图标', field: 'iconCls', width: 40,
                    //    formatter: function (v, d, i)
                    //    {
                    //        return '<span class="icon ' + v + '">&nbsp;</span>';
                    //    },
                    //    align: 'center'
                    //},
                    { title: '执行类名', field: 'RunClassName', width: 40, sortable: true },
                    {
                        title: '操作类型', field: 'OperationType', width: 20, sortable: true, formatter: function (v, d, i)
                        {
                            var operationtype_str;
                            switch (v)
                            {
                                case 1:
                                    operationtype_str = "新增";
                                    break;
                                case 2:
                                    operationtype_str = "逻辑删除";
                                    break;
                                case 4:
                                    operationtype_str = "修改";
                                    break;
                                case 16:
                                    operationtype_str = "物理删除";
                                    break;
                                case 17:
                                    operationtype_str = "分配角色";
                                    break;
                                case 18:
                                    operationtype_str = "设置角色菜单按钮关系";
                                    break;
                                case 19:
                                    operationtype_str = "设置菜单按钮关系";
                                    break;
                                case 20:
                                    operationtype_str = "用户配置修改";
                                    break;
                                default:
                                    operationtype_str = "未知操作";
                                    break; 
                            }
                            return operationtype_str;

                        }
                    },
                    {
                        title: '操作时间', field: 'OperationTime', width: 20, sortable: true, formatter: function (v, d, i)
                        {

                            return Conver.ChangeDateFormat(v);

                        }
                    },
                    { title: '操作人ID', field: 'UserID', width: 20,sortable: true },
                    { title: '影响记录行数', field: 'SaveChangesint', width: 20, sortable: true },
                    { title: '操作人名字', field: 'UserName', width: 20, sortable: true },
                    { title: '操作人角色', field: 'PurviewName', width: 20, sortable: true }

                ]],
                pagination: true,
                pageSize: usergridRows,
                pageList: pageList
                , view: detailview,
                detailFormatter: function (rowIndex, rowData)
                {
                    return JSON.stringify(rowData.Model).replace(/</g, "&lt;").replace(/>/g, "&gt;");
                }

            });

        },
        reload: function ()
        {
            $('#LogGrid').datagrid('clearSelections').datagrid('reload', {});
         
        }

    };
   

    var VM= avalon.define({
        $id: "AdminLogcontroller",
        clear_log: function ()
        {
            var state=true;
            top.$.messager.confirm("提示", '确认要执行清空日志操作吗？改操作不可恢复！',
            function (event)
            { 
                if (event)
                {
                    $.ajax({
                        async: false,
                        type: "post",
                        url: "Delete",
                        success: function (data)
                        {
                            console.log(data)
                            if (data)
                            {
                                grid.reload();
                            }
                        }
                    });
                } else
                {
                    state = false;
                }
            });

            return state;
             
        }
        ,search_log : function ()
        {
           
            $('#LogGrid').datagrid('clearSelections').datagrid('reload', { filter: '', runclass: $("#runclass").combobox("getValue") || "", username: $("#username").combobox("getValue") || "" });

        }
        , arrow_undo: function ()
        { 
            grid.reload();
            $("#runclass").combobox("setValue", "");
            $("#username").combobox("setValue", "");
        }

    });
    avalon.scan();

    //avalon.log(VM)

    autoResize({ dataGrid: '#LogGrid', gridType: 'datagrid', callback: grid.bind, height: 20, width: 8 });
   // var init = {
   //     ClassCombo: $.ajax({
   //         async: false,
   //         type: "post",
   //         url: "GetClasslist",
   //         success: function (data)
   //         {
   //             $('#ClassCombo').combobox({
   //                 data: data, panelHeight: 'auto', editable: false, valueField: 'value', textField: 'name'
   //             });
   //         }
   //     })

   // }; 
   //init.ClassCombo();

});