// Make the scanner interactive
const scanner = document.querySelector('.scanner-animation');
const scannedBarcode = document.querySelector('.scanned-barcode');
let isScanning = false;

scanner.addEventListener('click', function () {
    if (isScanning) return;

    isScanning = true;

    // Show scanning state
    this.style.borderColor = 'var(--success-color)';
    this.style.boxShadow = '0 0 15px rgba(67,97,238,0.5)';

    // Show the barcode after a delay (simulating scan time)
    setTimeout(() => {
        scannedBarcode.style.display = 'flex';

        // Show success state
        setTimeout(() => {
            this.style.borderColor = 'var(--success-color)';
            this.style.boxShadow = '0 0 0 3px rgba(40,167,69,0.3)';

            // Show success message
            const message = document.createElement('div');
            message.textContent = 'Product verified successfully!';
            message.style.color = 'var(--success-color)';
            message.style.fontWeight = '500';
            message.style.marginTop = '1rem';

            const scannerBody = document.querySelector('.scanner-body');
            const oldMessage = scannerBody.querySelector('.success-message');
            if (oldMessage) oldMessage.remove();

            message.classList.add('success-message');
            scannerBody.appendChild(message);

            // Reset after 3 seconds
            setTimeout(() => {
                scannedBarcode.style.display = 'none';
                this.style.borderColor = 'var(--primary-color)';
                this.style.boxShadow = 'none';
                isScanning = false;
            }, 3000);
        }, 1000);
    }, 1500);
});

// Add subtle animation to barcode
const barcode = document.querySelector('.barcode-display');
setInterval(() => {
    barcode.style.transform = `rotate(${Math.random() * 2 - 1}deg)`;
}, 3000);