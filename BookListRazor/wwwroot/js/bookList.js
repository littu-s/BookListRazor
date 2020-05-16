var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#Booktbl').DataTable({
        // ajax 4 API call
        "ajax": {
            "url": "/api/Book",
            "type": "GET",
            "datatype": "json"
        },
        // define the caol.s of tbl
        //col 4 edit & delete btn
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },            
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center" >
                                <a href="/BookList/Upsert?id=${data}" class='btn btn-xs btn-success text-white' style='cursor:pointer; width:70px;' >
                                    Edit
                                </a>
                                &nbsp;
                                <a class='btn btn-xs btn-danger text-white' style='cursor:pointer; width:70px;' onclick=Delete('/api/Book?id='+${data}) >
                                    Delete
                                </a>
                            </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    }); 
}

// call sweet alert swal()
// use ajax using then()
function Delete(url) {
    swal({
        title: "Are you Sure?",
        text: "Once deleted, you won't be able to recover data",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}