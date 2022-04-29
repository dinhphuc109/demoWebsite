var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/Trade/Index";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/Searchorder";
        });
        

        $('#btnUpdate').off('click').on('click', function () {
            var listproduct = $('.txtquantity');
            var cartlist = [];
            $.each(listproduct, function (i, item) {
                cartlist.push({
                    Quantity: $(this).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartlist) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/do-dung";
                    }
                }
            })
        });

        $('#btnDeleteAll').off('click').on('click', function () {


            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/do-dung";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/do-dung";
                    }
                }
            })
        });

        $('#btnGive').off('click').on('click', function () {
            window.location.href = "/give";
        });
    }
}
cart.init();