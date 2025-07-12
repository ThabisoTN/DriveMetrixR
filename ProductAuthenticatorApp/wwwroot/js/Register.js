// Toggle business fields
const toggleSwitch = document.getElementById('isBusinessClient');
const businessFields = document.getElementById('businessFields');

toggleSwitch.addEventListener('change', function () {
    if (this.checked) {
        businessFields.style.display = 'block';
    } else {
        businessFields.style.display = 'none';
    }
});

// Initialize on page load in case of validation errors
if (toggleSwitch.checked) {
    businessFields.style.display = 'block';
}

// Password strength indicator
function checkPasswordStrength(password) {
    const strengthText = document.getElementById('strength-text');
    const strengthMeter = document.getElementById('strength-meter');

    // Reset
    let strength = 0;
    strengthMeter.style.width = '0%';
    strengthMeter.style.backgroundColor = '#dc3545';
    strengthText.textContent = 'Weak';
    strengthText.style.color = '#dc3545';

    if (!password) return;

    // Length check
    if (password.length > 7) strength += 1;
    if (password.length > 11) strength += 1;

    // Complexity checks
    if (/[A-Z]/.test(password)) strength += 1;
    if (/[0-9]/.test(password)) strength += 1;
    if (/[^A-Za-z0-9]/.test(password)) strength += 1;

    // Update UI
    if (strength > 4) {
        strengthMeter.style.width = '100%';
        strengthMeter.style.backgroundColor = '#28a745';
        strengthText.textContent = 'Strong';
        strengthText.style.color = '#28a745';
    } else if (strength > 2) {
        strengthMeter.style.width = '66%';
        strengthMeter.style.backgroundColor = '#f59e0b';
        strengthText.textContent = 'Medium';
        strengthText.style.color = '#f59e0b';
    } else {
        strengthMeter.style.width = '33%';
        strengthMeter.style.backgroundColor = '#dc3545';
        strengthText.textContent = 'Weak';
        strengthText.style.color = '#dc3545';
    }
}

// Form submission feedback
document.getElementById('registerForm').addEventListener('submit', function () {
    const btn = document.getElementById('registerSubmit');
    btn.disabled = true;
    btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Creating Account...';
});