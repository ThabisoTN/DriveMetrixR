// First fetch the current user ID when page loads
async function getCurrentUserId() {
    try {
        console.log('Fetching current user ID...');
        const response = await fetch('/api/Product/GetUserId', {
            credentials: 'include' // Important for authentication cookies
        });

        if (!response.ok) {
            throw new Error('Failed to get user ID');
        }

        const userId = await response.text();
        console.log('Retrieved user ID:', userId);

        if (userId) {
            document.getElementById('UserId').value = userId;
            return userId;
        }

        throw new Error('User ID not found');
    } catch (error) {
        console.error('Error getting user ID:', error);
        showErrorMessage('Please login to add products');
        document.getElementById('submitBtn').disabled = true;
        return null;
    }
}

// Modified form submission handler
document.getElementById('addProductForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const submitBtn = document.getElementById('submitBtn');
    submitBtn.disabled = true;
    submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Adding Product...';

    const statusMessage = document.getElementById('statusMessage');
    statusMessage.style.display = 'none';
    document.querySelectorAll('.text-danger').forEach(el => el.textContent = '');

    // First ensure we have a user ID
    let userId = document.getElementById('UserId').value;
    if (!userId) {
        userId = await getCurrentUserId();
        if (!userId) {
            submitBtn.disabled = false;
            submitBtn.innerHTML = '<i class="fas fa-plus"></i> Add Product';
            return;
        }
    }

    // Prepare product data including userId
    const product = {
        ProductName: document.getElementById('ProductName').value.trim(),
        ProductDescription: document.getElementById('ProductDescription').value.trim(),
        SerialNumber: document.getElementById('SerialNumber').value.trim(),
        UserId: userId
    };

    console.log('Submitting product with userId:', product);

    try {
        const response = await fetch('/api/Product/AddProduct', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            credentials: 'include', // Important for authentication
            body: JSON.stringify(product)
        });

        console.log('Response status:', response.status);

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Failed to add product');
        }

        const responseData = await response.json();
        console.log('Product added successfully:', responseData);

        // Show success message
        statusMessage.className = 'status-message status-success';
        statusMessage.innerHTML = `
                    <i class="fas fa-check-circle"></i>
                    Product added successfully!
                    <div style="margin-top: 10px;">
                        <strong>Name:</strong> ${responseData.productName}<br>
                        <strong>Serial:</strong> ${responseData.serialNumber}
                    </div>
                    <a href="/User/Products" class="btn-view-products"
                       style="display: inline-block; margin-top: 0.5rem; color: var(--success-color); font-weight: 500;">
                        View Your Products
                    </a>
                `;
        statusMessage.style.display = 'block';

        // Reset form
        document.getElementById('addProductForm').reset();

    } catch (error) {
        console.error('Error adding product:', error);

        statusMessage.className = 'status-message status-error';
        statusMessage.innerHTML = `
                    <i class="fas fa-exclamation-circle"></i>
                    ${error.message || 'Failed to add product'}
                    <div style="margin-top: 10px; font-size: 0.9em; color: var(--dark-color);">
                        Please check your information and try again.
                    </div>
                `;
        statusMessage.style.display = 'block';

        // Handle field-specific errors if available
        if (error.errors) {
            for (const [field, errors] of Object.entries(error.errors)) {
                const errorElement = document.getElementById(`${field}Error`);
                if (errorElement) {
                    errorElement.textContent = errors.join(', ');
                }
            }
        }
    } finally {
        submitBtn.disabled = false;
        submitBtn.innerHTML = '<i class="fas fa-plus"></i> Add Product';
    }
});

// Helper function to show error messages
function showErrorMessage(message) {
    const statusMessage = document.getElementById('statusMessage');
    statusMessage.className = 'status-message status-error';
    statusMessage.innerHTML = `<i class="fas fa-exclamation-circle"></i> ${message}`;
    statusMessage.style.display = 'block';
}

// Initialize by getting user ID when page loads
document.addEventListener('DOMContentLoaded', async () => {
    await getCurrentUserId();

    // Client-side validation
    document.querySelectorAll('input[required], textarea[required]').forEach(input => {
        input.addEventListener('blur', function () {
            if (!this.value.trim()) {
                this.classList.add('is-invalid');
            } else {
                this.classList.remove('is-invalid');
            }
        });
    });
});