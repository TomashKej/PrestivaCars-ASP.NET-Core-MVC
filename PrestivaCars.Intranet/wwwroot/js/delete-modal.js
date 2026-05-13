/**
 *  Delete Modal Script
 *  If the delete button is clicked, it fetches the delete confirmation modal content via AJAX and displays it in a Bootstrap modal.
 */

document.addEventListener("DOMContentLoaded", function () {
    const deleteButtons = document.querySelectorAll(".js-delete-btn");
    const modalElement = document.getElementById("deleteModal");
    const modalContent = document.getElementById("deleteModalContent");

    if (!deleteButtons.length || !modalElement || !modalContent) {
        return;
    }

    deleteButtons.forEach(button => {
        button.addEventListener("click", async function () {
            const deleteUrl = this.getAttribute("data-delete-url");

            if (!deleteUrl) {
                console.error("Missing data-delete-url attribute on delete button.");
                return;
            }

            try {
                const response = await fetch(deleteUrl);

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