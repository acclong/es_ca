var tabID = 0;
var appID = "dndoilgpiheppnefpchcihehebljcihh";
var signTimeout = 300;

var waitSeconds = 300;	//Biến đếm countdown
var Run = null;			//Biến chứa hàm để lặp

chrome.tabs.onUpdated.addListener(function(tabId, changeInfo, tab){
	if (changeInfo.status == "complete")
	{
		console.log("Tab complete: ");	//xóa khi build
		//Add text nhận diện vào txtHasAddons để báo có add-ons
		chrome.tabs.executeScript(tabId, {
				code: 'var txtHasAddons = document.getElementsByClassName("essign_txtHasAddons");' +
					'if (txtHasAddons.length > 0) txtHasAddons[0].value = "Addons_SignWeb_NLDC";'
			});
		//Lắng nghe sự kiện btnReady_Click liên tục đã đc thiết lập ở manifest.json
		//chrome.tabs.executeScript(tabId, {file: "contentListenClick.js"});
	}
});

// chrome.runtime.onConnect.addListener(function(port) {
	// port.onMessage.addListener(function(args) {
		// console.log("onConnect:" + args.type);	//xóa khi build
		// if (args.type == "RunAddons")
		// {
			// console.log("RunAddons: ");	//xóa khi build
			// //Disable btn không cho user click lại
			// chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
				// var tabId = tabs[0].id;
				// chrome.tabs.executeScript(tabId,{
						// code: 'document.getElementsByClassName("essign_btnReady")[0].disabled = true;'
					// });
			// });
			// //Chạy hàm quét txtBase64
			// waitSeconds = signTimeout;
			// CheckReady();
		// }
	// });
// });

//Lắng nghe sự kiện từ content scripts
chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
	console.log("onMessage: " + request.type);	//xóa khi build
	var tabId = sender.tab.id;
	tabID = sender.tab.id;
	if (request.type == "RunAddons")
	{
		console.log("RunAddons: ");	//xóa khi build
		//Disable btn không cho user click lại
		chrome.tabs.executeScript(tabId,{
			code: 'document.getElementsByClassName("essign_btnReady")[0].disabled = true;'
		});
		//Chạy hàm quét txtBase64 (gọi "RunCheckFile" từ js)
		waitSeconds = signTimeout;
		CheckReady(tabId);
	}
	else if (request.type == "RunCheckFile")
	{
		console.log("RunCheckFile: " + sender.tab.id);	//xóa khi build
		textBase64 = request.text;	
		
		if (textBase64 != "")
		{
			if (textBase64 == "HUY")
			{
				console.log("RunCheckFile: " + tabId);	//xóa khi build
				//Server hủy lệnh do file đang không được phép ký
				//Xóa essign_txtBase64 và enabble btnReady
				chrome.tabs.executeScript(tabId,{
					code: 'document.getElementsByClassName("essign_txtBase64")[0].value = "";' + 						
						'document.getElementsByClassName("essign_btnReady")[0].disabled = false;'
				});
			}
			else
			{
				console.log("RunCheckFile: not HUY");	//xóa khi build
				//Chỉ chạy hàm check file sau khi load lại page và đã có base64
				waitSeconds = signTimeout;
				//Lấy base64 từ web và đẩy sang app để lưu file
				chrome.runtime.sendMessage(appID, { launch: true, text: textBase64 });
			}
		}
		else
		{
			//Chạy lại hàm kiểm tra text of btnReady liên tục trong 300s
			if (waitSeconds != 0)
			{
				waitSeconds -= 1;
				console.log("RunCheckFile:" + waitSeconds);	//xóa khi build
				btnReady_SetText(tabId, "Đang tải (" + waitSeconds + ")");
				Run = setTimeout(function(){CheckReady(tabId);}, 1000);
			}
			else
			{
				//Hết thời gian chờ --> enable cho phép ký lại
				chrome.tabs.executeScript(tabId, {
					code: 'document.getElementsByClassName("essign_btnReady")[0].disabled = false;'
				});
			}
		}
	}
});

//Lắng nghe sự kiện từ app
chrome.runtime.onMessageExternal.addListener(function(request, sender, sendResponse) {
	console.log("Message from " + sender.id);	//Xóa khi build
	if (sender.id == appID) {
		console.log(request.text);	//Xóa khi build
		//Xóa essign_txtBase64
		chrome.tabs.executeScript(tabID,{
				code: 'document.getElementsByClassName("essign_txtBase64")[0].value = ""'
			});		
		//Xóa fileUp cũ nếu có
		// if(fileIO.exists(fileUp))
			// fileIO.remove(fileUp);
		//Nhấn btnSign
		chrome.tabs.executeScript(tabID, {
			code: 'document.getElementsByClassName("essign_btnSign")[0].click();'
		});
		//Theo dõi thư mục để lấy file kết quả
		//CheckFile();	
	}
});
  
//Hàm kiểm tra text of txtBase64 để bắt đầu chạy hàm CheckFile
function CheckReady(tabId)
{
	chrome.tabs.executeScript(tabId, {file: "contentGetText.js"});
}

//Hàm kiểm tra nếu có file thì đẩy vào web page
function CheckFile()
{
	if (waitSeconds != 0)
	{
		if(fileIO.exists(fileUp))
		{
			var content = fileIO.read(fileUp);
			//console.log("106");	//xóa khi build
			//Đẩy base64 và nhấn nút btnUpload
			var worker = tabs.activeTab.attach({
					contentScript: ['document.getElementsByClassName("essign_txtBase64")[0].value = "' + content + '";',
								'document.getElementsByClassName("essign_btnUpload")[0].click();']
				});
			worker.destroy();
				
			fileIO.remove(fileUp);
		}
		else
		{
			//Set thời gian đếm ngược cho btnReady và chạy lại hàm CheckFile
			waitSeconds -= 1;
			//console.log("CheckFile:" + waitSeconds);	//xóa khi build
			var worker = tabs.activeTab.attach({
					contentScript: ['document.getElementsByClassName("essign_btnReady")[0].value = "' + waitSeconds + '";']
				});	
			worker.destroy();			
			Run = tmr.setTimeout(CheckFile,1000);
		}
	}
	else
	{
		//Truyền lệnh HUY và nhấn btnUpload để trả lại trạng thái nếu hết thời gian chờ
		var worker = tabs.activeTab.attach({
				contentScript: ['document.getElementsByClassName("essign_txtBase64")[0].value = "HUY";',
							'document.getElementsByClassName("essign_btnUpload")[0].click();']
			});
		worker.destroy();
	}
}

function btnReady_SetText(tabId ,text)
{
	chrome.tabs.executeScript(tabId, {
		code: 'document.getElementsByClassName("essign_btnReady")[0].value = "' + text + '";'
	});
}







