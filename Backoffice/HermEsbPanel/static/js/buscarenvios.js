/**
 * Created by abalarezr on 8/04/14.
 */

function BuscarEnvioLocalizador()
{
    $('#div_envio_loading').show();
    $('#div_envio_result').html('');
    $.post("/buscarenvio/localizador/" + $('#txtIdEnvio').val()).done(function( data ){
         $('#div_envio_result').html(data);
         $('#div_envio_loading').hide();
    });

}

function SeguimientoEnvioLocalizador()
{
    $('#div_envio_loading').show();
    $('#div_envio_error').hide();
    $('#div_envio_result').html('');
    $.post("/seguimientoenvio/localizador/" + $('#txtIdEnvio').val()).done(function( data ){
         $('#div_envio_result').html(data);
         $('#div_envio_loading').hide();
    }).error(function(data){
        $('#div_envio_loading').hide();
        $('#div_envio_error').show();
    });

}
