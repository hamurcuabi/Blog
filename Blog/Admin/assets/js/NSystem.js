var modalCount = 0;
function ShowMessage(type, message, focusObject, timeOut) {
    var msg = null;
    var overlay = null;
    var html = '';
    if (window.top.$('#dvNMessage').length > 0) {
        msg = window.top.$('#dvNMessage');
        overlay = window.top.$('#dvNMessageOverlay');
    }
    else {
        msg = window.top.document.createElement('div');
        msg.id = 'dvNMessage';
        msg.className = 'md-modal md-effect-1';
        msg = window.top.$(msg);
        $(window.top.document.body).append(msg);

        overlay = window.top.document.createElement('div');
        overlay.id = 'dvNMessageOverlay';
        overlay = window.top.$(overlay);
        $(window.top.document.body).append(overlay);
    }
    overlay.removeClass('md-overlay-success').removeClass('md-overlay-info');
    overlay.addClass('md-overlay' + (type == "Success" ? " md-overlay-success" : (type == 'Info' ? ' md-overlay-info' : '')));
    html += '<div class="md-content' + (type == "Success" ? " md-content-success" : (type == 'Info' ? ' md-content-info' : '')) + '">';
    html += '<div class="md-header">';
    if (type == 'Info') {
        html += '<i class="fa fa-info-circle"></i>'
        html += '<h3> Bilgilendirme </h3>';
    }
    else if (type == 'Error') {
        html += '<i class="fa fa-minus-circle"></i>'
        html += '<h3> Hatalı İşlem </h3>';
    }
    else if (type == 'Success') {
        html += '<i class="fa fa-check-circle "></i>'
        html += '<h3> Başarılı İşlem </h3>';
    }
    html += '<a href="javascript:void(0);" class="close">X</a>'
    html += '</div>'
    html += '<div>';
    html += htmlDecode(message);
    html += '</div>';
    html += '</div>';

    msg.html(html);
    $(this).unbind('click');
    msg.find('.close').bind('click', function () {
        if (focusObject) {
            $('#' + focusObject).focus();
        }
        msg.removeClass('md-show');

    })

    if (timeOut && timeOut > 0) {
        setTimeout(function () { msg.removeClass('md-show'); }, timeOut);
    }

    setTimeout(function () { msg.addClass('md-show'); }, 100);
}
function Notification(typeNoti, message) {
    if (typeNoti == "info") {
        new PNotify({
            title: 'Bilgilendirme',
            text: message,
            delay: 2500,
            type: typeNoti
        });
    }
    else if (typeNoti == "error") {
        new PNotify({
            title: 'Hata!',
            text: message,
            delay: 2500,
            type: typeNoti
        });
    }
    else if (typeNoti == "success") {
        new PNotify({
            title: 'Başarılı',
            text: message,
            delay: 2500,
            type: typeNoti
        });
    }
    else if (typeNoti == "warning") {
        new PNotify({
            title: 'Uyarı',
            text: message,
            delay: 2500,
            type: typeNoti
        });
    }
    else {
        new PNotify({
            title: '!',
            text: message,
            delay: 2500,
            type: typeNoti
        });
    }
}
function ShowModal(src, frameSize, frameHeight, caption, submitObject) {
    var mdl = null;
    var html = '';
    window.top.modalCount++;

    if (window.top.$('#dvNMModal' + window.top.modalCount).length > 0) {
        mdl = window.top.$('#dvNMModal' + window.top.modalCount);
    }
    else {
        mdl = window.top.document.createElement('div');
        mdl.id = 'dvNMModal' + window.top.modalCount;
        mdl.className = 'modal modal-padding-right fade' + (frameSize == 'lg' ? ' bs-example-modal-lg' : (frameSize == 'sm' ? ' bs-example-modal-sm' : (frameSize == 'full' ? ' bs-example-modal-lg' : '')));
        mdl.setAttribute('tabindex', '-1');
        mdl.setAttribute('role', 'dialog');
        mdl.setAttribute('aria-labelledby', 'myModalLabel' + window.top.modalCount);
        mdl.setAttribute('aria-hidden', 'true');
        mdl = window.top.$(mdl);
        mdl.css('z-index', 100 + (window.top.modalCount * 10))
        mdl.css('overflow', 'hidden');
    }
    html += '<div class="modal-dialog' + (frameSize == 'lg' ? ' modal-lg' : (frameSize == 'sm' ? ' modal-sm' : (frameSize == 'full' ? ' modal-full' : ''))) + ' modal-dialog-margin" style="max-height:' + frameHeight + 'px!important;overflow:visible;z-index:' + 100 + (window.top.modalCount * 10) + '!important;">';
    html += '<div class="modal-content">';
    html += '<div class="modal-header">';
    html += '<button id="btnCloseModal" type="button" class="close" title="Kapat" onclick="CloseModal(true);">x</button>';
    html += '<h4 class="modal-title" id="myModalLabel' + window.top.modalCount + '">' + caption + '</h4>';
    html += '</div>';
    html += '<div class="modal-body" style="padding:0px!important;height:' + frameHeight + 'px!important;">';
    html += '<iframe src="' + src + '" id="NFrame' + window.top.modalCount + '"  name="NFrame' + window.top.modalCount + '"  frameborder="0" style="width:100%;height:100%;" /> ';
    html += '</div>';
    html += '</div>';
    html += '</div>';

    mdl.html(html);
    if (submitObject) {
        mdl.attr('submitObject', submitObject);
    }
    else {
        mdl.attr('submitObject', null);
    }

    var m = mdl.modal({ show: true, backdrop: 'static', keyboard: false });



    mdl.find('.modal-content').draggable({
        handle: ".modal-header"
    });
}
function CloseModal(doSubmit, submitObject) {
    if (window.top.$('#dvNMModal' + window.top.modalCount).length > 0) {
        var modal = window.top.$('#dvNMModal' + window.top.modalCount);
        modal.modal('hide');
        if (doSubmit) {
            if (submitObject) {
                fp(submitObject).click();
            }
            else if (modal.attr('submitObject')) {
                fp(modal.attr('submitObject')).click();
            }
        }
        window.top.modalCount--;
    }
}
function fp(obj) {
    if (window.top.modalCount == 1) { /*1. seviye*/
        return window.top.$('#' + obj);
    }
    else {
        return window.top.$('#NFrame' + (window.top.modalCount - 1)).contents().find('#' + obj)
    }
}
function fo(obj) {
    if (window.top.modalCount == 0) { /*1. seviye*/
        return window.top.$('#' + obj);
    }
    else {
        return window.top.$('#NFrame' + (window.top.modalCount)).contents().find('#' + obj)
    }
}
function htmlEncode(value) {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}
function htmlDecode(value) {
    return $('<div/>').html(value).text();
}
if (Sys) {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestPublic);
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestPublic);
}

