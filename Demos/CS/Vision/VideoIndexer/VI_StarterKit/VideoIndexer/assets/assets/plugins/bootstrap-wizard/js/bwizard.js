!function(a, b, c) {
    var d = /\+/g;
    function e(a) {
        return a;
    }
    function f(a) {
        return decodeURIComponent(a.replace(d, " "));
    }
    var g = a.cookie = function(d, h, i) {
        if (h !== c) {
            i = a.extend({}, g.defaults, i);
            if (null === h) i.expires = -1;
            if ("number" === typeof i.expires) {
                var j = i.expires, k = i.expires = new Date();
                k.setDate(k.getDate() + j);
            }
            h = g.json ? JSON.stringify(h) : String(h);
            return b.cookie = [ encodeURIComponent(d), "=", g.raw ? h : encodeURIComponent(h), i.expires ? "; expires=" + i.expires.toUTCString() : "", i.path ? "; path=" + i.path : "", i.domain ? "; domain=" + i.domain : "", i.secure ? "; secure" : "" ].join("");
        }
        var l = g.raw ? e : f;
        var m = b.cookie.split("; ");
        for (var n = 0, o = m.length; n < o; n++) {
            var p = m[n].split("=");
            if (l(p.shift()) === d) {
                var q = l(p.join("="));
                return g.json ? JSON.parse(q) : q;
            }
        }
        return null;
    };
    g.defaults = {};
    a.removeCookie = function(b, c) {
        if (null !== a.cookie(b)) {
            a.cookie(b, null, c);
            return true;
        }
        return false;
    };
}(jQuery, document);

