function showLoginAlert() {
    Swal.fire({
        title: 'Login Required',
        html: 'You need to <strong>sign in</strong> to view vehicle details.<br><br>Don\'t have an account? <a href="/Identity/Account/Register">Register here</a>',
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#2563eb',
        cancelButtonColor: '#64748b',
        confirmButtonText: 'Sign In Now',
        cancelButtonText: 'Continue Browsing',
        focusConfirm: false,
        allowOutsideClick: false
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Identity/Account/Login?returnUrl=' + encodeURIComponent(window.location.pathname);
        }
    });
}