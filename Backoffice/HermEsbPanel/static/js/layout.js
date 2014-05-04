function changeEnvironment(env)
{
    $('#selectEnvironment').text(env);
    environment = env;
    createCookie('environment', env, null);
    reload();
}

function createCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
       expires = "; expires=" + date.toGMTString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}
