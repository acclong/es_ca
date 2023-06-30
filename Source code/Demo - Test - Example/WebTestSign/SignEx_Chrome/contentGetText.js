//var port = chrome.runtime.connect();
var txtBase64 = document.getElementsByClassName("essign_txtBase64");
if (txtBase64.length > 0)
	chrome.runtime.sendMessage({ type: "RunCheckFile", text: txtBase64[0].value} );
	//port.postMessage({ type: "RunCheckFile", text: txtBase64[0].value});
