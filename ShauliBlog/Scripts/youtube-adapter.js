
function search() {
    q = $("#query").val();

    $.get(
        "https://www.googleapis.com/youtube/v3/search", {
            part: 'snippet, id',
            q: q,
            type: 'video',
            key: 'AIzaSyA5ScoMprnv9ikhsD6eLujlyTRhfScf9pw'
        },
        function (data) {
            var output = {};
            var items = [];

            $.each(data.items, function (i, item) {
                items.push(getOutput(item));
            });

            output.items = items;

            localStorage.setItem("youtubeData", JSON.stringify(output));
            console.log(localStorage.getItem("youtubeData"));
        }
    ).then(function () {
        var table = buildTable();

        $('#results').append(table);
    });
}

function buildTable() {
    var input = JSON.parse(localStorage.getItem("youtubeData"));

    var output =
        "<table>" +
        "   <thead>" +
        "       <td>Name</td>" +
        "       <td>Thumb</td>" +
        "   </thead>" +
        "   <tbody>";
    $.each(input.items, function (index, item) {
        output += "<tr>" +
            "<td>" +
                 "<a href=\"javascript:void(0);\" onclick=\"changeURL('" + item.videoId + "')\">" + item.title + "</a>" +
            "</td>" +
             "<td>" +
                 "<img width=\"100px\" height=\"100px\" src=\"" + item.thumb + "\" />"
        "</td>" +
   "</tr>"
    });

    output +=
        "   </tbody>" +
        "</table>";

    return output;
}

function getOutput(item) {
    var output = {
        videoId: item.id.videoId,
        title: item.snippet.title,
        thumb: item.snippet.thumbnails.high.url
    };
    return output;
}

$("#search-form").submit(function (e) {
    e.preventDefault();
});

function changeURL(youtubeId) {
    $('#VideoUrl').val(youtubeId);
}
