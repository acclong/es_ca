self.port.on("GetValue", GetValue);
function GetValue(){
	var txtBase64 = document.getElementsByClassName("essign_txtBase64");
	if (txtBase64.length > 0)
		self.port.emit("RunCheckFile", txtBase64[0].value)
	else
		self.port.emit("RunCheckFile", "")
}