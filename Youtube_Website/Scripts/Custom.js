// write code fetching data from own API
function getUsers() {
    $.ajax({
        url: '/Api/GetUsersList/',
        method: 'Get',
        success: function (responce1) {
            console.log(responce1);
            if (responce1) {

                $('#users-div').html('');

                //$('#users-div').append('<ul id="users"></ul>');
                //$('<ul id="users"></ul?').appendTo('#users-div');

                for (let i = 0; i < responce1.length; i++)
                {
                    console.log(responce1[i].name);
                    $('#users').append('<li>' + responce1[i].name + ' | ' + responce1[i].email + '</li>');
                }
            }
        },
        error: function () {
            console.log('error occured');
        }
    });
}
$('#get-data-btn').click('click', getUsers);

function getvideosfunc() {
    $.ajax({
        url: '/Api/GetVideos/',
        method: 'Get',
        success: function (getVideoApi) {
            if (getVideoApi) {

                $('#show-video1').html('');

                for (let i = 0; i < getVideoApi.length; i++) {

                    $('#show-video1').append('<video src=' + getVideoApi[i].source + '  width="400" controls style="margin-right: 40px; display:inline"></video><h2>' + getVideoApi[i].title + '</h2><p>' + getVideoApi[i].description + '</p>');
                }
            }
        },
        error: function () {
            console.log('Error occured while consuming API from that Link');
        }
    });
}
$('#btn-click').click('click', getvideosfunc);