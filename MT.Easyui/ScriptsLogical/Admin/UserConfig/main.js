/// <reference path="../../../Scripts/jquery/jquery-1.8.2.js" />
/// <reference path="../../../Scripts/avalon/avalon.js" />
/// <reference path="../../../Scripts/admin/jQuery.Ajax.js" />
/// <reference path="../../../Scripts/easyui1.3.3/jquery.easyui.all.js" />
/// <reference path="../../../Scripts/easyui1.3.3/Xiucai.EasyUI.Extensions.js" />
/// <reference path="../../../Scripts/Linqjs/linq.js" />
/// <reference path="../../../Scripts/Linqjs/linq.jquery.js" />
require(["avalon", "easyui", "easyui_lang_zh_CN", "jqueryAjax", "XiucaiEasyUIExtensions"], function ()
{
 
    //系统全局设置
    var _data = {
        theme: [{ "title": "默认皮肤", "name": "default" },
            { "title": "流行灰", "name": "gray" },
            { "title": "Metro", "name": "metro" },
            { "title": "黑色", "name": "black" },
            { "title": "Bootstrap", "name": "bootstrap" }
        ],
        navType: [{ "id": "Accordion", "text": "手风琴(2级)", "selected": true }, { "id": "Accordion2", "text": "手风琴大图标(2级)" }, { "id": "tree", "text": "树形结构" }]
    };

    function initCtrl()
    {
        $('#txt_theme').combobox({
            data: _data.theme, panelHeight: 'auto', editable: false, valueField: 'name', textField: 'title'
        });

        $('#txt_nav_showtype').combobox({
            data: _data.navType, panelHeight: 'auto', editable: false, valueField: 'id', textField: 'text', width: 180,
            onSelect: function (item)
            {
                $('#imgPreview').attr('src', '/images/menustyles/' + item.id + '.png');
            }
        });

        $('#imgPreview').click(function ()
        {
            var src = $(this).attr('src');
            top.$.hDialog({
                content: '<img src="' + src + '" />',
                width: 665,
                height: 655,
                title: '效果图预览',
                showBtns: false
            });
        });

        $('#txt_grid_rows').val(20).numberspinner({ min: 10, max: 500, increment: 10 });

        if (sys_config)
        {
            $('#txt_theme').combobox('setValue', sys_config.theme.name);
            $('#txt_nav_showtype').combobox('setValue', sys_config.showType);
            $('#txt_grid_rows').numberspinner('setValue', sys_config.gridRows);
            $('#imgPreview').attr('src', '/images/menustyles/' + sys_config.showType + '.png');
        }
    }

    $(function ()
    {
        initCtrl();
        $('#btnok').click(saveConfig);

        $('body').css('overflow', 'auto');



    });

    function saveConfig()
    {
        var theme = $('#txt_theme').combobox('getValue');
        var navtype = $('#txt_nav_showtype').combobox('getValue');
        var gridrows = $('#txt_grid_rows').numberspinner('getValue');

        var findThemeObj = function ()
        {
            var obj = null;
            $.each(_data.theme, function (i, n)
            {
                if (n.name == theme)
                    obj = n;
            });
            return obj;
        };
        var configObj = { theme: findThemeObj(), showType: navtype, gridRows: gridrows };

        var str = JSON.stringify(configObj);

        $.ajaxtext('SetConfig', { json:str}, function (d)
        {
            if (d == 1)
                msg.ok('恭喜，全局设置保存成功,按F5看效果');
            else
                MessageOrRedirect("保存失败！");
        });
    }
})