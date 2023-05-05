const textarea = document.getElementById("body")
const iframe = document.getElementById("iframe-body")


function insert(startTag, endTag) {
    const selectionStart = textarea.selectionStart;
    const selectionEnd = textarea.selectionEnd;
    const oldText = textarea.value;
  
    const prefix = oldText.substring(0, selectionStart);
    const inserted =
      startTag + oldText.substring(selectionStart, selectionEnd) + endTag;
    const suffix = oldText.substring(selectionEnd);
    textarea.value = `${prefix}${inserted}${suffix}`;
    iframe.srcdoc = `<!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
    </head>
    <body>
        <style>
        html {
          width:490px;
          height:490px;
          font-size: 18px;
          margin: 0;
          box-sizing: border-box;
          font-family: 'Poppins', sans-serif;
          padding: 0;
          word-wrap:break-word;
          border:2px solid #424242;
        }
            .quote
    {
      padding: 2px 5px;
      font-style: italic;
      width: fit-content;
    }
    .quote-author
    {
      font-style: normal;
      font-weight: bold;
    }
        </style>` + textarea.value + `</body>
        </html>`
  
    const newSelectionStart = selectionStart + startTag.length;
    const newSelectionEnd = selectionEnd + startTag.length;
    textarea.setSelectionRange(newSelectionStart, newSelectionEnd);
  
    textarea.focus();
  }

textarea.addEventListener('input',function()
{
  iframe.srcdoc = `<!DOCTYPE html>
  <html lang="en">
  <head>
      <meta charset="UTF-8">
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
  </head>
  <body>
      <style>
      html {
        width:490px;
        height:490px;
        font-size: 18px;
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        font-family: 'Poppins', sans-serif;
        word-wrap:break-word;
        border:2px solid #424242;
      }
          .quote
  {
    padding: 5px 2px;
    font-style: italic;
    width: fit-content;
  }
  .quote-author
  {
    font-style: normal;
    font-weight: bold;
  }
      </style>` + textarea.value + `</body>
      </html>`
})


$('#quote').click(function () { 
    insert('<blockquote class="quote"><p>"','"</p><p class="quote-author"></p></blockquote>')
});

$('#body').on('keypress', function (e) {
    var code = e.keyCode || e.which;
    if(code == 13)
    {
       insert('<br>', "")
    }
});

$('input[type=file]').on('change', function(event) {
    var file = event.target.files[0];
    var reader = new FileReader();
    reader.onload = function(event) {
      var byteArray = new Uint8Array(event.target.result);
      insert("<image class='img-blog' src='data:image/png;base64," + btoa(String.fromCharCode.apply(null,  byteArray)) + "'/>","")
    };
    reader.readAsArrayBuffer(file);
  });

  $('#h1').click(function()
  {
    insert("<h1>","</h1>")
  })
  $('#h2').click(function()
  {
    insert("<h2>","</h2>")
  })
  $('#h3').click(function()
  {
    insert("<h3>","</h3>")
  })
  $('#h4').click(function()
  {
    insert("<h4>","</h4>")
  })
  $('#h5').click(function()
  {
    insert("<h5>","</h5>")
  })
  
  $('#h6').click(function()
  {
    insert("<h6>","</h6>")
  })

  $('#italic').click(
    function()
    {
      insert('<i>',"</i>")
    }
  )

  $('#bold').click(
    function()
    {
      insert('<b>',"</b>")
    }
  )

  
  $('#link').click(
    function()
    {
      insert('<a href="">', "</a>")
    }
  )
  