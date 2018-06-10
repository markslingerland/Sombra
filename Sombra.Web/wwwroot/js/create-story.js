$("document").ready(function () {
    document.getElementById('file-header').addEventListener('change', headerPic, true);
    document.getElementById('file-profile-picute').addEventListener('change', profilePic, true);
    document.getElementById('file').addEventListener('change', mainPic, true);
    document.getElementById('file-2').addEventListener('change', second1Pic, true);
    document.getElementById('file-3').addEventListener('change', second2Pic, true);
    function profilePic() {
        var file = document.getElementById("file-profile-picute").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('shape-frame').style.backgroundImage = "url(" + reader.result + ")";
        };
        if (file) {
            reader.readAsDataURL(file);
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
        } 
    }

    function mainPic() {
        var file = document.getElementById("file").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('story-image').style.backgroundImage = "url(" + reader.result + ")";
        };
        if (file) {
            reader.readAsDataURL(file);
        } 
    }

    function second1Pic() {
        var file = document.getElementById("file-2").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('story-image-2').style.backgroundImage = "url(" + reader.result + ")";
        };
        if (file) {
            reader.readAsDataURL(file);
        }
    }

    function second2Pic() {
        var file = document.getElementById("file-3").files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById('story-image-3').style.backgroundImage = "url(" + reader.result + ")";
        };
        if (file) {
            reader.readAsDataURL(file);
        }
    }
});
