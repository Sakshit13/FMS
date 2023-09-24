    $(document).ready(function () {
        $("#createForm").validate({ 
            errorClass: "text-danger",
            errorElement: "span", 
            highlight: function (element) {
                $(element).closest(".form-group").addClass("has-error");
            },
            unhighlight: function (element) {
                $(element).closest(".form-group").removeClass("has-error");
            }
        });

    $("#Grnnumber").rules("add", {
        required: true,
    messages: {
        required: "Please enter the GRN number."
            }
    });
        $("#Grnnumber").rules("add", {
            required: true,
            messages: {
                required: "Please enter the GRN number."
            }
        });
        $("#PartNumber").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Part number."
            }
        });
        $("#RecievedQty").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Received Quantity."
            }
        });
        $("#AcceptedQty").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Accepted Quantity."
            }
        });
        $("#RejectedQty").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Rejected Quantity."
            }
        });
        $("#UnitPrice").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Unit Price."
            }
        });
        $("#Tax").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Tax."
            }
        });
        $("#Discount").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Discount."
            }
        });
        $("#Cgst").rules("add", {
            required: true,
            messages: {
                required: "Please enter the CGST."
            }
        });
        $("#Sgst").rules("add", {
            required: true,
            messages: {
                required: "Please enter the SGST."
            }
        });
        $("#Igst").rules("add", {
            required: true,
            messages: {
                required: "Please enter the IGST."
            }
        });
        $("#Cgst").rules("add", {
            required: true,
            messages: {
                required: "Please enter the CGST."
            }
        });
        $("#Tcs").rules("add", {
            required: true,
            messages: {
                required: "Please enter the TCS."
            }
        });
        $("#Cartage").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Cartage."
            }
        });
        $("#FreightAmount").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Freight Amount."
            }
        });
        $("#OpeningStock").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Opening Stock."
            }
        });
        $("#ClosingStock").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Closing Stock."
            }
        });
       
        $("#Remarks").rules("add", {
            required: true,
            messages: {
                required: "Please enter the Remark."
            }
        });

        $("#CreatedOn").rules("add", {
            required: true,
            messages: {
                required: "Please select Created Date."
            }
        });
        $("#ModifiedOn").rules("add", {
            required: true,
            messages: {
                required: "Please select Modified Date."
            }
        });
        $("#ModifiedBy").rules("add", {
            required: true,
            messages: {
                required: "Please enter Modify By."
            }
        });
        $("#UploadInvoice").rules("add", {
            required: true,
            messages: {
                required: "Please upload Invoice File."
            }
        });

    });
