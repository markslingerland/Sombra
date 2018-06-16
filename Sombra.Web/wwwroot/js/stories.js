$('img').click(function () {
    document.getElementById('video-label').style.visibility = 'hidden';
        video = '<iframe src="' + $(this).attr('data-video') + '"></iframe>';
        $(this).replaceWith(video);
    });