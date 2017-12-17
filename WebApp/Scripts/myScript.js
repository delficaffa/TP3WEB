$(document).ready(function () {
    $("#send").click( function () {
        $(".error").hide();

        //Validaciones de nombre
        var firstName = $("#name");

        if (firstName.val() == "") {
            $('#nombre_error').html("Nombre es un campo obligatorio");
            $("#nombre_error").fadeIn('fast');
            $("#name").focus();
            return false;
        } else if (!firstName.val().match(/^[a-zA-Z Áa Éé Íí Óó Úú Üü]+$/)) {
            $('#nombre_error').html("El Nombre no puede contener numeros");
            $("#nombre_error").fadeIn('fast');
            $("#name").focus();
            return false;
        } else if (firstName.val().length < 3) {
            $('#nombre_error').html("Formato de Nombre muy corto");
            $("#nombre_error").fadeIn('fast');
            $("#name").focus();
            return false;
        }


        //Validaciones de apellido
        var lastName = $("#surname");

        if (lastName.val() == "") {
            $('#apellido_error').html("Apellido es un campo obligatorio");
            $("#apellido_error").fadeIn('fast');
            $("#surname").focus();
            return false;
        } else if (!lastName.val().match(/^[a-zA-Z Áa Éé Íí Óó Úú Üü]+$/)) {
            $('#apellido_error').html("El Apellido no puede contener numeros");
            $("#apellido_error").fadeIn('fast');
            $("#surname").focus();
            return false;
        } else if (lastName.val().length < 3) {
            $('#apellido_error').html("Formato de Apellido muy corto");
            $("#apellido_error").fadeIn('fast');
            $("#surname").focus();
            return false;
        }

        //Validaciones de paises
        var country = $("#country");
        var value = country.val();

        if (parseInt(country.val()) == -1) {
            $('#pais_error').html("Debe seleccionar un país");
            $("#pais_error").fadeIn('fast');
            $("#country").focus();
            return false;
        } 

        //Validaciones de fecha de ingreso
        var entry = $("#entry");

        
        //Validaciones de Precio
        var price = $("#Price");
        var value = price.val();

        if (!($.isNumeric(parseFloat(value)))) {
            $('#price_error').html("El precio no puede contener letras");
            $("#price_error").fadeIn('fast');
            $("#price").focus();
            return false;
        } else if (value.match(/^[a-zA-Z Áa Éé Íí Óó Úú Üü]+$/)) {
            $('#price_error').html("El precio no puede contener letras");
            $("#price_error").fadeIn('fast');
            $("#price").focus();
            return false;
        } else if ((value * 1) < 0) {
            $('#price_error').html("El precio por hora no puede ser negativo");
            $("#price_error").fadeIn('fast');
            $("#price").focus();
            return false;
        }

        var ctrl = document.formCreate.Turn;
               
            for (i = 0; i < ctrl.length; i++)
                if (ctrl[i].checked) var valueTurn = ctrl[i].value;

            if (valueTurn == undefined) {
                $('#turn_error').html("Debe seleccionar al menos un turno");
                $("#turn_error").fadeIn('fast');
                return false;
            }

    });
})