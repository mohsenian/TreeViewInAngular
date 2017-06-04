function JSFunctionValidate() {
    if ((document.getElementById("parnetDropDown").selectedIndex == 0) || (document.getElementById("childDropDown").selectedIndex == 0)) {
        alert("Please select car and car model form dropdwon list");
        return false;
    }
    return true;
}