﻿window.pageUtils = function (a) { var b, c = "", d = !1; return { setAppRootPath: function (a) { if (!a || 0 === a.length) { c = "/"; return } "/" !== a.slice(-1) && (a += "/"), c = a }, resolvePath: function (a) { return a && 0 !== a.length ? ("/" === a[0] && (a = a.substr(1)), c + a) : a }, setupPopupFileManager: function () { a.addEventListener("message", function (d) { var a = d.data; if ("SpaceApp.FileManager.FileSelected" === a.type) { console.log(a); var c = $.Event("spaceAppFileManager:fileSelected"); c.url = a.content.url, b.trigger(c), b = void 0, $("#fileManagerModal").modal("hide") } }), d = !0 }, openPopupFileManager: function (a) { d && (b = $(a), $("#fileManagerModal").modal("show")) }, getSelectedFile: function () { $("#fileManagerModal iframe")[0].contentWindow.postMessage("returnFile", document.location.origin) }, getDefaultFileSelectedHandler: function () { return function (a) { var b = $(this).parents(".media"); $($('input[type="text"]', b)[0]).val(a.url), $(".media-thumbnail", b).css("background-image", "url('" + pageUtils.resolvePath(a.url) + "')") } }, bindDefaultFileManagerHandlers: function (a) { $("body").on("click", a, function () { pageUtils.openPopupFileManager(this) }), $("body").on("spaceAppFileManager:fileSelected", a, pageUtils.getDefaultFileSelectedHandler()) } } }(window)