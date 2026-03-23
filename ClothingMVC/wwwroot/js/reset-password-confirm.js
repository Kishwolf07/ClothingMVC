window.showResetCompleteAlert = function () {
    Swal.fire({
        title: 'Password Updated',
        text: 'Your password has been reset successfully. You can now sign in with your new credentials.',
        icon: 'success',
        confirmButtonColor: '#212529',
        customClass: {
            popup: 'rounded-4 border-0 shadow'
        }
    });
};