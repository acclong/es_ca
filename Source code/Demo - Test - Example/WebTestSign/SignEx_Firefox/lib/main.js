var {Cc, Ci} = require("chrome");
var fileIO = require("sdk/io/file");
var tmr = require("sdk/timers");
var tabs = require("sdk/tabs");
var self = require("sdk/self");

var file = Cc["@mozilla.org/file/directory_service;1"].  
                     getService(Ci.nsIProperties).  
                     get("AppData", Ci.nsIFile);  
var fileUp = file.path + "\\esSignOnWeb\\data\\upload.ess";
var fileDown = file.path + "\\esSignOnWeb\\data\\download.ess";
var signTimeout = 300;
var textReadySignal = "Ký văn bản";

var waitSeconds = 300;	//Biến đếm countdown
var Run = null;			//Biến chứa hàm để lặp

tabs.on('ready',function(tab){
	//console.log("Tab ready: ");	//xóa khi build
	//Add text nhận diện vào txtHasAddons để báo có add-ons
	var worker = tabs.activeTab.attach({
			contentScript: ['var txtHasAddons = document.getElementsByClassName("essign_txtHasAddons");', 
				'if (txtHasAddons.length > 0) txtHasAddons[0].value = "Addons_SignWeb_NLDC";']
		});
	worker.destroy();
	//Lắng nghe sự kiện btnReady_Click
	var worker = tab.attach({
		contentScriptFile: self.data.url("contentListenClick.js")
	});
	worker.port.emit("ListenPage");
	worker.port.on("RunAddons", function(text_btn) {
		//console.log("RunAddons:");	//xóa khi build
		//Disable btn không cho user click lại
		var worker = tabs.activeTab.attach({
				contentScript: ['document.getElementsByClassName("essign_btnReady")[0].disabled = true;']
			});
		worker.destroy();
		//Chạy hàm quét txtBase64
		waitSeconds = signTimeout;
		CheckReady();
	});
});

//Hàm kiểm tra text of btnReady để bắt đầu chạy hàm CheckFile
function CheckReady()
{
	var worker = tabs.activeTab.attach({
			contentScriptFile: self.data.url("contentGetText.js")
		});
	worker.port.emit("GetValue");
	worker.port.on("RunCheckFile", function(textBase64) {
		//console.log("RunCheckFile: ");	//xóa khi build
		if (textBase64 != "")
		{	
			if (textBase64 == "HUY")
			{
				//Server hủy lệnh do file đang không được phép ký
				//Xóa essign_txtBase64 và enabble btnReady
				var worker = tabs.activeTab.attach({
						contentScript: ['document.getElementsByClassName("essign_txtBase64")[0].value = "";',							
								'document.getElementsByClassName("essign_btnReady")[0].disabled = false;']
					});
				worker.destroy();
			}
			else
			{	
				//console.log("68 CheckReady: ");	//xóa khi build		
				//Chỉ chạy hàm check file sau khi load lại page và đã có base64
				waitSeconds = signTimeout;
				//Lấy base64 từ web và convert to file
				var TextWriter = fileIO.open(fileDown, "w");
				if (!TextWriter.closed) 
				{
					TextWriter.write(textBase64);
					TextWriter.close();
				}
				//Xóa essign_txtBase64
				var worker = tabs.activeTab.attach({
						contentScript: ['document.getElementsByClassName("essign_txtBase64")[0].value = "";']
					});
				worker.destroy();
				//Xóa fileUp cũ nếu có
				if(fileIO.exists(fileUp))
					fileIO.remove(fileUp);
				//Nhấn btnSign
				var worker = tabs.activeTab.attach({
						contentScript: ['document.getElementsByClassName("essign_btnSign")[0].click();']
					});
				worker.destroy();
				//Theo dõi thư mục để lấy file kết quả
				CheckFile();
			}
		}
		else
		{
			//Chạy lại hàm kiểm tra text of btnReady liên tục trong 300s
			if (waitSeconds != 0)
			{
				waitSeconds -= 1;
				btnReady_SetText("Đang tải (" + waitSeconds + ")");
				Run = tmr.setTimeout(CheckReady,1000);
			}
			else
			{
				//Hết thời gian chờ --> enable cho phép ký lại
				var worker = tabs.activeTab.attach({
						contentScript: ['document.getElementsByClassName("essign_btnReady")[0].disabled = false;']
					});
				worker.destroy();
			}
		}
	});
	//worker.destroy();
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
			btnReady_SetText("Chờ ký (" + waitSeconds + ")");		
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

function btnReady_SetText(text)
{
	var worker = tabs.activeTab.attach({
			contentScript: ['document.getElementsByClassName("essign_btnReady")[0].value = "' + text + '";']
		});	
	worker.destroy();
}







