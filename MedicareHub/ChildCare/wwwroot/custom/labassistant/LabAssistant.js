(function ($) {
    function LabAssistantList() {

        var $this = this, formAddEditTreatment;
        function initializeGrid() {
            var gridLabAssistant = new Global.GridHelper('#grid-lab-list', {
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false
                    },
                    {
                        "targets": [1],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [2],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [3],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [4],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [5],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    //{
                    //    "targets": [6],
                    //    "sortable": true,
                    //    "searchable": false,
                    //    "data": "6",
                    //    "render": function (data, type, row, meta) {
                    //        var json = {
                    //            type: "checkbox",
                    //            class: "switchBox switch-small",
                    //            value: row[0],
                    //            'data-on': "success",
                    //            'data-off': "danger",
                    //            'data-on-text': 'ACTIVE',
                    //            'data-off-text': 'INACTIVE',
                    //        };

                    //        if (row[6].toLowerCase() === "True") {
                    //            json.checked = true;
                    //        }
                    //        return $('<input/>', json).get(0).outerHTML;
                    //    }
                    //},
                    {
                        "targets": [6],
                        "data": "0",
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, row, meta) {
                            var actionLink = $("<a/>", {
                                href: "/SuperAdmin/LabAssistantManager/AddEditLabAssistant/" + row[0],
                                id: "editTypeModal",
                                class: "btn btn-success btn-sm",
                                oncontextmenu: 'return false',
                                'data-bs-toggle': "modal",
                                'data-bs-target': "#modal-add-edit-lab",
                                html: $("<i/>", {
                                    class: "bi bi-pencil-square", title: "Edit LabAssistant"
                                }),
                            }).append().get(0).outerHTML + "&nbsp;"

                            actionLink += $("<a/>", {
                                href: "/SuperAdmin/LabAssistantManager/AddEditLabAssistant/" + row[0],
                                id: "deletePresenter",
                                class: "btn btn-danger btn-sm",
                                oncontextmenu: 'return false',
                                'data-bs-toggle': "modal",
                                'data-bs-target': "#modal-delete-lab",
                                html: $("<i/>", {
                                    class: "bi bi-trash", title: "Delete LabAssistant"
                                }),
                            }).append().get(0).outerHTML + "&nbsp;"
                        
                            return actionLink;
                        }
                    }
                ],
                "direction": "rtl",
                "bPaginate": true,
                "sPaginationType": "simple_numbers",
                "bProcessing": true,
                "bServerSide": true,
                "bAutoWidth": false,
                "stateSave": true,
                "sAjaxSource": "/SuperAdmin/LabAssistantManager/Index",
                "fnServerData": function (url, data, callback) {
                    $.ajax({
                        "url": url,
                        "data": data,
                        "success": callback,
                        "contentType": "application/x-www-form-urlencoded; charset=utf-8",
                        "dataType": "json",
                        "type": "POST",
                        "cache": false,
                        "error": function () {

                        }
                    });
                },
                "fnDrawCallback": function (oSettings) {
                    /*         initGridControlsWithEvents();*/
                    if (oSettings._iDisplayLength > oSettings.fnRecordsDisplay()) {
                        $(oSettings.nTableWrapper).find('.dataTables_paginate').hide();
                    }
                    else {
                        $(oSettings.nTableWrapper).find('.dataTables_paginate').show();
                    }
                },

                "stateSaveCallback": function (settings, data) {
                    localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data));
                },
                "stateLoadCallback": function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance));
                }
            });
            table = gridLabAssistant.DataTable();
        }

        //function initGridControlsWithEvents() {
        //    if ($('.switchBox').data('bootstrapSwitch')) {
        //        $('.switchBox').off('switchChange.bootstrapSwitch');
        //        $('.switchBox').bootstrapSwitch('destroy');
        //    }

        //    //$('.switchBox').bootstrapSwitch()
        //    //    .on('switch-change', function () {
        //    //        debugger;
        //    //        var switchElement = this;
        //    //        var id = this.value;
        //    //        $.get(Global.DomainName + "Admin/Treatment/UpdateStatus", { id: id }, function (result) {
        //    //            if (!result.isSuccess) {
        //    //                $(switchElement).bootstrapSwitch('toggleState', true);
        //    //            }
        //    //            else {
        //    //                alertify.success(result.data);
        //    //            }
        //    //        });
        //    //    });
        //}



        function initilizeModel() {
            $("#modal-add-edit-lab").on('shown.bs.modal', function (event) {
                var button = $(event.relatedTarget);
                var recipient1 = $(button).attr("href");
                $('#modal-add-edit-lab .modal-content').load(recipient1, function () {
                    formAddEditTreatment = new Global.FormHelper($(this).find("#frm-add-edit-treatment form"), { updateTargetId: "validation-summary" }, function (data) {
                        if (data.isSuccess == true) {
                            $("#validation-summary").html("");
                            $("#validation-summary").hide();
                            window.location.href = data.redirectUrl;
                        }
                        else {
                            //$("#validation-summary").text(data.data).show().delay(5000).fadeOut(2000);
                            alertify.error(data.message);
                        }
                    });
                    $('.form-checkbox').bootstrapSwitch();

                });
            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

            $("#modal-delete-lab").on('shown.bs.modal', function (event) {
                var button = $(event.relatedTarget);
                var recipient1 = $(button).attr("href");
                $('#modal-delete-lab .modal-content').load(recipient1, function () {
                    formaddEditMedicine = new Global.FormHelper($('#form'),
                        { updateTargetId: "validation-summary" }, function (data) {

                        });
                });
            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

        }
        $this.init = function () {

            initializeGrid();
            initilizeModel();
        };

    }
    $(function () {
        var self = new LabAssistantList();
        self.init();
    });
})(jQuery);