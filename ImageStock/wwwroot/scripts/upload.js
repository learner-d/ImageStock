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
});

function setImgFromFile(fileInput, imgSelector, onLoadAction, onErrAction) {
	if (fileInput.files && fileInput.files[0]) {
		var reader = new FileReader();

		reader.onload = function (e) {
            $(imgSelector).attr('src', e.target.result);
            if (onLoadAction)
	            onLoadAction();
		}
        reader.onerror = onErrAction;

        $(imgSelector).attr('src', '/img/loading.gif');
        reader.readAsDataURL(fileInput.files[0]);
	}
}


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