function updateAvatarData(fileInput, imgSelector, filenameSelector) {
	if (fileInput.files && fileInput.files[0]) {
		setImgFromFile(fileInput, imgSelector,
			function() {
				$(filenameSelector).text(fileInput.files[0].name);
			},
			function(e) {
				$(imgSelector).attr('src', '/img/def-avatar.png');
				$(filenameSelector).text('Не вдалося завантажити файл');
			});
	}
}