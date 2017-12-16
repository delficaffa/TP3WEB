function ValidateCreate() {
    var obj = document.getElementByName("Price");
    var realStringObj = obj && obj.toString();
    return !jQuery.isArray(obj) && (realStringObj - parseFloat(realStringObj) + 1) >= 0;
}