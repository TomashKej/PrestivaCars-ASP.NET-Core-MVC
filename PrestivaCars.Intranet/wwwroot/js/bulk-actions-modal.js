/**
 * Bulk Actions Modal Script
 * Handles select all, bulk delete and bulk restore confirmation modals.
 */

document.addEventListener("DOMContentLoaded", function () {
    const selectAllCheckbox = document.getElementById("selectAllCheckbox");
    const bulkDeleteButton = document.getElementById("bulkDeleteButton");
    const bulkRestoreButton = document.getElementById("bulkRestoreButton");

    const modalElement = document.getElementById("bulkActionModal");
    const submitButton = document.getElementById("bulkActionSubmitButton");

    if (!modalElement || !submitButton) {
        return;
    }

    function getCheckedBoxes() {
        return document.querySelectorAll(".bulk-checkbox:checked");
    }

    function getActiveSelectedCount() {
        let count = 0;

        getCheckedBoxes().forEach(function (checkbox) {
            if (checkbox.dataset.active === "true") {
                count++;
            }
        });

        return count;
    }

    function getInactiveSelectedCount() {
        let count = 0;

        getCheckedBoxes().forEach(function (checkbox) {
            if (checkbox.dataset.active === "false") {
                count++;
            }
        });

        return count;
    }

    function updateBulkButtons() {
        const activeCount = getActiveSelectedCount();
        const inactiveCount = getInactiveSelectedCount();

        if (bulkDeleteButton) {
            bulkDeleteButton.disabled = activeCount === 0;
        }

        if (bulkRestoreButton) {
            bulkRestoreButton.disabled = inactiveCount === 0;
        }
    }

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener("change", function () {
            document.querySelectorAll(".bulk-checkbox").forEach(function (checkbox) {
                checkbox.checked = selectAllCheckbox.checked;
            });

            updateBulkButtons();
        });
    }

    document.querySelectorAll(".bulk-checkbox").forEach(function (checkbox) {
        checkbox.addEventListener("change", function () {
            const allCheckboxes = document.querySelectorAll(".bulk-checkbox");
            const checkedCheckboxes = document.querySelectorAll(".bulk-checkbox:checked");

            if (selectAllCheckbox) {
                selectAllCheckbox.checked =
                    allCheckboxes.length > 0 &&
                    allCheckboxes.length === checkedCheckboxes.length;
            }

            updateBulkButtons();
        });
    });

    document.querySelectorAll("[data-bulk-action]").forEach(function (button) {
        button.addEventListener("click", function () {
            const actionUrl = this.dataset.bulkAction;
            const actionType = this.dataset.bulkType;
            const entityName = this.dataset.entityName || "record";

            const titleElement = document.getElementById("bulkActionModalTitle");
            const messageElement = document.getElementById("bulkActionModalMessage");
            const countElement = document.getElementById("bulkActionModalCount");
            const infoElement = document.getElementById("bulkActionModalInfo");
            const noteElement = document.getElementById("bulkActionModalNote");

            submitButton.setAttribute("formaction", actionUrl);
            submitButton.classList.remove("btn-danger", "btn-success");

            if (actionType === "delete") {
                const activeCount = getActiveSelectedCount();

                titleElement.innerText = "Confirm bulk delete";
                messageElement.innerText = `Are you sure you want to delete selected active ${entityName}s?`;
                countElement.innerText = `${activeCount} active ${entityName}(s) selected`;
                infoElement.innerText = "Only active selected records will be marked as inactive.";
                noteElement.innerText = "This action uses soft delete. Records will remain in the database.";
                submitButton.innerText = "Delete selected";
                submitButton.classList.add("btn-danger");
            }

            if (actionType === "restore") {
                const inactiveCount = getInactiveSelectedCount();

                titleElement.innerText = "Confirm bulk restore";
                messageElement.innerText = `Are you sure you want to restore selected inactive ${entityName}s?`;
                countElement.innerText = `${inactiveCount} inactive ${entityName}(s) selected`;
                infoElement.innerText = "Only inactive selected records will be restored.";
                noteElement.innerText = "Restored records will become active again.";
                submitButton.innerText = "Restore selected";
                submitButton.classList.add("btn-success");
            }

            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        });
    });

    updateBulkButtons();
});