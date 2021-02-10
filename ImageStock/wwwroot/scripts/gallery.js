$(document).ready(function () {
    $(".postPreview").click(function () {
        showImgView(this.dataset.postid);
    });
});

function showImgView(imgId) {
    let url = 'gallery/viewpost?id=' + imgId;
    $.get(url, function (data) {
        $("#imgViewModalContent").html(data);
        $('#imgViewModal').modal('show');
    })
}