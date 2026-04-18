document.addEventListener("DOMContentLoaded", function () {
    const deleteButtons = document.querySelectorAll(".js-delete-btn");
    const modalElement = document.getElementById("deleteVehicleModal");
    const modalContent = document.getElementById("deleteVehicleModalContent");

    if (!deleteButtons.length || !modalElement || !modalContent) {
        return;
    }

    deleteButtons.forEach(button => {
        button.addEventListener("click", async function () {
            const vehicleId = this.getAttribute("data-id");

            if (!vehicleId) {
                return;
            }

            try {
                const response = await fetch(`/Vehicles/Delete/${vehicleId}`);

                if (!response.ok) {
                    throw new Error(`HTTP error: ${response.status}`);
                }

                const html = await response.text();
                modalContent.innerHTML = html;

                const modal = new bootstrap.Modal(modalElement);
                modal.show();
            }
            catch (error) {
                console.error("Failed to load delete modal:", error);
            }
        });
    });
});