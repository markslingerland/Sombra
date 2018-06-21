$(document).ready(function () {
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
            document.getElementById('label-box').style.opacity = 0;
            document.getElementById('label-box').style.color = "#fff";
            document.getElementById('label-box').innerText = "Kies een andere video";
            document.getElementById('image-box').style.opacity = 0;
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
            document.getElementById('label1-box').style.opacity = 0;
            document.getElementById('label1-box').style.color = "#fff";
            document.getElementById('label1-box').innerText = "Kies een andere foto of video";
            $('#label1-box').addClass('has-shadow');
            $('.image1-box').css('opacity', '0');
            $('.video1-box').css('opacity', '0');
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
            document.getElementById('label2-box').style.opacity = 0;
            document.getElementById('label2-box').style.color = "#fff";
            document.getElementById('label2-box').innerText = "Kies een andere foto of video";
            $('#label2-box').addClass('has-shadow');
            $('.image2-box').css('opacity', '0');
            $('.video2-box').css('opacity', '0');
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
            document.getElementById('label3-box').style.opacity = 0;
            document.getElementById('label3-box').style.color = "#fff";
            document.getElementById('label3-box').innerText = "Kies een andere foto of video";
            $('#label3-box').addClass('has-shadow');
            $('.image3-box').css('opacity', '0');
            $('.video3-box').css('opacity', '0');
        };
        if (file) {
            reader.readAsDataURL(file);
        }
    }

    $.get(charitiesUrl, function(data) {
        var dropdownContent = $('#select-your-charity');
        $.each(data, function (index, value) {
            dropdownContent.append(`<option value="${value.charityKey}">${value.name}</option>`);
        });

        $('#select-your-charity').selectize({
            sortField: 'text',
            maxItems: 1,
            create: false,
            valueField: 'id',
            labelField: 'title',
            searchField: 'title',
            placeholder: "Naam van het goede doel..."
        });
    });
});