!function(a, b) {
    var c = 0, d = Array.prototype.slice, e = a.cleanData;
    a.cleanData = function(b) {
        for (var c = 0, d; null != (d = b[c]); c++) try {
            a(d).triggerHandler("remove");
        } catch (f) {}
        e(b);
    };
    a.widget = function(b, c, d) {
        var e, f, g, h, i = b.split(".")[0];
        b = b.split(".")[1];
        e = i + "-" + b;
        if (!d) {
            d = c;
            c = a.Widget;
        }
        a.expr[":"][e.toLowerCase()] = function(b) {
            return !!a.data(b, e);
        };
        a[i] = a[i] || {};
        f = a[i][b];
        g = a[i][b] = function(a, b) {
            if (!this._createWidget) return new g(a, b);
            if (arguments.length) this._createWidget(a, b);
        };
        a.extend(g, f, {
            version: d.version,
            _proto: a.extend({}, d),
            _childConstructors: []
        });
        h = new c();
        h.options = a.widget.extend({}, h.options);
        a.each(d, function(b, e) {
            if (a.isFunction(e)) d[b] = function() {
                var a = function() {
                    return c.prototype[b].apply(this, arguments);
                }, d = function(a) {
                    return c.prototype[b].apply(this, a);
                };
                return function() {
                    var b = this._super, c = this._superApply, f;
                    this._super = a;
                    this._superApply = d;
                    f = e.apply(this, arguments);
                    this._super = b;
                    this._superApply = c;
                    return f;
                };
            }();
        });
        g.prototype = a.widget.extend(h, {
            widgetEventPrefix: b
        }, d, {
            constructor: g,
            namespace: i,
            widgetName: b,
            widgetBaseClass: e,
            widgetFullName: e
        });
        if (f) {
            a.each(f._childConstructors, function(b, c) {
                var d = c.prototype;
                a.widget(d.namespace + "." + d.widgetName, g, c._proto);
            });
            delete f._childConstructors;
        } else c._childConstructors.push(g);
        a.widget.bridge(b, g);
    };
    a.widget.extend = function(c) {
        var e = d.call(arguments, 1), f = 0, g = e.length, h, i;
        for (;f < g; f++) for (h in e[f]) {
            i = e[f][h];
            if (e[f].hasOwnProperty(h) && i !== b) if (a.isPlainObject(i)) c[h] = a.isPlainObject(c[h]) ? a.widget.extend({}, c[h], i) : a.widget.extend({}, i); else c[h] = i;
        }
        return c;
    };
    a.widget.bridge = function(c, e) {
        var f = e.prototype.widgetFullName;
        a.fn[c] = function(g) {
            var h = "string" === typeof g, i = d.call(arguments, 1), j = this;
            g = !h && i.length ? a.widget.extend.apply(null, [ g ].concat(i)) : g;
            if (h) this.each(function() {
                var d, e = a.data(this, f);
                if (!e) return a.error("cannot call methods on " + c + " prior to initialization; attempted to call method '" + g + "'");
                if (!a.isFunction(e[g]) || "_" === g.charAt(0)) return a.error("no such method '" + g + "' for " + c + " widget instance");
                d = e[g].apply(e, i);
                if (d !== e && d !== b) {
                    j = d && d.jquery ? j.pushStack(d.get()) : d;
                    return false;
                }
            }); else this.each(function() {
                var b = a.data(this, f);
                if (b) b.option(g || {})._init(); else new e(g, this);
            });
            return j;
        };
    };
    a.Widget = function() {};
    a.Widget._childConstructors = [];
    a.Widget.prototype = {
        widgetName: "widget",
        widgetEventPrefix: "",
        defaultElement: "<div>",
        options: {
            disabled: false,
            create: null
        },
        _createWidget: function(b, d) {
            d = a(d || this.defaultElement || this)[0];
            this.element = a(d);
            this.uuid = c++;
            this.eventNamespace = "." + this.widgetName + this.uuid;
            this.options = a.widget.extend({}, this.options, this._getCreateOptions(), b);
            this.bindings = a();
            this.hoverable = a();
            this.focusable = a();
            if (d !== this) {
                a.data(d, this.widgetName, this);
                a.data(d, this.widgetFullName, this);
                this._on({
                    remove: function(a) {
                        if (a.target === d) this.destroy();
                    }
                });
                this.document = a(d.style ? d.ownerDocument : d.document || d);
                this.window = a(this.document[0].defaultView || this.document[0].parentWindow);
            }
            this._create();
            this._trigger("create", null, this._getCreateEventData());
            this._init();
        },
        _getCreateOptions: a.noop,
        _getCreateEventData: a.noop,
        _create: a.noop,
        _init: a.noop,
        destroy: function() {
            this._destroy();
            this.element.unbind(this.eventNamespace).removeData(this.widgetName).removeData(this.widgetFullName).removeData(a.camelCase(this.widgetFullName));
            this.widget().unbind(this.eventNamespace).removeAttr("aria-disabled").removeClass(this.widgetFullName + "-disabled ui-state-disabled");
            this.bindings.unbind(this.eventNamespace);
            this.hoverable.removeClass("ui-state-hover");
            this.focusable.removeClass("ui-state-focus");
        },
        _destroy: a.noop,
        widget: function() {
            return this.element;
        },
        option: function(c, d) {
            var e = c, f, g, h;
            if (0 === arguments.length) return a.widget.extend({}, this.options);
            if ("string" === typeof c) {
                e = {};
                f = c.split(".");
                c = f.shift();
                if (f.length) {
                    g = e[c] = a.widget.extend({}, this.options[c]);
                    for (h = 0; h < f.length - 1; h++) {
                        g[f[h]] = g[f[h]] || {};
                        g = g[f[h]];
                    }
                    c = f.pop();
                    if (d === b) return g[c] === b ? null : g[c];
                    g[c] = d;
                } else {
                    if (d === b) return this.options[c] === b ? null : this.options[c];
                    e[c] = d;
                }
            }
            this._setOptions(e);
            return this;
        },
        _setOptions: function(a) {
            var b;
            for (b in a) this._setOption(b, a[b]);
            return this;
        },
        _setOption: function(a, b) {
            this.options[a] = b;
            if ("disabled" === a) {
                this.widget().toggleClass(this.widgetFullName + "-disabled ui-state-disabled", !!b).attr("aria-disabled", b);
                this.hoverable.removeClass("ui-state-hover");
                this.focusable.removeClass("ui-state-focus");
            }
            return this;
        },
        enable: function() {
            return this._setOption("disabled", false);
        },
        disable: function() {
            return this._setOption("disabled", true);
        },
        _on: function(b, c) {
            if (!c) {
                c = b;
                b = this.element;
            } else {
                b = a(b);
                this.bindings = this.bindings.add(b);
            }
            var d = this;
            a.each(c, function(c, e) {
                function f() {
                    if (true === d.options.disabled || a(this).hasClass("ui-state-disabled")) return;
                    return ("string" === typeof e ? d[e] : e).apply(d, arguments);
                }
                if ("string" !== typeof e) f.guid = e.guid = e.guid || f.guid || a.guid++;
                var g = c.match(/^(\w+)\s*(.*)$/), h = g[1] + d.eventNamespace, i = g[2];
                if (i) d.widget().delegate(i, h, f); else b.bind(h, f);
            });
        },
        _off: function(a, b) {
            b = (b || "").split(" ").join(this.eventNamespace + " ") + this.eventNamespace;
            a.unbind(b).undelegate(b);
        },
        _delay: function(a, b) {
            function c() {
                return ("string" === typeof a ? d[a] : a).apply(d, arguments);
            }
            var d = this;
            return setTimeout(c, b || 0);
        },
        _hoverable: function(b) {
            this.hoverable = this.hoverable.add(b);
            this._on(b, {
                mouseenter: function(b) {
                    a(b.currentTarget).addClass("ui-state-hover");
                },
                mouseleave: function(b) {
                    a(b.currentTarget).removeClass("ui-state-hover");
                }
            });
        },
        _focusable: function(b) {
            this.focusable = this.focusable.add(b);
            this._on(b, {
                focusin: function(b) {
                    a(b.currentTarget).addClass("ui-state-focus");
                },
                focusout: function(b) {
                    a(b.currentTarget).removeClass("ui-state-focus");
                }
            });
        },
        _trigger: function(b, c, d) {
            var e, f, g = this.options[b];
            d = d || {};
            c = a.Event(c);
            c.type = (b === this.widgetEventPrefix ? b : this.widgetEventPrefix + b).toLowerCase();
            c.target = this.element[0];
            f = c.originalEvent;
            if (f) for (e in f) if (!(e in c)) c[e] = f[e];
            this.element.trigger(c, d);
            return !(a.isFunction(g) && false === g.apply(this.element[0], [ c ].concat(d)) || c.isDefaultPrevented());
        }
    };
    a.each({
        show: "fadeIn",
        hide: "fadeOut"
    }, function(b, c) {
        a.Widget.prototype["_" + b] = function(d, e, f) {
            if ("string" === typeof e) e = {
                effect: e
            };
            var g, h = !e ? b : true === e || "number" === typeof e ? c : e.effect || c;
            e = e || {};
            if ("number" === typeof e) e = {
                duration: e
            };
            g = !a.isEmptyObject(e);
            e.complete = f;
            if (e.delay) d.delay(e.delay);
            if (g && a.effects && (a.effects.effect[h] || false !== a.uiBackCompat && a.effects[h])) d[b](e); else if (h !== b && d[h]) d[h](e.duration, e.easing, f); else d.queue(function(c) {
                a(this)[b]();
                if (f) f.call(d[0]);
                c();
            });
        };
    });
    if (false !== a.uiBackCompat) a.Widget.prototype._getCreateOptions = function() {
        return a.metadata && a.metadata.get(this.element[0])[this.widgetName];
    };
}(jQuery);

