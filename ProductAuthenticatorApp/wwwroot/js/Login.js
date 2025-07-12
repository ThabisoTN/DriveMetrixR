// Enhanced form interactions
document.querySelectorAll('.form-control').forEach(input => {
    input.addEventListener('focus', function () {
        this.style.borderColor = 'var(--primary-color)';
        this.style.boxShadow = '0 0 0 0.25rem rgba(67, 97, 238, 0.25)';
    });

    input.addEventListener('blur', function () {
        this.style.boxShadow = 'none';
        if (!this.value) {
            this.style.borderColor = 'var(--light-gray)';
        }
    });
});

// Button loading state
document.getElementById('account').addEventListener('submit', function () {
    const button = document.getElementById('login-submit');
    const buttonText = document.getElementById('button-text');

    button.disabled = true;
    buttonText.innerHTML = 'Signing in <span class="spinner">🌀</span>';
});

// Show validation errors with more emphasis
document.addEventListener('DOMContentLoaded', function () {
    const validationErrors = document.querySelectorAll('.text-danger');
    validationErrors.forEach(error => {
        if (error.textContent.trim() !== '') {
            const input = error.closest('.form-floating').querySelector('.form-control');
            input.style.borderColor = '#dc3545';
        }
    });
});