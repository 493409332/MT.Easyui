/// <reference path="DateFormat.js" />
define("Conver", ["DateFormat"], function ()
{
    return {
        ChangeDateFormat: function (val)
        { 
            if (val != null)
            {
                var birthdayMilliseconds = parseInt(val.replace(/\D/igm, "")); 
                var date = new Date(birthdayMilliseconds);
            
                var datefmt = new DateFormat(); 
                return datefmt.format(date);
            }
            return "";

        }
    }
}
);