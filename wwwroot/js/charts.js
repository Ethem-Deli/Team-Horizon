// wwwroot/js/charts.js
window.budgetCharts = (function () {
  const chartInstances = new Map();

  function destroyIfExists(canvasId) {
    const existing = chartInstances.get(canvasId);
    if (existing) {
      existing.destroy();
      chartInstances.delete(canvasId);
    }
  }

  function renderPie(canvasId, labels, values) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;

    destroyIfExists(canvasId);

    const ctx = canvas.getContext("2d");
    const chart = new Chart(ctx, {
      type: "pie",
      data: {
        labels: labels,
        datasets: [{
          data: values
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: "bottom" },
          tooltip: {
            callbacks: {
              label: (ctx) => {
                const val = ctx.raw ?? 0;
                const label = ctx.label ?? "";
                return `${label}: ${val.toFixed(2)}`;
              }
            }
          }
        }
      }
    });

    chartInstances.set(canvasId, chart);
  }

  function renderBar(canvasId, labels, values) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;

    destroyIfExists(canvasId);

    const ctx = canvas.getContext("2d");
    const chart = new Chart(ctx, {
      type: "bar",
      data: {
        labels: labels,
        datasets: [{
          label: "Amount",
          data: values
        }]
      },
      options: {
        responsive: true,
        scales: {
          y: { beginAtZero: true }
        },
        plugins: {
          legend: { display: false },
          tooltip: {
            callbacks: {
              label: (ctx) => `${(ctx.raw ?? 0).toFixed(2)}`
            }
          }
        }
      }
    });

    chartInstances.set(canvasId, chart);
  }

  return {
    renderPie,
    renderBar
  };
})();
