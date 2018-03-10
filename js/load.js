var Global = function(){}
Global.images = {};
Global.music = {};
Global.count = 0;
Global.progress = 0;
Global.loading = function (predata, data, fn) {
	for (var n in predata) {
		if (predata[n].type == "image") {
			Global.images[predata[n].name] = new Image();
			Global.images[predata[n].name].src = predata[n].src;
			Global.images[predata[n].name].onload = function() {
				Global.count++;
			}
		}
		if (predata[n].type == "audio") {
			Global.music[predata[n].name] = document.createElement("audio");
			Global.music[predata[n].name].src = predata[n].src;
			if (predata[n].name == 'bgm') {
				Global.music[predata[n].name].loop = 'true';
			}
			Global.count++;
		}
	}
	
	var pretimer = setInterval(function() {
		if (Global.count >= predata.length) {
			clearInterval(pretimer);
			Global.count = 0;
			$('#loading').fadeIn(200, function() {
				mainload(data,fn);
			});
		}
	}, 20);
	
	function mainload(data,fn){
		for (var n in data) {
            if (data[n].type == "image") {
                Global.images[data[n].name] = new Image();
                Global.images[data[n].name].src = data[n].src;
                Global.images[data[n].name].onload = function () {
                    Global.count++;
                }
            }
            if (data[n].type == "audio") {
                Global.music[data[n].name] = document.createElement("audio");
                Global.music[data[n].name].src = data[n].src;
                Global.count++;
            }
        }
		var maintimer = setInterval(function(){
			Global.progress = Math.ceil(Global.count / data.length * 100) + '%';
			$('.progress .inner').css('width',Global.progress);
			$('.prognum').html(Global.progress);
			if(Global.count>=data.length){
				clearInterval(maintimer);
				fn&&fn();
			}
		},20);
	}
}