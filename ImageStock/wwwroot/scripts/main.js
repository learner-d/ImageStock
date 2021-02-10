$(function () {
})

function showInfoMessage(message, options, usePrimaryOption = true, locked = false) {
	$("#messageText")[0].innerHTML = message;

	$("#modalOptions")[0].innerHTML = "";
	for (var i = 0; i < options.length; i++) {
		let optBtn = document.createElement("button");
		optBtn.classList.add("btn", "ml-2");
		if (i === 0 && usePrimaryOption) {
			optBtn.classList.add("btn-primary");
		}
		else {
			optBtn.classList.add("btn-dark");
		}
		optBtn.innerHTML = options[i]['name'];
		optBtn.addEventListener("click", options[i]['clickAction']);
		$("#modalOptions")[0].appendChild(optBtn);
	}

	if (locked)
		$("#infoWindow").modal({ backdrop: 'static', keyboard: false });
	else
		$("#infoWindow").modal();
}

function hideInfoMessage() {
	$("#infoWindow").modal('hide');
}