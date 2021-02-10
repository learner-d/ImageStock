$(document).ready(function () {
	$('#deleteBtn').click(deleteBtn_click);
	$('#commentForm form').submit(postComment_Click);
	$('.commentInfo .deleteCmt').click(deleteComment_Click);
});

function deleteComment_Click(eventData) {
	eventData.preventDefault();
	let cmtInfo = $(this).parents(".commentInfo");
	let cmtId = cmtInfo.data("comment_id");

	$.post(`/gallery/deletecomment?id=${cmtId}`)
		.done(function (data)
		{
			cmtInfo.html('<span class="cmtMessage">Коментар видалено.</span>');
			setTimeout(function () {
				cmtInfo.remove();
			}, 2000);
		})
		.fail(function (data)
		{
			alert(`Не вдалося видалити.\n${data.status}: ${data.statusText}`);
		});
}

function postComment_Click(submitEventData) {
	submitEventData.preventDefault();
	let cmtFormData = new FormData(this);
	let authorId = cmtFormData.get('author_id');
	let authorName = cmtFormData.get('author');
	let authorAvatarUrl = cmtFormData.get('author_avatar_url');
	let cmtText = cmtFormData.get('comment_text');
	let newcmt = 
	$(
		`<div class="commentInfo">
            <div class="userInfo d-inline-block">
                <div class="userAvatar d-inline-block">
                    <img src="${authorAvatarUrl}" />
                </div>
                <div class="userName d-inline-block">${authorName}</div>
            </div>
            <span>: </span>
            <div class="commentText d-inline-block">
                <p>${cmtText}</p>
            </div>
            <div class="cmtControlPanel">
            </div>
        </div>`
		);
	let ctrlPanel = newcmt.children('.cmtControlPanel');
	ctrlPanel.html('<span class="text-info">Надсилання...</span><br />');

	newcmt.data("author_id", authorId);

	newcmt.appendTo('#commentsSection');

	doRequest();
	function doRequest() {
		$.ajax({
			type: 'POST',
			url: '/gallery/addcomment',
			data: cmtFormData,
			processData: false,
			contentType: false
		})
			.done(function (data) {
				newcmt.data("comment_id", data);
				ctrlPanel.html(
					`<span class="text-success">
						✔Надіслано
					</span><br />`
				);
				setTimeout(function () {
					if (authorId && authorId != -1) {
						let dltLink = ctrlPanel.html(
							`<a href="#" class="deleteCmt">Видалити</a>`
						);
						dltLink.click(deleteComment_Click);
					}
					else
						ctrlPanel.html('');
				}, 2000);
			})
			.fail(function () {
				let msg = ctrlPanel.html(
					`<span class="text-danger">
						❌Не вдалося надіслати
					</span>
					<a href="#">Надіслати знову</a>
					<br />`
				);
				let retryLink = msg.children('a');
				retryLink.click(function (e) {
					e.preventDefault();
					doRequest();
				})
			});
	}
}

function deleteBtn_click(e) {
	showInfoMessage("Видалити допис?",
		[
			{
				"name": "Видалити",
				"clickAction": function ()
				{
					let url = `/gallery/deletepost?id=${$("#mainContent").data("postid")}`;
					$.get(url).done(deletePostSuccess).fail(deletePostFail);
				}
			},
			{
				"name": "Скасувати",
				"clickAction": hideInfoMessage
			}
		], true, true
	);
}

function deletePostSuccess(data) {
	showInfoMessage("Допис видалено.",
		[
			{
				"name": "Повернутися до галереї",
				"clickAction": function () {
					window.location = "/gallery"
				}
			}
		],
		true, true
	);
	console.log(data);
}

function deletePostFail(data) {
	showInfoMessage("Не вдалося видалити. Повторіть спробу",
		[
			{
				"name": "Закрити",
				"clickAction": hideInfoMessage
			}
		]
	);
	console.log(data);
}