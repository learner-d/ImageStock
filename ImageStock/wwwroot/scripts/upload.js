$(document).ready(function () {
    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [label]);
    });

    $('.btn-file :file').on('fileselect', function (event, label) {

        var input = $(this).parents('.input-group').find(':text'),
            log = label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }

    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#uploadImg').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#imgInp").change(function () {
        readURL(this);
    });

    $('form').submit(doUpload);
});

function doUpload(submitEventData) {
    submitEventData.preventDefault();
    var formData = new FormData(this);

    $.ajax({
        type: 'POST',
        url: this.action,
        data: formData,
        processData: false,
        contentType: false,
    })
    .done(function (data) {
        showInfoMessage("Допис успішно створено.",
            options =
            [
                {
                    'name': "Завантажити ще",
                    'clickAction': function () {
                        window.location.href = "/upload"
                    }
                },
                {
                    'name': "Перейти до галереї",
                    'clickAction': function () {
                        hideInfoMessage();
                        window.location.href = "/gallery";
                    }
                }
            ],
            true, true);
        });
}