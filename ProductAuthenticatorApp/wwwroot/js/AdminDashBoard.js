// Vehicle Distribution Chart
const ctx = document.getElementById('vehicleChart').getContext('2d');
const vehicleChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: @Html.Raw(JsonConvert.SerializeObject(((IEnumerable < dynamic >)ViewData["VehicleDistribution"]).Select(v => v.Make))),
    datasets: [{
        data: @Html.Raw(JsonConvert.SerializeObject(((IEnumerable < dynamic >)ViewData["VehicleDistribution"]).Select(v => v.Count))),
backgroundColor: [
    '#3b82f6', '#ef4444', '#f59e0b', '#10b981', '#8b5cf6',
    '#ec4899', '#14b8a6', '#f97316', '#64748b', '#a855f7'
],
    borderWidth: 0
                }]
            },
options: {
    responsive: true,
        maintainAspectRatio: false,
            plugins: {
        legend: {
            position: 'right'
        }
    }
}
        });