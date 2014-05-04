/**
 * Created by Sergio on 8/04/14.
 */

function activateMenu(id, submenu)
{
        $('#' + id).addClass('active');
        var img = $('#' + id + ' i.fa-angle-left');
        img.addClass('fa-angle-down');
        img.removeClass('fa-angle-left');
        $('#'+ id +' ul').show();

        if(submenu != undefined)
        {
            $('#' + submenu).addClass('active');
        }
}