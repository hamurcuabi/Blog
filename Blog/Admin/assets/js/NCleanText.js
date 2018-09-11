(function ($) {
    $.fn.NCleanText = function () {
        var lang = 'TR';
        var iconDirection = (lang == "AR" ? 'left' : 'right');
        return this.each(function () {
            if (!$(this).parent().hasClass('input-icon')) {
                var txt = $(this);
                var txtSpanTypeClass = ' input-icon';
                var txtIconClass = ' ace-icon fa fa-times grey clean-text-icon';
                var txtIconTypeClass = '';
                var isLangAR = (lang == "AR");
                if (lang != "AR") {
                    txtSpanTypeClass += " input-icon-" + iconDirection;
                }

                if (txt.hasClass('clean-text-ntextbox')) {
                    txtIconTypeClass = 'clean-text-icon-ntextbox-' + iconDirection;
                }
                else if (txt.parent().hasClass('input-group') && txt.hasClass('clean-text-autocomplete')) {
                    txtSpanTypeClass += " padding-" + iconDirection + "-2n";
                    txtIconTypeClass = 'clean-text-icon-autocomplete-' + iconDirection;
                }
                else if (!txt.parent().hasClass('input-group') && txt.hasClass('clean-text-autocomplete')) {
                    txtSpanTypeClass += " padding-" + iconDirection + "-2n";
                    txtIconTypeClass = 'clean-text-icon-' + iconDirection;
                }
                else if (txt.hasClass('clean-text-mail')) {
                    txtSpanTypeClass += " padding-" + iconDirection + "-2n";
                    txtIconTypeClass = 'clean-text-icon-mail-' + iconDirection;
                }
                else if (txt.hasClass('clean-text-duration')) {
                    txtSpanTypeClass += " padding-" + iconDirection + "-2n";
                    txtIconTypeClass = 'clean-text-icon-duration-' + iconDirection;
                }
                else if (txt.hasClass('clean-text-date')) { }
                else if (txt.hasClass('clean-text-ntextboxmodal')) {
                    txtIconTypeClass = 'clean-text-icon-' + iconDirection;
                }
                else {
                    txtSpanTypeClass += " width-100-percent padding-" + iconDirection + "-2n";
                    txtIconTypeClass = 'clean-text-icon-' + iconDirection;
                }

                if (txt.hasClass('clean-text-date')) {
                    txt.parent().addClass(txtSpanTypeClass);
                }
                else if (txt.parent().hasClass('input-group') && txt.hasClass('clean-text-autocomplete')) {
                    txt.parent().addClass(txtSpanTypeClass);
                }
                else if (!txt.parent().hasClass('input-group') && txt.hasClass('clean-text-autocomplete')) {
                    txt.parent().addClass(txtSpanTypeClass);
                }
                else if (txt.parent().hasClass('input-group') && txt.hasClass('clean-text-mail')) {
                    txt.parent().addClass(txtSpanTypeClass);
                }
                else if (txt.parent().hasClass('input-group') && txt.hasClass('clean-text-ntextbox')) {
                    txt.parent().addClass(txtSpanTypeClass);
                }
                else if (txt.parent().hasClass('input-group') && txt.hasClass('clean-text-ntextboxmodal')) {
                    txt.parent().addClass(txtSpanTypeClass);
                }
                else if (txt.parent().hasClass('input-group')) {
                    txt.parent().addClass(txtSpanTypeClass);
                } else {
                    $('<span id="sp_' + this.id + '" class="' + txtSpanTypeClass + '"></span>').appendTo(txt.parent());
                    txt.appendTo($('#sp_' + this.id));
                }
                var icon = '<i id="i_' + this.id + '" class="' + txtIconClass + ' ' + txtIconTypeClass + '"></i>';
                $(icon).insertAfter(txt);

                $('#i_' + this.id).click(function () {
                    var text = $(this).prev('input,text,.clean-text');
                    if (text && text.length > 0) {
                        text.val('');
                        text.change();
                        var isTextDateBox = $(text).hasClass('clean-text-date');
                        if (!isTextDateBox) {
                            text.focus();
                        }
                    }
                });
                $(this).focus(function () {
                    var icon = $(this).next('i.clean-text-icon');
                    if (icon && icon.length > 0) {
                        icon.addClass('clean-text-opacity');
                    }
                });
                $(this).blur(function () {
                    var icon = $(this).next('i.clean-text-icon');
                    if (icon && icon.length > 0) {
                        icon.removeClass('clean-text-opacity');
                    }
                });

            }
        });
    };
}(window.jQuery));

