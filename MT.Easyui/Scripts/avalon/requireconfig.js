

avalon.config.debug = false;
require.config({
    baseUrl: "/",
    debug: false,
    paths: {
        jquery1: "Scripts/easyui1.3.3/jquery-1.7.2.min",
        jquery: "Scripts/jquery/jquery-1.8.2",
        easyui: "Scripts/easyui1.3.3/jquery.easyui.all",
        jqueryAjax: "Scripts/admin/jQuery.Ajax.js",
        XiucaiEasyUIExtensions: "Scripts/easyui1.3.3/Xiucai.EasyUI.Extensions",
        json2: "Scripts/json/json2",
        jqueryvalidate: "Scripts/jquery/jquery.validate",
        easyui_lang_zh_CN: "Scripts/easyui1.3.3/locale/easyui-lang-zh_CN",
        showICON: "ScriptsLogical/Common/showICON",
        Search: "Scripts/admin/Search",
        linq: "Scripts/Linqjs/linq",
        linq_jquery: "Scripts/Linqjs/linq.jquery"
    },
    shim: {
        jquery: {
            exports: "jquery"
        },
        jqueryAjax: {
            deps: ["jquery"],
            exports: "jqueryAjax"
        },
        easyui: {
            deps: ["jquery", "css!Content/iconcss/icon.css", "css!Scripts/easyui1.3.3/themes/bootstrap/easyui.css"],
            exports: "easyui"
        }
        , easyui_lang_zh_CN: {
            deps: ["easyui"],
            exports: "easyui_lang_zh_CN"
        }, XiucaiEasyUIExtensions: {
            deps: ["easyui"],
            exports: "XiucaiEasyUIExtensions"
        }, json2: {
            exports: "json2"
        }, jqueryvalidate: {
            deps: ["jquery"],
            exports: "jqueryvalidate"
        },
        Search: { exports: "Search" },
        linq: {
            exports: "linq"
        },
        linq_jquery: {
            deps: ["linq", "jquery"],
            exports: "linq_jquery"
        }
    }
   , urlArgs: "bust=" + (new Date()).getTime()
});