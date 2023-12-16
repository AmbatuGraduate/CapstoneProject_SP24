function saveTree() {
    // Collect data from the form
    var treeData = {
        id: Math.floor(Math.random() * 10000) + 1, // Generating a random id
        district: $('#add_tree_district').val(),
        street: $('#add_tree_street').val(),
        rootType: $('#add_tree_rootType').val(),
        type: $('#add_tree_type').val(),
        bodyDiameter: parseFloat($('#add_tree_diameter').val()),
        leafLength: parseFloat($('#add_tree_leaf').val()),
        plantTime: new Date($('#add_tree_plant').val()).toISOString(),
        cutTime: new Date($('#add_tree_cut').val()).toISOString(),
        intervalCutTime: parseInt($('#add_tree_interval').val()),
        note: $('#add_tree_note').val()
    };

    // Make a POST request
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7024/api/Tree',
        contentType: 'application/json',
        data: JSON.stringify(treeData),
        success: function (response) {
            console.log('Tree saved successfully:', response);
            // Handle success, e.g., show a success message or redirect
        },
        error: function (xhr) {
            console.error('Error saving tree:', xhr);

            // Check if the response contains JSON data
            if (xhr.responseJSON && xhr.responseJSON.errors) {
                // Log the validation errors
                console.error('Validation errors:', xhr.responseJSON.errors);
            }
        }
    });
    // You may want to close the modal after saving
    $('#addTreeModal').modal('hide');
}
