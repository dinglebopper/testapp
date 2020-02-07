function send(type, from, to, message, cb) {
  var xhr = new XMLHttpRequest();
  xhr.open('GET', '/send?type=' + type +
    '&from=' + from +
    '&to=' + escape(to) +
    '&message=' + escape(message), true);
  xhr.onload = function(e) {
    if(this.status != 200) return cb(null);
    cb(JSON.parse(this.responseText));
  };

  xhr.send();
}

function showDialog(msg) {
  document.querySelector('.dialog .info').innerHTML = msg;
  document.querySelector('.overlay').style.display = 'flex';
}

function handleDialogClose() {
  document.querySelector('.dialog .info').innerHTML = '';
  document.querySelector('.overlay').style.display = 'none';
}

function handleSendClick() {
  var checked = document.querySelector('input[name="type"]:checked');
  if(!checked) return showDialog('Please select a protocol');
  var type = document.querySelector('input[name="type"]:checked').value;
  var from = document.querySelector('input[name="from"]').value;
  if(from.trim() == '') return showDialog('Please enter a from');
  var to = document.querySelector('input[name="to"]').value;
  if(to.trim() == '') return showDialog('Please enter a to');
  var message = document.querySelector('textarea').value;
  if(message.trim() == '') return showDialog('Please enter a message');
  send(type, from, to, message, (r) => {
    showDialog(r.response);
  });
}

function load() {
  document.querySelector('.send a').addEventListener('click', handleSendClick);
  document.querySelector('.overlay .button a').addEventListener('click',
    handleDialogClose);
}

window.addEventListener('load', load);
