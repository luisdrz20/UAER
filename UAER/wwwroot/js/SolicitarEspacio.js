let datatable;
$(document).ready(function () {
    loadDataTable();

});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/SolicitarEspacio/ObtenerTodos"
        },
        "columns": [
            { "data": "areasS.nombre" },
            { "data": "nombreSolicitante" },
            { "data": "espacio.nombre" },
            {
                "data": "fechaSolicitud",
                "render": function (data) {
                    // Asegúrate de que el dato no sea nulo o indefinido
                    if (data) {
                        // Convertir el string de fecha a un objeto Date
                        var fecha = new Date(data);

                        // Formatear la fecha como DD-MM-YYYY
                        var dia = String(fecha.getDate()).padStart(2, '0');
                        var mes = String(fecha.getMonth() + 1).padStart(2, '0'); // Los meses van de 0 a 11
                        var anio = fecha.getFullYear();

                        // Retornar la fecha en el formato deseado
                        return dia + '-' + mes + '-' + anio;
                    }
                    return ""; // Si el dato es nulo o indefinido, retornar un string vacío
                }
            },
            { "data": "horaSolicitud" },
          
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Autorizado";
                    } else {
                        return "En revisión";
                    }
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/SolicitarEspacio/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/SolicitarEspacio/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay datos disponibles en la tabla",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas totales",
            "infoEmpty": "Mostrando de 0 a 0 en 0 entradas",
            "infoFiltered": "(Filtrado de MAX entradas totales)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron registros coincidentes",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "aria": {
                "orderable": "Ordenar por esta columna",
                "orderableReverse": "Ordena al revés por esta columna"
            }
        }

    });
}

function Delete(url) {
    swal({
        title: "Seguro que quieres eliminar la solicitud de Espacio?",
        text: "Este registro no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //actualizar la tabla
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
