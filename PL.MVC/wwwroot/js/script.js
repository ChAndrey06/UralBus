var globalVariables = {};

$(window).on("load", function () {
    $('#phone').mask('+9 (999) 999-99-99');
    //$('#tel').mask('+9(999)999-99-99')
    $('#modal__input').mask('(999) 999-99-99')
    $('#modal__aunth-input').mask('(999) 999-99-99')
    $('#date').mask('99/99/9999')
    select = $(".select1").selectize({
        onChange: function (value) {

            const el = $('#trip-orderBy-query');
            el.val(value);
        },
        onInitialize: function () {

            const el = $('#trip-orderBy-query');
            const selectedItem = this.options[this.items[0]];
            el.val(selectedItem.value);
        }
    });

    const select1 = $(".modal__select").selectize({
        valueField: "code",
        labelField: "code",
        searchField: ["country", "code"],
        onFocus() {
            const parent = document.querySelector('.modal__form')
            const dropdown = $(parent).find('.selectize-dropdown')
            const btn = document.querySelector('.modal__btn')
            const dropdownBtn = document.querySelector('.modal__btn--hidden')
            dropdown[0].classList.add('selectize-dropdown--active')
            btn.classList.add('modal__btn--hidden')
            dropdownBtn.classList.remove('modal__btn--hidden')
        },
        options: [
            { country: "Russia1", code: "+7" },
            { country: "Belarus1", code: "+8" },
            { country: "Ukraine1", code: "+9" },
            { country: "Russia2", code: "+723" },
            { country: "Belarus2", code: "+238" },
            { country: "Ukraine2", code: "+249" },
            { country: "Russia3", code: "+722" },
            { country: "Belarus3", code: "+83" },
            { country: "Ukraine3", code: "+92" },
            { country: "Russia4", code: "+76" },
            { country: "Belarus4", code: "+88" },
            { country: "Ukraine4", code: "+90" },
        ],
        render: {
            option: function (item, escape) {
                var label = item.code || item.country;
                var caption = item.code ? item.country : null;
                return (
                    "<div class='modal__item'>" +
                    '<span class="modal__label">' +
                    escape(caption) +
                    "</span>" +
                    (caption
                        ? '<span class="modal__caption">' + escape(label) + "</span>"
                        : "") +
                    "</div>"
                );
            },
        },
        // ------------------------------------------
        load: function (callback) {
            // Select the first item in the options array
            const defaultOption = this.options[0].code;
            this.setValue(defaultOption);

            // Trigger the onChange event
            this.onChange(defaultOption);
            this.onFocus();
            callback();
        },
        // ------------------------------------------
        onChange: function (value) {

            const countryCode = value;
            const countryCodeEl = $('#country-code');

            countryCodeEl.val(countryCode);
        }
    });

    $("#modal__aunth-input").keyup(function (e) {
        if ($(this).val().replace(/\D/g, '').length < 10) {
            return;
        }

        const phoneNumberEl = $('#phone-number');

        phoneNumberEl.val($(this).val().replace(/\D/g, ''));
    });

    $('.modal__code-item').keydown(function (e) {
        $(this).val('');
    });
    $('.modal__code-item').keyup(function (e) {
        var wrap = $(this).closest('.modal__code-value');
        var $inputs = wrap.find('input[type="number"]');
        var val = $(this).val();

        if (val == val.replace(/[0-9]/, '')) {
            $(this).val('');
            return false;
        }

        $inputs.eq($inputs.index(this) + 1).focus();

        var fullval = '';
        $inputs.each(function () {
            fullval = fullval + (parseInt($(this).val()) || '0');
        });
        wrap.find('input[type="hidden"]').val(fullval);
    });


    const swiper = new Swiper('.content__directions-swiper', {
        direction: 'horizontal',
        loop: true,
        spaceBetween: 30,
        breakpoints: {
            320: {
                slidesPerView: 1.1,
            },
            720: {
                slidesPerView: 2.1,
            },
            1110: {
                slidesPerView: 3.1,
            },
        }
    })

    const swiper1 = new Swiper('.swiper', {
        direction: 'horizontal',
        loop: true,
        enabled: true,
        spaceBetween: 30,
        slidesPerView: 1.1,
    })

    $('.tripOrder__switch').on('click', () => {
        let first = $('#from')
        let firstVal = $('#from').val()
        let firstPlaceholder = $('#from').attr('placeholder')
        let secondVal = $('#from').val()
        let second = $('#to')
        if (firstVal) {
            $(first).val(second.val())
            $(second).val(firstVal)
        }
    })

    var marks = document.querySelectorAll('.details__busStop')
    marks.forEach(function (elem) {
        elem.addEventListener('click', function () {
            //for (let i = 0; i < marks.length; i++) {
            //    marks[i].classList.remove('details__busStop--active')
            //}
            //this.classList.add('details__busStop--active')

            if (this.classList.contains('details__busStop--active')) {
                this.classList.remove('details__busStop--active');
            } else {
                this.classList.add('details__busStop--active')
            }

            var selectedMarks = document.querySelectorAll('.details__busStop--active > input')

            if (selectedMarks.length == 2) {
                $('#start-point-id').val($(selectedMarks[0]).val());
                $('#end-point-id').val($(selectedMarks[1]).val());
                $('#marks-form').submit();
            }
        })
    })

    let firstOption = $('.tripOrder__options').children()[0];
    let secondOption = $('.tripOrder__options').children()[1];
    let data = $('.tripOrder__when').find('.tripOrder__input');

    $(firstOption).on('click', () => {
        let tomorrow = new Date()
        tomorrow.setDate(tomorrow.getDate() + 1)
        tomorrow = tomorrow.toLocaleDateString('en-CA').split('/').reverse().join('-')
        $(data).val(tomorrow)
    })

    $(secondOption).on('click', () => {
        let tomorrow = new Date()
        tomorrow.setDate(tomorrow.getDate() + 2)
        tomorrow = tomorrow.toLocaleDateString('en-CA').split('/').reverse().join('-')
        $(data).val(tomorrow)
    })

    var targetMap = document.querySelectorAll('[data-target]'),
        map = document.querySelectorAll('.faq__content')
    targetMap?.forEach((elem) => {
        elem.addEventListener('click', function (e) {
            e.preventDefault()
            var target = this.getAttribute('data-target')
            map.forEach((elem) => {
                elem.classList.remove('faq__content--opacity', 'faq__content--active')
            })
            targetMap.forEach((elem) => {
                elem.classList.remove('faq__tabs-item--active')
            })
            this.classList.add('faq__tabs-item--active')
            var cat = document.querySelector('[data-info="' + target + '"]')
            cat.classList.add('faq__content--active')
            setTimeout(() => {
                cat.classList.add('faq__content--opacity')
            }, 400)
        })
    })

    const parent = document.querySelector('.modal__form')
    const dropdown = $(parent).find('.selectize-dropdown')
    const formBtn = document.querySelectorAll('.modal__btn')[1]
    const modalBtn = document.querySelectorAll('.modal__btn')[1]
    const btn = document.querySelector('.modal__btn')
    const dropdownBtn = document.querySelector('.modal__btn--hidden')
    const overlay = document.querySelector('.modal__overlay')

    modalBtn.addEventListener('click', function () {
        dropdown[0].classList.remove('selectize-dropdown--active')
        btn.classList.remove('modal__btn--hidden')
        dropdownBtn.classList.add('modal__btn--hidden')
    })
    overlay.addEventListener('click', function () {
        //
        hideModals()
    })

    function hideModals() {
        $('.modal__box').addClass('modal__box--hidden')
        $('.welcome').addClass('welcome--hidden')
        $('.modal__overlay').addClass('modal__overlay--hidden')
        $('.modal__code').addClass('modal__code--hidden')
        $('.modal__registration').addClass('modal__registration--hidden')
    }

    let aunth = document.querySelectorAll('.register')
    let aunth1 = document.querySelectorAll('.enter')
    function openModalRegistration() {
        $('.burger').removeClass('burger--open')
        $('.header__burger').removeClass('header__burger--open')
        $('.modal__box').removeClass('modal__box--hidden')
        $('.modal__overlay').removeClass('modal__overlay--hidden')
        $.ajax({
            url: '/Public/Auth/RegisterAndSendCode?phone=' + "encodedPhone",
            type: 'GET',

        });
    }
    function openModalAunth() {
        $('.burger').removeClass('burger--open')
        $('.header__burger').removeClass('header__burger--open')
        $('.modal__registration').removeClass('modal__registration--hidden')
        $('.modal__overlay').removeClass('modal__overlay--hidden')

        $.ajax({
            url: '/Public/Auth/RegisterAndSendCode?phone=' + "encodedPhone",
            type: 'GET',

        });
    }

    if (aunth) {
        aunth.forEach((element) => {
            element.addEventListener('click', openModalRegistration)

        })
        aunth1.forEach((element) => {
            element.addEventListener('click', openModalAunth)
        })
    }

    let x = $('.header__icons-item--notauthorized')[0];
    if (x == undefined) {

        $('.header__nav-item--extra').css('display', 'block')
    }

    function userOptionOpen(e) {
        e.preventDefault()
        $('.header__userOptions').toggleClass('header__userOptions--hidden')
    }

    $('#form-registration').on('submit', function (e) {
        e.preventDefault()

        const phone = $('#country-code').val() + $('#modal__input').val().replace(/\D/g, '');
        const encodedPhone = encodeURIComponent(phone);
        //$('.modal__box').addClass('modal__box--hidden')

        $.ajax({
            url: '/Public/Auth/RegisterAndSendCode?phone=' + encodedPhone,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            statusCode: {
                200: function (data) {
                    $('.modal__box').addClass('modal__box--hidden');
                    $('.modal__code').removeClass('modal__code--hidden');
                    $('#phone-number-span').text(formatPhoneNumber(phone));
                    localStorage.setItem('auth.phone', phone);
                },
                400: function (data) {
                    const authCodeErrorEl = $('#registration-code-error');
                    authCodeErrorEl.css("display", "block");
                    var error = "Пользователь с таким номером телефона уже существует";
                    authCodeErrorEl.text(error);
                },
                default: function (jqXHR, textStatus, errorThrown) {
                    $("#auth-error").text('Ошибка. Попробуйте позднее');
                    console.log(textStatus + jqXHR.responseText);
                }
            }
        });


        switch (data) {
            case 200:
                $('.welcome').removeClass('welcome--hidden')
                setTimeout(() => {
                    $('.welcome').addClass('welcome--hidden')
                    $('.modal__overlay').addClass('modal__overlay--hidden')
                    $('#header-icon').removeClass('header__icons-icon--notauthorized').addClass('header__icons-item--authorized')
                    $('.header__aunth').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
                    $('#header-icon--burger').removeClass('header__icons-icon--notauthorized').addClass('header__icons-icon--authorized')
                    $('.header__aunth--burger').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
                    $('.header__nav-item--extra').css('display', 'block')


                    aunth.forEach((element) => {
                        element.removeEventListener('click', openModalRegistration)
                    })
                    aunth1.forEach((element) => {
                        element.removeEventListener('click', openModalAunth)
                    })
                }, 3000);
                break;
            default:
                break;
            // $('.modal__code').removeClass('modal__code--hidden')
        }
    })
    // --------------------------------------------
    $('.form-auth-phone').on('submit', function (e) {
        e.preventDefault()

        const phone = $('#country-code').val() + $('#phone-number').val()
        const encodedPhone = encodeURIComponent(phone);

        $("#auth-error").css("display", "none");

        $.ajax({
            url: '/Public/Auth/SendLoginCode?phone=' + encodedPhone,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                console.log(response);
            },
            statusCode: {
                404: function (jqXHR, textStatus, errorThrown) {
                    $("#auth-error").css("display", "block");

                    $("#auth-error").text('Пользователь с таким номером не найден');
                },
                200: function () {
                    $('.modal__registration').addClass('modal__registration--hidden');
                    $('.modal__code').removeClass('modal__code--hidden');
                    $('#phone-number-span').text(formatPhoneNumber(phone));
                    localStorage.setItem('auth.phone', phone);
                },
                default: function (jqXHR, textStatus, errorThrown) {
                    $("#auth-error").text('Ошибка. Попробуйте позднее');
                    console.log(textStatus + jqXHR.responseText);
                }
            }
        });





        // $('.welcome').removeClass('welcome--hidden')

        // setTimeout(() => {
        //     $('.welcome').addClass('welcome--hidden')
        //     $('.modal__overlay').addClass('modal__overlay--hidden')
        //     $('#header-icon').removeClass('header__icons-icon--notauthorized').addClass('header__icons-item--authorized')
        //     $('.header__aunth').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
        //     $('#header-icon--burger').removeClass('header__icons-icon--notauthorized').addClass('header__icons-icon--authorized')
        //     $('.header__aunth--burger').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
        //     $('.header__nav-item--extra').css('display', 'block')
        //     $('.header__icons-item--authorized').on('click', userOptionOpen)

        //     aunth.forEach((element) => {
        //         element.removeEventListener('click', openModalRegistration)
        //     })
        //     aunth1.forEach((element) => {
        //         element.removeEventListener('click', openModalAunth)
        //     })
        // }, 3000);
    })

    $('#form-auth-code').on('submit', function (e) {
        e.preventDefault()
        //;
        const code = $('#form-auth-code-input').val();
        const phone = localStorage.getItem('auth.phone');
        localStorage.setItem('exit', false);
        $.ajax({
            url: '/Public/Auth/SmsAuth',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                phone: phone,
                code: code
            }),
            success: function (response) {
                console.log(response);
            },
            statusCode: {
                400: function (jqXHR, textStatus, errorThrown) {
                    const authCodeErrorEl = $('#auth-code-error');
                    authCodeErrorEl.css("display", "block");
                    var error = "";

                    switch (jqXHR.responseText) {
                        case "Invalid":
                            error = "Неверный код";
                            authCodeErrorEl.text(error);
                            break;

                        case "ExpiredByTime":
                            error = "Время действия кода истекло";
                            authCodeErrorEl.text(error);
                            break;

                        case "AlreadyUsed":
                            error = "Код уже использован";
                            authCodeErrorEl.text(error);
                            break;


                        default:
                            error = jqXHR.responseText;
                            authCodeErrorEl.text(error);
                            break;
                    }

                },
                404: function (jqXHR, textStatus, errorThrown) {
                    error = "Пользователь с таким номером не найден";
                    authCodeErrorEl.text(error);
                },
                200: function (jqXHR, textStatus, errorThrown) {
                    $('.modal__registration').addClass('modal__registration--hidden');
                    $('.modal__code').addClass('modal__code--hidden');
                    $('#phone-number-span').text(formatPhoneNumber(phone));
                    localStorage.setItem('auth.phone', phone);

                    const cookie = jqXHR.getResponseHeader("Set-Cookie");
                    setTimeout(function () {
                        document.cookie = cookie;

                        location.reload();
                    }, 4000);

                    // Set cookie


                    localStorage.setItem('auth.showAuthSucceedWindow', "true");

                    showAuthSucceedWindow();
                },
                default: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus + jqXHR.responseText);
                }
            }
        });
    });
    function showAuthSucceedWindow() {
        //
        if ("true" != localStorage.getItem("auth.showAuthSucceedWindow")) {
            return;
        }
        $('.welcome').removeClass('welcome--hidden')
        setTimeout(() => {
            $('.welcome').addClass('welcome--hidden')
            $('.modal__overlay').addClass('modal__overlay--hidden')
            $('#header-icon').removeClass('header__icons-icon--notauthorized').addClass('header__icons-item--authorized')
            $('.header__aunth').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
            $('#header-icon--burger').removeClass('header__icons-icon--notauthorized').addClass('header__icons-icon--authorized')
            $('.header__aunth--burger').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
            $('.header__nav-item--extra').css('display', 'block')

            aunth.forEach((element) => {
                element.removeEventListener('click', openModalRegistration)
            })
            aunth1.forEach((element) => {
                element.removeEventListener('click', openModalAunth)
            })
        }, 3000);
    }

    const phone1 = localStorage.getItem('auth.phone');
    if (phone1) {
        $('#header-icon').removeClass('header__icons-icon--notauthorized').addClass('header__icons-item--authorized')
        $('.header__aunth').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
        $('#header-icon--burger').removeClass('header__icons-icon--notauthorized').addClass('header__icons-icon--authorized')
        $('.header__aunth--burger').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
    }

    // $('.modal__form-wrapper').on('submit', function (e) {cd
    //     e.preventDefault()
    //     //
    //     $('.modal__code').addClass('modal__code--hidden')
    //     $('.welcome').removeClass('welcome--hidden')
    //     setTimeout(() => {
    //         $('.welcome').addClass('welcome--hidden')
    //         $('.modal__overlay').addClass('modal__overlay--hidden')
    //         $('#header-icon').removeClass('header__icons-icon--notauthorized').addClass('header__icons-item--authorized')
    //         $('.header__aunth').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
    //         $('#header-icon--burger').removeClass('header__icons-icon--notauthorized').addClass('header__icons-icon--authorized')
    //         $('.header__aunth--burger').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
    //         $('.header__nav-item--extra').css('display', 'block')
    //         $('.header__icons-item--authorized').on('click', userOptionOpen)
    //
    //         aunth.forEach((element) => {
    //             element.removeEventListener('click', openModalRegistration)
    //         })
    //         aunth1.forEach((element) => {
    //             element.removeEventListener('click', openModalAunth)
    //         })
    //     }, 3000);
    // })

    $('#delete').on('click', function () {

        $('.delete').removeClass('delete--hidden')
        $('.modal__overlay').removeClass('modal__overlay--hidden')
    })
    $('#delete-burger').on('click', function () {
        $('.delete').removeClass('delete--hidden')
        $('.burger').removeClass('burger--open')
        $('.modal__overlay').removeClass('modal__overlay--hidden')
        $('.header__burger').removeClass('header__burger--open')
        aunth.forEach((element) => {
            element.addEventListener('click', openModalRegistration)

        })
        aunth1.forEach((element) => {
            element.addEventListener('click', openModalAunth)
        })
    })

    $('#delete-btn').on('click', function () {
        $('.delete__buttons').submit();
        
        $('.delete').addClass('delete--hidden')
        $('.modal__overlay').addClass('modal__overlay--hidden')
        $('#header-icon').addClass('header__icons-icon--notauthorized').removeClass('header__icons-item--authorized')
        $('.header__aunth').addClass('header__aunth--notauthorized').removeClass('header__aunth--authorized')
        $('#header-icon--burger').addClass('header__icons-icon--notauthorized').removeClass('header__icons-icon--authorized')
        $('.header__userOptions').addClass('header__userOptions--hidden')
        $('.header__nav-item--extra').css('display', 'none')
        $('.header__icons-item--notauthorized').off('click', userOptionOpen)
        let aunth = document.querySelectorAll('.register')
        let aunth1 = document.querySelectorAll('.enter')
        aunth.forEach((element) => {
            element.addEventListener('click', openModalRegistration)
        })
        aunth1.forEach((element) => {
            element.addEventListener('click', openModalAunth)
        })
    })

    $('#close').on('click', function () {
        $('.delete').addClass('delete--hidden')
        $('.modal__overlay').addClass('modal__overlay--hidden')
    })
    
    $('#exit').on('click', function () {
        localStorage.removeItem('exit')

        let aunth = document.querySelectorAll('.register')
        let aunth1 = document.querySelectorAll('.enter')
        $('.header__icons-icon--notauthorized').off()

        aunth.forEach((element) => {
            element.addEventListener('click', openModalRegistration)
        })
        aunth1.forEach((element) => {
            element.addEventListener('click', openModalAunth)
        })
        $('.header__nav-item--extra').css('display', 'none')
    })
    const exit = localStorage.getItem('exit')
    const phone = localStorage.getItem('auth.phone');
    if(phone && exit !== null){
        $('#header-icon').removeClass('header__icons-icon--notauthorized').addClass('header__icons-item--authorized')
        $('.header__aunth').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
        $('#header-icon--burger').removeClass('header__icons-icon--notauthorized').addClass('header__icons-icon--authorized')
        $('.header__aunth--burger').removeClass('header__aunth--notauthorized').addClass('header__aunth--authorized')
    } else{
        $('#header-icon').removeClass('header__icons-item--authorized').addClass('header__icons-icon--notauthorized')
        $('.header__aunth').removeClass('header__aunth--authorized').addClass('header__aunth--notauthorized')
        $('#header-icon--burger').removeClass('header__icons-icon--authorized').addClass('header__icons-icon--notauthorized')
        $('.header__aunth--burger').removeClass('header__aunth--authorized').addClass('header__aunth--notauthorized')
    }
    $('#exit-burger').on('click', function () {
        $('.burger').removeClass('burger--open')
        $('.header__burger').removeClass('.header__burger--open')
        $('#header-icon').addClass('header__icons-icon--notauthorized').removeClass('header__icons-item--authorized')
        $('#header-icon--burger').addClass('header__icons-icon--notauthorized').removeClass('header__icons-icon--authorized')
        $('.header__aunth').addClass('header__aunth--notauthorized').removeClass('header__aunth--authorized')
        $('.header__userOptions').addClass('header__userOptions--hidden')
        $('.header__nav-item--extra').css('display', 'none')
        let aunth = document.querySelectorAll('.register')
        let aunth1 = document.querySelectorAll('.enter')
        $('.header__icons-item--notauthorized').off()
        aunth.forEach((element) => {
            element.addEventListener('click', openModalRegistration)
        })
        aunth1.forEach((element) => {
            element.addEventListener('click', openModalAunth)
        })
    })

    var btnPlus = document.querySelectorAll(".passengersValue__quantity-plus");
    btnPlus.forEach(function (btn) {
        btn.addEventListener("click", function () {
            const id = this.getAttribute("id");
            const el = $(`.passengersValue__quantity-value[data-id=${id}]`);
            const value = Number(el.val());
            
            if (value < 100) el.val(value + 1);
            
            // debugger;
            // const id = this.getAttribute("id");
            // const val = document.querySelector('.passengersValue__quantity-value[data-id="' + id + '"]');
            // const numberStr = val.getAttribute("value");
            // const number = Number(numberStr);
            // let result = Number(val.value);
            // result = result + number;
            // val.value = result;
        })
    })

    var btnMinus = document.querySelectorAll(".passengersValue__quantity-minus");
    btnMinus.forEach(function (btn) {
        btn.addEventListener("click", function () {
            const id = this.getAttribute("id");
            const el = $(`.passengersValue__quantity-value[data-id=${id}]`);
            const value = Number(el.val());

            if (value > 0) el.val(value - 1);
            
            // var id = this.getAttribute("id"),
            //     val = document.querySelector('.passengersValue__quantity-value[data-id="' + id + '"]'),
            //     numberStr = val.getAttribute("value"),
            //     number = Number(numberStr);
            // result = Number(val.value);
            // result = result - number;
            // if (result < number) {
            //     val.value = number
            // }
            // else {
            //     val.value = result;
            // }
        })
    })

    $('.header__icons-item--authorized').on('click', function (e) {
        e.preventDefault()
        $('.header__userOptions').toggleClass('header__userOptions--hidden')
    })

    $('.faq__accordeon').find('.faq__text-hidden').slideUp()

    $('.faq__accordeon').on('click', function (e) {
        let activeClass = 'faq__accordeon--active'
        if ($(this).hasClass(activeClass)) {
            $(this).find('.faq__text-hidden').slideUp()
            $('.faq__accordeon').removeClass(activeClass)
            $('.faq__accordeon').not(this).find('.faq__text-hidden').slideUp()
        }
        else {
            $(this).addClass(activeClass)
            $(this).find('.faq__text-hidden').slideDown()
            $('.faq__accordeon').not(this).removeClass(activeClass)
            $('.faq__accordeon').not(this).find('.faq__text-hidden').slideUp()
        }
    })

    if (window.innerWidth <= 768) {
        $('.news__list').addClass('news__list--hidden')
        $('.swiper').removeClass('swiper--hidden')
    }

    if (window.innerWidth <= 1350) {
        const burger = document.querySelector(".burger");
        navigation = document.querySelector(".header__burger");
        burger.addEventListener("click", function () {
            navigation.classList.toggle("header__burger--open");
            this.classList.toggle("burger--open");
        });
    }

    //let choiceselect = $("#from").selectize({
    //    persist: false,
    //    placeholder: '',
    //    maxItems: 1,
    //    valueField: "id",
    //    labelField: "title",
    //    searchField: ["title", "id"],
    //    placeholder: 'Саратов',
    //    options: [
    //        { id: 1, title: "Саратов" },
    //        { id: 2, title: "Энгельс" },
    //        { id: 3, title: "Маркс" },
    //        { id: 4, title: "Самара" },
    //        { id: 5, title: "Энгельс" },
    //        { id: 6, title: "Энгельс" },
    //        { id: 12, title: "Саратов" },
    //        { id: 2, title: "Энгельс" },
    //        { id: 3, title: "Энгельс" },
    //        { id: 4, title: "Энгельс" },
    //        { id: 5, title: "Энгельс" },
    //        { id: 6, title: "Энгельс" },
    //    ],
    //    render: {
    //        item: function (item, escape) {
    //            return (
    //                "<div>" +
    //                (item.title
    //                    ? `<span class="town" id="${item.id}">` + escape(item.title) + "</span>"
    //                    : "") +

    //                "</div>"
    //            );
    //        },
    //        option: function (item, escape) {
    //            var label = item.title || item.id;
    //            return (
    //                "<div>" +
    //                '<span class="label">' +
    //                escape(label) +
    //                "</span>" +
    //                "</div>"
    //            );
    //        },
    //    },
    //});
    //let choiceselect1 = $("#to").selectize({
    //    persist: false,
    //    maxItems: 1,
    //    valueField: "id",
    //    labelField: "title",
    //    searchField: ["title", "id"],
    //    value: '1',
    //    placeholder: 'Энгельс',
    //    options: [
    //        { id: 1, title: "Саратов" },
    //        { id: 2, title: "Энгельс" },
    //        { id: 3, title: "Маркс" },
    //        { id: 4, title: "Самара" },
    //        { id: 5, title: "Энгельс" },
    //        { id: 6, title: "Энгельс" },
    //        { id: 12, title: "Саратов" },
    //        { id: 2, title: "Энгельс" },
    //        { id: 3, title: "Энгельс" },
    //        { id: 4, title: "Энгельс" },
    //        { id: 5, title: "Энгельс" },
    //        { id: 6, title: "Энгельс" },
    //    ],
    //    onitialize: function () {
    //        this.setValue({ id: 3, title: "Энгельс" })
    //    },
    //    render: {
    //        item: function (item, escape) {
    //            return (
    //                "<div>" +
    //                (item.title
    //                    ? `<span class="town" id="${item.id}">` + escape(item.title) + "</span>"
    //                    : "") +

    //                "</div>"
    //            );
    //        },
    //        option: function (item, escape) {
    //            var label = item.title || item.id;
    //            return (
    //                "<div>" +
    //                '<span class="label">' +
    //                escape(label) +
    //                "</span>" +
    //                "</div>"
    //            );
    //        },
    //    },
    //});

    //$.ajax({
    //    url: '/RoutePoints/Details',
    //    type: 'GET',
    //    dataType: 'json',
    //    success: function (response) {
    //        // Create an array of options from the response data
    //        var options = response.items.map(function (item) {
    //            return {
    //                id: item.id,
    //                title: item.title,
    //                address: item.address,
    //                latitude: item.latitude,
    //                longitude: item.longitude
    //            };
    //        });

    //        // Initialize the Selectize plugin with the new options array
    //        $('#from').selectize({
    //            persist: false,
    //            placeholder: '',
    //            maxItems: 1,
    //            valueField: 'id',
    //            labelField: 'title',
    //            searchField: ['title', 'id'],
    //            placeholder: 'Саратов',
    //            options: options,
    //            render: {
    //                item: function (item, escape) {
    //                    return (
    //                        '<div>' +
    //                        (item.title ?
    //                            '<span class="town" id="' + item.id + '">' + escape(item.title) + '</span>' :
    //                            '') +
    //                        '</div>'
    //                    );
    //                },
    //                option: function (item, escape) {
    //                    var label = item.title || item.id;
    //                    return (
    //                        '<div>' +
    //                        '<span class="label">' +
    //                        escape(label) +
    //                        '</span>' +
    //                        '</div>'
    //                    );
    //                }
    //            }
    //        });
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {
    //        console.error('Error fetching data:', errorThrown);
    //    }
    //});

    // Initialize the Selectize plugin

    var selectizeBody = {
        persist: false,
        placeholder: '',
        maxItems: 1,
        valueField: "id",
        labelField: "title",
        searchField: ["title", "id"],
        placeholder: 'Саратов',
        load: function (query, callback) {
            // Fetch the data via AJAX
            $.ajax({
                url: '/RoutePoints/Details',
                type: 'GET',
                dataType: 'json',
                data: {
                    searchQuery: query
                },
                success: function (response) {
                    // Map the response data to an array of options
                    var options = response.items.map(function (item) {
                        return {
                            id: item.id,
                            title: item.title,
                            address: item.address,
                            latitude: item.latitude,
                            longitude: item.longitude
                        };
                    });

                    // Pass the options to the callback function
                    callback(options);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data:', errorThrown);
                }
            });
        },
        render: {
            item: function (item, escape) {
                return (
                    '<div>' +
                    (item.title ?
                        '<span class="town" id="' + item.id + '">' + escape(item.title) + '</span>' :
                        '') +
                    '</div>'
                );
            },
            option: function (item, escape) {
                var label = item.title || item.id;
                return (
                    '<div>' +
                    '<span class="label">' +
                    escape(label) +
                    '</span>' +
                    '</div>'
                );
            }
        },
        onChange: function (value) {
            // Set the value of another element to the selected value
            var selectedOption = this.options[value];
            if (selectedOption) {
                var selectedTitle = selectedOption.title;
                $('#otherElement').val(selectedTitle);
            }
        }
    }

    // function initializeSelectize(selectizeBody, onChangeElementId, targetId) {
    //     // Initialize the Selectize plugin
    //     var choiceselect = $('#' + targetId).selectize(selectizeBody);
    //
    //     // Set the value of another element to the selected value
    //     choiceselect[0].selectize.on('change', function (value) {
    //         var selectedOption = this.options[value];
    //         if (selectedOption) {
    //             var selectedTitle = selectedOption.id;
    //             $('#' + onChangeElementId).val(selectedTitle);
    //         }
    //     });
    // }

    function formatPhoneNumber(phoneNumber) {
        const countryCode = phoneNumber.slice(0, 2);
        const firstThree = phoneNumber.slice(2, 5);
        const lastTwo = phoneNumber.slice(-2);
        const middle = phoneNumber.slice(5, -2).replace(/\d/g, '*');

        return `${countryCode} ${firstThree} ${middle} ${lastTwo}`;
    }

    $('#form-filter').on('submit', function (e) {
        if ($('#trips-get-api-mode').val() == 'true') {
            e.preventDefault();

            // Get form data
            var formData = $('#form-filter').serializeArray();

            // Convert form data to object
            var filterModel = {};
            for (var i = 0; i < formData.length; i++) {
                var name = formData[i].name;
                var value = formData[i].value;
                if (name === 'DepartureTime') {
                    value = new Date(value);
                }
                if (name.startsWith('Filter.')) {
                    var propertyName = name.substring('Filter.'.length);
                    filterModel[propertyName] = value;
                } else {
                    filterModel[name] = value;
                }
            }

            // Serialize object to JSON string
            var jsonData = JSON.stringify(filterModel);

            $.ajax({
                url: "/Trips/UpdateList",
                contentType: 'application/json',
                dataType: 'json',
                method: 'POST',
                data: jsonData,
                success: function (result) {
                    // Clear existing trip data from list
                    $('#trip-choice-list').empty();

                    // Loop through each trip object and generate HTML for it

                    result.objects.items.forEach(function (trip) {
                        moment.locale('ru');
                        var tripHtml = `
                        <div class="tripChoice__list-item tripChoice__list-item">
                            <div class="tripChoice__wrapper">
                                <div class="tripChoice__departure">
                                    <div class="tripChoice__top">
                                        <div class="tripChoice__date">${moment(trip.departureTime).format('DD MMMM')}</div>
                                        <div class="tripChoice__time">${moment(trip.departureTime).format('HH:mm')}</div>
                                    </div>
                                    <div class="tripChoice__place">${trip.departureRoutePoint.title}</div>
                                </div>
                                <div class="tripChoice__arrival">
                                    <div class="tripChoice__top">
                                        <div class="tripChoice__date">${moment(trip.arrivalTime).format('DD MMMM')}</div>
                                        <div class="tripChoice__time">${moment(trip.arrivalTime).format('HH:mm')}</div>
                                    </div>
                                    <div class="tripChoice__place">${trip.arrivalRoutePoint.title}</div>
                                </div>
                                <div class="tripChoice__options">
                                    <div class="tripChoice__options-left">
                                        <div class="tripChoice__price">Цена</div>
                                        <div class="tripChoice__priceValue">${trip.price} ₽</div>
                                    </div>
                                    <div class="tripChoice__options-right">
                                        <div class="tripChoice__travelTime">Время в пути</div>
                                        <div class="tripChoice__timeValue">${trip.travelHours} ч ${trip.travelMinutes} мин</div>
                                    </div>
                                </div>
                                <div class="tripChoice__confirm tripChoice__confirm">

                                    <div onclick="location.href='/trips/details?id=${trip.id}';" class="tripChoice__btn">Купить</div>
                                    <div class="tripChoice__enabled tripChoice__enabled--myTrip">Доступно ${trip.freeSeats} мест</div>
                                </div>
                            </div>
                        </div>
                    `;

                        // Append trip HTML to list container
                        $('#trip-choice-list').append(tripHtml);
                    });
                }
            });
        }
    });


    function initializeSelectize(selectizeBody, onChangeElementId, targetId, selectedId = null, selectedTitle = null) {
        // Initialize the Selectize plugin
        var choiceselect = $('#' + targetId).selectize(selectizeBody);

        // Preselect an option based on the provided id and title
        if (selectedId && selectedTitle) {
            choiceselect[0].selectize.addOption({ id: selectedId, title: selectedTitle });
            choiceselect[0].selectize.setValue(selectedId);
        }

        // Set the value of another element to the selected value
        choiceselect[0].selectize.on('change', function (value) {
            var selectedOption = this.options[value];
            if (selectedOption) {
                var selectedId = selectedOption.id;
                $('#' + onChangeElementId).val(selectedId);
            }
        });
    }

    //localStorage.setItem('filter', JSON.stringify(filter));



    let date = new Date();

    var filter = JSON.parse(localStorage.getItem('filter') ?? "");
    var choiceselectFrom = initializeSelectize(selectizeBody, 'fromRoutePointId', 'from', filter?.From?.Id ?? null, filter?.From?.Title ?? null);
    var choiceselectTo = initializeSelectize(selectizeBody, 'toRoutePointId', 'to', filter?.To?.Id ?? null, filter?.To?.Title ?? null);

    date = new Date(filter.Departure);



    let tomorrow = date;
    tomorrow.setDate(tomorrow.getDate() + 0)
    tomorrow = tomorrow.toLocaleDateString('en-CA').split('/').reverse().join('-')
    $(data).val(tomorrow)

    localStorage.removeItem('filter');

    // Refresh the options on every key press
    $('#from-selectized').on('keyup', function () {
        console.log('key pressed');
        var query = $(this).val();
        if (query.length >= 3) {
            choiceselectFrom[0].selectize.load();
        }
    });

    $('#from-selectized').on('keyup', function () {
        console.log('key pressed');
        var query = $(this).val();
        if (query.length >= 3) {
            choiceselectTo[0].selectize.load();
        }
    });
})