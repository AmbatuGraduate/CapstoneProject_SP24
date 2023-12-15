document.addEventListener('DOMContentLoaded', async () => {
    try {
        const response = await fetch('https://localhost:7024/api/Tree', {
            mode: 'cors' // Include this option to enable CORS
        });
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();
        console.log(data);
        renderTreesList(data);
    } catch (error) {
        console.error('Error fetching tree data:', error);
    }
});

function renderTreesList(trees) {
    $.each(trees, function (index, tree) {
        // Create a new tree row
        var treeRow = $('<div class="trees-row"></div>');

        // Append button and function cell
        var functionCell = $('<div class="tree-cell function"></div>');
        functionCell.append('<span class="cell-label">Chỉnh sửa:</span>');
        functionCell.append('<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editTreeModal">Sửa</button>');
        functionCell.append('<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Xóa</button>');
        treeRow.append(functionCell);

        // Define the properties you want to display and their corresponding labels
        var properties = {
            'Mã số cây': 'id',
            'Quận': 'district',
            'Tuyến đường': 'street',
            'Loại cây': 'rootType',
            'Giống cây': 'type',
            'Đường kính thân': 'bodyDiameter',
            'Tán lá': 'leafLength',
            'Thời điểm trồng': 'plantTime',
            'Thời điểm cắt': 'cutTime',
            'Thời hạn cắt': 'idk',
            'Ghi chú': 'note'
        };

        // Loop through the properties and create corresponding div elements
        $.each(properties, function (label, property) {
            // Create a div for each property
            var propertyDiv = $('<div class="tree-cell ' + property.toLowerCase() + '"><span class="cell-label">' + label + ':</span>' + tree[property] + '</div>');

            // Append the property div to the tree row
            treeRow.append(propertyDiv);
        });

        // Append the tree row to the container
        $('#table-footer').prepend(treeRow);
    });

}