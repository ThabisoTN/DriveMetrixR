<script>
        // Make the Add Product button functional
    document.getElementById('addProductBtn').addEventListener('click', function() {
        // In a real app, this would redirect to the add product page
        console.log('Redirecting to add product page...');
    window.location.href = '@Url.Action("AddProduct", "User")';
        });

    // Add hover effects to table rows
    const tableRows = document.querySelectorAll('.scan-table-row');
        tableRows.forEach(row => {
        row.addEventListener('mouseenter', function () {
            this.style.transform = 'translateX(5px)';
        });
    row.addEventListener('mouseleave', function() {
        this.style.transform = 'translateX(0)';
            });
        });

    // Responsive sidebar toggle for mobile
    function toggleSidebar() {
            const sidebar = document.querySelector('.sidebar');
    sidebar.classList.toggle('collapsed');
        }

    // Add this if you want a menu toggle button for mobile
    // You'll need to add the button to your HTML