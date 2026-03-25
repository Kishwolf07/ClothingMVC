$(document).ready(function () {
    const $passwordInput = $('#passwordInput');
    const $guide = $('#passwordGuide');

    // Show guide when user starts typing or focuses
    $passwordInput.on('focus keyup', function () {
        $guide.removeClass('d-none');
        const val = $(this).val();

        // Check each requirement against regex
        updateRequirement($('#lengthReq'), val.length >= 6);
        updateRequirement($('#upperReq'), /[A-Z]/.test(val));
        updateRequirement($('#lowerReq'), /[a-z]/.test(val));
        updateRequirement($('#specialReq'), /[^A-Za-z0-9]/.test(val));
    });

    function updateRequirement($el, isValid) {
        if (isValid) {
            $el.removeClass('text-danger').addClass('text-success');
            $el.find('i').removeClass('bi-x-circle').addClass('bi-check-circle');
        } else {
            $el.removeClass('text-success').addClass('text-danger');
            $el.find('i').removeClass('bi-check-circle').addClass('bi-x-circle');
        }
    }
});