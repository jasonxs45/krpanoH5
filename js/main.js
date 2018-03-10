document.write("<script src='js/layer/layer.js'></script>");

function __dealCssEvent(eventNameArr, callback) {
  var events = eventNameArr,
    i, dom = this; // jshint ignore:line
  function fireCallBack(e) {
    /*jshint validthis:true */
    if (e.target !== this) return;
    callback.call(this, e);
    for (i = 0; i < events.length; i++) {
      dom.removeEvent(events[i], fireCallBack);
    }
  }
  if (callback) {
    for (i = 0; i < events.length; i++) {
      dom.addEvent(events[i], fireCallBack);
    }
  }
}
if (window.Element && !window.Element.prototype.addEvent) {
  window.Element.prototype.addEvent = function (event, callback) {
    if (this.addEventListener) {
      this.addEventListener(event, callback, false);
    } else if (this.attachEvent) {
      this.attachEvent('on' + event, callback);
    } else {
      this['on' + event] = callback;
    }
    return this;
  }
}
if (window.Element && !window.Element.prototype.removeEvent) {
  window.Element.prototype.removeEvent = function (event, callback) {
    if (this.removeEventListener) {
      this.addEventListener(event, callback, false);
    } else if (this.detachEvent) {
      this.detachEvent('on' + event, callback);
    } else {
      this['on' + event] = null;
    }
    return this;
  }
}
if (window.Element && !window.Element.prototype.animationEnd) {
  window.Element.prototype.animationEnd = function (callback) {
    __dealCssEvent.call(this, ['webkitAnimationEnd', 'animationend'], callback);
    return this;
  };
}
if (window.Element && !window.Element.prototype.transitionEnd) {
  window.Element.prototype.transitionEnd = function (callback) {
    __dealCssEvent.call(this, ['webkitTransitionEnd', 'transitionend'], callback);
    return this;
  }
}

function toast(content) {
  layer.open({
    content: content ? content : '',
    skin: 'msg',
    time: 2 //2秒后自动关闭
  });
}

function showloading(content) {
  window.loadingIndex = layer.open({
    type: 2,
    content: content,
    shadeClose: false
  })
}

function hideloading() {
  layer.close(loadingIndex)
}

function showConfirm(opt) {
  var defaultOption = {
    title: '提示',
    content: '提醒内容',
    btn: ['确定', '取消'],
    yes: function (index) {
      layer.close(index)
    },
    no: function () {},
    end: function () {}
  }
  if (!opt) {
    layer.open(defaultOption);
  } else {
    opt = extend(defaultOption, opt);
    layer.open(opt);
  }
}
// 公用弹框 非layer插件
function showAlert(id) {
  $('#' + id + '.alertbox').fadeIn();
};

function hideAlert(id) {
  $('#' + id + '.alertbox').fadeOut();
};

function hideAlertAll(id) {
  $('.alertbox').fadeOut();
};
$('.alertbox .close').click(function () {
  $(this).parents('.alertbox').fadeOut();
});

function extend(destination, source) { // 一个静态方法表示继承, 目标对象将拥有源对象的所有属性和方法
  for (var property in source) {
    destination[property] = source[property]; // 利用动态语言的特性, 通过赋值动态添加属性与方法
  }
  return destination; // 返回扩展后的对象
}