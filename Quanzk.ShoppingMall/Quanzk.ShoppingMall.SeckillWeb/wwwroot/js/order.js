
// 订单确认页面
$(function () {
    // 1、下单
    $(".btn-order").click(function () {
        // 判断是否登录
        if (!isHasLogin()) {
            return;
        }

        var ProductId = $("#ProductId").val();
        var ProductUrl = $("#ProductUrl").val();
        var ProductTitle = $("#ProductTitle").val();
        var ProductPrice = $("#ProductPrice").val();
        var ProductCount = $("#ProductCount").val();

        $.ajax({
            method: "POST",
            url: "https://localhost:5006/api/Order/",
            dataType: "json",
            data: {
                "ProductId": ProductId,
                "ProductUrl": ProductUrl,
                "ProductName": ProductTitle,
                "OrderTotalPrice": ProductPrice,
                "ProductCount": ProductCount
            },
            success: function (result) {
                if (result.ErrorNo == "0") {
                    // 1、跳转到支付页面
                    var resultDic = result.ResultDic;
                    location.href = "/Payment/Index?OrderId=" + resultDic.OrderId + "&OrderSn=" + resultDic.OrderSn + "&OrderTotalPrice=" + resultDic.OrderTotalPrice + "&UserId=" + resultDic.UserId + "&ProductId=" + resultDic.ProductId + "&ProductName=" + resultDic.ProductName +"";
                } else {
                    alert(result.ErrorInfo);
                }
            }
        })
    })
})

