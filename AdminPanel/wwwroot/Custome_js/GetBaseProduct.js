

function GetPrice(id) {
    debugger;
    $.ajax({
        //url: '@Url.Action("PriceUpdate", "Product")',
        url: '/Product/PriceUpdate',
        type: 'GET',
        data: { Base_Product_Id: id },
        success: function (response) {
            debugger;
            $('#ShowModel').html(response);
            var modal = new bootstrap.Modal(
                document.getElementById('exampleModal')
            );
            modal.show();
        },
        error: function (xhr) {

        }
    });
}

function GetDiscount(id) {
    debugger;
    $.ajax({
        url: '/Product/DiscountPrice/',
        type: 'GET',
        data: { Base_Product_Id: id },
        success: function (response) {
            $('#ShowModelA').html(response);
            var modal = new bootstrap.Modal(
                document.getElementById('exampleModalA')
            );
            modal.show();
        }, error: function (xhr) {
        }
    });
}


//$("#txtSearch").keyup(function () {
//    debugger;
//    var text = $(this).val();

//    if (text.length == 0) {
//        $("#suggestionBox").hide();
//        return;
//    }

//    $.ajax({
//        url: '/Product/SearchSuggestion',
//        type: 'GET',
//        data: { search: text },

//        success: function (data) {

//            var html = "";

//            $.each(data, function (i, item) {

//                html += `<a href="#" class="list-group-item suggestion"
//                         data-id="${item.id}">
//                         ${item.name}
//                         </a>`;
//            });

//            $("#suggestionBox").html(html).show();
//        }
//    });

//});



$("#searchBox").keyup(function () {

    var search = $(this).val();
    //var search = $(this).text();

    if (search.length < 2) {

        $.ajax({
            url: '/Product/SearchBaseProduct',
            type: 'GET',
            success: function (response) {
                $("#productTableBody").empty();
                var srNo = 1;
                $.each(response, function (index, item) {

                    var row = `
                    <tr>
                        <td>${srNo++}</td>
                        <td>${item.name}</td>
                        <td>${item.mainCategory.name}</td>
                        <td>${item.material.name}</td>

                        <td>
                            <a class="btn"
                               onclick="GetPrice(${item.id})"
                               data-bs-toggle="modal"
                               data-bs-target="#exampleModal">
                                ${item.price}
                            </a>
                        </td>

                        <td>
                            <a class="btn"
                               onclick="GetDiscount(${item.id})"
                               data-bs-toggle="modal"
                               data-bs-target="#exampleModalA">
                                ${item.discount}
                            </a>
                        </td>

                        <td>${item.totalPrice}</td>

                        <td>
                            <div class="d-flex gap-2">

                                <a href="/Product/GetAllProduct?Base_Product_Id=${item.id}"
                                   class="btn btn-sm btn-outline-primary rounded-pill px-3">
                                   <i class="ri-pencil-fill me-1"></i>Edit
                                </a>

                                <a href="/Product/DeleteProductMaterial?Id=${item.id}"
                                   class="btn btn-sm btn-outline-danger rounded-pill px-3">
                                   <i class="ri-delete-bin-fill me-1"></i>Delete
                                </a>

                            </div>
                        </td>

                    </tr>`;

                    $("#productTableBody").append(row);
                });

            },

        })


        $("#suggestions").hide();

        return;
    }

    $.ajax({
        url: '/Product/SugestionBaseProduct',
        type: 'GET',
        data: { Product_Name: search },

        success: function (response) {
            debugger;
            var html = response;

            $("#suggestions").empty();

            for (var i = 0; i < response.length; i++) {
                $("#suggestions").append(
                    "<li  class='list-group-item suggestion-item'>" + response[i] + "</li>"
                );
            }

            $("#suggestions").show();
        }
    });

});


$(document).on("click", ".suggestion-item", function () {

    // Set the clicked value in the search textbox
    $("#searchBox").val($(this).text());

    // Hide suggestions
    $("#suggestions").hide();
});

$(document).on("click", "#SearchButton", function () {

    var product = $("#searchBox").val();

    $.ajax({
        url: '/Product/SearchBaseProduct',
        type: 'GET',
        data: {
            Product_Type: product
        },
        success: function (response) {

            $("#productTableBody").empty();

            var srNo = 1;

            $.each(response, function (index, item) {

                var row = `
                    <tr>
                        <td>${srNo++}</td>
                        <td>${item.name}</td>
                        <td>${item.mainCategory.name}</td>
                        <td>${item.material.name}</td>

                        <td>
                            <a class="btn"
                               onclick="GetPrice(${item.id})"
                               data-bs-toggle="modal"
                               data-bs-target="#exampleModal">
                                ${item.price}
                            </a>
                        </td>

                        <td>
                            <a class="btn"
                               onclick="GetDiscount(${item.id})"
                               data-bs-toggle="modal"
                               data-bs-target="#exampleModalA">
                                ${item.discount}
                            </a>
                        </td>

                        <td>${item.totalPrice}</td>

                        <td>
                            <div class="d-flex gap-2">

                                <a href="/Product/GetAllProduct?Base_Product_Id=${item.id}"
                                   class="btn btn-sm btn-outline-primary rounded-pill px-3">
                                   <i class="ri-pencil-fill me-1"></i>Edit
                                </a>

                                <a href="/Product/DeleteProductMaterial?Id=${item.id}"
                                   class="btn btn-sm btn-outline-danger rounded-pill px-3">
                                   <i class="ri-delete-bin-fill me-1"></i>Delete
                                </a>

                            </div>
                        </td>

                    </tr>`;

                $("#productTableBody").append(row);
            });

        },
        error: function () {
            alert("Error while searching.");
        }
    });

});