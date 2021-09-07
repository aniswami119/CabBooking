var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable()
{
    dataTable= $('#tblCity').DataTable({
        "ajax": {
            "url": "/Admin/City/GetAll"
        },
        "columns": [
            {
                "data":"name","width":"60%"
            },
            {
                "data": "id",
                "render": function (data)
                {
                    return `
                     <div class="text-center">
                    <a href="/Admin/City/Upsert/${data}" class="btn btn-success"><i class="fas fa-edit"></i></a>
                      <a onclick=Delete("/Admin/City/Delete/${data}") class="btn btn-danger text-white"><i class="fas fa-trash-alt"></i></a>
                    </div>
                      `;
                },"width":"40%"
            }
        ]
    })
}

function Delete(url)
{
    swal({
        title: "Are you sure, do you want to delete this record?",
        text: "You will not be able to restore the data later once deleted",
        icon: "warning",
        buttons: true,
        dangerModel:true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data)
                {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                                       
                    else
                        toastr.error(data.message);
                }
            })
        }
        
    })
}