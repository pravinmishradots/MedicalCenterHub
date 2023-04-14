var parent;
var validPassCode;
$(document).ready(function () {
    validPassCode = false;
    $.validator.addMethod("validatePasscode", function () {
        return validPassCode;
    }, "*");
});

function SearchBegin(sender) {
    
    parent = $(sender).closest(".auto-address");
    var scriptTag = document.getElementById("postcodes4youelelement");
    var headTag = document.getElementsByTagName("head").item(0);
    var strUrl = "";
    var SearchTerm = $(parent).find(".postcode").val();
    var key = document.getElementById("postcodes4ukey").innerHTML;
    var user = document.getElementById("postcodes4uuser").innerHTML;

    if (SearchTerm.trim().indexOf(' ') < 0) {
        if (SearchTerm.trim() == "") {
            Global.Alert("Invalid Postcode", "Please enter postcode.");
            return;
        }
        Global.Alert("Invalid Postcode", "The postocde you have entered is invalid. Please ensure that you have entered space between the postcode.");
        return;
    }


    //Build the url
    strUrl = "https://services.3xsoftware.co.uk/Search/ByPostcode/json?";
    strUrl += "username=" + encodeURI(user);
    strUrl += "&key=" + encodeURI(key);
    strUrl += "&postcode=" + encodeURI(SearchTerm);
    strUrl += "&callback=SearchEnd";

    //Make the request
    if (scriptTag) {
        try {
            headTag.removeChild(scriptTag);
        }
        catch (e) {
            //Ignore
        }
    }
    scriptTag = document.createElement("script");
    scriptTag.src = strUrl;
    scriptTag.type = "text/javascript";
    scriptTag.id = "postcodes4youelelement";
    headTag.appendChild(scriptTag);
}

function SearchEnd(response) {
    //Test for an error  
    
    if (response != null && response['Error'] != null) {

        //Show the error message  
        if ($(parent).find('.errormsg').length > 0) {

            if (response['Error'].Id == 8) { // out of credit error
                $(parent).find('.errormsg').text("Service Temporarily Unavailable!").css({ "color": "gray", "font-size": "10px", "font-weight": "normal" });
            }
            else {
                //if ($(".postcode").length > 0) {
                //    $('.postcode').rules("add", {
                //        validatePasscode: true,
                //        validPassCode: true
                //    });
                //}
                //if ($("#SignatoryPostcode").length > 0) {
                //    $("#SignatoryPostcode").rules("add", {
                //        validatePasscode: true,
                //        validPassCode: true
                //    });
                //}
                $(parent).find('.errormsg').text("Invalid postcode");
            }
        } else {
            if (response['Error'].Id == 8)
                Global.Alert("Postcode Service", "Service Temporarily Unavailable!");
            else {
                //if ($(".postcode").length > 0) {
                //    $('.postcode').rules("add", {
                //        validatePasscode: true,
                //        validPassCode: true
                //    });
                //}
                //if ($("#SignatoryPostcode").length > 0) {
                //    $("#SignatoryPostcode").rules("add", {
                //        validatePasscode: true,
                //        validPassCode: true
                //    });
                //}
                Global.Alert("Invalid Postcode", response['Error'].Cause);
            }
        }

    }
    else {

        var addresslist = response["Summaries"];

        // console.log(addresslist);
        //Check if there were any items found
        if (addresslist.length == 0) {

            //$('.postcode').rules("add", {
            //    validatePasscode: true,
            //    validPassCode: true
            //});
            //if ($("#SignatoryPostcode").length > 0) {
            //    $("#SignatoryPostcode").rules("add", {
            //        validatePasscode: true,
            //        validPassCode: true
            //    });
            //}
            if ($(parent).find('.errormsg').length > 0) {
                $(parent).find('.errormsg').text("Invalid postcode");
            }
            else {
                Global.Alert("Invalid Postcode", "The postocde you have entered is invalid. Please ensure you have entered it correctly.");
            }
        }
        else {
            $(parent).find('.errormsg').text('');
            var dropdown = $(parent).find(".dropdown");
            $(parent).find(".post-dropdown").show();
            $(dropdown).css("display", "block");
            //dropdown.style.display = '';
            var html = "<option value=''>Select an address:</option>";
            //dropdown.options.length = 0;
            //dropdown.options.add(new Option("Select an address:", ""));
            for (var j = 0; j < addresslist.length; j++) {
                //dropdown.options.add(new Option(addresslist[j].StreetAddress + ", " + addresslist[j].Place, addresslist[j].Id));                
                html = html + "<option value='" + addresslist[j].Id + "'>" + addresslist[j].StreetAddress + ", " + addresslist[j].Place + "</option>";
            }

            //$('.postcode').rules("add", {
            //    validatePasscode: false,
            //    validPassCode: false
            //});

            //if ($("#SignatoryPostcode").length > 0) {
            //    $("#SignatoryPostcode").rules("add", {
            //        validatePasscode: false,
            //        validPassCode: false
            //    });
            //}
            $(dropdown).html(html);
        }
    }
}

