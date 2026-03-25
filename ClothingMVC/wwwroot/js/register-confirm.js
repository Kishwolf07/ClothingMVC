window.showRegistrationAlert = function (email) {
    Swal.fire({
        title: 'Registration Successful',
        text: `We've sent a confirmation link to ${email}. Please verify your account before signing in.`,
        icon: 'info',
        confirmButtonColor: '#212529',
        customClass: {
            popup: 'rounded-4 border-0 shadow'
        }
    });
};