/**
 * Created by abalarezr on 8/04/14.
 */

function VersionadorG2EnvioLocalizador()
{
    $('#div_envio_loading').show();
    $('#div_envio_result').html('');
    $.post("/integraciong2/versionadorg2/regenerar/" + $('#txtIdEnvio').val()).done(function( data ){
         $('#div_envio_result').html(data);
         $('#div_envio_loading').hide();
    });

}
