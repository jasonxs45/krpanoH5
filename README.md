# krpanoH5

基于 krpano 的简单 H5

自己写的内容主要在 custom.xml 和 tour.html 的 js 里面

# html 里面，预先写好可以在 xml 中使用的 js 方法（type 为 JavaScript 的 action 标签内会用到）

利用 krpano 对象的 call 方法 调用 xml 中使用的内置语法 createSpot

```Javascript
	// 声明krpano对象
	var krpano = document.getElementById("krpanoSWFObject");
	// 遍历创建热点
	function addSpot() {
		for (var i = 0; i < detailArr.length; i++) {
			krpano.call("createSpot(hotspot" + i + "," + detailArr[i].coordinates.ath + "," + detailArr[i].coordinates.atv +
				",'images/" + detailArr[i].py + "3.png'," + i + ")");
		}
	}
```

需要自己控制全景的开始与进场方法

```Javascript
	function littleplanetintro() {
		krpano.call('startup();');
		krpano.call('mylittleplanetintro();');
		// krpano.call('skin_setup_littleplanetintro();');
		// krpano.call('switch(plugin[skin_gyro].enabled);');
	}
```

# custom.xml 中 利用其内置的标签 写好热点的三种样式以及需要用到的 action

```xml
	<style name="stars" onclick="" style="skin_hotspotstyle" scale=".6" onover="tween(scale,.65);" onloaded="floatup();"
	       onout="tween(scale,.6);" zorder='10'/>
	<style name="planet" onclick="" style="skin_hotspotstyle" scale="1" onover="tween(scale,1);"
	       onout="tween(scale,1);" zorder='1'/>
	<style name="stars-away" onclick="" style="skin_hotspotstyle" scale=".6" onover="tween(scale,.6);" onloaded="floatup();"
	       onout="tween(scale,.6);" />
  <!-- 热点的简单动效 -->
	<action name="floatup">
		tween(oy,-8,1.5,linear);
		mul(val, random, 1.5);
		add(val,1);
		delayedcall(get(val),if(loaded,floatdown();));
	</action>
	<action name="floatdown">
		tween(oy,8,1.5,linear);
		mul(val, random, 1.5);
		add(val,1);
		delayedcall(get(val),if(loaded,floatup();));
	</action>
  	<!-- 在全景里先定义添加热点的方法，在js中可调用 krpano.call('creatSpot') -->
	<action name="createSpot">
			addhotspot(%1);
			set(hotspot[%1].ath,%2);
			set(hotspot[%1].atv,%3);
			hotspot[%1].loadstyle(stars);
			set(hotspot[%1].url,%4);
			set(hotspot[%1].onclick,showDetail(%5));
		<!-- 循环 -->
		<!-- for(set(i,0), i LT detailArr.length, inc(i),
			trace('',i);
		); -->
	</action>
	<!-- 定义在scene里面添加热点action，调用在js里面的定义的addSpot方法 -->
	<action type="Javascript" name="addSpot">
		addSpot();
	</action>
```
