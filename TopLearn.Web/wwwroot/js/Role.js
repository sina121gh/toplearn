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
                object += '<td class="colspan="5">' + 'Roles not available' + '</td>';
                object += '</tr>';
                $('#rolesList').html(object);
            } else {
                var object = '';
                $.each(response, function (index, role) {
                    object += '<tr>';
                    object += '<td>' + role.title + '</td>';
                    object += '<td>' + `<a href="/admin/edit-role/${role.id}/" class="btn btn-primary btn-sm">
                                    ویرایش
                                    </a>
                                            <a onclick="showSwal(${role.id})" class="btn btn-danger btn-sm text-white">
                                    حذف
                                    </a>` + '</td>';
                    object += '</tr>';
                });
                $('#rolesList').html(object);
            }
        },
        error: function () {
            alert('Unable to read the data');
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
            url: '/create-role/',
            data: formData,
            type: 'post',
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    alert('Unable to save the data');
                } else {
                    hideModal()
                    getRoles();
                }
            },
            error: function () {
                hideModal()
                alert('Unable to save the data');
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

const showSwal = (id) => {

    Swal.fire({
        title: 'آیا از حذف مطمئن هستید؟',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'حذف',
        cancelButtonText: 'لغو',
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#e0a800',
        focusConfirm: false,
        // customClass: {
        //     confirmButton: 'btn btn-danger',
        //     cancelButton: 'btn btn-warning mx-2',
        // },
        // buttonsStyling: false,
    }).then(result => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'get',
                url: `/admin/delete-role/${id}/`,
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