function BeginRequestPublic() {
   // ShowIndicator();
}
function EndRequestPublic() {
    InitializePagePublic();

    $(":input[SubmitButton]").each(function () {
        $(this).keyup(function (event) {
            if (event.keyCode == 13) {
                $('#' + $(this).attr("SubmitButton"))[0].click();
                event.preventDefault();
            }
        });
    });
    setTimeout(function () { HideIndicator(); }, 100);
}

function InitializePagePublic() {
    var currentCulture = 'TR';
    var dateFormat = 'dd.mm.yyyy';

    $('.date-box').datepicker({ autoclose: true, todayHighlight: true, language: currentCulture.toLowerCase().replace('zh', 'zh-CN') }).next().on(ace.click_event, function () { $(this).prev().focus(); });

    $('.dateboxleft').datepicker({
        autoclose: true, todayHighlight: true, language: currentCulture.toLowerCase().replace('zh', 'zh-CN')
    })
        .prev().on(ace.click_event, function () {
            $(this).next().focus();
        });

    $(".tablesorter").tablesorter(); 
    $(".treetable").treetable({ expandable: true });
    $(".search").keyup(function () {
        var searchTerm = $(".search").val();
        var listItem = $('.results tbody').children('tr');
        var searchSplit = searchTerm.replace(/ /g, "'):containsi('")

        $.extend($.expr[':'], {
            'containsi': function (elem, i, match, array) {
                return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
            }
        });

        $(".results tbody tr").not(":containsi('" + searchSplit + "')").each(function (e) {
            $(this).attr('visible', 'false');
        });

        $(".results tbody tr:containsi('" + searchSplit + "')").each(function (e) {
            $(this).attr('visible', 'true');
        });

        var jobCount = $('.results tbody tr[visible="true"]').length;
        $('.counter').text(jobCount + ' item');

        if (jobCount == '0') { $('.no-result').show(); }
        else { $('.no-result').hide(); }
    });

    $('.btn-delete').click(function (e) {
        var id = this.id;
        e.preventDefault();
        bootbox.setLocale('tr');
        bootbox.confirm(
            {
                size: 'small',
                message: 'Bu kayıt ve ilişkili tüm kayıtlar silinecektir!\nOnaylıyor musunuz?',
                callback: function (confirmed) {
                    if (confirmed) {
                        __doPostBack(id, '');
                    }
                }
            }
        );
    });
    //$('.date-time-box').datetimepicker({ sideBySide: true, format: 'DD.MM.YYYY HH:mm', weekStart :1}).next().on(ace.click_event, function () { $(this).prev().focus(); });
    $('[data-rel="tooltip"]').tooltip({ html: true });
    //$('.input-mask-phone').mask('0(999) 999 99 99');
    $(":input[SubmitButton]").each(function () {
        $(this).keydown(function (event) {
            if (event.keyCode == 13) {
                var btn = $('#' + $(this).attr("SubmitButton"));
                //ToDo: eger href ise farkli button ise farkli olacak
                eval(btn.attr('href'));
                return false;

            }
        });

    });

    $('.clean-filter').click(function () {
        $(this).parents('.filter').find('input,select').each(
            function (i, o) {
                switch (this.tagName) {
                    case 'INPUT':
                        {
                            switch (this.type) {
                                case 'text': { $(this).val(''); break; }
                            }
                            break;
                        }
                    case 'SELECT':
                        {
                            $(this).val('0');
                            break;
                        }

                }
            }
        );
    });


    //clean-text start: textbox içeriğini temizlemek için
    //$('.clean-text-icon').click(function () {
    //    var text = $(this).prev('input,text,.clean-text');
    //    if(text && text.length > 0){
    //        text.val('');
    //        text.change();
    //        var isTextDateBox = $(text).hasClass('clean-text-date');
    //        if (!isTextDateBox) {
    //            text.focus();
    //        }
    //    }
    //});
    //$('.clean-text').focus(function () {
    //    var icon = $(this).next('i.clean-text-icon');
    //    if (icon && icon.length > 0) {
    //        icon.addClass('clean-text-opacity');
    //    }
    //});
    //$('.clean-text').blur(function () {
    //    var icon = $(this).next('i.clean-text-icon');
    //    if (icon && icon.length > 0) {
    //        icon.removeClass('clean-text-opacity');
    //    }
    //});
    //clean-text finish


    //SetActiveMenu();
    //SetFilter();
}