function SearchIdBegin(sender) {
    parent = $(sender).closest(".auto-address");
    
    var scriptTag = document.getElementById("postcodes4youelelement");
    var headTag = document.getElementsByTagName("head").item(0);
    var strUrl = "";
    var Id = $(parent).find(".dropdown").val();
    var key = document.getElementById("postcodes4ukey").innerHTML;
    var user = document.getElementById("postcodes4uuser").innerHTML;

    //Build the url
    strUrl = "https://services.3xsoftware.co.uk/search/byid/json?";
    strUrl += "username=" + encodeURI(user);
    strUrl += "&key=" + encodeURI(key);
    strUrl += "&id=" + encodeURI(Id);

    strUrl += "&callback=SearchIdEnd";

    //Make the request
    if (scriptTag) {
        try {
            headTag.removeChild(scriptTag);
        }
        catch (e) {
            //Ignore
        }
    }
    scriptTag = document.createElement("script");
    scriptTag.src = strUrl;
    scriptTag.type = "text/javascript";
    scriptTag.id = "postcodes4youelelement";
    headTag.appendChild(scriptTag);
}

function SearchIdEnd(response) {
    //Test for an error
    if (response != null && response['Error'] != null) {
        //Show the error message
        //alert(response['Error'].Cause);
        if ($(parent).find('.errormsg').length > 0) {
            if (response['Error'].Id == 8) { // out of credit error
                $(parent).find('.errormsg').text("Service Temporarily Unavailable!").css({ "color": "gray", "font-size": "10px", "font-weight": "normal" });
            }
            else {
                $(parent).find('.errormsg').text("Invalid postcode");
            }
        }
        else {
            if (response['Error'].Id == 8)
                Global.Alert("Postcode Service", "Service Temporarily Unavailable!");
            else
                Global.Alert("Invalid Postcode", response['Error'].Cause);
        }
    }
    else {
        //Check if there were any items found
        if (response.length == 0) {
            // alert("Sorry, no matching items found");
            $(parent).find('.errormsg').text("Invalid postcode");
        }
        else {
            if ($(parent).find(".other-address-line").length) {
                $(parent).find(".other-address-line").val("");
            }
            var urlToCheck = document.URL.toLowerCase();

            $(parent).find('.errormsg').text("");
            var address = response["Address"];
            if (urlToCheck.indexOf("donor") != -1 || urlToCheck.indexOf("referrer") != -1 || urlToCheck.indexOf("family") != -1 || urlToCheck.indexOf("regulargift") != -1 || urlToCheck.indexOf("member") != -1 || urlToCheck.indexOf("certificateissuer") != -1 || urlToCheck.indexOf("foodbank") != -1 || urlToCheck.indexOf("superadmin") != -1) {
                $(parent).find(".street-name").val(address.SecondaryStreet.trim() != "" ? address.SecondaryStreet.trim() + ", " + address.PrimaryStreet.trim() : address.PrimaryStreet.trim());
                $(parent).find(".district").val(address.PostTown.trim());
                $(parent).find(".city").val(address.PostTown.trim());
                if (address.BuildingName == "")
                    $(parent).find(".house-name").val(address.Company.trim());
                else
                    $(parent).find(".house-name").val((address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim());
                $(parent).find(".house-number").val(address.BuildingNumber.trim());

                $(parent).find(".post-dropdown").hide();
                $(parent).find(".dropdown").css("display", "none");
                $(parent).find(".dropdown").html("");
            }
            else if (urlToCheck.indexOf("branch") != -1) {
                var controlvalue = "";
                if (address.BuildingNumber != "") {
                    if (address.BuildingName != "")
                        controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                    else if (address.Company != "")
                        controlvalue += address.Company.trim();

                }
                else if (address.BuildingName != "") {
                    controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                }
                else if (address.Company != "") {
                    controlvalue += address.Company.trim();
                }
                controlvalue += controlvalue.trim() != "" ? ",  " + address.BuildingNumber.trim() : address.BuildingNumber.trim();
                controlvalue += "  " + address.PrimaryStreet.trim();
                controlvalue += controlvalue.trim() != "" ? ",  " + address.PostTown.trim() : address.PostTown.trim();
                if (controlvalue.charAt(0) == ",")
                    controlvalue = controlvalue.substr(1);
                $(parent).find(".controlvalue").val(controlvalue.trim());
                $(parent).find(".post-dropdown").hide();
                $(parent).find(".dropdown").css("display", "none");
                $(parent).find(".dropdown").html("");
            }

            else if (urlToCheck.indexOf("user") != -1) {
                var controlvalue = "";
                if (address.BuildingNumber != "") {

                    if (address.BuildingName != "")
                        controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                    else if (address.Company != "")
                        controlvalue += address.Company.trim();



                }
                else if (address.BuildingName != "") {
                    controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                }
                else if (address.Company != "") {
                    controlvalue += address.Company.trim();
                }
                controlvalue += controlvalue.trim() != "" ? ",  " + address.BuildingNumber.trim() : address.BuildingNumber.trim();
                controlvalue += "  " + address.PrimaryStreet.trim();
                controlvalue += controlvalue.trim() != "" ? ",  " + address.PostTown.trim() : address.PostTown.trim();
                if (controlvalue.charAt(0) == ",")
                    controlvalue = controlvalue.substr(1);
                $(parent).find(".controlvalue").val(controlvalue.trim());
                $(parent).find(".controlcityvalue").val(address.PostTown.trim());
                $(parent).find(".post-dropdown").hide();
                $(parent).find(".dropdown").css("display", "none");
                $(parent).find(".dropdown").html("");
            }

            else if (urlToCheck.indexOf("agent") != -1 || urlToCheck.indexOf("updateprofile") != -1 || urlToCheck.indexOf("letter") != -1) {

                var controlvalue = "";
                if (address.BuildingNumber != "") {
                    if (address.BuildingName != "")
                        controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                    else if (address.Company != "")
                        controlvalue += address.Company.trim();

                }
                else if (address.BuildingName != "") {
                    controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                }
                else if (address.Company != "") {
                    controlvalue += address.Company.trim();
                }
                controlvalue += controlvalue.trim() != "" ? ",  " + address.BuildingNumber.trim() : address.BuildingNumber.trim();
                controlvalue += "  " + address.PrimaryStreet.trim();
                controlvalue += controlvalue.trim() != "" ? ",  " + address.PostTown.trim() : address.PostTown.trim();
                if (controlvalue.charAt(0) == ",")
                    controlvalue = controlvalue.substr(1);
                $(parent).find(".controlvalue").val(controlvalue.trim());
                $(parent).find(".post-dropdown").hide();
                $(parent).find(".dropdown").css("display", "none");
                $(parent).find(".dropdown").html("");
            }

            else if (urlToCheck.indexOf("charity") != -1) {
                var controlvalue = "";
                if (address.BuildingNumber != "") {

                    if (address.BuildingName != "")
                        controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                    else if (address.Company != "")
                        controlvalue += address.Company.trim();
                }
                else if (address.BuildingName != "") {
                    controlvalue += (address.Company.trim() + ", " + address.SubBuilding + " " + address.BuildingName).trim().trimChar(',').trim();
                }
                else if (address.Company != "") {
                    controlvalue += address.Company.trim();
                }

                controlvalue += controlvalue.trim() != "" ? ",  " + address.BuildingNumber.trim() : address.BuildingNumber.trim();
                controlvalue += "  " + address.PrimaryStreet.trim();
                controlvalue += controlvalue.trim() != "" ? ",  " + address.PostTown.trim() : address.PostTown.trim();
                if (controlvalue.charAt(0) == ",")
                    controlvalue = controlvalue.substr(1);
                $(parent).find(".controlvalue").val(controlvalue.trim());
                $(parent).find(".post-dropdown").hide();
                $(parent).find(".dropdown").css("display", "none");
                $(parent).find(".dropdown").html("");
            }


        }
    }
}
