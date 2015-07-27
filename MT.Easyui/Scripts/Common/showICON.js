define("showICON", [], function ()
{
    window.showICON = function ()
    {
        top.$('#selecticon').click(function ()
        {
            var iWin = top.$.hWindow({
                iconCls: 'icon-application_view_icons',
                href: '/Content/iconcss/iconlist.htm?v=' + Math.random(), title: '图标选取', width: 800, height: 600, submit: function ()
                {
                    top.$('#i').window('close');
                }, onLoad: function ()
                {
                    top.$('#iconlist li').attr('style', 'float:left;border:1px solid #fff; line-height:20px; margin-right:4px;width:16px;cursor:pointer')
                    .click(function ()
                    {
                        top.$('#txticoncls').val(top.$(this).find('span').attr('class').split(" ")[1]);
                        iWin.window('close');
                    }).hover(function ()
                    {
                        top.$(this).css({ 'border': '1px solid red' });
                    }, function ()
                    {
                        top.$(this).css({ 'border': '1px solid #fff' });
                    });
                }
            });
        });

    } 
}
);