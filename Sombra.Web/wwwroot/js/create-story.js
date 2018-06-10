$("document").ready(function () {
    document.getElementById('file').addEventListener('change', mainPic, true);
    document.getElementById('file-header').addEventListener('change', headerPic, true);
    function mainPic() {
        var file = document.getElementById("file").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('story-image').style.backgroundImage = "url(" + reader.result + ")";
        };
        if (file) {
            reader.readAsDataURL(file);
        } else {
        }
    }
    function headerPic() {
        var file = document.getElementById("file-header").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('header-image').style.backgroundImage = "url(" + reader.result + ")";
        };
        if (file) {
            reader.readAsDataURL(file);
        } else {
        }
    }
});
