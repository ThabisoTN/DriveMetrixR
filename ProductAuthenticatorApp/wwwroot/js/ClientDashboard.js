// Simple client-side search and filter functionality
document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const filterDropdown = document.getElementById('filterDropdown');
    const vehicleCards = document.querySelectorAll('.vehicle-card');

    function filterVehicles() {
        const searchTerm = searchInput.value.toLowerCase();
        const filterValue = filterDropdown.value.toLowerCase();

        vehicleCards.forEach(card => {
            const text = card.textContent.toLowerCase();
            const matchesSearch = text.includes(searchTerm);
            const matchesFilter = filterValue === '' || text.includes(filterValue);

            if (matchesSearch && matchesFilter) {
                card.style.display = 'block';
            } else {
                card.style.display = 'none';
            }
        });
    }

    searchInput.addEventListener('input', filterVehicles);
    filterDropdown.addEventListener('change', filterVehicles);
});