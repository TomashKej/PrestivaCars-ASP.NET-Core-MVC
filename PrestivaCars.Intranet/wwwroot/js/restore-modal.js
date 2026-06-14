/**
 * Restore Modal Script
 * If the restore button is clicked, it fetches the restore confirmation modal content via AJAX and displays it in a Bootstrap modal.
 */

document.addEventListener("DOMContentLoaded", function () {
    const restoreButtons = document.querySelectorAll(".js-restore-btn");
    const modalElement = document.getElementById("restoreModal");
    const modalContent = document.getElementById("restoreModalContent");

    if (!restoreButtons.length || !modalElement || !modalContent) {
        return;
    }

    restoreButtons.forEach(button => {
        button.addEventListener("click", async function () {
            const restoreUrl = this.getAttribute("data-restore-url");

            if (!restoreUrl) {
                console.error("Missing data-restore-url attribute on restore button.");
                return;
            }

            try {
                const response = await fetch(restoreUrl);

                if (!response.ok) {
                    throw new Error(`HTTP error: ${response.status}`);
                }

                const html = await response.text();

                modalContent.innerHTML = html;

                const modal = new bootstrap.Modal(modalElement);
                modal.show();
            }
            catch (error) {
                console.error("Failed to load restore modal:", error);
            }
        });
    });
});