window.showResetSuccessAlert = function (email) {
    Swal.fire({
        title: 'Check Your Inbox',
        text: `If an account exists for ${email}, a reset link has been sent.`,
        icon: 'success',
        confirmButtonColor: '#212529',
        customClass: {
            popup: 'rounded-4 border-0 shadow'
        }
    });
};

$(document).ready(function () {
    const $form = $('#forgotPasswordForm');
    const $submitBtn = $('#submitBtn');
    const $btnText = $('#btnText');
    const $btnSpinner = $('#btnSpinner');

    $form.on('submit', function (e) {
        // 1. Check if the form is valid using jQuery Validation
        // This prevents the button from disabling if there's a typo in the email
        if ($(this).valid()) {
            $btnText.text('Sending Link...');
            $btnSpinner.removeClass('d-none');
            $submitBtn.prop('disabled', true);
        } else {
            // If invalid, ensure the button stays enabled so they can fix errors
            $submitBtn.prop('disabled', false);
        }
    });
});