!function(a, b) {
    a.widget("bootstrap.bwizard", {
        options: {
            clickableSteps: true,
            autoPlay: false,
            delay: 3e3,
            loop: false,
            hideOption: {
                fade: true
            },
            showOption: {
                fade: true,
                duration: 400
            },
            ajaxOptions: null,
            cache: false,
            cookie: null,
            stepHeaderTemplate: "",
            panelTemplate: "",
            spinner: "",
            backBtnText: "&larr; Previous",
            nextBtnText: "Next &rarr;",
            labelFinish: "Finish",
            add: null,
            remove: null,
            activeIndexChanged: null,
            show: null,
            load: null,
            validating: null
        },
        _defaults: {
            stepHeaderTemplate: "<li>#{title}</li>",
            panelTemplate: "<div></div>",
            spinner: "<em>Loading&#8230;</em>"
        },
        _create: function() {
            var a = this;
            a._pageLize(true);
        },
        _init: function() {
            var a = this.options, b = a.disabled;
            if (a.disabledState) {
                this.disable();
                a.disabled = b;
            } else if (a.autoPlay) this.play();
        },
        _setOption: function(b, c) {
            a.Widget.prototype._setOption.apply(this, arguments);
            switch (b) {
              case "activeIndex":
                this.show(c);
                break;

              case "navButtons":
                this._createButtons();
                break;

              default:
                this._pageLize();
            }
        },
        play: function() {
            var a = this.options, b = this, c;
            if (!this.element.data("intId.bwizard")) {
                c = window.setInterval(function() {
                    var c = a.activeIndex + 1;
                    if (c >= b.panels.length) if (a.loop) c = 0; else {
                        b.stop();
                        return;
                    }
                    b.show(c);
                }, a.delay);
                this.element.data("intId.bwizard", c);
            }
        },
        stop: function() {
            var a = this.element.data("intId.bwizard");
            if (a) {
                window.clearInterval(a);
                this.element.removeData("intId.bwizard");
            }
        },
        _normalizeBlindOption: function(a) {
            if (a.blind === b) a.blind = false;
            if (a.fade === b) a.fade = false;
            if (a.duration === b) a.duration = 200;
            if ("string" === typeof a.duration) try {
                a.duration = parseInt(a.duration, 10);
            } catch (c) {
                a.duration = 200;
            }
        },
        _createButtons: function() {
            var b = this, c = this.options, d, e = c.backBtnText, f = c.nextBtnText;
            this._removeButtons();
            if ("none" === c.navButtons) return;
            if (!this.buttons) {
                d = c.navButtons;
                var g = false;
                this.buttons = a('<ul class="pager"/>');
                this.buttons.addClass("bwizard-buttons");
                if ("" != e) {
                    this.backBtn = a("<li class='previous'><a href='#'>" + e + "</a></li>").appendTo(this.buttons).bind({
                        click: function() {
                            b.back();
                            return false;
                        }
                    }).attr("role", "button");
                    var g = true;
                }
                if ("" != f) {
                    this.nextBtn = a("<li class='next'><a href='#'>" + f + "</a>").appendTo(this.buttons).bind({
                        click: function() {
                            b.next();
                            return false;
                        }
                    }).attr("role", "button");
                    var g = true;
                }
                if (g) this.buttons.appendTo(this.element); else this.buttons = null;
            }
        },
        _removeButtons: function() {
            if (this.buttons) {
                this.buttons.remove();
                this.buttons = b;
            }
        },
        _pageLize: function(c) {
            var d = this, e = this.options, f = /^#.+/;
            var g = false;
            this.list = this.element.children("ol,ul").eq(0);
            var h = this.list.length;
            if (this.list && 0 === h) this.list = null;
            if (this.list) {
                if ("ol" === this.list.get(0).tagName.toLowerCase()) g = true;
                this.lis = a("li", this.list);
                this.lis.each(function(b) {
                    if (e.clickableSteps) {
                        a(this).click(function(a) {
                            a.preventDefault();
                            d.show(b);
                        });
                        a(this).contents().wrap('<a href="#step' + (b + 1) + '" class="hidden-phone"/>');
                    } else a(this).contents().wrap('<span class="hidden-phone"/>');
                    a(this).attr("role", "tab");
                    a(this).css("z-index", d.lis.length - b);
                    a(this).prepend('<span class="label">' + (b + 1) + "</span>");
                    if (!g) a(this).find(".label").addClass("visible-phone");
                });
            }
            if (c) {
                this.panels = a("> div", this.element);
                this.panels.each(function(b, c) {
                    a(this).attr("id", "step" + (b + 1));
                    var d = a(c).attr("src");
                    if (d && !f.test(d)) a.data(c, "load.bwizard", d.replace(/#.*$/, ""));
                });
                this.element.addClass("bwizard clearfix");
                if (this.list) {
                    this.list.addClass("bwizard-steps clearfix").attr("role", "tablist");
                    if (e.clickableSteps) this.list.addClass("clickable");
                }
                this.container = a("<div/>");
                this.container.addClass("well");
                this.container.append(this.panels);
                this.container.appendTo(this.element);
                this.panels.attr("role", "tabpanel");
                if (e.activeIndex === b) {
                    if ("number" !== typeof e.activeIndex && e.cookie) e.activeIndex = parseInt(d._cookie(), 10);
                    if ("number" !== typeof e.activeIndex && this.panels.filter(".bwizard-activated").length) e.activeIndex = this.panels.index(this.panels.filter(".bwizard-activated"));
                    e.activeIndex = e.activeIndex || (this.panels.length ? 0 : -1);
                } else if (null === e.activeIndex) e.activeIndex = -1;
                e.activeIndex = e.activeIndex >= 0 && this.panels[e.activeIndex] || e.activeIndex < 0 ? e.activeIndex : 0;
                this.panels.addClass("hide").attr("aria-hidden", true);
                if (e.activeIndex >= 0 && this.panels.length) {
                    this.panels.eq(e.activeIndex).removeClass("hide").addClass("bwizard-activated").attr("aria-hidden", false);
                    this.load(e.activeIndex);
                }
                this._createButtons();
            } else {
                this.panels = a("> div", this.container);
                e.activeIndex = this.panels.index(this.panels.filter(".bwizard-activated"));
            }
            this._refreshStep();
            if (e.cookie) this._cookie(e.activeIndex, e.cookie);
            if (false === e.cache) this.panels.removeData("cache.bwizard");
            if (e.showOption === b || null === e.showOption) e.showOption = {};
            this._normalizeBlindOption(e.showOption);
            if (e.hideOption === b || null === e.hideOption) e.hideOption = {};
            this._normalizeBlindOption(e.hideOption);
            this.panels.unbind(".bwizard");
        },
        _refreshStep: function() {
            var a = this.options;
            if (this.lis) {
                this.lis.removeClass("active").attr("aria-selected", false).find(".label").removeClass("badge-inverse");
                if (a.activeIndex >= 0 && a.activeIndex <= this.lis.length - 1) if (this.lis) this.lis.eq(a.activeIndex).addClass("active").attr("aria-selected", true).find(".label").addClass("badge-inverse");
            }
            if (this.buttons && !a.loop) {
                this.backBtn[a.activeIndex <= 0 ? "addClass" : "removeClass"]("disabled").attr("aria-disabled", 0 === a.activeIndex);
                this.nextBtn[a.activeIndex >= this.panels.length - 1 ? "addClass" : "removeClass"]("disabled").attr("aria-disabled", a.activeIndex >= this.panels.length - 1);
            }
        },
        _sanitizeSelector: function(a) {
            return a.replace(/:/g, "\\:");
        },
        _cookie: function() {
            var b = this.cookie || (this.cookie = this.options.cookie.name);
            return a.cookie.apply(null, [ b ].concat(a.makeArray(arguments)));
        },
        _ui: function(a) {
            return {
                panel: a,
                index: this.panels.index(a)
            };
        },
        _removeSpinner: function() {
            var a = this.element.data("spinner.bwizard");
            if (a) {
                this.element.removeData("spinner.bwizard");
                a.remove();
            }
        },
        _resetStyle: function(b) {
            b.css({
                display: ""
            });
            if (!a.support.opacity) b[0].style.removeAttribute("filter");
        },
        destroy: function() {
            var b = this.options;
            this.abort();
            this.stop();
            this._removeButtons();
            this.element.unbind(".bwizard").removeClass([ "bwizard", "clearfix" ].join(" ")).removeData("bwizard");
            if (this.list) this.list.removeClass("bwizard-steps clearfix").removeAttr("role");
            if (this.lis) {
                this.lis.removeClass("active").removeAttr("role");
                this.lis.each(function() {
                    if (a.data(this, "destroy.bwizard")) a(this).remove(); else a(this).removeAttr("aria-selected");
                });
            }
            this.panels.each(function() {
                var b = a(this).unbind(".bwizard");
                a.each([ "load", "cache" ], function(a, c) {
                    b.removeData(c + ".bwizard");
                });
                if (a.data(this, "destroy.bwizard")) a(this).remove(); else a(this).removeClass([ "bwizard-activated", "hide" ].join(" ")).css({
                    position: "",
                    left: "",
                    top: ""
                }).removeAttr("aria-hidden");
            });
            this.container.replaceWith(this.container.contents());
            if (b.cookie) this._cookie(null, b.cookie);
            return this;
        },
        add: function(c, d) {
            if (c === b) c = this.panels.length;
            if (d === b) d = "Step " + c;
            var e = this, f = this.options, g = a(f.panelTemplate || e._defaults.panelTemplate).data("destroy.bwizard", true), h;
            g.addClass("hide").attr("aria-hidden", true);
            if (c >= this.panels.length) if (this.panels.length > 0) g.insertAfter(this.panels[this.panels.length - 1]); else g.appendTo(this.container); else g.insertBefore(this.panels[c]);
            if (this.list && this.lis) {
                h = a((f.stepHeaderTemplate || e._defaults.stepHeaderTemplate).replace(/#\{title\}/g, d));
                h.data("destroy.bwizard", true);
                if (c >= this.lis.length) h.appendTo(this.list); else h.insertBefore(this.lis[c]);
            }
            this._pageLize();
            if (1 === this.panels.length) {
                f.activeIndex = 0;
                h.addClass("ui-priority-primary");
                g.removeClass("hide").addClass("bwizard-activated").attr("aria-hidden", false);
                this.element.queue("bwizard", function() {
                    e._trigger("show", null, e._ui(e.panels[0]));
                });
                this._refreshStep();
                this.load(0);
            }
            this._trigger("add", null, this._ui(this.panels[c]));
            return this;
        },
        remove: function(a) {
            var b = this.options, c = this.panels.eq(a).remove();
            this.lis.eq(a).remove();
            if (a < b.activeIndex) b.activeIndex--;
            this._pageLize();
            if (c.hasClass("bwizard-activated") && this.panels.length >= 1) this.show(a + (a < this.panels.length ? 0 : -1));
            this._trigger("remove", null, this._ui(c[0]));
            return this;
        },
        _showPanel: function(b) {
            var c = this, d = this.options, e = a(b), f;
            e.addClass("bwizard-activated");
            if ((d.showOption.blind || d.showOption.fade) && d.showOption.duration > 0) {
                f = {
                    duration: d.showOption.duration
                };
                if (d.showOption.blind) f.height = "toggle";
                if (d.showOption.fade) f.opacity = "toggle";
                e.hide().removeClass("hide").animate(f, d.showOption.duration || "normal", function() {
                    c._resetStyle(e);
                    c._trigger("show", null, c._ui(e[0]));
                    c._removeSpinner();
                    e.attr("aria-hidden", false);
                    c._trigger("activeIndexChanged", null, c._ui(e[0]));
                });
            } else {
                e.removeClass("hide").attr("aria-hidden", false);
                c._trigger("show", null, c._ui(e[0]));
                c._removeSpinner();
                c._trigger("activeIndexChanged", null, c._ui(e[0]));
            }
        },
        _hidePanel: function(b) {
            var c = this, d = this.options, e = a(b), f;
            e.removeClass("bwizard-activated");
            if ((d.hideOption.blind || d.hideOption.fade) && d.hideOption.duration > 0) {
                f = {
                    duration: d.hideOption.duration
                };
                if (d.hideOption.blind) f.height = "toggle";
                if (d.hideOption.fade) f.opacity = "toggle";
                e.animate(f, d.hideOption.duration || "normal", function() {
                    e.addClass("hide").attr("aria-hidden", true);
                    c._resetStyle(e);
                    c.element.dequeue("bwizard");
                });
            } else {
                e.addClass("hide").attr("aria-hidden", true);
                this.element.dequeue("bwizard");
            }
        },
        show: function(b) {
            if (b < 0 || b >= this.panels.length) return this;
            if (this.element.queue("bwizard").length > 0) return this;
            var c = this, d = this.options, e = a.extend({}, this._ui(this.panels[d.activeIndex])), f, g;
            e.nextIndex = b;
            e.nextPanel = this.panels[b];
            if (false === this._trigger("validating", null, e)) return this;
            f = this.panels.filter(":not(.hide)");
            g = this.panels.eq(b);
            d.activeIndex = b;
            this.abort();
            if (d.cookie) this._cookie(d.activeIndex, d.cookie);
            this._refreshStep();
            if (g.length) {
                if (f.length) this.element.queue("bwizard", function() {
                    c._hidePanel(f);
                });
                this.element.queue("bwizard", function() {
                    c._showPanel(g);
                });
                this.load(b);
            } else throw "Bootstrap Wizard: Mismatching fragment identifier.";
            return this;
        },
        next: function() {
            var a = this.options, b = a.activeIndex + 1;
            if (a.disabled) return false;
            if (a.loop) b %= this.panels.length;
            if (b < this.panels.length) {
                this.show(b);
                return true;
            }
            return false;
        },
        back: function() {
            var a = this.options, b = a.activeIndex - 1;
            if (a.disabled) return false;
            if (a.loop) b = b < 0 ? this.panels.length - 1 : b;
            if (b >= 0) {
                this.show(b);
                return true;
            }
            return false;
        },
        load: function(b) {
            var c = this, d = this.options, e = this.panels.eq(b)[0], f = a.data(e, "load.bwizard"), g;
            this.abort();
            if (!f || 0 !== this.element.queue("bwizard").length && a.data(e, "cache.bwizard")) {
                this.element.dequeue("bwizard");
                return;
            }
            if (d.spinner) {
                g = this.element.data("spinner.bwizard");
                if (!g) {
                    g = a('<div class="modal" id="spinner" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true"/>');
                    g.html(d.spinner || c._defaults.spinner);
                    g.appendTo(document.body);
                    this.element.data("spinner.bwizard", g);
                    g.modal();
                }
            }
            this.xhr = a.ajax(a.extend({}, d.ajaxOptions, {
                url: f,
                dataType: "html",
                success: function(f, g) {
                    a(e).html(f);
                    if (d.cache) a.data(e, "cache.bwizard", true);
                    c._trigger("load", null, c._ui(c.panels[b]));
                    try {
                        if (d.ajaxOptions && d.ajaxOptions.success) d.ajaxOptions.success(f, g);
                    } catch (h) {}
                },
                error: function(a, f) {
                    c._trigger("load", null, c._ui(c.panels[b]));
                    try {
                        if (d.ajaxOptions && d.ajaxOptions.error) d.ajaxOptions.error(a, f, b, e);
                    } catch (g) {}
                }
            }));
            c.element.dequeue("bwizard");
            return this;
        },
        abort: function() {
            this.element.queue([]);
            this.panels.stop(false, true);
            this.element.queue("bwizard", this.element.queue("bwizard").splice(-2, 2));
            if (this.xhr) {
                this.xhr.abort();
                delete this.xhr;
            }
            this._removeSpinner();
            return this;
        },
        url: function(a, b) {
            this.panels.eq(a).removeData("cache.bwizard").data("load.bwizard", b);
            return this;
        },
        count: function() {
            return this.panels.length;
        }
    });
}(jQuery);