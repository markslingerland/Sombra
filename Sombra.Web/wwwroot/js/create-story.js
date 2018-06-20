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
            document.getElementById('image1-box').style.opacity = 0;
            document.getElementById('video1-box').style.opacity = 0;
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
            document.getElementById('image2-box').style.opacity = 0;
            document.getElementById('video2-box').style.opacity = 0;
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
            document.getElementById('image3-box').style.opacity = 0;
            document.getElementById('video3-box').style.opacity = 0;
        };
        if (file) {
            reader.readAsDataURL(file);
        }
    }
});

$('.category-dropdown').click(function(){
    if ($('.checkbox-dropdown').hasClass("shown")) {
     $('.checkbox-dropdown').hide();
     $('.checkbox-dropdown').removeClass("shown");
     $('.category-dropdown').removeClass("category-dropdown-clicked");
 
    } else {
     $('.checkbox-dropdown').show();
     $('.checkbox-dropdown').addClass("shown");
     $('.category-dropdown').addClass("category-dropdown-clicked");
    }
 });

 let limitChecks = 2;
 $('.checkbox-dropdown').on('change', 'input[type="checkbox"]', function(evt) {
    if($(this).closest('.checkbox-dropdown').find(':checked').length > limitChecks) {
     $(this).prop('checked', false);
    }
 });
 

 $('#select-your-charity').selectize({
    sortField: 'text',
    maxItems: 1,
    create: false,
    valueField: 'id',
    labelField: 'title',
    searchField: 'title',
    placeholder: "BANK",
    options: [
        { id: 1, title: 'ABNA', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 2, title: 'ASNB', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 3, title: 'BUNQ', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 4, title: 'FTSB', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 5, title: 'INGB', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 6, title: 'KNAB', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 7, title: 'RABO', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 8, title: 'SNSB', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 9, title: 'TRIO', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 10, title: 'FVLB', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});