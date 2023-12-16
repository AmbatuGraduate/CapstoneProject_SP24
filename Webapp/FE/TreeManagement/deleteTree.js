// Define the variable outside the functions

// function deleteTree(treeId) {
//     // Make a DELETE request
//     $.ajax({
//         type: 'DELETE',
//         url: 'https://localhost:7024/api/Tree/' + treeId,
//         success: function (response) {
//             console.log('Tree deleted successfully:', response);
//             // Optionally, you can remove the deleted tree from the UI
//             // Example: $('#treeRow_' + treeId).remove();
//         },
//         error: function (xhr) {
//             console.error('Error deleting tree:', xhr);
//         }
//     });
// }
var treeIdToDelete;
$(document).on('click', '.delete-btn', function () {
    // Retrieve the treeId from the data attribute
    console.log('tree' + $(this).data('treeid'));
    var treeId = $(this).data('treeid');

    // Set the treeIdToDelete variable
    treeIdToDelete = treeId;

    // Show the confirmation modal
    $('#deleteModal').modal('show');
});

function confirmDelete() {
    // Close the confirmation modal
    $('#deleteModal').modal('hide');

    // Make a DELETE request
    $.ajax({
        type: 'DELETE',
        url: 'https://localhost:7024/api/Tree?id=' + treeIdToDelete,
        success: function (response) {
            console.log('Tree deleted successfully:', response);

            // Optionally, you can remove the deleted tree from the UI
            // Example: $('#treeRow_' + treeIdToDelete).remove();
        },
        error: function (xhr) {
            console.error('Error deleting tree:', xhr);
        }
    });
}
