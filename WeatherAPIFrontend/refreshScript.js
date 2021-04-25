let counter = 0;
setInterval(()=> {
counter++;
if(counter > 1) location.reload();
}, 1000)