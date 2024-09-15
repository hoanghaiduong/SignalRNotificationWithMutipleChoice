var connection = new signalR.HubConnectionBuilder()
  .withUrl("/notificationHub")
  .build();
connection
  .start()
  .then(function () {
    console.log("Connected To Hub...");
  })
  .catch(function (err) {
    return console.error(err.toString());
  });
const Toast = Swal.mixin({
  toast: true,
  position: "top-end",
  showConfirmButton: false,
  timer: 5000,
  timerProgressBar: true,
  didOpen: (toast) => {
    toast.onmouseenter = Swal.stopTimer;
    toast.onmouseleave = Swal.resumeTimer;
  },
});

connection.on("Connected", function (response) {
  Toast.fire({
    icon: "info",
    title: response,
  });
  var username = $("#hfUsername").val();
  console.log(username);
  connection
    .invoke("SaveUserConnection", username)
    .then((res) => {
      console.log(res);
    })
    .catch(function (err) {
      return console.error(err.toString());
    });
});


connection.on("ReceivedNotification", function (message) {
    Toast.fire({
        icon: "success",
        title: message,
      });
});

connection.on("ReceivedPersonalNotification", function ( username,message) {
    Toast.fire({
        icon: "success",
        title: `Hi ${username}`,
        text: message
      });
      console.log("ReceivedPersonalNotification",username,message);
});

connection.on("Disconnected", function (response) {
  Toast.fire({
    icon: "error",
    title: response,
  });
});