function SetActiveMenu() {
    var menuID = $("input[id$='hdnLastMenuID']").val();
    if (menuID) {
        if (menuID != '') {
            var menuItem = $("a[data-menuid='" + menuID + "']");
            if (menuItem) {
                menuItem.parents('li').attr('class', 'open active');
            }
        }
    }
}

var indicatorTOP = null;
function ShowIndicator() {
    indicatorTOP = null;
    var html = '';
    if (window.top.$('#dvIndicator').length > 0) {
        indicatorTOP = window.top.$('#dvIndicator');
    }
    else {
        indicatorTOP = window.top.document.createElement('div');
        indicatorTOP.id = 'dvIndicator';
        indicatorTOP.className = 'modal fade bs-example-modal-sm';
        indicatorTOP.setAttribute('tabindex', '-1');
        indicatorTOP.setAttribute('role', 'dialog');
        indicatorTOP.setAttribute('aria-labelledby', 'myModalLabel' + window.top.modalCount);
        indicatorTOP.setAttribute('aria-hidden', 'true');
        indicatorTOP = window.top.$(indicatorTOP);
        indicatorTOP.css('z-index', 105 + (window.top.modalCount * 10))
        indicatorTOP.css('overflow', 'hidden');
    }
    html += '<div class="modal-dialog modal-sm" style="max-height:100px!important;overflow:visible;z-index:' + 105 + (window.top.modalCount * 10) + '!important;">';
    html += '<div class="modal-content">';

    html += '<div class="modal-body" style="padding:10px!important;text-align:center">';
    html += '<div><img src="/Admin/assets/images/indicator.gif" width="100px" height="100px"/></div>';
    html += '<div><b>İşleminiz Yapılıyor...</b></div>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    indicatorTOP.css('z-index', 105 + (window.top.modalCount * 10))
    indicatorTOP.css('overflow', 'hidden');
    indicatorTOP.html(html);
    indicatorTOP.modal({ show: true, backdrop: 'static', keyboard: false });
}
function HideIndicator() {
    if (window.top.$('#dvIndicator') != null) {
        indicatorTOP.modal('hide')
    }
}
function GetQueryString(param) {
    var retval = '';
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        if (hashes[i].split('=')[0] == param) {
            retval = hashes[i].split('=')[1];
            break;
        }
    }
    return retval;
}
function FormatDate(input) {
    return input.format('dd.MM.yyyy HH:mm');
}
function FormatDateUniversal(input) {
    return input.format('yyyy-MM-dd HH:mm:ss.fff');
}
function ConvertJsonDate(input) {
    return eval(input.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
}
function Complete(completeObject, url, selectedHidden) {
    $("#" + completeObject).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: url,
                type: "POST",
                contentType: 'application/json',
                dataType: 'json',
                data: '{query:"' + request.term + '"}',
                success: function (data) {
                    response(data.d);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                },
            });
        },
        minLength: 2,
        autoFocus: true,
        select: function (event, ui) {
            $('#' + selectedHidden).val(ui.item.value);
            $("#" + completeObject).val(ui.item.label.split('~')[0]);
            $(this).autocomplete('widget').css('z-index', 0);

            event.preventDefault();
            return false;
        },
        open: function () {
            $(this).autocomplete('widget').css('z-index', 100);
            $(this).autocomplete('widget').css('float', 'right');
            return false;
        },

    }).autocomplete("instance")._renderItem = function (ul, item) {
        var lbl = item.label.split('~');
        var html = ''

        html += "<div class='AutoCompleteResult'>";
        html += "<div class='row'><div class='col-xs-12'>" + lbl[0] + "</div></div>";
        if (lbl.length > 1) {
            html += "<div class='row'>" + lbl[1] + "</div>";
        }
        if (lbl.length > 2) {
            html += "<div class='row'>" + lbl[2] + "</div>";
        }
        html += "</div>";

        return $("<li>")
            .append(html)
            .appendTo(ul);
    };
    $("#" + completeObject).on('change', function () { $('#' + selectedHidden).val(''); });
}
function MenuClicked(menuID) {
    $.ajax({
        url: "/WebService/Member.asmx/MenuClicked"
        , type: 'POST'
        , data: "menuID=" + menuID
        , cache: false
    });
}



function SetUniqueRadioButton(nameregex, current) {
    $("input:radio[name*='" + nameregex + "']").each(
        function () {
            if (this.id != current.id) {
                this.checked = false;
            }
        });
    current.checked = true;
}
$(document).ready(function () { InitializePagePublic(); })


$('.OnlyNumeric').keydown(function (e) {
    if (e.which != 8 && e.which != 13) {
        if (!String.fromCharCode(e.which).match(/[0-9]/g)) {
            e.preventDefault();
        }
    }
});