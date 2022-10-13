// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings("custom-file-label").addClass(selected).html(fileName);
});

function CalcTotalExperiences() {
    var x = document.getElementsByClassName('YearsWorked');
    var totalExp = 0;
    var i;
    for (i = 0; i < x.length(); i++) {
        totalExp = totalExp + eval(x[i].value);
    }
    document.getElementById('TotalExperience').ariaValueMax = totalExp;
    return;
}

document.addEventListener('change', function (e) {
    if (e.target.classList.contains('YearsWorked')) {
        CalcTotalExperiences();
    }
}
    , false);


function DeleteItem(btn) {
    //to prevent user from deleting first row
    var table = document.getElementById('ExpTable');

    if (btn.id.indexOf("Inst")>0)
        table = document.getElementById('InstructionTable');
    else
        table = document.getElementById('ExpTable');

    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2)// 2 because header is also considered a row
    {
        alert("This row Cannot be Deleted ");
        return;
    }
    var btnIdx = btn.id.replaceAll('btnremove-', '');
    /* var idOfIsDeleted = btnIdx + "__IsDeleted";*/

    if (btn.id.indexOf("Inst") > 0)
        btnIdx = btn.id.replaceAll('btnRemoveInst-', '');

    var idOfIsDeleted = btnIdx + "__IsDeleted";

    if (btn.id.indexOf("Inst") > 0)
        idOfIsDeleted = btnIdx + "__IsHidden";


    var hidIsDelId = document.querySelector("[id$='" + idOfIsDeleted + "']").id;
    document.getElementById(hidIsDelId).value = "true";

    //  $(btn).closest('tr').remove();
    $(btn).closest('tr').hide(); // we dont actually delete the row but it will hide
    CalcTotalExperiences()
}

function AddItem(btn) {

    var table = document.getElementById('ExpTable');// we gave the table an id

    if (btn.id =='btnAddInstruction')
        table = document.getElementById('InstructionTable');
    else
        table = document.getElementById('ExpTable');


    var rows = table.getElementsByTagName('tr'); //we get the rows of the table

    var rowOuterHtml = rows[rows.length - 1].outerHTML; //the last row, but we are clonign last row, whci gives them all same id

    // teh follwing lines save the solution
    var lastrowIdx = rows.length - 2//document.getElementById('hdnLastIndex').value;

    var nextrowIdx = eval(lastrowIdx) + 1;// incrementing the index so that it won't stay by the last row

    //document.getElementById('hdnLastIndex').value = nextrowIdx;

    // rowouterHtml holds the source of the last row of the table
    // so we are replacing the old index with the new index
    rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);

    // then we attach the new row to the table
    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;

    //// this will show the add and delete buttons at the right time
    //var btnAddID=btn.id;
    //var btnDeleteid=btnAddID.replace('btnadd', 'btnremove');

    //var delbtn=document.getElementById(btnDeleteid);
    //delbtn.classList.add("visible");
    //delbtn.classList.remove("invisible");

    //var addbtn=document.getElementById(btnAddID);
    //addbtn.classList.remove("visible");
    //addbtn.classList.add("invisible");

    // in order when cloning, that the cloned line is empty
    var x = document.getElementsByTagName("input");

    for (var cnt = 0; cnt < x.length; cnt++) {
        if (x[cnt].type == "text" && x[cnt].id.indexOf('_' + nextRowIdx + '_') > 0)// check if it is a textcontorl
            x[cnt].value = ' ';
        else if (x[cnt].type == "number" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)// check if it is a textcontorl
            x[cnt].value = ' ';
    }

    rebindvalidators(); // in order to add validation to dynaimcally added buttons.

}

// in order for validation to work on dynamically made fields by javascript
function rebindvalidators() {
    var $form = $("#ApplicantForm"); // reference of form by id
    $form.unbind();// reomve all contorls
    $form.data("validator", null); // clear all validator
    $.validator.unobtrusive.parse($form);// add validators again
    $form.validate($form.data("unobtrusiveValidation").options); // rebind it to the js ploding
}

// in order for stars review to work
function SubmitComment() {
    if ($("#Rating").val() == "0") {
        alert("Please rate this service provider.");
        return false;
    }
    else {
        return true;
    }
}
function CRate(r) {
    $("#Rating").val(r);
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
    // unselect remaining
    for (var i = r + 1; i <= 5; i++) {
        $("#Rate" + i).attr('class', 'starFade');
    }
}
function CRateOver(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
}
function CRateOut(r) {
    for (var i = 1; i <= r; i++) {
        $("#Rate" + i).attr('class', 'starFade');
    }
}
function CRateSelected() {
    var setRating = $("#Rating").val();
    for (var i = 1; i <= setRating; i++) {
        $("#Rate" + i).attr('class', 'starGlow');
    }
}

function SCRate() {
    for (var i = 1; i <= @totalRating; i++) {
        $("#sRate" + i).attr('class', 'starGlowN');
    }
}
$(function () {
    SCRate();
});

