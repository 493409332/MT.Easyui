
/// <reference path="../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../Scripts/avalon/avalon.js" />
/// <reference path="../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../Scripts/easyui1.3.3/jquery.easyui.all.js" />

require(["avalon", "easyui", "jqueryAjax", "easyui_lang_zh_CN", "XiucaiEasyUIExtensions", "jqueryvalidate", "linq_jquery"], function () {

    var formUrl = '/ScriptsLogical/User/UserForm.html';
    var edit_formUrl = '/Sys/EditUserForm';
    var editpass_formUrl = '/Sys/EditPass';

    $(function () {
        var size = { width: $(window).width(), height: $(window).height() };
        mylayout.init(size);
        //deptree.init();
        autoResize({ dataGrid: '#userGrid', gridType: 'datagrid', callback: mygrid.databind, height: 36, width: 230 });

        $('#a_add').click(crud.add);
        $('#a_edit').click(crud.update);
        $('#a_delete').click(crud.del);
        $('#a_editPass').click(crud.editPass);
        $('#a_role').click(SetRoles);
        //$('#a_authorize').click(authorize.run);
        //$('#a_dataset').click(authorize.setDeptments);

        $('#a_search').click(function () {
            search.go('userGrid');
        });
        $(window).resize(function () {
            size = { width: $(window).width(), height: $(window).height() };
            mylayout.resize(size);
        });
    });

    var mylayout = {
        init: function (size) {
            $('#layout').width(size.width - 4).height(size.height - 4).layout();
            var center = $('#layout').layout('panel', 'center');
            center.panel({
                onResize: function (w, h) {
                    $('#userGrid').datagrid('resize', { width: w - 6, height: h - 36 });
                }
            });
        },
        resize: function (size) {
            mylayout.init(size);
            $('#layout').layout('resize');
        }
    };
    function createParam(action, ID) {
        var o = {};
        var form = top.$('#xiucaiForm');
        var query = '';
        if (form) {
            query = top.$('#xiucaiForm').serializeArray();
            query = convertArray(query);
            o.jsonEntity = JSON.stringify(query);
        }
        o.action = action;
        o.ID = ID;
        return "json=" + JSON.stringify(o);
    }


    var deptree = {
        init: function () {
            $('#deptree').tree({
                lines: true,
                url: "/Department/GetAllReplace",
                animate: true,
                onLoadSuccess: function (node, data) {
                    $('body').data('depData', data);
                },
                onClick: function (node) {
                    var depId = node.id;
                    var children = $('#deptree').tree('getChildren', node.target);
                    // alert(children.length);
                    var arr = [];
                    arr.push(depId);
                    for (var i = 0; i < children.length; i++) {
                        arr.push(children[i].id);
                    }

                    var strDepIds = arr.join(',');
                    $('#userGrid').datagrid('load', { filter: strDepIds });
                }
            });
        },
        data: function () {
            var d = JSON.stringify($('body').data('depData'));
            d = '[{"id":0,"text":"请选择部门"},' + d.substr(1);
            return JSON.parse(d);
        }

    };

    var mygrid = {
        databind: function (size) {
            $('#userGrid').datagrid({
                url: "/User/GetAllUser",
                width: size.width,
                height: size.height,
                idField: 'ID',
                singleSelect: true,
                striped: true,
                columns: [[
                    { title: 'ID', field: 'ID', width: 80, sortable: true, hidden: true },
                    { title: '用户名', field: 'UserName', width: 140, sortable: true },
                    { title: '真实姓名', field: 'TrueName', width: 120, sortable: true },
                    //{ title: '部门名称', field: 'DepartmentName', width: 160 },
                    { title: '邮箱', field: 'Email', width: 200, sortable: true },
                    {
                        title: '超管',
                        field: 'IsAdmin',
                        width: 80,
                        align: 'center',
                        formatter: function (v, d, i) {
                            if (d.UserName == "admin")
                                return '';
                            return '<img style="cursor:pointer" title="设置超管" onclick="javascript:window.setUserAttr(\'isadmin\',' + d.ID + ',' + v + ')" src="/Content/iconcss/icon/16/bullet_' + (v ? "tick.png" : "minus.png") + '" />';
                        }
                    },
                    {
                        title: '状态',
                        field: 'IsDisabled',
                        width: 80,
                        align: 'center',
                        formatter: function (v, d, i) {
                            if (d.UserName == "admin")
                                return '';
                            return '<img style="cursor:pointer" title="激活禁用帐号" onclick="javascript:window.setUserAttr(\'isdisabled\',' + d.ID + ',' + v + ')" src="/Content/iconcss/icon/16/bullet_' + (v == false ? "tick.png" : "minus.png") + '" />';
                        }
                    }, { title: '描述', field: 'Remark', width: 200 }
                ]],
                pagination: true,
                pageSize: 10,
                pageList: [20, 40, 50],
                rowStyler: function (index, row, css) {
                    if (row.UserName == "admin") {
                        return 'font-weight:bold;';
                    }
                }
            });
        },
        reload: function () {
            $('#userGrid').datagrid('clearSelections').datagrid('reload');
        },
        selectRow: function () {
            return $('#userGrid').datagrid('getSelected');
        }
    };
    //设置用户是否为超管及激活禁用帐号

    window.setUserAttr = function (action, uid, val) {
        //var query = createParam(action, uid) + '&val=' + val;
        var url;
        if (action == "isadmin") {
            url = "/User/SetAdmin";
        }
        if (action == "isdisabled") {
            url = "/User/SetEnabled";
        }
        $.ajaxjson(url, { UserID: uid, val: val }, function (d) {
            if (d.Success) {
                mygrid.reload();
            } else {
                MessageOrRedirect(d);
            }
        });
    }


    var crud = {
        initData: function (depid) {
            top.$('#txt_isdisabled,#txt_isadmin').combobox({ panelHeight: 'auto' });
            //var _depid = depid || 0;
            //top.$('#txt_department').combotree({
            //    data: deptree.data(),
            //    valueField: 'id',
            //    textField: 'text',
            //    value: _depid
            //});
            top.$('#txt_role').combobox({ multiple: true, valueField: 'ID', textField: 'RoleName' });

            $.getJSON("/User/GetAllRole", function (d) {
                top.$('#txt_role').combobox({ data: d });
                (function (roles) {
                    $.each(roles, function (i, n) {
                        if (n.IsDefault == 1)
                            top.$('#txt_role').combobox('setValue', n.ID);
                    });
                }(d));
            });

            top.$('#userTab').tabs({
                onSelect: function () {
                    top.$('.validatebox-tip').remove();
                }
            });

            top.$('#txt_passSalt').val(randomString());
        },
        add: function () {
            var addDialog = top.$.hDialog({
                href: "/ScriptsLogical/User/UserForm.html" + '?v=' + Math.random(),
                width: 500,
                height: 400,
                title: '新建帐号',
                iconCls: 'icon-user_add',
                onLoad: function () {
                    //var dep = $('#deptree').tree('getSelected');
                    //var depID = 0;
                    //if (dep)
                    //    depID = dep.id || 0;

                    //如果左侧有选中部门，则添加的时候，部门默认选中
                    crud.initData();
                },
                closed: false,
                submit: function () {
                    var tab = top.$('#userTab').tabs('getSelected');
                    var index = top.$('#userTab').tabs('getTabIndex', tab);
                    if (top.$('#xiucaiForm').form('validate')) {
                        $.ajaxjson("Add", { role: top.$('#txt_role').combo('getValues') }, function (d) {
                            if (d.Success) {
                                msg.ok('添加成功');
                                mygrid.reload();
                                addDialog.dialog('close');
                            } else {
                                if (d.Data == -2) {
                                    msg.error('用户名已存在，请更改用户名。');
                                    if (index > 0)
                                        top.$('#userTab').tabs('select', 0);
                                    top.$('#txt_username').select();
                                } else {
                                    MessageOrRedirect(d);
                                }
                            }
                        });
                    } else {
                        if (index > 0)
                            top.$('#userTab').tabs('select', 0);
                    }
                }
            });

        },
        update: function () {
            var row = mygrid.selectRow();
            if (row) {
                var editDialog = top.$.hDialog({
                    href: "/ScriptsLogical/User/EditUserForm.html" + '?v=' + Math.random(),
                    width: 500,
                    height: 400,
                    title: '修改帐号',
                    iconCls: 'icon-user_add',
                    onLoad: function () {
                        crud.initData();
                        top.$("#ID").val(row.ID);
                        top.$("#txt_username").val(row.UserName);
                        top.$("#txt_truename").val(row.TrueName);
                        top.$("#txt_mobile").val(row.Mobile);
                        top.$("#txt_email").val(row.Email);
                        top.$("#txt_qq").val(row.QQ);
                        top.$("#txt_remark").val(row.Remark);
                        //top.$('#txt_department').combotree('setValue', row.DepartmentId);
                        //top.$('#txt_department').combotree('setText', row.DepartmentName);
                        top.$('#txt_isadmin').combobox('setValue', row.IsAdmin.toString());
                        top.$('#txt_isdisabled').combobox('setValue', (row.IsDisabled).toString());
                    },
                    submit: function () {
                        if (top.$('#xiucaiForm').form('validate')) {
                            var quer = top.$('#xiucaiForm').serializeArray();
                            $.ajaxjson("Update", quer, function (d) {
                                if (d.Success) {
                                    msg.ok('修改成功');
                                    mygrid.reload();
                                    editDialog.dialog('close');
                                } else {
                                    if (d.Data == -2) {
                                        msg.error('用户名已存在，请更改用户名。');
                                        if (index > 0)
                                            top.$('#userTab').tabs('select', 0);
                                        top.$('#txt_username').select();
                                    } else {
                                        MessageOrRedirect(d);
                                    }
                                }
                            });
                        } else {
                            var tab = top.$('#userTab').tabs('getSelected');
                            var index = top.$('#userTab').tabs('getTabIndex', tab);
                            if (index > 0)
                                top.$('#userTab').tabs('select', 0);
                        }
                    }
                });
            } else {
                msg.warning('请选择要修改的用户。');
            }
        },
        del: function () {
            var row = mygrid.selectRow();
            if (row) {
                if (row.UserName == "admin") {
                    msg.warning('admin为系统帐号，不能删除！');
                    return false;
                }
                //var query = createParam('delete', row.ID);
                top.$.messager.confirm('删除帐号', '确认要删除选中的用户吗?', function (r) {
                    if (r) {
                        $.ajaxjson("/User/Delete", { ID: row.ID }, function (d) {
                            if (d.Success) {
                                msg.ok('删除成功');
                                mygrid.reload();
                            } else {
                                if (d.Data == -2)
                                    msg.error('admin为系统帐号，不能删除。');
                                else {
                                    MessageOrRedirect(d);
                                }
                            }
                        });
                    }
                });
            } else {
                msg.warning('请选择要删除的用户。');
            }
        },
        editPass: function () {
            var row = mygrid.selectRow();
            if (row) {
                var pDialog = top.$.hDialog({
                    href: editpass_formUrl,
                    title: '修改密码',
                    width: 300,
                    height: 200, iconCls: 'icon-key',
                    onLoad: function () {

                    },
                    submit: function () {
                        if (top.$('#xiucaiForm').form('validate')) {
                            var query = createParam('editpass', row.ID) + '&password=' + top.$('#txt_newpass').val();
                            $.ajaxtext(actionUrl, query, function (d) {
                                if (d > 0) {
                                    msg.ok('亲，密码修改成功。');
                                    pDialog.dialog('close');
                                } else {
                                    msg.error('亲，密码修改失败啦~~');
                                }
                            });
                        }
                    }
                });
            } else {
                msg.warning('请选择帐号。');
            }
        }
    };



    function SetRoles() { //设置角色
        var row = mygrid.selectRow();
        if (row) {
            var rDialog = top.$.hDialog({
                href: '/ScriptsLogical/User/SetRoles.html', width: 600, height: 500, title: '设置帐号角色', iconCls: 'icon-group',
                onLoad: function () {
                    top.$('#rlayout').layout();
                    top.$('#uname').text(row.UserName);
                    top.$('#allRoles,#selectedRoles').datagrid({
                        width: 400,
                        height: 350,
                        iconCls: 'icon-group',
                        nowrap: false, //折行
                        rownumbers: true, //行号
                        striped: true, //隔行变色
                        idField: 'ID',//主键
                        singleSelect: true, //单选
                        columns: [[
                            { title: '角色名称', field: 'RoleName', width: 140 },
                            { title: '备注', field: 'Remark', width: 210 }
                        ]],
                        pagination: false,
                        pageSize: 20,
                        pageList: [20, 40, 50]
                    });

                    top.$('#allRoles').datagrid({
                        url: '/User/GetAllRole',
                        onDblClickRow: function (rowIndex, rowData) {
                            top.$('#a_select_role').click();
                        }
                    });

                    top.$('#selectedRoles').datagrid({
                        url: '/User/GetRoleByUserID?UserID=' + row.ID,
                        onDblClickRow: function (rowIndex, rowData) {
                            top.$('#selectedRoles').datagrid('deleteRow', rowIndex);
                        }
                    });
                    top.$('#a_select_role').click(function () {
                        var _row = top.$('#allRoles').datagrid('getSelected');
                        if (_row) {
                            var hasRoleName = false;
                            var roles = top.$('#selectedRoles').datagrid('getRows');
                            $.each(roles, function (i, n) {
                                if (n.RoleName == _row.RoleName) {
                                    hasRoleName = true;
                                }
                            });
                            if (!hasRoleName)
                                top.$('#selectedRoles').datagrid('appendRow', _row);
                            else {
                                alert('角色已存在，请不要重复添加。');
                                return false;
                            }
                        } else {
                            alert('请选择角色');
                        }
                    });

                    top.$('#a_delete_role').click(function () {
                        var trow = top.$('#selectedRoles').datagrid('getSelected');
                        if (trow) {
                            var rIndex = top.$('#selectedRoles').datagrid('getRowIndex', trow);
                            top.$('#selectedRoles').datagrid('deleteRow', rIndex).datagrid('unselectAll');
                        } else {
                            alert('请选择角色');
                        }
                    });
                },
                submit: function () {
                    var selectedRoles = top.$('#selectedRoles').datagrid('getRows');
                    var roleIdArr = [];
                    $.each(selectedRoles, function (i, n) {
                        roleIdArr.push(n.ID);
                    });
                    //var query = createParam("setroles", row.ID) + '&roles=' + roleIdArr.join(',');
                    $.ajaxtext("/User/SetRole", { UserID: row.ID, roleids: roleIdArr.join(',') }, function (d) {
                        if (d > 0) {
                            msg.ok('亲,角色设置成功啦!');
                            rDialog.dialog('close');
                        } else {
                            alert(':( 设置失败啦。');
                        }
                    });
                }
            });
        } else {
            msg.warning('亲，请选择一个帐号哦！');
        }
    }
})