$(document).ready(function () {
    // Add loading indicator when status changes
    $('.status-select').change(function () {
        $(this).attr('disabled', true);
        $(this).closest('tr').find('.badge').html('<i class="bi bi-hourglass"></i> Updating...');
    });
});