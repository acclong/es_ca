self.port.on("ListenPage", ListenPage);
function ListenPage(){
	var btnReady = document.getElementsByClassName("essign_btnReady");
	if (btnReady.length > 0)
		btnReady[0].addEventListener("click", function(){
			document.getElementsByClassName("essign_txtBase64")[0].value = "";
			self.port.emit("RunAddons", btnReady[0].value)
		});
}