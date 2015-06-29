 
/// <reference path="../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../Scripts/avalon/avalon.js" />
/// <reference path="../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../Scripts/easyui1.3.3/jquery.easyui.all.js" />
/// <reference path="../../Scripts/easyui1.3.3/Xiucai.EasyUI.Extensions.js" />
/// <reference path="../../Scripts/admin/Search.js" />


require(["easyui", "jqueryAjax", "easyui_lang_zh_CN", "showICON", "Search"], function ()
{
    $.log("hehe");
    var pform = '<form  id="uiform" ><table cellpadding=5 cellspacing=0 width=100% align="center" class="grid2" border=0><tr><td align="right">';
    pform += '权限名称：</td><td><input name="ButtonText" id="txtPname" type="text" class="txt03 required" ></td></tr><tr><td align="right">';
    pform += '图标：</td><td><input name="iconcls" id="txticoncls" required="true" type="text" class="txt03" >&nbsp;<a id="selecticon" href="javascript:;" plain="true" class="easyui-linkbutton" icon="icon-search" title="选择图标"></a></td></tr><tr><td align="right">';
    pform += '权限代码：</td><td><input name="ButtonTag" id="txtPcode" type="text" class="txt03 required" ></td></tr><tr><td align="right">';
    pform += '排序：</td><td><input name="Sortnum" value="1" id="txtorderid" required="true" type="text"></td></tr><tr><td align="right">';
    pform += '权限说明：</td><td><textarea name="remark" rows="3" name=remark style="width:200px;height:50px;" class="txt03" id="txtremark" /></td></tr></table><input id="ID" type="hidden" name="ID" /></form>';





  
    
 
    var grid = {
        bind: function (winSize)
        {
            $('#btnGrid').datagrid({
                url: "GetPage",
                toolbar: '#toolbar',
                title: "系统按扭",
                iconCls: 'icon icon-list',
                width: winSize.width,
                height: winSize.height,
                nowrap: false, //折行
                rownumbers: true, //行号
                striped: true, //隔行变色
                idField: 'ID',//主键
                singleSelect: true, //单选
                frozenColumns: [[]],
                columns: [[
                    { title: 'ID', field: 'ID', width: 60, sortable: true },
                    { title: '图标', field: 'iconCls', width: 40,
                      formatter: function (v, d, i) {
                            return '<span class="icon ' + v + '">&nbsp;</span>';
                      },
                      align: 'center'
                    },
                    { title: '按钮名称', field: 'ButtonText', width: 100, sortable: true },
                    { title: '权限标识', field: 'ButtonTag', width: 80, sortable: true },
                    { title: '排序', field: 'Sortnum', width: 80, sortable: true },
                    { title: '说明', field: 'Remark', width: 300 }
                ]],
                pagination: true,
                pageSize: 20,
                pageList: [20, 40, 50]
            });
        },
        getSelectedRow: function ()
        {
            return $('#btnGrid').datagrid('getSelected');
        },
        reload: function ()
        {
             $('#btnGrid').datagrid('clearSelections').datagrid('reload', { filter: '' });
            //$('#btnGrid').datagrid('clearSelections').datagrid('reload', {
            //    filter: JSON.stringify( {
            //        "groupOp": "And", "rules": [{ "field": "ID", "op": "eq", "data": "1  ') AS [Extent1]  update   dbo.T_Button  set IsDelete='False' --" }]
            //    })
            //});
        }
    };

    function createParam(action, keyid)
    {
        var o = {};
        var query = top.$('#uiform').serializeArray();
        query = convertArray(query);
        o.jsonEntity = JSON.stringify(query);
        o.action = action;
        o.keyid = keyid;
        return "json=" + JSON.stringify(o);
    }

     
    var CRUD = {
        add: function ()
        {
            var hDialog = top.$.hDialog({
                title: '添加按钮', width: 350, height: 300, content: pform, iconCls: 'icon-add', submit: function ()
                {
                    if (top.$('#uiform').validate().form())
                    {
                        var query = createParam('add', '0');
                        $.ajaxjson("Add", top.$('#uiform').serializeArray(), function (d)
                        {
                            if (d.Success)
                            {
                                msg.ok(d.Message);
                                hDialog.dialog('close');
                                grid.reload();
                            } else
                            {
                                MessageOrRedirect(d.Message);
                            }
                        });
                    }
                    return false;
                }
            });
            top.$('#txtPname,#txtPcode').validatebox();
            top.$('#txtPname').val('');
            top.$('#txtPcode').val('');
            top.$('#txtorderid').numberspinner();
            showICON();
            top.$('#uiform').validate();
        },
        edit: function ()
        {
            var row = grid.getSelectedRow();
            if (row)
            {
                var hDialog = top.$.hDialog({
                    title: '编辑按钮', width: 350, height: 300, content: pform, iconCls: 'icon-save', submit: function ()
                    {
                        if (top.$('#uiform').validate().form())
                        {
                            //var query = createParam('edit', row.ID);;
                            $.ajaxjson("Update", top.$('#uiform').serializeArray(), function (d)
                            {
                                if (d.Success)
                                {
                                    msg.ok(d.Message);
                                    hDialog.dialog('close');
                                    grid.reload();
                                } else
                                {
                                    MessageOrRedirect(d.Message);
                                }
                            });
                        }
                        return false;
                    }
                });
                top.$('#uiform').validate();
                showICON();
                top.$('#ID').val(row.ID);
                top.$('#txtPname').val(row.ButtonText);
                top.$('#txtPcode').val(row.ButtonTag);
                top.$('#txticoncls').val(row.iconCls);
                top.$('#txtremark').val(row.Remark);
                top.$('#txtorderid').val(row.Sortnum).numberspinner();
            } else
            {
                msg.warning('请选择要修改的行。');
            }
        },
        del: function ()
        {
            var row = grid.getSelectedRow();
            if (row)
            { 
                var state=true;
                top.$.messager.confirm("提示", '确认要执行删除操作吗？',
                function (event)
                {

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
                });
                return state;

            } else
            {
                msg.warning('请选择要删除的行。');
            }
        }

    };


    autoResize({ dataGrid: '#btnGrid', gridType: 'datagrid', callback: grid.bind, height: 20, width: 8 });

    $('#a_edit').click(CRUD.edit);
    $('#a_delete').click(CRUD.del);
    $('#a_add').click(CRUD.add);
    $('#a_search').click(function ()
    {
        search.go('btnGrid');
    });

    /*导出EXCEL*/
    $('#a_export').click(function ()
    {
        var ee = new ExportExcel('btnGrid');
        ee.go();
    });

    //初始化搜索框
    function simpleSearch()
    {
        var columns = $('#btnGrid').datagrid('options').columns[0];
        $('#searchMenu').empty();
        $.each(columns, function (i, n)
        {
            $('#searchMenu').append('<div data-options="name:\'' + n.field + '\'">' + n.title + '</div>');
        });

        $('#ss').searchbox({
            menu: '#searchMenu',
            searcher: function (value, name)
            {
                if (value != '')
                {
                    var filter = { groupOp: 'And', rules: [{ field: name, op: 'eq', data: value }] };
                    $('#btnGrid').datagrid('reload', { filter: JSON.stringify(filter) });
                }
                else
                {
                    msg.warning('请输入关键字进行查询');
                }
            },
            prompt:"请输入查询值！"
        });
        $("#CZ").click(function ()
        {
            $('#searchMenu').empty();
            $('#ss').searchbox("setValue","");
            grid.reload();

        });
    }

    simpleSearch();

});