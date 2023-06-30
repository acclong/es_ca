self.port.on("ListenPage", ListenPage);
function ListenPage(){
	var elementId = document.getElementById("MainContent_btnSign");
	var myMessagePayload = document.getElementById("MainContent_txtDataSigned");
	elementId.addEventListener("click", function(){
		var DataClipBroad = myMessagePayload.value;
		self.port.emit("dataClipbroad", DataClipBroad)
	});
}