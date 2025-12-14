"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.on("ReceiveMessage", function (user, message) {
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

document.getElementById("sendButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message)
        .catch(err => console.error(err.toString()));

    event.preventDefault();
});
