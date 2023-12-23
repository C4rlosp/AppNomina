// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// wwwroot/js/Empleados.js
$(document).ready(function () {
    $('#btnCalculate').click(function () {
        // Obtener los valores de HNormales y HExtras
        var hNormales = parseFloat($('#HNormales').val()) || 0;
        var hExtras = parseFloat($('#HExtras').val()) || 0;

        // Realizar el cálculo
        var salarioBruto = (hNormales * 1800) + (hExtras * 2700);
        var deducciones = calcularDeducciones(salarioBruto);
        var salarioNeto = salarioBruto - deducciones;

        // Mostrar los resultados en los campos correspondientes
        $('#SalarioBruto').val(salarioBruto.toFixed(2));
        $('#Deducciones').val(deducciones.toFixed(2));
        $('#SalarioNeto').val(salarioNeto.toFixed(2));

        // Mostrar el mensaje de confirmación personalizado
        if (confirm('¿Desea guardar este empleado con los cálculos realizados?')) {
            // Continuar con el envío del formulario
            $('form').submit();
        }
    });

    function calcularDeducciones(salarioBruto) {
        // Tu lógica para calcular deducciones basadas en las condiciones proporcionadas
        if (salarioBruto <= 250000) {
            return salarioBruto * 0.09;
        } else if (salarioBruto <= 380000) {
            return salarioBruto * 0.12;
        } else {
            return salarioBruto * 0.15;
        }
    }
});

