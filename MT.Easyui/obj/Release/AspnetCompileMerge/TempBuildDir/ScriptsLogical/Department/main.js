﻿
require(["avalon", "easyui", "easyui_lang_zh_CN", "jqueryAjax", "XiucaiEasyUIExtensions", "jqueryvalidate"], function ()
{
    var formHtml = '<form  id="xiucaiForm"><table class="grid">';
    formHtml += '<tr><td>部门名称：</td><td><input type="text" class="txt03 required" name="DepartmentName" id="txtgroupname" /></td></tr>';
    formHtml += '<tr><td>上级部门：</td><td><input style="width:200px" id="txtparentid" name="parentid" /></td></tr>';
    formHtml += '<tr><td>排序：</td><td><input id="txtsortnum" name="sortnum" value="0" /></td></tr>';
    formHtml += '<tr><td>备注：</td><td><textarea tip="备注信息"  style="width:80%;height:50px;" class="txt03" rows=5 id="txtremark" name="remark"></textarea></td></tr>';
    formHtml += '</table><input id="ID" type="hidden" name="ID"/></form>';

    $(function () {
        autoResize({ dataGrid: '#depGrid', gridType: 'treegrid', callback: mygrid.bindGrid, height: 5 });
        $('#a_add').click(crud.add);
        $('#a_setbtn').click(crud.setButtons);
        $('#a_edit').click(crud.edit);
        $('#a_delete').click(crud.del);
    });



    var mygrid = {
        bindGrid: function (size) {
            $('#depGrid').treegrid({
                toolbar: '#toolbar',
                title: '部门列表',
                iconCls: 'icon icon-list',
                width: size.width,
                height: size.height,
                nowrap: false,
                rownumbers: true,
                animate: true,
                collapsible: false,
                singleSelect: true, //单选
                url: "GetAll",
                idField: 'ID',
                treeField: 'DepartmentName',
                columns: [[
                    { title: 'ID', field: 'ID', width: 100, sortable: true,hidden:true},
                    { title: '部门名称', field: 'DepartmentName', width: 200 },
                    { title: '排序', field: 'Sortnum', width: 60, align: 'center' },
                    { title: '备注', field: 'Remark', width: 240 }
                ]],
                pagination: true,
                pageSize: 20,
                pageList: [20, 40, 50]
            });
        },
        getSelectedRow: function () {
            return $('#depGrid').datagrid('getSelected');
        },
        reload: function () {
            $('#depGrid').datagrid('clearSelections').datagrid('reload', { filter: '' });
        }
    }

    var crud = {
        createParam: function (action, ID) {
            var o = {};
            var query = top.$('#xiucaiForm').serializeArray();
            query = convertArray(query);
            o.jsonEntity = JSON.stringify(query);
            o.action = action;
            o.ID = ID;
            return "json=" + JSON.stringify(o);
        },
        initCtrl: function (depId) {
            var treedata = $('#depGrid').treegrid("getData");
            var str = JSON.stringify(treedata);
            str = str.replace(/DepartmentName/g, "text").replace(/ID/g, "id");
            str = '[{"text":"请选择上级部门",id:0,"selected":true},' + str.substr(1);
            if (treedata) {
                top.$('#txtparentid').combotree({
                    data: eval(str),
                    onSelect: function (item) {
                        if (item.id == depId) {
                            var pId = top.$('#txtparentid').combotree('getValue');
                            top.$('#txtparentid').combotree('setValue', pId);
                            alert("上级部门不能与当前部门相同。");
                            return false;
                        }
                    }
                });
            }
            top.$('#txtsortnum').numberspinner({ min: 0 });
            top.$('#xiucaiForm').validate();
        },
        add: function () {
            var addDialog = top.$.hDialog({
                content: formHtml, height: 300, title: "增加部门", iconCls: 'icon-add',
                submit: function () {
                    if (top.$('#xiucaiForm').validate().form()) {
                        //var query = crud.createParam('add', 0);
                        $.ajaxjson("Add", top.$('#xiucaiForm').serializeArray(), function (d) {
                            if (d.Success) {
                                $('#depGrid').treegrid("reload");
                                addDialog.dialog('close');
                                msg.ok(d.Message);
                            } else {
                                MessageOrRedirect(d);
                            }
                        });
                    }
                    return false;
                }
            });

            crud.initCtrl();

            var row = $('#depGrid').treegrid('getSelected');
            if (row) {
                var childrennodes = $('#depGrid').treegrid('getChildren', row.ID);

                if (childrennodes.length == 0)
                    top.$('#txtparentid').combotree('setValue', row.ParentId);
                else
                    top.$('#txtparentid').combotree('setValue', row.ID);
            } else {
                top.$('#txtparentid').combotree('setValue', 0);
            }
        },
        edit: function () {
            var row = $('#depGrid').treegrid('getSelected');
           
            if (row) {
                var editDialog = top.$.hDialog({
                    content: formHtml, height: 300, title: "修改部门", iconCls: 'icon-edit',
                    submit: function () {
                        if (top.$('#xiucaiForm').validate().form()) {

                            var childrennodes = $('#depGrid').treegrid('getChildren', row.ID);
                            var newparentid = top.$('#txtparentid').combotree('getValue');

                            var i = 0;
                            $.each(childrennodes, function () {
                                if (this.ID == newparentid)
                                    i++;
                            });

                            if (i > 0) {
                                alert('上级部门为当前部门的子级。');
                                return false;
                            }

                            //var query = crud.createParam('edit', row.ID);
                            var data = top.$('#xiucaiForm').serializeArray();
                            $.ajaxjson("Update", data, function (d) {
                                if (d.Success) {
                                    $('#depGrid').treegrid("reload");
                                    editDialog.dialog('close');
                                    msg.ok(d.Message);
                                } else {
                                    MessageOrRedirect(d);
                                }
                            });
                        }
                        return false;
                    }
                });
                top.$('#ID').val(row.ID);
                crud.initCtrl(row.ID);
                top.$('#txtgroupname').val(row.DepartmentName);
                top.$('#txtparentid').combotree('setValue', row.ParentId);
                top.$('#txtsortnum').numberspinner('setValue', row.Sortnum);
                top.$('#txtremark').val(row.Remark);

            } else {
                msg.warning('请选择要修改的部门.');
                return false;
            }
            return false;
        },
        del: function () {
            var row = $('#depGrid').treegrid('getSelected');
            if (row) {
                var childrensCount = $('#depGrid').treegrid('getChildren', row.ID).length;

                if (childrensCount == 0) {
                    if (confirm('确认要删除此部门吗？')) {
                        $.ajaxjson("Delete", { ID: row.ID }, function (d) {
                            if (d.Success) {
                                $('#depGrid').treegrid("reload");
                                msg.ok(d.Message);
                            } else {
                                MessageOrRedirect(d);
                            }
                        });
                    }
                } else {
                    msg.warning('当前部门拥有下级部门，不允许删除.');
                    return false;
                }
            }
            else {
                msg.warning('请选择要删除的部门.');
            }
            return false;
        }
    }
})