//var port = chrome.runtime.connect();
var btnReady = document.getElementsByClassName("essign_btnReady");
if (btnReady.length > 0)
	btnReady[0].addEventListener("click", function(){
		document.getElementsByClassName("essign_txtBase64")[0].value = "";
		chrome.runtime.sendMessage({ type: "RunAddons"} );
		//port.postMessage({ type: "RunAddons"});
	}, false);
