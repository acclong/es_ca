var {Cc, Ci} = require("chrome");
var fileIO = require("sdk/io/file");
var tmr = require("sdk/timers");
var clipboard = require("sdk/clipboard");
var tabs = require("sdk/tabs");
var self = require("sdk/self");

var file = Cc["@mozilla.org/file/directory_service;1"].  
                     getService(Ci.nsIProperties).  
                     get("AppRegD", Ci.nsIFile);  
var pathFile = file.path;
var pathFileRun = pathFile + "\\FolderFileRun\\SignExcelInWeb.exe";
var Run = null;

tabs.on('ready', function(tab) {
	var worker = tab.attach({
		contentScriptFile: self.data.url("content-script.js")
	});
	worker.port.emit("ListenPage");

	worker.port.on("dataClipbroad", function(dtClipBroad) {
		if(dtClipBroad != "")
			if(dtClipBroad.split(" ")[0] != "ESVCGM")
			{
				clipboard.set(dtClipBroad);
				var fileRun = Cc["@mozilla.org/file/local;1"]
								.createInstance(Ci.nsILocalFile);
				fileRun.initWithPath(pathFileRun);					
				var process = Cc["@mozilla.org/process/util;1"]
								.createInstance(Ci.nsIProcess);							
				process.init(fileRun);
				var arguments= [] ;
				process.run(false, arguments, arguments.length);
				Run = tmr.setTimeout(ToServer,3000);
			}
	});
});

function ToServer()
{
	var dataClipBoard = clipboard.get();
	if(dataClipBoard != null)
	{
		var dataget = dataClipBoard.split(" ");
		if(dataget[0] == "ESVCGM")
		{		
			tabs.activeTab.attach({
				contentScript: ['document.getElementById("MainContent_txtDataSigned").value = "' + dataClipBoard + '";',
								'document.getElementById("MainContent_btnSign").click();']
			});		
			clipboard.set("");
		}
		else
		{
			Run = tmr.setTimeout(ToServer,3000);
		}
	}
}