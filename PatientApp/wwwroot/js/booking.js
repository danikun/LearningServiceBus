"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/bookingHub").build();

connection.on("ConfirmBooking", () => {
    document.getElementById("confirm").submit();
});

connection.start().then(() => {
    connection.invoke("Subscribe", id).catch(err => {
        return console.error(err.toString());
    });    
}).catch(err => {
    return console.error(err.toString());
});