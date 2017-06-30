
var RSAClient = {
    Encryt: function (dataArray) {
        var result = { encryptData: null, tmpToken: null };
        $.ajax({
            type: "GET",
            url: g_appPath + "/RSA/getRSAKey?t=" + new Date().getTime(),
            async: false,
            success: function (msg) {
                var keys = msg.split('|');
                setMaxDigits(129);
                var key = new RSAKeyPair(keys[0], "", keys[1]);
                result.tmpToken = keys[2];

                var arr = new Array();
                dataArray.forEach(function (element) {
                    var eData = encryptedString(key, element);
                    arr.push(eData);
                });

                result.encryptData = arr;
            },
            error: function (e) {

            }
        });
        return result;
    }
}
