$(document).ready(function () {
    getRoles()
})

function getRoles() {
    $.ajax({
        url: '/get-roles/',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td class="colspan="5">' + 'نقشی موجود نیست' + '</td>';
                object += '</tr>';
                $('#rolesList').html(object);
            } else {
                var object = '';
                $.each(response, function (index, role) {
                    object += '<tr>';
                    object += '<td>' + role.title + '</td>';
                    object += '<td>' + `<a href="/admin/roles/${role.id}/edit/" class="btn btn-primary btn-sm">
                                    ویرایش
                                    </a>
                                            <a onclick="showSwal(${role.id}, '${role.title}')" class="btn btn-danger btn-sm text-white">
                                    حذف
                                    </a>` + '</td>';
                    object += '</tr>';
                });
                $('#rolesList').html(object);
            }
        },
        error: function () {
            Swal.fire({
                title: 'مشکلی در دریافت اطلاعات به وجود آمد',
                icon: 'error'
            })
        }
    })
}

$('#btnAdd').click(function () {
    $('#roleModal').modal('show')
    $('#modalTitle').text('افزودن نقش')
})

function InsertRole() {
    if (validate()) {
        var formData = new Object();
        formData.title = $('#Title').val();

        $.ajax({
            url: '/admin/roles/create/',
            data: formData,
            type: 'post',
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    Swal.fire({
                        title: 'مشکلی در ذخیره اطلاعات به وجود آمد',
                        icon: 'error'
                    })
                } else {
                    hideModal()
                    getRoles();
                }
            },
            error: function () {
                hideModal()
                Swal.fire({
                    title: 'مشکلی در دریافت اطلاعات به وجود آمد',
                    icon: 'error'
                })
            }
        })
    }
}

function validate() {
    var isValid = true;

    if ($('#Title').val().trim() == "") {
        $("#Title").css('border-color', 'Red');
        isValid = false;
    } else {
        $("#Title").css('border-color', 'lightgrey');
    }

    return isValid;
}

$('#Title').change(function () {
    validate()
})

function hideModal() {
    ClearData()
    $('#roleModal').modal('hide')
}

function ClearData() {
    $('#Title').val('');
    $("#Title").css('border-color', 'lightgrey');
}

const showSwal = (id, title) => {

    Swal.fire({
        title: `آیا از حذف نقش <u>${title}</u> مطمئن هستید؟`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'حذف',
        cancelButtonText: 'لغو',
        //confirmButtonColor: '#dc3545',
        //cancelButtonColor: '#e0a800',
        focousConfirm: false,
         customClass: {
             confirmButton: 'btn btn-outline-danger',
             cancelButton: 'btn btn-outline-info mx-2',
         },
         buttonsStyling: false,
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'get',
                url: `/admin/roles/${id}/delete/`,
                success: function (response) {
                    Swal.fire({
                        title: 'نقش با موفقیت حذف شد',
                        icon: 'success'
                    })
                    getRoles()
                }
            })
        }
    })
}