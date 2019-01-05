function tryGetMusicVideo(videoUrl) {
    const videoId = getVideoIdFromUrl(videoUrl);
    if (!videoId) {
        $('#musicVideo').hide();
    } else {
        const embeddedUrl = "//www.youtube.com/embed/" + videoId;
        $('#musicVideo').attr("src", embeddedUrl);
    }
}