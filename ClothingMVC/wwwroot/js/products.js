// Opens the Modal
function openCrudModal(url) {
    console.log("Attempting to open modal for URL:", url); // Debugging line

    $.get(url)
        .done(function (data) {
            $('#crudModalBody').html(data);
            $('#crudModal').modal('show');
        })
        .fail(function (err) {
            console.error("Modal failed to load:", err);
            alert("Error loading content. Check if the Controller action exists.");
        });
}

// Filters the product grid
function filterProducts() {
    const searchTerm = $('#productSearch').val().toLowerCase();
    const selectedBrand = $('#brandFilter').val();
    let visibleCount = 0;

    $('.product-item').each(function () {
        const name = $(this).data('name') || "";
        const brand = $(this).data('brand') || "";

        const matchesSearch = name.includes(searchTerm);
        const matchesBrand = selectedBrand === "" || brand === selectedBrand;

        if (matchesSearch && matchesBrand) {
            $(this).show(); // Use show() for faster filtering
            visibleCount++;
        } else {
            $(this).hide();
        }
    });

    if (visibleCount === 0) {
        $('#noResults').removeClass('d-none');
    } else {
        $('#noResults').addClass('d-none');
    }
}

$(document).ready(function () {
    $('#productSearch').on('input', filterProducts);
    $('#brandFilter').on('change', filterProducts);
});